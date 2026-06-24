using System.Diagnostics;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;

namespace Milvus.IntegrationTests;

public sealed class Environment : IAsyncDisposable
{
    private const string MilvusImage = "milvusdb/milvus:v2.5.27";
    private const ushort MilvusPort = 19530;
    private const ushort MilvusHealthPort = 9091;
    private const string DefaultApiKey = "root:Milvus";
    private static readonly TimeSpan StartupTimeout = TimeSpan.FromMinutes(3);

    public IContainer? Container { get; init; }
    public required MilvusClient Client { get; init; }

    public async ValueTask DisposeAsync()
    {
        Client.Dispose();
        if (Container != null)
        {
            await Container.DisposeAsync();
        }
    }

    public static async Task<Environment> PrepareAsync(EnvironmentType? environmentType = null)
    {
        environmentType ??= InferEnvironment();
        switch (environmentType)
        {
            case EnvironmentType.Local:
            {
                var client = new MilvusClient(
                    DefaultApiKey,
                    baseUri: new Uri($"http://127.0.0.1:{MilvusPort}"));

                return new Environment
                {
                    Client = client,
                };
            }
            case EnvironmentType.Container:
            {
                var container = new ContainerBuilder(MilvusImage)
                    .WithPortBinding(MilvusPort, assignRandomHostPort: true)
                    .WithPortBinding(MilvusHealthPort, assignRandomHostPort: true)
                    .WithEnvironment("ETCD_USE_EMBED", "true")
                    .WithEnvironment("ETCD_DATA_DIR", "/var/lib/milvus/etcd")
                    .WithEnvironment("COMMON_STORAGETYPE", "local")
                    .WithCommand("milvus", "run", "standalone")
                    .WithWaitStrategy(
                        Wait.ForUnixContainer()
                            .UntilHttpRequestIsSucceeded(request => request
                                .ForPath("/healthz")
                                .ForPort(MilvusHealthPort)))
                    .Build();

                using var cts = new CancellationTokenSource(StartupTimeout);
                await container.StartAsync(cts.Token);

                var client = new MilvusClient(
                    DefaultApiKey,
                    baseUri: new UriBuilder(
                        Uri.UriSchemeHttp,
                        container.Hostname,
                        container.GetMappedPublicPort(MilvusPort)).Uri);

                return new Environment
                {
                    Container = container,
                    Client = client,
                };
            }
            default:
                throw new ArgumentOutOfRangeException(nameof(environmentType), environmentType, null);
        }
    }

    private static EnvironmentType InferEnvironment()
    {
        if (System.Environment.GetEnvironmentVariable("MILVUS_TEST_ENVIRONMENT") is { Length: > 0 } environmentValue &&
            Enum.TryParse<EnvironmentType>(environmentValue, ignoreCase: true, out var environmentType))
        {
            return environmentType;
        }

        if (IsDockerAvailable())
        {
            return EnvironmentType.Container;
        }

#if DEBUG
        return EnvironmentType.Local;
#else
        return EnvironmentType.Container;
#endif
    }

    private static bool IsDockerAvailable()
    {
        try
        {
            using var process = Process.Start(new ProcessStartInfo
            {
                FileName = "docker",
                ArgumentList = { "info", "--format", "{{.ServerVersion}}" },
                RedirectStandardOutput = true,
                RedirectStandardError = true,
            });

            return process is not null &&
                   process.WaitForExit(milliseconds: 5000) &&
                   process.ExitCode == 0;
        }
        catch
        {
            return false;
        }
    }
}

public enum EnvironmentType
{
    Local,
    Container,
}

# Authentication

The Milvus REST API uses Bearer token authentication.

## Getting Your Token

Milvus supports token-based authentication when configured with `authorizationEnabled` set to `true` in the server configuration. The default root credentials are `root:Milvus`.

You can obtain a token by using the root credentials or a user-specific token provided by your Milvus deployment.

## Using the SDK

```csharp
using Milvus;

// Create a client with Bearer token authentication
using var client = new MilvusClient(apiKey: "your-token-here");
```

## Custom Base URL

If your Milvus instance is running on a custom host or port, you can specify the base URL:

```csharp
using var client = new MilvusClient(apiKey: "your-token-here")
{
    BaseUri = new Uri("http://your-milvus-host:19530"),
};
```

## Environment Variables

For integration tests and CI/CD pipelines, set the API key via environment variable:

```bash
export MILVUS_API_KEY="your-token-here"
```

```csharp
var apiKey =
    Environment.GetEnvironmentVariable("MILVUS_API_KEY") is { Length: > 0 } value
        ? value
        : throw new AssertInconclusiveException("MILVUS_API_KEY environment variable is not found.");

using var client = new MilvusClient(apiKey: apiKey);
```

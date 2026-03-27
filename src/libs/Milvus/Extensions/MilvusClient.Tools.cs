#pragma warning disable CS3002 // Return type is not CLS-compliant

using System.Text.Json;
using Microsoft.Extensions.AI;

namespace Milvus;

/// <summary>
/// Extensions for using MilvusClient as MEAI tools with any IChatClient.
/// </summary>
public static class MilvusClientToolExtensions
{
    /// <summary>
    /// Creates an <see cref="AIFunction"/> that searches vectors by similarity
    /// in a Milvus collection. This is the most important vector database operation.
    /// </summary>
    /// <param name="client">The Milvus client to use.</param>
    /// <param name="defaultLimit">Default maximum number of results to return (default: 10).</param>
    /// <returns>An AIFunction that can be passed to ChatOptions.Tools.</returns>
    public static AIFunction AsSearchVectorsTool(
        this MilvusClient client,
        int defaultLimit = 10)
    {
        ArgumentNullException.ThrowIfNull(client);

        return AIFunctionFactory.Create(
            async (
                string collectionName,
                string vectorJson,
                string? filter,
                int? limit,
                string? outputFieldsJson,
                CancellationToken cancellationToken) =>
            {
                var vectorData = JsonSerializer.Deserialize<List<List<float>>>(vectorJson)
                    ?? throw new ArgumentException("Invalid vector JSON. Expected a JSON array of float arrays.", nameof(vectorJson));

                var vector = vectorData
                    .Select(v => (IList<AnyOf<int?, string>>)v
                        .Select(f => new AnyOf<int?, string>(value1: null, value2: f.ToString(System.Globalization.CultureInfo.InvariantCulture)))
                        .ToList())
                    .ToList();

                List<string>? outputFields = null;
                if (!string.IsNullOrWhiteSpace(outputFieldsJson))
                {
                    outputFields = JsonSerializer.Deserialize<List<string>>(outputFieldsJson);
                }

                var response = await client.VectorOperationsV2.CreateVectordbEntitiesSearchAsync(
                    collectionName: collectionName,
                    vector: vector,
                    searchParams: new SearchParams(),
                    filter: filter,
                    limit: limit ?? defaultLimit,
                    outputFields: outputFields,
                    cancellationToken: cancellationToken).ConfigureAwait(false);

                return FormatSearchResponse(response);
            },
            name: "SearchVectors",
            description: "Searches for similar vectors in a Milvus collection using vector similarity. Provide the collection name and query vectors as a JSON array of float arrays (e.g. '[[0.1, 0.2, 0.3]]'). Optionally specify a filter expression, result limit, and output fields as a JSON array of field names.");
    }

    /// <summary>
    /// Creates an <see cref="AIFunction"/> that inserts vectors/entities
    /// into a Milvus collection.
    /// </summary>
    /// <param name="client">The Milvus client to use.</param>
    /// <returns>An AIFunction that can be passed to ChatOptions.Tools.</returns>
    public static AIFunction AsInsertVectorsTool(this MilvusClient client)
    {
        ArgumentNullException.ThrowIfNull(client);

        return AIFunctionFactory.Create(
            async (
                string collectionName,
                string dataJson,
                string? partitionName,
                CancellationToken cancellationToken) =>
            {
                var dataList = JsonSerializer.Deserialize<List<object>>(dataJson)
                    ?? throw new ArgumentException("Invalid data JSON. Expected a JSON array of objects.", nameof(dataJson));

                var data = new AnyOf<CreateVectordbEntitiesInsertRequestData, IList<object>>(
                    value1: null,
                    value2: dataList);

                var response = await client.VectorOperationsV2.CreateVectordbEntitiesInsertAsync(
                    collectionName: collectionName,
                    data: data,
                    partitionName: partitionName,
                    cancellationToken: cancellationToken).ConfigureAwait(false);

                return FormatInsertResponse(response);
            },
            name: "InsertVectors",
            description: "Inserts entities (vectors and associated data) into a Milvus collection. Provide the collection name and data as a JSON array of objects, where each object contains the fields matching the collection schema (e.g. '[{\"id\": 1, \"vector\": [0.1, 0.2], \"text\": \"hello\"}]'). Optionally specify a partition name.");
    }

    /// <summary>
    /// Creates an <see cref="AIFunction"/> that creates a new collection
    /// in Milvus using the quick-setup mode.
    /// </summary>
    /// <param name="client">The Milvus client to use.</param>
    /// <param name="requestTimeout">Request timeout in seconds (default: 30).</param>
    /// <returns>An AIFunction that can be passed to ChatOptions.Tools.</returns>
    public static AIFunction AsCreateCollectionTool(
        this MilvusClient client,
        int requestTimeout = 30)
    {
        ArgumentNullException.ThrowIfNull(client);

        return AIFunctionFactory.Create(
            async (
                string collectionName,
                int dimension,
                string? metricType,
                string? description,
                CancellationToken cancellationToken) =>
            {
                await client.CollectionOperationsV2.CreateVectordbCollectionsCreateAsync(
                    requestTimeout: requestTimeout,
                    collectionName: collectionName,
                    dimension: dimension,
                    metricType: metricType ?? "COSINE",
                    cancellationToken: cancellationToken).ConfigureAwait(false);

                return $"Collection '{collectionName}' created successfully with dimension {dimension} and metric type {metricType ?? "COSINE"}.";
            },
            name: "CreateCollection",
            description: "Creates a new vector collection in Milvus using quick-setup mode. Provide the collection name and vector dimension. Optionally specify the metric type (L2, IP, or COSINE; default: COSINE) and a description.");
    }

    /// <summary>
    /// Creates an <see cref="AIFunction"/> that lists all collections
    /// in a Milvus database.
    /// </summary>
    /// <param name="client">The Milvus client to use.</param>
    /// <param name="dbName">The database name (default: "default").</param>
    /// <returns>An AIFunction that can be passed to ChatOptions.Tools.</returns>
    public static AIFunction AsListCollectionsTool(
        this MilvusClient client,
        string dbName = "default")
    {
        ArgumentNullException.ThrowIfNull(client);

        return AIFunctionFactory.Create(
            async (CancellationToken cancellationToken) =>
            {
                // The List Collections endpoint returns void in the generated client,
                // so we use the request-based overload and make the HTTP call manually
                // to capture the response body.
                var request = new CreateVectordbCollectionsListRequest
                {
                    DbName = dbName,
                };

                using var httpRequest = new System.Net.Http.HttpRequestMessage(
                    System.Net.Http.HttpMethod.Post,
                    new Uri(
                        (client.HttpClient.BaseAddress ?? new Uri(MilvusClient.DefaultBaseUrl))
                            .ToString().TrimEnd('/') + "/v2/vectordb/collections/list"));

                httpRequest.Content = new System.Net.Http.StringContent(
                    JsonSerializer.Serialize(request),
                    System.Text.Encoding.UTF8,
                    "application/json");

                // Copy authorization headers
                foreach (var auth in client.Authorizations)
                {
                    if (auth.Type is "Http" or "OAuth2")
                    {
                        httpRequest.Headers.Authorization =
                            new System.Net.Http.Headers.AuthenticationHeaderValue(auth.Name, auth.Value);
                    }
                    else if (auth.Type == "ApiKey" && auth.Location == "Header")
                    {
                        httpRequest.Headers.Add(auth.Name, auth.Value);
                    }
                }

                using var httpResponse = await client.HttpClient.SendAsync(
                    httpRequest, cancellationToken).ConfigureAwait(false);

                httpResponse.EnsureSuccessStatusCode();

                var content = await httpResponse.Content.ReadAsStringAsync(
                    cancellationToken).ConfigureAwait(false);

                using var doc = JsonDocument.Parse(content);
                var root = doc.RootElement;

                var parts = new List<string>();

                if (root.TryGetProperty("data", out var dataElement) &&
                    dataElement.ValueKind == JsonValueKind.Array)
                {
                    var collections = new List<string>();
                    foreach (var item in dataElement.EnumerateArray())
                    {
                        collections.Add(item.GetString() ?? item.ToString());
                    }

                    if (collections.Count > 0)
                    {
                        parts.Add($"Collections ({collections.Count}):");
                        foreach (var name in collections)
                        {
                            parts.Add($"  - {name}");
                        }
                    }
                    else
                    {
                        parts.Add("No collections found.");
                    }
                }
                else
                {
                    parts.Add("No collections found.");
                }

                return string.Join("\n", parts);
            },
            name: "ListCollections",
            description: "Lists all vector collections in the Milvus database. Returns the collection names available in the current database.");
    }

    /// <summary>
    /// Creates an <see cref="AIFunction"/> that describes a collection
    /// in Milvus, returning its schema, fields, indexes, and load status.
    /// </summary>
    /// <param name="client">The Milvus client to use.</param>
    /// <param name="dbName">The database name (default: "default").</param>
    /// <returns>An AIFunction that can be passed to ChatOptions.Tools.</returns>
    public static AIFunction AsDescribeCollectionTool(
        this MilvusClient client,
        string dbName = "default")
    {
        ArgumentNullException.ThrowIfNull(client);

        return AIFunctionFactory.Create(
            async (string collectionName, CancellationToken cancellationToken) =>
            {
                var response = await client.CollectionOperationsV2.CreateVectordbCollectionsDescribeAsync(
                    dbName: dbName,
                    collectionName: collectionName,
                    cancellationToken: cancellationToken).ConfigureAwait(false);

                return FormatDescribeCollectionResponse(response);
            },
            name: "DescribeCollection",
            description: "Describes a Milvus collection, returning its schema with field names and types, indexes, load status, and other metadata. Use this to understand the structure of a collection before searching or inserting data.");
    }

    /// <summary>
    /// Creates an <see cref="AIFunction"/> that queries vectors/entities
    /// in a Milvus collection by a scalar filter expression.
    /// </summary>
    /// <param name="client">The Milvus client to use.</param>
    /// <returns>An AIFunction that can be passed to ChatOptions.Tools.</returns>
    public static AIFunction AsQueryVectorsTool(this MilvusClient client)
    {
        ArgumentNullException.ThrowIfNull(client);

        return AIFunctionFactory.Create(
            async (
                string collectionName,
                string filter,
                string? outputFieldsJson,
                CancellationToken cancellationToken) =>
            {
                List<string>? outputFields = null;
                if (!string.IsNullOrWhiteSpace(outputFieldsJson))
                {
                    outputFields = JsonSerializer.Deserialize<List<string>>(outputFieldsJson);
                }

                var response = await client.VectorOperationsV2.CreateVectordbEntitiesQueryAsync(
                    collectionName: collectionName,
                    filter: filter,
                    outputFields: outputFields,
                    cancellationToken: cancellationToken).ConfigureAwait(false);

                return FormatQueryResponse(response);
            },
            name: "QueryVectors",
            description: "Queries entities in a Milvus collection using a scalar filter expression (e.g. 'id in [1, 2, 3]' or 'age > 25'). Unlike vector search, this filters by scalar fields without vector similarity. Optionally specify output fields as a JSON array of field names to return.");
    }

    /// <summary>
    /// Creates an <see cref="AIFunction"/> that deletes vectors/entities
    /// from a Milvus collection by a filter expression.
    /// </summary>
    /// <param name="client">The Milvus client to use.</param>
    /// <returns>An AIFunction that can be passed to ChatOptions.Tools.</returns>
    public static AIFunction AsDeleteVectorsTool(this MilvusClient client)
    {
        ArgumentNullException.ThrowIfNull(client);

        return AIFunctionFactory.Create(
            async (
                string collectionName,
                string filter,
                string? partitionName,
                CancellationToken cancellationToken) =>
            {
                var response = await client.VectorOperationsV2.CreateVectordbEntitiesDeleteAsync(
                    collectionName: collectionName,
                    filter: filter,
                    partitionName: partitionName,
                    cancellationToken: cancellationToken).ConfigureAwait(false);

                return FormatDeleteResponse(response);
            },
            name: "DeleteVectors",
            description: "Deletes entities from a Milvus collection using a filter expression (e.g. 'id in [1, 2, 3]' or 'status == \"inactive\"'). Optionally specify a partition name to delete from a specific partition. This action is irreversible.");
    }

    private static string FormatSearchResponse(CreateVectordbEntitiesSearchResponse response)
    {
        var parts = new List<string>();

        if (response.Code is { } code && code != 0)
        {
            parts.Add($"Error (code {code})");
            return string.Join("\n", parts);
        }

        if (response.Data is { Count: > 0 })
        {
            parts.Add($"Found {response.Data.Count} results:");
            foreach (var item in response.Data)
            {
                parts.Add($"- {JsonSerializer.Serialize(item)}");
            }
        }
        else
        {
            parts.Add("No results found.");
        }

        return string.Join("\n", parts);
    }

    private static string FormatInsertResponse(HttpapiGenericRespCustomerInsertResp response)
    {
        var parts = new List<string>();

        if (response.Code is { } code && code != 0)
        {
            parts.Add($"Error (code {code})");
            return string.Join("\n", parts);
        }

        if (response.Data is { } data)
        {
            parts.Add($"Inserted {data.InsertCount} entities.");
            if (data.InsertIds is { Count: > 0 })
            {
                parts.Add($"IDs: {string.Join(", ", data.InsertIds)}");
            }
        }
        else
        {
            parts.Add("Insert completed.");
        }

        return string.Join("\n", parts);
    }

    private static string FormatDescribeCollectionResponse(CreateVectordbCollectionsDescribeResponse response)
    {
        var parts = new List<string>();
        var data = response.Data;

        parts.Add($"Collection: {data.CollectionName}");
        parts.Add($"Description: {data.Description}");
        parts.Add($"Load Status: {data.Load}");
        parts.Add($"Auto ID: {data.AutoID}");
        parts.Add($"Dynamic Fields: {data.EnableDynamicField}");
        parts.Add($"Shards: {data.ShardsNum}");

        if (data.Fields is { Count: > 0 })
        {
            parts.Add("Fields:");
            foreach (var field in data.Fields)
            {
                var entry = $"  - {field.Name} ({field.Type})";
                if (field.PrimaryKey)
                {
                    entry += " [PK]";
                }
                if (field.AutoId)
                {
                    entry += " [AutoID]";
                }
                if (field.PartitionKey)
                {
                    entry += " [PartitionKey]";
                }
                if (!string.IsNullOrWhiteSpace(field.Description))
                {
                    entry += $" - {field.Description}";
                }
                parts.Add(entry);
            }
        }

        if (data.Indexes is { Count: > 0 })
        {
            parts.Add("Indexes:");
            foreach (var index in data.Indexes)
            {
                parts.Add($"  - {JsonSerializer.Serialize(index)}");
            }
        }

        return string.Join("\n", parts);
    }

    private static string FormatQueryResponse(CreateVectordbEntitiesQueryResponse response)
    {
        var parts = new List<string>();

        if (response.Code is { } code && code != 0)
        {
            parts.Add($"Error (code {code})");
            return string.Join("\n", parts);
        }

        if (response.Data is { Count: > 0 })
        {
            parts.Add($"Found {response.Data.Count} entities:");
            foreach (var item in response.Data)
            {
                parts.Add($"- {JsonSerializer.Serialize(item)}");
            }
        }
        else
        {
            parts.Add("No entities found.");
        }

        return string.Join("\n", parts);
    }

    private static string FormatDeleteResponse(HttpapiGenericRespCustomerDeleteResp response)
    {
        var parts = new List<string>();

        if (response.Code is { } code && code != 0)
        {
            parts.Add($"Error (code {code})");
            return string.Join("\n", parts);
        }

        parts.Add("Delete completed successfully.");

        return string.Join("\n", parts);
    }
}

/*
order: 20
title: Vectors
slug: vectors

Insert, search, query, and delete vectors in a Milvus collection.
*/

using System.Net.Http.Json;
using System.Text.Json;

namespace Milvus.IntegrationTests;

public partial class Tests
{
    //// Create a collection, insert vectors with data,
    //// search for nearest neighbors, query by filter, and delete entities.

    [TestMethod]
    public async Task Example_Vectors()
    {
        var client = Client;
        var collectionName = $"test_vectors_{Guid.NewGuid():N}";

        //// Create a collection with 4-dimensional vectors using Cosine distance.

        await client.CollectionOperationsV2.CreateVectordbCollectionsCreateAsync(
            requestTimeout: 30,
            collectionName: collectionName,
            dimension: 4,
            metricType: "COSINE",
            autoId: false,
            primaryFieldName: "id",
            vectorFieldName: "vector");

        //// Insert vectors with associated data into the collection.
        //// Note: Row-based insert with mixed types (int, float[]) requires raw JSON
        //// because the source-generated serializer cannot handle polymorphic object arrays.

        using var insertHttpResponse = await client.HttpClient.PostAsJsonAsync(
            "/v2/vectordb/entities/insert",
            new
            {
                collectionName,
                data = new[]
                {
                    new { id = 1, vector = new[] { 0.05f, 0.61f, 0.76f, 0.74f } },
                    new { id = 2, vector = new[] { 0.19f, 0.81f, 0.75f, 0.11f } },
                    new { id = 3, vector = new[] { 0.36f, 0.55f, 0.47f, 0.94f } },
                    new { id = 4, vector = new[] { 0.18f, 0.01f, 0.85f, 0.80f } },
                    new { id = 5, vector = new[] { 0.24f, 0.18f, 0.22f, 0.44f } },
                },
            });
        insertHttpResponse.EnsureSuccessStatusCode();
        var insertJson = JsonDocument.Parse(await insertHttpResponse.Content.ReadAsStringAsync());
        var insertCode = insertJson.RootElement.GetProperty("code").GetInt32();
        var insertCount = insertJson.RootElement.GetProperty("data").GetProperty("insertCount").GetInt32();

        insertCode.Should().Be(0);
        insertCount.Should().Be(5);

        Console.WriteLine($"Inserted {insertCount} vectors.");

        //// Search for the 3 nearest neighbors using a query vector.
        //// Note: Milvus v2.5+ uses "data" field for search vectors (v2.4 used "vector").

        using var searchHttpResponse = await client.HttpClient.PostAsJsonAsync(
            "/v2/vectordb/entities/search",
            new
            {
                collectionName,
                data = new[] { new[] { 0.2f, 0.1f, 0.9f, 0.7f } },
                limit = 3,
                outputFields = new[] { "id" },
            });
        searchHttpResponse.EnsureSuccessStatusCode();
        var searchJson = JsonDocument.Parse(await searchHttpResponse.Content.ReadAsStringAsync());
        var searchCode = searchJson.RootElement.GetProperty("code").GetInt32();
        var searchData = searchJson.RootElement.GetProperty("data");

        searchCode.Should().Be(0);
        searchData.GetArrayLength().Should().Be(3);

        Console.WriteLine($"Search returned {searchData.GetArrayLength()} results.");

        //// Query entities by filter expression.

        var queryResponse = await client.VectorOperationsV2.CreateVectordbEntitiesQueryAsync(
            collectionName: collectionName,
            filter: "id in [1, 2, 3]",
            outputFields: ["id"]);

        queryResponse.Should().NotBeNull();
        queryResponse.Code.Should().Be(0);
        queryResponse.Data.Should().NotBeNull();
        queryResponse.Data.Should().HaveCount(3);

        Console.WriteLine($"Query returned {queryResponse.Data!.Count} entities.");

        //// Delete specific entities by filter.

        var deleteResponse = await client.VectorOperationsV2.CreateVectordbEntitiesDeleteAsync(
            collectionName: collectionName,
            filter: "id in [1, 2]");

        deleteResponse.Should().NotBeNull();
        deleteResponse.Code.Should().Be(0);

        Console.WriteLine("Deleted entities with id in [1, 2].");

        //// Wait for delete to propagate (Milvus deletes are eventually consistent).

        await Task.Delay(TimeSpan.FromSeconds(2));

        //// Verify the remaining entity count.

        var queryAfterDelete = await client.VectorOperationsV2.CreateVectordbEntitiesQueryAsync(
            collectionName: collectionName,
            filter: "id >= 0",
            outputFields: ["id"]);

        queryAfterDelete.Data.Should().HaveCount(3);

        Console.WriteLine($"Remaining entities: {queryAfterDelete.Data!.Count}");

        //// Cleanup: drop the collection.

        await client.CollectionOperationsV2.CreateVectordbCollectionsDropAsync(
            collectionName1: collectionName);
    }
}

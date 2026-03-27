/*
order: 20
title: Vectors
slug: vectors

Insert, search, query, and delete vectors in a Milvus collection.
*/

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
            autoID: "false",
            primaryFieldName: "id",
            vectorFieldName: "vector");

        //// Insert vectors with associated data into the collection.

        var insertResponse = await client.VectorOperationsV2.CreateVectordbEntitiesInsertAsync(
            request: new CreateVectordbEntitiesInsertRequest
            {
                CollectionName = collectionName,
                Data = new AnyOf<CreateVectordbEntitiesInsertRequestData, IList<object>>(
                    new List<object>
                    {
                        new Dictionary<string, object>
                        {
                            ["id"] = 1,
                            ["vector"] = new[] { 0.05f, 0.61f, 0.76f, 0.74f },
                        },
                        new Dictionary<string, object>
                        {
                            ["id"] = 2,
                            ["vector"] = new[] { 0.19f, 0.81f, 0.75f, 0.11f },
                        },
                        new Dictionary<string, object>
                        {
                            ["id"] = 3,
                            ["vector"] = new[] { 0.36f, 0.55f, 0.47f, 0.94f },
                        },
                        new Dictionary<string, object>
                        {
                            ["id"] = 4,
                            ["vector"] = new[] { 0.18f, 0.01f, 0.85f, 0.80f },
                        },
                        new Dictionary<string, object>
                        {
                            ["id"] = 5,
                            ["vector"] = new[] { 0.24f, 0.18f, 0.22f, 0.44f },
                        },
                    }),
            });

        insertResponse.Should().NotBeNull();
        insertResponse.Code.Should().Be(0);
        insertResponse.Data.Should().NotBeNull();
        insertResponse.Data!.InsertCount.Should().Be(5);

        Console.WriteLine($"Inserted {insertResponse.Data.InsertCount} vectors.");

        //// Search for the 3 nearest neighbors using a query vector.

        var searchResponse = await client.VectorOperationsV2.CreateVectordbEntitiesSearchAsync(
            request: new CreateVectordbEntitiesSearchRequest
            {
                CollectionName = collectionName,
                Vector = [
                    new List<AnyOf<int?, string>> { "0.2", "0.1", "0.9", "0.7" },
                ],
                Limit = 3,
                OutputFields = ["id"],
                SearchParams = new SearchParams(),
            });

        searchResponse.Should().NotBeNull();
        searchResponse.Code.Should().Be(0);
        searchResponse.Data.Should().NotBeNull();
        searchResponse.Data.Should().HaveCount(3);

        Console.WriteLine($"Search returned {searchResponse.Data!.Count} results.");

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

/*
order: 30
title: Indexes
slug: indexes

Create, list, describe, and drop indexes on a Milvus collection.
*/

namespace Milvus.IntegrationTests;

public partial class Tests
{
    //// Create a collection with a custom schema, build an index on the vector field,
    //// list indexes, describe the index details, and then drop the index.

    [TestMethod]
    public async Task Example_Indexes()
    {
        var client = Client;
        var collectionName = $"test_indexes_{Guid.NewGuid():N}";
        var indexName = "vector_index";
        var vectorFieldName = "vector";

        //// Create a collection with quick-setup (auto-creates an index).

        await client.CollectionOperationsV2.CreateVectordbCollectionsCreateAsync(
            requestTimeout: 30,
            collectionName: collectionName,
            dimension: 4,
            metricType: "COSINE",
            autoId: false,
            primaryFieldName: "id",
            vectorFieldName: vectorFieldName);

        //// List all indexes for the collection.

        var listResponse = await client.IndexOperationsV2.CreateVectordbIndexesListAsync(
            dbName: "default",
            collectionName: collectionName);

        listResponse.Should().NotBeNull();
        listResponse.Code.Should().Be(0);
        listResponse.Data.Should().NotBeEmpty();

        Console.WriteLine($"Found {listResponse.Data.Count} index(es): {string.Join(", ", listResponse.Data)}");

        //// Describe the auto-created index to get detailed information.

        var autoIndexName = listResponse.Data[0];

        var describeResponse = await client.IndexOperationsV2.CreateVectordbIndexesDescribeAsync(
            collectionName: collectionName,
            indexName: autoIndexName);

        describeResponse.Should().NotBeNull();
        describeResponse.Code.Should().Be(0);
        describeResponse.Data.Should().NotBeEmpty();

        var indexDetail = describeResponse.Data[0];
        indexDetail.FieldName.Should().Be(vectorFieldName);
        indexDetail.MetricType.Should().Be("COSINE");

        Console.WriteLine($"Index name: {indexDetail.IndexName}");
        Console.WriteLine($"Index type: {indexDetail.IndexType}");
        Console.WriteLine($"Field name: {indexDetail.FieldName}");
        Console.WriteLine($"Metric type: {indexDetail.MetricType}");
        Console.WriteLine($"Index state: {indexDetail.IndexState}");

        //// Release the collection before dropping the index (required by Milvus).

        await client.CollectionOperationsV2.CreateVectordbCollectionsReleaseAsync(
            collectionName: collectionName);

        Console.WriteLine($"Collection '{collectionName}' released.");

        //// Drop the auto-created index.

        await client.IndexOperationsV2.CreateVectordbIndexesDropAsync(
            collectionName: collectionName,
            indexName1: autoIndexName);

        Console.WriteLine($"Index '{autoIndexName}' dropped.");

        //// Create a new index with explicit parameters.

        await client.IndexOperationsV2.CreateVectordbIndexesCreateAsync(
            collectionName: collectionName,
            indexParams:
            [
                new IndexParam
                {
                    MetricType = "L2",
                    FieldName = vectorFieldName,
                    IndexName = indexName,
                    IndexConfig = new IndexConfig
                    {
                        IndexType = "AUTOINDEX",
                    },
                },
            ]);

        Console.WriteLine($"Index '{indexName}' created.");

        //// Verify the new index exists.

        var listAfterCreate = await client.IndexOperationsV2.CreateVectordbIndexesListAsync(
            dbName: "default",
            collectionName: collectionName);

        listAfterCreate.Data.Should().Contain(indexName);

        //// Describe the newly created index.

        var describeNew = await client.IndexOperationsV2.CreateVectordbIndexesDescribeAsync(
            collectionName: collectionName,
            indexName: indexName);

        describeNew.Data.Should().NotBeEmpty();
        describeNew.Data[0].FieldName.Should().Be(vectorFieldName);
        describeNew.Data[0].MetricType.Should().Be("L2");

        Console.WriteLine($"New index metric type: {describeNew.Data[0].MetricType}");

        //// Cleanup: drop the collection.

        await client.CollectionOperationsV2.CreateVectordbCollectionsDropAsync(
            collectionName1: collectionName);
    }
}

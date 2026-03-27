/*
order: 10
title: Collections
slug: collections

Manage Milvus collections -- create, list, describe, and drop.
*/

namespace Milvus.IntegrationTests;

public partial class Tests
{
    //// Create a collection with 4-dimensional vectors using Cosine distance,
    //// list all collections, describe the collection details, and then drop it.

    [TestMethod]
    public async Task Example_Collections()
    {
        var client = Client;
        var collectionName = $"test_collection_{Guid.NewGuid():N}";

        //// Create a new collection with 4-dimensional vectors using Cosine distance.

        var createResponse = await client.CollectionOperationsV2.CreateVectordbCollectionsCreateAsync(
            requestTimeout: 30,
            collectionName: collectionName,
            dimension: 4,
            metricType: "COSINE");

        createResponse.Should().NotBeNull();
        createResponse.Code.Should().Be(0);

        Console.WriteLine($"Collection '{collectionName}' created successfully.");

        //// Check that the collection exists using the Has endpoint.

        var hasResponse = await client.CollectionOperationsV2.CreateVectordbCollectionsHasAsync(
            dbName: "default",
            collectionName: collectionName);

        hasResponse.Should().NotBeNull();
        hasResponse.Code.Should().Be(0);
        hasResponse.Data.Has.Should().BeTrue();

        Console.WriteLine($"Collection '{collectionName}' exists: {hasResponse.Data.Has}");

        //// Describe the collection to get detailed information.

        var describeResponse = await client.CollectionOperationsV2.CreateVectordbCollectionsDescribeAsync(
            dbName: "default",
            collectionName: collectionName);

        describeResponse.Should().NotBeNull();
        describeResponse.Code.Should().Be(0);
        describeResponse.Data.Should().NotBeNull();
        describeResponse.Data.CollectionName.Should().Be(collectionName);
        describeResponse.Data.Fields.Should().NotBeEmpty();

        Console.WriteLine($"Collection name: {describeResponse.Data.CollectionName}");
        Console.WriteLine($"Fields count: {describeResponse.Data.Fields.Count}");
        Console.WriteLine($"Load status: {describeResponse.Data.Load}");

        //// Drop the collection.

        var dropResponse = await client.CollectionOperationsV2.CreateVectordbCollectionsDropAsync(
            collectionName1: collectionName);

        dropResponse.Should().NotBeNull();
        dropResponse.Code.Should().Be(0);

        Console.WriteLine($"Collection '{collectionName}' dropped.");

        //// Verify the collection no longer exists.

        var hasAfterDrop = await client.CollectionOperationsV2.CreateVectordbCollectionsHasAsync(
            dbName: "default",
            collectionName: collectionName);

        hasAfterDrop.Data.Has.Should().BeFalse();
    }
}

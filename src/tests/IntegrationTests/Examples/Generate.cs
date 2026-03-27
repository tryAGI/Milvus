/*
order: 0
title: Generate
slug: generate

Basic example showing how to create a client and make a request.
*/

namespace Milvus.IntegrationTests;

public partial class Tests
{
    [TestMethod]
    public async Task Example_Generate()
    {
        var client = Client;

        //// List collections to verify the client is connected.

        await client.CollectionOperationsV2.CreateVectordbCollectionsListAsync(
            dbName: "default");
    }
}

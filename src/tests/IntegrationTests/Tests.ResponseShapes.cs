namespace Milvus.IntegrationTests;

/// <summary>
/// Calls that exist to prove the generated models can read what the server actually sends.
/// </summary>
/// <remarks>
/// The generated deserializers enforce <c>required</c>, so a field the server omits turns a
/// successful HTTP 200 into a <c>JsonException</c> - which went unnoticed elsewhere in the
/// workspace until a probe hit the surface: Anthropic's `caller`, Label Studio's `username` and
/// Qdrant's `peer_id` all failed on successful responses.
///
/// This spec enforces 21 required fields across six response schemas, the deepest being
/// `index-detail` at eight - already covered by `Example_Indexes`, which describes an index. What
/// was left is here. `alias-detail` stays uncovered on purpose: the spec gives
/// `POST /v2/vectordb/aliases/create` no request body at all, so the generated method takes only a
/// timeout and there is no way to create the alias a describe would need.
/// </remarks>
public partial class Tests
{
    [TestMethod]
    public async Task CollectionExistence_Deserializes()
    {
        // Milvus only allows letters, numbers and underscores in a collection name.
        var collectionName = $"tryagi_probe_{Guid.NewGuid():N}";

        // `Has` answers for a collection that does not exist too, so no fixture is needed.
        var missing = await Client.CollectionOperationsV2.CreateVectordbCollectionsHasAsync(
            collectionName: collectionName,
            dbName: "default");

        missing.Should().NotBeNull();
        missing.Data?.Has.Should().BeFalse();
    }

    [TestMethod]
    public async Task RolePrivileges_Deserialize()
    {
        // `public` is built in, so this needs no fixture either.
        var privileges = await Client.RoleOperationsV2.CreateVectordbRolesDescribeAsync(
            roleName1: "public");

        privileges.Should().NotBeNull();
    }
}

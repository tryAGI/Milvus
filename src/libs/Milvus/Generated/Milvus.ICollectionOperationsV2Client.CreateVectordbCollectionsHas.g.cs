#nullable enable

namespace Milvus
{
    public partial interface ICollectionOperationsV2Client
    {
        /// <summary>
        /// Has Collection<br/>
        /// This operation checks whether a collection exists.
        /// </summary>
        /// <param name="requestTimeout"></param>
        /// <param name="authorization"></param>
        /// <param name="request"></param>
        /// <param name="cancellationToken">The token to cancel the operation with</param>
        /// <exception cref="global::Milvus.ApiException"></exception>
        global::System.Threading.Tasks.Task<global::Milvus.Has> CreateVectordbCollectionsHasAsync(

            global::Milvus.HasReq request,
            int? requestTimeout = default,
            string? authorization = default,
            global::System.Threading.CancellationToken cancellationToken = default);
        /// <summary>
        /// Has Collection<br/>
        /// This operation checks whether a collection exists.
        /// </summary>
        /// <param name="requestTimeout"></param>
        /// <param name="authorization"></param>
        /// <param name="dbName">
        /// The name of the database in which to check the existence of a collection.
        /// </param>
        /// <param name="collectionName">
        /// The name of an existing collection.
        /// </param>
        /// <param name="cancellationToken">The token to cancel the operation with</param>
        /// <exception cref="global::System.InvalidOperationException"></exception>
        global::System.Threading.Tasks.Task<global::Milvus.Has> CreateVectordbCollectionsHasAsync(
            string dbName,
            string collectionName,
            int? requestTimeout = default,
            string? authorization = default,
            global::System.Threading.CancellationToken cancellationToken = default);
    }
}
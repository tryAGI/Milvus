#nullable enable

namespace Milvus
{
    public partial interface ICollectionOperationsV2Client
    {
        /// <summary>
        /// Release Collection<br/>
        /// This operation releases the data of the current collection from memory.
        /// </summary>
        /// <param name="requestTimeout"></param>
        /// <param name="request"></param>
        /// <param name="requestOptions">Per-request overrides such as headers, query parameters, timeout, retries, and response buffering.</param>
        /// <param name="cancellationToken">The token to cancel the operation with</param>
        /// <exception cref="global::Milvus.ApiException"></exception>
        global::System.Threading.Tasks.Task<string> CreateVectordbCollectionsReleaseAsync(

            global::Milvus.CreateVectordbCollectionsReleaseRequest request,
            int? requestTimeout = default,
            global::Milvus.AutoSDKRequestOptions? requestOptions = default,
            global::System.Threading.CancellationToken cancellationToken = default);
        /// <summary>
        /// Release Collection<br/>
        /// This operation releases the data of the current collection from memory.
        /// </summary>
        /// <param name="requestTimeout"></param>
        /// <param name="dbName">
        /// The name of the database to which the cpllection belongs.<br/>
        /// Setting this to a non-existing database results in a **MilvusException**.
        /// </param>
        /// <param name="collectionName">
        /// The name of the target colletion.<br/>
        /// Setting this to a non-existing collection results in a **MilvusException**.
        /// </param>
        /// <param name="requestOptions">Per-request overrides such as headers, query parameters, timeout, retries, and response buffering.</param>
        /// <param name="cancellationToken">The token to cancel the operation with</param>
        /// <exception cref="global::System.InvalidOperationException"></exception>
        global::System.Threading.Tasks.Task<string> CreateVectordbCollectionsReleaseAsync(
            string collectionName,
            int? requestTimeout = default,
            string? dbName = default,
            global::Milvus.AutoSDKRequestOptions? requestOptions = default,
            global::System.Threading.CancellationToken cancellationToken = default);
    }
}
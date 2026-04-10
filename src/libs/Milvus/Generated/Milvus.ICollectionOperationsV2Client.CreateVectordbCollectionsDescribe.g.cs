#nullable enable

namespace Milvus
{
    public partial interface ICollectionOperationsV2Client
    {
        /// <summary>
        /// Describe Collection<br/>
        /// Describes the details of a collection.
        /// </summary>
        /// <param name="requestTimeout"></param>
        /// <param name="request"></param>
        /// <param name="requestOptions">Per-request overrides such as headers, query parameters, timeout, retries, and response buffering.</param>
        /// <param name="cancellationToken">The token to cancel the operation with</param>
        /// <exception cref="global::Milvus.ApiException"></exception>
        global::System.Threading.Tasks.Task<global::Milvus.CreateVectordbCollectionsDescribeResponse> CreateVectordbCollectionsDescribeAsync(

            global::Milvus.CreateVectordbCollectionsDescribeRequest request,
            int? requestTimeout = default,
            global::Milvus.AutoSDKRequestOptions? requestOptions = default,
            global::System.Threading.CancellationToken cancellationToken = default);
        /// <summary>
        /// Describe Collection<br/>
        /// Describes the details of a collection.
        /// </summary>
        /// <param name="requestTimeout"></param>
        /// <param name="dbName">
        /// The name of the database.
        /// </param>
        /// <param name="collectionName">
        /// The name of the collection to describe.
        /// </param>
        /// <param name="requestOptions">Per-request overrides such as headers, query parameters, timeout, retries, and response buffering.</param>
        /// <param name="cancellationToken">The token to cancel the operation with</param>
        /// <exception cref="global::System.InvalidOperationException"></exception>
        global::System.Threading.Tasks.Task<global::Milvus.CreateVectordbCollectionsDescribeResponse> CreateVectordbCollectionsDescribeAsync(
            string dbName,
            string collectionName,
            int? requestTimeout = default,
            global::Milvus.AutoSDKRequestOptions? requestOptions = default,
            global::System.Threading.CancellationToken cancellationToken = default);
    }
}
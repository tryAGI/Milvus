#nullable enable

namespace Milvus
{
    public partial interface IUserOperationsV2Client
    {
        /// <summary>
        /// Describe User<br/>
        /// This operation describes the detailed information of a specific user.
        /// </summary>
        /// <param name="requestTimeout"></param>
        /// <param name="request"></param>
        /// <param name="requestOptions">Per-request overrides such as headers, query parameters, timeout, retries, and response buffering.</param>
        /// <param name="cancellationToken">The token to cancel the operation with</param>
        /// <exception cref="global::Milvus.ApiException"></exception>
        global::System.Threading.Tasks.Task<global::Milvus.CreateVectordbUsersDescribeResponse> CreateVectordbUsersDescribeAsync(

            global::Milvus.CreateVectordbUsersDescribeRequest request,
            int? requestTimeout = default,
            global::Milvus.AutoSDKRequestOptions? requestOptions = default,
            global::System.Threading.CancellationToken cancellationToken = default);
        /// <summary>
        /// Describe User<br/>
        /// This operation describes the detailed information of a specific user.
        /// </summary>
        /// <param name="requestTimeout"></param>
        /// <param name="userName">
        ///   The name of the user to describe.
        /// </param>
        /// <param name="requestOptions">Per-request overrides such as headers, query parameters, timeout, retries, and response buffering.</param>
        /// <param name="cancellationToken">The token to cancel the operation with</param>
        /// <exception cref="global::System.InvalidOperationException"></exception>
        global::System.Threading.Tasks.Task<global::Milvus.CreateVectordbUsersDescribeResponse> CreateVectordbUsersDescribeAsync(
            string userName,
            int? requestTimeout = default,
            global::Milvus.AutoSDKRequestOptions? requestOptions = default,
            global::System.Threading.CancellationToken cancellationToken = default);
    }
}
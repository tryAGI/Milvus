#nullable enable

namespace Milvus
{
    public partial interface IUserOperationsV2Client
    {
        /// <summary>
        /// List Users<br/>
        /// This operation lists the information of all existing users.
        /// </summary>
        /// <param name="requestTimeout"></param>
        /// <param name="authorization"></param>
        /// <param name="cancellationToken">The token to cancel the operation with</param>
        /// <exception cref="global::Milvus.ApiException"></exception>
        global::System.Threading.Tasks.Task<global::Milvus.CreateVectordbUsersListResponse> CreateVectordbUsersListAsync(
            int? requestTimeout = default,
            string? authorization = default,
            global::System.Threading.CancellationToken cancellationToken = default);
    }
}
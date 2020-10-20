namespace Fur.Core
{
    /// <summary>
    /// 权限管理器
    /// </summary>
    public interface IAuthorizationManager
    {
        /// <summary>
        /// 获取用户 Id
        /// </summary>
        /// <returns></returns>
        object GetUserId();

        /// <summary>
        /// 获取用户 Id
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T GetUserId<T>();
    }
}
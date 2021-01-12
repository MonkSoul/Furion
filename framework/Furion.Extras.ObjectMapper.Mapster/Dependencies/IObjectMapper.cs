using System;

namespace Mapster
{
    /// <summary>
    /// 自定义对象映射依赖接口
    /// </summary>
    [Obsolete("该接口已过时，请调用 IRegister 方法代替。")]
    public interface IObjectMapper : IRegister
    {
    }
}
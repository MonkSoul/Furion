// MIT 许可证
//
// 版权 © 2020-present 百小僧, 百签科技（广东）有限公司 和所有贡献者
//
// 特此免费授予任何获得本软件副本和相关文档文件（下称“软件”）的人不受限制地处置该软件的权利，
// 包括不受限制地使用、复制、修改、合并、发布、分发、转授许可和/或出售该软件副本，
// 以及再授权被配发了本软件的人如上的权利，须在下列条件下：
//
// 上述版权声明和本许可声明应包含在该软件的所有副本或实质成分中。
//
// 本软件是“如此”提供的，没有任何形式的明示或暗示的保证，包括但不限于对适销性、特定用途的适用性和不侵权的保证。
// 在任何情况下，作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

using System.Xml.Linq;

namespace Furion.ClayObject;

/// <summary>
/// 粘土对象对象类型枚举器实现类
/// </summary>
[SuppressSniffer]
public sealed class ClayObjectEnumerator : IEnumerator
{
    /// <summary>
    /// 粘土对象
    /// </summary>
    public dynamic _clay;

    /// <summary>
    /// 当前索引
    /// </summary>
    private int position = -1;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="clay">粘土对象</param>
    public ClayObjectEnumerator(dynamic clay)
    {
        _clay = clay;
    }

    /// <summary>
    /// 推进（获取）下一个元素
    /// </summary>
    /// <returns></returns>
    public bool MoveNext()
    {
        position++;
        return (position < _clay.Length);
    }

    /// <summary>
    /// 将元素索引恢复初始值
    /// </summary>
    public void Reset()
    {
        position = -1;
    }

    /// <summary>
    /// 当前元素
    /// </summary>
    public KeyValuePair<string, dynamic> Current
    {
        get
        {
            try
            {
                var xElement = ((XElement)_clay.XmlElement).Elements().ElementAtOrDefault(position);

                // 获取节点真实标签名
                var localName = xElement.Name == "{item}item"
                    ? xElement.Attribute("item").Value
                    : xElement.Name.LocalName;

                return new(localName, _clay[localName]);
            }
            catch (IndexOutOfRangeException)
            {
                throw new InvalidOperationException();
            }
        }
    }

    /// <summary>
    /// 当前元素（内部）
    /// </summary>
    object IEnumerator.Current => Current;
}
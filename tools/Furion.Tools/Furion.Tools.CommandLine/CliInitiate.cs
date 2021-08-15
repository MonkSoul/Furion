using System.Linq;
using System.Reflection;

namespace Furion.Tools.CommandLine
{
    /// <summary>
    /// Cli 初始化
    /// </summary>
    public static partial class Cli
    {
        /// <summary>
        /// 输入参数信息
        /// </summary>
        internal static Arguments Arguments { get; set; }

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static Cli()
        {
            // 填充参数信息
            Fill();
        }

        /// <summary>
        /// 填充参数信息
        /// </summary>
        private static void Fill()
        {
            // 如果已经填充过，不再重新解析
            if (ArgumentMetadatas != null) return;

            // 获取入口方法声明类型
            var entryType = Assembly.GetEntryAssembly().DefinedTypes.First(u => u.Name == "Program");

            // 填充当前属性值
            Arguments.Populate(entryType);

            // 获取传递参数字典
            Arguments = Arguments.Parse(u =>
            {
                u.TargetType = entryType;
            });
            var argumentDictionary = Arguments.ArgumentDictionary;

            // 解析定义参数集合
            ArgumentMetadatas = Arguments.GetArgumentInfo(entryType)
                                                                 .Select(u => new ArgumentMetadata
                                                                 {
                                                                     HelpText = u.HelpText,
                                                                     IsCollection = u.IsCollection,
                                                                     LongName = u.LongName,
                                                                     ShortName = u.ShortName,
                                                                     Property = u.Property,
                                                                     IsTransmission = argumentDictionary.ContainsKey(u.ShortName.ToString()) || argumentDictionary.ContainsKey(u.LongName),
                                                                     Value = argumentDictionary.ContainsKey(u.ShortName.ToString())
                                                                        ? argumentDictionary[u.ShortName.ToString()]
                                                                        : (argumentDictionary.ContainsKey(u.LongName)
                                                                                ? argumentDictionary[u.LongName]
                                                                                : default)
                                                                 });
        }
    }
}
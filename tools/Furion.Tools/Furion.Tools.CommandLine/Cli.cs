using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Furion.Tools.CommandLine
{
    /// <summary>
    /// Cli 常用方法
    /// </summary>
    public static partial class Cli
    {
        /// <summary>
        /// 参数元数据集合
        /// </summary>
        public static IEnumerable<ArgumentMetadata> ArgumentMetadatas { get; internal set; }

        /// <summary>
        /// 最小侵入初始化
        /// </summary>
        public static void Inject()
        {
            foreach (var (propertyName, handler) in ArgumentHandlers)
            {
                Check(propertyName, handler);
            }

            // 没有匹配的 Handler
            var noMatchHandler = GetEntryType().DeclaredMethods.FirstOrDefault(u => u.IsStatic
                                                                       && u.Name == "NoMatchHandler"
                                                                       && u.ReturnType == typeof(void)
                                                                       && u.GetParameters().Length == 3
                                                                       && u.GetParameters()[0].ParameterType == typeof(bool)
                                                                       && u.GetParameters()[1].ParameterType == typeof(string[])
                                                                       && u.GetParameters()[2].ParameterType == typeof(Dictionary<string, object>));

            if (noMatchHandler != null)
            {
                CheckNoMatch((Action<bool, string[], Dictionary<string, object>>)Delegate.CreateDelegate(typeof(Action<bool, string[], Dictionary<string, object>>), noMatchHandler));
            }
        }

        /// <summary>
        /// 判断参数是否定义
        /// </summary>
        /// <param name="argumentName"></param>
        /// <param name="handler"></param>
        public static void Check(string argumentName, Action<ArgumentMetadata> handler)
        {
            if (handler == null) throw new ArgumentNullException(nameof(handler));

            var argumentMetadata = ArgumentMetadatas.FirstOrDefault(u => u.ShortName.ToString() == argumentName || u.LongName == argumentName || u.Property.Name == argumentName);
            if (argumentMetadata != null && argumentMetadata.IsTransmission) handler(argumentMetadata);
        }

        /// <summary>
        /// 检查未匹配字符
        /// </summary>
        /// <param name="handler">arg1: 是否传递空参数，arg2：操作符列表，args3：未匹配的参数列表</param>
        /// <returns></returns>
        public static void CheckNoMatch(Action<bool, string[], Dictionary<string, object>> handler)
        {
            if (handler == null) throw new ArgumentNullException(nameof(handler));

            // 所有匹配的字符
            var matches = ArgumentMetadatas.Select(u => u.ShortName.ToString())
                                                            .Concat(ArgumentMetadatas.Select(u => u.LongName));

            // 未匹配字符
            var noMatches = Arguments.ArgumentDictionary.Where(u => !matches.Contains(u.Key)).ToDictionary(u => u.Key, u => u.Value);

            // 操作符
            var operands = Arguments.OperandList.Where(u => !u.Contains(Assembly.GetEntryAssembly().GetName().Name)).ToArray();

            handler(Arguments.ArgumentDictionary.Count == 0 && operands.Length == 0, operands, noMatches);
        }

        /// <summary>
        /// 获取当前工具版本
        /// </summary>
        /// <returns></returns>
        public static string GetVersion()
        {
            return Assembly.GetEntryAssembly()
                                .GetCustomAttribute<AssemblyInformationalVersionAttribute>()
                                .InformationalVersion
                                .ToString();
        }

        /// <summary>
        /// 获取当前工具描述
        /// </summary>
        /// <returns></returns>
        public static string GetDescription()
        {
            return Assembly.GetEntryAssembly()
                               .GetCustomAttribute<AssemblyDescriptionAttribute>()
                               .Description
                               .ToString();
        }
    }
}
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

namespace Furion.TimeCrontab;

/// <summary>
/// Cron 表达式抽象类
/// </summary>
/// <remarks>主要将 Cron 表达式转换成 OOP 类进行操作</remarks>
public partial class Crontab
{
    /// <summary>
    /// 解析 Cron 表达式字段并存储其 所有发生值 字符解析器
    /// </summary>
    /// <param name="expression">Cron 表达式</param>
    /// <param name="format">Cron 表达式格式化类型</param>
    /// <returns><see cref="Dictionary{TKey, TValue}"/></returns>
    /// <exception cref="TimeCrontabException"></exception>
    private static Dictionary<CrontabFieldKind, List<ICronParser>> ParseToDictionary(string expression, CronStringFormat format)
    {
        // Cron 表达式空检查
        if (string.IsNullOrWhiteSpace(expression))
        {
            throw new TimeCrontabException("The provided cron string is null, empty or contains only whitespace.");
        }

        // 通过空白符切割 Cron 表达式每个字段域
        var instructions = expression.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

        // 验证当前 Cron 格式化类型字段数量和表达式字段数量是否一致
        var expectedCount = Constants.ExpectedFieldCounts[format];
        if (instructions.Length > expectedCount)
        {
            throw new TimeCrontabException(string.Format("The provided cron string <{0}> has too many parameters.", expression));
        }
        if (instructions.Length < expectedCount)
        {
            throw new TimeCrontabException(string.Format("The provided cron string <{0}> has too few parameters.", expression));
        }

        // 初始化字段偏移量和字段字符解析器
        var defaultFieldOffset = 0;
        var fieldParsers = new Dictionary<CrontabFieldKind, List<ICronParser>>();

        // 判断当前 Cron 格式化类型是否包含秒字段域，如果包含则优先解析秒字段域字符解析器
        if (format == CronStringFormat.WithSeconds || format == CronStringFormat.WithSecondsAndYears)
        {
            fieldParsers.Add(CrontabFieldKind.Second, ParseField(instructions[0], CrontabFieldKind.Second));
            defaultFieldOffset = 1;
        }

        // Cron 常规字段域
        fieldParsers.Add(CrontabFieldKind.Minute, ParseField(instructions[defaultFieldOffset + 0], CrontabFieldKind.Minute));   // 偏移量 1
        fieldParsers.Add(CrontabFieldKind.Hour, ParseField(instructions[defaultFieldOffset + 1], CrontabFieldKind.Hour));   // 偏移量 2
        fieldParsers.Add(CrontabFieldKind.Day, ParseField(instructions[defaultFieldOffset + 2], CrontabFieldKind.Day)); // 偏移量 3
        fieldParsers.Add(CrontabFieldKind.Month, ParseField(instructions[defaultFieldOffset + 3], CrontabFieldKind.Month)); // 偏移量 4
        fieldParsers.Add(CrontabFieldKind.DayOfWeek, ParseField(instructions[defaultFieldOffset + 4], CrontabFieldKind.DayOfWeek)); // 偏移量 5

        // 判断当前 Cron 格式化类型是否包含年字段域，如果包含则解析年字段域字符解析器
        if (format == CronStringFormat.WithYears || format == CronStringFormat.WithSecondsAndYears)
        {
            fieldParsers.Add(CrontabFieldKind.Year, ParseField(instructions[defaultFieldOffset + 5], CrontabFieldKind.Year));   // 偏移量 6
        }

        // 检查非法字符解析器，如 2 月没有 30 和 31 号
        CheckForIllegalParsers(fieldParsers);

        return fieldParsers;
    }

    /// <summary>
    /// 解析 Cron 单个字段域所有发生值 字符解析器
    /// </summary>
    /// <param name="field">字段值</param>
    /// <param name="kind">Cron 表达式格式化类型</param>
    /// <returns><see cref="List{T}"/></returns>
    /// <exception cref="TimeCrontabException"></exception>
    private static List<ICronParser> ParseField(string field, CrontabFieldKind kind)
    {
        /*
         * 在 Cron 表达式中，单个字段域值也支持定义多个值（我们称为值中值），如 1,2,3 或 SUN,FRI,SAT
         * 所以，这里需要将字段域值通过 , 进行切割后独立处理
         */

        try
        {
            return field.Split(',').Select(parser => ParseParser(parser, kind)).ToList();
        }
        catch (Exception ex)
        {
            throw new TimeCrontabException(
                string.Format("There was an error parsing '{0}' for the {1} field.", field, Enum.GetName(typeof(CrontabFieldKind), kind))
                , ex);
        }
    }

    /// <summary>
    /// 解析 Cron 字段域值中值
    /// </summary>
    /// <param name="parser">字段值中值</param>
    /// <param name="kind">Cron 表达式格式化类型</param>
    /// <returns><see cref="ICronParser"/></returns>
    /// <exception cref="TimeCrontabException"></exception>
    private static ICronParser ParseParser(string parser, CrontabFieldKind kind)
    {
        // Cron 字段中所有字母均采用大写方式，所以需要转换所有为大写再操作
        var newParser = parser.ToUpper();

        try
        {
            // 判断值是否以 * 字符开头
            if (newParser.StartsWith("*", StringComparison.OrdinalIgnoreCase))
            {
                // 继续往后解析
                newParser = newParser[1..];

                // 判断是否以 / 字符开头，如果是，则该值为带步长的 Cron 值
                if (newParser.StartsWith("/", StringComparison.OrdinalIgnoreCase))
                {
                    // 继续往后解析
                    newParser = newParser[1..];

                    // 解析 Cron 值步长并创建 StepParser 解析器
                    var steps = GetValue(ref newParser, kind);
                    return new StepParser(0, steps, kind);
                }

                // 处理 * 携带意外值
                if (newParser != string.Empty)
                {
                    throw new TimeCrontabException(string.Format("Invalid parser '{0}'.", parser));
                }

                // 否则，创建 AnyParser 解析器
                return new AnyParser(kind);
            }

            // 判断值是否以 L 字符开头
            if (newParser.StartsWith("L") && kind == CrontabFieldKind.Day)
            {
                // 继续往后解析
                newParser = newParser[1..];

                // 是否是 LW 字符，如果是，创建 LastWeekdayOfMonthParser 解析器
                if (newParser == "W")
                {
                    return new LastWeekdayOfMonthParser(kind);
                }
                // 否则创建 LastDayOfMonthParser 解析器
                else
                {
                    return new LastDayOfMonthParser(kind);
                }
            }

            // 判断值是否等于 ?
            if (newParser == "?")
            {
                // 创建 BlankDayOfMonthOrWeekParser 解析器
                return new BlankDayOfMonthOrWeekParser(kind);
            }

            /*
             * 如果上面均不匹配，那么该值类似取值有：2，1/2，1-10，1-10/2，SUN，SUNDAY，SUNL，JAN，3W，3L，2#5 等
             */

            // 继续推进解析
            var firstValue = GetValue(ref newParser, kind);

            // 如果没有返回新的待解析字符，则认为这是一个具体值
            if (string.IsNullOrEmpty(newParser))
            {
                // 对年份进行特别处理
                if (kind == CrontabFieldKind.Year)
                {
                    return new SpecificYearParser(firstValue, kind);
                }
                else
                {
                    // 创建 SpecificParser 解析器
                    return new SpecificParser(firstValue, kind);
                }
            }

            // 如果存在待解析字符，如 - / # L W 值，则进一步解析
            switch (newParser[0])
            {
                // 判断值是否以 / 字符开头
                case '/':
                    {
                        // 继续往后解析
                        newParser = newParser[1..];

                        // 解析 Cron 值步长并创建 StepParser 解析器
                        var steps = GetValue(ref newParser, kind);
                        return new StepParser(firstValue, steps, kind);
                    }
                // 判断值是否以 - 字符开头
                case '-':
                    {
                        // 继续往后解析
                        newParser = newParser[1..];

                        // 获取范围结束值
                        var endValue = GetValue(ref newParser, kind);
                        int? steps = null;

                        // 继续推进解析，判断是否以 / 开头，如果是，则获取步长
                        if (newParser.StartsWith("/"))
                        {
                            newParser = newParser[1..];
                            steps = GetValue(ref newParser, kind);
                        }

                        // 创建 RangeParser 解析器
                        return new RangeParser(firstValue, endValue, steps, kind);
                    }
                // 判断值是否以 # 字符开头
                case '#':
                    {
                        // 继续往后解析
                        newParser = newParser[1..];

                        // 获取第几个
                        var weekNumber = GetValue(ref newParser, kind);

                        // 继续推进解析，如果存在其他字符，则抛异常
                        if (!string.IsNullOrEmpty(newParser))
                        {
                            throw new TimeCrontabException(string.Format("Invalid parser '{0}.'", parser));
                        }

                        // 创建 SpecificDayOfWeekInMonthParser 解析器
                        return new SpecificDayOfWeekInMonthParser(firstValue, weekNumber, kind);
                    }
                // 判断解析值是否等于 L 或 W
                default:
                    // 创建 LastDayOfWeekInMonthParser 解析器
                    if (newParser == "L" && kind == CrontabFieldKind.DayOfWeek)
                    {
                        return new LastDayOfWeekInMonthParser(firstValue, kind);
                    }
                    // 创建 NearestWeekdayParser 解析器
                    else if (newParser == "W" && kind == CrontabFieldKind.Day)
                    {
                        return new NearestWeekdayParser(firstValue, kind);
                    }
                    break;
            }

            throw new TimeCrontabException(string.Format("Invalid parser '{0}'.", parser));
        }
        catch (Exception ex)
        {
            throw new TimeCrontabException(string.Format("Invalid parser '{0}'. See inner exception for details.", parser), ex);
        }
    }

    /// <summary>
    /// 将 Cron 字段值中值进一步解析
    /// </summary>
    /// <param name="parser">当前解析值</param>
    /// <param name="kind">Cron 表达式格式化类型</param>
    /// <returns><see cref="int"/></returns>
    /// <exception cref="TimeCrontabException"></exception>
    private static int GetValue(ref string parser, CrontabFieldKind kind)
    {
        // 值空检查
        if (string.IsNullOrEmpty(parser))
        {
            throw new TimeCrontabException("Expected number, but parser was empty.");
        }

        // 字符偏移量
        int offset;

        // 判断首个字符是数字还是字符串
        var isDigit = char.IsDigit(parser[0]);
        var isLetter = char.IsLetter(parser[0]);

        // 推进式遍历值并检查每一个字符，一旦出现类型不连贯则停止检查
        for (offset = 0; offset < parser.Length; offset++)
        {
            // 如果存在不连贯数字或字母则跳出循环
            if ((isDigit && !char.IsDigit(parser[offset])) || (isLetter && !char.IsLetter(parser[offset])))
            {
                break;
            }
        }

        var maximum = Constants.MaximumDateTimeValues[kind];

        // 前面连贯类型的值
        var valueToParse = parser[..offset];

        // 处理数字开头的连贯类型值
        if (int.TryParse(valueToParse, out var value))
        {
            // 导出下一轮待解析的值（依旧采用推进式）
            parser = parser[offset..];

            var returnValue = value;

            // 验证值范围
            if (returnValue > maximum)
            {
                throw new TimeCrontabException(string.Format("Value for {0} parser exceeded maximum value of {1}.", Enum.GetName(typeof(CrontabFieldKind), kind), maximum));
            }

            return returnValue;
        }
        // 处理字母开头的连贯类型值，通常认为这是一个单词，如SUN，JAN
        else
        {
            List<KeyValuePair<string, int>> replaceVal = null;

            // 判断当前 Cron 字段类型是否是星期，如果是，则查找该单词是否在 Constants.Days 定义之中
            if (kind == CrontabFieldKind.DayOfWeek)
            {
                replaceVal = Constants.Days.Where(x => valueToParse.StartsWith(x.Key)).ToList();
            }
            // 判断当前 Cron 字段类型是否是月份，如果是，则查找该单词是否在 Constants.Months 定义之中
            else if (kind == CrontabFieldKind.Month)
            {
                replaceVal = Constants.Months.Where(x => valueToParse.StartsWith(x.Key)).ToList();
            }

            // 如果存在且唯一，则进入下一轮判断
            // 接下来的判断是处理 SUN + L 的情况，如 SUNL == 0L == SUNDAY，它们都是合法的 Cron 值
            if (replaceVal != null && replaceVal.Count == 1)
            {
                var missingParser = "";

                // 处理带 L 和不带 L 的单词问题
                if (parser.Length == offset
                    && parser.EndsWith("L")
                    && kind == CrontabFieldKind.DayOfWeek)
                {
                    missingParser = "L";
                }
                parser = parser[offset..] + missingParser;

                // 转换成 int 值返回（SUN，JAN.....）
                var returnValue = replaceVal.First().Value;

                // 验证值范围
                if (returnValue > maximum)
                {
                    throw new TimeCrontabException(string.Format("Value for {0} parser exceeded maximum value of {1}.", Enum.GetName(typeof(CrontabFieldKind), kind), maximum));
                }

                return returnValue;
            }
        }

        throw new TimeCrontabException("Parser does not contain expected number.");
    }

    /// <summary>
    /// 检查非法字符解析器，如 2 月没有 30 和 31 号
    /// </summary>
    /// <remarks>检查 2 月份是否存在 30 和 31 天的非法数值解析器</remarks>
    /// <param name="parsers">Cron 字段解析器字典集合</param>
    /// <exception cref="TimeCrontabException"></exception>
    private static void CheckForIllegalParsers(Dictionary<CrontabFieldKind, List<ICronParser>> parsers)
    {
        // 获取当前 Cron 表达式月字段和天字段所有数值
        var monthSingle = GetSpecificParsers(parsers, CrontabFieldKind.Month);
        var daySingle = GetSpecificParsers(parsers, CrontabFieldKind.Day);

        // 如果月份为 2 月单天数出现 30 和 31 天，则是无效数值
        if (monthSingle.Any() && monthSingle.All(x => x.SpecificValue == 2))
        {
            if (daySingle.Any() && daySingle.All(x => (x.SpecificValue == 30) || (x.SpecificValue == 31)))
            {
                throw new TimeCrontabException("The February 30 and 31 don't exist.");
            }
        }
    }

    /// <summary>
    /// 查找 Cron 字段类型所有具体值解析器
    /// </summary>
    /// <param name="parsers">Cron 字段解析器字典集合</param>
    /// <param name="kind">Cron 字段种类</param>
    /// <returns><see cref="List{T}"/></returns>
    private static List<SpecificParser> GetSpecificParsers(Dictionary<CrontabFieldKind, List<ICronParser>> parsers, CrontabFieldKind kind)
    {
        var kindParsers = parsers[kind];

        // 查找 Cron 字段类型所有具体值解析器
        return kindParsers.Where(x => x.GetType() == typeof(SpecificParser)).Cast<SpecificParser>()
            .Union(
            kindParsers.Where(x => x.GetType() == typeof(RangeParser)).SelectMany(x => ((RangeParser)x).SpecificParsers)
            ).Union(
                kindParsers.Where(x => x.GetType() == typeof(StepParser)).SelectMany(x => ((StepParser)x).SpecificParsers)
            ).ToList();
    }

    /// <summary>
    /// 获取特定时间范围下一个发生时间
    /// </summary>
    /// <param name="baseTime">起始时间</param>
    /// <param name="endTime">结束时间</param>
    /// <returns><see cref="DateTime"/></returns>
    private DateTime InternalGetNextOccurence(DateTime baseTime, DateTime endTime)
    {
        // 判断当前 Cron 格式化类型是否支持秒
        var isSecondFormat = Format == CronStringFormat.WithSeconds || Format == CronStringFormat.WithSecondsAndYears;

        // 由于 Cron 格式化类型不包含毫秒，则裁剪掉毫秒部分
        var newValue = baseTime;
        newValue = newValue.AddMilliseconds(-newValue.Millisecond);

        // 如果当前 Cron 格式化类型不支持秒，则裁剪掉秒部分
        if (!isSecondFormat)
        {
            newValue = newValue.AddSeconds(-newValue.Second);
        }

        // 获取分钟、小时所有字符解析器
        var minuteParsers = Parsers[CrontabFieldKind.Minute].Where(x => x is ITimeParser).Cast<ITimeParser>().ToList();
        var hourParsers = Parsers[CrontabFieldKind.Hour].Where(x => x is ITimeParser).Cast<ITimeParser>().ToList();

        // 获取秒、分钟、小时解析器中最小起始值
        // 该值主要用来获取下一个发生值的输入参数
        var firstSecondValue = newValue.Second;
        var firstMinuteValue = minuteParsers.Select(x => x.First()).Min();
        var firstHourValue = hourParsers.Select(x => x.First()).Min();

        // 定义一个标识，标识当前时间下一个发生时间值是否进入新一轮循环
        // 如：如果当前时间的秒数为 59，那么下一个秒数应该为 00，那么当前时间分钟就应该 +1
        // 以此类推，如果 +1 后分钟数为 59，那么下一个分钟数也应该为 00，那么当前时间小时数就应该 +1
        // ....
        var overflow = true;

        // 处理 Cron 格式化类型包含秒的情况 =================================================================
        var newSeconds = newValue.Second;
        if (isSecondFormat)
        {
            // 获取秒所有字符解析器
            var secondParsers = Parsers[CrontabFieldKind.Second].Where(x => x is ITimeParser).Cast<ITimeParser>().ToList();

            // 获取秒解析器最小起始值
            firstSecondValue = secondParsers.Select(x => x.First()).Min();

            // 获取秒下一个发生值
            newSeconds = Increment(secondParsers, newValue.Second, firstSecondValue, out overflow);

            // 设置起始时间为下一个秒时间
            newValue = new DateTime(newValue.Year, newValue.Month, newValue.Day, newValue.Hour, newValue.Minute, newSeconds);

            // 如果当前秒并没有进入下一轮循环但存在不匹配的字符过滤器
            if (!overflow && !IsMatch(newValue))
            {
                // 重置秒为起始值并标记 overflow 为 true 进入新一轮循环
                newSeconds = firstSecondValue;

                // 此时计算时间秒部分应该为起始值
                // 如 22:10:59 -> 22:10:00
                newValue = new DateTime(newValue.Year, newValue.Month, newValue.Day, newValue.Hour, newValue.Minute, newSeconds);

                // 标记进入下一轮循环
                overflow = true;
            }

            // 如果程序到达这里，说明并没有进入上面分支，则直接返回下一秒时间
            if (!overflow)
            {
                return MinDate(newValue, endTime);
            }
        }

        // 程序到达这里，说明秒部分已经标识进入新一轮循环，那么分支就应该获取下一个分钟发生值 =================================================================
        var newMinutes = Increment(minuteParsers, newValue.Minute + (overflow ? 0 : -1), firstMinuteValue, out overflow);

        // 设置起始时间为下一个分钟时间
        newValue = new DateTime(newValue.Year, newValue.Month, newValue.Day, newValue.Hour, newMinutes, overflow ? firstSecondValue : newSeconds);

        // 如果当前分钟并没有进入下一轮循环但存在不匹配的字符过滤器
        if (!overflow && !IsMatch(newValue))
        {
            // 重置秒，分钟为起始值并标记 overflow 为 true 进入新一轮循环
            newSeconds = firstSecondValue;
            newMinutes = firstMinuteValue;

            // 此时计算时间秒和分钟部分应该为起始值
            // 如 22:59:59 -> 22:00:00
            newValue = new DateTime(newValue.Year, newValue.Month, newValue.Day, newValue.Hour, newMinutes, firstSecondValue);

            // 标记进入下一轮循环
            overflow = true;
        }

        // 如果程序到达这里，说明并没有进入上面分支，则直接返回下一分钟时间
        if (!overflow)
        {
            return MinDate(newValue, endTime);
        }

        // 程序到达这里，说明分钟部分已经标识进入新一轮循环，那么分支就应该获取下一个小时发生值 =================================================================
        var newHours = Increment(hourParsers, newValue.Hour + (overflow ? 0 : -1), firstHourValue, out overflow);

        // 设置起始时间为下一个小时时间
        newValue = new DateTime(newValue.Year, newValue.Month, newValue.Day, newHours,
            overflow ? firstMinuteValue : newMinutes,
            overflow ? firstSecondValue : newSeconds);

        // 如果当前小时并没有进入下一轮循环但存在不匹配的字符过滤器
        if (!overflow && !IsMatch(newValue))
        {
            // 此时计算时间秒，分钟和小时部分应该为起始值
            // 如 23:59:59 -> 23:00:00
            newValue = new DateTime(newValue.Year, newValue.Month, newValue.Day, firstHourValue, firstMinuteValue, firstSecondValue);

            // 标记进入下一轮循环
            overflow = true;
        }

        // 如果程序到达这里，说明并没有进入上面分支，则直接返回下一小时时间
        if (!overflow)
        {
            return MinDate(newValue, endTime);
        }

        // 如果程序达到这里，说明天数变了（一旦天数变了，那么月份可能也变了，星期可能也变了，年份也可能变了）
        // 所以这里的计算最为复杂
        List<ITimeParser> yearParsers = null;

        // 首先先判断当前 Cron 格式化类型是否支持年份
        var isYearFormat = Format == CronStringFormat.WithYears || Format == CronStringFormat.WithSecondsAndYears;

        // 如果支持，读取年份字符过滤器
        if (isYearFormat)
        {
            yearParsers = Parsers[CrontabFieldKind.Year].Where(x => x is ITimeParser).Cast<ITimeParser>().ToList();
        }

        // 程序能够执行到这里，那么说明时间已经是 23:59:59，所以起始时间追加 1 天
        // 这里的代码看起来很奇怪，但是是为了处理终止时间为 12/31/9999 23:59:59.999 的情况，也就是世界末日了~~~
        try
        {
            newValue = newValue.AddDays(1);
        }
        catch
        {
            return endTime;
        }

        // 在有效的年份时间内死循环至天、周、月、年全部匹配才终止循环
        while (!(IsMatch(newValue, CrontabFieldKind.Day)
            && IsMatch(newValue, CrontabFieldKind.DayOfWeek)
            && IsMatch(newValue, CrontabFieldKind.Month)
            && (!isYearFormat || IsMatch(newValue, CrontabFieldKind.Year))))
        {
            // 如果当前匹配到的时间已经大于或等于终止时间，则直接返回
            if (newValue >= endTime)
            {
                return MinDate(newValue, endTime);
            }

            // 如果 Cron 年份字段域获取下一个发生值为 null，那么直接返回 终止时间
            // 也就是已经没有匹配项了
            if (isYearFormat && yearParsers!.Select(x => x.Next(newValue.Year - 1)).All(x => x == null))
            {
                return endTime;
            }

            // 同样防止终止时间为 12/31/9999 23:59:59.999 的情况
            try
            {
                // 不断增加 1 天直至匹配成功
                newValue = newValue.AddDays(1);
            }
            catch
            {
                return endTime;
            }
        }

        return MinDate(newValue, endTime);
    }

    /// <summary>
    /// 获取当前时间解析器下一个发生值
    /// </summary>
    /// <param name="parsers">解析器</param>
    /// <param name="value">当前值</param>
    /// <param name="defaultValue">默认值</param>
    /// <param name="overflow">控制秒、分钟、小时到达59秒/分和23小时开关</param>
    /// <returns><see cref="int"/></returns>
    private static int Increment(IEnumerable<ITimeParser> parsers, int value, int defaultValue, out bool overflow)
    {
        var nextValue = parsers.Select(x => x.Next(value))
            .Where(x => x > value)
            .Min()
            ?? defaultValue;

        // 如果此时秒或分钟或23到达最大值，则应该返回起始值
        overflow = nextValue <= value;

        return nextValue;
    }

    /// <summary>
    /// 处理下一个发生时间边界值
    /// </summary>
    /// <remarks>如果发生时间大于终止时间，则返回终止时间，否则返回发生时间</remarks>
    /// <param name="newTime">下一个发生时间</param>
    /// <param name="endTime">终止时间</param>
    /// <returns><see cref="DateTime"/></returns>
    private static DateTime MinDate(DateTime newTime, DateTime endTime)
    {
        return newTime >= endTime ? endTime : newTime;
    }

    /// <summary>
    /// 判断 Cron 所有字段字符解析器是否都能匹配当前时间各个部分
    /// </summary>
    /// <param name="datetime">当前时间</param>
    /// <returns><see cref="bool"/></returns>
    private bool IsMatch(DateTime datetime)
    {
        return Parsers.All(fieldKind =>
            fieldKind.Value.Any(parser => parser.IsMatch(datetime))
        );
    }

    /// <summary>
    /// 判断当前 Cron 字段类型字符解析器和当前时间至少存在一种匹配
    /// </summary>
    /// <param name="datetime">当前时间</param>
    /// <param name="kind">Cron 字段种类</param>
    /// <returns></returns>
    private bool IsMatch(DateTime datetime, CrontabFieldKind kind)
    {
        return Parsers.Where(x => x.Key == kind)
            .SelectMany(x => x.Value)
            .Any(parser => parser.IsMatch(datetime));
    }

    /// <summary>
    /// 将 Cron 字段解析器转换成字符串
    /// </summary>
    /// <param name="paramList">Cron 字段字符串集合</param>
    /// <param name="kind">Cron 字段种类</param>
    private void JoinParsers(List<string> paramList, CrontabFieldKind kind)
    {
        paramList.Add(
            string.Join(",", Parsers
                .Where(x => x.Key == kind)
                .SelectMany(x => x.Value.Select(y => y.ToString())).ToArray()
            )
        );
    }
}
/**
 * 作业触发器类型
 */
export declare interface Trigger {
  /**
   * 支持索引获取
   */
  [index: string]: any;
  /**
   * 作业触发器 Id
   */
  triggerId?: string | null;
  /**
   * 作业 Id
   */
  jobId?: string | null;
  /**
   * 作业触发器类型
   */
  triggerType?: string | null;
  /**
   * 作业触发器类型所在程序集
   */
  assemblyName?: string | null;
  /**
   * 作业触发器参数
   */
  args?: string | null;
  /**
   * 描述信息
   */
  description?: string | null;
  /**
   * 作业触发器状态
   */
  status?: number | null;
  /**
   * 起始时间
   */
  startTime?: string | null;
  /**
   * 结束时间
   */
  endTime?: string | null;
  /**
   * 最近运行时间
   */
  lastRunTime?: string | null;
  /**
   * 下一次运行时间
   */
  nextRunTime?: string | null;
  /**
   * 触发次数
   */
  numberOfRuns?: number | null;
  /**
   * 最大触发次数
   */
  maxNumberOfRuns?: number | null;
  /**
   * 出错次数
   */
  numberOfErrors?: number | null;
  /**
   * 最大出错次数
   */
  maxNumberOfErrors?: number | null;
  /**
   * 重试次数
   */
  numRetries?: number | null;
  /**
   * 重试间隔时间
   */
  retryTimeout?: number | null;
  /**
   * 是否立即启动
   */
  startNow?: boolean | null;
  /**
   * 是否启动时执行一次
   */
  runOnStart?: boolean | null;
  /**
   * 是否在启动时重置最大触发次数等于一次的作业
   */
  resetOnlyOnce?: boolean | null;
  /**
   * 执行结果
   */
  result?: string | null;
  /**
   * 执行耗时
   */
  elapsedTime?: number | null;
  /**
   * 作业触发器更新时间
   */
  updatedTime?: string | null;
}

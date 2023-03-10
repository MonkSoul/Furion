/**
 * 作业触发器运行记录类型
 */
export declare interface TriggerTimeline {
  /**
   * 支持索引获取
   */
  [index: string]: any;

  /**
   * 最近运行时间
   */
  lastRunTime?: string | null;

  /**
   * 触发次数
   */
  numberOfRuns?: number | null;

  /**
   * 下一次运行时间
   */
  nextRunTime?: string | null;

  /**
   * 作业触发器状态
   */
  status?: number | null;

  /**
   * 执行结果
   */
  result?: string | null;

  /**
   * 执行耗时
   */
  elapsedTime?: number | null;
}

import { Trigger } from "./trigger";

/**
 * 作业信息类型
 */
export declare interface JobDetail {
  /**
   * 支持索引获取
   */
  [index: string]: any;
  /**
   * 作业 Id
   */
  jobId?: string | null;
  /**
   * 作业组名称
   */
  groupName?: string | null;
  /**
   * 作业处理程序类型
   */
  jobType?: string | null;
  /**
   * 作业处理程序类型所在程序集
   */
  assemblyName?: string | null;
  /**
   * 描述信息
   */
  description?: string | null;
  /**
   * 作业执行方式
   */
  concurrent?: boolean | null;
  /**
   * 是否配置扫描作业处理程序特性作业触发器
   */
  includeAnnotations?: boolean | null;
  /**
   * 作业信息额外数据
   */
  properties?: string | null;
  /**
   * 作业更新时间
   */
  updatedTime?: string | null;
  /**
   * 触发器列表
   */
  triggers?: Trigger[] | null;

  /**
   * 刷新时间
   */
  refreshDate?: Date | null;
}

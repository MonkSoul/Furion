import { JobDetail } from "./jobdetail";
import { Trigger } from "./trigger";

/**
 * 作业计划类型
 */
export declare interface Scheduler {
  /**
   * 作业信息
   */
  jobDetail?: JobDetail | null;
  /**
   * 作业触发器集合
   */
  triggers?: Trigger[] | null;
}

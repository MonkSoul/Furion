import {
  IconCalendarClock,
  IconDelete,
  IconMore,
  IconRestart,
  IconStop
} from "@douyinfe/semi-icons";
import {
  Button,
  Descriptions,
  Divider,
  Dropdown,
  Modal,
  Popconfirm,
  Table,
  Tag,
  Timeline,
  Toast,
  Typography
} from "@douyinfe/semi-ui";
import { Data } from "@douyinfe/semi-ui/lib/es/descriptions";
import {
  ExpandedRowRender,
  OnRow
} from "@douyinfe/semi-ui/lib/es/table/interface";
import { useCallback, useContext, useEffect, useMemo, useState } from "react";
import useFetch from "use-http/dist/cjs/useFetch";
import { JobDetail, Scheduler, Trigger, TriggerTimeline } from "../../types";
import apiconfig from "./apiconfig";
import columns from "./columns";
import GlobalContext from "./context";
import StatusText from "./state-text";

const style = {
  boxShadow: "var(--semi-shadow-elevated)",
  backgroundColor: "var(--semi-color-bg-2)",
  borderRadius: "4px",
  padding: "10px",
  margin: "10px",
  width: "350px",
};

export default function Jobs() {
  /**
   * 作业状态
   */
  const [jobs, setJobs] = useState<Scheduler[]>([]);
  const { rate } = useContext(GlobalContext);

  /**
   * 初始化请求配置
   */
  const { post, response, loading } = useFetch(
    apiconfig.hostAddress,
    apiconfig.options
  );

  /**
   * 获取内存中所有作业
   */
  const loadJobs = async () => {
    const data = await post("/get-jobs");
    if (response.ok) setJobs((s) => data);
  };

  /**
   * 操作作业触发器
   */
  const callAction = async (
    jobid: string,
    triggerid: string,
    action: string
  ) => {
    const data = await post(
      "/operate-trigger?jobid=" +
        jobid +
        "&triggerid=" +
        triggerid +
        "&action=" +
        action
    );

    if (response.ok) {
      Toast.success({
        content: "操作成功",
        duration: 3,
      });
    } else {
      Toast.error({
        content: "操作失败",
        duration: 3,
      });
    }
  };

  /**
   * 生成表格类型数据
   */
  const data: JobDetail[] = useMemo(() => {
    const jobDetails: JobDetail[] = [];
    if (!jobs || jobs.length === 0) return jobDetails;

    for (const scheduler of jobs) {
      let jobDetail = scheduler.jobDetail!;
      jobDetail.triggers = scheduler.triggers;
      jobDetails.push(jobDetail);
    }

    return jobDetails;
  }, [jobs]);

  /**
   * 初始化
   */
  useEffect(() => {
    const timer = setInterval(() => {
      loadJobs();
    }, rate);

    return () => {
      clearInterval(timer);
    };
  }, [rate]);

  /**
   * 展开行渲染
   */
  const expandRowRender: ExpandedRowRender<JobDetail> = useCallback(
    (jobDetail, index) => {
      // 查找作业计划
      var scheduler = jobs.find((u) => u.jobDetail?.jobId === jobDetail?.jobId);

      // 构建触发器列表
      const triggerData: Data[][] = [];
      for (const trigger of scheduler?.triggers!) {
        const triggerItem: Data[] = [];
        for (const prop in trigger) {
          triggerItem.push({
            key: prop.charAt(0).toUpperCase() + prop.slice(1),
            value: (
              <RenderValue
                prop={prop}
                value={trigger[prop]}
                trigger={trigger}
              />
            ),
            ovalue: trigger[prop],
          } as Data);
        }

        triggerData.push(triggerItem);
      }

      return (
        <div style={{ display: "flex", flexWrap: "wrap" }}>
          {triggerData.map((expandData, index) => (
            <div
              style={style}
              key={
                (expandData[0] as any).ovalue!.toString() +
                "_" +
                (expandData[1] as any).ovalue!.toString() +
                index
              }
            >
              <div style={{ marginTop: 3, marginRight: 5, textAlign: "right" }}>
                <Dropdown
                  render={
                    <Dropdown.Menu>
                      <Dropdown.Item
                        onClick={() =>
                          callAction(
                            (expandData[1] as any).ovalue!.toString(),
                            (expandData[0] as any).ovalue!.toString(),
                            "start"
                          )
                        }
                      >
                        <IconRestart size="extra-large" /> 启动
                      </Dropdown.Item>
                      <Dropdown.Item
                        onClick={() =>
                          callAction(
                            (expandData[1] as any).ovalue!.toString(),
                            (expandData[0] as any).ovalue!.toString(),
                            "pause"
                          )
                        }
                      >
                        <IconStop size="extra-large" /> 暂停
                      </Dropdown.Item>
                      <Dropdown.Item>
                        <Popconfirm
                          zIndex={10000000}
                          title={
                            "确定删除当前作业触发器 [" +
                            (expandData[0] as any).ovalue!.toString() +
                            "] 吗？"
                          }
                          onConfirm={() =>
                            callAction(
                              (expandData[1] as any).ovalue!.toString(),
                              (expandData[0] as any).ovalue!.toString(),
                              "remove"
                            )
                          }
                        >
                          <IconDelete size="small" /> &nbsp;删除
                        </Popconfirm>
                      </Dropdown.Item>
                    </Dropdown.Menu>
                  }
                >
                  <IconMore style={{ cursor: "pointer" }} />
                </Dropdown>
              </div>

              <Divider margin="8px" />
              <Descriptions align="left" data={expandData} />
            </div>
          ))}
        </div>
      );
    },
    [jobs]
  );

  const handleRow: OnRow<JobDetail> = (jobDetail, index) => {
    // 给偶数行设置斑马纹
    if (index! % 2 === 0) {
      return {
        style: {
          background: "var(--semi-color-fill-0)",
        },
      };
    } else {
      return {};
    }
  };

  return (
    <Table
      rowKey="jobId"
      columns={columns}
      dataSource={data}
      onRow={handleRow}
      expandedRowRender={expandRowRender}
      pagination={false}
      rowExpandable={(jobDetail) =>
        !!(
          jobDetail?.jobId &&
          jobs.find((u) => u.jobDetail?.jobId === jobDetail?.jobId)?.triggers
            ?.length !== 0
        )
      }
    />
  );
}

/**
 * 渲染触发器属性值
 * @param props
 * @returns
 */
function RenderValue(props: { prop: string; value: any; trigger: Trigger }) {
  const { prop, value, trigger } = props;
  const [visible, setVisible] = useState(false);
  const [timelines, setTimelines] = useState<TriggerTimeline[]>([]);
  const { Text } = Typography;

  /**
   * 初始化请求配置
   */
  const { post, response, loading } = useFetch(
    apiconfig.hostAddress,
    apiconfig.options
  );

  const showDialog = () => {
    setVisible(true);
    getTimelines(trigger.jobId!, trigger.triggerId!);
  };
  const handleOk = () => {
    setVisible(false);
  };
  const handleCancel = () => {
    setVisible(false);
  };
  const handleAfterClose = () => {};

  /**
   * 操作作业触发器
   */
  const getTimelines = async (jobid: string, triggerid: string) => {
    const data = await post(
      "/operate-trigger?jobid=" +
        jobid +
        "&triggerid=" +
        triggerid +
        "&action=timelines"
    );

    if (response.ok) setTimelines(data);
  };

  /**
   * 构建预览节点
   */
  let preview = <span>{value?.toString() || ""}</span>;

  /**
   * 处理下一次运行时间
   */
  if (prop === "nextRunTime") {
    preview = value ? (
      <Tag color="light-green" type="solid">
        {value}
      </Tag>
    ) : (
      <span></span>
    );
  } else if (prop === "lastRunTime") {
    /**
     * 处理最近运行时间
     */
    preview = value ? (
      <>
        <Tag color="grey" type="light" style={{ verticalAlign: "middle" }}>
          {value}
        </Tag>
        <Button
          size="small"
          icon={<IconCalendarClock />}
          style={{ marginLeft: 5, verticalAlign: "middle", fontSize: 12 }}
          onClick={showDialog}
        >
          记录
        </Button>
      </>
    ) : (
      <span></span>
    );
  } else if (prop === "numberOfRuns") {
    /**
     * 处理运行次数
     */
    preview = (
      <Tag color="green" type="light">
        {value}
      </Tag>
    );
  } else if (prop === "numberOfErrors") {
    /**
     * 处理错误次数
     */
    preview = (
      <Tag color="red" type="ghost">
        {value}
      </Tag>
    );
  } else if (prop === "status") {
    /**
     * 处理状态
     */
    preview = <StatusText value={Number(value)} />;
  } else if (
    prop === "startNow" ||
    prop === "runOnStart" ||
    prop === "resetOnlyOnce"
  ) {
    /**
     * 处理 bool 类型
     */
    preview = (
      <Tag color="blue" type="ghost">
        {value === true ? "是" : "否"}
      </Tag>
    );
  } else if (prop === "triggerId" || prop === "jobId") {
    /**
     * 处理触发器和作业 Id
     */
    preview = (
      <span style={{ textDecoration: "underline", fontWeight: "bold" }}>
        {value?.toString() || ""}
      </span>
    );
  } else if (prop === "args") {
    /**
     * 处理参数类型
     */
    preview = value ? (
      <Text mark>{value?.toString() || ""}</Text>
    ) : (
      <span></span>
    );
  } else if (prop === "triggerType") {
    /**
     * 处理触发器类型
     */
    preview = <span>{value?.toString() || ""}</span>;
  } else preview = <span>{value?.toString() || ""}</span>;

  return (
    <>
      {preview}
      <Modal
        title={trigger.triggerId + " 最近运行记录"}
        visible={visible}
        onOk={handleOk}
        afterClose={handleAfterClose} //>=1.16.0
        onCancel={handleCancel}
        closeOnEsc={true}
      >
        <Timeline mode="center">
          {timelines.map((timeline, i) => (
            <Timeline.Item
              key={timeline.numberOfRuns!}
              time={
                <>
                  <Tag color="grey" type="light">
                    {timeline.lastRunTime}
                  </Tag>
                </>
              }
            >
              第{" "}
              <Tag color="green" type="light">
                {timeline.numberOfRuns}
              </Tag>{" "}
              次运行
            </Timeline.Item>
          ))}
        </Timeline>
      </Modal>
    </>
  );
}

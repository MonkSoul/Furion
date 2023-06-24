import {
  IconDelete,
  IconMore,
  IconPlayCircle,
  IconStop,
} from "@douyinfe/semi-icons";
import {
  Descriptions,
  Divider,
  Dropdown,
  Popconfirm,
  Table,
  Toast,
  Tooltip,
} from "@douyinfe/semi-ui";
import { Data } from "@douyinfe/semi-ui/lib/es/descriptions";
import {
  ExpandedRowRender,
  OnRow,
} from "@douyinfe/semi-ui/lib/es/table/interface";
import { useCallback, useEffect, useMemo, useState } from "react";
import useFetch from "use-http/dist/cjs/useFetch";
import { JobDetail, Scheduler } from "../../types";
import apiconfig from "./apiconfig";
import columns from "./columns";
import RenderValue from "./render-value";

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

  /**
   * 初始化请求配置
   */
  const { post, response } = useFetch(apiconfig.hostAddress, apiconfig.options);

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
    await post(
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
      jobDetail.refreshDate = new Date();
      jobDetails.push(jobDetail);
    }

    return jobDetails;
  }, [jobs]);

  useEffect(() => {
    loadJobs();

    var eventSource = new EventSource(apiconfig.hostAddress + "/check-change");

    eventSource.onmessage = function (e) {
      loadJobs();
    };

    return () => {
      eventSource.close();
    };
  }, []);

  /**
   * 展开行渲染
   */
  const expandRowRender: ExpandedRowRender<JobDetail> = useCallback(
    (jobDetail, index) => {
      // 查找作业计划
      var scheduler = jobs.find((u) => u.jobDetail?.jobId === jobDetail?.jobId);
      if (!scheduler) return <></>;

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
              <div
                style={{
                  marginTop: 3,
                  marginRight: 5,
                  marginLeft: 5,
                  display: "flex",
                  justifyContent: "space-between",
                }}
              >
                {Number((expandData[6] as any).ovalue) === 3 ? (
                  <Tooltip content="启动">
                    <IconPlayCircle
                      style={{ color: "red", cursor: "pointer" }}
                      size="large"
                      onClick={() =>
                        callAction(
                          (expandData[1] as any).ovalue!.toString(),
                          (expandData[0] as any).ovalue!.toString(),
                          "start"
                        )
                      }
                    />
                  </Tooltip>
                ) : (
                  <span></span>
                )}
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
                        <IconPlayCircle size="extra-large" /> 启动
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
                  <IconMore style={{ cursor: "pointer" }} size="large" />
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

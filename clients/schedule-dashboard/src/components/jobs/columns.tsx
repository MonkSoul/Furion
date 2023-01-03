import {
  IconDelete,
  IconMore,
  IconRestart,
  IconStop
} from "@douyinfe/semi-icons";
import {
  Descriptions,
  Divider,
  Dropdown,
  Popconfirm,
  Popover,
  Tag,
  Toast
} from "@douyinfe/semi-ui";
import { Data } from "@douyinfe/semi-ui/lib/es/descriptions";
import { ColumnProps } from "@douyinfe/semi-ui/lib/es/table/interface";
import useFetch from "use-http";
import { JobDetail, Trigger } from "../../types";
import apiconfig from "./apiconfig";

const style = {
  padding: "10px",
};

/**
 * 获取触发器简要渲染
 * @param trigger
 * @returns
 */
function getData(trigger: Trigger): Data[] {
  return [
    {
      key: "TriggerId",
      value: (
        <span style={{ textDecoration: "underline", fontWeight: "bold" }}>
          {trigger.triggerId || ""}
        </span>
      ),
    },
    {
      key: "LastRunTime",
      value: trigger.lastRunTime ? (
        <Tag color="grey" type="light">
          {trigger.lastRunTime}
        </Tag>
      ) : (
        <span></span>
      ),
    },
    {
      key: "NextRunTime",
      value: trigger.nextRunTime ? (
        <Tag color="light-green" type="solid">
          {trigger.nextRunTime}
        </Tag>
      ) : (
        <span></span>
      ),
    },
    {
      key: "NumberOfRuns",
      value: (
        <Tag color="green" type="light">
          {trigger.numberOfRuns || 0}
        </Tag>
      ),
    },
  ];
}

/**
 * 表格列配置
 */
const columns: ColumnProps<JobDetail>[] = [
  {
    title: "JobId",
    dataIndex: "jobId",
    render: (text, jobDetail, index) => {
      return (
        <Popover
          content={
            <div style={style}>
              {jobDetail.triggers?.map((t, i) => (
                <>
                  <Descriptions key={t.triggerId} data={getData(t)} />
                  {i !== jobDetail.triggers?.length! - 1 && (
                    <Divider margin="8px" />
                  )}
                </>
              ))}
            </div>
          }
          position="right"
          showArrow
        >
          <span style={{ textDecoration: "underline", fontWeight: "bold" }}>
            {text}
          </span>
        </Popover>
      );
    },
  },
  {
    title: "GroupName",
    dataIndex: "groupName",
  },
  {
    title: "Description",
    dataIndex: "description",
  },
  {
    title: "JobType",
    dataIndex: "jobType",
  },
  {
    title: "AssemblyName",
    dataIndex: "assemblyName",
  },

  {
    title: "Concurrent",
    dataIndex: "concurrent",
    align: "center",
    render: (text, jobDetail, index) => {
      return jobDetail.concurrent === true ? (
        <Tag color="red" type="light">
          并行
        </Tag>
      ) : (
        <Tag color="red" type="solid">
          串行
        </Tag>
      );
    },
  },
  {
    title: "IncludeAnnotations",
    dataIndex: "includeAnnotations",
    align: "center",
    render: (text, jobDetail, index) => {
      return jobDetail.includeAnnotations === true ? (
        <Tag color="blue" type="light">
          是
        </Tag>
      ) : (
        <Tag color="blue" type="ghost">
          否
        </Tag>
      );
    },
  },
  {
    title: "Properties",
    dataIndex: "properties",
  },
  {
    title: "UpdatedTime",
    dataIndex: "updatedTime",
  },
  {
    title: "",
    dataIndex: "operate",
    render: (text, jobDetail, index) => <Operation jobid={jobDetail.jobId} />,
  },
];

/**
 * 操作按钮
 * @param props
 * @returns
 */
function Operation(props: { jobid?: string | null }) {
  const { jobid } = props;

  /**
   * 初始化请求配置
   */
  const { post, response, loading } = useFetch(
    apiconfig.hostAddress,
    apiconfig.options
  );

  /**
   * 操作作业
   */
  const callAction = async (action: string) => {
    const data = await post(
      "/operate-job?jobid=" + jobid + "&action=" + action
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

  return (
    <Dropdown
      render={
        <Dropdown.Menu>
          <Dropdown.Item onClick={() => callAction("start")}>
            <IconRestart size="extra-large" /> 启动
          </Dropdown.Item>
          <Dropdown.Item onClick={() => callAction("pause")}>
            <IconStop size="extra-large" /> 暂停
          </Dropdown.Item>
          <Dropdown.Item>
            <Popconfirm
              zIndex={10000000}
              title={"确定删除当前作业 [" + jobid + "] 吗？"}
              onConfirm={() => callAction("remove")}
            >
              <IconDelete size="small" /> &nbsp;删除
            </Popconfirm>
          </Dropdown.Item>
        </Dropdown.Menu>
      }
    >
      <IconMore style={{ cursor: "pointer" }} />
    </Dropdown>
  );
}

export default columns;

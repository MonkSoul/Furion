import {
  IconDelete,
  IconMore,
  IconPlayCircle,
  IconStop,
  IconVigoLogo
} from "@douyinfe/semi-icons";
import {
  Descriptions,
  Divider,
  Dropdown,
  Popconfirm,
  Popover,
  Tag,
  Toast,
  Tooltip,
  Typography
} from "@douyinfe/semi-ui";
import { Data } from "@douyinfe/semi-ui/lib/es/descriptions";
import { ColumnProps } from "@douyinfe/semi-ui/lib/es/table/interface";
import Paragraph from "@douyinfe/semi-ui/lib/es/typography/paragraph";
import dayjs from "dayjs";
import "dayjs/locale/zh-cn";
import relativeTime from "dayjs/plugin/relativeTime";
import useFetch from "use-http";
import { JobDetail, Trigger } from "../../types";
import apiconfig from "./apiconfig";
import RenderValue from "./render-value";
import StatusText from "./state-text";
dayjs.extend(relativeTime);
dayjs.locale("zh-cn");

const style = {
  padding: "10px",
};

const showProps = [
  "triggerId",
  "description",
  "status",
  "lastRunTime",
  "nextRunTime",
  "numberOfRuns",
  "elapsedTime",
];

/**
 * 获取触发器简要渲染
 * @param trigger
 * @returns
 */
function getData(trigger: Trigger): Data[] {
  var data: Data[] = [];
  for (const prop of showProps) {
    data.push({
      key: prop.charAt(0).toUpperCase() + prop.slice(1),
      value: (
        <RenderValue prop={prop} value={trigger[prop]} trigger={trigger} />
      ),
    });
  }

  return data;
}

/**
 * 表格列配置
 */
const columns: ColumnProps<JobDetail>[] = [
  {
    title: "JobId",
    dataIndex: "jobId",
    width: 150,
    render: (text, jobDetail, index) => {
      return (
        <>
          <Popover
            content={
              <div style={style}>
                {jobDetail.triggers?.map((t, i) => (
                  <div key={t.triggerId}>
                    <Descriptions data={getData(t)} />
                    {i !== jobDetail.triggers?.length! - 1 && (
                      <Divider margin="8px" />
                    )}
                  </div>
                ))}
              </div>
            }
            position="right"
            showArrow
          >
            <Paragraph
              copyable
              underline
              strong
              style={{ display: "inline-block" }}
            >
              {text}
            </Paragraph>
          </Popover>
          {(jobDetail.triggers?.length || 0) > 0 &&
            jobDetail.triggers?.filter((u) => u.status === 3).length ===
              jobDetail.triggers?.length && (
              <span style={{ marginLeft: 5 }}>
                <StatusText value={3} />
              </span>
            )}
        </>
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
    render: (text, jobDetail, index) => {
      const value: string = text || "";
      return (
        <Tooltip content={text}>
          {value.length >= 8 ? value.substring(0, 8) + "..." : value}
        </Tooltip>
      );
    },
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
        <Tooltip content={"默认执行方式，不会等待上一次任务完成"}>
          <Tag color="red" type="light">
            并行
          </Tag>
        </Tooltip>
      ) : (
        <Tooltip
          content={
            "如果上一次任务未完成，则进入阻塞状态，并在下一次触发时间尝试执行"
          }
        >
          <Tag color="red" type="solid">
            串行
          </Tag>
        </Tooltip>
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
    render: (text, jobDetail, index) => {
      return (
        <Typography.Paragraph
          ellipsis={{
            rows: 2,
            expandable: true,
            collapsible: true,
            collapseText: "折叠",
          }}
          style={{ width: 200 }}
          copyable
        >
          {text}
        </Typography.Paragraph>
      );
    },
  },
  {
    title: "lastRunTime",
    dataIndex: "lastRunTime",
    width: 150,
    render: (text, jobDetail, index) => {
      var lastRunTimes =
        jobDetail.triggers
          ?.filter((u) => !!u.lastRunTime)
          ?.map((u) => new Date(u.lastRunTime!)) || [];

      var lastRunTime =
        lastRunTimes.length === 0
          ? null
          : new Date(Math.max.apply(null, lastRunTimes as any));

      return lastRunTime ? (
        <Tooltip content={dayjs(lastRunTime).format("YYYY/MM/DD HH:mm:ss")}>
          <Tag color="grey" type="light" style={{ verticalAlign: "middle" }}>
            {dayjs(lastRunTime).fromNow()}
          </Tag>
        </Tooltip>
      ) : (
        <></>
      );
    },
  },
  {
    title: "UpdatedTime",
    dataIndex: "updatedTime",
  },
  {
    title: "",
    dataIndex: "operate",
    width: 50,
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
            <IconPlayCircle size="extra-large" /> 启动
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
          <Dropdown.Item onClick={() => callAction("run")}>
            <IconVigoLogo size="extra-large" /> 立即执行
          </Dropdown.Item>
        </Dropdown.Menu>
      }
    >
      <IconMore style={{ cursor: "pointer" }} />
    </Dropdown>
  );
}

export default columns;

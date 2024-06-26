import { IconUploadError } from "@douyinfe/semi-icons";
import { Popover, Space, Tag, Tooltip } from "@douyinfe/semi-ui";
import { ReactNode } from "react";

const status = {
  "0": "积压",
  "1": "就绪",
  "2": "运行",
  "3": "暂停",
  "4": "阻塞",
  "5": "就绪 *",
  "6": "归档",
  "7": "崩溃",
  "8": "超限",
  "9": "无触发时间",
  "10": "初始未启动",
  "11": "未知触发器",
  "12": "未知处理程序",
};

/**
 * 触发器状态
 * @param props
 * @returns
 */
export default function StatusText(props: {
  value: number;
  showError?: boolean;
  onErrorClick?: VoidFunction;
}) {
  const { value } = props;

  const createTagStatus = () => {
    var count = Object.keys(status).length;
    var lines = Math.ceil(count / 3);
    const list: ReactNode[] = [];
    for (let i = 0; i < lines; i++) {
      var begin = (i + 1) * 3 - 3;
      var end = Math.min(count, begin + 3);
      var items: ReactNode[] = [];

      for (let j = begin; j < end; j++) {
        items.push(
          <TagText key={j} current={value} value={j}>
            {(status as any)[j.toString()]}
          </TagText>
        );
      }

      list.push(<Space key={i}>{items}</Space>);
    }

    return list;
  };

  return (
    <Space>
      <Popover
        content={
          <div>
            <Space vertical>{createTagStatus()}</Space>
          </div>
        }
        position="right"
        showArrow
        zIndex={10000000001}
      >
        <Tag
          color={value === 3 ? "red" : "light-blue"}
          type={value === 3 ? "solid" : "light"}
        >
          {(status as any)[value.toString()]}
        </Tag>
      </Popover>
      {props.showError === true && value === 5 && (
        <Tooltip content="有异常">
          <IconUploadError
            style={{ color: "red", cursor: "pointer" }}
            onClick={props.onErrorClick}
          />
        </Tooltip>
      )}
    </Space>
  );
}

function TagText(props: {
  current: number;
  value: number;
  children: ReactNode;
}) {
  const { value, children, current } = props;
  return (
    <Tag color={current === value ? "green" : "light-blue"} type="light">
      {children}: {value}
    </Tag>
  );
}

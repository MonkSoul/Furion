import { Popover, Space, Tag } from "@douyinfe/semi-ui";
import { useMemo } from "react";

/**
 * 触发器状态
 * @param props
 * @returns
 */
export default function StatusText(props: { value: number }) {
  const { value } = props;

  const text: string = useMemo(() => {
    switch (value) {
      case 0:
        return "积压";
      case 1:
        return "就绪";
      case 2:
        return "运行";
      case 3:
        return "暂停";
      case 4:
        return "阻塞";
      case 5:
        return "就绪*";
      case 6:
        return "归档";
      case 7:
        return "奔溃";
      case 8:
        return "超限";
      case 9:
        return "无触发时间";
      case 10:
        return "初始未启动";
      case 11:
        return "未知触发器";
      case 12:
        return "未知处理程序";
      default:
        return "";
    }
  }, [value]);

  return (
    <Popover
      content={
        <div>
          <Space vertical>
            <Space>
              <Tag color="light-blue" type="light">
                积压: 0
              </Tag>
              <Tag color="light-blue" type="light">
                就绪: 1
              </Tag>
              <Tag color="light-blue" type="light">
                运行: 2
              </Tag>
            </Space>
            <Space>
              <Tag color="light-blue" type="light">
                暂停: 3
              </Tag>
              <Tag color="light-blue" type="light">
                阻塞: 4
              </Tag>
              <Tag color="light-blue" type="light">
                就绪*: 5
              </Tag>
            </Space>
            <Space>
              <Tag color="light-blue" type="light">
                归档: 6
              </Tag>
              <Tag color="light-blue" type="light">
                崩溃: 7
              </Tag>
              <Tag color="light-blue" type="light">
                超限: 8
              </Tag>
            </Space>
            <Space>
              <Tag color="light-blue" type="light">
                无触发时间: 9
              </Tag>
              <Tag color="light-blue" type="light">
                初始未启动: 10
              </Tag>
              <Tag color="light-blue" type="light">
                未知触发器: 11
              </Tag>
            </Space>
            <Space>
              <Tag color="light-blue" type="light">
                未知处理程序: 12
              </Tag>
            </Space>
          </Space>
        </div>
      }
      position="right"
      showArrow
    >
      <Tag color="light-blue" type="light">
        {text}
      </Tag>
    </Popover>
  );
}

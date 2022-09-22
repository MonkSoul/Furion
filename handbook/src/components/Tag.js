import React from "react";
import styled from "styled-components";
import IconFont from "./iconfonts";

export default function (props) {
  const { children } = props;
  const operates = {
    新增: {
      icon: "xinzeng",
      bgColor: "#39b54a",
    },
    修复: {
      icon: "bug",
      bgColor: "#9c26b0",
    },
    文档: {
      icon: "wendang",
      bgColor: "rgb(79, 147, 255)",
    },
    更新: {
      icon: "gengxin",
      bgColor: "#0081ff",
    },
    调整: {
      icon: "tiaozheng",
      bgColor: "#333",
    },
    升级: {
      icon: "shengji",
      bgColor: "#e03997",
    },
    移除: {
      icon: "shanchu",
      bgColor: "#666",
    },
    答疑: {
      icon: "dayi",
      bgColor: "#bbb",
    },
    优化: {
      icon: "youhua",
      bgColor: "#38e550",
    },
  };
  return (
    <Label
      title={children}
      style={{ backgroundColor: operates[children].bgColor }}
    >
      <Icon name={operates[children].icon} color="white" size={14} /> {children}
    </Label>
  );
}

const Label = styled.label`
  display: inline-flex;
  align-items: center;
  color: #fff;
  padding: 4px 5px;
  font-size: 12px;
  color: #fff;
  border-radius: 3px;
  line-height: normal;
  margin-left: -3px;
  vertical-align: middle;
  margin-right: 4px;
`;

const Icon = styled(IconFont)`
  margin-right: 4px;
`;

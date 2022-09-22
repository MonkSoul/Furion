import React from "react";
import IconFont from "./iconfonts";
import classes from "./Tag.module.css";

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
    <label
      className={classes.label}
      title={children}
      style={{ backgroundColor: operates[children].bgColor }}
    >
      <IconFont
        name={operates[children].icon}
        color="white"
        size={14}
        className={classes.icon}
      />{" "}
      {children}
    </label>
  );
}

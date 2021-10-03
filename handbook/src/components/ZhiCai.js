import React, { useState } from "react";
import classes from "./ZhiCai.module.css";

export default function ZhiCai({}) {
  const [show, setShow] = useState(true);

  return (
    show ? (
      <div className={classes.container}>
        <div className={classes.gb} onClick={() => setShow(setShow(false))}>
          关闭
        </div>
        <div className={classes.title}>通告声明</div>
        <div className={classes.content}>
          <p>
            自2012年09月投入开源以来，这9年期间一直免费无偿提供优质开源项目，其中但不限于
            Hui，DotDot，Layx，Furion 等项目。
            <br />
            但在2020年09月开源新项目{" "}
            <a href="https://gitee.com/dotnetchina/Furion" target="_blank">
              Furion
            </a>{" "}
            后这一年里受到来自某些无脑 .NET
            开发者来自各大论坛、社区、QQ/微信群等各大平台的辱骂和恶意抹黑，故而发此通告。
            <b>
              本框架及所在{" "}
              <a href="https://gitee.com/dotnetchina" target="_blank">
                dotNET China
              </a>{" "}
              组织将全面通告和制裁以下人员：
            </b>
          </p>
          <p>
            <b>FreeSql 作者及其所有项目：</b>
            <div className={classes.des}>
              一个故意抹黑竞争对手、公开辱骂他人并大肆博同情关注的无良开源作者。一个把国外开源项目{" "}
              <a href="https://github.com/ctstone/csredis" target="_blank">
                csredis
              </a>{" "}
              贡献记录抹掉并占为己有的失德开发者，恶行性质和{" "}
              <a href="https://github.com/dotnetcore/NPOI" target="_blank">
                DotNetCore.NPOI
              </a>{" "}
              一致。
              <br />
            </div>
          </p>
          <p>
            <b>
              无名小卒刘冰心（
              <a href="https://github.com/rwing" target="_blank">
                Rwing
              </a>
              ）：
            </b>
            <div>
              占着博客园十多年博龄的无名小卒，从最开始的开源中国博客无脑恶意评论开始，进而在博客园、知乎、各大.NET群发布各种恶意评论、恶意P图并求转发的跳梁小丑。
            </div>
          </p>
          <p>
            以下（或其余）上不来台面就不过多介绍：
            <div>
              <a href="https://www.cnblogs.com/linhuiy/" target="_blank">
                她微笑的脸
              </a>
              、
              <a href="https://www.cnblogs.com/oldli/" target="_blank">
                NetModular 作者
              </a>
              、
              <a href="https://www.cnblogs.com/wer-ltm/" target="_blank">
                角落的白板报
              </a>
            </div>
          </p>
          <p>
            <b>
              以上列举之人后续将在各大公开平台及dotNET China
              社区编写博文进行一一举证及保留公开辱骂他人证据并起诉。
            </b>
          </p>
        </div>
      </div>
    ):<></>
  );
}

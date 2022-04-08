/**
 * Copyright (c) Facebook, Inc. and its affiliates.
 *
 * This source code is licensed under the MIT license found in the
 * LICENSE file in the root directory of this source tree.
 */
import React from "react";
import clsx from "clsx";
import LastUpdated from "@theme/LastUpdated";
import EditThisPage from "@theme/EditThisPage";
import TagsListInline from "@theme/TagsListInline";
import styles from "./styles.module.css";
import { ThemeClassNames } from "@docusaurus/theme-common";
import GitalkComponent from "gitalk/dist/gitalk-component";
import "gitalk/dist/gitalk.css";
import BrowserOnly from "@docusaurus/BrowserOnly";

function TagsRow(props) {
  return (
    <div
      className={clsx(
        ThemeClassNames.docs.docFooterTagsRow,
        "row margin-bottom--sm"
      )}
    >
      <div className="col">
        <TagsListInline {...props} />
      </div>
    </div>
  );
}

function EditMetaRow({
  editUrl,
  lastUpdatedAt,
  lastUpdatedBy,
  formattedLastUpdatedAt,
}) {
  return (
    <div className={clsx(ThemeClassNames.docs.docFooterEditMetaRow, "row")}>
      <div className="col">{editUrl && <EditThisPage editUrl={editUrl} />}</div>

      <div className={clsx("col", styles.lastUpdated)}>
        {(lastUpdatedAt || lastUpdatedBy) && (
          <LastUpdated
            lastUpdatedAt={lastUpdatedAt}
            formattedLastUpdatedAt={formattedLastUpdatedAt}
            lastUpdatedBy={lastUpdatedBy}
          />
        )}
      </div>
    </div>
  );
}

export default function DocItemFooter(props) {
  const { content: DocContent } = props;
  const { metadata } = DocContent;
  const {
    editUrl,
    lastUpdatedAt,
    formattedLastUpdatedAt,
    lastUpdatedBy,
    tags,
  } = metadata;
  const canDisplayTagsRow = tags.length > 0;
  const canDisplayEditMetaRow = !!(editUrl || lastUpdatedAt || lastUpdatedBy);
  const canDisplayFooter = canDisplayTagsRow || canDisplayEditMetaRow;

  if (!canDisplayFooter) {
    return null;
  }

  return (
    <footer
      className={clsx(ThemeClassNames.docs.docFooter, "docusaurus-mt-lg")}
    >
      {canDisplayTagsRow && <TagsRow tags={tags} />}
      {canDisplayEditMetaRow && (
        <EditMetaRow
          editUrl={editUrl}
          lastUpdatedAt={lastUpdatedAt}
          lastUpdatedBy={lastUpdatedBy}
          formattedLastUpdatedAt={formattedLastUpdatedAt}
        />
      )}

      <BrowserOnly>
        {() => {
          const docId = window.location.pathname
            .split("/")
            .filter((u) => u && u.trim().length > 0)
            .pop();
          const issueId = docIssues[docId];
          return (
            issueId && (
              <>
                <div style={{ textAlign: "center", marginTop: 20 }}>
                  ⭐️ 本章节讨论区：
                  <a
                    href={
                      "https://github.com/MonkSoul/Furion/issues/" + issueId
                    }
                    target="_blank"
                  >
                    https://github.com/MonkSoul/Furion/issues/{issueId}
                  </a>
                </div>
                <GitalkComponent
                  options={{
                    clientID: "5c5f6505b699717b8a86",
                    clientSecret: "164cc4221e5892accacd87f9ad46c8db6be5a460",
                    repo: "Furion",
                    owner: "MonkSoul",
                    admin: ["MonkSoul"],
                    number: issueId,
                    labels: ["Furion"],
                  }}
                />
              </>
            )
          );
        }}
      </BrowserOnly>
    </footer>
  );
}

const docIssues = {
  docs: 40,
  author: 41,
  source: 42,
  case: 43,
  donate: 44,
  upgrade: 45,
  course: 46,
  "get-start": 47,
  "get-start-net6": 48,
  template: 49,
  reference: 50,
  inject: 51,
  "net5-to-net6": 52,
  appstartup: 53,
  configuration: 54,
  options: 55,
  "dynamic-api-controller": 56,
  "specification-document": 57,
  "friendly-exception": 58,
  "data-validation": 59,
  "dbcontext-start": 60,
  dbcontext: 61,
  "dbcontext-locator": 62,
  entity: 63,
  "dbcontext-repository": 64,
  "dbcontext-add": 65,
  "dbcontext-update": 66,
  "dbcontext-add-or-update": 67,
  "dbcontext-delete": 68,
  "dbcontext-batch": 69,
  "dbcontext-query": 70,
  "dbcontext-hight-query": 71,
  "dbcontext-view": 72,
  "dbcontext-proc": 73,
  "dbcontext-function": 74,
  "dbcontext-sql": 75,
  "dbcontext-sql-template": 76,
  "dbcontext-sql-proxy": 77,
  "dbcontext-multi-database": 78,
  "dbcontext-db-first": 79,
  "dbcontext-code-first": 80,
  "dbcontext-seed-data": 81,
  "dbcontext-audit": 82,
  "dbcontext-filter": 83,
  "dbcontext-Interceptor": 84,
  "dbcontext-entitytrigger": 85,
  tran: 86,
  "dbcontext-read-write": 87,
  "split-db": 88,
  "efcore-recommend": 89,
  sqlsugar: 90,
  dapper: 91,
  mongodb: 92,
  saas: 93,
  "dependency-injection": 94,
  "object-mapper": 95,
  cache: 96,
  "auth-control": 97,
  cors: 98,
  "view-engine": 99,
  logging: 100,
  http: 101,
  encryption: 102,
  "local-language": 103,
  "event-bus": 104,
  "json-serialization": 105,
  signalr: 106,
  "process-service": 107,
  job: 108,
  idgenerator: 109,
  "module-dev": 110,
  clayobj: 111,
  "sensitive-detection": 112,
  "file-provider": 113,
  "sesssion-state": 114,
  ipc: 115,
  "deploy-iis": 116,
  "deploy-docker": 117,
  "deploy-nginx": 118,
  "virtual-deploy": 119,
  "deploy-docker-auto": 120,
  devops: 121,
  unittest: 122,
  performance: 123,
  benchmark: 124,
  bingfa: 125,
  "dotnet-tools": 126,
  contribute: 127,
  "sqlsugar-old": 128,
  "event-bus-old": 129,
  app: 130,
  db: 131,
  datavalidator: 132,
  oops: 133,
  linqexpression: 134,
  shttp: 135,
  jsonserializer: 136,
  l: 137,
  messagecenter: 138,
  json: 139,
  scoped: 140,
  sparetime: 141,
  fs: 142,
  jwt: 143,
  appsettings: 144,
  corsaccessorsettings: 145,
  validationTypemessagesettings: 146,
  dependencyinjectionsettings: 147,
  dynamicapicontrollersettings: 148,
  friendlyexceptionsettings: 149,
  specificationdocumentsettings: 150,
  localizationsettings: 151,
  jwtsettings: 152,
  unifyresultsettings: 153,
};

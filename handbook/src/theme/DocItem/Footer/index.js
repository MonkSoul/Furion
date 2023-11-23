import { ThemeClassNames } from "@docusaurus/theme-common";
import { useDoc } from "@docusaurus/theme-common/internal";
import EditThisPage from "@theme/EditThisPage";
import LastUpdated from "@theme/LastUpdated";
import TagsListInline from "@theme/TagsListInline";
import clsx from "clsx";
import React, { useContext } from "react";
import Assistance from "../../../components/Assistance";
import Donate from "../../../components/Donate";
import GlobalContext from "../../../components/GlobalContext";
import SpecDonate from "../../../components/SpecDonate";
import VipImageList from "../../../components/VipImageList";
import styles from "./styles.module.css";

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
  const { adv } = useContext(GlobalContext);

  return (
    <div className={clsx(ThemeClassNames.docs.docFooterEditMetaRow, "row")}>
      <div className="col">
        {editUrl && (
          <>
            {adv && (
              <Assistance style={{ margin: 0, marginBottom: 10, height: 80 }} />
            )}
            <EditThisPage editUrl={editUrl} />
          </>
        )}
      </div>

      <div className={clsx("col", styles.lastUpdated)}>
        {(lastUpdatedAt || lastUpdatedBy) && (
          <>
            {adv && (
              <Donate
                style={{
                  margin: 0,
                  marginBottom: 10,
                  border: "2px solid #ffb02e",
                  marginTop: -4,
                }}
              />
            )}
            <LastUpdated
              lastUpdatedAt={lastUpdatedAt}
              formattedLastUpdatedAt={formattedLastUpdatedAt}
              lastUpdatedBy={lastUpdatedBy}
            />
          </>
        )}
      </div>
    </div>
  );
}
export default function DocItemFooter() {
  const { metadata } = useDoc();
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
  const { adv } = useContext(GlobalContext);

  if (!canDisplayFooter) {
    return null;
  }
  return (
    <footer
      className={clsx(ThemeClassNames.docs.docFooter, "docusaurus-mt-lg")}
    >
      {adv && (
        <>
          <SpecDonate />
        </>
      )}
      {canDisplayTagsRow && <TagsRow tags={tags} />}
      {canDisplayEditMetaRow && (
        <>
          <EditMetaRow
            editUrl={editUrl}
            lastUpdatedAt={lastUpdatedAt}
            lastUpdatedBy={lastUpdatedBy}
            formattedLastUpdatedAt={formattedLastUpdatedAt}
          />

          {adv && (
            <VipImageList />
            // <div
            //   style={{
            //     marginTop: 20,
            //     textAlign: "center",
            //     fontSize: 18,
            //   }}
            // >
            //   ⭐️{" "}
            //   <Link
            //     to={useBaseUrl("docs/subscribe")}
            //     style={{
            //       color: "red",
            //       fontWeight: "bold",
            //       textDecoration: "underline",
            //     }}
            //   >
            //     开通 VIP 服务仅需 499 元/年，尊享 365 天项目无忧
            //   </Link>{" "}
            //   ⭐️
            // </div>
          )}
        </>
      )}
    </footer>
  );
}

import VipDesc from "../components/VipDesc.mdx";
import classes from "./TopBanner.module.css";

export default function TopBanner() {
  return (
    <div className={classes.container}>
      <div className="furion-banner-container">
        <div className={classes.content}>
          <div className={classes.title}>VIP 服务</div>
          <VipDesc />
        </div>
      </div>
    </div>
  );
}

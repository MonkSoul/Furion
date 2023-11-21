import classes from "./TopBanner.module.css";
import VipImageList from "./VipImageList";

export default function TopBanner() {
  return (
    <div className={classes.container}>
      <div className="furion-banner-container">
        <VipImageList padding={20} />
      </div>
    </div>
  );
}

import dayjs from "dayjs";
import "dayjs/locale/zh-cn";
import relativeTime from "dayjs/plugin/relativeTime";
import utc from "dayjs/plugin/utc";
import apiconfig from "./components/jobs/apiconfig";

dayjs.extend(relativeTime);
dayjs.extend(utc);
dayjs.locale("zh-cn");

export function dayTimeUtc(date: dayjs.ConfigType) {
  return dayjs.utc(date);
}

export function dayTime(date: dayjs.ConfigType) {
  if (apiconfig.useUtcTimestamp === "true") {
    return dayTimeUtc(date);
  } else {
    return dayjs(date);
  }
}

export function dayFromNow(date: dayjs.ConfigType) {
  return dayTime(date).fromNow();
}

export function findMaxUtcTimeString(utcTimeStrings: string[]) {
  if (utcTimeStrings.length === 0) {
    throw new Error("数组不能为空");
  }

  let maxDate = null;
  let maxTimeString = "";

  for (let i = 0; i < utcTimeStrings.length; i++) {
    const timeString = utcTimeStrings[i];
    const date = new Date(timeString);

    if (isNaN(date.getTime())) {
      throw new Error(`无效的UTC时间字符串: ${timeString}`);
    }

    if (maxDate === null || date > maxDate) {
      maxDate = date;
      maxTimeString = timeString;
    }
  }

  return maxTimeString;
}

export function findMinUtcTimeString(utcTimeStrings: string[]) {
  if (utcTimeStrings.length === 0) {
    throw new Error("数组不能为空");
  }

  let minDate = new Date(utcTimeStrings[0]);
  let minTimeString = utcTimeStrings[0];

  if (isNaN(minDate.getTime())) {
    throw new Error(`无效的UTC时间字符串: ${utcTimeStrings[0]}`);
  }

  for (let i = 1; i < utcTimeStrings.length; i++) {
    const timeString = utcTimeStrings[i];
    const date = new Date(timeString);

    if (isNaN(date.getTime())) {
      throw new Error(`无效的UTC时间字符串: ${timeString}`);
    }

    if (date < minDate) {
      minDate = date;
      minTimeString = timeString;
    }
  }

  return minTimeString;
}

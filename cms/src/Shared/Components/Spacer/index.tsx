import React from "react";

import "./index.css";

type spacerType = "column" | "row";
type Props = { type: spacerType };

function index({ type }: Props) {
  return <div className={type} />;
}

export default index;

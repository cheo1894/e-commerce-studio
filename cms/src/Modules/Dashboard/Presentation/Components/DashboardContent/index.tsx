import React from "react";

type Props = {
  children?: React.ReactNode;
};

function index({ children: content }: Props) {
  const ds = "dashboard";
  return <div className={ds + "-content"}>{content}</div>;
}

export default index;

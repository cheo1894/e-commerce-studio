import React from "react";
import { Children } from "react";

type Props = {
  children: React.ReactNode;
};

function Index({ children }: Props) {
  return <th className="table-head">{children}</th>;
}

export default Index;

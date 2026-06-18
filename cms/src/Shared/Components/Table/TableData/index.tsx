import React from "react";

type Props = {
  children: React.ReactNode;
};

function Index({ children }: Props) {
  return <td className="table-data">{children}</td>;
}

export default Index;

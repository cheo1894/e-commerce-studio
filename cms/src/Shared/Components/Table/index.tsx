import React from "react";
import "./index.css";
import { Children } from "react";

type Props = {
  children: React.ReactNode;
};

function Index({ children }: Props) {
  return <table className="table-container">{children}</table>;
}

export default Index;

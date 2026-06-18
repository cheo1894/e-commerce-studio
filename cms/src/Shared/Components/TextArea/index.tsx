import React from "react";
import "./index.css";

type Props = {
  Title: string;
};

function Index({ Title }: Props) {
  return (
    <div className="text-area-container">
      {Title && <h5 className="input-title">{Title}</h5>}
      <textarea className="text-area-component" name="" id=""></textarea>
    </div>
  );
}

export default Index;

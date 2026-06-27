import React, { useState } from "react";
import LinkTo from "../LinkTo";
import "./index.css";
import { useNavigate } from "react-router";

type buttonprops = {
  name: string;
  to?: string;
  subroutes?: buttonprops[];
};

type Props = { buttonList: buttonprops[] };

function Index({ buttonList }: Props) {
  const navigate = useNavigate();
  const [visible, setVisible] = useState<number | null>(null);
  return (
    <>
      {buttonList.map((button, index) => {
        if (button.to && button.to !== "") {
          return <LinkTo key={index} to={button.to} title={button.name} />;
        }

        return (
          <>
            <div
              key={index}
              onMouseEnter={() => setVisible(index)}
              onMouseLeave={() => setVisible(null)}
            >
              <LinkTo
                className="principal-tag"
                key={index}
                to="#"
                title={`${button.name}`}
                disabled={true}
              />

              {visible === index && (
                <ul className={`item-list ${visible ? "visible" : ""}`}>
                  {button.subroutes?.map((b, index2) => {
                    if (b.to && b.to !== "") {
                      return (
                        <li className="item-link">
                          <LinkTo
                            key={index2}
                            to="#"
                            title={b.name}
                            onClick={() => {
                              // console.log("navegando");
                              navigate(b.to!);
                            }}
                          />
                        </li>
                      );
                    }
                  })}
                </ul>
              )}
            </div>
            <br />
          </>
        );
      })}
    </>
  );
}

export default Index;

import React from "react";
import "./index.css";

type Props = React.InputHTMLAttributes<HTMLInputElement> & {
  Title?: string;
  PlaceHolder?: string;
  onTextChange?: (value: string) => void;
};

const index = React.forwardRef<HTMLInputElement, Props>(function index(
  { PlaceHolder, Title, type = "text", value, style, onTextChange, ...rest },
  ref,
) {
  return (
    <div className="input-container">
      {Title && <h5 className="input-title">{Title}</h5>}
      <input
        ref={ref}
        value={value}
        type={type}
        style={style}
        placeholder={PlaceHolder}
        className="input-body"
        {...rest}
        onChange={(e) => {
          if (onTextChange) onTextChange(e.target.value);
        }}
      />
    </div>
  );
});

export default index;

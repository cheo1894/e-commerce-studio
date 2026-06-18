import { StrictMode } from "react";
import { createRoot } from "react-dom/client";
import AppRouter from "./app/router/AppRouter";
import "./Styles/index.css";

createRoot(document.getElementById("root")!).render(
  <StrictMode>
    <AppRouter />
  </StrictMode>,
);

import React from "react";
import ReactDOM from "react-dom/client";
import { BrowserRouter, Route, Routes } from "react-router-dom";
import App from "./App";
import apiconfig from "./components/jobs/apiconfig";
import "./index.css";

const root = ReactDOM.createRoot(
  document.getElementById("root") as HTMLElement
);
root.render(
  <React.StrictMode>
    <BrowserRouter basename={apiconfig.requestPath}>
      <Routes>
        <Route index element={<App />} />
      </Routes>
    </BrowserRouter>
  </React.StrictMode>
);

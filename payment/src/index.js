import React from "react";
import ReactDOM from "react-dom/client";
import App from "./App"; // 👈 This is the main component
import "./index.css";
import "bootstrap/dist/css/bootstrap.min.css";
<link
  href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css"
  rel="stylesheet"
/>


const root = ReactDOM.createRoot(document.getElementById("root"));
root.render(<App />);

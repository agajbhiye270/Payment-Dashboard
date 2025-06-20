import React, { useState } from "react";
import PaymentReport from "./PaymentReport";
import CardBalanceReport from "./CardBalanceReport";

export default function ReportsDashboard() {
  const [reportType, setReportType] = useState("payment");

  return (
    <div>
      <div className="btn-group mb-3">
        <button
          className={`btn btn-outline-primary ${reportType === "payment" ? "active" : ""}`}
          onClick={() => setReportType("payment")}
        >
          Payment Report
        </button>
        <button
          className={`btn btn-outline-secondary ${reportType === "balance" ? "active" : ""}`}
          onClick={() => setReportType("balance")}
        >
          Card Balances
        </button>
      </div>

      {/* Report Viewer */}
      {reportType === "payment" && <PaymentReport />}
      {reportType === "balance" && <CardBalanceReport />}
    </div>
  );
}

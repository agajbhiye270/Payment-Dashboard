import React, { useState } from "react";
import LoginForm from "./LoginForm";
import CardValidator from "./CardValidator";
import PaymentForm from "./PaymentForm";
import RefundForm from "./RefundForm";
import ReportsDashboard from "./ReportsDashboard";

const TABS = {
  VALIDATE: "Validate Card",
  PAY: "Make Payment",
  REFUND: "Refund Payment",
  REPORT: "Reports",
};

function App() {
  const [token, setToken] = useState(localStorage.getItem("token"));
  const [activeTab, setActiveTab] = useState(TABS.VALIDATE);

  const handleLogout = () => {
    localStorage.removeItem("token");
    setToken(null);
  };

  const renderContent = () => {
    switch (activeTab) {
      case TABS.VALIDATE:
        return <CardValidator />;
      case TABS.PAY:
        return <PaymentForm />;
      case TABS.REFUND:
        return <RefundForm />;
      case TABS.REPORT:
        return <ReportsDashboard />;
      default:
        return <CardValidator />;
    }
  };

  if (!token) return <LoginForm onLoginSuccess={setToken} />;

  return (
    <div className="container py-4">
      {/* Header & Navigation */}
      <div className="d-flex justify-content-between align-items-center mb-3">
        <h2 className="text-primary">Payment App Dashboard</h2>
        <button onClick={handleLogout} className="btn btn-danger">
          Logout
        </button>
      </div>

      {/* Navigation Tabs */}
      <ul className="nav nav-tabs mb-4">
        {Object.entries(TABS).map(([key, label]) => (
          <li className="nav-item" key={key}>
            <button
              className={`nav-link ${activeTab === label ? "active" : ""}`}
              onClick={() => setActiveTab(label)}
            >
              {label}
            </button>
          </li>
        ))}
      </ul>

      {/* Dynamic Tab Content */}
      {renderContent()}
    </div>
  );
}

export default App;

import React, { useState } from "react";
import api from "./api";

export default function RefundForm() {
  const [transactionId, setTransactionId] = useState("");
  const [message, setMessage] = useState("");

  const handleRefund = async () => {
    try {
      const res = await api.post("/payment/refund", { transactionId });
      setMessage(res.data.message);
    } catch {
      setMessage("❌ Refund failed");
    }
  };

  return (
    <div>
      <h3>Refund Payment</h3>
      <input value={transactionId} onChange={(e) => setTransactionId(e.target.value)} placeholder="Transaction ID" />
      <button onClick={handleRefund}>Refund</button>
      <p>{message}</p>
    </div>
  );
}

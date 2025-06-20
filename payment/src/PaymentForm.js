import React, { useState } from "react";
import api from "./api";

export default function PaymentForm() {
  const [amount, setAmount] = useState("");
  const [cardId, setCardId] = useState("");
  const [message, setMessage] = useState("");

  const handlePayment = async () => {
    try {
      const res = await api.post("/payment/process", { amount, cardId });
      setMessage(`✅ ${res.data.message} | Txn: ${res.data.transactionId}`);
    } catch (err) {
      setMessage("❌ Payment failed");
    }
  };

  return (
    <div>
      <h3>Make Payment</h3>
      <input value={cardId} placeholder="Card ID" onChange={(e) => setCardId(e.target.value)} />
      <input value={amount} placeholder="Amount" onChange={(e) => setAmount(e.target.value)} />
      <button onClick={handlePayment}>Pay</button>
      <p>{message}</p>
    </div>
  );
}

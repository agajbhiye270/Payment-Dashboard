import React, { useEffect, useState } from "react";
import api from "./api";

export default function ReportsDashboard() {
  const [payments, setPayments] = useState([]);
  const [cards, setCards] = useState([]);

  useEffect(() => {
    const fetchReports = async () => {
      try {
        const [pRes, cRes] = await Promise.all([
          api.get("/reports/payments"),
          api.get("/reports/card-balances"),
        ]);
        setPayments(pRes.data.items || []);
        setCards(cRes.data.items || []);
      } catch {
        console.error("Failed to load reports");
      }
    };

    fetchReports();
  }, []);

  return (
    <div>
      <h3>Payments</h3>
      <ul>{payments.map(p => <li key={p.transactionId}>{p.transactionId} - {p.amount}</li>)}</ul>

      <h3>Card Balances</h3>
      <ul>{cards.map(c => <li key={c.cardNumber}>{c.cardNumber} - ${c.balance}</li>)}</ul>
    </div>
  );
}

import React, { useEffect, useState } from "react";
import api from "./api";

export default function CardBalanceReport() {
  const [cards, setCards] = useState([]);

  useEffect(() => {
    api.get("/reports/card-balances")
      .then(res => setCards(res.data))
      .catch(() => setCards([]));
  }, []);

  return (
    <div>
      <h4>Card Balances</h4>
      <table className="table table-bordered">
        <thead>
          <tr>
            <th>Card Number</th>
            <th>Balance</th>
            <th>Status</th>
          </tr>
        </thead>
        <tbody>
          {cards.map((card, index) => (
            <tr key={index}>
              <td>{card.cardNumber}</td>
              <td>{card.balance}</td>
              <td>{card.isValid ? "✅ Valid" : "❌ Invalid"}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}

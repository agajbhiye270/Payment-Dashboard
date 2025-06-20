import React, { useEffect, useState } from "react";
import api from "./api";

export default function PaymentReport() {
  const [payments, setPayments] = useState([]);

  useEffect(() => {
    api.get("/reports/payments")
      .then(res => setPayments(res.data))
      .catch(() => setPayments([]));
  }, []);

  return (
    <div>
      <h4>Payment Reports</h4>
      <table className="table table-bordered">
        <thead>
          <tr>
            <th>Transaction ID</th>
            <th>Amount</th>
            <th>Card ID</th>
            <th>Date</th>
          </tr>
        </thead>
        <tbody>
          {payments.map((p, i) => (
            <tr key={i}>
              <td>{p.transactionId}</td>
              <td>{p.amount}</td>
              <td>{p.cardId}</td>
              <td>{new Date(p.timestamp).toLocaleString()}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}

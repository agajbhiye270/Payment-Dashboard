import React, { useState } from "react";
import api from "./api";

export default function CardValidator() {
  const [cardNumber, setCardNumber] = useState("");
//   const [cvv, setCvv] = useState("");
  const [result, setResult] = useState(null);

  const validateCard = async () => {
    try {
      const response = await api.post("/card/validate", {
         cardNumber
         //,
        // cvv
      });
      setResult(response.data.isValid ? "Valid Card ✅" : "Invalid ❌");
    } catch {
      setResult("Invalid ❌");
    }
  };

  return (
    <div>
      <h3>Validate Card</h3>
      <input placeholder="Card Number" value={cardNumber} onChange={(e) => setCardNumber(e.target.value)} />
      {/* <input placeholder="CVV" value={cvv} onChange={(e) => setCvv(e.target.value)} /> */}
      <button onClick={validateCard}>Validate</button>
      {result && <p>{result}</p>}
    </div>
  );
}

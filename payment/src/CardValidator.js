import React, { useState } from "react";
import api from "./api";

export default function CardValidator() {
  const [cardNumber, setCardNumber] = useState("");
  const [cvv, setCvv] = useState("");
  const [result, setResult] = useState(null);
  const [error, setError] = useState("");

  // Client-side validation while typing
  const handleCardNumberChange = (e) => {
    const value = e.target.value;
    setCardNumber(value);

    if (value.length > 16) {
      setError("Card number cannot be more than 16 digits");
    } else if (value.length < 16) {
      setError("Card number must be 16 digits");
    } else if (!/^\d+$/.test(value)) {
      setError("Card number must contain only digits");
    } else {
      setError("");
    }
  };

  const validateCard = async () => {
    if (error) {
      setResult("❌ Fix errors before validating.");
      return;
    }

    try {
      const response = await api.post("/card/validate", {
        cardNumber,
        // cvv,
      });

      setResult(
        response.data.isValid
          ? "✅ Valid card according to server"
          : "❌ Invalid card according to server"
      );
    } catch {
      setResult("❌ API validation failed");
    }
  };

  return (
    <div>
      <h3>Validate Card</h3>

      <input
        placeholder="Card Number"
        value={cardNumber}
        onChange={handleCardNumberChange}
        maxLength={16}
        style={{ borderColor: error ? "red" : "inherit" }}
      />
      {error && <p style={{ color: "red" }}>{error}</p>}

      {<input
        placeholder="CVV"
        value={cvv}
        onChange={(e) => setCvv(e.target.value)}
        maxLength={3}
      /> }

      <button onClick={validateCard} disabled={!!error || cardNumber.length !== 16}>
        Validate
      </button>

      {result && <p>{result}</p>}
    </div>
  );
}

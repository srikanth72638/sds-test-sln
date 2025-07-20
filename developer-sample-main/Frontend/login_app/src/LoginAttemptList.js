import React, { useState } from "react";
import "./LoginAttemptList.css";

const maskPassword = (password) => "•".repeat(password.length);

const LoginAttempt = ({ login, password }) => (
  <li>
	 username: {login} and masked password {maskPassword(password)}
  </li>
);

const LoginAttemptList = ({ attempts }) => {
  const [filter, setFilter] = useState("");

  const filteredAttempts = attempts.filter(attempt =>
    attempt.login.toLowerCase().includes(filter.toLowerCase())
  );

  return (
    <div className="Attempt-List-Main">
      <p>Recent activity</p>
      <input
        type="text"
        placeholder="Filter by name..."
        value={filter}
        onChange={(e) => setFilter(e.target.value)}
      />
      <ul className="Attempt-List">
        {filteredAttempts.length === 0 ? (
          <li>No data.</li>
        ) : (
          filteredAttempts.map((attempt, index) => (
            <LoginAttempt
              key={index}
              login={attempt.login}
              password={attempt.password}
            />
          ))
        )}
      </ul>
    </div>
  );
};

export default LoginAttemptList;

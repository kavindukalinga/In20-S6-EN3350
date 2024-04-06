import React, { useState } from 'react';

function App() {
  const [accessToken, setAccessToken] = useState('');

  // Function to handle sign in and get access token
  const handleSignIn = () => {
    fetch('http://localhost:9000/auth/signin', {
      method: 'POST',
      headers: {
        "Content-type": "application/json"
      },
      body: JSON.stringify({
        "login": "nuwan1",
        "password": "123"
      })
    })
    .then(response => response.json())
    .then(data => {
      // Set the access token received from the response
      setAccessToken(data.accessToken);
    })
    .catch(error => console.error('Error signing in:', error));
  };

  // Function to fetch current question
  const getCurrentQuestion = () => {
    fetch('http://localhost:9000/get-current-question', {
      method: 'GET',
      headers: {
        "Content-type": "application/json",
        "Authorization": "Bearer " + accessToken
      }
    })
    .then(response => response.json())
    .then(data => {
      console.log('Current question:', data);
    })
    .catch(error => console.error('Error fetching current question:', error));
  };

  return (
    <div>
      <button onClick={handleSignIn}>Sign In</button>
      <button onClick={getCurrentQuestion}>Get Current Question</button>
    </div>
  );
}

export default App;

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
.then(function(response) {
    return response.json();
})
.then(function(data) {
    // Assuming the access token is in data.accessToken
    const accessToken = data.accessToken;

    // Now, use the accessToken to make another request
    fetch('http://localhost:9000/get-current-question', {
        method: 'GET',
        headers: {
            "Content-type": "application/json",
            "Authorization": "Bearer " + accessToken
        }
    })
    .then(function(response) {
        return response.json();
    })
    .then(function(data) {
        console.log('Request succeeded with JSON response', data);
    })
    .catch(function(error) {
        console.log('Request failed', error);
    });
})
.catch(function(error) {
    console.log('Request failed', error);
});

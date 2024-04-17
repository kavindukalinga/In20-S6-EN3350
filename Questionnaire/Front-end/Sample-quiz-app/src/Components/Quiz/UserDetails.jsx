import React, { useState, useEffect } from 'react';
import { useParams } from 'react-router-dom';

function UserDetails() {
    const { username, password } = useParams(); // Retrieve username and password from the URL params
    const [user, setUser] = useState({ username: '', password: '' });
    let accessToken = localStorage.getItem('access_token');

    useEffect(() => {
        // Update the user state with the retrieved username and password
        setUser({ username, password });
    }, [accessToken,username,password]);

    localStorage.setItem('username', username);
    localStorage.setItem('password', password);

    return (
        window.location.href = 'http://localhost:5173'
    );
}

export default UserDetails;

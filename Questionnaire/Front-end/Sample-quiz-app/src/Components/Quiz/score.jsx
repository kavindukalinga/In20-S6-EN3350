import React from 'react';

function Score() {
    return (
        <div className="container">
            <div style={styles.container}>
                <div style={styles.box}>
                    <h1>You have completed the Questionnaire Successfully</h1>
                    <h1 style={styles.title}>Go Back to the Game</h1>
                </div>
            </div>
        </div>
    );
}

const styles = {
    container: {
        display: 'flex',
        flexDirection: 'column',
        alignItems: 'center',
        justifyContent: 'center',
        minHeight: '100vh',
        borderColor: '#555',
        borderWidth: '2px',
        padding: '20px',
        textAlign: 'center',
    },
    box: {
        width: "1200px",
        margin: "auto",
        marginTop: "auto",
        background: "linear-gradient(120deg, #faebb0 0%, #ffc1b1 80%)",
        color: "#262626",
        display: 'flex',
        flexDirection: 'column',
        // gap: '20px',
        borderRadius: '10px',
        padding: '40px 50px',
    },
    title: {
        fontSize: '100px',
        color: '#333',
        marginTop: '20px',
    },
};

export default Score;

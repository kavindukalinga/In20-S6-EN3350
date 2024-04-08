import React, { useEffect, useState } from 'react'
import './Quiz.css'
import { data, summaryData } from '../../assets/data';
import { useNavigate } from 'react-router-dom';

const Quiz = () => {

    const navigate = useNavigate();
    let [index, setIndex] = React.useState(1);
    let [question, setQuestion] = React.useState(data[0]);
    let [lock, setLock] = React.useState(false);
    let [score, setScore] = React.useState(0);
    let [result, setResult] = React.useState(false);
    let [feedback, setFeedback] = React.useState(data[0]);
    let [accessToken, setAccessToken] = React.useState("token");

    const isAuth = async () => {
        try {
            const response = await fetch(`http://127.0.0.1:9000/auth/accessToken`, {
                method: 'GET'
            });
            const response_in_json = await response.json();
            console.log("response_in_json", response_in_json);
            const accessTokenStore = response_in_json?.accessToken;
            setAccessToken(accessTokenStore);
            // localStorage.setItem('accesstoken', accessTokenStore);
        } catch (error) {
            console.error('Error fetching data:', error);
        }
    };

    // if (localStorage.getItem('accesstoken') === null) {
    //     const accessTokenStore = "your_access_token_here"; // Example data
    //     localStorage.setItem('accesstoken', accessTokenStore);
    // }

    // const accessToken = localStorage.getItem('accesstoken');
    // console.log("Hello World");

    const currentQuestion = async () => {
        // isAuth();
        try {
            const response = await fetch(`http://127.0.0.1:9000/get-current-question`, {
                method: 'GET',
                headers: {
                    "Content-type": "application/json",
                    "Authorization": "Bearer " + accessToken
                }
            });
            const response_in_json = await response.json();
            const currentQuestionId = response_in_json['available_question']
            console.log("currentQuestionId", currentQuestionId);
            setIndex(currentQuestionId);
            // console.log("index", index);
            //console.log("data", data[currentQuestionId]);

            // Getting score:
            const response3 = await fetch(`http://127.0.0.1:9000/get-score`, {
                headers: {
                    "Authorization": `Bearer ${accessToken}`
                }
            });
            const response3_in_json = await response3.json();
            console.log("response3_in_json", response3_in_json['score']);
            setScore(response3_in_json['score']);

            if (currentQuestionId <= 10) {
                const response2 = await fetch(`http://127.0.0.1:9000/get-question/${currentQuestionId}`, {
                    headers: {
                        "Authorization": `Bearer ${accessToken}`
                    }
                });
                const response2_in_json = await response2.json();
                // console.log("response2_in_json", response2_in_json["question"]);
                setQuestion(response2_in_json);
            }
            else {
                setResult(true);
            }
            // setQuestion(data[currentQuestionId]);
            // return currentQuestionId;

        } catch (error) {
            console.error('Error fetching data:', error);
        }
    };

    useEffect(() => {
        isAuth();

    }, []);

    useEffect(() => {
        const fetchDataWithDelay = async () => {
            await new Promise(resolve => setTimeout(resolve, 1)); // Adding a delay of 2000 milliseconds (2 seconds)
            currentQuestion();
        };

        fetchDataWithDelay();
    }, [accessToken]);

    let Option1 = React.useRef(null);
    let Option2 = React.useRef(null);
    let Option3 = React.useRef(null);
    let Option4 = React.useRef(null);

    let option_array = [Option1, Option2, Option3, Option4];

    const checkAns = async (e, ans, question_id) => {
        if (!lock) {
            try {
                const response = await fetch(`http://127.0.0.1:9000/get-answer/${question_id}/${ans}`, {
                    headers: {
                        "Authorization": `Bearer ${accessToken}`
                    }
                });
                const data5 = await response.json();
                console.log("data5", data5);
                const fetchedAnswer = data5?.correctAnswer;
                console.log("fetchedAnswer", fetchedAnswer);

                setFeedback(data5);

                // const userAnswers = await fetch(`http://127.0.0.1:9000/api/data`, {
                //     method: 'POST',
                //     headers: {
                //         'Content-Type': 'application/json',
                //         "Authorization": `Bearer ${accessToken}`
                //     },
                //     body: JSON.stringify({ ans, question_id }),
                // });

                if (fetchedAnswer === ans) {
                    e.target.classList.add('correct');
                    setLock(true);
                    setScore(prev => prev + 1);
                } else {
                    e.target.classList.add('wrong');
                    setLock(true);
                    // option_array[fetchedAnswer - 1].current.classList.add('correct');
                }
            } catch (error) {
                console.error('Error fetching data:', error);
            }
        }
    };

    const next = () => {
        if (lock === true) {
            if (index === 11) {
                setResult(true);
                return 0;
            }
            setIndex(++index);
            currentQuestion();
            // setQuestion(data[index]);
            setLock(false);
            option_array.map((option) => {
                option.current.classList.remove('wrong');
                option.current.classList.remove('correct');
                return null;
            })
        }
    }

    const toGame = () => {
        setTimeout(() => {
            window.location.href = `http://127.0.0.1:9000/game?score=${score}`;
        }, 2000);

        return null;
    }


    return (
        <div className='container'>

            {result ? <></> : <>
                <div className='box'>
                    <h1>Questionnaire</h1>
                    <hr />
                    <h2>{index}. {question?.question}</h2>
                    <ul>
                        <li ref={Option1} onClick={(e) => { checkAns(e, 'A', index) }}>{question?.answers?.A}</li>
                        <li ref={Option2} onClick={(e) => { checkAns(e, 'B', index) }}>{question?.answers?.B}</li>
                        <li ref={Option3} onClick={(e) => { checkAns(e, 'C', index) }}>{question?.answers?.C}</li>
                        <li ref={Option4} onClick={(e) => { checkAns(e, 'D', index) }}>{question?.answers?.D}</li>
                    </ul>
                    <button onClick={next} className={lock ? "lock" : ""}>Next</button>
                    <div className="index">{index} of {10} questions</div>
                    <div className="currentscore">{score} of {index} answers are Correct</div>

                    {lock ? <>
                        <hr />
                        <h2>Feedback </h2>
                        <li>{feedback?.generalFeedback}</li>
                        <li>{feedback?.specificFeedback}</li>
                    </> : <></>}
                </div>
            </>}

            {result ? <>
                <div className='lastbox'>
                    <h2>You got {score} answers correct out of {10} questions</h2>
                    <button onClick={toGame}> Go to the Game! </button>
                    <hr />

                    <div className='summary'>
                        <h2>Summary</h2>
                        <ul>
                            {summaryData.map((item, index) => (
                                <li key={index} className="question">
                                    <div className="feedback">
                                        <li className="question-text">Question {item.questionid} : {item.question}</li>
                                        <li className="answer">Your Answer: {item.playerAnswer}</li>
                                        <li className="correct-answer">Correct Answer: {item.correctAnswer}</li>
                                        <li className="correctness">{item.isCorrect ? 'Your answer is correct' : 'Your answer is wrong'}</li>

                                        <li className="specific-feedback">Specific Feedback: {item.specificFeedback}</li>
                                        <li className="general-feedback">General Feedback: {item.generalFeedback}</li>
                                    </div>
                                </li>
                            ))}
                        </ul>

                    </div>
                </div>
            </> : <></>}

        </div>
    )
}

export default Quiz
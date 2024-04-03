import React, { useEffect, useState } from 'react'
import './Quiz.css'
import { data } from '../../assets/data';
import { useNavigate } from 'react-router-dom';

const Quiz = () => {

    const navigate = useNavigate();
    let [index, setIndex] = React.useState(0);
    let [question, setQuestion] = React.useState(data[0]);
    let [lock, setLock] = React.useState(false);
    let [score, setScore] = React.useState(0);
    let [result, setResult] = React.useState(false);



    const currentQuestion = async () => {
        try {
            const response = await fetch(`http://127.0.0.1:5000/get-current-question`);
            const response_in_json = await response.json();
            const currentQuestionId = response_in_json['available_question'];

            // console.log("currentQuestionId", currentQuestionId);
            setIndex(currentQuestionId);
            // console.log("index", index);
            //console.log("data", data[currentQuestionId]);
            if (currentQuestionId < 10) {
                const response2 = await fetch(`http://127.0.0.1:5000/get-question/${currentQuestionId}`);
                const response2_in_json = await response2.json();
                // console.log("response2_in_json", response2_in_json);
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
        currentQuestion();
    }, []);

    let Option1 = React.useRef(null);
    let Option2 = React.useRef(null);
    let Option3 = React.useRef(null);
    let Option4 = React.useRef(null);

    let option_array = [Option1, Option2, Option3, Option4];

    const checkAns = async (e, ans, question_id) => {
        if (!lock) {
            try {
                const response = await fetch(`http://127.0.0.1:5000/get-answer/${question_id}`);
                const data = await response.json();
                const fetchedAnswer = data['answer'];

                const userAnswers = await fetch(`http://127.0.0.1:5000/api/data`, {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json',
                    },
                    body: JSON.stringify({ ans, question_id }),
                });

                if (fetchedAnswer === ans) {
                    e.target.classList.add('correct');
                    setLock(true);
                    setScore(prev => prev + 1);
                } else {
                    e.target.classList.add('wrong');
                    setLock(true);
                    option_array[fetchedAnswer - 1].current.classList.add('correct');
                }
            } catch (error) {
                console.error('Error fetching data:', error);
            }
        }
    };

    // const checkAns = (e, ans, question_id) => {
    //     if (lock === false) {
    //         const fetchData = async () => {
    //             try {
    //                 const response = await fetch(`http://127.0.0.1:5000/get-answer/${question_id}`);
    //                 const data = await response.json();
    //                 return (data['answer'], 10)
    //             } catch (error) {
    //                 console.error('Error fetching data:', error);
    //             }
    //         };

    //         answer = fetchData();

    //         if (answer === ans) {
    //             e.target.classList.add('correct');
    //             setLock(true);
    //             setScore(prev => prev + 1);
    //         }
    //         else {
    //             e.target.classList.add('wrong');
    //             setLock(true);
    //             option_array[question.ans - 1].current.classList.add('correct');
    //         }
    //     }

    // }

    const next = () => {
        if (lock === true) {
            if (index === 10 - 1) {
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
            window.location.href = `http://127.0.0.1:5000/game?score=${score}`;
        }, 2000);

        return null;
    }


    return (
        <div className='container'>
            <h1>Questionnaire</h1>
            <hr />
            {result ? <></> : <>
                <h2>{index + 1}. {question?.question}</h2>
                <ul>
                    <li ref={Option1} onClick={(e) => { checkAns(e, 1, index + 1) }}>{question?.answers?.A}</li>
                    <li ref={Option2} onClick={(e) => { checkAns(e, 2, index + 1) }}>{question?.answers?.B}</li>
                    <li ref={Option3} onClick={(e) => { checkAns(e, 3, index + 1) }}>{question?.answers?.C}</li>
                    <li ref={Option4} onClick={(e) => { checkAns(e, 4, index + 1) }}>{question?.answers?.D}</li>
                </ul>
                <button onClick={next}>Next</button>
                <div className="index">{index + 1} of {10} questions</div>
            </>}
            {result ? <>
                <h2>You got {score} answers correct out of {10} questions</h2>
                <button onClick={toGame}> Go to the Game! </button>
            </> : <></>}

        </div>
    )
}

export default Quiz
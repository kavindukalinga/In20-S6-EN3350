import React, { useEffect } from 'react'
import './Quiz.css'
import { data } from '../../assets/data';
import { useNavigate } from 'react-router-dom';

const Quiz = () => {

    const navigate = useNavigate();
    let [index, setIndex] = React.useState(0);
    let [question, setQuestion] = React.useState(data[index]);
    let [lock, setLock] = React.useState(false);
    let [score, setScore] = React.useState(0);
    let [result, setResult] = React.useState(false);

    // useEffect(() => {
    //     console.log("Quiz22");

    //     setQuestion(data[index]);
    // }, []);

    let Option1 = React.useRef(null);
    let Option2 = React.useRef(null);
    let Option3 = React.useRef(null);
    let Option4 = React.useRef(null);

    let option_array = [Option1, Option2, Option3, Option4];

    const checkAns = (e, ans) => {
        if (lock === false) {
            if (question.ans === ans) {
                e.target.classList.add('correct');
                setLock(true);
                setScore(prev => prev + 1);
            }
            else {
                e.target.classList.add('wrong');
                setLock(true);
                option_array[question.ans - 1].current.classList.add('correct');
            }
        }

    }

    const next = () => {
        if (lock === true) {
            if (index === data.length - 1) {
                setResult(true);
                return 0;
            }
            setIndex(++index);
            setQuestion(data[index]);
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
                <h2>{index + 1}. {question.question}</h2>
                <ul>
                    <li ref={Option1} onClick={(e) => { checkAns(e, 1) }}>{question.option1}</li>
                    <li ref={Option2} onClick={(e) => { checkAns(e, 2) }}>{question.option2}</li>
                    <li ref={Option3} onClick={(e) => { checkAns(e, 3) }}>{question.option3}</li>
                    <li ref={Option4} onClick={(e) => { checkAns(e, 4) }}>{question.option4}</li>
                </ul>
                <button onClick={next}>Next</button>
                <div className="index">{index + 1} of {data.length} questions</div>
            </>}
            {result ? <>
                <h2>You got {score} answers correct out of {data.length} questions</h2>
                <button onClick={toGame}> Go to the Game! </button>
            </> : <></>}

        </div>
    )
}

export default Quiz
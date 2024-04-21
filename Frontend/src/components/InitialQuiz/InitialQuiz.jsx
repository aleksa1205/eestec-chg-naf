import { Link, useLoaderData } from 'react-router-dom';
import classes from './InitialQuiz.module.css';
import { useState } from 'react';
import { setLevel, getLevel } from '../../assets/userData';

function InitialQuiz() {
    const [currIndex, setCurrIndex] = useState(0);
    const [answerEntered, setAnswerEntered] = useState('');
    const [finish, setFinish] = useState(false);
    const pitanja = useLoaderData();
    let engleski, native;
    const threshold = 80;
    let engLevel = 1;

    native = pitanja.map(el => {
        let [engleski, native] = el.split('|');
        return native;
    });

    engleski = pitanja.map(el => {
        let [engleski, native] = el.split('|');
        return engleski;
    });

    // console.log(pitanjaPrikaz);
    
    function submitHandler(event) {
        setCurrIndex((values) => values + 1);

        async function sendToGrade() {
            const orgMess = engleski[currIndex];
            const userMess = answerEntered;
            const request = `https://localhost:7050/OpenAi/Grade/${orgMess}/${userMess}`;
            console.log(request);
            const response = await fetch(request);
            const data = await response.text();
            return data;   
        }

        sendToGrade().then(response => {
            if(response > threshold) {
                // console.log("level up!");
                engLevel = getLevel();
                if(engLevel == 1)
                {
                    engLevel = 3;
                    console.log("level up to 3");
                }
                else if(engLevel == 3) 
                {
                    engLevel = 4;
                    console.log("level up to 4");
                }
                else if(engLevel == 4)
                {
                    engLevel = 5;
                    console.log("level up to 5");
                    setFinish(true);
                }
                setLevel(engLevel);
                console.log(getLevel());
            }
            else {
                console.log(getLevel());
                setFinish(true);
            }
        });
    }

    function answerEnteredHandler(event) {
        setAnswerEntered(event.target.value);
    }

    return (
        <form className={classes.form} action="">
            <p>Please Translate this sentence to english</p>
            <p>{native[currIndex]}</p>
            <input size={50} onChange={answerEnteredHandler} type="text" />
            <br />
            <br />
            {
                finish ? 
                <Link to='/' className='button'>Finish Test</Link> :
                <button type='button' onClick={submitHandler} className='button'>Submit</button>
            }
        </form>
    );
}

export default InitialQuiz;

export async function loader() {
    const response = await fetch("https://localhost:7050/OpenAi/GenerateInitialTest");
    const data = await response.json();
    return data;
}
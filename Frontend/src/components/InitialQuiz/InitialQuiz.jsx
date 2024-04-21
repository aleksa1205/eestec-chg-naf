import { useLoaderData } from 'react-router-dom';
import classes from './InitialQuiz.module.css';

function InitialQuiz() {
    const pitanja = useLoaderData();
    console.log(pitanja);

    return (
        <form className={classes.form} action="">
            <p>Please Translate this sentence to english</p>
            <p>Nigga</p>
            <input type="text" />
            <br />
            <br />
            <button type='button' className='button'>Submit</button>
        </form>
    );
}

export default InitialQuiz;

export async function loader() {
    const response = await fetch("https://localhost:7050/OpenAi/Generate");
    const data = await response.text();
    return data;
}
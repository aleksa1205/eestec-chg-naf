import classes from './InitialQuiz.module.css';

function InitialQuiz() {

    async function fetchData() {
        const response = await fetch("https://localhost:7050/OpenAi/Generate");
        console.log(response);
        const data = await response.text();
        console.log(data);
        return data;

    }

    return (
        <form className={classes.form} action="">
            <p>Please Translate this sentence to english</p>
            <p>Nigga</p>
            <input type="text" />
            <br />
            <br />
            <button type='button' onClick={fetchData} className='button'>Submit</button>
        </form>
    );
}

export default InitialQuiz;
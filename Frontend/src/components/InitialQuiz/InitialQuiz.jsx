import classes from './InitialQuiz.module.css';

function InitialQuiz() {
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
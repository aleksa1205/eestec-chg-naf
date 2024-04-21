import classes from './PopQuiz.module.css';

function PopQuiz() {
    return (
        <form className={classes.form} action="">
            <p>Please Translate this sentence to english</p>
            <p></p>
            <input type="text" />
            <br />
            <br />
            <button type='button' onClick={fetchData} className='button'>Submit</button>
        </form>
    );
}

export default PopQuiz;
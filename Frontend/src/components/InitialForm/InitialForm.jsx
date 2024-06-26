import languageData from '../../assets/languageData.json';
import classes from './InitialForm.module.css';
import { Link } from 'react-router-dom';

function InitialForm() {
    const arrayData = [];
    for(const key in languageData) {
        arrayData.push({code: key, language: languageData[key]})
    }
    
    return (
        <form className={classes.form}>
            <p>Please choose a language you want to learn</p>
            <select defaultValue={languageData.en} name="language" id="language">
                {arrayData.map(el => {
                    return el.code != 'en' 
                        ?
                        <option disabled value={el.code} key={el.code}>{el.language.name}</option>
                        :
                        <option value={el.code} key={el.code}>{el.language.name}</option>
                    })}
            </select>
            <br />
            <br />
            <Link className='button' to='/initial-quiz'>Submit</Link>
        </form>
    );
}

export default InitialForm;
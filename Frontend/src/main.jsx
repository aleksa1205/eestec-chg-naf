import React from 'react'
import ReactDOM from 'react-dom/client'
import App from './App.jsx'
import { RouterProvider, createBrowserRouter } from 'react-router-dom'
import InitialQuiz, { loader as initialQuizLoader} from './components/InitialQuiz/InitialQuiz.jsx'
import './index.css'
import PopQuiz from './components/PopQuiz/PopQuiz.jsx'

const router = createBrowserRouter([
  { path: '/', element: <App /> },
  { path: '/initial-quiz', element: <InitialQuiz />, loader: initialQuizLoader},
  { path: '/pop-quiz', element: <PopQuiz /> }
]);

ReactDOM.createRoot(document.getElementById('root')).render(
  <React.StrictMode>
    {/* <App /> */}
    <RouterProvider router={router} />
  </React.StrictMode>,
)

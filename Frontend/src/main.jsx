import React from 'react'
import ReactDOM from 'react-dom/client'
import App from './App.jsx'
import { RouterProvider, createBrowserRouter } from 'react-router-dom'
import InitialQuiz from './components/InitialQuiz/InitialQuiz.jsx'
import PopQuiz from './components/PopQuiz/PopQuiz.jsx'
import './index.css'

const router = createBrowserRouter([
  { path: '/', element: <App /> },
  { path: '/initial-quiz', element: <InitialQuiz />},
  { path: '/pop-quiz', element: <PopQuiz />}
]);

ReactDOM.createRoot(document.getElementById('root')).render(
  <React.StrictMode>
    {/* <App /> */}
    <RouterProvider router={router} />
  </React.StrictMode>,
)

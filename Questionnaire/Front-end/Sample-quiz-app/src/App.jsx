import React from 'react'
import Quiz from './Components/Quiz/Quiz'
import UserDetails from './Components/Quiz/UserDetails'
import Score from './Components/Quiz/score'
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';


const App = () => {
  return (
    <Router>
      <div>
        <Routes>
          <Route exact path="/" element={<Quiz />}></Route>
          <Route path="/user/:username/:password" element={<UserDetails />}></Route>
          <Route path="/gotogame" element={<Score />}></Route>
        </Routes>
      </div>
    </Router>
  )
}

export default App
import { Routes, Route, NavLink } from 'react-router-dom'
import List from './List'
import Denmark from './Denmark'
import Single from './Single'

export function App() {
  return (
    <>
      <nav className="navbar navbar-expand-lg navbar-dark bg-dark">
        <div className="container">
          <NavLink className="navbar-brand" to="/">Csatahajók</NavLink>
          <div className="navbar-nav">
            <NavLink className="nav-link" to="/">Csatahajók</NavLink>
            <NavLink className="nav-link" to="/denmark">A Denmark Strait csata</NavLink>
          </div>
        </div>
      </nav>

      <div className="container mt-4">
        <Routes>
          <Route path="/" element={<List />} />
          <Route path="/single/:shipName" element={<Single />} />
          <Route path="/denmark" element={<Denmark />} />
        </Routes>
      </div>
    </>
  )
}
import { useEffect, useState } from 'react'
import { NavLink } from 'react-router-dom'
import axios from 'axios'

export default function List() {
  const [csatahajok, setCsatahajok] = useState([])

  useEffect(() => {
    axios.get('https://localhost:7074/api/Hajo/All')
      .then(res => setCsatahajok(res.data))
      .catch(err => console.log(err))
  }, [])

  return (
    <div>
      <h2>Csatahajók</h2>
      <div className="row row-cols-1 row-cols-md-4 g-3">
        {csatahajok.map((hajo, index) => (
          <div className="col" key={index}>
            <div className="card h-100">
              <div className="card-body">
                <NavLink to={`/single/${hajo.nev}`}>
                  <h5 className="card-title">{hajo.nev}</h5>
                </NavLink>
              </div>
            </div>
          </div>
        ))}
      </div>
    </div>
  )
}
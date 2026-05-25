import { useEffect, useState } from 'react'
import { useNavigate } from 'react-router-dom'
import axios from 'axios'

export default function Denmark() {
  const [csatahajok, setCsatahajok] = useState([])
  const navigate = useNavigate()

  useEffect(() => {
    axios.get('https://localhost:7074/api/Csata/Resztvevok/Denmark Strait')
      .then(res => setCsatahajok(res.data))
      .catch(err => console.log(err))
  }, [])

  const torles = (hajonev) => {
    if (window.confirm('Biztosan szeretnéd törölni?')) {
      axios.delete(`https://localhost:7074/api/Kimenet/KimenetTorles/Denmark Strait/${hajonev}`)
        .then(() => {
          alert('Sikeres törlés!')
          navigate('/')
        })
        .catch(err => console.log(err))
    }
  }

  return (
    <div>
      <h2>Denmark Strait csata</h2>
      <div className="row row-cols-1 row-cols-md-4 g-3">
        {csatahajok.map((hajo, index) => (
          <div className="col" key={index}>
            <div className="card h-100">
              <div className="card-body d-flex justify-content-between align-items-center">
                <span>{hajo}</span>
                <i
                  className="bi bi-trash text-danger"
                  style={{ cursor: 'pointer' }}
                  onClick={() => torles(hajo)}
                ></i>
              </div>
            </div>
          </div>
        ))}
      </div>
    </div>
  )
}
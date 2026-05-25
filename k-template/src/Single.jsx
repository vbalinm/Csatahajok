import { useEffect, useState } from 'react'
import { useParams, NavLink } from 'react-router-dom'
import axios from 'axios'

export default function Single() {
  const { shipName } = useParams()
  const [csatahajo, setCsatahajo] = useState(null)

  useEffect(() => {
    axios.get(`https://localhost:7074/api/Hajo/ByName/${shipName}`)
      .then(res => setCsatahajo(res.data))
      .catch(err => console.log(err))
  }, [shipName])

  if (!csatahajo) return <div className="spinner-border"></div>

  return (
    <div>
      <div className="card">
        <div className="card-body">
          <h3>{csatahajo.nev}</h3>
          <p>Osztály: {csatahajo.osztaly}</p>
          <p>Felavatva: {csatahajo.felavatva}</p>
          <p>Ágyúk száma: {csatahajo.agyukSzama}</p>
          <p>Kaliber: {csatahajo.kaliber}</p>
          <p>Vízkiszorítás: {csatahajo.vizkiszoritas}</p>
        </div>
      </div>
      <NavLink to="/" className="btn btn-secondary mt-3">
        <i className="bi bi-arrow-left"></i> Vissza a csatahajókhoz
      </NavLink>
    </div>
  )
}
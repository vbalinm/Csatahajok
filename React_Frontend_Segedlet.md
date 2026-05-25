# React Frontend Segédlet – Szakmai Vizsga

## Tartalomjegyzék
1. [Projekt telepítése](#1-projekt-telepítése)
2. [Csomagok telepítése](#2-csomagok-telepítése)
3. [Fájlstruktúra](#3-fájlstruktúra)
4. [main.jsx – Belépési pont](#4-mainjsx--belépési-pont)
5. [App.jsx – Navbar és Routes](#5-appjsx--navbar-és-routes)
6. [List.jsx – Összes adat listázása](#6-listjsx--összes-adat-listázása)
7. [Single.jsx – Egy elem részletei](#7-singlejsx--egy-elem-részletei)
8. [Delete komponens – Törlés funkcióval](#8-delete-komponens--törlés-funkcióval)
9. [API hívások – Axios](#9-api-hívások--axios)
10. [React Router – Navigáció](#10-react-router--navigáció)
11. [Bootstrap – UI elemek](#11-bootstrap--ui-elemek)
12. [Tipikus hibák és megoldások](#12-tipikus-hibák-és-megoldások)
13. [Leadás előtt](#13-leadás-előtt)

---

## 1. Projekt telepítése

```bash
# Tanár templatejéből (git clone)
git clone https://github.com/<tanar-github>/kando-template projektnev
cd projektnev

# .git mappa törlése (Windows)
rd /s /q .git

# Függőségek telepítése
bun install

# Fejlesztői szerver indítása
bun run dev
```

Böngészőben: `http://localhost:5173/`

---

## 2. Csomagok telepítése

```bash
bun add bootstrap bootstrap-icons axios react-router-dom
```

| Csomag | Mire kell |
|--------|-----------|
| `bootstrap` | UI megjelenítés (kártyák, gombok, navbar) |
| `bootstrap-icons` | Ikonok (kuka, nyíl, stb.) |
| `axios` | API hívások (GET, POST, DELETE) |
| `react-router-dom` | Oldalak közötti navigáció |

---

## 3. Fájlstruktúra

```
src/
├── App.jsx          ← Navbar + Routes (útvonalak)
├── main.jsx         ← Belépési pont, Bootstrap import
├── List.jsx         ← Főoldal – összes elem listázása
├── Single.jsx       ← Egy elem részletes adatai
└── Denmark.jsx      ← Speciális oldal törlés funkcióval
```

> ⚠️ A fájlok neve **pontosan** meg kell egyezzen az importokban használt névvel!

---

## 4. main.jsx – Belépési pont

```jsx
import { App } from './App.jsx'
import 'bootstrap/dist/css/bootstrap.min.css'
import 'bootstrap/dist/js/bootstrap.min.js'
import 'bootstrap-icons/font/bootstrap-icons.css'
import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import { BrowserRouter } from 'react-router-dom'

createRoot(document.getElementById('root')).render(
  <StrictMode>
    <BrowserRouter>
      <App />
    </BrowserRouter>
  </StrictMode>
)
```

> ⚠️ A `BrowserRouter` itt kell legyen, nem az `App.jsx`-ben!
> ⚠️ Az `App` importja `{ App }` – kapcsos zárójelben, mert **named export**!

---

## 5. App.jsx – Navbar és Routes

```jsx
import { Routes, Route, NavLink } from 'react-router-dom'
import List from './List'
import Single from './Single'
import Denmark from './Denmark'

export function App() {
  return (
    <>
      {/* Navbar */}
      <nav className="navbar navbar-expand-lg navbar-dark bg-dark">
        <div className="container">
          <NavLink className="navbar-brand" to="/">Alkalmazás neve</NavLink>
          <div className="navbar-nav">
            <NavLink className="nav-link" to="/">1. menüpont</NavLink>
            <NavLink className="nav-link" to="/masodik">2. menüpont</NavLink>
          </div>
        </div>
      </nav>

      {/* Oldalak */}
      <div className="container mt-4">
        <Routes>
          <Route path="/" element={<List />} />
          <Route path="/single/:nev" element={<Single />} />
          <Route path="/masodik" element={<Denmark />} />
        </Routes>
      </div>
    </>
  )
}
```

> ⚠️ Az `App` **named export** (`export function App`), nem default!
> ⚠️ A `:nev` a dinamikus URL paraméter neve – ezt kell használni a `useParams()`-ban!

---

## 6. List.jsx – Összes adat listázása

```jsx
import { useEffect, useState } from 'react'
import { NavLink } from 'react-router-dom'
import axios from 'axios'

export default function List() {
  const [adatok, setAdatok] = useState([])

  // Adatok lekérése az API-ból
  useEffect(() => {
    axios.get('https://localhost:XXXX/api/Elem/All')
      .then(res => setAdatok(res.data))
      .catch(err => console.log(err))
  }, []) // ← üres tömb = csak egyszer fut le (betöltéskor)

  return (
    <div>
      <h2>Elemek listája</h2>
      <div className="row row-cols-1 row-cols-md-4 g-3">
        {adatok.map((elem, index) => (
          <div className="col" key={index}>
            <div className="card h-100">
              <div className="card-body">
                {/* Kártyára kattintva navigál a Single oldalra */}
                <NavLink to={`/single/${elem.nev}`}>
                  <h5 className="card-title">{elem.nev}</h5>
                </NavLink>
              </div>
            </div>
          </div>
        ))}
      </div>
    </div>
  )
}
```

> ⚠️ A `key` prop kötelező a `.map()`-ban – használj `index`-et ha nincs ID!
> ⚠️ A mezőneveket a **Swagger** alapján kell kitölteni (pl. `elem.nev`, `elem.ar`, stb.)!

---

## 7. Single.jsx – Egy elem részletei

```jsx
import { useEffect, useState } from 'react'
import { useParams, NavLink } from 'react-router-dom'
import axios from 'axios'

export default function Single() {
  const { nev } = useParams() // ← az App.jsx-ben megadott paraméter neve!
  const [elem, setElem] = useState(null)

  useEffect(() => {
    axios.get(`https://localhost:XXXX/api/Elem/ByName/${nev}`)
      .then(res => setElem(res.data))
      .catch(err => console.log(err))
  }, [nev])

  // Betöltés jelző amíg az adat meg nem jön
  if (!elem) return <div className="spinner-border"></div>

  return (
    <div>
      <div className="card">
        <div className="card-body">
          <h3>{elem.nev}</h3>
          <p>Mező 1: {elem.mezo1}</p>
          <p>Mező 2: {elem.mezo2}</p>
          {/* ... további mezők a Swagger alapján */}
        </div>
      </div>
      {/* Vissza gomb */}
      <NavLink to="/" className="btn btn-secondary mt-3">
        <i className="bi bi-arrow-left"></i> Vissza a főoldalra
      </NavLink>
    </div>
  )
}
```

> ⚠️ A `useParams()`-ban lévő név **pontosan** meg kell egyezzen az `App.jsx`-beli `:nev` paraméterrel!

---

## 8. Delete komponens – Törlés funkcióval

```jsx
import { useEffect, useState } from 'react'
import { useNavigate } from 'react-router-dom'
import axios from 'axios'

export default function DeleteOldal() {
  const [adatok, setAdatok] = useState([])
  const navigate = useNavigate() // ← navigáláshoz kell

  useEffect(() => {
    axios.get('https://localhost:XXXX/api/Csata/Resztvevok/NevParaméter')
      .then(res => setAdatok(res.data))
      .catch(err => console.log(err))
  }, [])

  const torles = (elemNev) => {
    // Confirm ablak törlés előtt
    if (window.confirm('Biztosan szeretnéd törölni?')) {
      axios.delete(`https://localhost:XXXX/api/Elem/Torles/NevParaméter/${elemNev}`)
        .then(() => {
          alert('Sikeres törlés!')
          navigate('/') // ← visszanavigál a főoldalra
        })
        .catch(err => console.log(err)) // ← hiba esetén konzolba ír
    }
  }

  return (
    <div>
      <h2>Oldal neve</h2>
      <div className="row row-cols-1 row-cols-md-4 g-3">
        {adatok.map((elem, index) => (
          <div className="col" key={index}>
            <div className="card h-100">
              <div className="card-body d-flex justify-content-between align-items-center">
                <span>{elem}</span> {/* vagy elem.nev ha objektum */}
                {/* Kuka ikon */}
                <i
                  className="bi bi-trash text-danger"
                  style={{ cursor: 'pointer' }}
                  onClick={() => torles(elem)}
                ></i>
              </div>
            </div>
          </div>
        ))}
      </div>
    </div>
  )
}
```

> ⚠️ Ha az API **stringek tömbjét** adja vissza (pl. `["Bismarck", "Hood"]`), akkor `{elem}` kell!
> ⚠️ Ha az API **objektumok tömbjét** adja vissza, akkor `{elem.nev}` kell!

---

## 9. API hívások – Axios

### GET – Adatok lekérése
```jsx
axios.get('https://localhost:XXXX/api/Elem/All')
  .then(res => setAdatok(res.data))
  .catch(err => console.log(err))
```

### GET – Egy elem lekérése paraméterrel
```jsx
axios.get(`https://localhost:XXXX/api/Elem/ByName/${nev}`)
  .then(res => setElem(res.data))
  .catch(err => console.log(err))
```

### DELETE – Elem törlése
```jsx
axios.delete(`https://localhost:XXXX/api/Elem/Torles/${csataNev}/${elemNev}`)
  .then(() => {
    alert('Sikeres törlés!')
    navigate('/')
  })
  .catch(err => console.log(err))
```

### POST – Új elem létrehozása
```jsx
axios.post('https://localhost:XXXX/api/Elem/Letrehoz', {
  nev: 'Példa',
  ertek: 42
})
  .then(res => console.log(res.data))
  .catch(err => console.log(err))
```

> ⚠️ A portszámot a **Swagger URL**-jéből kell kiolvasni! (pl. `7074` vagy `2014`)
> ⚠️ Ha `https` a Swagger, akkor `https` kell az axios hívásban is!

---

## 10. React Router – Navigáció

### Útvonalak definiálása (App.jsx)
```jsx
<Routes>
  <Route path="/" element={<List />} />
  <Route path="/single/:nev" element={<Single />} />  {/* dinamikus */}
  <Route path="/masodik" element={<MasikOldal />} />
</Routes>
```

### NavLink – Navbar menüpontokhoz
```jsx
<NavLink className="nav-link" to="/">Főoldal</NavLink>
<NavLink className="nav-link" to="/masodik">Másik oldal</NavLink>
```

### useNavigate – Kódból navigáláshoz
```jsx
const navigate = useNavigate()
navigate('/')           // főoldalra
navigate('/masodik')    // másik oldalra
navigate(-1)            // vissza (mint a böngésző vissza gomb)
```

### useParams – URL paraméter olvasása
```jsx
// Ha az útvonal: /single/:nev
const { nev } = useParams()
// Ha az URL: /single/Bismarck → nev = "Bismarck"
```

---

## 11. Bootstrap – UI elemek

### Kártyák rácsban (4 oszlop)
```jsx
<div className="row row-cols-1 row-cols-md-4 g-3">
  <div className="col">
    <div className="card h-100">
      <div className="card-body">
        <h5 className="card-title">Cím</h5>
        <p className="card-text">Szöveg</p>
      </div>
    </div>
  </div>
</div>
```

### Navbar
```jsx
<nav className="navbar navbar-expand-lg navbar-dark bg-dark">
  <div className="container">
    <NavLink className="navbar-brand" to="/">Brand</NavLink>
    <div className="navbar-nav">
      <NavLink className="nav-link" to="/">Menüpont</NavLink>
    </div>
  </div>
</nav>
```

### Gombok
```jsx
<button className="btn btn-primary">Elsődleges</button>
<button className="btn btn-danger">Veszélyes</button>
<button className="btn btn-secondary">Másodlagos</button>
```

### Bootstrap ikonok
```jsx
<i className="bi bi-trash text-danger"></i>      {/* kuka */}
<i className="bi bi-arrow-left"></i>             {/* vissza nyíl */}
<i className="bi bi-plus-circle"></i>            {/* hozzáadás */}
<i className="bi bi-pencil"></i>                 {/* szerkesztés */}
<i className="bi bi-check-circle text-success"></i> {/* pipa */}
```

### Betöltés jelző
```jsx
<div className="spinner-border"></div>
```

---

## 12. Tipikus hibák és megoldások

| Hiba | Ok | Megoldás |
|------|----|----------|
| `Cannot read properties of undefined` | API mezőnév elírva | Swagger-ben ellenőrizd a pontos mezőneveket |
| Kártyák nem jelennek meg | API nem ad vissza adatot | F12 → Network fülön ellenőrizd az API választ |
| `useNavigate is not defined` | Hiányzó import | `import { useNavigate } from 'react-router-dom'` |
| `navigate is not defined` | `const navigate = useNavigate()` hiányzik | Add hozzá a komponens elejéhez |
| Bootstrap ikonok nem jelennek meg | Hiányzó import | `import 'bootstrap-icons/font/bootstrap-icons.css'` a `main.jsx`-be |
| Router nem működik | `BrowserRouter` hiányzik | `main.jsx`-ben köré kell csomagolni az `<App />`-ot |
| CORS hiba | Backend nem engedi | Backend `Program.cs`-ben legyen CORS beállítva |
| `map is not a function` | API nem tömböt ad vissza | `console.log(res.data)` – nézd meg mi jön vissza |
| Oldal nem frissül törlés után | `navigate` helyett kellene reload | `window.location.reload()` vagy újra lekérdezés |
| `No routes matched location` | Hiányzó Route | App.jsx-ben add hozzá a hiányzó `<Route>`-ot |

---

## 13. Leadás előtt

```bash
# 1. node_modules törlése (Windows)
rd /s /q node_modules

# 2. Ellenőrzés – csak ezek maradjanak:
# src/, public/, package.json, vite.config.js, index.html, bun.lock

# 3. Becsomagolás
# Fájlnév: Vezeteknev_Keresztnev_frontend.zip
```

> ⚠️ A `node_modules` mappát **soha ne csomagold be** – több száz MB lehet!
> ⚠️ A `.git` mappát bent hagyhatod!

---

## Gyors emlékeztető – Feladat checklist

- [ ] React projekt létrehozva és hibamentesen fut
- [ ] Bootstrap + bootstrap-icons + axios + react-router-dom telepítve
- [ ] `main.jsx`-ben Bootstrap importok és `BrowserRouter`
- [ ] `App.jsx`-ban Navbar + Routes
- [ ] `List.jsx` – összes elem kártyákon, kattintásra navigál
- [ ] `Single.jsx` – egy elem részletei + vissza gomb
- [ ] Speciális oldal (pl. Denmark) – kuka ikonnal törlés
- [ ] Confirm ablak törlés előtt
- [ ] Alert törlés után
- [ ] Hiba esetén `console.log(err)`
- [ ] `node_modules` törölve leadás előtt

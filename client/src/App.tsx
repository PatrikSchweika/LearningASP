import {useEffect, useState} from 'react'
import reactLogo from './assets/react.svg'
import viteLogo from '/vite.svg'
import './App.css'
import {User} from "./models/user.ts";
import {styled} from "@mui/material";
import {APP_CONFIG} from "./setup/app-config.ts";

function App() {
  const [count, setCount] = useState(0)

  const [apiMessage, setApiMessage] = useState('')
  const [users, setUsers] = useState<User[]>([])  
  
  useEffect(() => {
      fetch(APP_CONFIG.apiUrl)
          .then(res => res.text())
          .then(text => setApiMessage(text))
      
      fetch(`${APP_CONFIG.apiUrl}/user`)
          .then(res => res.json())
          .then(users => setUsers(users))
  }, [])  
    
  console.log(users)
    
  return (
    <>
      <div>
        <a href="https://vitejs.dev" target="_blank">
          <img src={viteLogo} className="logo" alt="Vite logo" />
        </a>
        <a href="https://react.dev" target="_blank">
          <img src={reactLogo} className="logo react" alt="React logo" />
        </a>
      </div>
      <h1>Vite + React</h1>
      <div className="card">
        <button onClick={() => setCount((count) => count + 1)}>
          count is {count}
        </button>
        <p>
          Message from API: {apiMessage}
        </p>
      </div>
      <h2>Users</h2>
        <UserList>
            {users.map(user => (
                <li>({user.id}) {user.firstName} {user.lastName}</li>
            ))}
        </UserList>    
    </>
  )
}

const UserList = styled('ul')`
    text-align: left;
`

export default App

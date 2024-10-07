import viteLogo from '/vite.svg'
import reactLogo from '../assets/react.svg'
import { UserList } from './UserList.tsx'
import { useEffect, useState } from 'react'
import { User } from '../models/user.ts'
import { APP_CONFIG } from '../setup/app-config.ts'

export const HomePage = () => {
  const [apiMessage, setApiMessage] = useState('')
  const [users, setUsers] = useState<User[]>([])

  useEffect(() => {
    fetch(APP_CONFIG.apiUrl)
      .then((res) => res.text())
      .then((text) => setApiMessage(text))

    fetch(`${APP_CONFIG.apiUrl}/user`)
      .then((res) => res.json())
      .then((users) => setUsers(users))
  }, [])

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
        <p>API url: {APP_CONFIG.apiUrl}</p>
        <p>Message from API: {apiMessage}</p>
      </div>
      <h2>Users</h2>
      <UserList users={users} />
    </>
  )
}

import { UserList } from './UserList.tsx'
import { useEffect, useState } from 'react'
import { User } from '../models/user.ts'
import { APP_CONFIG } from '../setup/app-config.ts'
import { Paper, Stack, Typography } from '@mui/material'

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
      <Stack spacing={4} sx={{ textAlign: 'center', alignItems: 'center' }}>
        <Typography variant="h1">LearningASP</Typography>
        <Paper sx={{ maxWidth: 800 }}>
          <Stack spacing={4} sx={{ padding: 5 }}>
            <Typography>API url: {APP_CONFIG.apiUrl}</Typography>
            <Typography>Message from API: {apiMessage}</Typography>
            <Typography variant="h4">Users</Typography>
            <UserList users={users} />
          </Stack>
        </Paper>
      </Stack>
    </>
  )
}

import { User } from '../models/user.ts'
import { List, ListItem, ListItemText } from '@mui/material'

interface UserListProps {
  users: User[]
}

export const UserList = ({ users }: UserListProps) => {
  return (
    <List>
      {users.map((user) => (
        <ListItem key={user.id}>
          <ListItemText>
            ({user.id}) {user.firstName} {user.lastName}
          </ListItemText>
        </ListItem>
      ))}
    </List>
  )
}

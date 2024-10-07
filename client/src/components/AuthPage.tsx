import {
  Button,
  Divider,
  Paper,
  Stack,
  styled,
  TextField,
  Typography,
} from '@mui/material'

export const AuthPage = () => {
  return (
    <Layout>
      <PaperStyled>
        <Stack spacing={2}>
          <Typography variant={'h3'}>Login</Typography>
          <TextField label="Email" type="email" fullWidth />

          <TextField label="Password" type="password" fullWidth />

          <Button variant="contained">Login</Button>
        </Stack>
      </PaperStyled>

      <PaperStyled>
        <Stack spacing={2}>
          <Typography variant={'h3'}>Register</Typography>

          <TextField label="Email" type="email" fullWidth />
          <TextField label="Password" type="password" fullWidth />
          <Divider />
          <TextField label="First name" fullWidth />
          <TextField label="Last name" fullWidth />

          <Button variant="contained">Register</Button>
        </Stack>
      </PaperStyled>
    </Layout>
  )
}

const Layout = styled('div')`
  padding-top: 40px;
  display: flex;
  flex-direction: row;
  justify-content: center;
  gap: 20px;
`

const PaperStyled = styled(Paper)`
  padding: 40px;

  width: 300px;
`

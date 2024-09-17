import './App.css'
import { RouterProvider } from 'react-router-dom'
import { APP_ROUTER } from './setup/app-router.tsx'

function App() {
  return <RouterProvider router={APP_ROUTER} />
}

export default App

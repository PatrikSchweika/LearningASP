import './App.css'
import { createBrowserRouter, RouterProvider } from 'react-router-dom'
import { AppRouter } from './setup/app-router.tsx'

const router = createBrowserRouter(AppRouter)

function App() {
  return <RouterProvider router={router} />
}

export default App

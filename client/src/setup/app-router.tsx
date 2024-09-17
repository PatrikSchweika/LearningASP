import { createBrowserRouter } from 'react-router-dom'
import { HomePage } from '../components/HomePage.tsx'
import { ErrorPage } from '../components/ErrorPage.tsx'

export const APP_ROUTER = createBrowserRouter([
  {
    path: '/',
    element: <HomePage />,
    errorElement: <ErrorPage />,
  },
])

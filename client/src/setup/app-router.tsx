import { RouteObject } from 'react-router-dom'
import { HomePage } from '../components/HomePage.tsx'

export const AppRouter: RouteObject[] = [
  {
    path: '/',
    element: <HomePage />,
  },
]

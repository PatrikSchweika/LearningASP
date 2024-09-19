import {
  createBrowserRouter,
  createRoutesFromElements,
  Route,
} from 'react-router-dom'
import { HomePage } from '../components/HomePage.tsx'
import { ErrorPage } from '../components/ErrorPage.tsx'
import { LoginPage } from '../components/LoginPage.tsx'

export const APP_ROUTER = createBrowserRouter(
  createRoutesFromElements(
    <>
      <Route path={''} element={<HomePage />} errorElement={<ErrorPage />} />
      <Route path={'/login'} element={<LoginPage />} />
    </>,
  ),
)

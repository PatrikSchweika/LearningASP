import {
  createBrowserRouter,
  createRoutesFromElements,
  Route,
} from 'react-router-dom'
import { HomePage } from '../components/HomePage.tsx'
import { ErrorPage } from '../components/ErrorPage.tsx'
import { AuthPage } from '../components/AuthPage.tsx'

export const APP_ROUTER = createBrowserRouter(
  createRoutesFromElements(
    <>
      <Route path={''} element={<HomePage />} errorElement={<ErrorPage />} />
      <Route path={'/auth'} element={<AuthPage />} />
    </>,
  ),
)

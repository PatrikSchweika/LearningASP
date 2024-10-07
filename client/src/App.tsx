import { RouterProvider } from 'react-router-dom'
import { APP_ROUTER } from './setup/app-router.tsx'
import { QueryClient, QueryClientProvider } from '@tanstack/react-query'

const queryClient = new QueryClient()

function App() {
  return (
    <QueryClientProvider client={queryClient}>
      <RouterProvider router={APP_ROUTER} />
    </QueryClientProvider>
  )
}

export default App

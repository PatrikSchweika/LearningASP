export interface AppConfig {
  apiUrl: string
}

export const APP_CONFIG = {
  apiUrl: import.meta.env.VITE_API_URL,
}

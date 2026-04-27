export interface User {
  id: number
  username: string
  email: string
}

export interface AuthResponse {
  user: User
  token: string
}

export type AuthContextType = {
  token: string | null
  user: User | null
  login: (data: AuthResponse) => void
  logout: () => void
}
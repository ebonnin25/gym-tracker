import { ReactNode, useState } from "react"
import { AuthContext } from "./auth-context"
import { AuthResponse, User } from "@/types/auth"

export default function AuthProvider({ children }: { children: ReactNode }) {
  const [token, setToken] = useState<string | null>(
    localStorage.getItem("token")
  )

  const [user, setUser] = useState<User | null>(() => {
  const storedUser = localStorage.getItem("user")
    return storedUser ? JSON.parse(storedUser) : null
  })

  const login = (data: AuthResponse) => {
    localStorage.setItem("token", data.token)
    localStorage.setItem("user", JSON.stringify(data.user))
    setToken(data.token)
    setUser(data.user)
  }

  const logout = () => {
    localStorage.removeItem("token")
    localStorage.removeItem("user")
    setToken(null)
    setUser(null)
  }

  return (
    <AuthContext.Provider value={{ token, user, login, logout }}>
      {children}
    </AuthContext.Provider>
  )
}
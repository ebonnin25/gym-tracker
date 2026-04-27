import { useState } from "react"
import { useAuth } from "@/app/auth/use-auth"
import { login, register } from "@/api/auth.api"
import { useNavigate } from "react-router-dom"

export default function LoginPage() {
  const [mode, setMode] = useState<"login" | "register">("login")
  const [username, setUsername] = useState("")
  const [email, setEmail] = useState("")
  const [password, setPassword] = useState("")
  const [error, setError] = useState("")
  const { login: loginContext } = useAuth()
  const navigate = useNavigate()

  async function handleSubmit(e: React.FormEvent) {
    e.preventDefault()
    setError("")

    try {
      if (mode === "login") {
        const response = await login(email, password)
        loginContext(response)
      } else {
        const response = await register(username, email, password)
        loginContext(response)
      }
      navigate("/")
    } catch (err) {
      console.error(err)
      setError(mode === "login" ? "Invalid credentials" : "Error creating the account")
    }
  }

  return (
    <div style={{ padding: 20 }}>
      <h1>{mode === "login" ? "Connexion" : "Créer un compte"}</h1>

      <form onSubmit={handleSubmit}>
        {mode === "register" && (
          <input
            type="text"
            placeholder="Username"
            value={username}
            onChange={e => setUsername(e.target.value)}
          />
        )}

        <input
          type="email"
          placeholder="Email"
          value={email}
          onChange={e => setEmail(e.target.value)}
        />

        <input
          type="password"
          placeholder="Password"
          value={password}
          onChange={e => setPassword(e.target.value)}
        />

        <button type="submit">
          {mode === "login" ? "Log in" : "Create an account"}
        </button>
      </form>

      {error && <p style={{ color: "red" }}>{error}</p>}

      <button onClick={() => { setMode(mode === "login" ? "register" : "login"); setError("") }}>
        {mode === "login" ? "Don't have an account yet? Sign up" : "Already have an account? Log in"}
      </button>
    </div>
  )
}
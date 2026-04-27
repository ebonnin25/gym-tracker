import { Link, useNavigate } from "react-router-dom"
import { useAuth } from "@/app/auth/use-auth"

export default function Header() {
  const { user, logout } = useAuth()
  const navigate = useNavigate()

  const handleLogout = () => {
    logout()
    navigate("/login")
  }

  return (
    <header className="border-b border-border bg-background">
      <div className="container mx-auto px-4 py-4 flex items-center justify-between">
        <Link to="/" className="text-lg font-semibold">
          GymTracker
        </Link>
        {user && (
          <button
            onClick={handleLogout}
            className="text-sm text-muted-foreground hover:text-foreground transition-colors"
          >
            Log out
          </button>
        )}
      </div>
    </header>
  )
}
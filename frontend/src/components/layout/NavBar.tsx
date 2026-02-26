import { Link } from "react-router-dom"

export default function Navbar() {
  return (
    <header className="border-b border-border bg-background">
      <div className="container mx-auto px-4 py-4 flex items-center justify-between">
        <Link to="/" className="text-lg font-semibold">
          GymTracker
        </Link>

        <nav className="flex gap-6 text-sm">
          <Link to="/" className="hover:text-accent transition">
            Home
          </Link>
        </nav>
      </div>
    </header>
  )
}
import { NavLink } from "react-router-dom"

export default function BottomNav() {
  return (
    <nav className="fixed bottom-0 left-0 right-0 bg-white border-t p-2 flex justify-around text-sm">
      <NavLink to="/" className={({ isActive }) => isActive ? "text-primary" : "text-gray-500"}>Home</NavLink>
      <NavLink to="/exercises" className={({ isActive }) => isActive ? "text-primary" : "text-gray-500"}>Exercises</NavLink>
      <NavLink to="/history" className={({ isActive }) => isActive ? "text-primary" : "text-gray-500"}>History</NavLink>
      <NavLink to="/stats" className={({ isActive }) => isActive ? "text-primary" : "text-gray-500"}>Stats</NavLink>
      <NavLink to="/profile" className={({ isActive }) => isActive ? "text-primary" : "text-gray-500"}>Profile</NavLink>
    </nav>
  )
}
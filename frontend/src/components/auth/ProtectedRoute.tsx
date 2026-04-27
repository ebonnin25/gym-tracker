import { Navigate } from "react-router-dom"
import { useAuth } from "@/app/auth/use-auth"
import { ReactNode } from "react"

export default function ProtectedRoute({
  children,
}: {
  children : ReactNode
}) {
  const { token } = useAuth()

  if (!token) {
    return <Navigate to="/login" />
  }

  return children
}
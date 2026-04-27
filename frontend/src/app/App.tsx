import { Outlet } from "react-router-dom"
import Header from "@/components/layout/Header"
import BottomNav from "@/components/layout/BottomNav"
// import ProtectedRoute from "@/components/auth/ProtectedRoute"

export default function App() {
  return (
    // <ProtectedRoute>
       <div className="min-h-screen bg-background text-foreground flex flex-col">
      <Header />
      <main className="flex-1 container mx-auto px-4 py-8">
        <Outlet />
      </main>
      <BottomNav />
    </div>
    // </ProtectedRoute>
  )
}
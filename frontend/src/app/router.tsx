import { createBrowserRouter } from "react-router-dom"
import App from "@/app/App"
import HomePage from "@/pages/HomePage"
import ExercisesPage from "@/pages/ExercisesPage"
import HistoryPage from "@/pages/HistoryPage"
import ProfilePage from "@/pages/ProfilePage"
import StatsPage from "@/pages/StatsPage"
import LoginPage from "@/pages/LoginPage"
import ProtectedRoute from "@/components/auth/ProtectedRoute"
import { Outlet } from "react-router-dom"


export const router = createBrowserRouter([
  {
    path: "/",
    element: <App />,
    children: [
      { path: "login", element: <LoginPage /> },
      {
        element: (
          <ProtectedRoute>
            <Outlet />
          </ProtectedRoute>
        ),
        children: [
          { index: true, element: <HomePage /> },
          { path: "exercises", element: <ExercisesPage /> },
          { path: "history", element: <HistoryPage /> },
          { path: "stats", element: <StatsPage /> },
          { path: "profile", element: <ProfilePage /> },
        ],
      },
    ],
  },
])

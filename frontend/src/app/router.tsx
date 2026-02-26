import { createBrowserRouter } from "react-router-dom"
import App from "@/app/App"
import HomePage from "@/pages/HomePage"
import ProfilePage from "@/pages/ProfilePage"

export const router = createBrowserRouter([
  {
    path: "/",
    element: <App />,
    children: [
      { index: true, element: <HomePage /> },
      { path: "profile", element: <ProfilePage/>}
    ],
  },
])
import { AuthResponse } from "@/types/auth"

const API_URL = "http://192.168.1.65:5134/api"

export async function apiFetch(
  endpoint: string,
  options: RequestInit = {},
) {
  const token = localStorage.getItem("token")
  
  const response = await fetch(`${API_URL}${endpoint}`, {
    ...options,
    headers: {
      "Content-Type": "application/json",
      ...(token ? { Authorization: `Bearer ${token}` } : {}),
      ...options.headers,
    },
  })

  if (!response.ok) {
    const error = await response.text()
    console.error("API ERROR: ", error)
    throw new Error(error || "API Error")
  }

  return response.json()
}

export async function loginUser(email: string, password: string) {
  const response = await apiFetch("/users/login", {
    method: "POST",
    body: JSON.stringify({ email, password })
  })
  return response as AuthResponse
}
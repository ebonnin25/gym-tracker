import { apiFetch } from "./api-client"
import { AuthResponse } from "@/types/auth"

export async function login(email: string, password: string) {
  return apiFetch("/users/login", {
    method: "POST",
    body: JSON.stringify({ email, password }),
  }) as Promise<AuthResponse>
}

export async function register(username: string, email: string, password: string) {
  return apiFetch("/users/register", {
    method: "POST",
    body: JSON.stringify({ username, email, password }),
  }) as Promise<AuthResponse>
}

export async function fetchMe() {
  return apiFetch("/users/me", {
    method: "GET",
  }) // renvoie UserDTO
}

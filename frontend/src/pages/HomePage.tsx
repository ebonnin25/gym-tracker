import { useAuth } from "@/app/auth/use-auth"
import { Button } from "@/components/ui/button"

export default function HomePage() {
  const { user } = useAuth()

  return (
    <div className="flex flex-col items-center justify-center text-center gap-6 py-20">
      <h1 className="text-4xl font-bold tracking-tight">
        Welcome to GymTracker {user?.username}
      </h1>

      <p className="text-muted-foreground max-w-xl">
        Track your workouts, analyze your progress and improve your
        performance with a clean and modern experience.
      </p>

      <Button size="lg">Start a Workout</Button>
    </div>
  )
}
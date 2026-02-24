*PROJECT GYM TRACKER BY ELOISE BONNIN*

## STACK
- Git 2.38
- Node.js 20.20.0 (LTS)
- .NET 8 SDK (LTS)
- PostgreSQL 18.1  
    -> user/mdp : postgres/azerty  
    -> port : 5432 

## LINKS
http://localhost:5134/swagger/index.html  

## MEMO COMMANDS

### BACKEND
run : `dotnet run`  
run with hotreload : `dotnet watch run`

### DATABASE
connecting bd : `psql -U postgres -h localhost -d gym` (mdp: azertt)  
show databases : `\list`  
show tables : `\dt`  
show columns of a table : `\d "TableName"`  
display one table : `SELECT * FROM "TableName"`  
quit : `\q`  

### MIGRATIONS
create migration : `dotnet ef migrations add MigrationName`  
update database : `dotnet ef database update`  

### GITLAB
merge branch :  
`git status`  
`git add .`  
`git commit -m "message"`  
`git push`  
`git checkout main`  
`git pull`
`git merge branchname`  
`git push`  
`git branch -d branchname`  
`git push origin --delete branchname`  
`git checkout -b newbranch`  
`git push -u origin newbranch`

# REMINDER

backend/  
├─ Application/  
│   ├─ DTOs/  
│   │   ├─ AuthResponseDTO.cs  
│   │   ├─ CreateExeerciseDTO.cs  
│   │   ├─ CreateMuscleDTO.cs  
│   │   ├─ CreateSessionDTO.cs  
│   │   ├─ CreateSessionExerciseDTO.cs  
│   │   ├─ ExerciseDTO.cs  
│   │   ├─ LoginUserDTO.cs  
│   │   ├─ MuscleDTO.cs  
│   │   ├─ RegisterUserDTO.cs  
│   │   ├─ SessionDTO.cs  
│   │   ├─ SessionExerciseDTO.cs  
│   │   ├─ SessionSetDTO.cs  
│   │   ├─ UpdateExerciseDTO.cs  
│   │   ├─ UpdateMuscleDTO.cs  
│   │   └─ UserDTO.cs  
│   └─ Services/  
│       ├─ AuthService.cs  
│       ├─ ExerciseService.cs  
│       ├─ MuscleService.cs  
│       └─ UserService.cs  
│  
├─ Controllers/  
│   ├─ ExercisesController.cs  
│   ├─ MusclesController.cs  
│   ├─ SessionsController.cs  
│   └─ UsersController.cs  
│  
├─ Domain/  
│   ├─ Entities/  
│   │   ├─ Exercise.cs  
│   │   ├─ ExerciseMuscle.cs  
│   │   ├─ Muscle.cs  
│   │   ├─ Session.cs  
│   │   ├─ SessionExercise.cs  
│   │   ├─ SessionSet.cs  
│   │   └─ User.cs  
│   └─ Repositories/  
│       ├─ IExerciseRepository.cs  
│       ├─ IMuscleRepository.cs  
│       ├─ ISessionRepository.cs  
│       └─ IUserRepository.cs  
│  
├─ Infrastructure  
│   └─ Persistence/  
│       ├─ ExerciseRepository.cs  
│       ├─ GymContext.cs  
│       ├─ MuscleRepository.cs  
│       ├─ SessionRepository.cs  
│       └─ UserRepository.cs  
│  
├─ Program.cs  


# Fondamentaux du projet

**Projet** : une application de suivi de fitness / gym tracker, pour permettre aux utilisateurs de :

* Créer un compte, se connecter, gérer leur profil.  
* Enregistrer leurs **exercices** et les **muscles** correspondants.  
* Planifier et enregistrer des **séances (sessions)**, avec la possibilité de détailler les **exercices et sets**, et éventuellement enregistrer les **records de poids/répétitions**.  
* Visualiser l’historique et la progression (type calendrier ou timeline).  
* Gestion des relations utilisateurs (amis, si tu veux partager des séances).  

**Backend** :

* ASP.NET Core, architecture **DDD simplifiée** :

  * `Domain`: Entités (`User`, `Exercise`, `Muscle`, `Session`, `SessionExercise`, `SessionSet`) et repositories.  
  * `Application`: Services (logique métier) + DTOs.  
  * `Infrastructure/Persistence`: EF Core (`GymContext`) et implémentations des repositories.  
  * Controllers exposent des routes REST sécurisées avec `[Authorize]`.  
* Gestion des erreurs via exceptions dans les services, mapping vers les DTOs pour les réponses.  
* La modularité du backend repose sur l’identification de l’utilisateur via le token JWT.  

**Frontend** (à prévoir) :

* Interface pour gérer : exercices, muscles, séances, progression.  
* Affichage des records et historique.  
* Création, édition, suppression d’exercices et séances.  

**Base de données** :

* PostgreSQL, tables liées par EF Core avec relations many-to-many et one-to-many.  
* Seed des muscles de base.  

**Fonctionnalités déjà codées** :

* CRUD complet pour **Muscle** et **Exercise**, avec vérifications basiques.  
* CRUD pour **Session** avec exercices et sets, mapping complet vers DTOs.  
* Gestion des relations et contraintes (exercice-muscle, session-exercise, session-set).  
* JWT + Authorize pour sécuriser les routes.  

**Fonctionnalités restantes / à améliorer** :

* Gestion complète des utilisateurs (profil, amis, suppression, édition).  
* Modularité et permissions côté backend (accès aux propres sessions/exercices).  
* Gestion des records de performance par exercice.  
* Frontend complet et intégré avec backend.  
* Dockerisation BDD, backend, frontend.  
* Tests unitaires / intégration.  
* CI/CD pipeline pour automatiser build, tests, déploiement.  
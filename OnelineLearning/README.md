

## Innstallation
1. Cloner le projet dans " https://github.com/DinaNampoina/plateforme-de-formation-en-ligne/ "
2. Créer une base PostgreSQL nommée  " OnelinLearningdb "
3. Adapter la chaîne de connexion dans OnelineLearning/appsettings.json
4. Appliquer les migrations 
   Add-Migration Nomgration
   Update-Database

5. Lancer le projet : https://localhost:7207/swagger

## Architecture
Controllers/   -> endpoints REST
DTOs/          -> objets d'entrée/sortie 
Models/        -> entités EF Core
Data/          -> ApplicationDbContext (Fluent API)
Repositories/  -> Interfaces/ et Implementations/
Migrations/    -> migrations EF Core

## Relations
- Student 1..1 StudentProfile (suppression en cascade)
- Category 1..N Course 
- Student N..N 
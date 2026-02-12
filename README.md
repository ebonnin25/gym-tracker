*PROJECT GYM TRACKER BY ELOISE BONNIN*

## STACK
- Git 2.38
- Node.js v24.13.1 (LTS)
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
connecting bd : `psql -U postgres -h localhost -d gym`  
show databases : `\list`  
show tables : `\dt`  
display one table : `SELECT * FROM "TableName`  
quit : `\q`  

### MIGRATIONS
create migration : `dotnet ef migrations add MigrationName`  
update database : `dotnet ef database update`  
# LearningMarten
A project where I am learning about Marten


## Prerequsites
- Install dotnet core 2.1
- Install postgres (details below)

## Creating a postgres database
- Download postgres `https://www.enterprisedb.com/downloads/postgres-postgresql-downloads`
- I downloaded 64 bit windows 9.5
- Run through the installer
- Enter password as `password`
- Enter port as `5432`
- Skip Stack Builder and click finish

## Setting up the database management tool
- Open up a database management tool
- I used DBeaver
- Create a postgres database instance
- Host = `localhost`
- Database = `postgres`
- User = `postgres`
- Password = `password`
- Click `Test Connection...`
- There you go

## Creating a new project
- Run the command `dotnet new console`
- Open the project folder in an editor e.g. vs code

## Setting up a user for the new database
Enter these commands one at a time:
- `CREATE ROLE rds_superuser;`
- `ALTER ROLE rds_superuser WITH SUPERUSER INHERIT CREATEROLE CREATEDB LOGIN NOREPLICATION VALID UNTIL 'infinity';`
- `CREATE ROLE username LOGIN NOSUPERUSER INHERIT CREATEDB CREATEROLE NOREPLICATION;`
- `GRANT rds_superuser TO username;`
- `ALTER USER username with password 'password';`
`

## Once you have done this you can setup a new connection for Marten
`var store = DocumentStore.For("host=localhost; database=postgres; password=password;username=username");`


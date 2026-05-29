# Loans API

A very basic API for a loan application. The following endpoint are implemented:

* GET -> /loans, retrieves all the loans that exist in the database. **Use with caution!**
* GET -> /loans/:id, retrieves a specific loan by its id, if it exists.
* POST -> /loans, creates a new loan. Example request: { "amountRequested": 1000, "acountHolderId": 3}
* POST -> /loans/:id/payment, makes a payment to the loan specified by `:id`. Example request: { "amount": 500 }
* GET -> /account-holders, retrieves the list of account holders.

There's a Postman collection available in the `backend` folder too.

## Running the Backend

To build the backend, navigate to the `src` folder and run:

```sh
dotnet build
```

To run all tests:  
```sh
dotnet test
```

To start the main API:  
```sh
cd Fundo.Applications.WebApi  
dotnet run
```

## Missings Features

* Due to a lack of time, I wasn't able to implement the following features:
  * [ ] Unit testing
  * [ ] Integration testing on the account holder API
  * [ ] Authentication and Authorization
  * [ ] Dockerization
  * [ ] Disable all CORS origins, methods and headers; instead, be specific about the ones that are allowed
  * [ ] implement/integrate with an IAM (Identity and Access Management) system for secure access control, instead of implementing a user/password system.
  * [ ] Improve the logging system (i added too few logs).
  * [ ] Add a health check to the database in the compose file so the backend can start only after the database is up.
  * [ ] Add validation. I added just a few validations to the models.
  * [ ] Speaking of the model, improve the database schema: separate schemas, use a better naming convention.
  * [ ] Pagination, Sorting, filtering and projections. The fact that we're retrieving all the entities at once would be a problem for large database.
  * [ ] I would expose the API using an API Gateway to improve monitoring and security.

## Running With Docker

First you need to build the image, but to do that, you need to have docker, docker-compose and an environment file named `.env` in along the docker-compose.yml file file:

```
# .env
APP_PORT=5000 # The port used by the server (.net app)
HOST_APP_PORT=5000 # port published by the docker container (i.e, the one you use to consume the API from postman or the frontend).
DB_PASSWORD=A_SECURE_PASSWORD # be sure to use a strong password, otherwise mssql will complain.
APP_DB_PORT=1433 # The port used by the database in the docker network
HOST_DB_PORT=1433 # the port published by the docker container (i.e, the one you use to connect to the database from your computer).
```

Now you can build the image:

```
cd backend # assuming you are in the root of the project
docker-compose build
```

To test it, you have to seed the database. To do that, you have to run the following command from the `backend/src` folder:

```
# Replace DB_PASSWORD with the one you set in the .env file.
dotnet ef database update --project Fundo.Infrastructure/Fundo.Infrastructure.csproj --startup-project Fundo.Applications.WebApi/Fundo.Applications.WebApi.csproj --context Fundo.Infrastructure.DatabaseContext --configuration Debug 20260528175235_Initial --connection "Server=localhost,1433;Database=master;User Id=sa; Password=DB_PASSWORD;TrustServerCertificate=True" 
```

Now you should be able to consume the API with postman: `http://localhost:$HOST_APP_PORT`
You can also use the frontend to consume the API (localhost:4200 once you run it).
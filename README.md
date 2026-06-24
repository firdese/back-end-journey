# TaskTracker Backend (ASP.NET Core)

This repository hosts development of TaskTracker API using ASP.NET Core.

Current main project: 

## What This Repository Demonstrates

- RESTful API design
- Clean separation of layers (Onion Architecture)
- Entity modeling & database design
- Authentication middleware integration
- Dockerized local development
- Environment-based configuration
- Dependency injection patterns
- Error handling strategies

## Tech Stack

- ASP.NET Core Web API
- Entity Framework Core
- PostgreSQL
- Docker
- Keycloak (optional local authentication)

## Project Structure

TaskTracker.API/
TaskTracker.Application/
TaskTracker.Infrastructure/
TaskTracker.Domain/
docker-compose.yml

## Running Locally

docker compose up --build

By default, Docker Compose starts only the API. The API expects database and
authentication services to come from its active configuration.

To run the API with local PostgreSQL, Keycloak, and LocalStack S3 containers,
use the `local` profile and local override file:

docker compose --profile local -f docker-compose.yml -f docker-compose.override.yml -f docker-compose.local.yml up --build

Set `KEYCLOAK_REALM` in `.env` to match the realm used by the frontend and
backend token validation config.

LocalStack exposes S3 on `http://localhost:4566` from the host and
`http://localstack:4566` from other Compose services. Set `S3_BUCKET_NAME` in
`.env` to choose the bucket created on startup.

## API Endpoints

All API endpoints require a bearer token. Request bodies use DTO contracts instead of domain entities.

GET /taskgroup
POST /taskgroup
PUT /taskgroup
DELETE /taskgroup
GET /tasks?taskGroupId={id}
POST /tasks
PUT /tasks
DELETE /tasks

POST /storage
GET /storage/{objectKey}
DELETE /storage/{objectKey}

Create task requests include client-owned fields such as `taskDescription`,
`taskGroupId`, `taskProgress`, `taskSortOrder`, and `taskPriority`. Response
DTOs include server-managed fields such as IDs and UTC timestamps.

Storage uploads accept multipart form data with a `file` field. Returned
`objectKey` values are scoped to the authenticated user and can be used with
the download and delete storage endpoints.

Frontend repository:
https://github.com/firdese/tasktracker-frontend-angular

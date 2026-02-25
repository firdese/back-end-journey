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
- Keycloak (authentication)

## Project Structure

TaskTracker.API/
TaskTracker.Application/
TaskTracker.Infrastructure/
TaskTracker.Domain/
docker-compose.yml

## Running Locally

docker compose up --build

## API Endpoints

GET /task-groups
GET /task-groups/{id}/tasks
POST /tasks
PUT /tasks/{id}
DELETE /tasks/{id}

Frontend repository:
https://github.com/firdese/tasktracker-frontend-angular

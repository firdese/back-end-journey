# Backend Journey

This repository contains backend-focused projects and architectural
experiments using ASP.NET Core.

Current main project: TaskTracker API

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


## Roadmap

- [ ] Add integration tests
- [ ] Implement global exception middleware
- [ ] Add CI pipeline
- [ ] Add cloud deployment version
- [ ] Refactor toward Clean Architecture

Frontend repository:
https://github.com/firdese/front-end-journey

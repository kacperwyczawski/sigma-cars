# Sigma Cars

## How to run

1. Install [Docker](https://www.docker.com/)
2. Clone repository: `git clone https://github.com/kacperwyczawski/sigma-cars.git SigmaCars`
3. Run backend with `docker compose up -f SigmaCars/Backend/docker-compose.yml`.

## Technologies used in this project

Backend: Docker, Asp.Net Core, Dapper, PostgreSQL

## Other info

### Docker

This project uses 3 docker files:

1. [docker-compose.yml](Backend/docker-compose.yml)
2. Api [Dockerfile](Backend/Dockerfile)
3. Database [Dockerfile](Backend/SigmaCars.Database/Dockerfile)

If you want to apply changes to codebase append `--build` to docker compose command

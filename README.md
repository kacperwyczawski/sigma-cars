![Sigma cars](Assets/header.svg)

> **Note**
>
> Currently, application is still under development. Stay tuned for updates!

## 📝 Description

Sigma Cars is a car rental application that allows users to rent cars for personal or business use. This is a learning project that was created to practice and demonstrate the development of a full stack web application. Users can search for cars by location, price, and availability, and make reservations for the selected car.

## 🛠️ How to run

1. Install [Docker](https://www.docker.com/) and [Git](https://git-scm.com/downloads) on your machine.
2. Clone this repository by running `git clone https://github.com/kacperwyczawski/sigma-cars.git SigmaCars`.
3. Run with `docker compose up -f SigmaCars/docker-compose.yml`.

## 🚀 How to use

- After running the application, open `http://localhost` in your preferred web browser.
- You can access OpenAPI schema at `http://localhost:5000/schema/v1` (can be imported into Postman).
- All REST API endpoints start with `http://localhost:5000`.

## ℹ️ Other info

### 💻 Technologies

The following technologies were used in the development of this project:

- Backend: Asp.Net Core, Dapper, PostgreSQL, OpenAPI, FluentValidation
- Other tools: Docker, Postman, Rider

### 🐋 Docker

This project uses some docker configuration files:

1. [docker-compose.yml](Backend/docker-compose.yml)
2. Backend [Dockerfile](Backend/Dockerfile)
3. Database [Dockerfile](Backend/SigmaCars.Database/Dockerfile)
4. Frontend [Dockerfile](Frontend/Dockerfile)

To apply changes to the codebase, append `--build` to the docker compose command.

## 📫 Feedback

I hope you find Sigma Cars project helpful! If you encounter any issues or have any feedback, please don't hesitate to contact me via github issues.

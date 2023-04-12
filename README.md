# Sigma Cars

> **Note**
>
> Currently, the frontend is not yet available, but it will most likely be developed using React, Tailwind CSS, Next.js and TypeScript. Backend is still under development. Stay tuned for updates!

## 📝 Description

Sigma Cars is a car rental application that allows users to rent cars for personal or business use. This is a learning project that was created to practice and demonstrate the development of a full stack web application. Users can search for cars by location, price, and availability, and make reservations for the selected car.

## 🛠️ How to run

1. Install [Docker](https://www.docker.com/) and [Git](https://git-scm.com/downloads) on your machine.
2. Clone this repository by running `git clone https://github.com/kacperwyczawski/sigma-cars.git SigmaCars`.
3. Run with `docker compose up -f SigmaCars/Backend/docker-compose.yml`.

## 🚀 How to use

- After running the application, open `http://localhost:6000` in your preferred web browser. *(not working yet)*
- You can access OpenAPI schema at `http://localhost:5000/schema/v1` (can be imported into Postman).

## ℹ️ Other info

### 💻 Technologies

The following technologies were used in the development of this project:

- Backend: Asp.Net Core, Dapper, PostgreSQL, OpenAPI, FluentValidation
- Fullstack: Docker
- Tools: Postman, Rider

### 🐋 Docker

This project uses 3 docker files:

1. [docker-compose.yml](Backend/docker-compose.yml)
2. Api [Dockerfile](Backend/Dockerfile)
3. Database [Dockerfile](Backend/SigmaCars.Database/Dockerfile)

To apply changes to the codebase, append `--build` to the docker dompose command.

## 📫 Feedback

I hope you find Sigma Cars project helpful! If you encounter any issues or have any feedback, please don't hesitate to contact me via github issues.

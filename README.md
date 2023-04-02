# Sigma Cars

## 📝 Description

Sigma Cars is a car rental application that allows users to rent cars for personal or business use. This is a learning project that was created to practice and demonstrate the development of a full stack web application. Users can search for cars by location, price, and availability, and make reservations for the selected car.

## 🚀 How to run

1. Install [Docker](https://www.docker.com/) and [Git](https://git-scm.com/downloads) on your machine.
2. Clone this repository by running `git clone https://github.com/kacperwyczawski/sigma-cars.git SigmaCars`
3. Run backend with `docker compose up -f SigmaCars/Backend/docker-compose.yml`.



## ℹ️ Other info

### 💻 Technologies

The following technologies were used in the development of this project:

- Backend: Docker, Asp.Net Core, Dapper, PostgreSQL

### 🐋 Docker

This project uses 3 docker files:

1. [docker-compose.yml](Backend/docker-compose.yml)
2. Api [Dockerfile](Backend/Dockerfile)
3. Database [Dockerfile](Backend/SigmaCars.Database/Dockerfile)

To apply changes to the codebase, please append `--build` to the docker dompose command.

## 📫 Feedback

I hope you find Sigma Cars project helpful! If you encounter any issues or have any feedback, please don't hesitate to contact me via github issues.

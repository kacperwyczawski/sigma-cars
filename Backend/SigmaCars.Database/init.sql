create table cars (
    id serial primary key,
    car_model_id int not null,
    department_id int not null,
    registration_number varchar(10) not null,
    vin varchar(17) not null
);

create table car_models (
    id serial primary key,
    make varchar(50) not null,
    model varchar(50) not null,
    production_year smallint not null,
    color varchar(50) not null,
    price real not null,
    seatCount smallint not null
);

create table departments (
    id serial primary key,
    country_code varchar(2) not null,
    city varchar(50) not null,
    address varchar(100) not null
);

create table rentals (
    id serial primary key,
    car_id int not null,
    customer_id int not null,
    start_date timestamp not null,
    end_date timestamp not null
);

create table scheduled_maintenances (
    id serial primary key,
    car_id int not null,
    employee_id int not null,
    start_date timestamp not null,
    end_date timestamp not null,
    reason text not null
);

create table users (
    id serial primary key,
    first_name varchar(100) not null,
    last_name varchar(100) not null,
    email varchar(100) not null,
    password_hash varchar(100) not null,
    role varchar(50) not null
);
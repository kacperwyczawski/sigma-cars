create table cars
(
    id                  serial primary key,
    car_type_id         int         not null,
    department_id       int         not null,
    registration_number varchar(10) not null,
    vin                 varchar(17) not null
);

create table car_types
(
    id              serial primary key,
    make            varchar(50) not null,
    model           varchar(50) not null,
    production_year int         not null,
    price_per_day   real        not null,
    seat_count      int         not null
);

create table departments
(
    id           serial primary key,
    country_code varchar(2)   not null,
    city         varchar(50)  not null,
    address      varchar(100) not null
);

create table rentals
(
    id         serial primary key,
    car_id     int       not null,
    user_id    int       not null,
    start_date timestamp not null,
    end_date   timestamp not null
);

create table users
(
    id            serial primary key,
    first_name    varchar(100) not null,
    last_name     varchar(100) not null,
    email         varchar(100) not null,
    password_hash varchar(100) not null,
    role          varchar(50)  not null
);

insert into users (first_name, last_name, email, password_hash, role)
values ('John', 'Doe', 'admin@sigma.cars', 'admin', 'admin');
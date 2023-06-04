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
    seat_count      int         not null,
    image           bytea       not null
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

insert into car_types (make, model, production_year, price_per_day, seat_count, image)
values ('DeLorean', 'DMC-12', 1981, 350, 2, pg_read_binary_file('/images/delorean.jpg')),
       ('Aston Martin', 'DB5', 1964, 300, 2 , pg_read_binary_file('/images/astonmartin.jpg')),
       ('Volkswagen', 'Passat', 2019, 99.99, 5 , pg_read_binary_file('/images/volkswagen.jpg'));

insert into departments (country_code, city, address)
values ('US', 'Boston', 'August St. 1976');

insert into cars (car_type_id, department_id, registration_number, vin)
values (1, 1, 'OUTATIME', '12345678901234567'),
       (2, 1, 'BMT216A', '23456789012345678'),
       (3, 1, '345678', '34567890123456789'),
       (3, 1, '456789', '45678901234567890');

insert into users (first_name, last_name, email, password_hash, role)
values ('James', 'Bond', 'admin@sigma.cars', 'admin', 'admin');

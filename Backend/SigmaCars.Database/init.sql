create table Cars (
    Id serial primary key,
    CarModelId int not null,
    DepartmentId int not null,
    RegistrationNumber varchar(10) not null,
    Vin varchar(17) not null
);

create table CarModels (
    Id serial primary key,
    Make varchar(50) not null,
    Model varchar(50) not null,
    Year smallint not null,
    Color varchar(50) not null,
    Price real not null
);

create table Departments (
    Id serial primary key,
    CountryCode varchar(2) not null,
    City varchar(50) not null,
    Address varchar(100) not null
);

create table Rentals (
    Id serial primary key,
    CarId int not null,
    CustomerId int not null,
    StartDate timestamp not null,
    EndDate timestamp not null
);

create table ScheduledMaintenances (
    Id serial primary key,
    CarId int not null,
    EmployeeId int not null,
    StartDate timestamp not null,
    EndDate timestamp not null,
    Reason text not null
);

create table Users (
    Id serial primary key,
    FirstName varchar(100) not null,
    LastName varchar(100) not null,
    Email varchar(100) not null,
    PasswordHash varchar(100) not null,
    Role varchar(50) not null
);
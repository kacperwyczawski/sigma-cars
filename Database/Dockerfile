FROM postgres
ENV POSTGRES_PASSWORD=1qazXSW@
ENV POSTGRES_DB=sigma-cars
COPY ./init.sql /docker-entrypoint-initdb.d/
COPY ./Images/* /images/
EXPOSE 5432
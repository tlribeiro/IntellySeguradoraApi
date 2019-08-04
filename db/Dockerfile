FROM mysql:5.7.22
EXPOSE 3306
COPY ./IntellySeguradoraApi/DB/ home/database
COPY ./IntellySeguradoraApi/CI/init_database.sh /docker-entrypoint-initdb.d/init_database.sh

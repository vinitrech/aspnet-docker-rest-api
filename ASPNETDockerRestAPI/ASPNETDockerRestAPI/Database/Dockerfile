FROM mysql:8.0.35
EXPOSE 3306
COPY ./ASPNETDockerRestAPI/Database/Migrations/ /home/database
COPY ./ASPNETDockerRestAPI/Database/Datasets/ /home/database
COPY ./ASPNETDockerRestAPI/Database/init_database.sh /docker-entrypoint/initdb.d/init_database.sh

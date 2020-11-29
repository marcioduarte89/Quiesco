FROM docker.elastic.co/logstash/logstash:7.7.0

USER root 

COPY mssql-jdbc-8.4.1.jre8.jar /usr/share/logstash/logstash-core/lib/jars/mssql-jdbc-8.4.1.jre8.jar
COPY mongo-java-driver-3.12.6.jar /usr/share/logstash/logstash-core/lib/jars/mongo-java-driver-3.12.6.jar
COPY gson-2.8.6.jar /usr/share/logstash/logstash-core/lib/jars/gson-2.8.6.jar
COPY mongojdbc2.3.jar /usr/share/logstash/logstash-core/lib/jars/mongojdbc2.3.jar

COPY ./Pipelines/pipelines.yml /usr/share/logstash/config/pipelines.yml
COPY ./Pipelines/sqlConfig.conf /usr/share/logstash/sqlConfig.conf
COPY ./Pipelines/mongoConfig.conf /usr/share/logstash/mongoConfig.conf

COPY ./Pipelines/sqlLastValue.yaml /usr/share/logstash/sqlLastValue.yaml
COPY ./Pipelines/mongoSqlLastValue.yaml /usr/share/logstash/mongoSqlLastValue.yaml


# Add logstash plugins setup here:
#RUN logstash-plugin install logstash-input-jdbc
#RUN bin/logstash-plugin install logstash-input-mongodb
# FROM java:8
FROM maven:3.5-jdk-8-alpine

WORKDIR /app

ADD pom.xml /app/pom.xml
ADD src /app/src

RUN [ "mvn", "clean" ]
RUN [ "mvn", "package" ]

EXPOSE 8080

CMD [ "/app/target/review-service.jar" ]
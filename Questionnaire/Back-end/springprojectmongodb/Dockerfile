# Build stage
FROM maven:3.8.3-openjdk-17 AS build
WORKDIR /home/app
COPY src /home/app/src
COPY pom.xml /home/app
COPY run.sh /home/app     
RUN mvn -f /home/app/pom.xml clean package

# # Run stage
# FROM openjdk:17
# COPY --from=build /home/app/target/springprojectmongodb-0.0.1-SNAPSHOT.jar /usr/local/lib/springprojectmongodb-0.0.1-SNAPSHOT.jar
# COPY --from=build /home/app/run.sh /usr/local/bin/run.sh 
# RUN chmod +x /usr/local/bin/run.sh  
# ENTRYPOINT ["sh", "-c", "/usr/local/bin/run.sh"] 

# Run stage
FROM openjdk:17
COPY --from=build /home/app/target/*.jar /usr/local/lib/springprojectmongodb-0.0.1-SNAPSHOT.jar
COPY --from=build /home/app/run.sh /usr/local/bin/run.sh 
RUN chmod +x /usr/local/bin/run.sh  
ENTRYPOINT ["sh", "-c", "/usr/local/bin/run.sh"] 

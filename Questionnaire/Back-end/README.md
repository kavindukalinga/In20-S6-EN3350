# Questionnaire Backend

This backend is developed using Java Spring Boot, where the Spring Boot application is connected with a Mongodb-atlas database to store data related to players and questionnaires. In the database, the name is `player_db`, which consists of three collections: `questions`, `users` and `accessed`. 

- `questions` table is used to store the correct answers to the questionnaire.
- `user` table is used to store logging information of the user for authentification.
- `accessed` table is used to store the flag of the questionnaire to verify whether the player has attempted the questionnaire or not.

## Prerequisites
Ensure you have the following installed:
1. Java Development Kit 17/21
2. Spring Boot in Visual Studio Code
3. Docker-destop

## Steps to Install Java Development Kit
1. Follow [this link](https://www.oracle.com/java/technologies/downloads/) to download the JDK setup.
2. Follow [this YouTube video](https://www.youtube.com/watch?v=WRISYpKhIrc&t=2s) for installation guidance.

## Steps to Add Spring Boot Extension on Visual Studio Code
Follow [this link](https://code.visualstudio.com/docs/java/java-spring-boot) to add Spring Boot extension on Visual Studio Code.

## Steps to Install Docker-destop
Follow [this link](https://www.docker.com/get-started/) to install docker-destop.

# How to Run the Application
(Make sure to have a proper internet connection to connect with mongodb)
## Method 1

### Step 1:
Navigate to the `In20-S6-EN3350\Questionnaire\Back-end\springprojectmongoprojectmongodb` folder, open a terminal, and run the following commands according to your machine:
```bash
& 'C:\Program Files\Java\jdk-21\bin\java.exe' '@C:\Users\nuwan\AppData\Local\Temp\cp_ro0fkjvkub1511x2kn2p4tv1.argfile' 'com.infiniteloop.springproject.SpringprojectmongodbApplication'
```
Or
Go to the Spring Boot dashboard and run the Spring project.

## Method 2

### step 1:
Start the docker Engine.

### step 2:
Navigate to the `In20-S6-EN3350\Questionnaire\Back-end\springprojectmongoprojectmongodb` folder, open a terminal, and run the following commands:
```bash
docker build -t spring_mongo_image .
```
```bash
docker run -p 9000:9000 --name spring_mongo_container spring_mongo_image
```

## Reset the Questionnaire

### Step 1: Create an Account

To create an account, use the following endpoint:

- **Type**: POST
- **Endpoint**: [http://localhost:9000/auth/signup](http://localhost:9000/auth/signup)
- **Body**:
    ```json
    {
        "login": "xxxxxx",
        "password": "xxxx"
    }
    ```

### Step 2: Get the Access Token

To obtain the access token, use the following endpoint:

- **Type**: POST
- **Endpoint**: [http://localhost:9000/auth/signin](http://localhost:9000/auth/signin)
- **Request Body**:
    ```json
    {
        "login": "xxxxxx",
        "password": "xxxx"
    }
    ```
- **Response**:
    ```json
    {
        "accessToken": "xxxxxxx"
    }
    ```

### Step 3: Reset

To reset, use the following endpoint:

- **Type**: GET
- **Endpoint**: [http://localhost:9000/reset/{login}](http://localhost:9000/reset)



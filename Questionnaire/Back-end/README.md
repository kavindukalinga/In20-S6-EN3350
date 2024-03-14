# Questionnaire Backend

This backend is developed using Java Spring Boot, where the Spring Boot application is connected with a MySQL database to store data related to players and questionnaires. In the database, the name is `player_db`, which consists of three tables: `questions`, `player`, and `accessed`. 

- `questions` table is used to store the correct answers to the questionnaire.
- `player` table is used to store player information.
- `accessed` table is used to store the flag of the questionnaire to verify whether the player has attempted the questionnaire or not.

## Prerequisites
Ensure you have the following installed:
1. MySQL 8.0.36
2. Java Development Kit 21.0.2
3. Spring Boot in Visual Studio Code

## Steps to Install MySQL
1. Follow [this link](https://dev.mysql.com/downloads/installer/) to download the MySQL setup.
2. Follow [this YouTube video](https://www.youtube.com/watch?v=GwHpIl0vqY4) for installation guidance.

## Steps to Install Java Development Kit
1. Follow [this link](https://www.oracle.com/java/technologies/downloads/) to download the JDK setup.
2. Follow [this YouTube video](https://www.youtube.com/watch?v=WRISYpKhIrc&t=2s) for installation guidance.

## Steps to Add Spring Boot Extension on Visual Studio Code
Follow [this link](https://code.visualstudio.com/docs/java/java-spring-boot) to add Spring Boot extension on Visual Studio Code.

## How to Run the Application
### Step 1:
Open MySQL Workbench and create a new connection using the following details:
- Username: infiniteloop
- Password: infiniteloop

### Step 2:
Navigate to the `In20-S6-EN3350\Questionnaire\Back-end\springproject` folder, open a terminal, and run the following command according to your machine:
```bash
& 'C:\Program Files\Java\jdk-21\bin\java.exe' '@C:\Users\nuwan\AppData\Local\Temp\cp_ro0fkjvkub1511x2kn2p4tv1.argfile' 'com.infiniteloop.springproject.SpringprojectApplication'
```
or 


markdown
Copy code
# Questionnaire Backend

This backend is developed using Java Spring Boot, where the Spring Boot application is connected with a MySQL database to store data related to players and questionnaires. In the database, the name is `player_db`, which consists of three tables: `questions`, `player`, and `accessed`. 

- `questions` table is used to store the correct answers to the questionnaire.
- `player` table is used to store player information.
- `accessed` table is used to store the flag of the questionnaire to verify whether the player has attempted the questionnaire or not.

## Prerequisites
Ensure you have the following installed:
1. MySQL 8.0.36
2. Java Development Kit 21.0.2
3. Spring Boot in Visual Studio Code

## Steps to Install MySQL
1. Follow [this link](https://dev.mysql.com/downloads/installer/) to download the MySQL setup.
2. Follow [this YouTube video](https://www.youtube.com/watch?v=GwHpIl0vqY4) for installation guidance.

## Steps to Install Java Development Kit
1. Follow [this link](https://www.oracle.com/java/technologies/downloads/) to download the JDK setup.
2. Follow [this YouTube video](https://www.youtube.com/watch?v=WRISYpKhIrc&t=2s) for installation guidance.

## Steps to Add Spring Boot Extension on Visual Studio Code
Follow [this link](https://code.visualstudio.com/docs/java/java-spring-boot) to add Spring Boot extension on Visual Studio Code.

## How to Run the Application
### Step 1:
Open MySQL Workbench and create a new connection using the following details:
- Username: infiniteloop
- Password: infiniteloop

### Step 2:
Navigate to the `In20-S6-EN3350\Questionnaire\Back-end\springproject` folder, open a terminal, and run the following command according to your machine:
```bash
& 'C:\Program Files\Java\jdk-21\bin\java.exe' '@C:\Users\nuwan\AppData\Local\Temp\cp_ro0fkjvkub1511x2kn2p4tv1.argfile' 'com.infiniteloop.springproject.SpringprojectApplication'
```
Or
Go to the Spring Boot dashboard and run the Spring project.

docker build -t spring_mongo_image .

docker run -p 9000:9000 --name spring_mongo_container spring_mongo_image

docker run -d -p 9000:9000 --name spring_mongo_container spring_image
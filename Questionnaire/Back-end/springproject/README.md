docker build -t spring_image .

docker run -p 8800:8800 --name spring_container spring_image

docker run -d -p 8800:8800 --name spring_container  spring_image
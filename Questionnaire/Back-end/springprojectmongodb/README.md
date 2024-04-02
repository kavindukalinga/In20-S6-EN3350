# Spring Mongo Docker Setup

This file contains Docker setup for running a Spring application with MongoDB.

## Usage

### Building the Docker Image

To build the Docker image, run the following command:

```bash
docker build -t spring_mongo_image .
```

### Runing the docker container

1. Option 1 : Run Interactively

```bash
docker run -p 9000:9000 --name spring_mongo_container spring_mongo_image
```

2. Option2 : Run in Detached Mode

```bash
docker run -d -p 9000:9000 --name spring_mongo_container spring_mongo_image
```




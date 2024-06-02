package com.infiniteloop.springprojectmongodb.repositories;


import org.springframework.data.mongodb.repository.MongoRepository;
import com.infiniteloop.springprojectmongodb.models.Animal;

public interface AnimalRepo extends MongoRepository<Animal, String> {
    Animal findByName(String name);
}
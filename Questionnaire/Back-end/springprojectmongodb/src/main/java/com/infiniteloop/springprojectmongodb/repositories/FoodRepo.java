package com.infiniteloop.springprojectmongodb.repositories;
import com.infiniteloop.springprojectmongodb.models.Food;

import java.util.List;

import org.springframework.data.mongodb.repository.MongoRepository;
import org.springframework.data.mongodb.repository.Query;

public interface FoodRepo extends MongoRepository<Food, String> {
    Food findByName(String name);

    @Query("{'animalTypes': ?0}")
    List<Food> findByAnimalTypes(String animalType);
}


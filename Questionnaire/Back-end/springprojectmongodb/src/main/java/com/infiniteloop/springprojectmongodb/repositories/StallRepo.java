package com.infiniteloop.springprojectmongodb.repositories;

import org.springframework.data.mongodb.repository.MongoRepository;
import com.infiniteloop.springprojectmongodb.models.Stall;

public interface StallRepo extends MongoRepository<Stall, String> {
    Stall findByName(String name);
}

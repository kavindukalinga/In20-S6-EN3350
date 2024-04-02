package com.infiniteloop.springprojectmongodb.repositories;

import org.springframework.data.mongodb.repository.MongoRepository;
import com.infiniteloop.springprojectmongodb.models.Accessed;

public interface AccessedRepo extends MongoRepository<Accessed, String> {
    
}

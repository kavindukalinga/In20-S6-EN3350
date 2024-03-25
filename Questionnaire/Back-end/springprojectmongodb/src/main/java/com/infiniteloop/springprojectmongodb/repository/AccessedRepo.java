package com.infiniteloop.springprojectmongodb.repository;

import org.springframework.data.mongodb.repository.MongoRepository;
import com.infiniteloop.springprojectmongodb.model.Accessed;

public interface AccessedRepo extends MongoRepository<Accessed, String> {
    
}

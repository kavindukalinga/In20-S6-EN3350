package com.infiniteloop.springprojectmongodb.repositories;


import java.util.Optional;

import org.springframework.data.mongodb.repository.MongoRepository;
import com.infiniteloop.springprojectmongodb.models.Accessed;

// Repository interface for accessing Accessed documents in MongoDB
public interface AccessedRepo extends MongoRepository<Accessed, String> {

    Optional<Accessed> findByLogin(String login);

    
}

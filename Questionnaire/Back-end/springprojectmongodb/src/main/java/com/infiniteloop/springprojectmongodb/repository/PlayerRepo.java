package com.infiniteloop.springprojectmongodb.repository;

import org.springframework.data.mongodb.repository.MongoRepository;
import com.infiniteloop.springprojectmongodb.model.Player;

public interface PlayerRepo extends MongoRepository<Player, String> {
    
}
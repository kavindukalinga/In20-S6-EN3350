package com.infiniteloop.springprojectmongodb.repositories;

import org.springframework.data.mongodb.repository.MongoRepository;
import com.infiniteloop.springprojectmongodb.models.Player;

public interface PlayerRepo extends MongoRepository<Player, String> {

}


package com.infiniteloop.springprojectmongodb.repositories;

import org.springframework.data.mongodb.repository.MongoRepository;
import org.springframework.security.core.userdetails.UserDetails;

import com.infiniteloop.springprojectmongodb.models.User;

public interface UserRepo extends MongoRepository<User, String> {
    UserDetails findByLogin(String login);
}

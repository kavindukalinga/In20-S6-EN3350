package com.infiniteloop.springprojectmongodb.repositories;

import org.springframework.data.mongodb.repository.MongoRepository;
import org.springframework.security.core.userdetails.UserDetails;

import com.infiniteloop.springprojectmongodb.models.User;

// Repository interface for accessing User documents in MongoDB
public interface UserRepo extends MongoRepository<User, String> {
    
    // Custom query method to find a user by their login username
    UserDetails findByLogin(String login);
}

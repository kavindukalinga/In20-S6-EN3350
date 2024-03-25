package com.infiniteloop.springprojectmongodb.model;

import org.springframework.data.annotation.Id;
import org.springframework.data.mongodb.core.mapping.Document;

@Document(collection = "players") // Specify the MongoDB collection name
public class Player {
    @Id
    String id; // Change the type to String for MongoDB ObjectId

    String name;

    // Constructors, getters, and setters

    public Player(String name) {
        this.name = name;
    }

    // No-argument constructor required by Spring Data MongoDB
    public Player() {
    }

    // Getter and setter for id
    public String getId() {
        return id;
    }

    public void setId(String id) {
        this.id = id;
    }

    // Getter and setter for name
    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }
}


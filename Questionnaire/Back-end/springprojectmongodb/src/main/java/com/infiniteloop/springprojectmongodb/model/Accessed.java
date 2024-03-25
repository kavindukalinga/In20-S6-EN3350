package com.infiniteloop.springprojectmongodb.model;

import org.springframework.data.annotation.Id;
import org.springframework.data.mongodb.core.mapping.Document;

@Document(collection = "accessed") // Specify the MongoDB collection name
public class Accessed {
    @Id
    String id; // Change the type to String for MongoDB ObjectId

    Boolean isAnswered;

    // Constructors, getters, and setters

    public Accessed(Boolean isAnswered) {
        this.isAnswered = isAnswered;
    }

    // No-argument constructor required by Spring Data MongoDB
    public Accessed() {
    }

    // Getter and setter for id
    public String getId() {
        return id;
    }

    public void setId(String id) {
        this.id = id;
    }

    // Getter and setter for isAnswered
    public Boolean getIsAnswered() {
        return isAnswered;
    }

    public void setIsAnswered(Boolean isAnswered) {
        this.isAnswered = isAnswered;
    }
}

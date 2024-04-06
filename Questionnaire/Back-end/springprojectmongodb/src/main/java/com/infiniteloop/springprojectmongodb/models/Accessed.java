package com.infiniteloop.springprojectmongodb.models;

import org.springframework.data.annotation.Id;
import org.springframework.data.mongodb.core.mapping.Document;

// Annotated with @Document to specify MongoDB collection name
@Document(collection = "accessed")
public class Accessed {
    @Id
    String id; // MongoDB ObjectId, stored as String

    Boolean isAnswered;
    
    Integer score;

    String accessToken;

    // Constructors, getters, and setters

    // Constructor with isAnswered parameter
    public Accessed(Boolean isAnswered, String score, String accessToken) {
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

    // Getter and setter for score
    public Integer getScore() {
        return score;
    }

    public void setScore(Integer score) {
        this.score = score;
    }

    public String getAccessToken() {
        return accessToken;
    }

    public void setAccessToken(String accessToken) {
        this.accessToken = accessToken;
    }
}

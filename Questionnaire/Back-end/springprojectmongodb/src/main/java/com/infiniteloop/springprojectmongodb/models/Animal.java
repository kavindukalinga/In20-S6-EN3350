package com.infiniteloop.springprojectmongodb.models;

import org.springframework.data.annotation.Id;
import org.springframework.data.mongodb.core.mapping.Document;

import java.util.List;

@Document(collection = "animals")
public class Animal {

    @Id
    private String id;
    private String name;
    private List<Integer> health;

    public Animal() {}

    public Animal(String name, List<Integer> health) {
        this.name = name;
        this.health = health;
    }

    // Getters and setters
    public String getId() {
        return id;
    }

    public void setId(String id) {
        this.id = id;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public List<Integer> getHealth() {
        return health;
    }

    public void setHealth(List<Integer> health) {
        this.health = health;
    }
}
package com.infiniteloop.springprojectmongodb.models;

import org.springframework.data.annotation.Id;
import org.springframework.data.mongodb.core.mapping.Document;

import java.util.List;

@Document(collection = "foods")
public class Food {
    @Id
    private String id;
    private String name;
    private int count;
    private int healthPoints;
    private List<String> animalTypes;

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

    public int getCount() {
        return count;
    }

    public void setCount(int count) {
        this.count = count;
    }

    public int getHealthPoints() {
        return healthPoints;
    }

    public void setHealthPoints(int healthPoints) {
        this.healthPoints = healthPoints;
    }

    public List<String> getAnimalTypes() {
        return animalTypes;
    }

    public void setAnimalTypes(List<String> animalTypes) {
        this.animalTypes = animalTypes;
    }
}


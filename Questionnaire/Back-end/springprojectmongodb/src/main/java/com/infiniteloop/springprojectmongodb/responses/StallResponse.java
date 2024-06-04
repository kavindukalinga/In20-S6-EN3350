package com.infiniteloop.springprojectmongodb.responses;


public class StallResponse {
    private String name;
    private int level;

    // Constructors
    public StallResponse() {}

    public StallResponse(String name, int level) {
        this.name = name;
        this.level = level;
    }

    // Getters and setters
    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public int getLevel() {
        return level;
    }

    public void setLevel(int level) {
        this.level = level;
    }
}


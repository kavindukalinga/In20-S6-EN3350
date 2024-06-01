package com.infiniteloop.springprojectmongodb.responses;


public class FoodResponse {
    private String name;
    private int count;
    private int healthPoints;

    // Constructors, getters, and setters
    


    public FoodResponse() {
        //TODO Auto-generated constructor stub
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

    @Override
    public String toString() {
        return "FoodResponse{" +
                "name='" + name + '\'' +
                ", count=" + count +
                ", healthPoints=" + healthPoints +
                '}';
    }
}

package com.infiniteloop.springprojectmongodb.enums;

public class UpdateHealthRequest {
    private int index;
    private int delta;

    // Getters and setters
    public int getIndex() {
        return index;
    }

    public void setIndex(int index) {
        this.index = index;
    }

    public int getDelta() {
        return delta;
    }

    public void setDelta(int delta) {
        this.delta = delta;
    }
}


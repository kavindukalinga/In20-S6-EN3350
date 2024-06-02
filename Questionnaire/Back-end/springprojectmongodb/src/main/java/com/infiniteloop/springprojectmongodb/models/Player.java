package com.infiniteloop.springprojectmongodb.models;

import org.springframework.data.annotation.Id;
import org.springframework.data.mongodb.core.mapping.Document;

import java.time.LocalDateTime;

@Document(collection = "player")
public class Player {
    @Id
    private String id;
    private int coins;
    private LocalDateTime time;

    // Constructors, getters, setters, toString as defined previously

    public Player() {}

    public Player(int coins, LocalDateTime time) {
        this.coins = coins;
        this.time = time;
    }

    public String getId() {
        return id;
    }

    public void setId(String id) {
        this.id = id;
    }

    public int getCoins() {
        return coins;
    }

    public void setCoins(int coins) {
        this.coins = coins;
    }

    public LocalDateTime getTime() {
        return time;
    }

    public void setTime(LocalDateTime time) {
        this.time = time;
    }

    @Override
    public String toString() {
        return "Player{" +
                "id='" + id + '\'' +
                ", coins=" + coins +
                ", time=" + time +
                '}';
    }
}

package com.infiniteloop.springprojectmongodb.responses;

public class CoinsResponse {
    private int coins;

    public CoinsResponse(int coins) {
        this.coins = coins;
    }

    public int getCoins() {
        return coins;
    }

    public void setCoins(int coins) {
        this.coins = coins;
    }
}

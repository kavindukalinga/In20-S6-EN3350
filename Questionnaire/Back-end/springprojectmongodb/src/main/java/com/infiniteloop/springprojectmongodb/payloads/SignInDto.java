package com.infiniteloop.springprojectmongodb.payloads;

public record SignInDto(
    String login,
    String password) {
}


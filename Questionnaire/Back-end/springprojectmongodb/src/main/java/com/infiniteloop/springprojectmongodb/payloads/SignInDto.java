package com.infiniteloop.springprojectmongodb.payloads;

// Record representing data required for user sign-in
public record SignInDto(
    String login,     // User login username or email
    String password   // User password
) {
}

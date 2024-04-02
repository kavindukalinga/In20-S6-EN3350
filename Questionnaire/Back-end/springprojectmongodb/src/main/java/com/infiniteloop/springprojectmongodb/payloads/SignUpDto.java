package com.infiniteloop.springprojectmongodb.payloads;

import com.infiniteloop.springprojectmongodb.enums.UserRole;

// Record representing data required for user sign-up
public record SignUpDto(
    String login,     // User login username or email
    String password,  // User password
    UserRole role     // User role
) {
}

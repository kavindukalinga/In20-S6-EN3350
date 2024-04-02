package com.infiniteloop.springprojectmongodb.payloads;

import com.infiniteloop.springprojectmongodb.enums.UserRole;

public record SignUpDto(
    String login,
    String password,
    UserRole role) {
}

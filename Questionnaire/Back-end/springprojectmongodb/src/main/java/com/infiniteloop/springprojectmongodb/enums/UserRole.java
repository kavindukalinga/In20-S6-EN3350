package com.infiniteloop.springprojectmongodb.enums;

// Enum representing user roles
public enum UserRole {
    ADMIN("admin"), // Admin role
    USER("user"); // User role
  
    private String role;
  
    // Constructor to set role value
    UserRole(String role) {
      this.role = role;
    }
  
    // Method to get role value
    public String getValue() {
      return role;
    }
}

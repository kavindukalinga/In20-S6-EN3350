package com.infiniteloop.springprojectmongodb.exceptions;

import java.util.HashMap;
import java.util.List;
import java.util.Map;

import org.springframework.data.crossstore.ChangeSetPersister.NotFoundException;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.security.authentication.BadCredentialsException;
import org.springframework.security.authentication.InternalAuthenticationServiceException;
import org.springframework.web.bind.annotation.ExceptionHandler;
import org.springframework.web.bind.annotation.RestControllerAdvice;

// Global exception handler to handle various exceptions across the application
@RestControllerAdvice
public class GlobalExceptionHandler {

  // Exception handler for general exceptions
  @ExceptionHandler(Exception.class)
  public final ResponseEntity<Map<String, List<String>>> handleGeneralExceptions(Exception ex) {
    List<String> errors = List.of(ex.getMessage());
    return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR).body(errorsMap(errors));
  }

  // Exception handler for runtime exceptions
  @ExceptionHandler(RuntimeException.class)
  public final ResponseEntity<Map<String, List<String>>> handleRuntimeExceptions(RuntimeException ex) {
    List<String> errors = List.of(ex.getMessage());
    return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR).body(errorsMap(errors));
  }

  // Exception handler for invalid JWT exceptions
  @ExceptionHandler(InvalidJwtException.class)
  public ResponseEntity<Map<String, List<String>>> handleJwtErrors(InvalidJwtException ex) {
    List<String> errors = List.of(ex.getMessage());
    return ResponseEntity.status(HttpStatus.BAD_REQUEST).body(errorsMap(errors));
  }

  @ExceptionHandler(InternalAuthenticationServiceException.class)
  public ResponseEntity<Map<String, List<String>>> handleInternalAuthenticationServiceException(InternalAuthenticationServiceException ex) {
    List<String> errors = List.of("Invalid username or password");
    return ResponseEntity.status(HttpStatus.UNAUTHORIZED).body(errorsMap(errors));
  }

  // Exception handler for bad credentials exceptions
  @ExceptionHandler(BadCredentialsException.class)
  public ResponseEntity<Map<String, List<String>>> handleBadCredentialsError(BadCredentialsException ex) {
    List<String> errors = List.of("Invalid username or password");
    return ResponseEntity.status(HttpStatus.UNAUTHORIZED).body(errorsMap(errors));
  }


  // Exception handler for not found exceptions
  @ExceptionHandler(NotFoundException.class)
  public ResponseEntity<Map<String, List<String>>> handleNotFoundError(NotFoundException ex) {
    List<String> errors = List.of(ex.getMessage());
    return ResponseEntity.status(HttpStatus.NOT_FOUND).body(errorsMap(errors));
  }

  // Utility method to create error response map
  private Map<String, List<String>> errorsMap(List<String> errors) {
    Map<String, List<String>> errorResponse = new HashMap<>();
    errorResponse.put("errors", errors);
    return errorResponse;
  }
}

package com.infiniteloop.springprojectmongodb.exceptions;

import org.springframework.http.HttpStatus;
import org.springframework.security.core.AuthenticationException;
import org.springframework.web.bind.annotation.ResponseStatus;

// Exception class to represent invalid JWT exceptions
@ResponseStatus(HttpStatus.FORBIDDEN) // Return 403 Forbidden HTTP response for this exception
public class InvalidJwtException extends AuthenticationException {
  public InvalidJwtException(String ex) {
    super(ex);
  }
}

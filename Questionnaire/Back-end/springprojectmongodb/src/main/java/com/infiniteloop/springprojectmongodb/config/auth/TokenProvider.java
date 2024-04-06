package com.infiniteloop.springprojectmongodb.config.auth;

import com.auth0.jwt.JWT;
import com.auth0.jwt.algorithms.Algorithm;
import com.auth0.jwt.exceptions.JWTCreationException;
import com.auth0.jwt.exceptions.JWTVerificationException;
import com.infiniteloop.springprojectmongodb.models.User;


import org.springframework.security.authentication.BadCredentialsException;
import org.springframework.stereotype.Service;

import java.time.Instant;
import java.time.LocalDateTime;
import java.time.ZoneOffset;

@Service
public class TokenProvider {
  // Secret key for generating and validating tokens
  private String JWT_SECRET = "infiniteloopSecretKey";

  // Generate access token for user
  public String generateAccessToken(User user) throws JWTCreationException{
    try {
      Algorithm algorithm = Algorithm.HMAC256(JWT_SECRET);
      return JWT.create()
          .withSubject(user.getUsername())
          .withClaim("username", user.getUsername())
          .withExpiresAt(genAccessExpirationDate())
          .sign(algorithm);
    } catch (JWTCreationException exception) {
      throw new JWTCreationException("Error while generating token", exception);
    }
  }

  // Validate access token
  public String validateToken(String token) throws BadCredentialsException{
    try {
      Algorithm algorithm = Algorithm.HMAC256(JWT_SECRET);
      return JWT.require(algorithm)
          .build()
          .verify(token)
          .getSubject();
    } catch (JWTVerificationException exception) {
      throw new BadCredentialsException("Error while validating token");
    }
  }

  // Generate expiration date for access token
  private Instant genAccessExpirationDate() {
    return LocalDateTime.now().plusHours(5).toInstant(ZoneOffset.of("-03:00"));
  }
}

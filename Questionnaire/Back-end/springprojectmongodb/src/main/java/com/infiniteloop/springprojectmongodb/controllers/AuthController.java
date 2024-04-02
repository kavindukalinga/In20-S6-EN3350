package com.infiniteloop.springprojectmongodb.controllers;

import jakarta.validation.Valid;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.security.authentication.AuthenticationManager;
import org.springframework.security.authentication.UsernamePasswordAuthenticationToken;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;
import com.infiniteloop.springprojectmongodb.config.auth.TokenProvider;
import com.infiniteloop.springprojectmongodb.payloads.SignInDto;
import com.infiniteloop.springprojectmongodb.payloads.SignUpDto;
import com.infiniteloop.springprojectmongodb.payloads.JwtDto;
import com.infiniteloop.springprojectmongodb.models.User;
import com.infiniteloop.springprojectmongodb.services.AuthService;

@RestController
@RequestMapping("/auth")
public class AuthController {
  // Autowire necessary dependencies
  @Autowired
  private AuthenticationManager authenticationManager;
  @Autowired
  private AuthService service;
  @Autowired
  private TokenProvider tokenService;

  // Endpoint for user sign-up
  @PostMapping("/signup")
  public ResponseEntity<?> signUp(@RequestBody @Valid SignUpDto data) {
    service.signUp(data);
    return ResponseEntity.status(HttpStatus.CREATED).build();
  }

  // Endpoint for user sign-in
  @PostMapping("/signin")
  public ResponseEntity<JwtDto> signIn(@RequestBody @Valid SignInDto data) {
    // Authenticate user
    var usernamePassword = new UsernamePasswordAuthenticationToken(data.login(), data.password());
    var authUser = authenticationManager.authenticate(usernamePassword);
    // Generate access token
    var accessToken = tokenService.generateAccessToken((User) authUser.getPrincipal());
    return ResponseEntity.ok(new JwtDto(accessToken));
  }
}

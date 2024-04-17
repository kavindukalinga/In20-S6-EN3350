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
import org.springframework.web.bind.annotation.CrossOrigin;
import org.springframework.web.bind.annotation.GetMapping;
import com.infiniteloop.springprojectmongodb.config.auth.TokenProvider;
import com.infiniteloop.springprojectmongodb.payloads.SignInDto;
import com.infiniteloop.springprojectmongodb.payloads.SignUpDto;
import com.infiniteloop.springprojectmongodb.payloads.JwtDto;
import com.infiniteloop.springprojectmongodb.models.Accessed;
import com.infiniteloop.springprojectmongodb.models.User;
import com.infiniteloop.springprojectmongodb.services.AuthService;
import com.infiniteloop.springprojectmongodb.repositories.AccessedRepo;

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

  @Autowired
  private AccessedRepo accessedRepo;

  // Endpoint for user sign-up
  @CrossOrigin(origins = {"http://localhost:5173","http://localhost:5500"})
  @PostMapping("/signup")
  public ResponseEntity<?> signUp(@RequestBody @Valid SignUpDto data) {
        service.signUp(data);
    return ResponseEntity.status(HttpStatus.CREATED).body("{\"message\": \"User created\"}");
  }

  // Endpoint for user sign-in
  @CrossOrigin(origins = {"http://localhost:5173","http://localhost:5500"})
  @PostMapping("/signin")
  public ResponseEntity<JwtDto> signIn(@RequestBody @Valid SignInDto data) {
    // Authenticate user
    var usernamePassword = new UsernamePasswordAuthenticationToken(data.login(), data.password());
    var authUser = authenticationManager.authenticate(usernamePassword);
    // Generate access token
    var accessToken = tokenService.generateAccessToken((User) authUser.getPrincipal());

    // Save access token in MongoDB
    var accessed = accessedRepo.findByLogin(data.login()).orElseThrow();
    accessed.setAccessToken(accessToken);
    accessedRepo.save(accessed);

    return ResponseEntity.ok(new JwtDto(accessToken));
  }



  @CrossOrigin(origins = {"http://localhost:5173","http://127.0.0.1:5500"})
  @GetMapping(value = "/accessToken", produces = "application/json")
  public ResponseEntity<String> getAccessToken() {
    try {
      Accessed accessed = accessedRepo.findById("1").orElse(null);

      if (accessed != null) {
        String result = accessed.getAccessToken();
        return ResponseEntity.ok("{\"accessToken\": \"" + result + "\"}");
      } else {
        return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR).body("{\"error\": \"No resource Found\"}");
      }
    } catch (Exception e) {
      return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR).body("{\"error\": \"An error occurred while processing the request\"}");
    }
  }   
}


  

package com.infiniteloop.springprojectmongodb.config;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.springframework.http.HttpMethod;
import org.springframework.security.authentication.AuthenticationManager;
import org.springframework.security.config.annotation.authentication.configuration.AuthenticationConfiguration;
import org.springframework.security.config.annotation.web.builders.HttpSecurity;
import org.springframework.security.config.annotation.web.configuration.EnableWebSecurity;
import org.springframework.security.config.http.SessionCreationPolicy;
import org.springframework.security.crypto.bcrypt.BCryptPasswordEncoder;
import org.springframework.security.crypto.password.PasswordEncoder;
import org.springframework.security.web.SecurityFilterChain;
import org.springframework.security.web.authentication.UsernamePasswordAuthenticationFilter;

import com.infiniteloop.springprojectmongodb.config.auth.SecurityFilter;

@Configuration
@EnableWebSecurity
public class AuthConfig {
  // Autowire SecurityFilter
  @Autowired
  SecurityFilter securityFilter;

  // Define Security Filter Chain
  @Bean
  SecurityFilterChain securityFilterChain(HttpSecurity httpSecurity) throws Exception {
    return httpSecurity
        .csrf(csrf -> csrf.disable())
        .sessionManagement(session -> session.sessionCreationPolicy(SessionCreationPolicy.STATELESS))
        .authorizeHttpRequests(authorize -> authorize
            .requestMatchers(HttpMethod.POST, "/auth/*").permitAll()
            .requestMatchers(HttpMethod.GET, "/auth/*").permitAll()
            .requestMatchers(HttpMethod.POST, "/api/*").permitAll()
            .requestMatchers(HttpMethod.GET, "/api/players/*").permitAll()
            .requestMatchers(HttpMethod.PUT, "/api/players/*").permitAll()
            .requestMatchers(HttpMethod.GET, "/api/animals/*").permitAll()
            .requestMatchers(HttpMethod.POST, "/api/animals/*").permitAll()
            .requestMatchers(HttpMethod.PUT, "/api/animals/*").permitAll()
            .requestMatchers(HttpMethod.GET, "/api/foods/*").permitAll()
            .requestMatchers(HttpMethod.POST, "/api/foods/*").permitAll()
            .requestMatchers(HttpMethod.PUT, "/api/foods/*").permitAll()
            .requestMatchers(HttpMethod.GET, "/api/stalls/*").permitAll()
            .requestMatchers(HttpMethod.POST, "/api/stalls/*").permitAll()
            .requestMatchers(HttpMethod.PUT, "/api/stalls/*").permitAll()
            .requestMatchers(HttpMethod.GET, "/api/animals/healths/*").permitAll()
            .requestMatchers(HttpMethod.POST, "/api/animals/healths/*").permitAll()
            .requestMatchers(HttpMethod.PUT, "/api/animals/healths/*").permitAll()
            .requestMatchers(HttpMethod.GET, "/accessed/isAnswered/*").permitAll()
            .requestMatchers(HttpMethod.GET, "/accessed/finalscore/*").permitAll()
            .anyRequest().authenticated())
        .addFilterBefore(securityFilter, UsernamePasswordAuthenticationFilter.class)
        .build();
  }

  // Define Authentication Manager
  @Bean
  AuthenticationManager authenticationManager(AuthenticationConfiguration authenticationConfiguration)
      throws Exception {
    return authenticationConfiguration.getAuthenticationManager();
  }

  // Define Password Encoder
  @Bean
  PasswordEncoder passwordEncoder() {
    return new BCryptPasswordEncoder();
  }
  
  

  
}

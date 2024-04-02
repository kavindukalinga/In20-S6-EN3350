package com.infiniteloop.springprojectmongodb.config.auth;

import jakarta.servlet.FilterChain;
import jakarta.servlet.ServletException;
import jakarta.servlet.http.HttpServletRequest;
import jakarta.servlet.http.HttpServletResponse;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.security.authentication.UsernamePasswordAuthenticationToken;
import org.springframework.security.core.context.SecurityContextHolder;
import org.springframework.stereotype.Component;
import org.springframework.web.filter.OncePerRequestFilter;
import com.infiniteloop.springprojectmongodb.repositories.UserRepo;
import java.io.IOException;

@Component
public class SecurityFilter extends OncePerRequestFilter {

  @Autowired
  TokenProvider tokenService;
  @Autowired
  UserRepo userRepository;

  @Override
  protected void doFilterInternal(HttpServletRequest request, HttpServletResponse response, FilterChain filterChain)
      throws ServletException, IOException {
    // Recover token from request
    var token = this.recoverToken(request);
    if (token != null) {
      // Validate token and retrieve login
      var login = tokenService.validateToken(token);
      // Find user by login
      var user = userRepository.findByLogin(login);
      // Create authentication token
      var authentication = new UsernamePasswordAuthenticationToken(user, null, user.getAuthorities());
      // Set authentication in SecurityContextHolder
      SecurityContextHolder.getContext().setAuthentication(authentication);
    }
    // Continue with the filter chain
    filterChain.doFilter(request, response);
  }

  // Method to recover token from request header
  private String recoverToken(HttpServletRequest request) {
    var authHeader = request.getHeader("Authorization");
    if (authHeader == null)
      return null;
    return authHeader.replace("Bearer ", "");
  }
}

package com.infiniteloop.springproject.controller;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.RestController;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.dao.DataAccessException; // Import DataAccessException

import com.infiniteloop.springproject.repository.AccessedRepo;
import com.infiniteloop.springproject.model.Accessed;

@RestController
@RequestMapping(value = "/accessed")
public class AccessedController {

    @Autowired
    AccessedRepo accessedRepo;

    @PostMapping("/add")
    Accessed addAccessed(@RequestBody Accessed accessed) {
        try {
            accessedRepo.save(accessed);
            return accessed;
        } catch (DataAccessException e) {
            return null; // Return null or throw a custom exception
        }
    }

    @GetMapping(value = "/{id}/isAnswered", produces = "application/json")
    public ResponseEntity<String> isAnswered(@PathVariable Integer id) {
        try {
            Accessed accessed = accessedRepo.findById(id).orElse(null);

            if (accessed != null) {
                boolean result = accessed.getIsAnswered();
                return ResponseEntity.ok("{\"isAnswered\": " + result + "}");
            } else {
                return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR).body("{\"error\": \"No resource Found\"}");
            }
        } catch (DataAccessException e) {
            
            return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR).body("{\"error\": \"Database access error\"}");
        }
    }

    @GetMapping(value = "/{id}/markAnswered", produces = "application/json" )
    public ResponseEntity<?> markAnswered(@PathVariable Integer id) {
        try {
            Accessed accessed = accessedRepo.findById(id).orElse(null);

            if (accessed != null) {
                accessed.setIsAnswered(true);
                accessedRepo.save(accessed);
                return ResponseEntity.ok().build();
            } else {
                return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR).body("{\"error\": \"No resource Found\"}");
            }
        } catch (DataAccessException e) {
            return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR).body("{\"error\": \"Database access error\"}");
        }
    }
}



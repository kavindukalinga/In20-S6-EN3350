package com.infiniteloop.springproject.controller;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.RestController;

import com.infiniteloop.springproject.repository.AccessedRepo;
import com.infiniteloop.springproject.model.Accessed;

import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;


@RestController
@RequestMapping(value = "/accessed")
public class AccessedController {

    @Autowired
    AccessedRepo accessedRepo;

    @PostMapping("/add")
    Accessed addAccessed(@RequestBody Accessed accessed) {
         accessedRepo.save(accessed);
         return accessed;
    }

    @GetMapping("/{id}/isAnswered")
    public ResponseEntity<Boolean> isAnswered(@PathVariable Integer id) {
        // Retrieve Accessed entity by ID
        Accessed accessed = accessedRepo.findById(id).orElse(null);
        
        // Check if the accessed entity exists
        if (accessed != null) {
            // Return the value of isAnswered field
            return ResponseEntity.ok(accessed.getIsAnswered());
        } else {
            // If the entity doesn't exist, return 404 Not Found
            return ResponseEntity.notFound().build();
        }
    }

    @GetMapping("/{id}/markAnswered")
    public ResponseEntity<?> markAnswered(@PathVariable Integer id) {
        // Retrieve Accessed entity by ID
        Accessed accessed = accessedRepo.findById(id).orElse(null);
        
        // Check if the accessed entity exists
        if (accessed != null) {
            // Update the isAnswered field to true
            accessed.setIsAnswered(true);
            accessedRepo.save(accessed);
            return ResponseEntity.ok().build();
        } else {
            // If the entity doesn't exist, return 404 Not Found
            return ResponseEntity.notFound().build();
        }
    }
    
    
}

package com.infiniteloop.springprojectmongodb.controllers;


import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.RestController;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;

import com.infiniteloop.springprojectmongodb.repositories.AccessedRepo;
import com.infiniteloop.springprojectmongodb.models.Accessed;

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

    @GetMapping(value = "/{id}/isAnswered", produces = "application/json")
    public ResponseEntity<String> isAnswered(@PathVariable String id) {
        Accessed accessed = accessedRepo.findById(id).orElse(null);

        if (accessed != null) {
            boolean result = accessed.getIsAnswered();
            return ResponseEntity.ok("{\"isAnswered\": " + result + "}");
        } else {
            return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR).body("{\"error\": \"No resource Found\"}");
        }
    }

    @PostMapping(value = "/{id}/markAnswered", produces = "application/json" )
    public ResponseEntity<?> markAnswered(@PathVariable String id) {
        Accessed accessed = accessedRepo.findById(id).orElse(null);

        if (accessed != null) {
            accessed.setIsAnswered(true);
            accessedRepo.save(accessed);
            return ResponseEntity.ok().build();
        } else {
            return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR).body("{\"error\": \"No resource Found\"}");
        }
    }
}



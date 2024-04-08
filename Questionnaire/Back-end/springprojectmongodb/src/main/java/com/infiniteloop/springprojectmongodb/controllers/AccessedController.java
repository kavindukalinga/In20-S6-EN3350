package com.infiniteloop.springprojectmongodb.controllers;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.RestController;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.CrossOrigin;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import com.infiniteloop.springprojectmongodb.repositories.AccessedRepo;
import com.infiniteloop.springprojectmongodb.models.Accessed;
import com.infiniteloop.springprojectmongodb.repositories.QuestionsRepo;

@RestController
@RequestMapping(value = "/accessed")
public class AccessedController {

    @Autowired
    AccessedRepo accessedRepo;

    @Autowired
    QuestionsRepo questionsRepo;

    // Endpoint to add Accessed record
    @PostMapping("/add")
    Accessed addAccessed(@RequestBody Accessed accessed) {
        accessedRepo.save(accessed);
        return accessed;
    }

    // Endpoint to check if Accessed record is answered
    @CrossOrigin(origins = "http://localhost:5173")
    @GetMapping(value = "/{id}/isAnswered", produces = "application/json")
    public ResponseEntity<String> isAnswered(@PathVariable String id) {
        try {
            Accessed accessed = accessedRepo.findById(id).orElse(null);

            if (accessed != null) {
                boolean result = accessed.getIsAnswered();
                return ResponseEntity.ok("{\"isAnswered\": " + result + "}");
            } else {
                return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR).body("{\"error\": \"No resource Found\"}");
            }
        } catch (Exception e) {
            return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR).body("{\"error\": \"An error occurred while processing the request\"}");
        }
    }

    

    // Endpoint to mark Accessed record as answered
    @PostMapping(value = "/{id}/markAnswered", produces = "application/json")
    public ResponseEntity<?> markAnswered(@PathVariable String id) {
        try {
            Accessed accessed = accessedRepo.findById(id).orElse(null);
            
            if (accessed != null) {
                accessed.setIsAnswered(true);
                accessedRepo.save(accessed);
                return ResponseEntity.ok().build();
            } else {
                return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR).body("{\"error\": \"No resource Found\"}");
            }
        } catch (Exception e) {
            return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR).body("{\"error\": \"An error occurred while processing the request\"}");
        }
    }

}

package com.infiniteloop.springproject.controller;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.data.crossstore.ChangeSetPersister.NotFoundException;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.RestController;

import com.infiniteloop.springproject.repository.QuestionsRepo;
import com.infiniteloop.springproject.model.Questions;  

import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.ExceptionHandler;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.PathVariable;
import java.util.Optional;
import java.util.List;

@RestController
@RequestMapping(value = "/questions")
public class QuestionsController {
    @Autowired
    QuestionsRepo questionRepo;

    @PostMapping("/add")
    Questions addQuestion(@RequestBody Questions question) {
         questionRepo.save(question);
         return question;
    }

    @GetMapping("/getall")
    List<Questions> getAllQuestions() {
        List<Questions> questions = questionRepo.findAll();
        return questions;
    }

    @GetMapping(value="/{id}/correctAnswer",produces="application/json") 
    ResponseEntity<String> getCorrectAnswerById(@PathVariable Integer id) {
        try {
            Optional<Questions> optionalQuestion = questionRepo.findById(id);
            if (optionalQuestion.isPresent()) {
                Questions question = optionalQuestion.get();
                question.setIsCompleted(true);
                questionRepo.save(question);
                
                String correctAnswer = question.getCorrectAnswer();
                return ResponseEntity.ok("{\"correctAnswer\": \"" + correctAnswer + "\"}");
            } else {
                String errorMessage = "{\"error\": \"Resource is not Found\"}";
                return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR).body(errorMessage);
            }
        } catch (Exception e) {
            // Handle specific exceptions
            String errorMessage = "{\"error\": \"Error occurred while retrieving correct answer\"}";
            return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR).body(errorMessage);
        }
    }


    @GetMapping(value = "/progress", produces = "application/json")
    ResponseEntity<String> getMaxCompletedQuestionId() {
        try {
            Integer maxCompletedId = questionRepo.findMaxCompletedQuestionId();
            if (maxCompletedId != null) {
                String jsonResponse = "{\"maxCompletedId\": " + maxCompletedId + "}";
                return ResponseEntity.ok(jsonResponse);
            } else {
                String errorMessage = "{\"message\": \"Not answered\"}";
                return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR).body(errorMessage);
            }
        } catch (Exception e) {
            // Handle specific exceptions related to finding max completed question id
            String errorMessage = "{\"error\": \"Error occurred while fetching max completed question id\"}";
            return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR).body(errorMessage);
        }
    }

    @ExceptionHandler(NotFoundException.class)
    public ResponseEntity<String> handleNotFoundException(NotFoundException ex) {
        String errorMessage = "{\"error\": \"Resource not found\"}";
        return ResponseEntity.status(HttpStatus.NOT_FOUND).body(errorMessage);
    }
}

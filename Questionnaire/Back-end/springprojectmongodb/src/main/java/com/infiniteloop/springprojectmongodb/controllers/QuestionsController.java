package com.infiniteloop.springprojectmongodb.controllers;


import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.RestController;
import com.infiniteloop.springprojectmongodb.repositories.QuestionsRepo;
import com.infiniteloop.springprojectmongodb.models.Questions;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.ExceptionHandler;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
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

    @GetMapping(value = "/{id}/correctAnswer", produces = "application/json")
    public ResponseEntity<String> getCorrectAnswerById(@PathVariable String id) {
        try {
            Questions question = questionRepo.findById(id).orElse(null);
            if (question != null) {
                question.setIsCompleted(true);
                questionRepo.save(question);
                String correctAnswer = question.getCorrectAnswer();
                return ResponseEntity.ok("{\"correctAnswer\": \"" + correctAnswer + "\"}");
            } else {
                String errorMessage = "{\"error\": \"Resource is not Found\"}";
                return ResponseEntity.status(HttpStatus.NOT_FOUND).body(errorMessage);
            }
        } catch (Exception e) {
            String errorMessage = "{\"error\": \"Error occurred while retrieving correct answer\"}";
            return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR).body(errorMessage);
        }
    }

    @GetMapping("/progress")
    ResponseEntity<String> getMaxCompletedQuestionId() {
        try {
            Questions maxCompletedQuestion = questionRepo.findFirstByIsCompletedOrderByQuestionIdDesc(true);
            if (maxCompletedQuestion != null) {
                String jsonResponse = "{\"maxCompletedId\": " + maxCompletedQuestion.getQuestionId() + "}";
                return ResponseEntity.ok(jsonResponse);
            } else {
                return ResponseEntity.noContent().build();
            }
        } catch (Exception e) {
            String errorMessage = "{\"error\": \"Error occurred while fetching max completed question id\"}";
            return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR).body(errorMessage);
        }
    }

    @ExceptionHandler(Exception.class)
    public ResponseEntity<String> handleException(Exception ex) {
        String errorMessage = "{\"error\": \"" + ex.getMessage() + "\"}";
        return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR).body(errorMessage);
    }
}


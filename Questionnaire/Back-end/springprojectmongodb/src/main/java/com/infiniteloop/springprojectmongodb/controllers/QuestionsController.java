package com.infiniteloop.springprojectmongodb.controllers;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.RestController;
import com.infiniteloop.springprojectmongodb.repositories.QuestionsRepo;
import com.fasterxml.jackson.databind.ObjectMapper;
import com.infiniteloop.springprojectmongodb.models.Questions;
import org.springframework.web.bind.annotation.ExceptionHandler;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import java.util.List;
import java.util.Map;

@RestController
public class QuestionsController {
    @Autowired
    QuestionsRepo questionRepo;

    // Endpoint to add a question
    @PostMapping("/add")
    Questions addQuestion(@RequestBody Questions question) {
         questionRepo.save(question);
         return question;
    }

    // Endpoint to get all questions
    @GetMapping("/getall-questions")
    public ResponseEntity<?> getAllQuestions() {
        Questions maxCompletedQuestion = questionRepo.findFirstByIsCompletedOrderByQuestionIdDesc(true);
        if (maxCompletedQuestion.getQuestionId() == "10"){
            List<Questions> questions = questionRepo.findAll();
            return ResponseEntity.ok(questions);
        }else{
            return ResponseEntity.ok("{\"error\": \"complete the questionnaire first!!!\"}");
        }
        
    }

    // Endpoint to get correct answer by question id
    @GetMapping(value = "/get-answer/{id}/{answer}", produces = "application/json")
    public ResponseEntity<?> getCorrectAnswerById(@PathVariable String id, @PathVariable String answer) {
                try {
                    Questions question = questionRepo.findById(id).orElse(null);
                    if (question != null) {
                        question.setIsCompleted(true);
                        question.setPlayerAnswer(answer);
                        questionRepo.save(question);
                        String correctAnswer = question.getCorrectAnswer();
                        Map<String, String> specificFeedback = question.getSpecificFeedback();
                        ObjectMapper objectMapper = new ObjectMapper();
                        String jsonString = objectMapper.writeValueAsString(specificFeedback);
                        String generalFeedback = question.getGeneralFeedback();
                        return ResponseEntity.ok("{\"correctAnswer\": \"" + correctAnswer + "\", \"specificFeedback\": " + jsonString + ", \"generalFeedback\": \"" + generalFeedback + "\"}");
                    } else {
                        String errorMessage = "{\"error\": \"Resource is not Found\"}";
                        return ResponseEntity.status(HttpStatus.NOT_FOUND).body(errorMessage);
                    }
                } catch (Exception e) {
            String errorMessage = "{\"error\": \"Error occurred while retrieving correct answer\"}";
            return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR).body(errorMessage);
        }
    }

    // Endpoint to get maximum completed question id
    @GetMapping("/get-current-question")
    ResponseEntity<String> getMaxCompletedQuestionId() {
        try {
            Questions maxCompletedQuestion = questionRepo.findFirstByIsCompletedOrderByQuestionIdDesc(true);
            if (maxCompletedQuestion != null) {
                String jsonResponse = "{\"available_question\": " + (Integer.parseInt(maxCompletedQuestion.getQuestionId()) + 1) + "}";
                return ResponseEntity.ok(jsonResponse);
            } else {
                return ResponseEntity.ok("{\"available_question\": 1}");
            }
        } catch (Exception e) {
            String errorMessage = "{\"error\": \"Error occurred while fetching max completed question id\"}";
            return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR).body(errorMessage);
        }
    }

    // Exception handler for handling all exceptions
    @ExceptionHandler(Exception.class)
    public ResponseEntity<String> handleException(Exception ex) {
        String errorMessage = "{\"error\": \"" + ex.getMessage() + "\"}";
        return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR).body(errorMessage);
    }
}

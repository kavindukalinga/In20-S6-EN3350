package com.infiniteloop.springproject.controller;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.RestController;

import com.infiniteloop.springproject.repository.QuestionsRepo;
import com.infiniteloop.springproject.model.Questions;  

import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.PathVariable; // Import added for PathVariable
import java.util.Optional; // Import added for Optional
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

    @GetMapping("/{id}/correctAnswer") 
    ResponseEntity<String> getCorrectAnswerById(@PathVariable Integer id) {
        Optional<Questions> optionalQuestion = questionRepo.findById(id);
        if (optionalQuestion.isPresent()) {
            Questions question = optionalQuestion.get();
            question.setIsCompleted(true); // Update isCompleted to true
            questionRepo.save(question); // Save the updated entity
            
            String correctAnswer = question.getCorrectAnswer();
            return ResponseEntity.ok("{\"correctAnswer\": \"" + correctAnswer + "\"}");
        } else {
            return ResponseEntity.notFound().build();
        }
    }


    @GetMapping("/progress")
    ResponseEntity<Integer> getMaxCompletedQuestionId() {
        Integer maxCompletedId = questionRepo.findMaxCompletedQuestionId();
        if (maxCompletedId != null) {
            return ResponseEntity.ok(maxCompletedId);
        } else {
            return ResponseEntity.notFound().build();
        }
    }
}

package com.infiniteloop.springprojectmongodb.controllers;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.RestController;
import com.infiniteloop.springprojectmongodb.repositories.QuestionsRepo;

import jakarta.servlet.http.HttpServletRequest;

import com.infiniteloop.springprojectmongodb.repositories.AccessedRepo;
import com.fasterxml.jackson.databind.JsonNode;
import com.fasterxml.jackson.databind.ObjectMapper;
import com.infiniteloop.springprojectmongodb.payloads.QuestionsDto;
import com.infiniteloop.springprojectmongodb.models.Accessed;
import com.infiniteloop.springprojectmongodb.models.Questions;

import org.springframework.web.bind.annotation.CrossOrigin;
import org.springframework.web.bind.annotation.ExceptionHandler;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;

import java.util.Base64;
import java.util.List;
import java.util.Map;
import java.util.stream.Collectors;




@RestController
public class QuestionsController {
    @Autowired
    QuestionsRepo questionRepo;

    @Autowired
    AccessedRepo accessedRepo;

    // Endpoint to add a question
    @PostMapping("/add-question")
    Questions addQuestion(@RequestBody Questions question) {
         questionRepo.save(question);
         return question;
    }
    

    private static String decode(String encodedString) {
        return new String(Base64.getUrlDecoder().decode(encodedString));
    }

    // Endpoint to get all questions with answers
    @CrossOrigin(origins = "http://localhost:5173")
    @GetMapping("/getall-questions")
    public ResponseEntity<?> getAllQuestions(HttpServletRequest request) {
        try {
            String authHeader = request.getHeader("Authorization");
            String token = authHeader.substring(7);
            String[] parts = token.split("\\.");
            String payload = decode(parts[1]);
            ObjectMapper mapper = new ObjectMapper();
            JsonNode payloadNode = mapper.readTree(payload);
            String login = payloadNode.get("username").asText();

            Questions maxCompletedQuestion = questionRepo.findFirstByIsCompletedOrderBySortKeyDesc(true);
            if (Integer.parseInt(maxCompletedQuestion.getQuestionId()) == 10) {
                List<Questions> questions = questionRepo.findAll();
                List<QuestionsDto> questionDTOs = questions.stream().map(question -> {
                    QuestionsDto dto = new QuestionsDto();
                    dto.setQuestionId(question.getQuestionId());
                    dto.setQuestion(question.getQuestion());
                    dto.setCorrectAnswer(question.getAnswers().get(question.getCorrectAnswer()));
                    dto.setGeneralFeedback(question.getGeneralFeedback());
                    String playerSpecificFeedback = question.getSpecificFeedback().get(question.getPlayerAnswer());
                    dto.setSpecificFeedback(playerSpecificFeedback);
                    dto.setPlayerAnswer(question.getAnswers().get(question.getPlayerAnswer()));
                    if(question.getCorrectAnswer().equals(question.getPlayerAnswer())){
                        dto.setIsCorrect(true);
                    }else{
                        dto.setIsCorrect(false);
                    }
                    return dto;
                }).collect(Collectors.toList());
                Accessed accessed = accessedRepo.findByLogin(login).orElse(null);
                if (accessed != null) {
                    if(accessed.getIsAnswered() == false){
                        accessed.setIsAnswered(true);
                        accessedRepo.save(accessed);
                  
                }}
                return ResponseEntity.ok(questionDTOs);
            } else {
                return ResponseEntity.ok("{\"error\": \"complete the questionnaire first!!!\"}");
            }
        } catch (Exception e) {
            return ResponseEntity.ok("{\"error\": \"complete the questionnaire first!!!\"}");
        }
    }

    // Endpoint to get correct answer by question id
    @CrossOrigin(origins = "http://localhost:5173")
    @GetMapping(value = "/get-answer/{id}/{answer}", produces = "application/json")
    public ResponseEntity<?> getCorrectAnswerById(@PathVariable String id, @PathVariable String answer) {
                try {
                    Questions question = questionRepo.findById(id).orElse(null);
                    if (question != null) {
                        if (question.getIsCompleted() == true) {
                            return ResponseEntity.ok("{\"error\": \"Question already answered\"}");
                        }else{
                            int available_question;
                            Questions maxCompletedQuestion = questionRepo.findFirstByIsCompletedOrderBySortKeyDesc(true);
                            if (maxCompletedQuestion ==  null) {
                                  available_question = 1;
                            } else {
                                  available_question = Integer.parseInt(maxCompletedQuestion.getQuestionId()) + 1;
                            }
                            if (available_question != Integer.parseInt(id)) {
                                return ResponseEntity.ok("{\"error\": \"Answer the questions in order\"}");
                            }else{
                                question.setIsCompleted(true);
                                question.setPlayerAnswer(answer);
                                questionRepo.save(question);
                                String correctAnswer = question.getCorrectAnswer();
                                String playerAnswer = question.getPlayerAnswer();
                                if (correctAnswer.equals(playerAnswer)) {
                                    question.setScore(1);
                                } else {
                                    question.setScore(0);
                                }
                                questionRepo.save(question);
                                Map<String, String> specificFeedback = question.getSpecificFeedback();
                                String playerSpecificFeedback = specificFeedback.get(answer);
                                ObjectMapper objectMapper = new ObjectMapper();
                                String jsonString = objectMapper.writeValueAsString(playerSpecificFeedback);
                                String generalFeedback = question.getGeneralFeedback();
                                return ResponseEntity.ok("{\"correctAnswer\": \"" + correctAnswer + "\", \"specificFeedback\": " + jsonString + ", \"generalFeedback\": \"" + generalFeedback + "\"}");
                            }
                        }
                    } else {
                        String errorMessage = "{\"error\": \"Resource is not Found\"}";
                        return ResponseEntity.status(HttpStatus.NOT_FOUND).body(errorMessage);
                    }
                } catch (Exception e) {
            String errorMessage = "{\"error\": \"Error occurred while retrieving correct answer\"}";
            return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR).body(errorMessage);
        }
    }


    // Endpoint to get score
    @CrossOrigin(origins = {"http://localhost:5173","http://localhost:5500"})
    @GetMapping(value = "/get-score", produces = "application/json")
    public ResponseEntity<?> getScore(HttpServletRequest request)  {
        try {
            String authHeader = request.getHeader("Authorization");
            String token = authHeader.substring(7);
            String[] parts = token.split("\\.");
            String payload = decode(parts[1]);
            ObjectMapper mapper = new ObjectMapper();
            JsonNode payloadNode = mapper.readTree(payload);
            String login = payloadNode.get("username").asText();
            Questions maxCompletedQuestion = questionRepo.findFirstByIsCompletedOrderBySortKeyDesc(true);
            if (maxCompletedQuestion == null) {
                return ResponseEntity.ok("{\"score\": 0}");
            }else if (Integer.parseInt(maxCompletedQuestion.getQuestionId()) == 10) {
                int score = 0;
                for (int i = 1; i <= 10; i++) {
                    Questions question = questionRepo.findById(String.valueOf(i)).orElse(null);
                    if (question != null) {
                        score += question.getScore();
                    }
                }
                Accessed accessed = accessedRepo.findByLogin(login).orElse(null);
                if (accessed != null) {
                    if (accessed.getIsAnswered() == false){
                    accessed.setScore(score);
                    accessedRepo.save(accessed);
                    }
                }
                return ResponseEntity.ok("{\"score\": " + score + "}");
                
            }
            else{
            Integer maxCompltedQuestionId = Integer.parseInt(maxCompletedQuestion.getQuestionId());
            int score = 0;
            for (int i = 1; i <= maxCompltedQuestionId; i++) {
                Questions question = questionRepo.findById(String.valueOf(i)).orElse(null);
                if (question != null) {
                    score += question.getScore();
                }
            }
            return ResponseEntity.ok("{\"score\": " + score + "}");
        }
        } catch (Exception e) {
            String errorMessage = "{\"error\": \"Error occurred while retrieving score\"}";
            return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR).body(errorMessage);
        }
    }


    // Endpoint to get question by id
    @CrossOrigin(origins = "http://localhost:5173")
    @GetMapping(value = "/get-question/{id}", produces = "application/json")
    public ResponseEntity<?> getQuestionById(@PathVariable String id) {
                try {
                    Questions question = questionRepo.findById(id).orElse(null);
                    if (question != null) {
                        if (question.getIsCompleted() == true) {
                            return ResponseEntity.ok("{\"error\": \"Question already answered\"}");
                        }else{
                            int available_question;
                            Questions maxCompletedQuestion = questionRepo.findFirstByIsCompletedOrderBySortKeyDesc(true);
                            if (maxCompletedQuestion ==  null) {
                                  available_question = 1;
                            } else {
                                  available_question = Integer.parseInt(maxCompletedQuestion.getQuestionId()) + 1;
                            }
                            if (available_question != Integer.parseInt(id)) {
                                return ResponseEntity.ok("{\"error\": \"Get the questions in order\"}");
                            }else{
                                String Question = question.getQuestion();
                                Map<String, String> answers = question.getAnswers();
                                ObjectMapper objectMapper = new ObjectMapper();
                                String jsonString = objectMapper.writeValueAsString(answers);
                                return ResponseEntity.ok("{\"question\": \"" + Question + "\", \"answers\": " + jsonString +"}");
                            }
                        }
                    } else {
                        String errorMessage = "{\"error\": \"Resource is not Found\"}";
                        return ResponseEntity.status(HttpStatus.NOT_FOUND).body(errorMessage);
                    }
                } catch (Exception e) {
            String errorMessage = "{\"error\": \"Error occurred while retrieving correct answer\"}";
            return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR).body(errorMessage);
        }
    }


    // Endpoint to get available question id
    @CrossOrigin(origins = "http://localhost:5173")
    @GetMapping("/get-current-question")
    ResponseEntity<String> getMaxCompletedQuestionId() {
        try {
            Questions maxCompletedQuestion = questionRepo.findFirstByIsCompletedOrderBySortKeyDesc(true);
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

    // Endpoint to reset the questionnaire
    @CrossOrigin(origins = "http://localhost:5173")
    @GetMapping("/reset")
    public ResponseEntity<String> reset() {
        try {
            List<Questions> questions = questionRepo.findAll();
            for (Questions question : questions) {
                question.setIsCompleted(false);
                question.setPlayerAnswer(null);
                question.setScore(0);
                questionRepo.save(question);
            }
        } catch (Exception e) {
            return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR).body("{\"error\": \"An error occurred while processing the request\"}");
        }
        return ResponseEntity.ok("{\"message\": \"Questionnaire reset successfully\"}");
    }

    @GetMapping("/reset/{login}")
    public ResponseEntity<String> reset(@PathVariable String login) {
        try {
            List<Questions> questions = questionRepo.findAll();
            for (Questions question : questions) {
                question.setIsCompleted(false);
                question.setPlayerAnswer(null);
                question.setScore(0);
                questionRepo.save(question);
            }
            Accessed accessed = accessedRepo.findByLogin(login).orElse(null);
            if (accessed != null) {
                accessed.setIsAnswered(false);
                accessed.setScore(0);
                accessed.setAccessToken(null);
                accessedRepo.save(accessed);
            }}
        catch (Exception e) {
                return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR).body("{\"error\": \"An error occurred while processing the request\"}");
            }
            return ResponseEntity.ok("{\"message\": \"Questionnaire reset successfully\"}");
        }
    

    // Exception handler for handling all exceptions
    @ExceptionHandler(Exception.class)
    public ResponseEntity<String> handleException(Exception ex) {
        String errorMessage = "{\"error\": \"" + ex.getMessage() + "\"}";
        return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR).body(errorMessage);
    }
}

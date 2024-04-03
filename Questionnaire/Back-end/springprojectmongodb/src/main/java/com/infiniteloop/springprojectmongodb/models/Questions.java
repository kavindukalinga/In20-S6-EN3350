package com.infiniteloop.springprojectmongodb.models;

import java.util.Map;

import org.springframework.data.annotation.Id;
import org.springframework.data.mongodb.core.mapping.Document;

@Document(collection = "questionnaire") // Specify the MongoDB collection name
public class Questions {
    @Id
    private String questionId; // Change the type to String for MongoDB ObjectId

    private String correctAnswer;
    
    private Boolean isCompleted;

    private String question;
    
    private Integer sortKey; 
    
    private Map<String, String> answers;

    private String generalFeedback;

    private Map<String, String> specificFeedback;

    private String playerAnswer;

    // Constructors, getters, and setters

    public Questions(String questionId, String correctAnswer, Boolean isCompleted, String question, Map<String, String> answers, String generalFeedback, Map<String, String> specificFeedback, String playerAnswer, Integer sortKey) {
        this.questionId = questionId;
        this.correctAnswer = correctAnswer;
        this.isCompleted = isCompleted;
        this.question = question;
        this.answers = answers;
        this.generalFeedback = generalFeedback;
        this.specificFeedback = specificFeedback;
        this.playerAnswer = playerAnswer;
        this.sortKey = sortKey; // Set sortKey to ASCII value of the first character of questionId
        
    }

    // No-argument constructor required by Spring Data MongoDB
    public Questions() {
    }

    // Getter and setter for questionId
    public String getQuestionId() {
        return questionId;
    }

    public void setQuestionId(String questionId) {
        this.questionId = questionId;
        if (questionId != null && !questionId.isEmpty()) {
            this.sortKey = (int) questionId.charAt(0); // Update sortKey when questionId is set
        }
    }

    // Getter and setter for correctAnswer
    public String getCorrectAnswer() {
        return correctAnswer;
    }

    public void setCorrectAnswer(String correctAnswer) {
        this.correctAnswer = correctAnswer;
    }
    
    // Getter and setter for isCompleted
    public Boolean getIsCompleted() {
        return isCompleted;
    }

    public void setIsCompleted(Boolean isCompleted) {
        this.isCompleted = isCompleted;
    }

    // Getter and setter for question
    public String getQuestion() {
        return question;
    }

    public void setQuestion(String question) {
        this.question = question;
    }

    // Getter and setter for answers
    public Map<String, String> getAnswers() {
        return answers;
    }

    public void setAnswers(Map<String, String> answers) {
        this.answers = answers;
    }

    // Getter and setter for generalFeedback
    public String getGeneralFeedback() {
        return generalFeedback;
    }

    public void setGeneralFeedback(String generalFeedback) {
        this.generalFeedback = generalFeedback;
    }

    // Getter and setter for specificFeedback
    public Map<String, String> getSpecificFeedback() {
        return specificFeedback;
    }

    public void setSpecificFeedback(Map<String, String> specificFeedback) {
        this.specificFeedback = specificFeedback;
    }

    // Getter and setter for playerAnswer
    public String getPlayerAnswer() {
        return playerAnswer;
    }

    public void setPlayerAnswer(String playerAnswer) {
        this.playerAnswer = playerAnswer;
    }

    // Getter and setter for sortKey
    public Integer getSortKey() {
        return sortKey;
    }

    public void setSortKey(Integer sortKey) {
        this.sortKey = sortKey;
    }
}

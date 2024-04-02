package com.infiniteloop.springprojectmongodb.models;

import java.util.Map;

import org.springframework.data.annotation.Id;
import org.springframework.data.mongodb.core.mapping.Document;

@Document(collection = "questionnaire") // Specify the MongoDB collection name
public class Questions {
    @Id
    String questionId; // Change the type to String for MongoDB ObjectId

    String correctAnswer;

    Boolean isCompleted;

    String question; 

    Map<String, String> answers;

    String generalFeedback;

    Map<String, String> specificFeedback;

    String playerAnswer;

    // Constructors, getters, and setters

    public Questions(String questionId, String correctAnswer, Boolean isCompleted, String question, Map<String, String> answers, String generalFeedback, Map<String, String> specificFeedback, String playerAnswer) {
        this.questionId = questionId;
        this.correctAnswer = correctAnswer;
        this.isCompleted = isCompleted;
        this.question = question;
        this.answers = answers;
        this.generalFeedback = generalFeedback;
        this.specificFeedback = specificFeedback;
        this.playerAnswer = playerAnswer;
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
}

package com.infiniteloop.springprojectmongodb.model;

import org.springframework.data.annotation.Id;
import org.springframework.data.mongodb.core.mapping.Document;

@Document(collection = "questions") // Specify the MongoDB collection name
public class Questions {
    @Id
    String questionId; // Change the type to String for MongoDB ObjectId

    String correctAnswer;

    Boolean isCompleted;

    // Constructors, getters, and setters

    public Questions(String correctAnswer, Boolean isCompleted) {
        this.correctAnswer = correctAnswer;
        this.isCompleted = isCompleted;
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
}

package com.infiniteloop.springprojectmongodb.payloads;

import java.util.Map;

public class QuestionsDto {
    private String questionId; // Change the type to String for MongoDB ObjectId

    private String correctAnswer;

    private String question; // Corrected the field name

    private Map<String, String> answers;

    private String generalFeedback;

    private String specificFeedback;

    private String playerAnswer;
    
    // Add other fields as needed
    
    // Getters and setters
    public String getQuestionId() {
        return questionId;
    }

    public void setQuestionId(String questionId) {
        this.questionId = questionId;
    }

    public String getQuestion() { // Corrected method name to match field
        return question;
    }

    public void setQuestion(String question) { // Corrected method name to match field
        this.question = question;
    }

    public String getCorrectAnswer() {
        return correctAnswer;
    }

    public void setCorrectAnswer(String correctAnswer) {
        this.correctAnswer = correctAnswer;
    }

    public Map<String, String> getAnswers() {
        return answers;
    }

    public void setAnswers(Map<String, String> answers) {
        this.answers = answers;
    }

    public String getGeneralFeedback() {
        return generalFeedback;
    }

    public void setGeneralFeedback(String generalFeedback) {
        this.generalFeedback = generalFeedback;
    }

    public String getSpecificFeedback() {
        return specificFeedback;
    }

    public void setSpecificFeedback(String specificFeedback) {
        this.specificFeedback = specificFeedback;
    }

    public String getPlayerAnswer() {
        return playerAnswer;
    }

    public void setPlayerAnswer(String playerAnswer) {
        this.playerAnswer = playerAnswer;
    }
}

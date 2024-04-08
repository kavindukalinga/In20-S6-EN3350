package com.infiniteloop.springprojectmongodb.payloads;


public class QuestionsDto {
    private String questionId; // Change the type to String for MongoDB ObjectId

    private String correctAnswer;

    private String question; // Corrected the field name

    private String generalFeedback;

    private String specificFeedback;

    private String playerAnswer;

    private Boolean isCorrect;
    
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

    public Boolean getIsCorrect() {
        return isCorrect;
    }

    public void setIsCorrect(Boolean isCorrect) {
        this.isCorrect = isCorrect;
    }
}

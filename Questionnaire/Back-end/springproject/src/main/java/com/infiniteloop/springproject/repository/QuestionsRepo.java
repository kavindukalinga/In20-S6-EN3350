package com.infiniteloop.springproject.repository;
import com.infiniteloop.springproject.model.Questions;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;


public interface QuestionsRepo extends JpaRepository<Questions, Integer> {
    @Query(value = "SELECT MAX(questionId) FROM Questions WHERE isCompleted = true")
    Integer findMaxCompletedQuestionId();
}

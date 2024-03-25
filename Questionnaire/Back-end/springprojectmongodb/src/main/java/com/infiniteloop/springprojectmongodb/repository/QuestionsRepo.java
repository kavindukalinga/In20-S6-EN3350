package com.infiniteloop.springprojectmongodb.repository;

import com.infiniteloop.springprojectmongodb.model.Questions;
import org.springframework.data.mongodb.repository.MongoRepository;


public interface QuestionsRepo extends MongoRepository<Questions, String> {
    Questions findFirstByIsCompletedOrderByQuestionIdDesc(Boolean isCompleted);
}

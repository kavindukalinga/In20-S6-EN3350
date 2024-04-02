package com.infiniteloop.springprojectmongodb.repositories;

import com.infiniteloop.springprojectmongodb.models.Questions;
import org.springframework.data.mongodb.repository.MongoRepository;


public interface QuestionsRepo extends MongoRepository<Questions, String> {
    Questions findFirstByIsCompletedOrderByQuestionIdDesc(Boolean isCompleted);
}

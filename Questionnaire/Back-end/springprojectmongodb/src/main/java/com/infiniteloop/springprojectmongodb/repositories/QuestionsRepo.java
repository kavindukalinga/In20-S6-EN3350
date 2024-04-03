package com.infiniteloop.springprojectmongodb.repositories;

import com.infiniteloop.springprojectmongodb.models.Questions;
import org.springframework.data.mongodb.repository.MongoRepository;

// Repository interface for accessing Questions documents in MongoDB
public interface QuestionsRepo extends MongoRepository<Questions, String> {
    
    // Custom query method to find the first completed question by ordering them by question ID in descending order
    Questions findFirstByIsCompletedOrderBySortKeyDesc(Boolean isCompleted);
}

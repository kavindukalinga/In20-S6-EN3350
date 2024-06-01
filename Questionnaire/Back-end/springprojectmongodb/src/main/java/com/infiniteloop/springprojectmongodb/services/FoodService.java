package com.infiniteloop.springprojectmongodb.services;

import com.infiniteloop.springprojectmongodb.models.Food;
import com.infiniteloop.springprojectmongodb.repositories.FoodRepo;
import com.infiniteloop.springprojectmongodb.responses.FoodResponse;


import java.util.ArrayList;
import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

@Service
public class FoodService {

    @Autowired
    private FoodRepo foodRepository;

    public Food addFood(Food food) {
        return foodRepository.save(food);
    }

    public List<Food> getAllFoods() {
    return foodRepository.findAll();
}

public Food updateCountByConstant(String name, int constant) {
    Food food = foodRepository.findByName(name);
    if (food != null) {
        int newCount = food.getCount() + constant;
        food.setCount(newCount);
        return foodRepository.save(food);
    }
    return null;
}

public List<FoodResponse> getFoodTypesByAnimalType(String animalType) {
    List<Food> foods = foodRepository.findByAnimalTypes(animalType);
    List<FoodResponse> foodInfoList = new ArrayList<>();
    for (Food food : foods) {
        FoodResponse foodInfo = new FoodResponse();
        foodInfo.setName(food.getName());
        foodInfo.setCount(food.getCount());
        foodInfo.setHealthPoints(food.getHealthPoints());
        foodInfoList.add(foodInfo);
    }
    return foodInfoList;
}



}


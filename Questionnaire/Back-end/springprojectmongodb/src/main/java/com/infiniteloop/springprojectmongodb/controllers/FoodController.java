package com.infiniteloop.springprojectmongodb.controllers;

import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;
import com.infiniteloop.springprojectmongodb.models.Food;
import com.infiniteloop.springprojectmongodb.responses.FoodResponse;
import com.infiniteloop.springprojectmongodb.services.FoodService;

@RestController
@RequestMapping("/api/foods")
public class FoodController {

    @Autowired
    private FoodService foodService;

    @PostMapping
    public ResponseEntity<Food> addFood(@RequestBody Food food) {
        Food savedFood = foodService.addFood(food);
        return ResponseEntity.ok(savedFood);
    }

    @GetMapping("all")
    public ResponseEntity<List<Food>> getAllFoods() {
        List<Food> foods = foodService.getAllFoods();
        return ResponseEntity.ok(foods);
    }

    @PutMapping("/{name}")
    public ResponseEntity<Food> updateCountByConstant(@PathVariable String name, @RequestParam int value) {

        Food updatedFood = foodService.updateCountByConstant(name, value);
        if (updatedFood == null) {
            return ResponseEntity.notFound().build();
        }
        return ResponseEntity.ok(updatedFood);
    }

    @GetMapping("/{animalType}")
    public ResponseEntity<List<FoodResponse>> getFoodTypesByAnimalType(@PathVariable String animalType) {
        List<FoodResponse> foodInfoList = foodService.getFoodTypesByAnimalType(animalType);
        if (foodInfoList.isEmpty()) {
            return ResponseEntity.notFound().build();
        }
        return ResponseEntity.ok(foodInfoList);
    }


}


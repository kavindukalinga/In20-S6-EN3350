package com.infiniteloop.springprojectmongodb.controllers;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import com.infiniteloop.springprojectmongodb.enums.UpdateHealthRequest;
import com.infiniteloop.springprojectmongodb.models.Animal;
import com.infiniteloop.springprojectmongodb.services.AnimalService;

import java.util.List;
import java.util.Map;

@RestController
@RequestMapping("/api/animals")
public class AnimalController {

    @Autowired
    private AnimalService animalService;

    @PostMapping
    public ResponseEntity<Animal> addAnimal(@RequestBody Animal animal) {
        Animal savedAnimal = animalService.addAnimal(animal);
        return ResponseEntity.ok(savedAnimal);
    }

    @GetMapping("/all")
    public ResponseEntity<List<Animal>> getAllAnimals() {
        List<Animal> animals = animalService.getAllAnimals();
        return ResponseEntity.ok(animals);
    }

    @PutMapping("/updateHealth")
    public ResponseEntity<Void> updateHealthOfAllAnimals(@RequestParam int delta) {
        animalService.updateHealthOfAllAnimals(delta);
        return ResponseEntity.ok().build();
    }

    @GetMapping("/healths/{name}")
    public ResponseEntity<List<Integer>> getHealthsByAnimalType(@PathVariable String name) {
        List<Integer> healths = animalService.getHealthsByAnimalType(name);
        if (healths == null) {
            return ResponseEntity.notFound().build();
        }
        return ResponseEntity.ok(healths);
    }

    @PostMapping("/healths/{name}")
    public ResponseEntity<Animal> updateHealthElementByConstant(
            @PathVariable String name,
            @RequestBody UpdateHealthRequest updateHealthRequest) {
        System.out.println("name: " + name + ", index: " + updateHealthRequest.getIndex() + ", delta: " + updateHealthRequest.getDelta());
        Animal updatedAnimal = animalService.updateHealthElementByConstant(name, updateHealthRequest.getIndex(), updateHealthRequest.getDelta());
        if (updatedAnimal == null) {
            return ResponseEntity.notFound().build();
        }
        return ResponseEntity.ok(updatedAnimal);
    }

    @GetMapping("/counts")
    public ResponseEntity<Map<String, Long>> getAnimalCounts() {
        Map<String, Long> animalCounts = animalService.getAnimalCounts();
        return ResponseEntity.ok(animalCounts);
    }

    @PutMapping("/add/{name}")
    public ResponseEntity<Boolean> updateHealthIfZeroExists(
            @PathVariable String name,
            @RequestParam int newHealthValue) {
        boolean updated = animalService.updateHealthIfZeroExists(name, newHealthValue);
        return ResponseEntity.ok(updated);
    }
}
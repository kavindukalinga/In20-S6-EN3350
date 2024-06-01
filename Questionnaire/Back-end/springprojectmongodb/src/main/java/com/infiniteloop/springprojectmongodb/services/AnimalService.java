package com.infiniteloop.springprojectmongodb.services;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import com.infiniteloop.springprojectmongodb.models.Animal;
import com.infiniteloop.springprojectmongodb.repositories.AnimalRepo;

import java.util.List;

@Service
public class AnimalService {

    @Autowired
    private AnimalRepo animalRepository;

    public Animal addAnimal(Animal animal) {
        return animalRepository.save(animal);
    }

    public List<Animal> getAllAnimals() {
        return animalRepository.findAll();
    }

    public void updateHealthOfAllAnimals(int delta) {
        List<Animal> animals = animalRepository.findAll();
        for (Animal animal : animals) {
            List<Integer> health = animal.getHealth();
            for (int i = 0; i < health.size(); i++) {
                if (health.get(i) + delta > 0) {
                    health.set(i, health.get(i) + delta);
                }else{
                    health.set(i, 0);
                }
            }
            animal.setHealth(health);
            animalRepository.save(animal);
        }
    }
    

    public List<Integer> getHealthsByAnimalType(String name) {
        Animal animal = animalRepository.findByName(name);
        return animal != null ? animal.getHealth() : null;
    }

    public Animal updateHealthElementByConstant(String name, int index, int delta) {
        Animal animal = animalRepository.findByName(name);
        if (animal != null && index >= 0 && index < animal.getHealth().size()) {
            List<Integer> health = animal.getHealth();
            health.set(index, health.get(index) + delta);
            animal.setHealth(health);
            return animalRepository.save(animal);
        }
        return null;
    }
}
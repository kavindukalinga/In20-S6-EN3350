package com.infiniteloop.springprojectmongodb.controller;


import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.RestController;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;

import com.infiniteloop.springprojectmongodb.model.Player;
import com.infiniteloop.springprojectmongodb.repository.PlayerRepo; // Assuming PlayerRepository is your MongoDB repository interface

import java.util.List;

@RestController
@RequestMapping(value = "/player")
public class PlayerController {

    @Autowired
    private PlayerRepo playerRepository;

    @PostMapping("/add")
    Player addPlayer(@RequestBody Player player) {
        return playerRepository.save(player);
    }

    @GetMapping("/getall")
    List<Player> getAllPlayers() {
        return playerRepository.findAll();
    }
}


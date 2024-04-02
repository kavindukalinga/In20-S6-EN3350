package com.infiniteloop.springprojectmongodb.controllers;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.RestController;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import com.infiniteloop.springprojectmongodb.models.Player;
import com.infiniteloop.springprojectmongodb.repositories.PlayerRepo;

import java.util.List;

@RestController
@RequestMapping(value = "/player")
public class PlayerController {

    @Autowired
    private PlayerRepo playerRepository;

    // Endpoint to add a player
    @PostMapping("/add")
    Player addPlayer(@RequestBody Player player) {
        return playerRepository.save(player);
    }

    // Endpoint to get all players
    @GetMapping("/getall")
    List<Player> getAllPlayers() {
        return playerRepository.findAll();
    }
}

package com.infiniteloop.springproject.controller;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.RestController;

import com.infiniteloop.springproject.repository.PlayerRepo;
import com.infiniteloop.springproject.model.Player;

import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import java.util.List;

@RestController
@RequestMapping(value = "/player")
public class PlayerController {
    @Autowired
    PlayerRepo playerRepo;

    @PostMapping("/add")
    Player addPlayer(@RequestBody Player player) {
         playerRepo.save(player);
         return player;
    }

    @GetMapping("/getall")
    List<Player> getAllPlayers() {
        List<Player> players = playerRepo.findAll();
        return players;
    }
    

    
}

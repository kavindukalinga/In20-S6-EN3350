package com.infiniteloop.springprojectmongodb.controllers;

import com.infiniteloop.springprojectmongodb.models.Player;
import com.infiniteloop.springprojectmongodb.responses.CoinsResponse;
import com.infiniteloop.springprojectmongodb.responses.UpdateResponse;
import com.infiniteloop.springprojectmongodb.services.PlayerService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.time.LocalDateTime;
import java.util.Optional;

@RestController
@RequestMapping("/api/players")
public class PlayerController {
    @Autowired
    private PlayerService playerService;

    @PostMapping
    public ResponseEntity<Player> addPlayer(@RequestBody Player player) {
        Player createdPlayer = playerService.addPlayer(player);
        return ResponseEntity.ok(createdPlayer);
    }

    @GetMapping("/{id}")
    public ResponseEntity<Player> getPlayerById(@PathVariable String id) {
        Optional<Player> playerOpt = playerService.getPlayerById(id);
        return playerOpt.map(ResponseEntity::ok).orElseGet(() -> ResponseEntity.notFound().build());
    }

    @GetMapping("/coins")
    public ResponseEntity<CoinsResponse> getCoinsById() {
        Optional<Player> playerOpt = playerService.getPlayerById("1");
        return playerOpt.map(player -> ResponseEntity.ok(new CoinsResponse(player.getCoins())))
                        .orElseGet(() -> ResponseEntity.notFound().build());
    }

    @PutMapping("/coins")
    public ResponseEntity<Player> updateCoins(@RequestParam int coins) {
        Player updatedPlayer = playerService.updateCoins("1", coins);
        return updatedPlayer != null ? ResponseEntity.ok(updatedPlayer) : ResponseEntity.notFound().build();
    }

    @PutMapping("/time")
    public ResponseEntity<UpdateResponse> updateTime() {
        LocalDateTime oldTime = playerService.updateTime("1");
        if (oldTime != null) {
            LocalDateTime newTime = LocalDateTime.now();
            return ResponseEntity.ok(new UpdateResponse(oldTime, newTime));
        } else {
            return ResponseEntity.notFound().build();
        }
    }


}

package com.infiniteloop.springprojectmongodb.services;

import com.infiniteloop.springprojectmongodb.models.Player;
import com.infiniteloop.springprojectmongodb.repositories.PlayerRepo;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.time.LocalDateTime;
import java.util.Optional;

@Service
public class PlayerService {
    @Autowired
    private PlayerRepo playerRepository;

    public Player addPlayer(Player player) {
        return playerRepository.save(player);
    }

    public Optional<Player> getPlayerById(String id) {
        return playerRepository.findById(id);
    }

    public Player updateCoins(String id, int coins) {
        Optional<Player> playerOpt = playerRepository.findById(id);
        if (playerOpt.isPresent()) {
            Player player = playerOpt.get();
            player.setCoins(coins);
            return playerRepository.save(player);            
        }
        return null;
    }

    public LocalDateTime updateTime(String id) {
        Optional<Player> playerOpt = playerRepository.findById(id);
        if (playerOpt.isPresent()) {
            Player player = playerOpt.get();
            LocalDateTime oldTime = player.getTime();
            LocalDateTime newTime = LocalDateTime.now();
            player.setTime(newTime);
            playerRepository.save(player);
            return oldTime; // Return the old time
        }
        return null;
    }
    
    
}

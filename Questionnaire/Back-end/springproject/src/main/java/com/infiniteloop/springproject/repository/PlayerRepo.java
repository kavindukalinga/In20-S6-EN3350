package com.infiniteloop.springproject.repository;
import com.infiniteloop.springproject.model.Player;
import org.springframework.data.jpa.repository.JpaRepository;

public interface PlayerRepo extends JpaRepository<Player, Integer> {
    
}

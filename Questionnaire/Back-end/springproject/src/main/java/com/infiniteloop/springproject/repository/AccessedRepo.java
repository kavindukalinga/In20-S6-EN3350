package com.infiniteloop.springproject.repository;
import com.infiniteloop.springproject.model.Accessed;
import org.springframework.data.jpa.repository.JpaRepository;

public interface AccessedRepo extends JpaRepository<Accessed, Integer> {
    
}

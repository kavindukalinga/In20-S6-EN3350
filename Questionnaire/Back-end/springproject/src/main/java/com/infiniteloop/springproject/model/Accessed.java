package com.infiniteloop.springproject.model;
import jakarta.persistence.Entity;
import jakarta.persistence.GeneratedValue;
import jakarta.persistence.GenerationType;
import jakarta.persistence.Id;

@Entity
public class Accessed {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    Integer id;

    Boolean isAnswered;

    public Integer getId() {
        return id;
    }

    public void setId(Integer id) {
        this.id = id;
    }

    public Boolean getIsAnswered() {
        return isAnswered;
    }

    public void setIsAnswered(Boolean isAnswered) {
        this.isAnswered = isAnswered;
    }
    
}

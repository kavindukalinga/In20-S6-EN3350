package com.infiniteloop.springprojectmongodb.services;

import java.util.List;
import java.util.stream.Collectors;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import com.infiniteloop.springprojectmongodb.models.Stall;
import com.infiniteloop.springprojectmongodb.repositories.StallRepo;
import com.infiniteloop.springprojectmongodb.responses.StallResponse;

@Service
public class StallService {

    @Autowired
    private StallRepo stallRepository;

    public Stall addStall(Stall stall) {
        return stallRepository.save(stall);
    }

    public List<StallResponse> getAllStalls() {
        List<Stall> stalls = stallRepository.findAll();
        return stalls.stream()
                     .map(stall -> new StallResponse(stall.getName(), stall.getLevel()))
                     .collect(Collectors.toList());
    }

    public Stall updateStallLevel(String name, int level) {
        Stall stall = stallRepository.findByName(name);
        if (stall != null) {
            stall.setLevel(level);
            return stallRepository.save(stall);
        }
        return null;
    }


}

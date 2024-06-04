package com.infiniteloop.springprojectmongodb.controllers;

import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;
import com.infiniteloop.springprojectmongodb.models.Stall;
import com.infiniteloop.springprojectmongodb.responses.StallResponse;
import com.infiniteloop.springprojectmongodb.services.StallService;

@RestController
@RequestMapping("/api/stalls")
public class StallController {

    @Autowired
    private StallService stallService;

    @PostMapping
    public ResponseEntity<Stall> addStall(@RequestBody Stall stall) {
        Stall savedStall = stallService.addStall(stall);
        return ResponseEntity.ok(savedStall);
    }

    @GetMapping("/all")
    public ResponseEntity<List<StallResponse>> getAllStalls() {
        List<StallResponse> stalls = stallService.getAllStalls();
        return ResponseEntity.ok(stalls);
    }

    @PutMapping("/{name}")
    public ResponseEntity<Stall> updateStallLevel(@PathVariable String name, @RequestParam int level) {
        Stall updatedStall = stallService.updateStallLevel(name, level);
        if (updatedStall == null) {
            return ResponseEntity.notFound().build();
        }
        return ResponseEntity.ok(updatedStall);
    }

    @GetMapping("/{name}")
    public ResponseEntity<Integer> getStallLevelByName(@PathVariable String name) {
        Integer level = stallService.getStallLevelByName(name);
        if (level != null) {
            return ResponseEntity.ok(level);
        } else {
            return ResponseEntity.notFound().build();
        }
    }

}

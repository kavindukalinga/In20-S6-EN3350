package com.infiniteloop.springprojectmongodb.responses;

import java.time.LocalDateTime;

public class UpdateResponse {
    private LocalDateTime oldTime;
    private LocalDateTime newTime;

    public UpdateResponse(LocalDateTime oldTime, LocalDateTime newTime) {
        this.oldTime = oldTime;
        this.newTime = newTime;
    }

    public LocalDateTime getOldTime() {
        return oldTime;
    }

    public void setOldTime(LocalDateTime oldTime) {
        this.oldTime = oldTime;
    }

    public LocalDateTime getNewTime() {
        return newTime;
    }

    public void setNewTime(LocalDateTime newTime) {
        this.newTime = newTime;
    }
}

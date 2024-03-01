#!/bin/bash

# Open Terminal 1 and run Flask app
gnome-terminal --tab --title="Flask App" -- bash -c "cd ./Front-end/Sample-backend-flask-api && python main.py; exec bash"

# Open Terminal 2 and run npm dev server
gnome-terminal --tab --title="React App" -- bash -c "cd ./Front-end/Sample-quiz-app && npm run dev; exec bash"

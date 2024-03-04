from flask import Flask, request, jsonify, redirect
from flask_cors import CORS


answers = {
    1: 1,
    2: 4,
    3: 1,
    4: 3,
    5: 2,
}

app = Flask(__name__)
CORS(app)

@app.route('/')
def home():
    button_html = '<form action="/quiz" method="get"><button type="submit">Go to Quiz</button></form>'
    return button_html

user_data = {
    8888: {
        "user_id": 8888,
        "name": "Admin",
        "age": 99,
        "email": "Admin@example.com"
    }
}

@app.route('/get-answer/<int:question_id>')
def get_answer(question_id):
    try:
        return jsonify({"answer": answers[question_id]}), 200
    except KeyError:
        return jsonify({"error": f"Question with question_id:{question_id} not found"}), 404

@app.route('/get-user/<int:user_id>')
def get_user(user_id):
    try:
        return jsonify(user_data[user_id]), 200
    except KeyError:
        return jsonify({"error": f"User with user_id:{user_id} not found"}), 404

@app.route('/create-user', methods=['POST'])
def create_user():
    data = request.json
    user_id = data.get('user_id')
    if user_id not in user_data:
        user_data[user_id] = data
        return jsonify(data), 201
    else:
        return jsonify({"error": f"User with user_id:{user_id} already exists"}), 400

@app.route('/quiz')
def quiz():
    return redirect('http://localhost:5173/')

@app.route('/game')
def game():
    score = request.args.get('score')
    return "Game Menu with score: " + score

if __name__ == '__main__':
    app.run(debug=True)

from flask import Flask, request, jsonify, redirect
from flask_cors import CORS

# Need to add a database to store the data
'''
There should be 3 coloumns in questions Table:
id(primary key - int), the right answer(int), whether the question is answered or not(boolean - default value is False)

And there is 2 cloumns and 1 row in isAccessed Table:
id(primary key - int), isAccessed(boolean) 
'''
answers = {
    1: 1,
    2: 4,
    3: 1,
    4: 3,
    5: 2,
}

available_question = {'available_question':0}
userAnswers={}

app = Flask(__name__)
CORS(app)

@app.route('/')
def home():
    button_html = '<form action="/quiz" method="get"><button type="submit">Go to Quiz</button></form>'
    return button_html

@app.route('/useranswers')
def user_answers():
    return jsonify(userAnswers)

user_data = {
    8888: {
        "user_id": 8888,
        "name": "Admin",
        "age": 99,
        "email": "Admin@example.com"
    }
}
@app.route('/get-current-question')
def get_current_question():
    try:
        return jsonify(available_question), 200
    except KeyError:
        return jsonify({"error": "Available question not found"}), 404


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

@app.route('/api/data', methods=['POST'])
def receive_data():
    data = request.json
    ans = data.get('ans')
    question_id = data.get('question_id')
    userAnswers[question_id] = ans
    if available_question['available_question'] <=4:
        available_question['available_question'] = available_question['available_question']+1
    else:
        available_question['available_question'] = 0
    # Do something with the received data, such as checking the answer
    # Return appropriate response
    return jsonify({'answer': 'your_answer'})

## Need to get score like this as well


if __name__ == '__main__':
    app.run(debug=True)

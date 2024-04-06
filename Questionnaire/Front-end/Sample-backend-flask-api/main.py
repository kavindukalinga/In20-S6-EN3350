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
    6: 3,
    7: 2,
    8: 3,
    9: 3,
    10: 3
}

questionsforall= [
    {
        "questionId": "1",
        "correctAnswer": "C",
        "question": "What is the primary source of energy for most power grids around the world?",
        "answers": {
            "A": "Solar power",
            "B": "Wind power",
            "C": "Fossil fuels",
            "D": "Hydropower"
        },
        "generalFeedback": "While the mix of energy sources varies by region, fossil fuels remain the dominant source for electricity generation globally, though renewable sources are on the rise.",
        "specificFeedback": "Solar power is growing but is not the primary source globally.",
        "playerAnswer": "A"
    },
    {
        "questionId": "2",
        "correctAnswer": "C",
        "question": "How does electricity typically travel from power plants to consumers?",
        "answers": {
            "A": "Through water pipes",
            "B": "Via transmission and distribution networks",
            "C": "Directly from generators to homes",
            "D": "Through the internet"
        },
        "generalFeedback": "Electricity is generated at power plants and then transmitted over long distances via high-voltage transmission lines. It's then distributed to consumers through lower-voltage distribution networks.",
        "specificFeedback": "Water pipes are used for plumbing, not electrical transmission.",
        "playerAnswer": "A"
    },
    {
        "questionId": "3",
        "correctAnswer": "C",
        "question": "Why is energy efficiency important in homes and businesses",
        "answers": {
            "A": "It increases energy consumption",
            "B": "It leads to higher energy costs",
            "C": "It reduces energy bills and environmental impact",
            "D": "It has no impact on the environment"
        },
        "generalFeedback": "Energy efficiency is crucial for reducing energy consumption, lowering energy bills, and minimizing the environmental footprint by decreasing greenhouse gas emissions.",
        "specificFeedback": "Energy efficiency aims to reduce, not increase, consumption.",
        "playerAnswer": "A"
    },
    {
        "questionId": "4",
        "correctAnswer": "B",
        "question": "What is the primary goal of demand management in energy usage?",
        "answers": {
            "A": "To increase the overall energy consumption",
            "B": "To balance energy supply and demand",
            "C": "To eliminate the use of renewable energy sources",
            "D": "To double the energy costs for consumers"
        },
        "generalFeedback": "Demand management aims to ensure energy is used more efficiently, balancing the supply with the fluctuating demand to maintain grid stability and reduce costs.",
        "specificFeedback": "This is the opposite of demand management's goal, which aims to optimize, not increase, energy use",
        "playerAnswer": "A"
    },
    {
        "questionId": "5",
        "correctAnswer": "C",
        "question": "Which of the following is a common method used in demand management to encourage lower energy use during peak hours?",
        "answers": {
            "A": "Increasing energy prices during off-peak hours",
            "B": "Providing incentives for high energy consumption",
            "C": "Offering lower rates or incentives for using less energy during peak times",
            "D": "Discouraging the use of energy-efficient appliances"
        },
        "generalFeedback": "Lowering rates or providing incentives for reduced energy use during peak hours helps smooth out energy demand, benefiting both the grid and consumer.",
        "specificFeedback": "This approach would not encourage lower usage during peak times.",
        "playerAnswer": "A"
    },
    {
        "questionId": "6",
        "correctAnswer": "C",
        "question": "Benefits to the consumer from demand management include:",
        "answers": {
            "A": "Higher energy bills",
            "B": "Less control over their energy use",
            "C": "Savings on their electricity bill",
            "D": "Reduced internet connectivity"
        },
        "generalFeedback": "Participating in demand management programs can lead to significant savings on electricity bills for consumers by incentivizing energy use during off-peak hours.",
        "specificFeedback": "Demand management aims to reduce, not increase, consumer energy bills.",
        "playerAnswer": "A"
    },
    {
        "questionId": "7",
        "correctAnswer": "B",
        "question": "How does implementing demand management strategies benefit the environment?",
        "answers": {
            "A": "By significantly increasing carbon emissions",
            "B": "By reducing reliance on fossil fuels and lowering carbon emissions",
            "C": "By eliminating the need for public transportation",
            "D": "By discouraging the use of renewable energy"
        },
        "generalFeedback": "Implementing demand management strategies plays a crucial role in environmental conservation by reducing the reliance on non-renewable energy sources and minimizing carbon emissions.",
        "specificFeedback": "Demand management aims to decrease, not increase, carbon emissions.",
        "playerAnswer": "A"
    },
    {
        "questionId": "8",
        "correctAnswer": "C",
        "question": "What can be a direct benefit of participating in a demand management program for consumers?",
        "answers": {
            "A": "Higher energy bills",
            "B": "Less control over their energy use",
            "C": "Savings on their electricity bill",
            "D": "Reduced internet connectivity"
        },
        "generalFeedback": "Participation in demand management programs often results in direct benefits for consumers, such as savings on electricity bills, by encouraging energy use during less expensive, off-peak hours.",
        "specificFeedback": "The goal of demand management is to offer savings, not to increase bills.",
        "playerAnswer": "A"
    },
    {
        "questionId": "9",
        "correctAnswer": "C",
        "question": "Why is load shifting important in demand management?",
        "answers": {
            "A": "It increases the energy load during peak times",
            "B": "It shifts energy usage to times when demand is higher",
            "C": "It helps balance the power grid by using energy during lower-demand periods",
            "D": "It makes energy more expensive during off-peak hours"
        },
        "generalFeedback": "Load shifting is a critical component of demand management, aimed at moving energy use from peak to off-peak hours. This helps balance the power grid, reduces the need for additional power plants, and can lead to cost savings for consumers and utility providers alike.",
        "specificFeedback": "The purpose of load shifting is to decrease, not increase, the load during peak times to help balance energy demand.",
        "playerAnswer": "A"
    },
    {
        "questionId": "10",
        "correctAnswer": "C",
        "question": "Which of the following electric loads can be effectively managed as part of a demand management program?",
        "answers": {
            "A": "Fixed lighting systems in public areas",
            "B": "Emergency medical equipment",
            "C": "Residential air conditioning units",
            "D": "Data centers that require constant cooling"
        },
        "generalFeedback": "Demand management programs focus on adjusting the usage of flexible and non-critical electric loads to optimize energy consumption. Residential air conditioning units, for example, can be adjusted without compromising safety or critical operations, making them ideal for inclusion in these programs.",
        "specificFeedback": "While lighting can be managed, fixed systems in public areas often have safety implications that limit their flexibility.",
        "playerAnswer": "A"
    }
]

available_question = {'available_question':0}
userAnswers={
  1: 0,
  2: 0,
  3: 0,
  4: 0,
  5: 0,
  6: 0,
  7: 0,
  8: 0,
  9: 0,
  10: 0
}

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


@app.route('/get-question/<int:id>')
def get_question(id):
    try:
        return jsonify(questionsforall[id]), 200
    except KeyError:
        return jsonify({"error": f"Question with id:{id} not found"}), 404

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
    userAnswers[int(question_id)] = ans
    if available_question['available_question'] <=9:
        available_question['available_question'] = available_question['available_question']+1
    else:
        available_question['available_question'] = 0
    # Do something with the received data, such as checking the answer
    # Return appropriate response
    return jsonify({'answer': 'your_answer'})

## Need to get score like this as well

@app.route('/get-score')
def get_score():
    score = 0
    for key in userAnswers:
        if userAnswers[key] == answers[key]:
            score = score + 1
    return jsonify({'score': score})


if __name__ == '__main__':
    app.run(debug=True, port=9000, host='0.0.0.0')

from pymongo import MongoClient

def get_data(database_name, collection_name):
    # Connect to MongoDB
    client = MongoClient("mongodb+srv://infiniteloop:infiniteloop@cluster0.gpher9c.mongodb.net/")
    db = client[database_name]
    collection = db[collection_name]

    # Iterate over documents in the collection and print each document
    for document in collection.find():
        print(document)
        print("\n")

def update_is_completed(database_name, collection_name):
    # Connect to MongoDB
    client = MongoClient("mongodb+srv://infiniteloop:infiniteloop@cluster0.gpher9c.mongodb.net/")
    db = client[database_name]
    collection = db[collection_name]

    # Update all documents to set isCompleted to False
    collection.update_many({}, {"$set": {"isCompleted": False}})

    print("isCompleted field updated for all documents.")

if __name__ == "__main__":
    # Example usage:
    update_is_completed("player_db", "questionnaire")





if __name__ == "__main__":
    # Example usage:
    update_is_completed("player_db", "questionnaire")
    get_data("player_db", "questionnaire")

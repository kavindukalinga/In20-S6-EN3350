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

if __name__ == "__main__":
    # Example usage:
    get_data("player_db", "questionnaire")

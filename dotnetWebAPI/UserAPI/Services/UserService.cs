using System.Collections.Generic;
using MongoDB.Driver;
using UserAPI.Models;

namespace UserAPI.Services
{
    public class UserService
    {
        private readonly IMongoCollection<User> _Users;

        public UserService(IUsersDatabaseSettings settings)
        {            
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _Users = database.GetCollection<User>(settings.UsersCollectionName);
        }

        public List<User> Get() =>
            _Users.Find(user => true).ToList();

        public User Get(string id) =>
            _Users.Find<User>(user => user.Id == id).FirstOrDefault();

        public User Create(User user)
        {
            _Users.InsertOne(user);
            return user;
        }

        public void Update(string id, User userIn) =>
            _Users.ReplaceOne(user => user.Id == id, userIn);

        public void Remove(User userIn) =>
            _Users.DeleteOne(user => user.Id == userIn.Id);

        public void Remove(string id) => 
            _Users.DeleteOne(user => user.Id == id);
    }
} 
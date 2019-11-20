using CustomersMongoDb.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace CustomersMongoDb.Services
{
    
    public class CustomerService
    {
        private readonly MongoDB.Driver.IMongoCollection<Customer> _customers;
        
        public CustomerService(ICustomerDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _customers = database.GetCollection<Customer>(settings.CustomersCollectionName);
        }

        public List<Customer> Get() =>
            _customers.Find(customer => true).ToList();

        public Customer GetById(string id) =>
            _customers.Find<Customer>(customer => customer.Id == id).FirstOrDefault();

        public List<Customer> GetByAge(int age) =>
            _customers.Find<Customer>(customer => customer.age == age).ToList();

        public Customer Create(Customer customer)
        {
            _customers.InsertOne(customer);
            return customer;
        }

        public void Update(string id, Customer customerIn) {
            Customer customerToUpdate = new Customer { Id = id, name = customerIn.name, age = customerIn.age, active = customerIn.active, __v = customerIn.__v };
            _customers.ReplaceOne(customer => customer.Id == id, customerToUpdate);
        }

        public void Remove(string Id) =>
            _customers.DeleteOne(customer => customer.Id == Id);

        public void RemoveAll(){
            // https://stackoverflow.com/questions/53329048/mongo-c-sharp-driver-find-multiple-and-delete
            // Find the documents to delete. In this case all documents
            var filter = new BsonDocument();
            var docs = _customers.Find(filter).ToList();
            // Get the _id values of all the documents
            var ids = docs.Select(d => d.Id);
            // Create an $in filter for those ids
            var idsFilter = Builders<Customer>.Filter.In(d => d.Id, ids);
            // Delete the documents using the $in filter
            var result = _customers.DeleteMany(idsFilter);

        }
    }
}

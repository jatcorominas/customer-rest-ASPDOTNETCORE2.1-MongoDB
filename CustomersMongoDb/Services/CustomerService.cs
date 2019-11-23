using CustomersMongoDb.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        //public List<Customer> Get() =>
        //    _customers.Find(customer => true).ToList();
        public async Task<IEnumerable<Customer>> Get()
        {
            try
            {
                return await _customers
                        .Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        //public Customer GetById(string id) =>
        //    _customers.Find<Customer>(customer => customer.Id == id).FirstOrDefault();
        public async Task<Customer> GetById(string id)
        {
            try
            {
                return await _customers.Find<Customer>(customer => customer.Id == id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }

        }

        //public List<Customer> GetByAge(int age) =>
        //    _customers.Find<Customer>(customer => customer.age == age).ToList();
        public async Task<IEnumerable<Customer>> GetByAge(int age)
        {
            try
            {
                return await _customers.Find<Customer>(customer => customer.age == age).ToListAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        //public Customer Create(Customer customer)
        //{
        //    _customers.InsertOne(customer);
        //    return customer;
        //}
        public async Task<Customer> Create(Customer customer)
        {
            try {
                await _customers.InsertOneAsync(customer);
                return customer;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        //public void Update(string id, Customer customerIn) {
        //    Customer customerToUpdate = new Customer { Id = id, name = customerIn.name, age = customerIn.age, active = customerIn.active, __v = customerIn.__v };
        //    _customers.ReplaceOne(customer => customer.Id == id, customerToUpdate);
        //}
        public async Task Update(string id, Customer customerIn)
        {
            try
            {
                Customer customerToUpdate = new Customer { Id = id, name = customerIn.name, age = customerIn.age, active = customerIn.active, __v = customerIn.__v };
                await _customers.ReplaceOneAsync(customer => customer.Id == id, customerToUpdate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public void Remove(string Id) =>
        //    _customers.DeleteOne(customer => customer.Id == Id);

        public async Task Remove(string Id)
        {
            try
            {
                await _customers.DeleteOneAsync(customer => customer.Id == Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public void RemoveAll(){
        //    // https://stackoverflow.com/questions/53329048/mongo-c-sharp-driver-find-multiple-and-delete
        //    // Find the documents to delete. In this case all documents
        //    var filter = new BsonDocument();
        //    var docs = _customers.Find(filter).ToList();
        //    // Get the _id values of all the documents
        //    var ids = docs.Select(d => d.Id);
        //    // Create an $in filter for those ids
        //    var idsFilter = Builders<Customer>.Filter.In(d => d.Id, ids);
        //    // Delete the documents using the $in filter
        //    var result = _customers.DeleteMany(idsFilter);

        //}

        public async Task RemoveAll()
        {
            try
            {
                await _customers.DeleteManyAsync(new BsonDocument());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}

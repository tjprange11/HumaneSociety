using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Text;
using System.Threading.Tasks;

namespace HumaneSociety
{
    public static class Query
    {

        public static Client GetClient(string username, string password)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            var client = (Client)(db.Clients.Where(c => c.userName.Equals(username) && c.pass.Equals(password)));
            return client;
        }
        public static Animal GetAnimalByID(int ID)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            var animal = (Animal)(db.Animals.Where(id => id.ID == ID));
            return animal;
        }
        public static void Adopt(Animal animal, Client client)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            
        }
        public static IQueryable<ClientAnimalJunction> GetPendingAdoptions()
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            var pendingAdoptions = db.ClientAnimalJunctions.Where(c => c.approvalStatus.Equals("Approved"));
            return pendingAdoptions;
        }
        public static Employee EmployeeLogin(string username, string password)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            var employee = (Employee)(db.Employees.Where(c => c.userName.Equals(username) && c.pass.Equals(password)));
            return employee;
        }

        //public static ClientAnimalJunction GetUserAdoptionStatus(Client client)
        //{
        //    HumaneSocietyDataContext db = new HumaneSocietyDataContext();
        //    var status = db.ClientAnimalJunctions.Where(s => s.Animal1.adoptionStatus.)
        //        return status;
        //}
        
        public static IQueryable<Client> RetrieveClients()
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            var clients = db.Clients;
            return clients;
        }

        public static IQueryable<USState> GetStates()
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            var states = db.USStates;
            return states;
        }

        public static void AddNewClient(string firstName, string lastName, string username, string password, string email, string streetAddress, int zipCode, int state)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            USState uSState = new USState();
            UserAddress address = new UserAddress
            {
                addessLine1 = streetAddress,
                addressLine2 = streetAddress,
                zipcode = zipCode,
                USStates = uSState.ID

            };

            Client client = new Client()
            {
                firstName = firstName,
                lastName = lastName,
                userName = username,
                pass = password,
                email = email,
                userAddress = address.ID
            
            };
            db.Clients.InsertOnSubmit (client);
            db.SubmitChanges();
        }




    }
}

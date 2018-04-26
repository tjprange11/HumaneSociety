﻿using System;
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
            Client client = (Client)(db.Clients.Where(c => c.userName.Equals(username) && c.pass.Equals(password)));
            return client;
        }
        public static Animal GetAnimalByID(int ID)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            Animal animal = (Animal)(db.Animals.Where(id => id.ID == ID));
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
            Employee employee = (Employee)(db.Employees.Where(c => c.userName.Equals(username) && c.pass.Equals(password)));
            return employee;
        }
        public static bool CheckEmployeeUserNameExist(string username)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            Employee employee = (Employee)(db.Employees.Where(e => e.userName.Equals(username)));
            if(employee != null)
            {
                return true;
            }
            return false;
        }

        internal static Employee RetrieveEmployeeUser(string email, int employeeNumber)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            Employee employee = (Employee)(db.Employees.Where(e => e.email.Equals(email) && e.employeeNumber.Equals(employeeNumber)));
            return employee;
        }
        public static void AddUsernameAndPassword(Employee employee)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            Employee updatedEmployee = (Employee)(db.Employees.Where(e => e.Equals(employee)));
            updatedEmployee.userName = employee.userName;
            updatedEmployee.pass = employee.pass;
        }

        internal static void AddAnimal(Animal animal)
        {
            throw new NotImplementedException();
        }

        internal static int? GetLocation()
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            string location = UserInterface.GetStringData("location", "the animal's");
            var query = db.Rooms.Where(room => room.name == location).Select(room => room.ID).First();
            return query;
        }

        internal static void UpdateAdoption(bool v, ClientAnimalJunction clientAnimalJunction)
        {
            throw new NotImplementedException();
        }

        internal static int? GetDiet()
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            string diet = UserInterface.GetStringData("diet", "the animal's");
            int amount = UserInterface.GetIntegerData("amount", "the animal's");

            try
            {
                var query = db.DietPlans.Where(dietPlan => dietPlan.food == diet).Select(dietPlan => dietPlan.ID).First();
                return query;
            }
            catch
            {
                DietPlan newDietPlan = new DietPlan
                {
                    food = diet,
                    amount = amount
                };
                return newDietPlan.ID;

            }
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

        internal static int? GetBreed()
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            string breed = UserInterface.GetStringData("breed", "the animal's");
            int category = UserInterface.GetIntegerData("spiecies", "the animal's");
            string pattern = UserInterface.GetStringData("pattern", "the animal's");
            try
            {
                var query = db.Breeds.Where(b => b.breed1 == breed).Select(b => b.ID).First();
                return query;
            }
            catch
            {
                Breed newBreed = new Breed
                {
                    breed1 = breed,
                    catagory = category,
                    pattern = pattern
                };
                return newBreed.ID;
            }
            
        }

        internal static void RemoveAnimal(Animal animal)
        {
            throw new NotImplementedException();
        }

        internal static void EnterUpdate(Animal animal, Dictionary<int, string> updates)
        {
            throw new NotImplementedException();
        }

        internal static void UpdateShot(string v, Animal animal)
        {
            throw new NotImplementedException();
        }

        internal static object GetShots(Animal animal)
        {
            throw new NotImplementedException();
        }
    }
}

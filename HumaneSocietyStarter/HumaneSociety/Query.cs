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
            throw new NotImplementedException();
        }

        internal static int? GetDiet()
        {
            throw new NotImplementedException();
        }
    }
}

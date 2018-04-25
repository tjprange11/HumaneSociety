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


        
    }
}

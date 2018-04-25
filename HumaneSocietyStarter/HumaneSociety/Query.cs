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

        public static IQueryable<Client> GetClient(string username, string password)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            var client = (db.Clients.Where(c => c.userName.Equals(username) && c.pass.Equals(password)));
            return client;
        }
        public static IQueryable<Animal> GetAnimalByID(int ID)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            var animal = (db.Animals.Where(id => id.ID == ID));
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


        
    }
}

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
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            db.Animals.InsertOnSubmit(animal);
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
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            var currentJunction = db.ClientAnimalJunctions.Where(data => data.animal == clientAnimalJunction.animal && data.client == clientAnimalJunction.client).Select(data => data).First();
            var currentAnimal = db.Animals.Where(data => data.ID == clientAnimalJunction.animal).Select(data => data).First();
            if (v)
            {
                currentJunction.approvalStatus = "approved";
                currentAnimal.adoptionStatus = "adopted";
                UserInterface.DisplayUserOptions("transferring adoption fee from adopter");
            }
            else
            {
                currentJunction.approvalStatus = "denied";
            }
            db.SubmitChanges();
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
        internal static IQueryable<ClientAnimalJunction> GetUserAdoptionStatus(Client client)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            var adoptions = db.ClientAnimalJunctions.Where(data => data.client == client.ID).Select(data => data);
            return adoptions;
        }

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
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            db.Animals.DeleteOnSubmit(animal);
        }

        internal static void EnterUpdate(Animal animal, Dictionary<int, string> updates)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            foreach(int entry in updates.Keys)
            {
                switch (entry)
                {
                    case 1:
                        UpdateCategory(animal, updates[1]);
                        break;
                    case 2:
                        UpdateBreed(animal, updates[2]);
                        break;
                    case 3:
                        UpdateName(animal, updates[3]);
                        break;
                    case 4:
                        UpdateAge(animal, updates[4]);
                        break;
                    case 5:
                        UpdateDemeanor(animal, updates[5]);
                        break;
                    case 6:
                        UpdateKidFriendly(animal, updates[6]);
                        break;
                    case 7:
                        UpdatePetFriendly(animal, updates[7]);
                        break;
                    case 8:
                        UpdateWeight(animal, updates[8]);
                        break;

                }
                
            }
        }
        internal static void UpdateCategory(Animal animal, string v)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            Breed breed = db.Breeds.Where(b => b.ID == animal.ID).Select(b => b).First();
            int category = 0;
            var isValidCategory = int.TryParse(v, out category);
            if (isValidCategory)
            {
                breed.catagory = category;
                db.SubmitChanges();
            }
        }
        internal static void UpdateBreed(Animal animal, string v)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();

            Breed breed = db.Breeds.Where(b => b.ID == animal.ID).Select(b => b).First();
            breed.breed1 = v;
            db.SubmitChanges();

        }
        internal static void UpdateName(Animal animal, string v)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();

            Animal updatedAnimal = db.Animals.Where(a => a.ID == animal.ID).Select(a => a).First();

            updatedAnimal.name = v;
            db.SubmitChanges();
        }

        internal static void UpdateAge(Animal animal, string v)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();

            Animal updatedAnimal = db.Animals.Where(a => a.ID == animal.ID).Select(a => a).First();

            updatedAnimal.age = Int32.Parse(v);
            db.SubmitChanges();
        }

        internal static void UpdateDemeanor(Animal animal, string v)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();

            Animal updatedAnimal = db.Animals.Where(a => a.ID == animal.ID).Select(a => a).First();

            updatedAnimal.demeanor = v;
            db.SubmitChanges();
        }

        internal static void UpdateKidFriendly(Animal animal, string v)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();

            Animal updatedAnimal = db.Animals.Where(a => a.ID == animal.ID).Select(a => a).First();

            updatedAnimal.kidFriendly = UserInterface.GetBitData(v);
            db.SubmitChanges();
        }

        internal static void UpdatePetFriendly(Animal animal, string v)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();

            Animal updatedAnimal = db.Animals.Where(a => a.ID == animal.ID).Select(a => a).First();

            updatedAnimal.petFriendly = UserInterface.GetBitData(v);
            db.SubmitChanges();
        }

        internal static void UpdateWeight(Animal animal, string v)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();

            Animal updatedAnimal = db.Animals.Where(a => a.ID == animal.ID).Select(a => a).First();

            updatedAnimal.weight = Int32.Parse(v);
            db.SubmitChanges();
        }

        internal static void UpdateShot(string v, Animal animal)
        {
            throw new NotImplementedException();
        }

        internal static IQueryable<AnimalShotJunction> GetShots(Animal animal)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            var shots = db.AnimalShotJunctions.Where(data => data.Animal_ID == animal.ID).Select(data => data);
            return shots;
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
            db.Clients.InsertOnSubmit(client);
            db.SubmitChanges();
        }
        public static void UpdateFirstName(Client client)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            var clientData = db.Clients.Where(c => c.ID == client.ID).Select(c => c).First();
            clientData.firstName = client.firstName;
            db.SubmitChanges();
        }
        public static void UpdateLastName(Client client)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            var clientData = db.Clients.Where(c => c.ID == client.ID).Select(c => c).First();
            clientData.lastName = client.lastName;
            db.SubmitChanges();

        }
        public static void UpdateAddress(Client client)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            var clientData = db.Clients.Where(c => c.ID == client.ID).Select(c => c).First();
            clientData.UserAddress1.zipcode = client.UserAddress1.zipcode;
            clientData.UserAddress1.addessLine1 = client.UserAddress1.addessLine1;
            clientData.UserAddress1.USState = client.UserAddress1.USState;
            db.SubmitChanges();

        }

        public static void updateClient(Client client)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            db.Clients.InsertOnSubmit(client);
            db.SubmitChanges();
        }

        public static void UpdateUsername(Client client)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            var clientData = db.Clients.Where(c => c.ID == client.ID).Select(c => c).First();
            clientData.userName = client.userName;
            db.SubmitChanges();
        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;

namespace nucaapp
{
    public class getCities
    {
        private FirebaseClient firebase = new FirebaseClient("https://newcities-newurban.firebaseio.com");

        public async Task<List<CitiesModel>> GetAllNews()
        {
            try
            {
                List<CitiesModel> gn = (await firebase.Child("cities").Child("listn").OnceAsync<CitiesModel>())
                    .Select(item => new CitiesModel
                    {
                        id = item.Object.id,
                        name = item.Object.name,
                        namear = item.Object.namear,
                        image = item.Object.image
                    }).OrderBy(c=>c.id).ToList();
                return gn;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<CitiesModel> GetNewsById(int id)
        {
            CitiesModel gn = (await firebase.Child("cities").Child("note").OnceAsync<CitiesModel>())
                .Where(c => c.Object.id == id).Select(item => new CitiesModel
                {
                    id = item.Object.id,
                    
                    image = item.Object.image
                
                }).FirstOrDefault();
            return gn;
        }
    }
}

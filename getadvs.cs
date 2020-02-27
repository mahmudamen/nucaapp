using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;

namespace nucaapp
{
    public class getadvs
    {
        private FirebaseClient firebase = new FirebaseClient("https://newcities-newurban.firebaseio.com");

        public async Task<List<advModel>> GetAllAdvs()
        {
            try
            {
                List<advModel> gn = (await firebase.Child("cities").Child("adv").OnceAsync<advModel>())
                    .Select(item => new advModel
                    {
                        id = item.Object.id,
                        title = item.Object.title,
                        image = item.Object.image,
                        shortdesc = item.Object.shortdesc,
                        liqo = item.Object.liqo
                    }).OrderByDescending(c => c.id).Take(30).ToList();
                return gn;
            }
            catch (Exception)
            {
                return null;
            }

        }
        public async Task<advModel> GetAdvsById(int id)
        {
            advModel gn = (await firebase.Child("cities").Child("adv").OnceAsync<advModel>())
                .Where(c => c.Object.id == id).Select(item => new advModel
                {
                    id = item.Object.id,
                    title = item.Object.title,
                    image = item.Object.image,
                    shortdesc = item.Object.shortdesc,
                    liqo = item.Object.liqo
                }).FirstOrDefault();
            return gn;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;

namespace nucaapp
{
    public class getInvests
    {
        private FirebaseClient firebase = new FirebaseClient("https://newcities-newurban.firebaseio.com");

        public async Task<List<InvestsModel>> GetAllNews()
        {
            try
            {
                List<InvestsModel> gn = (await firebase.Child("cities").Child("landls").OnceAsync<InvestsModel>())
                    .Select(item => new InvestsModel
                    {
                        id = item.Object.id,
                        title = item.Object.title,
                        image = item.Object.image,
                        levelname = item.Object.levelname,
                        shortdesc = item.Object.shortdesc,
                        longdesc = item.Object.longdesc,
                        space = item.Object.space
                    }).OrderByDescending(c => c.id).Take(30).ToList();
                return gn;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<InvestsModel> GetNewsById(int id)
        {
            InvestsModel gn = (await firebase.Child("cities").Child("news").OnceAsync<InvestsModel>())
                .Where(c => c.Object.id == id).Select(item => new InvestsModel
                {
                    id = item.Object.id,
                    title = item.Object.title,
                    image = item.Object.image,
                    levelname = item.Object.levelname,
                    shortdesc = item.Object.shortdesc,
                    longdesc = item.Object.longdesc,
                    space = item.Object.space
                }).FirstOrDefault();
            return gn;
        }
    }
}

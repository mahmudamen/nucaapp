using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;
namespace nucaapp
{
    public class getNews
    {
        private FirebaseClient firebase = new FirebaseClient("https://newcities-newurban.firebaseio.com");

        public async Task<List<PhotoModel>> GetAllNews()
        {
            try
            {
                List<PhotoModel> gn = (await firebase.Child("cities").Child("news").OnceAsync<PhotoModel>())
                    .Select(item => new PhotoModel
                    {
                        id = item.Object.id,
                        title = item.Object.title,
                        image = item.Object.image,
                        date = item.Object.date,
                        dayx = item.Object.dayx
                    }).OrderByDescending(c => c.id).Take(30).ToList();
                return gn;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<PhotoModel> GetNewsById(int id)
        {
            PhotoModel gn = (await firebase.Child("cities").Child("news").OnceAsync<PhotoModel>())
                .Where(c=>c.Object.id == id).Select(item => new PhotoModel
                {
                    id = item.Object.id,
                    title = item.Object.title,
                    image = item.Object.image,
                    date = item.Object.date,
                    dayx = item.Object.dayx,
                    longdesc = item.Object.longdesc
                }).FirstOrDefault();
            return gn;
        }
    }

}

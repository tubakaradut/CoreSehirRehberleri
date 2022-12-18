using CoreSehirRehberleri.Models.Context;
using CoreSehirRehberleri.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSehirRehberleri.Data
{
    public class AppRepository : IAppRepository
    {
        private MyContext _myContext;
        
        public AppRepository(MyContext myContext)
        {
            _myContext = myContext;
        }
        public void Add<T>(T entity) where T : class
        {
            _myContext.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _myContext.Remove(entity);
        }

        public List<City> GetCities()
        {
            var cities=_myContext.Cities.Include(c=>c.Photos).ToList();
            return cities;
        }

        public City GetCityById(int cityid)
        {
            var city=_myContext.Cities.Include(c=>c.Photos).FirstOrDefault(c=>c.Id==cityid);

            return city;
        }

        public Photo GetPhoto(int id)
        {
            var photo = _myContext.Photos.FirstOrDefault(p => p.Id == id);
            return photo;

        }

        public List<Photo> GetPhotoByCity(int cityid)
        {
            var photos=_myContext.Photos.Where(p=>p.CityId== cityid).ToList();
            return photos;
        }

        public bool SaveAll()
        {
            return _myContext.SaveChanges()>0;
        }

        public void Update<T>(T entity) where T : class
        {
           _myContext.Entry(entity).CurrentValues.SetValues(entity);
        }
    }
}

using DostPatiler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DostPatiler.DA
{
    public class HayvanRepoConcrete : IHayvanRepo
    {
        private readonly ApplicationDbContext db;
        public HayvanRepoConcrete(ApplicationDbContext context)
        {
            db = context;
        }

        public Hayvan CreateAnimal(Hayvan a)
        {
            db.Hayvanlar.Add(a);
            db.SaveChanges();
            return a;
        }

        public void DeleteAnimal(int id)
        {
            var hayvan = db.Hayvanlar.FirstOrDefault(x => x.HayvanId == id);
            db.Hayvanlar.Remove(hayvan);
            db.SaveChanges();
        }

        public Hayvan GetAnimalById(int id)
        {
            return db.Hayvanlar.FirstOrDefault(x => x.HayvanId == id);
        }

        public List<Hayvan> GetAnimals()
        {
            return db.Hayvanlar.ToList();
        }

        public Hayvan UpdateAnimal(int id, Hayvan a)
        {
            var hayvan = db.Hayvanlar.FirstOrDefault(x => x.HayvanId == id);
            hayvan.HayvanCins = a.HayvanCins;
            db.Update(hayvan);
            db.SaveChanges();
            return hayvan;
        }
    }
}

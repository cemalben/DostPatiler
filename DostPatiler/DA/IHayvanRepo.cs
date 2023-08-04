using DostPatiler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DostPatiler.DA
{
    public interface IHayvanRepo
    {
        public Hayvan CreateAnimal(Hayvan a);
        public List<Hayvan> GetAnimals();
        public Hayvan GetAnimalById(int id);
        public void DeleteAnimal(int id);
        public Hayvan UpdateAnimal(int id, Hayvan a);
    }
}

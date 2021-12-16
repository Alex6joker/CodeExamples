using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategy
{
    public class Storage
    {
        /*
         * Класс описывает хранилище каждого города.
         * В нем показывается количество ресурсов, которое содержит город
         * согласно списку ресурсов
        */

        // Максимальное количество хранимого ресурса
        // при привышении новое поступление будет просто отсекаться
        private Int16 Limit;
        // (Временно) фиксированный список ресурсов с 2-мя ресурсами
        private Byte[] ResourcesArray;
        private Byte CountOfResources;

        public Storage()
        {
            // (Временно) при создании хранилища создаем его с фиксированными значениями
            ResourcesArray = new Byte[3];
            CountOfResources = (Byte)ResourcesArray.Length;
            for (int i = 0; i < CountOfResources; i++)
                ResourcesArray[i] = 0;
            // Ограничение по каждому ресурсу = 200
            Limit = 200;
        }

        public int GetResourcesCount()
        {
            return CountOfResources;
        }

        public int GetResourceCount(int ResourceIndex)
        {
            return ResourcesArray[ResourceIndex];
        }

        public Byte[] GetAllResoursesInfo()
        {
            return ResourcesArray;
        }

        public void AddResourceToStorage(Byte Count, int Type)
        {
            ResourcesArray[Type] += Count;
        }

        public void SubResourse(Byte Count, int Type)
        {
            if (ResourcesArray[Type] <= Count)
                ResourcesArray[Type] = 0;
            else
                ResourcesArray[Type] -= Count;
        }
    }
}

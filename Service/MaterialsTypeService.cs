using Lab5.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Lab5.Service
{
    class MaterialsTypeService
    {
        private static DatabaseContext db = Program.DB;

        public static void Create(MaterialsType model)
        {
            if (model.Name == null)
            {
                throw new Exception("Column Name could not be null");
            }
            if (!model.PricePerUnit.HasValue)
            {
                throw new Exception("Column PricePerUnit could not be null");
            }

            db.MaterialsTypes.Add(
                new MaterialsType()
                { 
                    Name = model.Name,
                    PricePerUnit = model.PricePerUnit
                });
            db.SaveChanges();
        }

        public static List<MaterialsType> Read(MaterialsType model, int pageSize = Int32.MaxValue, int currentPage = 0)
        {
            List<MaterialsType> answer = null;

            if (model.Name != null)
            {
                answer =
                    answer =
                    db.MaterialsTypes
                    .Where(rec => rec.Name == model.Name)
                    .Include(rec => rec.MaterialsSets)
                    .Skip(pageSize * currentPage)
                    .Take(pageSize)
                    .ToList();
            }
            else
            {
                answer =
                    db.MaterialsTypes
                    .Include(rec => rec.MaterialsSets)
                    .Skip(pageSize * currentPage)
                    .Take(pageSize)
                    .ToList();
            }

            return answer;
        }

        public static void Update(MaterialsType model)
        {
            MaterialsType materialsType = db.MaterialsTypes.FirstOrDefault(rec => rec.Name == model.Name);

            materialsType.PricePerUnit = model.PricePerUnit;

            db.SaveChanges();
        }

        public static void Delete(MaterialsType model)
        {
            MaterialsType materialsType = db.MaterialsTypes.FirstOrDefault(rec => rec.Name == model.Name);

            db.MaterialsTypes.Remove(materialsType);
            db.SaveChanges();
        }
    }
}

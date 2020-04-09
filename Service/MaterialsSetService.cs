using Lab5.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Lab5.Service
{
    class MaterialsSetService
    {
        private static DatabaseContext db = Program.DB;

        public static void Create(MaterialsSet model)
        {
            if (!model.MaterialsTypeId.HasValue)
            {
                throw new Exception("Column MaterialsTypeId could not be null");
            }
            if (!model.OrderId.HasValue)
            {
                throw new Exception("Column OrderId could not be null");
            }

            MaterialsSet materialsSet = db.MaterialsSets.FirstOrDefault(rec => rec.Id == model.Id);
            if (materialsSet != null)
            {
                Update(
                    new MaterialsSet() 
                    { 
                        Id = materialsSet.Id,
                        Count = materialsSet.Count + model.Count
                    });
                return;
            }

            db.MaterialsSets.Add(
                new MaterialsSet() 
                { 
                    Count = model.Count,
                    MaterialsTypeId = model.MaterialsTypeId,
                    OrderId = model.OrderId
                });
            db.SaveChanges();
        }

        public static List<MaterialsSet> Read(MaterialsSet model, int pageSize = Int32.MaxValue, int currentPage = 0)
        {
            List<MaterialsSet> answer = null;

            if (model.OrderId.HasValue && model.MaterialsTypeId.HasValue)
            {
                answer =
                    answer =
                    db.MaterialsSets
                    .Where(rec => rec.OrderId == model.OrderId && rec.MaterialsTypeId == model.MaterialsTypeId)
                    .Include(rec => rec.MaterialsType)
                    .Include(rec => rec.Order)
                    .Skip(pageSize * currentPage)
                    .Take(pageSize)
                    .ToList();
            }
            else if (model.OrderId.HasValue && !model.MaterialsTypeId.HasValue)
            {
                answer =
                    db.MaterialsSets
                    .Where(rec => rec.OrderId == model.OrderId)
                    .Include(rec => rec.MaterialsType)
                    .Include(rec => rec.Order)
                    .Skip(pageSize * currentPage)
                    .Take(pageSize)
                    .ToList();
            }
            else if (!model.OrderId.HasValue && model.MaterialsTypeId.HasValue)
            {
                answer =
                    db.MaterialsSets
                    .Where(rec => rec.MaterialsTypeId == model.MaterialsTypeId)
                    .Include(rec => rec.MaterialsType)
                    .Include(rec => rec.Order)
                    .Skip(pageSize * currentPage)
                    .Take(pageSize)
                    .ToList();
            }
            else
            {
                answer =
                    db.MaterialsSets
                    .Include(rec => rec.MaterialsType)
                    .Include(rec => rec.Order)
                    .Skip(pageSize * currentPage)
                    .Take(pageSize)
                    .ToList();
            }

            return answer;
        }

        public static (string, int) ReadMaxUsedMaterials()
        {
            var answer =
                db.MaterialsSets
                .Include(rec => rec.MaterialsType)
                .GroupBy(rec => rec.MaterialsType.Name)
                .Select(m => new
                {
                    name = m.Key,
                    count = m.Sum(rec => rec.Count)
                })
                .OrderByDescending(rec => rec.count)
                .First();

            return (answer.name, answer.count);
        }

        private static void Update(MaterialsSet model)
        {
            MaterialsSet materialsSet = db.MaterialsSets.FirstOrDefault(rec => rec.Id == model.Id);

            materialsSet.Count = model.Count;

            db.SaveChanges();
        }

        public static void Delete(MaterialsSet model)
        {
            List<MaterialsSet> materialsSets = null;

            if (model.OrderId.HasValue && model.MaterialsTypeId.HasValue)
            {
                materialsSets =
                    db.MaterialsSets
                    .Where(rec => rec.OrderId == model.OrderId && rec.MaterialsTypeId == model.MaterialsTypeId)
                    .ToList();
            }
            else if (model.OrderId.HasValue && !model.MaterialsTypeId.HasValue)
            {
                materialsSets =
                    db.MaterialsSets
                    .Where(rec => rec.OrderId == model.OrderId)
                    .ToList();
            }
            else if (!model.OrderId.HasValue && model.MaterialsTypeId.HasValue)
            {
                materialsSets =
                    db.MaterialsSets
                    .Where(rec => rec.MaterialsTypeId == model.MaterialsTypeId)
                    .ToList();
            }
            else
            {
                materialsSets =
                    db.MaterialsSets
                    .Where(rec => rec.Id == model.Id)
                    .ToList();
            }

            db.MaterialsSets.RemoveRange(materialsSets);
            db.SaveChanges();
        }
    }
}

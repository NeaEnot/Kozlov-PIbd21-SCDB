using Lab5.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Lab5.Service
{
    class BrigadeService
    {
        private static DatabaseContext db = Program.DB;

        public static void Create(Brigade model)
        {
            if (!model.WorkTypeId.HasValue)
            {
                throw new Exception("Column WorkTypeId could not be null");
            }

            db.Brigades.Add(
                new Brigade() 
                { 
                    OrderId = model.OrderId.HasValue ? model.OrderId : null,
                    WorkTypeId = model.WorkTypeId 
                });
            db.SaveChanges();
        }

        public static List<Brigade> Read(Brigade model, int pageSize = Int32.MaxValue, int currentPage = 0)
        {
            List<Brigade> answer = null;

            if (model.OrderId.HasValue && model.WorkTypeId.HasValue)
            {
                answer =
                    answer =
                    db.Brigades
                    .Where(rec => rec.OrderId == model.OrderId && rec.WorkTypeId == model.WorkTypeId)
                    .Include(rec => rec.WorkType)
                    .Include(rec => rec.Order)
                    .Include(rec => rec.Workers)
                    .Skip(pageSize * currentPage)
                    .Take(pageSize)
                    .ToList();
            }
            else if (model.OrderId.HasValue && !model.WorkTypeId.HasValue)
            {
                answer =
                    db.Brigades
                    .Where(rec => rec.OrderId == model.OrderId)
                    .Include(rec => rec.WorkType)
                    .Include(rec => rec.Order)
                    .Include(rec => rec.Workers)
                    .Skip(pageSize * currentPage)
                    .Take(pageSize)
                    .ToList();
            }
            else if (!model.OrderId.HasValue && model.WorkTypeId.HasValue)
            {
                answer =
                    db.Brigades
                    .Where(rec => rec.WorkTypeId == model.WorkTypeId)
                    .Include(rec => rec.WorkType)
                    .Include(rec => rec.Order)
                    .Include(rec => rec.Workers)
                    .Skip(pageSize * currentPage)
                    .Take(pageSize)
                    .ToList();
            }
            else
            {
                answer =
                    db.Brigades
                    .Include(rec => rec.WorkType)
                    .Include(rec => rec.Order)
                    .Include(rec => rec.Workers)
                    .Skip(pageSize * currentPage)
                    .Take(pageSize)
                    .ToList();
            }

            return answer;
        }

        public static void Update(Brigade model)
        {
            Brigade brigade = db.Brigades.FirstOrDefault(rec => rec.Id == model.Id);

            if (model.OrderId.HasValue)
            {
                brigade.OrderId = model.OrderId;
            }
            if (model.WorkTypeId.HasValue)
            {
                brigade.WorkTypeId = model.WorkTypeId;
            }

            db.SaveChanges();
        }

        public static void Delete(Brigade model)
        {
            Brigade brigade = db.Brigades.FirstOrDefault(rec => rec.Id == model.Id);

            db.Brigades.Remove(brigade);
            db.SaveChanges();
        }

    }
}

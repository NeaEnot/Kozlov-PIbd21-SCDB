using Lab5.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Lab5.Service
{
    class WorkService
    {
        private static DatabaseContext db = Program.DB;

        public static void Create(Work model)
        {
            if (!model.WorkTypeId.HasValue)
            {
                throw new Exception("Column WorkTypeId could not be null");
            }
            if (!model.OrderId.HasValue)
            {
                throw new Exception("Column OrderId could not be null");
            }

            db.Works.Add(
                new Work()
                {
                    OrderId = model.OrderId,
                    WorkTypeId = model.WorkTypeId
                });
            db.SaveChanges();
        }

        public static List<Work> Read(Work model, int pageSize = Int32.MaxValue, int currentPage = 0)
        {
            List<Work> answer = null;

            if (model.OrderId.HasValue && model.WorkTypeId.HasValue)
            {
                answer =
                    answer =
                    db.Works
                    .Where(rec => rec.OrderId == model.OrderId && rec.WorkTypeId == model.WorkTypeId)
                    .Include(rec => rec.WorkType)
                    .Include(rec => rec.Order)
                    .Skip(pageSize * currentPage)
                    .Take(pageSize)
                    .ToList();
            }
            else if (model.OrderId.HasValue && !model.WorkTypeId.HasValue)
            {
                answer =
                    db.Works
                    .Where(rec => rec.OrderId == model.OrderId)
                    .Include(rec => rec.WorkType)
                    .Include(rec => rec.Order)
                    .Skip(pageSize * currentPage)
                    .Take(pageSize)
                    .ToList();
            }
            else if (!model.OrderId.HasValue && model.WorkTypeId.HasValue)
            {
                answer =
                    db.Works
                    .Where(rec => rec.WorkTypeId == model.WorkTypeId)
                    .Include(rec => rec.WorkType)
                    .Include(rec => rec.Order)
                    .Skip(pageSize * currentPage)
                    .Take(pageSize)
                    .ToList();
            }
            else
            {
                answer =
                    db.Works
                    .Include(rec => rec.WorkType)
                    .Include(rec => rec.Order)
                    .Skip(pageSize * currentPage)
                    .Take(pageSize)
                    .ToList();
            }

            return answer;
        }

        public static void Delete(Work model)
        {
            List<Work> works = null;

            if (model.OrderId.HasValue && model.WorkTypeId.HasValue)
            {
                works =
                    db.Works
                    .Where(rec => rec.OrderId == model.OrderId && rec.WorkTypeId == model.WorkTypeId)
                    .ToList();
            }
            else if (model.OrderId.HasValue && !model.WorkTypeId.HasValue)
            {
                works =
                    db.Works
                    .Where(rec => rec.OrderId == model.OrderId)
                    .ToList();
            }
            else if (!model.OrderId.HasValue && model.WorkTypeId.HasValue)
            {
                works =
                    db.Works
                    .Where(rec => rec.WorkTypeId == model.WorkTypeId)
                    .ToList();
            }
            else
            {
                works =
                    db.Works
                    .Where(rec => rec.Id == model.Id)
                    .ToList();
            }

            db.Works.RemoveRange(works);
            db.SaveChanges();
        }
    }
}

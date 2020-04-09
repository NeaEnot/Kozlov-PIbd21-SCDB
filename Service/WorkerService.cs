using Lab5.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Lab5.Service
{
    class WorkerService
    {
        private static DatabaseContext db = Program.DB;

        public static void Create(Worker model)
        {
            if (model.FirstName == null)
            {
                throw new Exception("Column FirstName could not be null");
            }
            if (model.SecondName == null)
            {
                throw new Exception("Column SecondName could not be null");
            }
            if (model.LastName == null)
            {
                throw new Exception("Column LastName could not be null");
            }
            if (model.BirthDate == null)
            {
                throw new Exception("Column BirthDate could not be null");
            }
            if (model.AdmissionDate == null)
            {
                throw new Exception("Column AdmissionDate could not be null");
            }
            if (!model.PositionId.HasValue)
            {
                throw new Exception("Column PositionId could not be null");
            }
            if (!model.BrigadeId.HasValue)
            {
                throw new Exception("Column BrigadeId could not be null");
            }

            Worker worker =
                new Worker()
                {
                    FirstName = model.FirstName,
                    SecondName = model.SecondName,
                    LastName = model.LastName,
                    BirthDate = model.BirthDate,
                    AdmissionDate = model.AdmissionDate,
                    PositionId = model.PositionId,
                    BrigadeId = model.BrigadeId
                };

            db.Workers.Add(worker);
            db.SaveChanges();
        }

        public static List<Worker> Read(Worker model, int pageSize = Int32.MaxValue, int currentPage = 0)
        {
            List<Worker> answer = null;

            if (model.BrigadeId.HasValue && model.PositionId.HasValue)
            {
                answer =
                    db.Workers
                    .Where(rec => rec.BrigadeId == model.BrigadeId && rec.PositionId == model.PositionId)
                    .Include(rec => rec.Position)
                    .Include(rec => rec.Brigade)
                    .Skip(pageSize * currentPage)
                    .Take(pageSize)
                    .ToList();
            }
            else if (model.BrigadeId.HasValue && !model.PositionId.HasValue)
            {
                answer =
                    db.Workers
                    .Where(rec => rec.BrigadeId == model.BrigadeId)
                    .Include(rec => rec.Position)
                    .Include(rec => rec.Brigade)
                    .Skip(pageSize * currentPage)
                    .Take(pageSize)
                    .ToList();
            }
            else if (!model.BrigadeId.HasValue && model.PositionId.HasValue)
            {
                answer =
                    db.Workers
                    .Where(rec => rec.PositionId == model.PositionId)
                    .Include(rec => rec.Position)
                    .Include(rec => rec.Brigade)
                    .Skip(pageSize * currentPage)
                    .Take(pageSize)
                    .ToList();
            }
            else
            {
                answer =
                    db.Workers
                    .Where(rec => rec.Id == model.Id)
                    .Include(rec => rec.Position)
                    .Include(rec => rec.Brigade)
                    .Skip(pageSize * currentPage)
                    .Take(pageSize)
                    .ToList();
            }

            return answer;
        }

        public static void Update(Worker model)
        {
            Worker worker = db.Workers.FirstOrDefault(rec => rec.Id == model.Id);

            worker.SecondName = model.SecondName;
            worker.PositionId = model.PositionId;
            worker.BrigadeId = model.BrigadeId;

            db.SaveChanges();
        }

        public static void Delete(Worker model)
        {
            Worker worker = db.Workers.FirstOrDefault(rec => rec.Id == model.Id);

            db.Workers.Remove(worker);
            db.SaveChanges();
        }
    }
}

using Lab5.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Lab5.Service
{
    class WorkTypeService
    {
        private static DatabaseContext db = Program.DB;

        public static void Create(WorkType model)
        {
            if (model.Name == null)
            {
                throw new Exception("Column Name could not be null");
            }

            db.WorkTypes.Add(new WorkType() { Name = model.Name });
            db.SaveChanges();
        }

        public static List<WorkType> ReadWorkTypes(WorkType model, int pageSize = Int32.MaxValue, int currentPage = 0)
        {
            List<WorkType> answer = null;

            if (model.Name != null)
            {
                answer =
                    db.WorkTypes
                    .Where(rec => rec.Name == model.Name)
                    .Include(rec => rec.Works)
                    .Include(rec => rec.Brigades)
                    .Skip(pageSize * currentPage)
                    .Take(pageSize)
                    .ToList();
            }
            else
            {
                answer =
                    db.WorkTypes
                    .Include(rec => rec.Works)
                    .Include(rec => rec.Brigades)
                    .Skip(pageSize * currentPage)
                    .Take(pageSize)
                    .ToList();
            }

            return answer;
        }

        public static void DeleteWorkType(WorkType model)
        {
            WorkType workType = db.WorkTypes.FirstOrDefault(rec => rec.Name == model.Name);

            db.WorkTypes.Remove(workType);
            db.SaveChanges();
        }
    }
}

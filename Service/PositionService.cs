using Lab5.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace Lab5.Service
{
    class PositionService
    {
        private static DatabaseContext db = Program.DB;

        public static void Create(Position model)
        {
            if (model.Name == null)
            {
                throw new Exception("Column Name could not be null");
            }

            db.Positions.Add(
                new Position()
                { 
                    Name = model.Name, 
                    Salary = model.Salary 
                });
            db.SaveChanges();
        }

        public static List<Position> Read(Position model, int pageSize = Int32.MaxValue, int currentPage = 0)
        {
            List<Position> answer = null;

            if (model.Name != null)
            {
                answer =
                    answer =
                    db.Positions
                    .Where(rec => rec.Name == model.Name)
                    .Include(rec => rec.Workers)
                    .Skip(pageSize * currentPage)
                    .Take(pageSize)
                    .ToList();
            }
            else
            {
                answer =
                    db.Positions
                    .Include(rec => rec.Workers)
                    .Skip(pageSize * currentPage)
                    .Take(pageSize)
                    .ToList();
            }

            return answer;
        }

        public static void Update(Position model)
        {
            Position position = db.Positions.FirstOrDefault(rec => rec.Name == model.Name);

            position.Salary = model.Salary;

            db.SaveChanges();
        }

        public static void Delete(Position model)
        {
            Position position = db.Positions.FirstOrDefault(rec => rec.Name == model.Name);

            db.Positions.Remove(position);
            db.SaveChanges();
        }
    }
}

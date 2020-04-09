using Lab5.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Lab5.Service
{
    class OrderService
    {
        private static DatabaseContext db = Program.DB;

        public static void Create(Order model)
        {
            if (model.RegistrationDate == null)
            {
                throw new Exception("Column RegistrationDate could not be null");
            }
            if (model.DeliveryDate == null)
            {
                throw new Exception("Column DeliveryDate could not be null");
            }

            db.Orders.Add(
                new Order() 
                { 
                    RegistrationDate = model.RegistrationDate.Value.Date,
                    DeliveryDate = model.DeliveryDate.Value.Date 
                });
            db.SaveChanges();
        }

        public static List<Order> Read(Order model, int pageSize = Int32.MaxValue, int currentPage = 0)
        {
            List<Order> answer = null;

            if (model.RegistrationDate.HasValue && model.DeliveryDate.HasValue)
            {
                answer =
                    db.Orders
                    .Where(rec => rec.RegistrationDate >= model.RegistrationDate && rec.DeliveryDate <= model.DeliveryDate)
                    .Include(rec => rec.Works)
                    .Include(rec => rec.Brigades)
                    .Include(rec => rec.MaterialsSets)
                    .Skip(pageSize * currentPage)
                    .Take(pageSize)
                    .ToList();
            }
            else if (model.RegistrationDate.HasValue && !model.DeliveryDate.HasValue)
            {
                answer =
                    db.Orders
                    .Where(rec => rec.RegistrationDate >= model.RegistrationDate)
                    .Include(rec => rec.Works)
                    .Include(rec => rec.Brigades)
                    .Include(rec => rec.MaterialsSets)
                    .Skip(pageSize * currentPage)
                    .Take(pageSize)
                    .ToList();
            }
            else if (!model.RegistrationDate.HasValue && model.DeliveryDate.HasValue)
            {
                answer =
                    db.Orders
                    .Where(rec => rec.DeliveryDate == model.DeliveryDate)
                    .Include(rec => rec.Works)
                    .Include(rec => rec.Brigades)
                    .Include(rec => rec.MaterialsSets)
                    .Skip(pageSize * currentPage)
                    .Take(pageSize)
                    .ToList();
            }
            else
            {
                answer =
                    db.Orders
                    .Where(rec => rec.Id <= model.Id)
                    .Include(rec => rec.Works)
                    .Include(rec => rec.Brigades)
                    .Include(rec => rec.MaterialsSets)
                    .Skip(pageSize * currentPage)
                    .Take(pageSize)
                    .ToList();
            }

            return answer;
        }

        public static void Update(Order model)
        {
            Order order = db.Orders.FirstOrDefault(rec => rec.Id == model.Id);

            if (model.DeliveryDate != null)
            {
                order.DeliveryDate = model.DeliveryDate;
            }

            db.SaveChanges();
        }

        public static void Delete(Order model)
        {
            Order order = db.Orders.FirstOrDefault(rec => rec.Id == model.Id);

            db.Orders.Remove(order);
            db.SaveChanges();
        }
    }
}

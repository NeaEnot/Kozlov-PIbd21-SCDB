using Lab5.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab5
{
    static class Service
    {
        static DatabaseContext db = new DatabaseContext();

        public static void CreateBrigade(Brigade model)
        {
            if (!model.WorkTypeId.HasValue)
            {
                throw new Exception("Column WorkTypeId could not be null");
            }

            db.Brigades.Add(new Brigade() { OrderId = model.OrderId.HasValue? model.OrderId : null, WorkTypeId = model.WorkTypeId });
            db.SaveChanges();
        }

        public static List<Brigade> ReadBrigades(Brigade model)
        {
            List<Brigade> answer = null;

            if (model.OrderId.HasValue && model.WorkTypeId.HasValue)
            {
                answer =
                    db.Brigades
                    .Where(rec => rec.OrderId == model.OrderId && rec.WorkTypeId == model.WorkTypeId)
                    .Include(rec => rec.WorkType)
                    .Include(rec => rec.Order)
                    .Include(rec => rec.Workers)
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
                    .ToList();
            }
            else
            {
                answer =
                    db.Brigades
                    .Include(rec => rec.WorkType)
                    .Include(rec => rec.Order)
                    .Include(rec => rec.Workers)
                    .ToList();
            }

            return answer;
        }

        public static List<Brigade> ReadBrigades(Brigade model, int pageSize, int currentPage)
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

        public static void UpdateBrigade(Brigade model)
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

        public static void DeleteBrigade(Brigade model)
        {
            Brigade brigade = db.Brigades.FirstOrDefault(rec => rec.Id == model.Id);

            db.Brigades.Remove(brigade);
            db.SaveChanges();
        }

        public static void CreateMaterialsSet(MaterialsSet model)
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
                UpdateMaterialsSet(new MaterialsSet() { Id = materialsSet.Id, Count = materialsSet.Count + model.Count });
                return;
            }

            db.MaterialsSets.Add(new MaterialsSet() { Count = model.Count, MaterialsTypeId = model.MaterialsTypeId, OrderId = model.OrderId });
            db.SaveChanges();
        }

        public static List<MaterialsSet> ReadMaterialsSets(MaterialsSet model)
        {
            List<MaterialsSet> answer = null;

            if (model.OrderId.HasValue && model.MaterialsTypeId.HasValue)
            {
                answer =
                    db.MaterialsSets
                    .Where(rec => rec.OrderId == model.OrderId && rec.MaterialsTypeId == model.MaterialsTypeId)
                    .Include(rec => rec.MaterialsType)
                    .Include(rec => rec.Order)
                    .ToList();
            }
            else if (model.OrderId.HasValue && !model.MaterialsTypeId.HasValue)
            {
                answer =
                    db.MaterialsSets
                    .Where(rec => rec.OrderId == model.OrderId)
                    .Include(rec => rec.MaterialsType)
                    .Include(rec => rec.Order)
                    .ToList();
            }
            else if (!model.OrderId.HasValue && model.MaterialsTypeId.HasValue)
            {
                answer =
                    db.MaterialsSets
                    .Where(rec => rec.MaterialsTypeId == model.MaterialsTypeId)
                    .Include(rec => rec.MaterialsType)
                    .Include(rec => rec.Order)
                    .ToList();
            }
            else
            {
                answer =
                    db.MaterialsSets
                    .Include(rec => rec.MaterialsType)
                    .Include(rec => rec.Order)
                    .ToList();
            }

            return answer;
        }

        public static List<MaterialsSet> ReadMaterialsSets(MaterialsSet model, int pageSize, int currentPage)
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

        private static void UpdateMaterialsSet(MaterialsSet model)
        {
            MaterialsSet materialsSet = db.MaterialsSets.FirstOrDefault(rec => rec.Id == model.Id);

            materialsSet.Count = model.Count;

            db.SaveChanges();
        }

        public static void DeleteMaterialsSets(MaterialsSet model)
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

        public static void CreateMaterialsType(MaterialsType model)
        {
            if (model.Name == null)
            {
                throw new Exception("Column Name could not be null");
            }
            if (!model.PricePerUnit.HasValue)
            {
                throw new Exception("Column PricePerUnit could not be null");
            }

            db.MaterialsTypes.Add(new MaterialsType() { Name = model.Name, PricePerUnit = model.PricePerUnit });
            db.SaveChanges();
        }

        public static List<MaterialsType> ReadMaterialsTypes(MaterialsType model)
        {
            List<MaterialsType> answer = null;

            if (model.Name != null)
            {
                answer =
                    db.MaterialsTypes
                    .Where(rec => rec.Name == model.Name)
                    .Include(rec => rec.MaterialsSets)
                    .ToList();
            }
            else
            {
                answer =
                    db.MaterialsTypes
                    .Include(rec => rec.MaterialsSets)
                    .ToList();
            }

            return answer;
        }

        public static List<MaterialsType> ReadMaterialsTypes(MaterialsType model, int pageSize, int currentPage)
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

        public static void UpdateMaterialsType(MaterialsType model)
        {
            MaterialsType materialsType = db.MaterialsTypes.FirstOrDefault(rec => rec.Name == model.Name);

            materialsType.PricePerUnit = model.PricePerUnit;

            db.SaveChanges();
        }

        public static void DeleteMaterialsType(MaterialsType model)
        {
            MaterialsType materialsType = db.MaterialsTypes.FirstOrDefault(rec => rec.Name == model.Name);

            db.MaterialsTypes.Remove(materialsType);
            db.SaveChanges();
        }

        public static void CreateOrder(Order model)
        {
            if (model.RegistrationDate == null)
            {
                throw new Exception("Column RegistrationDate could not be null");
            }
            if (model.DeliveryDate == null)
            {
                throw new Exception("Column DeliveryDate could not be null");
            }

            db.Orders.Add(new Order() { RegistrationDate = model.RegistrationDate.Date, DeliveryDate = model.DeliveryDate.Date });
            db.SaveChanges();
        }

        public static List<Order> ReadOrders(Order model)
        {
            List<Order> answer = null;

            if (model.RegistrationDate != null && model.DeliveryDate != null)
            {
                answer =
                    db.Orders
                    .Where(rec => rec.RegistrationDate >= model.RegistrationDate && rec.DeliveryDate <= model.DeliveryDate)
                    .Include(rec => rec.Works)
                    .Include(rec => rec.Brigades)
                    .Include(rec => rec.MaterialsSets)
                    .ToList();
            }
            else if (model.RegistrationDate != null && model.DeliveryDate == null)
            {
                answer =
                    db.Orders
                    .Where(rec => rec.RegistrationDate >= model.RegistrationDate)
                    .Include(rec => rec.Works)
                    .Include(rec => rec.Brigades)
                    .Include(rec => rec.MaterialsSets)
                    .ToList();
            }
            else if (model.RegistrationDate == null && model.DeliveryDate != null)
            {
                answer =
                    db.Orders
                    .Where(rec => rec.DeliveryDate <= model.DeliveryDate)
                    .Include(rec => rec.Works)
                    .Include(rec => rec.Brigades)
                    .Include(rec => rec.MaterialsSets)
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
                    .ToList();
            }

            return answer;
        }

        public static List<Order> ReadOrders(Order model, int pageSize, int currentPage)
        {
            List<Order> answer = null;
            
            if (model.RegistrationDate != null && model.DeliveryDate != null)
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
            else if (model.RegistrationDate != null && model.DeliveryDate == null)
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
            else if (model.RegistrationDate == null && model.DeliveryDate != null)
            {
                answer =
                    db.Orders
                    .Where(rec => rec.DeliveryDate <= model.DeliveryDate)
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

        public static void UpdateOrder(Order model)
        {
            Order order = db.Orders.FirstOrDefault(rec => rec.Id == model.Id);

            if (model.DeliveryDate != null)
            {
                order.DeliveryDate = model.DeliveryDate;
            }

            db.SaveChanges();
        }

        public static void DeleteOrder(Order model)
        {
            Order order = db.Orders.FirstOrDefault(rec => rec.Id == model.Id);

            db.Orders.Remove(order);
            db.SaveChanges();
        }

        public static void CreatePosition(Position model)
        {
            if (model.Name == null)
            {
                throw new Exception("Column Name could not be null");
            }

            db.Positions.Add(new Position() { Name = model.Name, Salary = model.Salary });
            db.SaveChanges();
        }

        public static List<Position> ReadPositions(Position model)
        {
            List<Position> answer = null;

            if (model.Name != null)
            {
                answer =
                    db.Positions
                    .Where(rec => rec.Name == model.Name)
                    .Include(rec => rec.Workers)
                    .ToList();
            }
            else
            {
                answer =
                    db.Positions
                    .Include(rec => rec.Workers)
                    .ToList();
            }

            return answer;
        }

        public static List<Position> ReadPositions(Position model, int pageSize, int currentPage)
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

        public static void UpdatePosition(Position model)
        {
            Position position = db.Positions.FirstOrDefault(rec => rec.Name == model.Name);

            position.Salary = model.Salary;

            db.SaveChanges();
        }

        public static void DeletePosition(Position model)
        {
            Position position = db.Positions.FirstOrDefault(rec => rec.Name == model.Name);

            db.Positions.Remove(position);
            db.SaveChanges();
        }

        public static void CreateWork(Work model)
        {
            if (!model.WorkTypeId.HasValue)
            {
                throw new Exception("Column WorkTypeId could not be null");
            }
            if (!model.OrderId.HasValue)
            {
                throw new Exception("Column OrderId could not be null");
            }

            db.Brigades.Add(new Brigade() { OrderId = model.OrderId, WorkTypeId = model.WorkTypeId });
            db.SaveChanges();
        }

        public static List<Work> ReadWorks(Work model)
        {
            List<Work> answer = null;

            if (model.OrderId.HasValue && model.WorkTypeId.HasValue)
            {
                answer =
                    db.Works
                    .Where(rec => rec.OrderId == model.OrderId && rec.WorkTypeId == model.WorkTypeId)
                    .Include(rec => rec.WorkType)
                    .Include(rec => rec.Order)
                    .ToList();
            }
            else if (model.OrderId.HasValue && !model.WorkTypeId.HasValue)
            {
                answer =
                    db.Works
                    .Where(rec => rec.OrderId == model.OrderId)
                    .Include(rec => rec.WorkType)
                    .Include(rec => rec.Order)
                    .ToList();
            }
            else if (!model.OrderId.HasValue && model.WorkTypeId.HasValue)
            {
                answer =
                    db.Works
                    .Where(rec => rec.WorkTypeId == model.WorkTypeId)
                    .Include(rec => rec.WorkType)
                    .Include(rec => rec.Order)
                    .ToList();
            }
            else
            {
                answer =
                    db.Works
                    .Include(rec => rec.WorkType)
                    .Include(rec => rec.Order)
                    .ToList();
            }

            return answer;
        }

        public static List<Work> ReadWorks(Work model, int pageSize, int currentPage)
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

        public static void DeleteWorks(Work model)
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

        public static void CreateWorker(Worker model)
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

            db.Workers.Add(
                new Worker() 
                { 
                    FirstName = model.FirstName,
                    SecondName = model.SecondName,
                    LastName = model.LastName,
                    BirthDate = model.BirthDate,
                    AdmissionDate = model.AdmissionDate,
                    PositionId = model.PositionId,
                    BrigadeId = model.BrigadeId
                });
            db.SaveChanges();
        }

        public static List<Worker> ReadWorkers(Worker model)
        {
            List<Worker> answer = null;

            if (model.BrigadeId.HasValue && model.PositionId.HasValue)
            {
                answer =
                    db.Workers
                    .Where(rec => rec.BrigadeId == model.BrigadeId && rec.PositionId == model.PositionId)
                    .Include(rec => rec.Position)
                    .Include(rec => rec.Brigade)
                    .ToList();
            }
            else if (model.BrigadeId.HasValue && !model.PositionId.HasValue)
            {
                answer =
                    db.Workers
                    .Where(rec => rec.BrigadeId == model.BrigadeId)
                    .Include(rec => rec.Position)
                    .Include(rec => rec.Brigade)
                    .ToList();
            }
            else if (!model.BrigadeId.HasValue && model.PositionId.HasValue)
            {
                answer =
                    db.Workers
                    .Where(rec => rec.PositionId == model.PositionId)
                    .Include(rec => rec.Position)
                    .Include(rec => rec.Brigade)
                    .ToList();
            }
            else
            {
                answer =
                    db.Workers
                    .Where(rec => rec.Id == model.Id)
                    .Include(rec => rec.Position)
                    .Include(rec => rec.Brigade)
                    .ToList();
            }

            return answer;
        }

        public static List<Worker> ReadWorkers(Worker model, int pageSize, int currentPage)
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

        public static void UpdateWorker(Worker model)
        {
            Worker worker = db.Workers.FirstOrDefault(rec => rec.Id == model.Id);

            worker.SecondName = model.SecondName;
            worker.PositionId = model.PositionId;
            worker.BrigadeId = model.BrigadeId;

            db.SaveChanges();
        }

        public static void DeleteWorker(Worker model)
        {
            Worker worker = db.Workers.FirstOrDefault(rec => rec.Id == model.Id);

            db.Workers.Remove(worker);
            db.SaveChanges();
        }

        public static void CreateWorkType(WorkType model)
        {
            if (model.Name == null)
            {
                throw new Exception("Column Name could not be null");
            }

            db.WorkTypes.Add(new WorkType() { Name = model.Name });
            db.SaveChanges();
        }

        public static List<WorkType> ReadWorkType(WorkType model)
        {
            List<WorkType> answer = null;

            if (model.Name != null)
            {
                answer =
                    db.WorkTypes
                    .Where(rec => rec.Name == model.Name)
                    .Include(rec => rec.Works)
                    .Include(rec => rec.Brigades)
                    .ToList();
            }
            else
            {
                answer =
                    db.WorkTypes
                    .Include(rec => rec.Works)
                    .Include(rec => rec.Brigades)
                    .ToList();
            }

            return answer;
        }

        public static List<WorkType> ReadWorkTypes(WorkType model, int pageSize, int currentPage)
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

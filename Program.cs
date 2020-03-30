using Lab5.Models;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System;
using System.Linq;

namespace Lab5
{
    class Program
    {
        static DatabaseContext db = new DatabaseContext();

        static void Main(string[] args)
        {
            while(true)
            {
                string cmd = Console.ReadLine();

                switch(cmd.ToLower())
                {
                    case "create":
                        Create();
                        break;
                    case "read":
                        Read();
                        break;
                    case "update":
                        Update();
                        break;
                    case "delete":
                        Delete();
                        break;
                    case "exit": 
                        return;
                    default:
                        Console.WriteLine("Unknown instruction: " + cmd);
                        break;
                }
            }
        }

        static void Create()
        {
            Console.Write("Entity: ");
            string entity = Console.ReadLine();

            try
            {
                switch (entity.ToLower())
                {
                    case "brigade":
                        {
                            Console.WriteLine("Work type: ");
                            string workTypeName = Console.ReadLine();

                            Brigade brigade = new Brigade() { WorkTypeId = db.WorkTypes.First(rec => rec.Name == workTypeName).Id };

                            db.Brigades.Add(brigade);
                            db.SaveChanges();

                            break;
                        }

                    case "materialsset":
                        {
                            Console.WriteLine("Materials type: ");
                            string materialsTypeName = Console.ReadLine();

                            Console.WriteLine("Count: ");
                            int materialsCount = Int32.Parse(Console.ReadLine());

                            Console.WriteLine("Order Id: ");
                            int orderId = Int32.Parse(Console.ReadLine());

                            MaterialsSet materialsSet =
                                new MaterialsSet()
                                {
                                    Count = materialsCount,
                                    MaterialsTypeId = db.MaterialsTypes.First(rec => rec.Name == materialsTypeName).Id,
                                    OrderId = orderId
                                };

                            db.MaterialsSets.Add(materialsSet);
                            db.SaveChanges();

                            break;
                        }

                    case "materialstype":
                        {
                            Console.WriteLine("Materials type name: ");
                            string materialsTypeName = Console.ReadLine();

                            Console.WriteLine("Price per unit: ");
                            int pricePerUnit = Int32.Parse(Console.ReadLine());

                            MaterialsType materialsType =
                                new MaterialsType()
                                {
                                    Name = materialsTypeName,
                                    PricePerUnit = pricePerUnit
                                };

                            db.MaterialsTypes.Add(materialsType);
                            db.SaveChanges();

                            break;
                        }

                    case "order":
                        {
                            Console.WriteLine("Delivery date: ");
                            DateTime deliveryDate = DateTime.Parse(Console.ReadLine()).Date;

                            DateTime registrationDate = DateTime.Now.Date;

                            Order order =
                                new Order()
                                {
                                    RegistrationDate = registrationDate,
                                    DeliveryDate = deliveryDate
                                };

                            db.Orders.Add(order);
                            db.SaveChanges();

                            break;
                        }

                    case "position":
                        {
                            Console.WriteLine("Position name: ");
                            string positionName = Console.ReadLine();

                            Console.WriteLine("Salary: ");
                            int positionSalary = Int32.Parse(Console.ReadLine());

                            Position position =
                                new Position()
                                {
                                    Name = positionName,
                                    Salary = positionSalary
                                };

                            db.Positions.Add(position);
                            db.SaveChanges();

                            break;
                        }

                    case "work":
                        {
                            Console.WriteLine("Work type: ");
                            string workTypeName = Console.ReadLine();

                            Console.WriteLine("Order id: ");
                            int orderId = Int32.Parse(Console.ReadLine());

                            Work work =
                                new Work()
                                {
                                    WorkTypeId = db.WorkTypes.First(rec => rec.Name == workTypeName).Id,
                                    OrderId = orderId
                                };

                            db.Works.Add(work);
                            db.SaveChanges();

                            break;
                        }

                    case "worker":
                        {
                            Console.WriteLine("First name: ");
                            string firstName = Console.ReadLine();

                            Console.WriteLine("Second name: ");
                            string secondName = Console.ReadLine();

                            Console.WriteLine("Last name: ");
                            string lastName = Console.ReadLine();

                            Console.WriteLine("Birth day: ");
                            int birthDay = Int32.Parse(Console.ReadLine());

                            Console.WriteLine("Birth mounth (number): ");
                            int birthMounth = Int32.Parse(Console.ReadLine());

                            Console.WriteLine("Birth year: ");
                            int birthYear = Int32.Parse(Console.ReadLine());

                            Console.WriteLine("Position: ");
                            string positionName = Console.ReadLine();

                            Console.WriteLine("Brigade id: ");
                            int brigadeId = Int32.Parse(Console.ReadLine());

                            DateTime birthDate = new DateTime(birthYear, birthMounth, birthDay).Date;
                            DateTime admissionDate = DateTime.Now.Date;

                            Worker worker =
                                new Worker()
                                {
                                    FirstName = firstName,
                                    SecondName = secondName,
                                    LastName = lastName,
                                    BirthDate = birthDate,
                                    AdmissionDate = admissionDate,
                                    PositionId = db.Positions.First(rec => rec.Name == positionName).Id,
                                    BrigadeId = brigadeId
                                };

                            db.Workers.Add(worker);
                            db.SaveChanges();

                            break;
                        }

                    case "worktype":
                        {
                            Console.WriteLine("Work type name: ");
                            string workTypeName = Console.ReadLine();

                            WorkType workType = new WorkType() { Name = workTypeName };

                            db.WorkTypes.Add(workType);
                            db.SaveChanges();

                            break;
                        }

                    default:
                        {
                            Console.WriteLine("Unknown entity: " + entity);
                            break;
                        }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static void Read()
        {
            Console.Write("Enter entity: ");
            string entity = Console.ReadLine();

            switch(entity.ToLower())
            {
                case "brigade":
                    {
                        Console.Write("Enter order id: ");
                        int orderId = Int32.Parse(Console.ReadLine());

                        var brigades =
                            db.Brigades
                            .Where(rec => rec.OrderId == orderId)
                            .Include(rec => rec.WorkType)
                            .Select(rec => new Brigade
                            {
                                Id = rec.Id,
                                WorkTypeId = rec.WorkTypeId,
                                OrderId = rec.OrderId == null ? null : rec.OrderId,
                                WorkType = rec.WorkType
                            });

                        Console.WriteLine("Brigade id\tWork type");
                        Console.WriteLine();
                        foreach (var brigade in brigades)
                        {
                            Console.WriteLine("{0}\t{1}", brigade.Id, brigade.WorkType.Name);
                        }

                        Console.WriteLine();

                        break;
                    }

                case "materialsset":
                    {
                        Console.Write("Enter order id: ");
                        int orderId = Int32.Parse(Console.ReadLine());

                        var materialsSets =
                            db.MaterialsSets
                            .Where(rec => rec.OrderId == orderId)
                            .Include(rec => rec.MaterialsType)
                            .Select(rec => new MaterialsSet
                            {
                                Id = rec.Id,
                                Count = rec.Count,
                                MaterialsTypeId = rec.MaterialsTypeId,
                                OrderId = rec.OrderId,
                                MaterialsType = rec.MaterialsType
                            });

                        Console.WriteLine("Materials set id\tMaterials type\tCount");
                        Console.WriteLine();
                        foreach (var set in materialsSets)
                        {
                            Console.WriteLine("{0}\t{1}\t{2}", set.Id, set.MaterialsType.Name, set.Count);
                        }

                        Console.WriteLine();

                        break;
                    }

                case "materialstype":
                    {
                        var materialsTypes =
                            db.MaterialsTypes
                            .Select(rec => new MaterialsType
                            {
                                Id = rec.Id,
                                Name = rec.Name,
                                PricePerUnit = rec.PricePerUnit
                            });

                        Console.WriteLine("Materials type\tPrice per unit");
                        Console.WriteLine();
                        foreach (var type in materialsTypes)
                        {
                            Console.WriteLine("{0}\t{1}", type.Name, type.PricePerUnit);
                        }

                        Console.WriteLine();

                        break;
                    }

                case "order":
                    {
                        var orders =
                            db.Orders
                            .Select(rec => new Order
                            {
                                Id = rec.Id,
                                RegistrationDate = rec.RegistrationDate,
                                DeliveryDate = rec.DeliveryDate
                            });

                        Console.WriteLine("Order id\tRegistration date\tDelivery date");
                        Console.WriteLine();
                        foreach (var order in orders)
                        {
                            Console.WriteLine("{0}\t{1}\t{2}", order.Id, order.RegistrationDate.ToString("dd:MM:yyyy"), order.DeliveryDate.ToString("dd:MM:yyyy"));
                        }

                        Console.WriteLine();

                        break;
                    }

                case "position":
                    {
                        var positions =
                            db.Positions
                            .Select(rec => new Position
                            {
                                Id = rec.Id,
                                Name = rec.Name,
                                Salary = rec.Salary
                            });

                        Console.WriteLine("Position\tSalary");
                        Console.WriteLine();
                        foreach (var position in positions)
                        {
                            Console.WriteLine("{0}\t{1}", position.Name, position.Salary);
                        }

                        Console.WriteLine();

                        break;
                    }

                case "work":
                    {
                        Console.Write("Enter order id: ");
                        int orderId = Int32.Parse(Console.ReadLine());

                        var works =
                            db.Works
                            .Where(rec => rec.OrderId == orderId)
                            .Include(rec => rec.WorkType)
                            .Select(rec => new Work
                            {
                                Id = rec.Id,
                                WorkTypeId = rec.WorkTypeId,
                                OrderId = rec.OrderId,
                                WorkType = rec.WorkType
                            });

                        Console.WriteLine("Work id\tWork");
                        Console.WriteLine();
                        foreach (var work in works)
                        {
                            Console.WriteLine("{0}\t{1}", work.Id, work.WorkType.Name);
                        }

                        Console.WriteLine();

                        break;
                    }

                case "worker":
                    {
                        Console.Write("Enter brigade id: ");
                        int brigadeId = Int32.Parse(Console.ReadLine());

                        var workers =
                            db.Workers
                            .Where(rec => rec.BrigadeId == brigadeId)
                            .Include(rec => rec.Position)
                            .Select(rec => new Worker
                            {
                                Id = rec.Id,
                                FirstName = rec.FirstName,
                                SecondName = rec.SecondName,
                                LastName = rec.LastName,
                                BirthDate = rec.BirthDate,
                                AdmissionDate = rec.AdmissionDate,
                                PositionId = rec.PositionId,
                                BrigadeId = rec.BrigadeId,
                                Position = rec.Position
                            });

                        Console.WriteLine("Id\tWorker\tPosition\tAge\tAdmission date");
                        Console.WriteLine();
                        foreach (var worker in workers)
                        {
                            Console.WriteLine("{0}\t{1} {2}.{3}.\t{4}\t{5}\t{6}", worker.Id, worker.SecondName, worker.FirstName[0], worker.LastName[0], DateTime.Now.Year - worker.BirthDate.Year, worker.AdmissionDate.ToString("dd:MM:yyyy"));
                        }

                        Console.WriteLine();

                        break;
                    }

                case "worktype":
                    {
                        var workTypes =
                            db.WorkTypes
                            .Select(rec => new WorkType
                            {
                                Id = rec.Id,
                                Name = rec.Name
                            });

                        Console.WriteLine("Work type");
                        Console.WriteLine();
                        foreach (var type in workTypes)
                        {
                            Console.WriteLine(type.Name);
                        }

                        Console.WriteLine();

                        break;
                    }

                default:
                    {
                        Console.WriteLine("Unknown entity: " + entity);
                        break;
                    }
            }
        }

        static void Update()
        {

            Console.Write("Enter entity: ");
            string entity = Console.ReadLine();

            try
            {
                switch (entity.ToLower())
                {
                    case "brigade":
                        {
                            Console.Write("Enter brigade id: ");
                            int brigadeId = Int32.Parse(Console.ReadLine());

                            Brigade brigade = db.Brigades.First(rec => rec.Id == brigadeId);

                            Console.Write("New order id: ");
                            int orderId = Int32.Parse(Console.ReadLine());

                            brigade.OrderId = orderId;
                            db.SaveChanges();

                            break;
                        }

                    case "materialstype":
                        {
                            Console.Write("Enter materials type name: ");
                            string materialsTypeName = Console.ReadLine();

                            MaterialsType materialsType = db.MaterialsTypes.First(rec => rec.Name == materialsTypeName);

                            Console.Write("New price per unit: ");
                            int pricePerUnit = Int32.Parse(Console.ReadLine());

                            materialsType.PricePerUnit = pricePerUnit;
                            db.SaveChanges();

                            break;
                        }

                    case "order":
                        {
                            Console.Write("Enter order id: ");
                            int orderId = Int32.Parse(Console.ReadLine());

                            Order order = db.Orders.First(rec => rec.Id == orderId);

                            Console.Write("New delivery date: ");
                            DateTime deliveryDate = DateTime.Parse(Console.ReadLine());

                            order.DeliveryDate = deliveryDate;
                            db.SaveChanges();

                            break;
                        }

                    case "position":
                        {
                            Console.Write("Enter position name: ");
                            string positionName = Console.ReadLine();

                            Position position = db.Positions.First(rec => rec.Name == positionName);

                            Console.Write("New salary: ");
                            int salary = Int32.Parse(Console.ReadLine());

                            position.Salary = salary;
                            db.SaveChanges();

                            break;
                        }

                    case "worker":
                        {
                            Console.Write("Enter worker id: ");
                            int workerId = Int32.Parse(Console.ReadLine());

                            Worker worker = db.Workers.First(rec => rec.Id == workerId);

                            Console.Write("New position: ");
                            string positionName = Console.ReadLine();

                            Console.Write("New brigade id: ");
                            int brigadeId = Int32.Parse(Console.ReadLine());

                            worker.PositionId = db.Positions.First(rec => rec.Name == positionName).Id;
                            worker.BrigadeId = brigadeId;
                            db.SaveChanges();

                            break;
                        }

                    default:
                        {
                            Console.WriteLine("Unknown or non updatable entity: " + entity);
                            break;
                        }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static void Delete()
        {
            Console.Write("Enter entity: ");
            string entity = Console.ReadLine();

            switch (entity.ToLower())
            {
                case "brigade":
                    {
                        Console.Write("Enter brigade id: ");
                        int brigadeId = Int32.Parse(Console.ReadLine());

                        Brigade brigade = db.Brigades.First(rec => rec.Id == brigadeId);
                        db.Brigades.Remove(brigade);
                        db.SaveChanges();

                        break;
                    }

                case "materialsset":
                    {
                        Console.Write("Enter materials set id: ");
                        int materialsSetId = Int32.Parse(Console.ReadLine());

                        MaterialsSet materialsSet = db.MaterialsSets.First(rec => rec.Id == materialsSetId);
                        db.MaterialsSets.Remove(materialsSet);
                        db.SaveChanges();

                        break;
                    }

                case "materialstype":
                    {
                        Console.Write("Enter materials type: ");
                        string materialsTypeName = Console.ReadLine();

                        MaterialsType materialsType = db.MaterialsTypes.First(rec => rec.Name == materialsTypeName);
                        db.MaterialsTypes.Remove(materialsType);
                        db.SaveChanges();

                        break;
                    }

                case "order":
                    {
                        Console.Write("Enter order id: ");
                        int orderId = Int32.Parse(Console.ReadLine());

                        Order order = db.Orders.First(rec => rec.Id == orderId);
                        db.Orders.Remove(order);
                        db.SaveChanges();

                        break;
                    }

                case "position":
                    {
                        Console.Write("Enter position: ");
                        string positionName = Console.ReadLine();

                        Position position = db.Positions.First(rec => rec.Name == positionName);
                        db.Positions.Remove(position);
                        db.SaveChanges();

                        break;
                    }

                case "work":
                    {
                        Console.Write("Enter work id: ");
                        int workId = Int32.Parse(Console.ReadLine());

                        Work work = db.Works.First(rec => rec.Id == workId);
                        db.Works.Remove(work);
                        db.SaveChanges();

                        break;
                    }

                case "worker":
                    {
                        Console.Write("Enter worker id: ");
                        int workerId = Int32.Parse(Console.ReadLine());

                        Worker worker = db.Workers.First(rec => rec.Id == workerId);
                        db.Workers.Remove(worker);
                        db.SaveChanges();

                        break;
                    }

                case "worktype":
                    {
                        Console.Write("Enter work type: ");
                        string workTypeName = Console.ReadLine();

                        WorkType workType = db.WorkTypes.First(rec => rec.Name == workTypeName);
                        db.WorkTypes.Remove(workType);
                        db.SaveChanges();

                        break;
                    }

                default:
                    {
                        Console.WriteLine("Unknown entity: " + entity);
                        break;
                    }
            }
        }
    }
}

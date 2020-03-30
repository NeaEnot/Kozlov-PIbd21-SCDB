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
                        break;
                    case "delete":
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

                            Brigade brigade = new Brigade() { WorkTypeId = db.WorkTypes.FirstOrDefault(rec => rec.Name == workTypeName).Id };

                            db.Add(brigade);
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

                            Order order = db.Orders.Where(rec => rec.Id == orderId).FirstOrDefault();
                            if (order == null)
                            {
                                throw new Exception("Order with same Id isn't exist");
                            }

                            MaterialsSet materialsSet =
                                new MaterialsSet()
                                {
                                    Count = materialsCount,
                                    MaterialsTypeId = db.MaterialsTypes.FirstOrDefault(rec => rec.Name == materialsTypeName).Id,
                                    OrderId = orderId
                                };

                            db.Add(materialsSet);
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

                            db.Add(materialsType);
                            db.SaveChanges();

                            break;
                        }

                    case "order":
                        {
                            Console.WriteLine("Needed mounths: ");
                            int mounthCount = Int32.Parse(Console.ReadLine());

                            DateTime registrationDate = DateTime.Now.Date;
                            DateTime deliveryDate = registrationDate.AddMonths(mounthCount);

                            Order order =
                                new Order()
                                {
                                    RegistrationDate = registrationDate,
                                    DeliveryDate = deliveryDate
                                };

                            db.Add(order);
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

                            db.Add(position);
                            db.SaveChanges();

                            break;
                        }

                    case "work":
                        {
                            Console.WriteLine("Work type: ");
                            string workTypeName = Console.ReadLine();

                            Console.WriteLine("Order id: ");
                            int orderId = Int32.Parse(Console.ReadLine());

                            Order order = db.Orders.FirstOrDefault(rec => rec.Id == orderId);
                            if (order == null)
                            {
                                throw new Exception("Order with same Id isn't exist");
                            }

                            Work work =
                                new Work()
                                {
                                    WorkTypeId = db.WorkTypes.FirstOrDefault(rec => rec.Name == workTypeName).Id,
                                    OrderId = orderId
                                };

                            db.Add(work);
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

                            Brigade brigade = db.Brigades.FirstOrDefault(rec => rec.Id == brigadeId);
                            if (brigade == null)
                            {
                                throw new Exception("Order with same Id isn't exist");
                            }

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
                                    PositionId = db.Positions.FirstOrDefault(rec => rec.Name == positionName).Id,
                                    BrigadeId = brigadeId
                                };

                            db.Add(worker);
                            db.SaveChanges();

                            break;
                        }

                    case "worktype":
                        {
                            Console.WriteLine("Work type name: ");
                            string workTypeName = Console.ReadLine();

                            WorkType workType = new WorkType() { Name = workTypeName };

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
                            .Select(rec => new Brigade
                            {
                                Id = rec.Id,
                                WorkTypeId = rec.WorkTypeId,
                                OrderId = rec.OrderId == null ? null : rec.OrderId,
                                WorkType = db.WorkTypes.Include(recWT => recWT.Id == rec.WorkTypeId).FirstOrDefault()
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
                            .Select(rec => new MaterialsSet
                            {
                                Id = rec.Id,
                                Count = rec.Count,
                                MaterialsTypeId = rec.MaterialsTypeId,
                                OrderId = rec.OrderId,
                                MaterialsType = db.MaterialsTypes.Include(recMT => recMT.Id == rec.MaterialsTypeId).FirstOrDefault()
                            });

                        Console.WriteLine("Materials type\tCount");
                        Console.WriteLine();
                        foreach (var set in materialsSets)
                        {
                            Console.WriteLine("{0}\t{1}", set.MaterialsType.Name, set.Count);
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
                            .Select(rec => new Work
                            {
                                Id = rec.Id,
                                WorkTypeId = rec.WorkTypeId,
                                OrderId = rec.OrderId,
                                WorkType = db.WorkTypes.Include(recWT => recWT.Id == rec.WorkTypeId).FirstOrDefault()
                            });

                        Console.WriteLine("Work");
                        Console.WriteLine();
                        foreach (var work in works)
                        {
                            Console.WriteLine(work.WorkType.Name);
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
                                Position = db.Positions.Include(recP => recP.Id == rec.PositionId).FirstOrDefault()
                            });

                        Console.WriteLine("Worker\tPosition\tAge\tAdmission date");
                        Console.WriteLine();
                        foreach (var worker in workers)
                        {
                            Console.WriteLine("{0} {1}.{2}.\t{3}\t{4}\t{5}", worker.SecondName, worker.FirstName[0], worker.LastName[0], DateTime.Now.Year - worker.BirthDate.Year, worker.AdmissionDate.ToString("dd:MM:yyyy"));
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
    }
}

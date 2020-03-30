using Lab5.Models;
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
    }
}

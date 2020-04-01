using Lab5.Models;
using System;
using System.Linq;

namespace Lab5
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] times = new int[15];

            /*
             * Работать будем с заказами, бригадами и работниками
             */

            times[0] = ScriptInsert0();
            times[1] = ScriptInsert1();
            times[2] = ScriptInsert2();
            times[3] = ScriptRead0();
            times[4] = ScriptRead1();
            times[5] = ScriptRead2();
            times[6] = ScriptUpdate0();
            times[7] = ScriptUpdate1();
            times[8] = ScriptUpdate2();
            times[9] = ScriptCustom0();
            times[10] = ScriptCustom1();
            times[11] = ScriptCustom2();
            times[12] = ScriptDelete2();
            times[13] = ScriptDelete1();
            times[14] = ScriptDelete0();
        }

        static int ScriptInsert0()
        {
            Order model = new Order() { RegistrationDate = DateTime.Now.Date, DeliveryDate = DateTime.Now.AddMonths(4).Date };

            DateTime startTime = DateTime.Now;
            Service.CreateOrder(model);
            DateTime finishTime = DateTime.Now;

            return (int)(finishTime - startTime).TotalMilliseconds;
        }

        static int ScriptInsert1()
        {
            var order = Service.ReadOrders(new Order() { RegistrationDate = DateTime.Now.Date }, 1, 0).First();
            Brigade model = new Brigade() { WorkTypeId = 10, OrderId = order.Id };

            DateTime startTime = DateTime.Now;
            Service.CreateBrigade(model);
            DateTime finishTime = DateTime.Now;

            return (int)(finishTime - startTime).TotalMilliseconds;
        }

        static int ScriptInsert2()
        {
            var order = Service.ReadOrders(new Order() { RegistrationDate = DateTime.Now.Date }, 1, 0).First();
            var brigade = Service.ReadBrigades(new Brigade() { OrderId = order.Id }, 1, 0).First();

            Worker[] workers = new Worker[30];
            for (int i = 0; i < workers.Length; i++)
            {
                workers[i] =
                    new Worker()
                    {
                        FirstName = i.ToString(),
                        SecondName = "ScriptInsert2",
                        LastName = "Lab5",
                        BirthDate = DateTime.Now.Date,
                        AdmissionDate = DateTime.Now.Date,
                        PositionId = 7,
                        BrigadeId = brigade.Id
                    };
            }

            DateTime startTime = DateTime.Now;
            foreach (var model in workers)
            {
                Service.CreateWorker(model);
            }
            DateTime finishTime = DateTime.Now;

            return (int)(finishTime - startTime).TotalMilliseconds;
        }

        static int ScriptRead0()
        {
            Order model = new Order() { RegistrationDate = DateTime.Now.Date };

            DateTime startTime = DateTime.Now;
            Order order = Service.ReadOrders(model, 1, 0).First();
            DateTime finishTime = DateTime.Now;

            Console.WriteLine("{0}: {1} - {2}", order.Id, order.RegistrationDate.ToString("dd.MM.yyyy"), order.DeliveryDate.ToString("dd.MM.yyyy"));

            return (int)(finishTime - startTime).TotalMilliseconds;
        }

        static int ScriptRead1()
        {
            Order order = Service.ReadOrders(new Order() { RegistrationDate = DateTime.Now.Date }, 1, 0).First();
            Brigade model = new Brigade() { OrderId = order.Id };

            DateTime startTime = DateTime.Now;
            Brigade brigade = Service.ReadBrigades(model, 1, 0).First();
            DateTime finishTime = DateTime.Now;

            Console.WriteLine("{0}: {1}", brigade.Id, brigade.Workers.Count);

            return (int)(finishTime - startTime).TotalMilliseconds;
        }

        static int ScriptRead2()
        {
            Order order = Service.ReadOrders(new Order() { RegistrationDate = DateTime.Now.Date }, 1, 0).First();
            Brigade brigade = Service.ReadBrigades(new Brigade() { OrderId = order.Id }, 1, 0).First();
            Worker model = new Worker() { BrigadeId = brigade.Id };

            DateTime startTime = DateTime.Now;
            var workers = Service.ReadWorkers(model);
            DateTime finishTime = DateTime.Now;

            foreach (var worker in workers)
            {
                Console.WriteLine("{0} {1} {2}", worker.SecondName, worker.FirstName, worker.LastName);
            }

            return (int)(finishTime - startTime).TotalMilliseconds;
        }

        static int ScriptUpdate0()
        {
            Order order = Service.ReadOrders(new Order() { RegistrationDate = DateTime.Now.Date }, 1, 0).First();
            Order model = new Order() { Id = order.Id, DeliveryDate = order.DeliveryDate.AddMonths(4).Date };

            DateTime startTime = DateTime.Now;
            Service.UpdateOrder(model);
            DateTime finishTime = DateTime.Now;

            return (int)(finishTime - startTime).TotalMilliseconds;
        }

        static int ScriptUpdate1()
        {
            Order order = Service.ReadOrders(new Order() { RegistrationDate = DateTime.Now.Date }, 1, 0).First();
            Brigade brigade = Service.ReadBrigades(new Brigade() { OrderId = order.Id }, 1, 0).First();
            Brigade model = new Brigade() { Id = brigade.Id, WorkTypeId = 11 };

            DateTime startTime = DateTime.Now;
            Service.UpdateBrigade(model);
            DateTime finishTime = DateTime.Now;

            return (int)(finishTime - startTime).TotalMilliseconds;
        }

        static int ScriptUpdate2()
        {
            Order order = Service.ReadOrders(new Order() { RegistrationDate = DateTime.Now.Date }, 1, 0).First();
            Brigade brigade = Service.ReadBrigades(new Brigade() { OrderId = order.Id }, 1, 0).First();
            var workers = Service.ReadWorkers(new Worker() { BrigadeId = brigade.Id });

            DateTime startTime = DateTime.Now;
            foreach (var model in workers)
            {
                model.PositionId = 8;
                Service.UpdateWorker(model);
            }
            DateTime finishTime = DateTime.Now;

            return (int)(finishTime - startTime).TotalMilliseconds;
        }

        static int ScriptDelete0()
        {
            Order order = Service.ReadOrders(new Order() { RegistrationDate = DateTime.Now.Date }, 1, 0).First();
            Order model = new Order() { Id = order.Id };

            DateTime startTime = DateTime.Now;
            Service.DeleteOrder(model);
            DateTime finishTime = DateTime.Now;

            return (int)(finishTime - startTime).TotalMilliseconds;
        }

        static int ScriptDelete1()
        {
            Order order = Service.ReadOrders(new Order() { RegistrationDate = DateTime.Now.Date }, 1, 0).First();
            Brigade brigade = Service.ReadBrigades(new Brigade() { OrderId = order.Id }, 1, 0).First();
            Brigade model = new Brigade() { Id = brigade.Id };

            DateTime startTime = DateTime.Now;
            Service.DeleteBrigade(model);
            DateTime finishTime = DateTime.Now;

            return (int)(finishTime - startTime).TotalMilliseconds;
        }

        static int ScriptDelete2()
        {
            Order order = Service.ReadOrders(new Order() { RegistrationDate = DateTime.Now.Date }, 1, 0).First();
            Brigade brigade = Service.ReadBrigades(new Brigade() { OrderId = order.Id }, 1, 0).First();
            var workers = Service.ReadWorkers(new Worker() { BrigadeId = brigade.Id });

            DateTime startTime = DateTime.Now;
            foreach (var model in workers)
            {
                Service.DeleteWorker(model);
            }
            DateTime finishTime = DateTime.Now;

            return (int)(finishTime - startTime).TotalMilliseconds;
        }

        static int ScriptCustom0()
        {
            Order order = Service.ReadOrders(new Order() { RegistrationDate = DateTime.Now.Date }, 1, 0).First();

            DateTime startTime = DateTime.Now;
            var brigades = Service.ReadBrigades(new Brigade() { OrderId = order.Id });
            foreach (var brigade in brigades)
            {
                foreach (var worker in brigade.Workers)
                {
                    Console.WriteLine("{0} {1} {2} ", worker.SecondName, worker.FirstName, worker.LastName);
                }
            }
            DateTime finishTime = DateTime.Now;

            return (int)(finishTime - startTime).TotalMilliseconds;
        }

        static int ScriptCustom1()
        {
            DateTime startTime = DateTime.Now;
            var orders = Service.ReadOrders(new Order() { DeliveryDate = DateTime.Now.Date });
            foreach (var order in orders)
            {
                Console.WriteLine("{0}: {1} - {2} ", order.Id, order.RegistrationDate.ToString("dd.MM.yyyy"), order.DeliveryDate.ToString("dd.MM.yyyy"));
            }
            DateTime finishTime = DateTime.Now;

            return (int)(finishTime - startTime).TotalMilliseconds;
        }

        static int ScriptCustom2()
        {
            DateTime startTime = DateTime.Now;
            var materials = Service.ReadMaxUsedMaterials();
            Console.WriteLine("{0}: {1}", materials.Item1, materials.Item2);
            DateTime finishTime = DateTime.Now;

            return (int)(finishTime - startTime).TotalMilliseconds;
        }
    }
}

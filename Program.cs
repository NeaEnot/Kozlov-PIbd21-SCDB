using Lab5.Models;
using Lab5.Service;
using System;
using System.Linq;

namespace Lab5
{
    class Program
    {
        public static readonly DatabaseContext DB = new DatabaseContext("localhost", "5432", "postgres", "constructioncompany");

        static void Main(string[] args)
        {
            int[,] summaryTable = new int[11, 15];

            for (int i = 0; i < 10; i++)
            {
                int[] times = test();
                for (int j = 0; j < 15; j++)
                {
                    summaryTable[i, j] = times[j];
                }
            }

            for (int i = 0; i < 15; i++)
            {
                int min = Int32.MaxValue;
                for (int j = 0; j < 10; j++)
                {
                    if (summaryTable[j, i] < min)
                    {
                        min = summaryTable[j, i];
                    }
                }

                summaryTable[10, i] = min;
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("ScriptInsert0: " + summaryTable[10, 0]);
            Console.WriteLine("ScriptInsert1: " + summaryTable[10, 1]);
            Console.WriteLine("ScriptInsert2: " + summaryTable[10, 2]);
            Console.WriteLine("ScriptRead0: " + summaryTable[10, 3]);
            Console.WriteLine("ScriptRead1: " + summaryTable[10, 4]);
            Console.WriteLine("ScriptRead2: " + summaryTable[10, 5]);
            Console.WriteLine("ScriptUpdate0: " + summaryTable[10, 6]);
            Console.WriteLine("ScriptUpdate1: " + summaryTable[10, 7]);
            Console.WriteLine("ScriptUpdate2: " + summaryTable[10, 8]);
            Console.WriteLine("ScriptCustom0: " + summaryTable[10, 9]);
            Console.WriteLine("ScriptCustom1: " + summaryTable[10, 10]);
            Console.WriteLine("ScriptCustom2: " + summaryTable[10, 11]);
            Console.WriteLine("ScriptDelete0: " + summaryTable[10, 14]);
            Console.WriteLine("ScriptDelete1: " + summaryTable[10, 13]);
            Console.WriteLine("ScriptDelete2: " + summaryTable[10, 12]);
        }

        static int[] test()
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

            return times;
        }

        static int ScriptInsert0()
        {
            Order model = new Order() { RegistrationDate = DateTime.Now.Date, DeliveryDate = DateTime.Now.AddMonths(4).Date };

            DateTime startTime = DateTime.Now;
            OrderService.Create(model);
            DateTime finishTime = DateTime.Now;

            return (int)(finishTime - startTime).TotalMilliseconds;
        }

        static int ScriptInsert1()
        {
            var order = OrderService.Read(new Order() { RegistrationDate = DateTime.Now.Date }, 1, 0).First();

            // Предполагается, что действия до создания модели - это моделирование выбора пользователя

            Brigade model = new Brigade() { WorkTypeId = 10, OrderId = order.Id };

            DateTime startTime = DateTime.Now;
            BrigadeService.Create(model);
            DateTime finishTime = DateTime.Now;

            return (int)(finishTime - startTime).TotalMilliseconds;
        }

        static int ScriptInsert2()
        {
            var order = OrderService.Read(new Order() { RegistrationDate = DateTime.Now.Date }, 1, 0).First();
            var brigade = BrigadeService.Read(new Brigade() { OrderId = order.Id }, 1, 0).First();

            // Предполагается, что действия до создания модели - это моделирование выбора пользователя

            Worker[] models = new Worker[30];
            for (int i = 0; i < models.Length; i++)
            {
                models[i] =
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
            foreach (var model in models)
            {
                WorkerService.Create(model);
            }
            DateTime finishTime = DateTime.Now;

            return (int)(finishTime - startTime).TotalMilliseconds;
        }

        static int ScriptRead0()
        {
            Order model = new Order() { RegistrationDate = DateTime.Now.Date };

            DateTime startTime = DateTime.Now;
            Order order = OrderService.Read(model, 1, 0).First();
            DateTime finishTime = DateTime.Now;

            Console.WriteLine("{0}: {1} - {2}", order.Id, order.RegistrationDate.Value.ToString("dd.MM.yyyy"), order.DeliveryDate.Value.ToString("dd.MM.yyyy"));

            return (int)(finishTime - startTime).TotalMilliseconds;
        }

        static int ScriptRead1()
        {
            Order order = OrderService.Read(new Order() { RegistrationDate = DateTime.Now.Date }, 1, 0).First();

            // Предполагается, что действия до создания модели - это моделирование выбора пользователя

            Brigade model = new Brigade() { OrderId = order.Id };

            DateTime startTime = DateTime.Now;
            Brigade brigade = BrigadeService.Read(model, 1, 0).First();
            DateTime finishTime = DateTime.Now;

            Console.WriteLine("{0}: {1}", brigade.Id, brigade.Workers.Count);

            return (int)(finishTime - startTime).TotalMilliseconds;
        }

        static int ScriptRead2()
        {
            Order order = OrderService.Read(new Order() { RegistrationDate = DateTime.Now.Date }, 1, 0).First();
            Brigade brigade = BrigadeService.Read(new Brigade() { OrderId = order.Id }, 1, 0).First();

            // Предполагается, что действия до создания модели - это моделирование выбора пользователя

            Worker model = new Worker() { BrigadeId = brigade.Id };

            DateTime startTime = DateTime.Now;
            var models = WorkerService.Read(model);
            DateTime finishTime = DateTime.Now;

            foreach (var worker in models)
            {
                Console.WriteLine("{0} {1} {2}", worker.SecondName, worker.FirstName, worker.LastName);
            }

            return (int)(finishTime - startTime).TotalMilliseconds;
        }

        static int ScriptUpdate0()
        {
            Order order = OrderService.Read(new Order() { RegistrationDate = DateTime.Now.Date }, 1, 0).First();

            // Предполагается, что действия до создания модели - это моделирование выбора пользователя

            Order model = new Order() { Id = order.Id, DeliveryDate = order.DeliveryDate.Value.AddMonths(4).Date };

            DateTime startTime = DateTime.Now;
            OrderService.Update(model);
            DateTime finishTime = DateTime.Now;

            return (int)(finishTime - startTime).TotalMilliseconds;
        }

        static int ScriptUpdate1()
        {
            Order order = OrderService.Read(new Order() { RegistrationDate = DateTime.Now.Date }, 1, 0).First();
            Brigade brigade = BrigadeService.Read(new Brigade() { OrderId = order.Id }, 1, 0).First();

            // Предполагается, что действия до создания модели - это моделирование выбора пользователя

            Brigade model = new Brigade() { Id = brigade.Id, WorkTypeId = 11 };

            DateTime startTime = DateTime.Now;
            BrigadeService.Update(model);
            DateTime finishTime = DateTime.Now;

            return (int)(finishTime - startTime).TotalMilliseconds;
        }

        static int ScriptUpdate2()
        {
            Order order = OrderService.Read(new Order() { RegistrationDate = DateTime.Now.Date }, 1, 0).First();
            Brigade brigade = BrigadeService.Read(new Brigade() { OrderId = order.Id }, 1, 0).First();

            // Предполагается, что действия до создания модели - это моделирование выбора пользователя

            var models = WorkerService.Read(new Worker() { BrigadeId = brigade.Id });

            DateTime startTime = DateTime.Now;
            foreach (var model in models)
            {
                model.PositionId = 8;
                WorkerService.Update(model);
            }
            DateTime finishTime = DateTime.Now;

            return (int)(finishTime - startTime).TotalMilliseconds;
        }

        static int ScriptDelete0()
        {
            Order order = OrderService.Read(new Order() { RegistrationDate = DateTime.Now.Date }, 1, 0).First();

            // Предполагается, что действия до создания модели - это моделирование выбора пользователя

            Order model = new Order() { Id = order.Id };

            DateTime startTime = DateTime.Now;
            OrderService.Delete(model);
            DateTime finishTime = DateTime.Now;

            return (int)(finishTime - startTime).TotalMilliseconds;
        }

        static int ScriptDelete1()
        {
            Order order = OrderService.Read(new Order() { RegistrationDate = DateTime.Now.Date }, 1, 0).First();
            Brigade brigade = BrigadeService.Read(new Brigade() { OrderId = order.Id }, 1, 0).First();

            // Предполагается, что действия до создания модели - это моделирование выбора пользователя

            Brigade model = new Brigade() { Id = brigade.Id };

            DateTime startTime = DateTime.Now;
            BrigadeService.Delete(model);
            DateTime finishTime = DateTime.Now;

            return (int)(finishTime - startTime).TotalMilliseconds;
        }

        static int ScriptDelete2()
        {
            Order order = OrderService.Read(new Order() { RegistrationDate = DateTime.Now.Date }, 1, 0).First();
            Brigade brigade = BrigadeService.Read(new Brigade() { OrderId = order.Id }, 1, 0).First();

            // Предполагается, что действия до создания модели - это моделирование выбора пользователя

            var models = WorkerService.Read(new Worker() { BrigadeId = brigade.Id });

            DateTime startTime = DateTime.Now;
            foreach (var model in models)
            {
                WorkerService.Delete(model);
            }
            DateTime finishTime = DateTime.Now;

            return (int)(finishTime - startTime).TotalMilliseconds;
        }

        static int ScriptCustom0()
        {
            Order order = OrderService.Read(new Order() { RegistrationDate = DateTime.Now.Date }, 1, 0).First();

            // Предполагается, что действия до создания модели - это моделирование выбора пользователя

            Brigade model = new Brigade() { OrderId = order.Id };

            DateTime startTime = DateTime.Now;
            var brigades = BrigadeService.Read(model);
            foreach (var brigade in brigades)
            {
                foreach (var worker in brigade.Workers)
                {
                    Console.WriteLine("{0} {1} {2}", worker.SecondName, worker.FirstName, worker.LastName);
                }
            }
            DateTime finishTime = DateTime.Now;

            return (int)(finishTime - startTime).TotalMilliseconds;
        }

        static int ScriptCustom1()
        {
            DateTime startTime = DateTime.Now;
            var orders = OrderService.Read(new Order() { DeliveryDate = DateTime.Now.Date });
            foreach (var order in orders)
            {
                Console.WriteLine("{0}: {1} - {2} ", order.Id, order.RegistrationDate.Value.ToString("dd.MM.yyyy"), order.DeliveryDate.Value.ToString("dd.MM.yyyy"));
            }
            DateTime finishTime = DateTime.Now;

            return (int)(finishTime - startTime).TotalMilliseconds;
        }

        static int ScriptCustom2()
        {
            DateTime startTime = DateTime.Now;
            var materials = MaterialsSetService.ReadMaxUsedMaterials();
            Console.WriteLine("{0}: {1}", materials.Item1, materials.Item2);
            DateTime finishTime = DateTime.Now;

            return (int)(finishTime - startTime).TotalMilliseconds;
        }
    }
}

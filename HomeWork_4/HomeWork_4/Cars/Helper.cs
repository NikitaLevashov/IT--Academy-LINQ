using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_4
{
    class Helper
    {
        public static async Task<List<Car>> GetCar()
        {
            List<Car> cars;

            using (var sr = new StreamReader("cars.json"))
            {
                var json = await sr.ReadToEndAsync();
                cars = JsonConvert.DeserializeObject<List<Car>>(json);
            }

            return cars;

        }

        public static async Task<List<Customer>> GetCustomer()
        {
            List<Customer> customers;

            using (var sr = new StreamReader("customer.json"))
            {
                var json = await sr.ReadToEndAsync();
                customers = JsonConvert.DeserializeObject<List<Customer>>(json);
            }

            return customers;

        }

        public static async Task JoinCarCustomer()
        {
            Random rnd = new Random();

            string writePath = Path.Combine(Directory.GetCurrentDirectory(), "JoinCarCustomer.txt");

            var cars = await Helper.GetCar();
            var customers = await Helper.GetCustomer();

            cars.OrderBy(x => rnd.Next()).Take(100);
            customers.OrderBy(x => rnd.Next()).Take(100);

            var query = from car in cars
                        join customer in customers
                        on car.CarModel equals customer.CarModel
                        select new
                        {
                            car.CarModel,
                            customer.FirstName,
                            customer.LastName,
                            customer.Phone
                        };

            foreach (var item in query)
            {
                Console.WriteLine($"{item.CarModel} ||  {item.FirstName} {item.LastName} || {item.Phone} ");

                using (StreamWriter sw = new StreamWriter(writePath, true, Encoding.Default))
                {
                    sw.WriteLine($"{ item.CarModel} ||  { item.FirstName} || { item.LastName} || { item.Phone} ");
                }
            }

        }


        public static async Task CustomerShowCar()
        {
           
            string writePath = Path.Combine(Directory.GetCurrentDirectory(), "CustomerShowCar.txt");

            var cars = await Helper.GetCar();
            var customers = await Helper.GetCustomer();

            var query = from car in cars
                        join customer in customers
                        on car.CarModel equals customer.CarModel
                        select new
                        {
                            car.CarModel,
                            car.CarMake,
                            car.CarYear,
                            car.Color,
                            car.Cost,
                            customer.FirstName,
                            customer.LastName,
                            
                        };

            foreach (var item in query)
            {
                if (item.FirstName + " " + item.LastName == "Natassia Woollacott")
                    Console.WriteLine($"{item.FirstName} {item.LastName} =======> {item.CarMake} || {item.CarModel} || Cars color = {item.Color} || {item.CarYear} || {item.Cost}$");

                using (StreamWriter sw = new StreamWriter(writePath, true, Encoding.Default))
                {
                    sw.WriteLine($"{ item.FirstName} { item.LastName} =======> { item.CarMake} || { item.CarModel} { item.CarYear} || { item.Color} || { item.Cost}$");
                }
            }

        }

        public static async Task ShowCountCar()
        {
           
            string writePath = Path.Combine(Directory.GetCurrentDirectory(), "ShowCountCar.txt");

            var cars = await Helper.GetCar();

            var query = cars.GroupBy(x => x.CarModel).Select(x=> new { Model = x.Key, Count = x.Count()});


            foreach (var item in query)
            {
                Console.WriteLine($"{item.Model}: {item.Count}");

                
                using (StreamWriter sw = new StreamWriter(writePath, true, Encoding.Default))
                {
                    sw.WriteLine($"{item.Model}: {item.Count}");
                }
            }

        }

        public static async Task ShowSumCar()
        {

            string writePath = Path.Combine(Directory.GetCurrentDirectory(), "ShowSumCar.txt");

            var cars = await Helper.GetCar();

            int sum = cars.Sum(x => x.Cost);

            Console.WriteLine($"Oбщая стоимость авто в автосалоне составляет {sum}$");


        }

        public static async Task MaxMinYearCar()
        {

            string writePath = Path.Combine(Directory.GetCurrentDirectory(), "ShowSumCar.txt");

            var cars = await Helper.GetCar();
            
            var min = cars.Where(x => x.CarYear == cars.Min(x => x.CarYear));
            var max = cars.Where(x => x.CarYear == cars.Max(x => x.CarYear));

            Console.WriteLine("Машинs с минимальным годом выпуска : ");
            
            foreach(var item in min)
            {
                Console.WriteLine(item.CarYear);
                Console.WriteLine($" {item.CarMake} {item.CarModel} ");
            }

             
            Console.WriteLine("Машинs с максимальным годом выпуска: ");

            foreach (var item in max)
            {
                Console.WriteLine(item.CarYear);
                Console.WriteLine($" {item.CarMake} {item.CarModel} ");
            }
             
        }

        public static async Task ShowCustomerCar()
        {

            string writePath = Path.Combine(Directory.GetCurrentDirectory(), "ShowCustomerCar.txt");

            var cars = await Helper.GetCar();
            var customers = await Helper.GetCustomer();

            var query = from car in cars
                        join customer in customers
                        on car.CarModel equals customer.CarModel
                        select new
                        {
                            car.CarModel,
                            customer.FirstName,
                            customer.LastName,
                            car.CarMake
                        };

            //var query = from car in cars
            //             from customer in customers
            //             select new { Name = customer.FirstName+ " " +customer.LastName, Car = car.CarMake + " " + car.CarModel };

            var newquery = query.GroupBy(x => x.FirstName + " " + x.LastName).ToList();
            foreach (var item in newquery)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(item.Key);
                Console.ResetColor();

                foreach (var r in item)
                {
                    Console.WriteLine(r.CarMake + " " + r.CarModel);
                }
            }

        }

        
    }
}

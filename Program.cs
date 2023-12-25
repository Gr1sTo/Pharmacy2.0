using Pharmacy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Pharmacy.src.Models;

class Program
{
    public static readonly object locker = new object();
    public static SemaphoreSlim semaphore = new SemaphoreSlim(1);
    public static Mutex mutex = new Mutex();

    static async Task Main(string[] args)
    {
        var builder = new ConfigurationBuilder();

        //Встановлюємо шлях до поточного каталогу
        builder.SetBasePath(Directory.GetCurrentDirectory());

        //Отримуємо конфігурацію з appsettings.json
        builder.AddJsonFile("appsettings.json");

        //Створюємо конфігурацію
        var config = builder.Build();

        //Отримуємо строку підключення
        String connectionString = config.GetConnectionString("DefaultConnection");

        var optionsBuilder = new DbContextOptionsBuilder<PharmacyContext>();
        var options = optionsBuilder.UseSqlServer(connectionString).Options;

        using (PharmacyContext context = new PharmacyContext(options))
        {
            //Функція для внесення базових даних до бази даних
            void StartData()
            {
                
                // Додавання даних для Pharmacist
                var pharmacists = new List<Pharmacist>
                {
                    new Pharmacist { SurnameNamePatronymic = "Коваленко Андрый Іванович" },
                    new Pharmacist { SurnameNamePatronymic = "Кузьма Марія Петрівна" }
                };
                context.Pharmacists.AddRange(pharmacists);
                context.SaveChanges();
               /*// Додавання даних для Producer
                var producers = new List<Producer>
                {
                    new Producer { Name = "Лікарняний Виробник", LicenseNum = 12345 },
                    new Producer { Name = "Здоров'я Фарма", LicenseNum = 67890 }
                };
                context.Producers.AddRange(producers);
                context.SaveChanges();
                // Додавання даних для Medicine
                var medicines = new List<Medicine>
                {
                    new Medicine { Name = "Аспірин", ActiveSubstance = "Ацетилсаліцилова кислота", Country = "Україна", ExpirationDate = new DateTime(2025, 12, 31), MethodOfAdministration = "Орально", ReleaseForm = "Таблетки", CntInPack = 20, ReceipeNeed = false, Temperature = 25, ProducerCode = 1, Packing = "Блістер" },
                    new Medicine { Name = "Парацетамол", ActiveSubstance = "Парацетамол", Country = "Україна", ExpirationDate = new DateTime(2024, 11, 30), MethodOfAdministration = "Орально", ReleaseForm = "Капсули", CntInPack = 30, ReceipeNeed = false, Temperature = 25, ProducerCode = 2, Packing = "Блістер" }
                };
                context.Medicines.AddRange(medicines);
                context.SaveChanges();
                // Додавання даних для Bill
                var bills = new List<Bill>
                {
                    new Bill { IDPharmacist = 1, DiscountCardID = 1, FinalPrice = 150.50M, TypeOfPay = "Готівка", DateOfBuy = new DateTime(2023, 11, 15), Place = "Аптека №1" },
                    new Bill { IDPharmacist = 2, DiscountCardID = null, FinalPrice = 200.00M, TypeOfPay = "Картка", DateOfBuy = new DateTime(2023, 11, 16), Place = "Аптека №2" }
                };
                context.Bills.AddRange(bills);
                context.SaveChanges();
                // Додавання даних для GoodsInStorage
                var goodsInStorages = new List<GoodsInStorage>
                {
                    new GoodsInStorage { Article = 101, ExpirationDate = new DateTime(2025, 12, 31), Availability = true, StorageId = 1 },
                    new GoodsInStorage { Article = 102, ExpirationDate = new DateTime(2024, 11, 30), Availability = true, StorageId = 1 }
                };
                //context.GoodsInStorages.AddRange(goodsInStorages);
                //context.SaveChanges();
                // Додавання даних для Shopper
                /*var shoppers = new List<Shopper>
                {
                    new Shopper { PhoneNum = "3801234567", DiscountValue = 0.10f, IDDiscountCard = "DC1001" },
                    new Shopper { PhoneNum = "3809876543", DiscountValue = 0.15f, IDDiscountCard = "DC1002" }
                };
                context.Shoppers.AddRange(shoppers);
                context.SaveChanges();*/
                // Додавання даних для Storage
                /*var storages = new List<Storage>
                {
                    new Storage { Type = "dry", ShelfCapacity = 100 },
                    new Storage { Type = "wet", ShelfCapacity = 50 }
                };
                context.Storages.AddRange(storages);
                context.SaveChanges();
                // Додавання даних для MedicineList
                var medicineLists = new List<MedicineList>
                {
                    new MedicineList { MedArticle = 101, Price = 10.00M, Count = 5, BillID = 1 },
                    new MedicineList { MedArticle = 102, Price = 15.00M, Count = 3, BillID = 1 }
                };
                context.MedicineLists.AddRange(medicineLists);
                context.SaveChanges();*/
            }
            //StartData();

            //Функція об'єднання
            void UnionBills()
            {
                // Вибірка рахунків з ціною більшою за певне значення
                var billsWithHighPrice = context.Bills
                    .Where(b => b.FinalPrice > 100);

                // Вибірка рахунків без вказаного фармацевта
                var billsWithoutPharmacist = context.Bills
                    .Where(b => b.IDPharmacist == null);

                // Об'єднання цих двох списків
                var result = billsWithHighPrice
                    .Union(billsWithoutPharmacist)
                    .ToList();

                Console.WriteLine("Result of Union:\n--------------------------------------------------");

                foreach (var bill in result)
                {
                    Console.WriteLine($"Bill ID: {bill.ID}, FinalPrice: {bill.FinalPrice}, IDPharmacist: {bill.IDPharmacist}");
                    Console.WriteLine("--------------------------------------------------");
                }
            }
            //UnionBills();

            //Функція виключення
            void ExceptMedicines()
            {
                // Вибірка ліків з температурою зберігання вище 25 градусів
                var medicinesHighTemperature = context.Medicines
                    .Where(m => m.Temperature > 24);

                // Вибірка ліків, що потребують рецепту
                var medicinesRequiringPrescription = context.Medicines
                    .Where(m => m.ReceipeNeed);

                // Виключення з першого списку ліків, що потребують рецепту
                var result = medicinesHighTemperature
                    .Except(medicinesRequiringPrescription)
                    .ToList();

                Console.WriteLine("Result of Except:\n--------------------------------------------------");

                foreach (var medicine in result)
                {
                    Console.WriteLine($"Medicine Article: {medicine.Article}, Name: {medicine.Name}, Temperature: {medicine.Temperature}, ReceipeNeed: {medicine.ReceipeNeed}");
                    Console.WriteLine("--------------------------------------------------");
                }
            }
            //ExceptMedicines();

            //Функція перетину
            void IntersectBills()
            {
                // Вибірка рахунків з фінальною ціною вище за певне значення
                var billsWithHighPrice = context.Bills
                    .Where(b => b.FinalPrice > 100);

                // Вибірка рахунків, створених на певну дату
                var billsFromDate = context.Bills
                    .Where(b => b.DateOfBuy.Date == new DateTime(2023, 12, 10));

                // Перетин цих двох списків
                var result = billsWithHighPrice
                    .Intersect(billsFromDate)
                    .ToList();

                Console.WriteLine("Result of Intersect:\n--------------------------------------------------");

                foreach (var bill in result)
                {
                    Console.WriteLine($"Bill ID: {bill.ID}, FinalPrice: {bill.FinalPrice}, DateOfBuy: {bill.DateOfBuy}");
                    Console.WriteLine("--------------------------------------------------");
                }
            }
            //IntersectBills();

            //Функція з'єднання
            void JoinMedicineAndGoodsInStorage()
            {
                var result = from medicine in context.Medicines
                             join goods in context.goodsInStorages
                             on medicine.Article equals goods.Article
                             select new
                             {
                                 MedicineName = medicine.Name,
                                 ExpirationDate = medicine.ExpirationDate,
                                 Availability = goods.Availability
                             };

                Console.WriteLine("Result of Join:\n-------------------------------------------------------------------------");

                foreach (var item in result)
                {
                    Console.WriteLine($"Medicine Name: {item.MedicineName}, Expiration Date: {item.ExpirationDate.ToShortDateString()}, Availability: {(item.Availability ? "Available" : "Not Available")}");
                    Console.WriteLine("-------------------------------------------------------------------------");
                }
            }
            //JoinMedicineAndGoodsInStorage();

            //Функція унікальних значень
            void DistinctProducers()
            {
                var result = context.Medicines
                                    .Select(m => m.ProducerCode)
                                    .Distinct()
                                    .ToList();

                Console.WriteLine("Result of Distinct:\n----------------------");

                foreach (var producerCode in result)
                {
                    if (producerCode.HasValue) // Перевірка на null
                    {
                        Console.WriteLine($"Unique Producer Code: {producerCode.Value}");
                    }
                    else
                    {
                        Console.WriteLine("Unique Producer Code: null");
                    }
                    Console.WriteLine("----------------------");
                }
            }
            //DistinctProducers();

            //Функція групування
            void GroupByPharmacist()
            {
                var bills = context.Bills.AsEnumerable(); // Отримання всіх даних рахунків з БД

                var result = bills
                                .GroupBy(b => b.IDPharmacist)
                                .ToList();

                foreach (var group in result)
                {
                    string pharmacistIdDisplay = group.Key.HasValue ? group.Key.ToString() : "No Pharmacist";
                    Console.WriteLine($"=====================================================\nPharmacist ID: {pharmacistIdDisplay}");

                    foreach (var bill in group)
                    {
                        Console.WriteLine("-----------------------------------------------------");
                        Console.WriteLine($"Bill ID: {bill.ID}, Final Price: {bill.FinalPrice}, Date of Buy: {bill.DateOfBuy}");
                    }
                }

                Console.WriteLine("=====================================================");
            }
            //GroupByPharmacist();

            //Функція кількості
            void CountMedicines()
            {
                var count = context.Medicines.Count();
                Console.WriteLine($"Total count of medicines: {count}");
            }
            //CountMedicines();

            //Функція суми
            void SumFinalPrices()
            {
                // Обчислення загальної суми фінальних цін усіх рахунків
                var totalPrice = context.Bills.Sum(b => b.FinalPrice);

                Console.WriteLine($"Total price of all bills: {totalPrice}");
            }
            //SumFinalPrices();

            //Функція середнього значення
            void AverageFinalPrices()
            {
                // Розрахунок середньої ціни ліків
                var averagePrice = context.Bills.Average(m => m.FinalPrice);

                Console.WriteLine($"Average medicine price: {averagePrice}");
            }
            //AverageFinalPrices();

            //Функція мінімального значення
            void MinFinalPrices()
            {
                // Розрахунок середньої ціни ліків
                var minPrice = context.Bills.Min(m => m.FinalPrice);

                Console.WriteLine($"Average medicine price: {minPrice}");
            }
            //MinFinalPrices();

            //Функція максимального значення
            void MaxFinalPrices()
            {
                // Розрахунок середньої ціни ліків
                var maxPrice = context.Bills.Max(m => m.FinalPrice);

                Console.WriteLine($"Average medicine price: {maxPrice}");
            }
            //MaxFinalPrices();

            //Функція жадібного завантаження
            void EagerLoadingBills()
            {
                // Жадібне завантаження фармацевтів для кожного рахунку
                var bills = context.Bills.Include(b => b.Pharmacist).ToList();

                Console.WriteLine("Result of Eager Loading:\n-------------------------------------------------------------------------------");

                foreach (var bill in bills)
                {
                    Console.WriteLine($"Bill ID: {bill.ID}, FinalPrice: {bill.FinalPrice}, Pharmacist: {bill.Pharmacist?.SurnameNamePatronymic}");
                    Console.WriteLine("-------------------------------------------------------------------------------");
                }
            }
            //EagerLoadingBills();

            //Функція явного завантаження
            void ExplicitLoading()
            {
                // Вибірка першого рахунку
                var bill = context.Bills.FirstOrDefault();

                // Явне завантаження даних про фармацевта, пов'язаного з рахунком
                context.Entry(bill).Reference(b => b.Pharmacist).Load();

                Console.WriteLine("Result of Explicit Loading:\n-------------------------------------------------------------------------------");

                // Перевірка на наявність даних про фармацевта
                if (bill.Pharmacist != null)
                {
                    Console.WriteLine($"Bill ID: {bill.ID}, Pharmacist: {bill.Pharmacist.SurnameNamePatronymic}");
                }
                else
                {
                    Console.WriteLine($"Bill ID: {bill.ID}, Pharmacist: [No Pharmacist Assigned]");
                }

                Console.WriteLine("-------------------------------------------------------------------------------");
            }
            //ExplicitLoading();

            //Функція лінивого завантаження
            void LazyLoading()
            {
                // Отримання першого рахунку
                var bill = context.Bills.FirstOrDefault();

                // Виведення інформації про рахунок та пов'язаного фармацевта
                Console.WriteLine($"Bill ID: {bill.ID}, Final Price: {bill.FinalPrice}, Pharmacist: {bill.Pharmacist?.SurnameNamePatronymic}");

                // Якщо фармацевт не прив'язаний до рахунку, то властивість Pharmacist буде null,
                // і інформація про фармацевта не виведеться.
            }
            //LazyLoading();

            //Функція роботи з невідслідковуваними даними
            void UntrackedBillData()
            {
                // Завантажуємо рахунок, який не відстежується
                var bill = context.Bills.AsNoTracking().FirstOrDefault(); // Це завантаження не буде відстежуватися

                // Внесення змін в завантажені дані, що не відстежуються
                if (bill != null)
                {
                    bill.FinalPrice = 200.00M; // Змінюємо фінальну ціну
                    bill.TypeOfPay = "Картка"; // Змінюємо тип оплати
                }

                // Attach для відстеження змін та їх оновлення
                context.Bills.Attach(bill); // Attach для відстеження об'єкта
                context.Entry(bill).State = EntityState.Modified; // Помічаємо, що об'єкт змінено

                // Оновлення змінених даних
                context.SaveChanges(); // Зберігаємо зміни до бази даних
            }
            //UntrackedBillData();

            //Функція виклику збереженої функції в SQL
            void UseSavedFunction()
            {
                int pharmacistId = 1; // ID фармацевта, рахунки якого ми хочемо знайти

                var bills = context.Bills.FromSqlRaw("SELECT * FROM dbo.GetBillsByPharmacist({0})", pharmacistId).ToList();

                Console.WriteLine("Result of Use Saved Function:\n---------------------------------------------------");

                foreach (var bill in bills)
                {
                    Console.WriteLine($"Bill ID: {bill.ID}, Pharmacist ID: {bill.IDPharmacist}, Final Price: {bill.FinalPrice}");
                    Console.WriteLine("---------------------------------------------------");
                }
            }
            //UseSavedFunction();

            //Функція виклику збереженої процедури в SQL
            void UseSavedProcedure()
            {
                var expirationDate = new DateTime(2024, 12, 31); // Припустима дата для пошуку ліків з закінченим терміном придатності

                var medicines = context.Medicines.FromSqlRaw("SELECT * FROM dbo.GetExpiredMedicines({0})", expirationDate).ToList();

                Console.WriteLine("Result of Use Saved Procedure:\n---------------------------------------------------");

                foreach (var medicine in medicines)
                {
                    Console.WriteLine($"Medicine Article: {medicine.Article}, Name: {medicine.Name}, Expiration Date: {medicine.ExpirationDate}");
                    Console.WriteLine("---------------------------------------------------");
                }
            }
            //UseSavedProcedure();

            //Захист
            //Тор 3 фармацепта по виписаним чекам
            void FindTopPharmacists()
            {
                var topPharmacists = context.Bills
                    .GroupBy(b => b.IDPharmacist)
                    .Select(group => new { PharmacistId = group.Key, BillsCount = group.Count() })
                    .OrderByDescending(x => x.BillsCount)
                    .Take(3)
                    .ToList();

                Console.WriteLine("Top 3 Pharmacists:\n");

                foreach (var pharmacist in topPharmacists)
                {
                    Console.WriteLine($"Pharmacist ID: {pharmacist.PharmacistId}, Number of Bills: {pharmacist.BillsCount}");
                }
            }
            //FindTopPharmacists();


            //----------------Lab4----------------
            //Threading

            Thread shopperThread1 = new Thread(GenerateShoppers);
            Thread shopperThread2 = new Thread(GenerateShoppers);

            shopperThread1.Start();
            shopperThread2.Start();

            shopperThread1.Join();
            shopperThread2.Join();

            Console.WriteLine("Збереження клiєнтiв за допомогою Threading завершено");

            Thread medicineThread1 = new Thread(GenerateMedicines);
            Thread medicineThread2 = new Thread(GenerateMedicines);

            medicineThread1.Start();
            medicineThread2.Start();

            medicineThread1.Join();
            medicineThread2.Join();

            Console.WriteLine("Збереження рiєлторів за допомогою Threading завершено");

            Thread producerThread1 = new Thread(GenerateProducers);
            Thread producerThread2 = new Thread(GenerateProducers);

            producerThread1.Start();
            producerThread2.Start();

            producerThread1.Join();
            producerThread2.Join();

            Console.WriteLine("Збереження власникiв за допомогою Threading завершено");

            Thread loadShopperThread = new Thread(DisplayShoppers);
            Thread loadMedicineThread = new Thread(DisplayMedicines);
            Thread loadProducerThread = new Thread(DisplayProducers);

            loadShopperThread.Start();
            loadMedicineThread.Start();
            loadProducerThread.Start();

            loadShopperThread.Join();
            loadMedicineThread.Join();
            loadProducerThread.Join();

            Console.WriteLine("Зчитування та вивiд клiєнтiв, власникiв та рiєлторiв завершено");

            //Створення та збереження покупців до бази даних за допомогою Monitor
            void GenerateShoppers()
            {
                for (int i = 0; i < 5; i++)
                {
                    Monitor.Enter(locker);
                    try
                    {
                        string shopperPhoneNum = "380" + i.ToString().PadLeft(9, '0');
                        float discountValue = (i % 5) * 0.05f;
                        string idDiscountCard = "DC" + i.ToString().PadLeft(4, '0');

                        var shopper = new Shopper { PhoneNum = shopperPhoneNum, DiscountValue = discountValue, IDDiscountCard = idDiscountCard };
                        context.Shoppers.Add(shopper);
                        context.SaveChanges();
                    }
                    finally
                    {
                        Monitor.Exit(locker);
                    }
                }
            }

            //Створення та збереження ліків до бази даних за допомогою Semaphore
            void GenerateMedicines()
            {
                for (int i = 0; i < 5; i++)
                {
                    semaphore.Wait();
                    try
                    {
                        int article = i;
                        string name = "MedicineName" + i;
                        string activeSubstance = "ActiveSubstance" + i;
                        string country = "Country" + i;
                        DateTime expirationDate = DateTime.Now.AddYears(1);
                        string methodOfAdministration = "Oral";
                        string releaseForm = "Tablet";
                        int cntInPack = 20;
                        bool receipeNeed = false;
                        int temperature = 25;

                        var medicine = new Medicine
                        {
                            Article = article,
                            Name = name,
                            ActiveSubstance = activeSubstance,
                            Country = country,
                            ExpirationDate = expirationDate,
                            MethodOfAdministration = methodOfAdministration,
                            ReleaseForm = releaseForm,
                            CntInPack = cntInPack,
                            ReceipeNeed = receipeNeed,
                            Temperature = temperature
                        };

                        context.Medicines.Add(medicine);
                        context.SaveChanges();
                    }
                    finally
                    {
                        semaphore.Release();
                    }
                }
            }

            // Створення та збереження виробників до бази даних за допомогою Mutex
            void GenerateProducers()
            {
                for (int i = 0; i < 5; i++)
                {
                    try
                    {
                        mutex.WaitOne();

                        var producer = new Producer
                        {
                            Code = i, // Припускаю, що Code - це унікальний ідентифікатор
                            Name = "ProducerName" + i,
                            LicenseNum = 1000 + i
                        };

                        context.Producers.Add(producer);
                        context.SaveChanges();
                    }
                    finally
                    {
                        mutex.ReleaseMutex();
                    }
                }
            }

            //Зчитування та вивід покупців до консолі
            void DisplayShoppers()
            {
                List<Shopper> shoppers;

                lock (context)
                {
                    shoppers = context.Shoppers.ToList();
                }

                Console.WriteLine("\nУсі покупці:\n-----------------------------");

                foreach (var shopper in shoppers)
                {
                    Console.WriteLine($"Покупець: {shopper.PhoneNum}\n-----------------------------");
                }
            }

            //Зчитування та вивід ліків до консолі
            void DisplayMedicines()
            {
                List<Medicine> medicines;

                lock (context)
                {
                    medicines = context.Medicines.ToList();
                }

                Console.WriteLine("\nУсі ліки:\n-----------------------------");

                foreach (var medicine in medicines)
                {
                    Console.WriteLine($"Ліки: {medicine.Name}, Країна: {medicine.Country}, Термін придатності: {medicine.ExpirationDate.ToShortDateString()}\n-----------------------------");
                }
            }

            //Зчитування та вивід виробників до консолі
            void DisplayProducers()
            {
                List<Producer> producers;

                lock (context)
                {
                    producers = context.Producers.ToList();
                }

                Console.WriteLine("\nУсі виробники:\n-----------------------------");

                foreach (var producer in producers)
                {
                    Console.WriteLine($"Виробник: {producer.Name}, Код: {producer.Code}\n-----------------------------");
                }
            }

            //Task

            List<Task> tasks = new List<Task>();

            // Створення Shoppers, Medicines та Producers
            for (int i = 1; i <= 3; i++)
            {
                int shopperIndex = i;
                int medicineIndex = i;
                int producerIndex = i;

                Task shopperTask = Task.Run(async () =>
                {
                    await SaveShopperAsync($"ShopperPhoneNum{shopperIndex}", $"ShopperDiscountCard{shopperIndex}");
                });

                Task medicineTask = Task.Run(async () =>
                {
                    await SaveMedicineAsync($"MedicineName{medicineIndex}",  
                        $"MedicineSubstance{medicineIndex}",
                        $"MedicineCountry{medicineIndex}");
                });

                Task producerTask = Task.Run(async () =>
                {
                    await SaveProducerAsync($"ProducerName{producerIndex}");
                });

                tasks.Add(shopperTask);
                tasks.Add(medicineTask);
                tasks.Add(producerTask);

                shopperTask.Start();
                medicineTask.Start();
                producerTask.Start();
            }

            await Task.WhenAll(tasks);
            Console.WriteLine("Дані збережено в БД");

            await DisplayShoppersAsync();
            await DisplayMedicinesAsync();
            await DisplayProducersAsync();
        }

        //Збереження покупців до бази даних
        static async Task SaveShopperAsync(string phoneNum, string iDDiscountCard)
        {
            await semaphore.WaitAsync();
            try
            {
                using (var context = new PharmacyContext())
                {
                    var shopper = new Shopper
                    {
                        PhoneNum = phoneNum,
                        IDDiscountCard = iDDiscountCard
                    };

                    context.Shoppers.Add(shopper);
                    await context.SaveChangesAsync();
                }
            }
            finally
            {
                semaphore.Release();
            }
        }

        //Збереження ліків до бази даних
        static async Task SaveMedicineAsync(string name, string activeSubstance, string country)
        {
            await semaphore.WaitAsync();
            try
            {
                using (var context = new PharmacyContext())
                {
                    context.Medicines.Add(new Medicine
                    {
                        Name = name,
                        ActiveSubstance = activeSubstance,
                        Country = country
                    });
                    await context.SaveChangesAsync();
                }
            }
            finally
            {
                semaphore.Release();
            }
        }

        // Збереження виробників до бази даних
        static async Task SaveProducerAsync(string name)
        {
            await semaphore.WaitAsync();
            try
            {
                using (var context = new PharmacyContext())
                {
                    context.Producers.Add(new Producer { Name = name});
                    await context.SaveChangesAsync();
                }
            }
            finally
            {
                semaphore.Release();
            }
        }

        // Зчитування та вивід покупців
        static async Task DisplayShoppersAsync()
        {
            using (var context = new PharmacyContext())
            {
                Task<List<Shopper>> shoppersTask = context.Shoppers.ToListAsync();

                Console.WriteLine("\nЧитання даних...\n");

                await shoppersTask;

                if (shoppersTask.IsCompletedSuccessfully)
                {
                    List<Shopper> shoppers = shoppersTask.Result;

                    Console.WriteLine("\nУсi покупцi:\n-----------------------------");

                    // Виведення покупців за допомогою паралельного виконання
                    var displayShopperTasks = shoppers.Select(async shopper =>
                    {
                        await Task.Yield(); //Для розподілу виконання в різних потоках

                        Console.WriteLine($"Номер телефону: {shopper.PhoneNum}, Дисконтна картка: {shopper.IDDiscountCard}\n-----------------------------");
                    });

                    await Task.WhenAll(displayShopperTasks);
                }
            }
        }

        // Зчитування та вивід ліків
        static async Task DisplayMedicinesAsync()
        {
            using (var context = new PharmacyContext())
            {
                Task<List<Medicine>> medicinesTask = context.Medicines.ToListAsync();

                Console.WriteLine("\nЧитання даних...\n");

                await medicinesTask;

                if (medicinesTask.IsCompletedSuccessfully)
                {
                    List<Medicine> medicines = medicinesTask.Result;

                    Console.WriteLine("\nУсi лiки:\n-----------------------------");

                    // Виведення ліків за допомогою паралельного виконання
                    var displayMedicineTasks = medicines.Select(async medicine =>
                    {
                        await Task.Yield(); //Для розподілу виконання в різних потоках

                        Console.WriteLine($"Ліки: {medicine.Name},Активна речовина: {medicine.ActiveSubstance} Артикул: {medicine.Article}\n-----------------------------");
                    });

                    await Task.WhenAll(displayMedicineTasks);
                }
            }
        }

        //Зчитування та вивід виробників
        static async Task DisplayProducersAsync()
        {
            using (var context = new PharmacyContext())
            {
                Task<List<Producer>> producersTask = context.Producers.ToListAsync();

                Console.WriteLine("\nЧитання даних...\n");

                await producersTask;

                if (producersTask.IsCompletedSuccessfully)
                {
                    List<Producer> producers = producersTask.Result;

                    Console.WriteLine("\nУсі виробники:\n-----------------------------");

                    // Виведення виробників за допомогою паралельного виконання
                    var displayProducerTasks = producers.Select(async producer =>
                    {
                        await Task.Yield(); //Для розподілу виконання в різних потоках

                        Console.WriteLine($"Виробник: {producer.Name}, Код: {producer.Code}\n-----------------------------");
                    });

                    await Task.WhenAll(displayProducerTasks);
                }
            }
        }

    }
}
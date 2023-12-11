using Pharmacy;
using Pharmacy.src;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Pharmacy.src.Models;

class Program
{
    static void Main()
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
                    new Pharmacist { SurnameNamePatronymic = "Іваненко Іван Іванович" },
                    new Pharmacist { SurnameNamePatronymic = "Петрова Марія Петрівна" }
                };
                context.Pharmacists.AddRange(pharmacists);
                context.SaveChanges();
                // Додавання даних для Producer
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
                var storages = new List<Storage>
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
                context.SaveChanges();
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
                int pharmacistId = 1; // ID фармацевта, рахунки-фактури якого ми хочемо знайти

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
        }
    }
}
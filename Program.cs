using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Pharmacy.src;
using Pharmacy.src.Models;

class Program
{
    static void Main(string[] args)
    {

        using (var context = new PharmacyContext(new DbContextOptionsBuilder<PharmacyContext>()
            .UseSqlServer("PharmacyDatabase").Options))
        {
            // Заповнення бази даних початковими значеннями
            SeedDatabase(context);

            // Приклади операцій з базою даних
            // Створення об'єкта
            CreatePharmacist(context, new Pharmacist { SurnameNamePatronymic = "Іванов Іван Іванович" });

            // Оновлення об'єкта
            UpdatePharmacist(context, 1, "Петров Петро Петрович");

            // Видалення об'єкта
            DeletePharmacist(context, 1);
            // Ваш код тут
        }

        /*using (var context = new PharmacyContext())
        {
            // Заповнення бази даних початковими значеннями
            SeedDatabase(context);

            // Приклади операцій з базою даних
            // Створення об'єкта
            CreatePharmacist(context, new Pharmacist { SurnameNamePatronymic = "Іванов Іван Іванович" });

            // Оновлення об'єкта
            UpdatePharmacist(context, 1, "Петров Петро Петрович");

            // Видалення об'єкта
            DeletePharmacist(context, 1);
        }*/
    }
    static void SeedDatabase(PharmacyContext context)
    {
        if (!context.Pharmacists.Any())
        {
            context.Pharmacists.Add(new Pharmacist { SurnameNamePatronymic = "Коваленко Олена Петрівна" });
            context.SaveChanges();
        }
    }

    static void CreatePharmacist(PharmacyContext context, Pharmacist pharmacist)
    {
        context.Pharmacists.Add(pharmacist);
        context.SaveChanges();
        Console.WriteLine("Фармацевт створений.");
    }

    static void UpdatePharmacist(PharmacyContext context, int id, string newName)
    {
        var pharmacist = context.Pharmacists.FirstOrDefault(p => p.ID == id);
        if (pharmacist != null)
        {
            pharmacist.SurnameNamePatronymic = newName;
            context.SaveChanges();
            Console.WriteLine("Фармацевт оновлений.");
        }
    }

    static void DeletePharmacist(PharmacyContext context, int id)
    {
        var pharmacist = context.Pharmacists.FirstOrDefault(p => p.ID == id);
        if (pharmacist != null)
        {
            context.Pharmacists.Remove(pharmacist);
            context.SaveChanges();
            Console.WriteLine("Фармацевт видалений.");
        }
    }
}
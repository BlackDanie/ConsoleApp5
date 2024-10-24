using System;
using System.Collections.Generic;

public class ArtExhibition
{
    // Приватні поля
    private string title;
    private string organizer;
    private DateTime startDate;
    private DateTime endDate;
    private List<string> artworks;

    // Публічні властивості
    public string Title
    {
        get { return title; }
        set { title = value; }
    }

    public string Organizer
    {
        get { return organizer; }
        set { organizer = value; }
    }

    public DateTime StartDate
    {
        get { return startDate; }
    }

    public DateTime EndDate
    {
        get { return endDate; }
    }

    public List<string> Artworks
    {
        get { return artworks; }
        set { artworks = value; }
    }

    // Конструктор за замовчуванням
    public ArtExhibition()
    {
        title = string.Empty;
        organizer = string.Empty;
        startDate = DateTime.MinValue;
        endDate = DateTime.MinValue;
        artworks = new List<string>();
    }

    // Параметризований конструктор
    public ArtExhibition(string title, string organizer, DateTime startDate, DateTime endDate)
    {
        if (endDate < startDate)
        {
            throw new ArgumentException("Дата завершення не може бути раніше дати початку.");
        }

        this.title = title;
        this.organizer = organizer;
        this.startDate = startDate;
        this.endDate = endDate;
        this.artworks = new List<string>();
    }

    // Конструктор копіювання
    public ArtExhibition(ArtExhibition other)
    {
        this.title = other.title;
        this.organizer = other.organizer;
        this.startDate = other.startDate;
        this.endDate = other.endDate;
        this.artworks = new List<string>(other.artworks);
    }

    // Публічні методи
    public void AddArtwork(string artwork)
    {
        artworks.Add(artwork);
    }

    public void RemoveArtwork(string artwork)
    {
        artworks.Remove(artwork);
    }

    public void PrintInfo()
    {
        Console.WriteLine($"Назва виставки: {title}");
        Console.WriteLine($"Організатор: {organizer}");
        Console.WriteLine($"Дата початку: {startDate.ToShortDateString()}");
        Console.WriteLine($"Дата завершення: {endDate.ToShortDateString()}");
        Console.WriteLine($"Кількість творів мистецтва: {artworks.Count}");
    }

    public string GetArtworksSummary()
    {
        return string.Join(", ", artworks);
    }

    public List<string> FilterArtworksByCategory(string category)
    {
        return artworks.FindAll(a => a.Contains(category, StringComparison.OrdinalIgnoreCase));
    }

    // Перевантаження операторів
    public static bool operator ==(ArtExhibition ex1, ArtExhibition ex2)
    {
        if (ReferenceEquals(ex1, null) || ReferenceEquals(ex2, null))
        {
            return false;
        }

        return ex1.title == ex2.title && ex1.organizer == ex2.organizer;
    }

    public static bool operator !=(ArtExhibition ex1, ArtExhibition ex2)
    {
        return !(ex1 == ex2);
    }

    public override bool Equals(object obj)
    {
        if (obj is ArtExhibition otherExhibition)
        {
            return this == otherExhibition;
        }

        return false;
    }

    public override int GetHashCode()
    {
        return (title, organizer).GetHashCode();
    }
}

class Program
{
    static void Main()
    {
        try
        {
            // Введення даних з клавіатури
            Console.Write("Введіть назву виставки: ");
            string title = Console.ReadLine();

            Console.Write("Введіть організатора виставки: ");
            string organizer = Console.ReadLine();

            Console.Write("Введіть дату початку (yyyy-mm-dd): ");
            DateTime startDate = DateTime.Parse(Console.ReadLine());

            Console.Write("Введіть дату завершення (yyyy-mm-dd): ");
            DateTime endDate = DateTime.Parse(Console.ReadLine());

            // Створення нової виставки
            ArtExhibition exhibition = new ArtExhibition(title, organizer, startDate, endDate);

            // Додавання кількох творів мистецтва
            exhibition.AddArtwork("Sculpture - Venus");
            exhibition.AddArtwork("Painting - Starry Night");
            exhibition.AddArtwork("Painting - Mona Lisa");

            // Виведення інформації про виставку
            exhibition.PrintInfo();

            // Фільтрація творів за категорією "Painting"
            var paintings = exhibition.FilterArtworksByCategory("Painting");
            Console.WriteLine($"Картини на виставці: {string.Join(", ", paintings)}");

            // Виведення резюме творів мистецтва
            Console.WriteLine("Усі твори: " + exhibition.GetArtworksSummary());

            // Додавання і видалення ще одного твору
            exhibition.AddArtwork("Sculpture - David");
            exhibition.RemoveArtwork("Sculpture - Venus");

            // Виведення оновленого списку творів
            Console.WriteLine("Оновлений список творів: " + exhibition.GetArtworksSummary());

            // Створення копії виставки та порівняння
            ArtExhibition copiedExhibition = new ArtExhibition(exhibition);

            if (copiedExhibition == exhibition)
            {
                Console.WriteLine("Копія виставки ідентична оригіналу.");
            }
            else
            {
                Console.WriteLine("Копія виставки відрізняється від оригіналу.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Сталася помилка: " + ex.Message);
        }
    }
}

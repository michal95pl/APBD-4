namespace WebApplication1.Models;

public class Wizyta
{
    public DateTime Date { get; }
    public Animal Animal { get; }
    public string Description { get; }
    public double Cost { get; }

    public Wizyta(DateTime date, Animal animal, string description, double cost)
    {
        this.Date = date;
        this.Animal = animal;
        this.Description = description;
        this.Cost = cost;
    }
}
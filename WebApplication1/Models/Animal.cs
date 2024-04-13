namespace WebApplication1.Models;

public class Animal
{
    private static int _idMem = 0;
    public int Id { get; } = _idMem++;
    public string Name { get; set; }
    public string Category { get; set; }
    public double Weight { get; set; }
    public string Color { get; set; }
}
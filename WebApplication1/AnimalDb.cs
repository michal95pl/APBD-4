using WebApplication1.Models;

namespace WebApplication1;

public class AnimalDb
{
    private List<Animal> _animals = new();

    public void Add(Animal animal)
    {
        _animals.Add(animal);
    }

    public List<Animal> GetListData()
    {
        return _animals;
    }

    public Animal? GetById(int id)
    {
        return _animals.FirstOrDefault(a => a.Id == id);
    }

    public bool DeleteById(int id)
    {
        return _animals.RemoveAll(a => a.Id == id) > 0;
    }
}
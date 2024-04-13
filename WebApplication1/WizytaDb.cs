using WebApplication1.Models;

namespace WebApplication1;

public class WizytaDb
{
    private List<Wizyta> _list = new();

    public List<Wizyta> GetByAnimalId(int id)
    {
        List<Wizyta> temp = new();

        foreach (var vWizyta in _list)
        {
            if (vWizyta.Animal.Id == id)
                temp.Add(vWizyta);
        }

        return temp;
    }

    public void AddVisit(Wizyta wizyta)
    {
        _list.Add(wizyta);
    }
}
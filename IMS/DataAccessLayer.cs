using IMS;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

public class DataAccessLayer
{
    private readonly ApplicationDbContext _context;

    public DataAccessLayer(ApplicationDbContext context)
    {
        _context = context;
    }

    public List<Item> GetAllItems()
    {
        return _context.Items.ToList();
    }

    public void AddItem(Item item)
    {
        _context.Items.Add(item);
        _context.SaveChanges();
    }

    public void UpdateItem(Item item)
    {
        _context.Items.Update(item);
        _context.SaveChanges();
    }

    public void DeleteItem(Item item)
    {
        _context.Items.Remove(item);
        _context.SaveChanges();
    }
}

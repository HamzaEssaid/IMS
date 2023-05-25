using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using IMS;

namespace IMS.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Item> Item { get; set; }
        public string SortOrder { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        public async Task OnGetAsync(string sortOrder)
        {
            // Set the initial sort order if not provided
            if (string.IsNullOrEmpty(sortOrder))
            {
                sortOrder = "default";
            }

            SortOrder = sortOrder;

            // Perform the sorting based on the sort order
            switch (sortOrder)
            {
                case "type":
                    Item = await _context.Items.OrderBy(i => i.Type).ToListAsync();
                    break;
                case "brand":
                    Item = await _context.Items.OrderBy(i => i.Brand).ToListAsync();
                    break;
                case "status":
                    Item = await _context.Items.OrderBy(i => i.Status).ToListAsync();
                    break;
                case "yearModel":
                    Item = await _context.Items.OrderByDescending(i => i.YearModel).ToListAsync();
                    break;
                default:
                    Item = await _context.Items.ToListAsync();
                    break;
            }

            // Filter the items based on the search string
            if (!string.IsNullOrEmpty(SearchString))
            {
                Item = Item.Where(i => i.Type.IndexOf(SearchString, StringComparison.OrdinalIgnoreCase) >= 0 ||
                                       i.Brand.IndexOf(SearchString, StringComparison.OrdinalIgnoreCase) >= 0 ||
                                       i.Status.IndexOf(SearchString, StringComparison.OrdinalIgnoreCase) >= 0 ||
                                       i.Info.IndexOf(SearchString, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            }

        }
    }
}

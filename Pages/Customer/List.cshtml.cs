using System.Collections.Generic;
using System.Threading.Tasks;
using ekarazor.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;



namespace ekarazor.Pages.CustomerCrud
{
    public class ListModel : PageModel
    {
        private readonly AppDbContext _db;

        public ListModel(AppDbContext db)
        {
            _db = db;
        }

        public IList<Customer> Customers {get; private set;}

        public async Task OnGetAsync()
        {
            Customers = await _db.Customers.AsNoTracking().ToListAsync();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id){
            var contact =  await _db.Customers.FindAsync(id);
            if(contact != null)
            {
                _db.Customers.Remove(contact);
                await _db.SaveChangesAsync();
            }
            return RedirectToPage();
        }
    }
}
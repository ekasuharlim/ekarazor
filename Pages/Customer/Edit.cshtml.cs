using System;
using System.Threading.Tasks;
using ekarazor.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ekarazor.Pages.CustomerCrud
{
    public class EditModel : PageModel
    {
        private readonly AppDbContext _db;
        public EditModel(AppDbContext db)
        {
            _db = db;
        }

        [BindProperty]
         public Customer Customer { get; set; }
         
         public async Task<IActionResult> OnGetAsync(int id)
         {
             Customer = await _db.Customers.FindAsync(id);
             if(Customer == null)
             {
                 RedirectToPage("./List");                 
             }
             return Page();
         }

         public async Task<IActionResult> OnPostAsync()
         {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _db.Attach(Customer).State  = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException)
            {
                throw new Exception($"Customer {Customer.Id} not found!");
            }
            
            return RedirectToPage("./List");

         }
    }
}
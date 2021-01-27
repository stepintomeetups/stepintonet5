﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AspNetDemo.Backend;
using AspNetDemo.Backend.Model;

namespace AspNetDemo.Pages.TodoPages
{
    public class DeleteModel : PageModel
    {
        private readonly AspNetDemo.Backend.TodoDbContext _context;

        public DeleteModel(AspNetDemo.Backend.TodoDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Todo Todo { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Todo = await _context.Todos.FirstOrDefaultAsync(m => m.Id == id);

            if (Todo == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Todo = await _context.Todos.FindAsync(id);

            if (Todo != null)
            {
                _context.Todos.Remove(Todo);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}

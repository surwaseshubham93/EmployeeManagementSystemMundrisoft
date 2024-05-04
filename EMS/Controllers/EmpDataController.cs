using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EMS.Models;

namespace EMS.Controllers
{
    public class EmpDataController : Controller
    {
        private readonly DatabaseContext _context;

        public EmpDataController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: EmpData
        public async Task<IActionResult> Index()
        {
            return View(await _context.Emp.ToListAsync());
        }

        // GET: EmpData/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empData = await _context.Emp
                .FirstOrDefaultAsync(m => m.EID == id);
            if (empData == null)
            {
                return NotFound();
            }

            return View(empData);
        }

        // GET: EmpData/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EmpData/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EID,FirstName,MiddleName,LastName,Email,Department,MobileNo,Gender")] EmpData empData)
        {
            if (ModelState.IsValid)
            {
                _context.Add(empData);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(empData);
        }

        // GET: EmpData/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empData = await _context.Emp.FindAsync(id);
            if (empData == null)
            {
                return NotFound();
            }
            return View(empData);
        }

        // POST: EmpData/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EID,FirstName,MiddleName,LastName,Email,Department,MobileNo,Gender")] EmpData empData)
        {
            if (id != empData.EID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(empData);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmpDataExists(empData.EID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(empData);
        }

        // GET: EmpData/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empData = await _context.Emp
                .FirstOrDefaultAsync(m => m.EID == id);
            if (empData == null)
            {
                return NotFound();
            }

            return View(empData);
        }

        // POST: EmpData/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var empData = await _context.Emp.FindAsync(id);
            if (empData != null)
            {
                _context.Emp.Remove(empData);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmpDataExists(int id)
        {
            return _context.Emp.Any(e => e.EID == id);
        }
    }
}

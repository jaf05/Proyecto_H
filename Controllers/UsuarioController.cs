﻿using Microsoft.AspNetCore.Mvc;
using Proyecto.Datos;
using Proyecto.Models;

namespace Proyecto.Controllers
{
    public class UsuarioController : Controller
    {
        public readonly ApplicationDbContext _context;
        public UsuarioController(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        public IActionResult Index()
        {
            List<Usuario> listaUsuarios = _context.Usuarios.ToList();
            return View(listaUsuarios);
          
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                _context.Usuarios.Add(usuario);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return View();
            }
            var usuario = _context.Usuarios.FirstOrDefault
                (u => u.id_usuario == id);
            return View(usuario);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                _context.Usuarios.Update(usuario);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(usuario);
        }
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            var usuario = _context.Usuarios.FirstOrDefault(
                u => u.id_usuario == id);
            _context.Usuarios.Remove(usuario);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

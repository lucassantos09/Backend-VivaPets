using Dominio.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VivaPetsBackEnd.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AnimalController : ControllerBase
    {
        public ApplicationDbContext _context { get; set; }
    
        public AnimalController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Post(Animais animal)
        {
            try
            {
                var result = _context.Add(animal).Entity;
                _context.SaveChanges();
                return StatusCode(200, result);
                
            }
            catch (Exception)
            {

                return StatusCode(500);
            }

        }



        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var result = _context.Animais.ToList();
                return StatusCode(200, result);

            }
            catch (Exception)
            {

                return StatusCode(500);
            }

        }

        [HttpGet("{id}")]
        public IActionResult GetById(long id)
        {
            try
            {
                var result = _context.Animais.Where(x => x.Id == id).FirstOrDefault();
                return StatusCode(200, result);

            }
            catch (Exception)
            {

                return StatusCode(500);
            }

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            try
            {
                var entidade = _context.Animais.Where(x => x.Id == id).FirstOrDefault();
                if (entidade != null)
                {
                     var result = _context.Remove(entidade);
                    _context.SaveChanges();
                    return StatusCode(200, result.Entity);

                }return StatusCode(400);

            }
            catch (Exception)
            {

                return StatusCode(500);
            }

        }

    }
}

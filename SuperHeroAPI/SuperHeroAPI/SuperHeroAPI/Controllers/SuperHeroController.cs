using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SuperHeroAPI.Context;

namespace SuperHeroAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private readonly SuperHeroContext _context;

        public SuperHeroController(SuperHeroContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> GetAllSuperHeroes()
        {
            //return Ok(await _context.SuperHero.ToListAsync());
            return Ok(await _context.SuperHero.FromSqlRaw("Exec dbo.GetAllSuperHeroes").ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<SuperHero>>> GetSuperHeroes(int id)
        {
            return Ok(await _context.SuperHero.FromSqlRaw("Exec dbo.GetSuperHero @Id", new SqlParameter("@Id",id)).ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> CreateSuperHero(SuperHero hero)
        {
            //_context.SuperHero.Add(hero);
            //await _context.SaveChangesAsync();
            //return Ok(await _context.SuperHero.ToListAsync());

            return Ok(await _context.SuperHero.FromSqlRaw("Exec dbo.InsertSuperHero @Name, @FirstName, @LastName, @Place", new SqlParameter("@Name", hero.Name), new SqlParameter("@FirstName", hero.FirstName), new SqlParameter("@LastName", hero.LastName), new SqlParameter("@Place", hero.Place)).ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> UpdateSuperHero(SuperHero hero)
        {
            //var dbHero = await _context.SuperHero.FindAsync(hero.Id);
            //if (dbHero == null)
            //    return BadRequest("Hero not found");
            //dbHero.Name = hero.Name;
            //dbHero.FirstName = hero.FirstName;
            //dbHero.LastName = hero.LastName;
            //dbHero.Place = hero.Place;
            //await _context.SaveChangesAsync();
            //return Ok(await _context.SuperHero.ToListAsync());

            return Ok(await _context.SuperHero.FromSqlRaw("Exec dbo.UpdateSuperHero @Id, @Name, @FirstName, @LastName, @Place", new SqlParameter("@Id", hero.Id), new SqlParameter("@Name", hero.Name), new SqlParameter("@FirstName", hero.FirstName), new SqlParameter("@LastName", hero.LastName), new SqlParameter("@Place", hero.Place)).ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<SuperHero>>> DeleteSuperHero(int id)
        {
            //var dbHero = await _context.SuperHero.FindAsync(id);
            //if (dbHero == null)
            //    return BadRequest("Hero not found");

            //_context.SuperHero.Remove(dbHero);
            //await _context.SaveChangesAsync();
            //return Ok(await _context.SuperHero.ToListAsync());

            return Ok(await _context.SuperHero.FromSqlRaw("Exec dbo.DeleteSuperHero @Id", new SqlParameter("@Id",id)).ToListAsync());
        }
    }
}
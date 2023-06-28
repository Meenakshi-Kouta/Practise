using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperHeroAPI.Context;

namespace SuperHeroAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private readonly SuperHeroContext _dbContext;

        public SuperHeroController(SuperHeroContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> GetSuperHeroes()
        {
            return Ok(await _dbContext.SuperHero.ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> CreateSuperHeroes(SuperHero hero)
        {
            _dbContext.SuperHero.Add(hero);
            await _dbContext.SaveChangesAsync();
            return Ok(await _dbContext.SuperHero.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> UpdateSuperHeroes(SuperHero hero)
        {
            var dbHero = await _dbContext.SuperHero.FindAsync(hero.Id);
            if (dbHero == null)
            {
                return BadRequest("Hero not found");
            }
            dbHero.Name = hero.Name;
            dbHero.FirstName = hero.FirstName;
            dbHero.LastName = hero.LastName;
            dbHero.Place = hero.Place;
            await _dbContext.SaveChangesAsync();
            return Ok(await _dbContext.SuperHero.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<SuperHero>>> DeleteSuperHeroes(int id)
        {
            var dbHero = await _dbContext.SuperHero.FindAsync(id);
            if (dbHero == null)
            {
                return BadRequest("Hero not found");
            }
            _dbContext.SuperHero.Remove(dbHero);
            await _dbContext.SaveChangesAsync();
            return Ok(await _dbContext.SuperHero.ToListAsync());
        }
    }
}

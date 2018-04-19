namespace Kwikemart.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using Kwikemark.Entities;
    using Kwikemark.Entities.DTOs;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly DbKwikemartContext _dbContext;
        public ProductController(DbKwikemartContext dbContext)
        {
            _dbContext = dbContext;
        }
        // GET api/city
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var entities = await _dbContext.Products.Include("TypeProduct").ToListAsync();
                return Ok(entities);
            }
            catch (System.Exception ex)
            {
                throw ex;
                /*
                return StatusCode((int)HttpStatusCode.InternalServerError, 
                        "Error Obteniendo Ciudades");*/
            }
        }

        // POST api/city
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]ProductDTO value)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                var result = await _dbContext.AddAsync(new Product
                {
                    Id = value.Id,
                    Description = value.Description,
                    IdTypeProduct = value.IdTypeProduct
                });

                await _dbContext.SaveChangesAsync();
                return Ok();
            }
            catch (System.Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, 
                        "Error Almacenando Ciudad");
            }
        }

        // PUT api/city/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]ProductDTO value)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                var entity = await _dbContext.Products.FirstOrDefaultAsync(c => c.Id == id);

                if (entity == null)
                {
                    return BadRequest();
                }

                entity.Id = value.Id;
                entity.Description = value.Description;
                entity.IdTypeProduct = value.IdTypeProduct;

                _dbContext.Update(entity);

                await _dbContext.SaveChangesAsync();
                return Ok();
            }
            catch (System.Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                        "Error Editar Ciudad");
            }
        }

        // DELETE api/city/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var entity = await _dbContext.Products.FirstOrDefaultAsync(c => c.Id == id);

                if (entity == null)
                {
                    return BadRequest();
                }
                _dbContext.Remove(entity);
                await _dbContext.SaveChangesAsync();
                return Ok();
            }
            catch (System.Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                        "Error Eliminando Ciudad");
            }
        }
    }
}

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
    public class TypeProductController : Controller
    {
        private readonly DbKwikemartContext _dbContext;
        public TypeProductController(DbKwikemartContext dbContext)
        {
            _dbContext = dbContext;
        }
        // GET api/typeProduct
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var entities = await _dbContext.TypeProducts.Include("Products").ToListAsync();
                return Ok(entities);
            }
            catch
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, 
                        "Error Obteniendo Tipos de Producto");
            }
        }

        // POST api/typeProduct
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]TypeProductDTO value)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                var result = await _dbContext.AddAsync(new TypeProduct
                {
                    Id = value.Id,
                    Description = value.Description
                });

                await _dbContext.SaveChangesAsync();
                return Ok();
            }
            catch (System.Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, 
                        "Error Almacenando Tipo de Producto");
            }
        }

        // PUT api/typeProduct/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]TypeProductDTO value)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                var entity = await _dbContext.TypeProducts.FirstOrDefaultAsync(c => c.Id == id);

                if (entity == null)
                {
                    return BadRequest();
                }

                entity.Id = value.Id;
                entity.Description = value.Description;

                _dbContext.Update(entity);

                await _dbContext.SaveChangesAsync();
                return Ok();
            }
            catch (System.Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                        "Error Editar Tipo de Producto");
            }
        }

        // DELETE api/typeProduct/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var entity = await _dbContext.TypeProducts.FirstOrDefaultAsync(c => c.Id == id);

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
                        "Error Eliminando Tipo de Producto");
            }
        }
    }
}

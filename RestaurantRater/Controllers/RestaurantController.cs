using RestaurantRater.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace RestaurantRater.Controllers
{
    public class RestaurantController : ApiController
    {
        private readonly RestaurantDbContext _context = new RestaurantDbContext();

        //POST
        [HttpPost]
        public async Task<IHttpActionResult> PostRestaurant(Restaurant restaurant)
        {
            if (ModelState.IsValid && restaurant != null)
            {
                _context.Restaurants.Add(restaurant);
                await _context.SaveChangesAsync();
                return Ok();
            }

            return BadRequest(ModelState);
        }

        //GET ALL
        [HttpGet]
        public async Task<IHttpActionResult> GetAll()
        {
            List<Restaurant> restaurants = await _context.Restaurants.ToListAsync();
            return Ok(restaurants);
        }

        //GET BY ID
        [HttpGet]
        public async Task<IHttpActionResult> GetById(int id)
        {
            Restaurant restaurant = await _context.Restaurants.FindAsync(id);

            if(restaurant == null)
            {
                return NotFound();
            }

            return Ok(restaurant);
        }

        //PUT (Update)
        [HttpPut]
        public async Task<IHttpActionResult> UpdateRestaurant([FromUri]int id, [FromBody]Restaurant model)
        {
            if(ModelState.IsValid && model != null)
            {
                //This is our entity
                Restaurant restaurant = await _context.Restaurants.FindAsync(id);

                if (restaurant != null)
                {
                    restaurant.Name = model.Name;
                    restaurant.Rating = model.Rating;
                    restaurant.Style = model.Style;
                    restaurant.DollarSigns = model.DollarSigns;

                    await _context.SaveChangesAsync();

                    return Ok();
                }

                return NotFound();
            }

            return BadRequest(ModelState);
        }

        //DELETE BY ID
    }
}

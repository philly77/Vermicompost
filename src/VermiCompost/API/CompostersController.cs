﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VermiCompost.Data;
using VermiCompost.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace VermiCompost.API
{
    [Route("api/[controller]")]
    public class CompostersController : Controller
    {
        private ApplicationDbContext _db;
        
        public CompostersController(ApplicationDbContext db)
        {
            _db = db;
        }


        // GET: api/values
        [HttpGet]
        public IActionResult Get()
        {
            var composters = _db.Composters.ToList();
            return Ok(composters);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var products = _db.CompostersProducts.Where(cp => cp.ComposterId == id).Select(cp=>cp.Product).ToList();
            return Ok(products);
        }


        // POST api/values
        //add Products to a Composter, pass the ComposterId and the name of the Product
        [HttpPost("{id}")]
        public IActionResult Post(int id, [FromBody]Product product)
        {
            //create product
            _db.Products.Add(product);
            _db.SaveChanges();

            //add product to composters using the junction object
            _db.CompostersProducts.Add(new ComposterProduct
            {
                ComposterId = id,
                ProductId = product.Id
            });
            _db.SaveChanges();

            return Ok();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

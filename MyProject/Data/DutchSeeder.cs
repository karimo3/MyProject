﻿using Microsoft.AspNetCore.Hosting;
using MyProject.Data.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.Data
{
    public class DutchSeeder
    {
        private readonly DutchContext _ctx;
        private readonly IHostingEnvironment _hosting;

        public DutchSeeder(DutchContext ctx, IHostingEnvironment hosting)
        {
            _ctx = ctx;
            _hosting = hosting;
        }

        public void Seed()
        {
            _ctx.Database.EnsureCreated(); //make sure the database actually exist/created... only creates database if it doesnt exist

            if (!_ctx.Products.Any()) //call to Any() returns true if there are any Products in the database ... Select Count(Products) return true 
            {
                //Need to create Sample Data

                var filepath = Path.Combine(_hosting.ContentRootPath, "Data/art.json"); //in order for this to work at Runtime, we must inject the IHostingEnvironment hosting in the constructor as seen above
                var json = File.ReadAllText(filepath);
                var products = JsonConvert.DeserializeObject<IEnumerable<Product>>(json);
                _ctx.Products.AddRange(products); //this adds all the products into the database 


                //more sample data...not sure why we need this...
                var order = new Order()
                {
                    OrderDate = DateTime.Now,
                    OrderNumber = "12345",
                    Items = new List<OrderItem>()
                    {
                        new OrderItem()
                        {
                            Product = products.First(),
                            Quantity = 5,
                            UnitPrice = products.First().Price
                        }
                    }
                };

                //adding the "order" to the collection as well
                _ctx.Orders.Add(order);

                //save changes to database 
                _ctx.SaveChanges();
            }

        }

    }
}
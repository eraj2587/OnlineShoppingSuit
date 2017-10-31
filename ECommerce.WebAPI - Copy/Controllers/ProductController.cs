using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.OData;
using ECommerce.WebAPI.Models;

namespace ECommerce.WebAPI.Controllers
{
    //[EnableCors("http://localhost:53100/", "*", "*")]
    public class ProductController : ApiController
    {
        // GET api/product
        //[EnableQuery(PageSize = 50)] // Securty and constraints on OData query
        [EnableQuery()]
        [ResponseType(typeof(Product))]
        public IHttpActionResult Get()
        {
            try
            {
                var productRepository = new ProductRepository();
                return Ok(productRepository.Retrieve().AsQueryable());
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET api/product/5
        [ResponseType(typeof(Product))]
        public IHttpActionResult Get(int id)
        {
            try
            {
                // throw new Exception("Invalid operation occured");
                Product product;
                var productRepository = new ProductRepository();
                if (id > 0)
                {
                    var res = productRepository.Retrieve();
                    product = res.Find(x => x.ProductId.Equals(id));
                    if (product == null) return NotFound();
                }
                else
                {
                    product = productRepository.Create();
                }
                return Ok(product);
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
        }

        // POST api/product
        public IHttpActionResult Post([FromBody]Product product)
        {
            try
            {
                if (product == null)
                    return BadRequest("Product can not be null");

                if (!ModelState.IsValid) return BadRequest(ModelState);


                var productReppstory = new ProductRepository();

                var newProduct = productReppstory.Save(product);

                if (newProduct == null)
                    return Conflict();

                return Created(Request.RequestUri + newProduct.ProductId.ToString(), newProduct);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // PUT api/product/5
        public IHttpActionResult Put(int id, [FromBody]Product product)
        {
            try
            {
                if (product == null) return BadRequest("Product can not be null");

                if (!ModelState.IsValid) return BadRequest(ModelState);

                var productReppstory = new ProductRepository();
                var updatedProduct = productReppstory.Save(id, product);
                if (updatedProduct == null) NotFound();

                return Ok("Product Updated");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE api/product/5
        public void Delete(int id)
        {
        }
    }
}

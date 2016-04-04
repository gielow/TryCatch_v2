using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using TryCatch.Data;
using TryCatch.Interfaces;
using TryCatch.Models;

namespace TryCatch.Controllers
{
    public class ArticleController : System.Web.Mvc.Controller
    {
        public System.Web.Mvc.ActionResult Index()
        {
            var client = new HttpClient();
            var response = client.GetAsync("http://localhost/TC_v2/api/ArticleApi/");
            var articles = response.Result.Content.ReadAsAsync<IEnumerable<Article>>().Result;
            return View(articles);
        }
    }

    [RoutePrefix("api/Article")]
    public class ArticleApiController : ApiController
    {
        IRepository _repository;

        public ArticleApiController(IRepository repository)
        {
            _repository = repository;
        }
        //private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Article
        public IQueryable<Article> GetArticles()
        {
            return _repository.Articles.AsQueryable();
        }

        [HttpGet]
        [Route("Page/{number}")]
        public IQueryable<Article> Page(int? number)
        {
            var pageNumber = 1;

            if (number.HasValue && number.Value > 0)
                pageNumber = number.Value;

            return _repository.Articles.Skip((pageNumber - 1) * 10).Take(10).AsQueryable();
        }

        // GET: api/Article/5
        [ResponseType(typeof(Article))]
        public IHttpActionResult GetArticle(int id)
        {
            /*Article article = db.Articles.Find(id);
            if (article == null)
            {
                return NotFound();
            }

            return Ok(article);*/
            return Ok(new Article());
        }

        // PUT: api/Article/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutArticle(int id, Article article)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != article.Id)
            {
                return BadRequest();
            }

            //db.Entry(article).State = EntityState.Modified;

            try
            {
                //db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArticleExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Article
        [ResponseType(typeof(Article))]
        public IHttpActionResult PostArticle(Article article)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //db.Articles.Add(article);
            //db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = article.Id }, article);
        }

        // DELETE: api/Article/5
        [ResponseType(typeof(Article))]
        public IHttpActionResult DeleteArticle(int id)
        {
            /*Article article = db.Articles.Find(id);
            if (article == null)
            {
                return NotFound();
            }

            db.Articles.Remove(article);
            db.SaveChanges();

            return Ok(article);*/
            return Ok(new Article());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ArticleExists(int id)
        {
            //return db.Articles.Count(e => e.Id == id) > 0;
            return false;
        }
    }
}
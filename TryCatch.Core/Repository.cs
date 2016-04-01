using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TryCatch.Data;
using TryCatch.Models;

namespace TryCatch.Core
{
    public class Repository : IRepository
    {
        public List<Article> Articles
        {
            get
            {
                var articles = new List<Article>();
                articles.Add(new Article() {
                    Description = "Article 01",
                    Id = 1,
                    Price = 10
                });

                return articles;
            }
        }
    }
}

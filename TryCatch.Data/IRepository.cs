using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TryCatch.Models;

namespace TryCatch.Data
{
    public interface IRepository
    {
        List<Article> Articles { get; }
    }
}

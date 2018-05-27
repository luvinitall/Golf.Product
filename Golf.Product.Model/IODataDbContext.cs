using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Golf.Product.Model
{
    public interface IODataDbContext
    {
        IDbSet<Category> Categories { get; set; }


    }
}

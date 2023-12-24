using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_CRUD_1B.Models;
using Microsoft.EntityFrameworkCore;

namespace API_CRUD_1B.Models
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options)
        {

        }

        public DbSet<Student> SinhVien { get; set; }
    

       
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;



namespace QuanLy.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }
        public string HoTen { get; set; }
        public bool GioiTinh { get; set; }
        public int Tuoi { get; set; }
        public DateTime NgaySinh { get; set; }

        public float Luong { get; set; }
    }
}

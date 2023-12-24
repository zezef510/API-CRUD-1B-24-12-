using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QLHS_baitap3_.Models
{
    public class Student
    {
        public int Id { get; set; }

        [StringLength(15, MinimumLength = 3, ErrorMessage = "Họ tên phải có độ dài từ 3 đến 15 ký tự.")]
        public string HoTen { get; set; }

        public bool GioiTinh { get; set; }
        public int Tuoi { get; set; }
        public DateTime NgaySinh { get; set; }
        public float Luong { get; set; }

    }
}

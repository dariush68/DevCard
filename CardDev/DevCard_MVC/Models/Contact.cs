using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DevCard_MVC.Models
{
    public class Contact
    {
        [Required(ErrorMessage = "این فیلد اجباری است")]
        [MinLength(3, ErrorMessage = "حداقل طول کاراکتر 3 است")]
        [MaxLength(100, ErrorMessage = "حداکثر 100 کاراکتر")]
        public string Name { get; set; }

        [Required(ErrorMessage = "این فیلد اجباری است")]
        [EmailAddress(ErrorMessage = "فرمت ناصحیح ایمیل")]
        public string Email { get; set; }
        public string Message { get; set; }
        public string Service { get; set; }

    }
}

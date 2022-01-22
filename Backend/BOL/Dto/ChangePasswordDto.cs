using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOL.Dto
{
    public class ChangePasswordDto
    {
        [Required]
        [StringLength(255, MinimumLength = 3, ErrorMessage = "Must more than 3 character long.")]
        public string Username { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 4, ErrorMessage = "Must more than 4 character long.")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 4, ErrorMessage = "Must more than 4 character long.")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
    }
}

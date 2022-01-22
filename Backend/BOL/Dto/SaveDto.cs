using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOL.Dto
{
    public class SaveDto
    {
        public int Id { get; set; }
        public int FK_Posts_Id { get; set; }
        public int FK_Users_Id { get; set; }
    }
}

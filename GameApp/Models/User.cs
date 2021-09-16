using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameApp.Models
{
    public class User : IComparable<User>
    {
        [Key]
        public string Phone { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public double Score { get; set; } = 0;

        public virtual ICollection<WordsForUser> WordsForUsers { get; set; }

        public int CompareTo(User other)
        {
            if (this.Score > other.Score)
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }
    }
}

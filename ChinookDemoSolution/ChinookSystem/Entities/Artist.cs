using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
#endregion

namespace ChinookSystem.Entities
{
    [Table("Arstists")]
    internal class Artist
    {
        [Key]
        private string _Name;
        public int ArtistId { get; set; }
        //Name is nullable field so, it needs full implementation

        //[Required(ErrorMessage = "Artist name is required.")]
        [StringLength(120, ErrorMessage ="Artist name is limited to 120 characters.")]
        public string Name
        { get { return _Name; }
            set { _Name = string.IsNullOrEmpty(value) ? null : value; } 
        }

        //navigational properties
        //1 to many relationship; create the many relationship in this entity
        //Artist has collection of Albums
        //Album belong to an Artist
        public virtual ICollection<Album> Albums { get; set; }
    }


}

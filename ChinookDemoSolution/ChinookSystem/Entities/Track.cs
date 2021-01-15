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
    [Table("Tracks")]
    internal class Track
    {

        private string _Composer;
        [Key]
        public int TrackId { get; set; }

        [StringLength(200, ErrorMessage = "Track name is limited to 200 characters.")]
        [Required(ErrorMessage = "Track Name is required.")]
        public string Name { get; set; }

        public int? AlbumId { get; set; }

        [Required(ErrorMessage = "MediaTypeId is required.")]
        public int MediaTypeId { get; set; }

        public int? GenreId{ get; set; }

        [StringLength(220, ErrorMessage = "Composer is limited to 220 characters.")]
        public string Composer
        {
            get { return _Composer; }
            set { _Composer = string.IsNullOrEmpty(value)?null:value; }

        }

        public int Milliseconds { get; set; }
        public int? Bytes { get; set; }
        public decimal UnitPrice { get; set; }



    }
}

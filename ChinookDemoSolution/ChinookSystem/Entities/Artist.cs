
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

namespace ChinookSystem.Entities
{
    internal partial class Artist
    {
        private string _Name;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]

        //constructor helps in transactional process
        //when we create recods, it creates a hash set (set of unique number)
        //stage our transactional commands(add,update, delete)
        //EF will use it there
        public Artist()
        {
            Albums = new HashSet<Album>();
        }

        //don't need Key annotation
        public int ArtistId { get; set; }

        //need to add error messages
        [StringLength(120, ErrorMessage ="Artist name is limited to 120 characters")]
        public string Name { 
            get { return _Name; } 
            set { _Name = string.IsNullOrEmpty(value) ? null : value; } }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Album> Albums { get; set; }
    }
}

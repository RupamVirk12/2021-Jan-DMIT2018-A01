using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region AdditionalNamespaces
using ChinookSystem.Entities;
using ChinookSystem.DAL;
using ChinookSystem.ViewModels;
using System.ComponentModel;  //this one is for ODS
#endregion

namespace ChinookSystem.BLL
{
    [DataObject]
     public class AlbumController
    {
        //to make this method available to ODS wizard, false- means we need to choose from list of methods and not setting this particular method to default 
        //due to the fact that the enitites are internal, you CANNOT use the entity class as a return data type
        [DataObjectMethod(DataObjectMethodType.Select,false)]
        public List<ArtistAlbums> Albums_GetArtistAlbums()
        {
            using(var context = new ChinookSystemContext())
            {
                //Linq to Entity
                //when we run this, as a enitity framework, this will create a SQL query for us and execute in SQL. So, this is Linq to Entity but when we where in Linqpad it was Linq to SQL directly but here we need to go through entity(for e.g context.Albums
                IEnumerable<ArtistAlbums> results = from x in context.Albums
                                                    select new ArtistAlbums
                                                    {
                                                        Tilte = x.Title,
                                                        ReleaseYear = x.ReleaseYear,
                                                        ArtistName = x.Artist.Name
                                                    };
            return results.ToList();
            }
        }
    }
}

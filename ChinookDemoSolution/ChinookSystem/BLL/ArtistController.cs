using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region AdditionalNamespaces
using ChinookSystem.Entities; //this is for the internal entitties
using ChinookSystem.DAL;   //this is for the context class
using ChinookSystem.ViewModels; //this is for the public data classes fro transporting data from the library to the web application
using System.ComponentModel;  //this one is for ODS
#endregion

namespace ChinookSystem.BLL
{
    [DataObject]
    public class ArtistController
    {
        [DataObjectMethod(DataObjectMethodType.Select,false)]
        public List<SelectionList> Artist_DDLList()
        {
            using(var context = new ChinookSystemContext())
            {
                IEnumerable<SelectionList> results = from x in context.Artists
                                                     orderby x.Name
                                                select new SelectionList
                                                {
                                                    ValueField = x.ArtistId,
                                                    DisplayField = x.Name
                                                };
                return results.ToList();
            }
        }
    }
}

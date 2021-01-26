﻿using System;
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

        #region Queries
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
                                                        Title = x.Title,
                                                        ReleaseYear = x.ReleaseYear,
                                                        ArtistName = x.Artist.Name
                                                    };
            return results.ToList();
            }
        }

        public List<ArtistAlbums> Albums_GetAlbumsForArtist(int artistid)
        {
            using (var context = new ChinookSystemContext())
            {
                //Linq to Entity

                IEnumerable<ArtistAlbums> results = from x in context.Albums
                                                    where x.ArtistId == artistid
                                                    select new ArtistAlbums
                                                    {
                                                        Title = x.Title,
                                                        ReleaseYear = x.ReleaseYear,
                                                        ArtistName = x.Artist.Name,
                                                        ArtistId = x.ArtistId
                                                    };
                return results.ToList();
            }
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<AlbumItem> Albums_List()
        {
            using (var context = new ChinookSystemContext())
            {
                IEnumerable<AlbumItem> results = from x in context.Albums
                                                    select new AlbumItem
                                                    {
                                                        AlbumId = x.AlbumId,
                                                        Title = x.Title,
                                                        ArtistId = x.ArtistId,
                                                        ReleaseYear = x.ReleaseYear,
                                                        ReleaseLabel = x.ReleaseLabel

                                                    };
                return results.ToList();
                // we did use the following in the last sem, but now we have restricted our entities so, we have to go through view model class AlbumItem
               // return.context.Albums.ToList();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public AlbumItem Albums_FindbyId(int albumid)
        {
                 using (var context = new ChinookSystemContext())
            { 
               AlbumItem results = (from x in context.Albums
                                   where x.AlbumId == albumid
                                  select new AlbumItem
                                                 {
                                                     AlbumId = x.AlbumId,
                                                     Title = x.Title,
                                                     ArtistId = x.ArtistId,
                                                     ReleaseYear = x.ReleaseYear,
                                                     ReleaseLabel = x.ReleaseLabel

                                                 }).FirstOrDefault();
                return results;
            }
        }
        #endregion

        //List view CRUD using ODS and without any code-behind
        #region Add, Update, Delete

        [DataObjectMethod(DataObjectMethodType.Insert,false)]
        //adding a instance of Album - item
        public int Albums_Add(AlbumItem item)
        {
            using(var context = new ChinookSystemContext())
            {
                //Previously, when we don't have view model , we used the following to add a new item
                //context.Albums.Add(item);
                //context.SaveChanges();
                //since I have an int as the return datatype, I will return the new identity value
                // return item.AlbumId;

                //now we need to move the data from the viewmodel class into an entity instance BEFORE staging

                //the pkey of the Albums table is an Identity(), therefore we DO NOT need to supply the AlblumId value
                Album entityItem = new Album
                {
                    Title = item.Title,
                    ArtistId = item.ArtistId,
                    ReleaseYear = item.ReleaseYear,
                    ReleaseLabel = item.ReleaseLabel

                };

                //stagging
                context.Albums.Add(entityItem);
                //At this point, the new pkey value is NOT available
                //the new pkey value is created by the database

                //commit
                context.SaveChanges();
                //since I have an int as the return datatype, I will return the new identity value
                return entityItem.AlbumId;

            }
        }


        [DataObjectMethod(DataObjectMethodType.Update, false)]
        //updating an instance of Album - item
        public void Albums_Update(AlbumItem item)
        {
            using (var context = new ChinookSystemContext())
            {
               

                //now we need to move the data from the viewmodel class into an entity instance BEFORE staging
                //On update you NEED to supply the table's pkey value
                
                Album entityItem = new Album
                {
                    AlbumId = item.AlbumId,
                    Title = item.Title,
                    ArtistId = item.ArtistId,
                    ReleaseYear = item.ReleaseYear,
                    ReleaseLabel = item.ReleaseLabel

                };

                //stagging is to local memory
                context.Entry(entityItem).State = System.Data.Entity.EntityState.Modified;
                //commit is the action of sending your request to the database for action
                //At this point, the new pkey value is NOT available
                //the new pkey value is created by the database
                //Also, any validation annotation in your entity definitions class
                // is executed during this command
                context.SaveChanges();
               // return entityItem.AlbumId;

            }
        }
        [DataObjectMethod(DataObjectMethodType.Delete,false)]

        public void Albums_Delete(AlbumItem item)
        {
            //this method will call the actual delete method and pass it the only needed piece of data that is pkey
            Albums_Delete(item.AlbumId);
        }

        public void Albums_Delete(int albumid)
        {
            using (var context = new ChinookSystemContext())
            {
                var exists = context.Albums.Find(albumid);
                context.Albums.Remove(exists);
                context.SaveChanges();
            }
        }
        #endregion
    }
}

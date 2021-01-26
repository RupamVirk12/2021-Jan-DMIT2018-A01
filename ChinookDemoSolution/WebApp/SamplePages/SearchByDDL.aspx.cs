
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

#region Additional Namespaces
using ChinookSystem.BLL;
using ChinookSystem.ViewModels;
#endregion

namespace WebApp.SamplePages
{
    public partial class SearchByDDL : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                //this is the first time the page loading
                LoadArtistList();


            }
        }

        protected void LoadArtistList()
        {
            // user friendly error handling (aka try/catch)
            //use the user control MessageUserControl to manage the
            // error handling for the web page when control leaves
            // the web page and goes to the class library

            MessageUserControl.TryRun(() =>
            {

                //what goes inside the coding block?
                //your code that would normally be inside the try portion of try/catch
                ArtistController sysmgr = new ArtistController();
                List<SelectionList> info = sysmgr.Artist_DDLList();
                //Let's assume that the data collection need to be sorted
                info.Sort((x, y) => x.DisplayField.CompareTo(y.DisplayField));
                // for descending sorting, switch x and y

                //set up the ddl
                ArtistList.DataSource = info;
                //we can use the strings name as well, Display Field or ValueField
                //view models are reused over and over again for any DDL
                // ArtistList.DataTextField = "DisplayField"; this can be used instead but it is kinda magic string because we dont know where it is coming from but using nameof lets us know which field from the class , we are using
                ArtistList.DataTextField = nameof(SelectionList.DisplayField);
                ArtistList.DataValueField = nameof(SelectionList.ValueField);

                ArtistList.DataBind();

                //set a prompt line
                ArtistList.Items.Insert(0, new ListItem("select...", "0"));



            }, "Success title message", "the success title and body message are optional"
            // we can put in our success message her

            );
           
            
        }

        #region Error Handling Methods for ODS
        protected void SelectCheckForException(object sender, ObjectDataSourceStatusEventArgs e)
        {
            MessageUserControl.HandleDataBoundException(e);
        }
        
        #endregion
        protected void SearchAlbums_Click(object sender, EventArgs e)
        {
                if(ArtistList.SelectedIndex == 0)
            {
                //Am I on the first physical line (prompt line) of the DDL
               
                MessageUserControl.ShowInfo("Search Selection Concern", "Select an artist for the search.");
               //empty the grid 
                ArtistAlbumList.DataSource = null;
                ArtistAlbumList.DataBind();
            }
                else
            {
                MessageUserControl.TryRun(() => {

                    AlbumController sysmgr = new AlbumController();
                    List<ChinookSystem.ViewModels.ArtistAlbums> info = sysmgr.Albums_GetAlbumsForArtist(int.Parse(ArtistList.SelectedValue));

                    //testing if abort had happened
                    //throw new Exception("This is a test to see an abort from the web page code");
                    ArtistAlbumList.DataSource = info;
                    ArtistAlbumList.DataBind();
                }, "Search Results", "The list of albums for the selected artists");
                
            }
        }
    }
}
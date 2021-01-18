
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
            ArtistController sysmgr = new ArtistController();

            List<SelectionList> info = sysmgr.Artist_DDLList();

            //Let's assume that the data collection need to be sorted
            info.Sort((x,y) => x.DisplayField.CompareTo(y.DisplayField));
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


        }

        protected void SearchAlbums_Click(object sender, EventArgs e)
        {
                if(ArtistList.SelectedIndex == 0)
            {
                //Am I on the first physical line (prompt line) of the DDL
                MessageLabel.Text = "Select an artist for the search.";
               //empty the grid 
                ArtistAlbumList.DataSource = null;
                ArtistAlbumList.DataBind();
            }
                else
            {
                AlbumController sysmgr = new AlbumController();
                List<ChinookSystem.ViewModels.ArtistAlbums> info = sysmgr.Albums_GetAlbumsForArtist(int.Parse(ArtistList.SelectedValue));
                ArtistAlbumList.DataSource = info;
                ArtistAlbumList.DataBind();
            }
        }
    }
}
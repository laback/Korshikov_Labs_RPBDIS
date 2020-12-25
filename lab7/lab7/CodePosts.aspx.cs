using Lab7.Data;
using Lab7.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace lab7
{
    public partial class CodePosts : System.Web.UI.Page
    {
        private RailroadContext db = new RailroadContext();
        private string strFindPost = "";

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                strFindPost = TextBoxFindPost.Text;
                ShowData(strFindPost);
            }

        }

        protected void GridViewPost_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //Set the edit index.
            GridViewPost.EditIndex = e.NewEditIndex;
            //Bind data to the GridView control.
            ShowData(strFindPost);

        }


        protected void GridViewPost_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

            //Update the values.
            GridViewRow row = GridViewPost.Rows[e.RowIndex];
            int id = Convert.ToInt32(((TextBox)(row.Cells[1].Controls[0])).Text);
            Post post = db.Posts.Where(f => f.PostId == id).FirstOrDefault();
            post.NameOfPost = ((TextBox)(row.Cells[2].Controls[0])).Text;

            db.SaveChanges();
            //Reset the edit index.
            GridViewPost.EditIndex = -1;

            //Bind data to the GridView control.
            ShowData(strFindPost);

        }

        protected void GridViewPost_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //Update the values.
            GridViewRow row = GridViewPost.Rows[e.RowIndex];
            int id = Convert.ToInt32(((TextBox)(row.Cells[1].Controls[0])).Text);
            Post post = db.Posts.Where(f => f.PostId == id).FirstOrDefault();
            db.Posts.Remove(post);

            //db.Entry(fuel).State = EntityState.Modified;
            db.SaveChanges();
            //Reset the edit index.
            GridViewPost.EditIndex = -1;

            //Bind data to the GridView control.
            ShowData(strFindPost);

        }


        protected void GridViewPost_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            //Reset the edit index.
            GridViewPost.EditIndex = 1;
            //Bind data to the GridView control.
            ShowData(strFindPost);
        }


        protected void ButtonFindPost_Click(object sender, EventArgs e)
        {
            strFindPost = TextBoxFindPost.Text;
            ShowData(strFindPost);
        }

        protected void ButtonAddPost_Click(object sender, EventArgs e)
        {
            string nameOfPost = TextBoxNameOfPost.Text ?? "";
            if (nameOfPost != "")
            {
                Post post = new Post
                {
                    NameOfPost = nameOfPost
                };

                db.Posts.Add(post);
                db.SaveChanges();
                TextBoxNameOfPost.Text = "";
                ShowData(strFindPost);

            }


        }


        protected void GridViewPost_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewPost.PageIndex = e.NewPageIndex;
            ShowData(strFindPost);

        }
        protected void ShowData(string strFindPost = "")
        {

            List<Post> posts = db.Posts.Where(s => s.NameOfPost.Contains(strFindPost)).ToList();
            GridViewPost.DataSource = posts;
            GridViewPost.DataBind();
        }
    }
}
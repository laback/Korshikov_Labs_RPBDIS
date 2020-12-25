using Lab7.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Lab7.Models;

namespace lab7
{
    public partial class CodeTypes : System.Web.UI.Page
    {
        private RailroadContext db = new RailroadContext();
        private string strFindType = "";

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                strFindType = TextBoxFindType.Text;
                ShowData(strFindType);
            }

        }

        protected void GridViewType_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewType.EditIndex = e.NewEditIndex;
            ShowData(strFindType);

        }


        protected void GridViewType_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = GridViewType.Rows[e.RowIndex];
            int id = Convert.ToInt32(((TextBox)(row.Cells[1].Controls[0])).Text);
            Lab7.Models.Type Type = db.Types.Where(f => f.TypeId == id).FirstOrDefault();
            Type.NameOfType = ((TextBox)(row.Cells[2].Controls[0])).Text;

            db.SaveChanges();
            GridViewType.EditIndex = -1;
            ShowData(strFindType);

        }

        protected void GridViewType_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow row = GridViewType.Rows[e.RowIndex];
            int id = Convert.ToInt32(((TextBox)(row.Cells[1].Controls[0])).Text);
            Lab7.Models.Type Type = db.Types.Where(f => f.TypeId == id).FirstOrDefault();
            db.Types.Remove(Type);

            db.SaveChanges();
            GridViewType.EditIndex = -1;

            ShowData(strFindType);

        }


        protected void GridViewType_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridViewType.EditIndex = 1;
            ShowData(strFindType);
        }


        protected void ButtonFindType_Click(object sender, EventArgs e)
        {
            strFindType = TextBoxFindType.Text;
            ShowData(strFindType);
        }

        protected void ButtonAddType_Click(object sender, EventArgs e)
        {
            string nameOfType = TextBoxNameOfType.Text ?? "";
            if (nameOfType != "")
            {
                Lab7.Models.Type Type = new Lab7.Models.Type
                {
                    NameOfType = nameOfType
                };

                db.Types.Add(Type);
                db.SaveChanges();
                TextBoxNameOfType.Text = "";
                ShowData(strFindType);

            }


        }

        protected void GridViewType_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewType.PageIndex = e.NewPageIndex;
            ShowData(strFindType);

        }
        protected void ShowData(string strFindType = "")
        {

            List<Lab7.Models.Type> Types = db.Types.Where(s => s.NameOfType.Contains(strFindType)).ToList();
            GridViewType.DataSource = Types;
            GridViewType.DataBind();
        }
    }
}
using lab7.ViewModels;
using Lab7.Data;
using Lab7.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace lab7
{
    public partial class CodeTrains : System.Web.UI.Page
    {
        private RailroadContext db = new RailroadContext();
        private string strFindTrain = "";

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                strFindTrain = TextBoxFindTrain.Text;
                ShowData(strFindTrain);
            }

        }

        protected void GridViewTrain_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewTrain.EditIndex = e.NewEditIndex;
            ShowData(strFindTrain);

        }


        protected void GridViewTrain_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

            GridViewRow row = GridViewTrain.Rows[e.RowIndex];
            int id = Convert.ToInt32(((TextBox)(row.Cells[1].Controls[0])).Text);
            Train train = db.Trains.Where(f => f.TrainId == id).FirstOrDefault();
            train.TypeId = int.Parse(e.NewValues["TypeId"].ToString());
            train.IsFirm = Convert.ToBoolean(e.NewValues["IsFirm"].ToString());
            db.SaveChanges();
            GridViewTrain.EditIndex = -1;
            ShowData(strFindTrain);

        }

        protected void GridViewTrain_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow row = GridViewTrain.Rows[e.RowIndex];
            int id = Convert.ToInt32(((TextBox)(row.Cells[1].Controls[0])).Text);
            Train train = db.Trains.Where(f => f.TrainId == id).FirstOrDefault();
            db.Trains.Remove(train);

            db.SaveChanges();
            GridViewTrain.EditIndex = -1;

            ShowData(strFindTrain);

        }


        protected void GridViewTrain_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridViewTrain.EditIndex = 1;
            ShowData(strFindTrain);
        }


        protected void ButtonFindTrain_Click(object sender, EventArgs e)
        {
            strFindTrain = TextBoxFindTrain.Text;
            ShowData(strFindTrain);
        }

        protected void ButtonAddTrain_Click(object sender, EventArgs e)
        {
            bool isFirm = Convert.ToBoolean(TextBoxIsFirm.Text ?? "false");
            int typeId = int.Parse(TypeDropDownList.SelectedValue);
            Train train = new Train
            {
                IsFirm = isFirm,
                TypeId = typeId
            };
            db.Trains.Add(train);
            db.SaveChanges();
        }
        protected void GridViewTrain_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewTrain.PageIndex = e.NewPageIndex;
            ShowData(strFindTrain);

        }
        protected void ShowData(string strFindTrain = "")
        {

            List<TrainViewModel> trains = db.Trains.Include(t => t.Type).Where(t => t.Type.NameOfType.Contains(strFindTrain)).Select(t => new TrainViewModel
            {
                TrainId = t.TrainId,
                TypeId = t.TypeId,
                IsFirm = t.IsFirm,
                NameOfType = t.Type.NameOfType
            }).ToList();
            GridViewTrain.DataSource = trains;
            GridViewTrain.DataBind();
        }
    }
}
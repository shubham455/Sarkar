﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace betplayer.admin
{
	public partial class AgentDetails : System.Web.UI.Page
	{
        private DataTable dt;
        public DataTable MatchDataTable { get { return dt; } }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["msg"] == "Add")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Agent Added SuccessFully.....');", true);
                }
            }
            string CN = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
            using (MySqlConnection cn = new MySqlConnection(CN))
            {
                cn.Open();
                string s = "Select * From AgentMaster";
                MySqlCommand cmd = new MySqlCommand(s, cn);
                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                dt = new DataTable();
                adp.Fill(dt);
                DateTime DateFromDB = Convert.ToDateTime(dt.Rows[0]["Date"]);
                Date(DateFromDB);
            }
        }



        protected void txtsearch_TextChanged(object sender, EventArgs e)
        {
            string CN = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
            using (MySqlConnection cn = new MySqlConnection(CN))
            {
                cn.Open();
                string s = "Select* From AgentMaster Where Name Like '%" + txtsearch.Text + "%'";
                MySqlCommand cmd = new MySqlCommand(s, cn);
                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                dt = new DataTable();
                adp.Fill(dt);
            }

        }

        public void BindData()
        {
            string CN = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
            using (MySqlConnection cn = new MySqlConnection(CN))
            {
                cn.Open();
                string s = "Select * From AgentMaster";
                MySqlCommand cmd = new MySqlCommand(s, cn);
                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                dt = new DataTable();
                adp.Fill(dt);


            }
        }

        protected void DropDownstatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            string CN = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
            using (MySqlConnection cn = new MySqlConnection(CN))
            {
                cn.Open();
                string s = "update  AgentMaster set Status = '" + DropDownstatus.SelectedItem.Text + "' where AgentID = 1";
                MySqlCommand cmd = new MySqlCommand(s, cn);
                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                dt = new DataTable();
                adp.Fill(dt);
                BindData();

            }
        }
        public string delete(string id)
        {
            string CN = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
            using (MySqlConnection cn = new MySqlConnection(CN))
            {
                cn.Open();
                string s = "delete from Agentmaster  where AgentID = '" + id + "'";
                MySqlCommand cmd = new MySqlCommand(s, cn);
                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                dt = new DataTable();
                adp.Fill(dt);
                BindData();
                return dt.ToString();

            }
        }
        public string Date(DateTime DateTimefromDB)
        {
            DateTime oDate = DateTime.Parse(DateTimefromDB.ToString());
            string datetime = "Time: " + oDate.TimeOfDay.ToString();
            string date = oDate.Date.ToString("dd-MM-yyyy");
            string time = oDate.TimeOfDay.ToString();
            return date;
        }
    }
}

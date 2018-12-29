using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace betplayer.superagent
{
    public partial class UpdateAgentlimit : System.Web.UI.Page
    {
        private DataTable UpdateTable;
        public DataTable UpdateDataTable { get { return UpdateTable; } }


        protected void Page_Load(object sender, EventArgs e)
        {

            UpdateTable = new DataTable();
            UpdateTable.Columns.Add(new DataColumn("AgentID"));
            UpdateTable.Columns.Add(new DataColumn("Name"));
            UpdateTable.Columns.Add(new DataColumn("FixLimit"));
            UpdateTable.Columns.Add(new DataColumn("CurrentLimit"));
            UpdateTable.Columns.Add(new DataColumn("UsedLimit"));
            DataRow row = UpdateTable.NewRow();


            string CN = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
            using (MySqlConnection cn = new MySqlConnection(CN))
            {
                cn.Open();
                string Select = "Select * From AgentMaster where  CreatedBy = '" + Session["SuperAgentcode"] + "'";
                MySqlCommand cmd = new MySqlCommand(Select, cn);
                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                Decimal Total = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    int AgentID = Convert.ToInt16(dt.Rows[i]["AgentID"]);
                    String Name = dt.Rows[i]["Name"].ToString();
                    string FixLimit = dt.Rows[i]["Fixlimit"].ToString();
                    string Code = dt.Rows[i]["Code"].ToString();

                    Decimal ClientCurrentLimit = Convert.ToDecimal(dt.Rows[i]["CurrentLimit"]);
                    Total = Total + ClientCurrentLimit;
                    row["AgentID"] = AgentID;
                    row["Name"] = Name;
                    row["Fixlimit"] = FixLimit;
                    row["Currentlimit"] = ClientCurrentLimit;


                    string AgentUsedLimit = "Select * From ClientMaster Where CreatedBy = '" + Code + "' ";
                    MySqlCommand AgentUsedLimitcmd = new MySqlCommand(AgentUsedLimit,cn);
                    MySqlDataAdapter AgentUsedLimitadp = new MySqlDataAdapter(AgentUsedLimitcmd);
                    DataTable AgentUsedLimitdt = new DataTable();
                    AgentUsedLimitadp.Fill(AgentUsedLimitdt);
                    if (AgentUsedLimitdt.Rows.Count > 0)
                    {
                        decimal TotalusedLimit = 0;
                        for (int a = 0; a < AgentUsedLimitdt.Rows.Count; a++)
                        {
                            Decimal usedLimit = Convert.ToDecimal(AgentUsedLimitdt.Rows[a]["CurrentLimit"]);
                            TotalusedLimit = TotalusedLimit + usedLimit;
                        }
                        row["UsedLimit"] = TotalusedLimit;
                    }
                    else { row["UsedLimit"] = 0; }

                    UpdateTable.Rows.Add(row.ItemArray);

                }
                TotalLimit.Value = Total.ToString();


                    string SuperAgentlimit = "Select * From SuperAgentMaster where SuperAgentID = '" + Session["SuperAgentID"] + "'";
                    MySqlCommand Agentlimitcmd = new MySqlCommand(SuperAgentlimit, cn);
                    MySqlDataAdapter Agentlimitadp = new MySqlDataAdapter(Agentlimitcmd);
                    DataTable Agentlimitdt = new DataTable();
                    Agentlimitadp.Fill(Agentlimitdt);

                    SuperAgentLimit.Value = Agentlimitdt.Rows[0]["Currentlimit"].ToString();
                
                }
            }
        }
    }
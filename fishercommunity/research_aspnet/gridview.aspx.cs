using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class testaspnet_gridview : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bindData();
        }
    }
    private void bindData()
    {
        string sql = "select * from Word";
        DataTable dtGridView = new DataTable();
        dtGridView = DBHelper.GetDTBySql(sql);
        //将datatable转化为dataset
        DataSet dsGridView = new DataSet();
        dsGridView.Tables.Add(dtGridView);
        gvList.DataSource = dsGridView;
        gvList.DataBind();
    }
}
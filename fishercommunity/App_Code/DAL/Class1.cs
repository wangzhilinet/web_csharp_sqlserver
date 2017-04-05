using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
///Class1 的摘要说明
/// </summary>
public class Class1
{
    private static string connStr = "server=.;database=test;uid=sa;pwd=123";
    private SqlConnection conn = new SqlConnection(connStr);
    private SqlCommand cmd = null;
    private SqlDataAdapter da = null;
    public DataTable dtbysql(string sql)
    {
        da = new SqlDataAdapter(sql, conn);
        DataTable dt = new DataTable();
        da.Fill(dt);
        return dt;
    }
    public int exbysql(string sql)
    {
        cmd = new SqlCommand(sql, conn);
        conn.Open();
        int num = cmd.ExecuteNonQuery();
        conn.Dispose();
        conn.Close();
        return num;
    }
}
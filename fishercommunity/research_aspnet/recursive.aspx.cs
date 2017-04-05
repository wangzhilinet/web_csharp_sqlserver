using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class testaspnet_recursive : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        digui();
        test();
        console();
    }
    /// <summary>
    /// 递归求5的阶层
    /// </summary>
    public void digui()
    {
        int n = 5;
        int j;
        j = jieceng(n);
        this.Label1.Text = j.ToString();
    }
    public int jieceng(int n)
    {
        int result;
        if (n < 0)
        {
            return 0;
        }
        else if (n == 0 || n == 1)
        {
            result = 1;
        }
        else
        {
            result = jieceng(n - 1) * n;
        }
        return result;
    }
    public void test()
    {
        string b = "0";
        //ConfigurationHelper.GetBoolSetting(b);
        this.Label2.Text = b;
    }
    public void console()
    {
        Console.Write("test");
    }

}
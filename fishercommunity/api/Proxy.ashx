<%@ WebHandler Language="C#" Class="Proxy" %>

using System;
using System.Web;
using System.Text;

public class Proxy : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        context.Response.Write("Hello World");
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}
/******************************************************
* author :  冰点
* email  :  xiaohui300@gmail.com 
* function: 
* history:  created by 冰点 2009-6-5 15:12:25 
* safeitemname :FLVUpload
******************************************************/
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
namespace swfupload_demo
{
    public partial class FLVUpload : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.CacheControl = "no-cache";
            if (this.Page.Request.Files.Count > 0)
            {
                string updir = Server.MapPath("upload");
                try
                {
                    for (int i = 0; i < Page.Request.Files.Count; i++)
                    {
                        HttpPostedFile uploadFile = this.Page.Request.Files[i];
                        if (uploadFile.ContentLength > 0)
                        {
                            if (!Directory.Exists(updir))
                            {
                                Directory.CreateDirectory(updir);
                            }
                            string extname = Path.GetExtension(uploadFile.FileName);
                            string filename = uploadFile.FileName;
                            uploadFile.SaveAs(updir + "\\" + filename);
                        }
                    }

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
    }
}

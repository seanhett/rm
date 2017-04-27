using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Weston.EPA;
using SubSonic;
using System.IO;
using Microsoft.Practices.EnterpriseLibrary.Logging;

public partial class Mapping_MapImage : System.Web.UI.Page
{
    string id;
    string eventID;
    string photoID;

    protected void Page_Load(object sender, EventArgs e)
    {
        id = Request.QueryString["id"];
        if (!String.IsNullOrEmpty(id))
        {
            eventID = Request.QueryString["eventID"];
            if (!String.IsNullOrEmpty(eventID))
            {
                try
                {
                    GetPhotoID(id);
                    if (!String.IsNullOrEmpty(photoID))
                    {
                        WriteFile();
                    }
                }
                catch(Exception ex)
                {
                    Logger.Write("Exception... " + ex.ToString());
                }
            }
        }
    }

    private void WriteFile()
    {
        if (File.Exists(Server.MapPath(LoadFilePath())))
        {
            Response.ContentType = "image/JPEG";
            System.Drawing.Image img = System.Drawing.Image.FromFile(Server.MapPath(LoadFilePath()));
            System.Drawing.Image thmbImg = img.GetThumbnailImage(105, 90, null, IntPtr.Zero);

            thmbImg.Save(Context.Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);
        }
    }

    private void GetPhotoID(string id)
    {
        StoredProcedure sp = Weston.EPA.StoredProcedures.SPs.GetPhotoID(new Guid(id));
        using (IDataReader idr = sp.GetReader())
        {
            while (idr.Read())
            {
                photoID = idr[0].ToString();
            }
        }
    }

    private string LoadFilePath()
    {
        string folder = VirtualPathUtility.AppendTrailingSlash(ConfigurationManager.AppSettings[WebHubConstants.PHOTOS_FOLDER_LOAD_PATH]);

        return "~" + VirtualPathUtility.Combine(folder,
            eventID +
            Path.DirectorySeparatorChar.ToString() +
            photoID +
            WebHubConstants.PHOTO_SUFFIX);
    }
}

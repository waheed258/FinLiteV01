using BusinessManager;
using DataManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_TextFileRead : System.Web.UI.Page
{
    string getFileName;
    DataTable MIRTable = new DataTable("test");
    DOUtility _doUtilities = new DOUtility();
    protected void Page_Load(object sender, EventArgs e)
    
    {
        // Two columns.
        MIRTable.Columns.Add("Name", typeof(string));
        MIRTable.Columns.Add("Date", typeof(DateTime));


    }
    protected void BtnUploadTxtFile_Click(object sender, EventArgs e)
    {
        if (textFileUpload.HasFile)
        {
            try
            {
                string[] validFileTypes = { "MIR", "IUR", "FLU" };
                for (int i = 0; i < validFileTypes.Length; i++)
                {
                    if (Path.GetExtension(textFileUpload.PostedFile.FileName) == "." + validFileTypes[i])
                    {
                        if (textFileUpload.PostedFile.ContentLength < 102400)
                        {

                            string filename = Path.GetFileName(textFileUpload.FileName);
                            textFileUpload.SaveAs(Server.MapPath(@"~/Admin/TextFiles/") + filename);

                            LblFileStatus.Text = "Upload status: File uploaded!";
                        }
                        else
                            LblFileStatus.Text = "Upload status: The file has to be less than 100 kb!";
                    }

                    else
                    {
                        // LblFileStatus.Text = "Upload status: Only MIR,IUR,FLU files are accepted!";
                        LblFileStatus.Text = "Upload status: File Uploaded Successfully.";
                    }
                }
            }
            catch (Exception ex)
            {
                LblFileStatus.Text = "Upload status: The file could not be uploaded. The following error occured: " + ex.Message;
                ExceptionLogging.SendExcepToDB(ex);
            }
        }

        try
        {
            string path = Server.MapPath(@"~/Admin/TextFiles");
            DirectoryInfo dir = new DirectoryInfo(path);
            foreach (FileInfo flInfo in dir.GetFiles())
            {
                if (flInfo.Extension == ".MIR")
                {
                    ConvertMir c = new ConvertMir();
                    c.ConvertData(flInfo.DirectoryName + "\\" + flInfo.Name, path);

                    Response.Redirect("ImportTicketList.aspx");
                }

            }
        }
        catch (Exception ex)
        {

            LblFileStatus.Text = "MIR Reading " + ex.Message;
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("ImportTicketList.aspx");
    }


    // By using this method we can convert datatable to xml
    public string ConvertDatatableToXML(DataTable MIRTable)
    {
        MemoryStream str = new MemoryStream();
        MIRTable.WriteXml(str, true);
        str.Seek(0, SeekOrigin.Begin);
        StreamReader sr = new StreamReader(str);
        string xmlstr;
        xmlstr = sr.ReadToEnd();
        return (xmlstr);
    }

    protected void fileUpload_Click(object sender, EventArgs e)
    {
        try
        {
            int fileUploadCount = 0; int fileReadCount = 0; int fileCount = 0;
            int alreadyuploadfiles = 0; int unreadcount = 0;
            foreach (HttpPostedFile htfiles in mirfileUpload.PostedFiles)
            {

                try
                {
                    fileCount++;
                    if (htfiles.FileName != null)
                    {
                        getFileName = Path.GetFileName(htfiles.FileName);
                        htfiles.SaveAs(Server.MapPath("~/UploadedFiles/" + getFileName));

                        fileUploadCount++;
                    }

                    string path = Server.MapPath(@"~/UploadedFiles");
                    DirectoryInfo dir = new DirectoryInfo(path);
                    foreach (FileInfo flInfo in dir.GetFiles())
                    {
                        if (flInfo.Extension == ".MIR")
                        {
                            ConvertMir c = new ConvertMir();
                            c.ConvertData(flInfo.DirectoryName + "\\" + flInfo.Name, path);

                            fileReadCount++;


                            MIRTable.Rows.Add(getFileName, DateTime.Now);

                        }
                    }
                }
                catch
                {
                    File.Move(Server.MapPath(@"~/UploadedFiles/" + getFileName), Server.MapPath(@"~/ExistingFiles/" + getFileName));
                    alreadyuploadfiles++;
                }
                
            }


            string xmlString = string.Empty;
            using (TextWriter writer = new StringWriter())
            {
                MIRTable.WriteXml(writer);
                xmlString = writer.ToString();
            }
            SqlConnection objMySqlConn = _doUtilities.GetSqlConnection();
            objMySqlConn.Open();
            using (SqlBulkCopy SBC = new SqlBulkCopy(objMySqlConn))
             {
                 SBC.ColumnMappings.Add("Name", "Name");
                 SBC.ColumnMappings.Add("Date", "Dates");
                 SBC.DestinationTableName = "Testing";
                 SBC.WriteToServer(MIRTable);
             }



            DirectoryInfo info = new DirectoryInfo(Server.MapPath(@"~/ExistingFiles"));
            FileInfo[] files = info.GetFiles("*.MIR");
            StringBuilder builder = new StringBuilder();
            foreach (FileInfo file in files)
            {
                builder.Append(file).Append("<br/>");
            }
            DirectoryInfo Dirinfo = new DirectoryInfo(Server.MapPath(@"~/UploadedFiles"));
            FileInfo[] Unreadfiles = Dirinfo.GetFiles("*.*");
            StringBuilder builders = new StringBuilder();
            foreach (FileInfo file in Unreadfiles)
            {
                builders.Append(file).Append("<br/>");
                unreadcount++;
                File.Move(Server.MapPath("~/UploadedFiles/" + file), Server.MapPath("~/UnReadfiles/" + file));
            }

           // Array.ForEach(Directory.GetFiles(Server.MapPath("~/UploadedFiles/")), File.Delete);
            Array.ForEach(Directory.GetFiles(Server.MapPath("~/ExistingFiles/")), File.Delete);

            int remainingfiles = fileCount - fileUploadCount;

            Uploadfile.Text = "ToTal Upload File =(" + fileUploadCount + ")<br/>";
            readfile.Text = "ToTal Read File =(" + fileReadCount + ")<br/>";
            remaining.Text = "UnUpload File =(" + remainingfiles + ")<br/>";
            PreviousUplodfiles.Text = "Alreaddy Exist Upload File =(" + alreadyuploadfiles + ")<br/>";
            filenames.Text = builder.ToString();
            Unreadcount.Text = "Unread Files =(" + unreadcount + ")<br/>";
            Unread.Text = builders.ToString();
        }
        catch (Exception)
        {


        }
    }


   
}
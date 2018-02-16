using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EntityManager;
using BusinessManager;
using System.Text;
using System.Data;
using System.Diagnostics;
using System.Configuration;
using System.Threading;
public partial class Admin_MultipleFileUpload : System.Web.UI.Page
{
    BALFilereading objBALFilereading = new BALFilereading();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
 
    protected void button1_Click(object sender, EventArgs e)
    {
        EMFilereading objEMFilereading = new EMFilereading();
     
        try
        {

            
            int filecount = 0;
            int fileuploadcount = 0;

            //First Create the instance of Stopwatch Class
            Stopwatch sw = new Stopwatch();

            // Start The StopWatch ...From 000
            sw.Start();

           // List<string> Name = new List<string>();
            
           //// string Name = "";
           // string Type = "getContent";

           //       DataSet ds=objBALFilereading.GetFilesContent(Type);
           //       if (ds.Tables[0].Rows.Count > 0)
           //       {
           //           foreach (DataRow dtlRow in ds.Tables[0].Rows)
           //           {
           //               string name=dtlRow["FileName"].ToString();

           //               Name.Add(name);
                         
           //           }
           //       }
           //       else
           //       {
           //           Name.Add("FileName Not Match");
           //       }

        for (int i = 0; i < Request.Files.Count;i++ )
          {    
              filecount = fileuplaod1.PostedFiles.Count();

        HttpPostedFile postedFile = Request.Files[i];
         if (postedFile.ContentLength > 0)
          {
            string filename = Path.GetFileName(postedFile.FileName);

            //if (Name[i].ToString() != filename)
            //{
           
         //   string contentType = postedFile.ContentType;
            using (Stream fs = postedFile.InputStream)
            {

                string filetype = Path.GetExtension(postedFile.FileName);
                if (filetype == ".MIR")
                {
                  
                    System.IO.Stream content = new System.IO.MemoryStream();

                    fs.CopyTo(content);

                    byte[] bytes = new byte[content.Length];
                    content.Position = 0;
                    content.Read(bytes, 0, (int)content.Length);
                    string data = Encoding.ASCII.GetString(bytes);

                    using (BinaryReader br = new BinaryReader(fs))
                    {
                        //Get The File Extension  


                        objEMFilereading.FileName = postedFile.FileName;
                        objEMFilereading.FileContent = data;
                        objEMFilereading.CreatedBy = 0;

                        int result = objBALFilereading.InsertFilereading(objEMFilereading);
                      
                        if (result != 0)
                        {
                            fileuploadcount++;
                            double filesize = postedFile.ContentLength;
                            if (filesize < (1048576))
                            {
                                string getFileName = Path.GetFileName(postedFile.FileName);
                                postedFile.SaveAs(Server.MapPath(@"uplaodfiles\pdf\" + getFileName));
                              //  label1.Text += "[" + postedFile.FileName + "]- pdf file uploaded  successfully<br/>";
                            }

                            else
                            {
                                label1.Text += "[" + postedFile.FileName + "]- files not uploded size is greater then(1)MB.<br/>Your File Size is(" + (filesize / (1024 * 1034)) + ") MB </br>";
                                label1.Visible = true;
                            }

                         


                            label3.Visible = true;
                            label3.Text = "ToTal File =(" + filecount + ")<br/> Uploded file =(" + fileuploadcount + ")<br/> Not Uploaded=(" + (filecount - fileuploadcount) + ")";
                     
                        }


                    }


                }
                else
                {
                    label1.Text += "[" + postedFile.FileName + "]-file not uploaded <br/>";
                    label1.Visible = true;
                }
                            
                        }
 
                            
            }
        }

        //Stop the Timer
        sw.Stop();


        //Writing Execution Time in label 
        string ExecutionTimeTaken = string.Format("Minutes :{0}\nSeconds :{1}\n<br/> Mili seconds :{2}", sw.Elapsed.Minutes, sw.Elapsed.Seconds, sw.Elapsed.TotalMilliseconds);

        label1.Text = ExecutionTimeTaken;
    }
        
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex);
            label1.Text = "Some Technical Error occurred,Please visit after some time";
        }
    }
    protected void button2_Click(object sender, EventArgs e)
    {
        try
        {
            string content = ""; string FileName = "";
            string Type = "getContent";
            DataSet ds=objBALFilereading.GetFilesContent(Type);

           
            if (ds.Tables[0].Rows.Count > 0)
            {
                Stopwatch sw = new Stopwatch();

                // Start The StopWatch ...From 000
                sw.Start();

                HttpPostedFile UploadedFiles; int i = 0;
                foreach (DataRow dtlRow in ds.Tables[0].Rows)
                {
                    content = dtlRow["FileContent"].ToString();

                    FileName = dtlRow["FileName"].ToString();
                 //   UploadedFiles = FileName;

                    if (content != "")
                    {
                        ConvertMir c = new ConvertMir();
                      
                        c.ConvertData(FileName, content);
                       
                     
                       // DownloadTheFileToServer(FileName, i, content);
                    }
                }
                sw.Stop();


                //Writing Execution Time in label 
                string ExecutionTimeTaken = string.Format("Minutes :{0}\nSeconds :{1}\n<br/> Mili seconds :{2}", sw.Elapsed.Minutes, sw.Elapsed.Seconds, sw.Elapsed.TotalMilliseconds);
                System.Threading.Thread.Sleep(sw.Elapsed.Minutes);
                label1.Text = ExecutionTimeTaken;


                //string path = Server.MapPath(@"uplaodfiles\pdf");
                //DirectoryInfo dir = new DirectoryInfo(path);
                //foreach (FileInfo flInfo in dir.GetFiles())
                //{
                //    if (flInfo.Extension == ".MIR")
                //    {
                        
                //        ConvertMir c = new ConvertMir();
                //        c.ConvertData(flInfo.DirectoryName + "\\" + flInfo.Name, path);

                //        label1.Text = "MIR Reading " + "Successfully";


                //    }

                    //ClassWithTimer.TimerInClass InstanceOfTimer = new ClassWithTimer.TimerInClass(10, false);

                    //InstanceOfTimer.StartTimer();

                    //for (int x = 0; x < 100; x++)
                    //{
                    //    // let's waste some time
                    //    Thread.Sleep(10);
                    //}

                    //InstanceOfTimer.StopTimer();
              //  }
            }
         
        }
        catch (Exception ex)
        {

            label1.Text = "MIR Reading " + ex.Message;
            ExceptionLogging.SendExcepToDB(ex);
        }
    }

   
    public void DownloadTheFileToServer(string file, int id, string content)
    {
        Stream stream = null;
        MemoryStream fs = null;

        #region File Download Code goes here
        try
        {

          //  string strFileName = System.IO.Path.GetFileName(file.FileName);
           // int contentLength = file.ContentLength;

            // convert string to stream
            byte[] byteArray = Encoding.UTF8.GetBytes(content);
            //byte[] byteArray = Encoding.ASCII.GetBytes(contents);
            stream = new MemoryStream(byteArray);


           // stream = content;
            long totalUploadSize = stream.Length;
            int bufferSize = 0;
            //less than 1kB
            if (totalUploadSize <= 1024)
            {
                bufferSize = 1024;
            }
            //less than 4kB but more than 1kB
            else if (bufferSize <= 4096)
            {
                bufferSize = 4096;
            }
            //less than 8Kb
            else if (bufferSize <= 8192)
            {
                bufferSize = 8192;
            }
            else
            {
                bufferSize = 16384;
            }

            byte[] b = new byte[1024];
            int tripDownloadSize = 0;
            long totalDownloadedSize = 0;
            float Percentage = 0;
            bool isNewFile = true;
         //   string fileStoreLocation = ConfigurationManager.AppSettings["FileStoreLocation"];
          //  fs = new FileStream(fileStoreLocation + file, FileMode.Append);

            // convert string to stream
            byte[] ar = Encoding.UTF8.GetBytes(content);
            //byte[] byteArray = Encoding.ASCII.GetBytes(contents);
            fs = new MemoryStream(ar);


         
            //Uplaods 8Kb at a time
            while ((tripDownloadSize = stream.Read(b, 0, 1024)) > 0)
            {
                fs.Write(b, 0, tripDownloadSize);
                totalDownloadedSize += tripDownloadSize;
                Percentage = (int)(totalDownloadedSize * 100) / totalUploadSize;
                setProgressBar(id, Percentage.ToString());
                System.Threading.Thread.Sleep(100);
                isNewFile = false;
            }



        }
        catch (Exception objException)
        {
            throw objException;
        }
        finally
        {
            //if (stream != null)
            //{
            //    stream.Close();
            //    stream.Dispose();
            //}
            //if (fs != null)
            //{
            //    fs.Close();
            //    fs.Dispose();
            //}
        }
        #endregion
    }

    public static void setProgressBar(int id, string progressAmount)
    {

        StringBuilder sb = new StringBuilder();

        sb.Append("<body><script type='text/javascript'>SetProgressBarProgressAmount(" + id + ",'" + progressAmount + "'); </script></body>");

        HttpContext.Current.Response.Write(sb.ToString());

        HttpContext.Current.Response.Flush();

    }
}
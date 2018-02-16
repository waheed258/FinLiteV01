using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;


namespace BusinessManager
{
    public enum loglevel
    {
        loglevel = 2,
        All = 0,
        Warn = 1,
        Error = 2
    }
    public class ConvertMir
    {
     
        //string connstr = "server=localhost;user id=root;password=Dino@123;database=sd_finance; Connection Timeout=300000;";

        //string connstr = "server=67.225.171.204;user id=flv_admin;password=Dino@123;database=testflv;  Max Pool Size=2000000;";

        int datatable = 0;

        string connstr = "server=PC\\SQLEXPRESS;user id=sa;password=Dino@123;database=erp_Flv_Dino; Max Pool Size=2000000;";

        //testing server details
        //string connstr = "server=finlite.dinoosys.in;user id=finlite2_user;password=Dino@123;database=finlite2; Connection Timeout=120000;";

        //string connstr = "server=PC\\SQLEXPRESS;user id=sa;password=Dino@123;database=erp_flv_Dino; Connection Timeout=120000;";
        // string connstr = "server=USER;user id=sa;password=Dino@123;database=erp_Flv_Dino; Connection Timeout=120000;";
        string mirpath;
        // string mirpath;
        //string mirpathsuccess = "c:\\mirint\\success";
        //string mirpathfail = "c:\\mirint\\failure";
        //string mirlog = "c:\\mirlog\\mirint.log";      
        string MirFileName, TextLine, folder;
        string gT5ID;
        int T5ID;
        string PNR;
        bool T5OK, A02OK, A04OK, A07OK, A11OK;
        bool fileError, databaseError;
        bool success;
         
        List<string> files = new List<string>();
        public ConvertMir()
        {
            

        }
        //public ConvertMir(string fname)
        //{
        //    //mirpath = "C:\\WorkSpace\\ERP\\finance";
        //    // mirpath = filePath.Trim();
        //    mirpath = "~/MirData/";

        //}
        public void ConvertData(string fileName, string path)
        {
            // mirpath =  Server.MapPath(@"~/Admin/TextFiles");
            //files = Directory.GetFiles(mirpath).ToList();
            //foreach (string folderIdx in files)
            //{
            //    MirFileName = folderIdx;
            //    if (MirFileName.Substring(MirFileName.Length - 4) == ".MIR" || MirFileName.Substring(MirFileName.Length - 4) == ".M04" || MirFileName.Substring(MirFileName.Length - 4) == ".FIL" || MirFileName.Substring(MirFileName.Length - 4) == ".M08" || MirFileName.Substring(MirFileName.Length - 4) == ".IUR")
            //    {
            //        if (MirFileName.Substring(MirFileName.Length - 4) == ".MIR")
            //        {
            //Only Process files with .MIR extension
            // Open the file for input

            MirFileName = fileName;
             
            if (MirFileName.Substring(MirFileName.Length - 4) == ".MIR" || MirFileName.Substring(MirFileName.Length - 4) == ".M04" || MirFileName.Substring(MirFileName.Length - 4) == ".FIL" || MirFileName.Substring(MirFileName.Length - 4) == ".M08" || MirFileName.Substring(MirFileName.Length - 4) == ".IUR")
            {
                if (MirFileName.Substring(MirFileName.Length - 4) == ".MIR")
                {
                    //Only Process files with .MIR extension
                    // Open the file for input

                    FileStream MirFile = System.IO.File.Open(MirFileName, FileMode.Open);
                    StreamReader sr = new StreamReader(MirFile);
                    //    Read from the file && display the results.
                    TextLine = sr.ReadLine();



                    //byte[] byteArray = Encoding.ASCII.GetBytes(content);
                    //MemoryStream stream = new MemoryStream(byteArray);

                    //StreamReader sr = new StreamReader(stream);
                    //// Read from the file && display the results.
                    //TextLine = sr.ReadLine();

                    var allLines = TextLine.Split(new[] { '\r', '\n' }, StringSplitOptions.None);
                    ArrayList TicketsArray = new ArrayList();
                    var pnr = "";
                    var tkt = "";
                    var camount = "";
                    for (var k = 0; k < allLines.Length; k++)
                    {
                        if (allLines[k] != "")
                        {
                            if (k == 1)
                            {
                                var gbh = allLines[k].Split(' ');
                                for (var g = 0; g < gbh.Length; g++)
                                {
                                    if (gbh[g] != "")
                                    {
                                        pnr = gbh[6];
                                    }
                                }
                                // alert("PNR-------"+pnr);

                            }
                            if (k == 6)
                            {
                                var sgh = allLines[k].Split(' ');
                                for (var h = 0; h < sgh.Length; h++)
                                {
                                    if (sgh[h] != "")
                                    {
                                        tkt = sgh[26].Substring(12, 10);
                                    }
                                }
                                // alert("Ticket Number----"+tkt);
                            }
                            if (allLines[k].IndexOf("A11") != -1)
                            {
                                var BYT = allLines[k].Split(' ');
                                for (var D = 0; D < BYT.Length; D++)
                                {
                                    if (BYT[D] != "")
                                    {
                                        camount = BYT[6].Substring(0, -1);
                                    }
                                }
                                // alert("Cancel Amount----"+camount);
                            }
                        }
                    }

                    //FileStream MirFile = System.IO.File.Open(fileName, FileMode.Open);

                    //StreamReader sr = new StreamReader(MirFile);
                    // Read from the file && display the results.
                    //    TextLine = sr.ReadLine();



                    while (!sr.EndOfStream)
                    {

                        //// //WriteLog All, "INF" , " Processing Read Line " + Textline 

                        if (LEFT(TextLine, 2) == "T5")
                        {
                            string header = TextLine;
                            do
                            {
                                TextLine = sr.ReadLine();
                                if (TextLine == null)
                                    break;
                                if (LEFT(TextLine, 2) != "A0")
                                {
                                    header = header + TextLine;
                                }
                            } while (LEFT(TextLine, 2) != "A0");

                            //// //WriteLog All, "INF", "Line Header: " + header
                            //'Parse header file
                            gT5ID = ParseT5(header, fileName);
                            //ParseT5(header);
                            T5ID = string.IsNullOrEmpty(gT5ID) ? 0 : Convert.ToInt32(gT5ID);
                            T5OK = true;
                        }

                        if (T5ID > 0)
                        {
                            if (LEFT(TextLine, 3) == "A00")
                            {
                                string A00line = TextLine; ;
                                do
                                {
                                    TextLine = sr.ReadLine();
                                    if (TextLine == null)
                                        break;
                                    if (LEFT(TextLine, 2) != "A0" && LEFT(TextLine, 2) != "A1")
                                    {
                                        A00line = A00line + TextLine;
                                    }
                                } while (LEFT(TextLine, 2) != "A0" && LEFT(TextLine, 2) != "A1");
                                //// //WriteLog All, "INF", "Line A00: " + A00line

                                ParseA00(gT5ID, A00line);

                            }
                        }
                        else
                        {

                            break;
                        }

                        if (T5ID > 0)
                        {
                            if (LEFT(TextLine, 3) == "A01")
                            {
                                string A01line = TextLine; ;
                                do
                                {
                                    TextLine = sr.ReadLine();
                                    if (TextLine == null)
                                        break;
                                    if (LEFT(TextLine, 2) != "A0" && LEFT(TextLine, 2) != "A1")
                                    {
                                        A01line = A01line + TextLine;
                                    }
                                } while (LEFT(TextLine, 2) != "A0" && LEFT(TextLine, 2) != "A1");
                                // //WriteLog All, "INF", "Line A01: " + A01line
                                ParseA01(gT5ID, A01line);
                            }
                        }
                        else
                        {

                            break;
                        }


                        if (T5ID > 0)
                        {
                            if (LEFT(TextLine, 3) == "A02")
                            {
                                string A02line = TextLine; ;
                                do
                                {
                                    TextLine = sr.ReadLine();
                                    if (TextLine == null)
                                        break;
                                    if (LEFT(TextLine, 2) != "A0" && LEFT(TextLine, 2) != "A1")
                                    {
                                        A02line = A02line + TextLine;
                                    }
                                } while (LEFT(TextLine, 2) != "A0" && LEFT(TextLine, 2) != "A1");
                                // //WriteLog All, "INF", "Line A02: " + A02line
                                ParseA02(gT5ID, A02line);
                                A02OK = true;
                            }
                        }

                        else
                        {
                            break;
                        }
                        if (T5ID > 0)
                        {

                            if (LEFT(TextLine, 3) == "A03")
                            {
                                string A03line = TextLine; ;
                                do
                                {
                                    TextLine = sr.ReadLine();
                                    if (TextLine == null)
                                        break;
                                    if (LEFT(TextLine, 2) != "A0" && LEFT(TextLine, 2) != "A1")
                                    {
                                        A03line = A03line + TextLine;
                                    }
                                } while (LEFT(TextLine, 2) != "A0" && LEFT(TextLine, 2) != "A1");
                                // //WriteLog All, "INF", "Line A03: " + A03line
                                ParseA03(gT5ID, A03line);
                            }
                        }
                        else
                        {

                            break;
                        }
                        if (T5ID > 0)
                        {
                            if (LEFT(TextLine, 3) == "A04")
                            {
                                string A04line = TextLine; ;
                                do
                                {
                                    TextLine = sr.ReadLine();
                                    if (TextLine == null)
                                        break;
                                    if (LEFT(TextLine, 2) != "A0" && LEFT(TextLine, 2) != "A1")
                                    {
                                        A04line = A04line + TextLine;
                                    }
                                } while (LEFT(TextLine, 2) != "A0" && LEFT(TextLine, 2) != "A1");
                                // //WriteLog All, "INF", "Line A04: " + A04line
                                ParseA04(gT5ID, A04line);
                                A04OK = true;
                            }
                        }
                        else
                        {

                            break;
                        }

                        if (T5ID > 0)
                        {
                            if (LEFT(TextLine, 3) == "A05")
                            {
                                string A05line = TextLine; ;
                                do
                                {
                                    TextLine = sr.ReadLine();
                                    if (TextLine == null)
                                        break;
                                    if (LEFT(TextLine, 2) != "A0" && LEFT(TextLine, 2) != "A1")
                                    {
                                        A05line = A05line + TextLine;
                                    }
                                } while (LEFT(TextLine, 2) != "A0" && LEFT(TextLine, 2) != "A1");
                                // //WriteLog All, "INF", "Line A05: " + A05line
                                ParseA05(gT5ID, A05line);
                            }
                        }
                        else
                        {

                            break;
                        }
                        if (T5ID > 0)
                        {
                            if (LEFT(TextLine, 3) == "A06")
                            {
                                string A06line = TextLine; ;
                                do
                                {
                                    TextLine = sr.ReadLine();
                                    if (TextLine == null)
                                        break;
                                    if (LEFT(TextLine, 2) != "A0" && LEFT(TextLine, 2) != "A1")
                                    {
                                        A06line = A06line + TextLine;
                                    }
                                } while (LEFT(TextLine, 2) != "A0" && LEFT(TextLine, 2) != "A1");
                                // //WriteLog All, "INF", "Line A06: " + A06line
                                ParseA06(gT5ID, A06line);
                            }
                        }
                        else
                        {

                            break;
                        }
                        if (T5ID > 0)
                        {
                            if (LEFT(TextLine, 3) == "A07")
                            {
                                string A07line = TextLine; ;
                                do
                                {
                                    TextLine = sr.ReadLine();
                                    if (TextLine == null)
                                        break;
                                    if (LEFT(TextLine, 2) != "A0" && LEFT(TextLine, 2) != "A1")
                                    {
                                        A07line = A07line + TextLine;
                                    }
                                } while (LEFT(TextLine, 2) != "A0" && LEFT(TextLine, 2) != "A1");
                                // //WriteLog All, "INF", "Line A07: " + A07line
                                ParseA07(gT5ID, A07line);
                                A07OK = true;
                            }
                        }
                        else
                        {

                            break;
                        }

                        if (T5ID > 0)
                        {
                            if (LEFT(TextLine, 3) == "A08")
                            {
                                string A08line = TextLine; ;
                                do
                                {
                                    TextLine = sr.ReadLine();
                                    if (TextLine == null)
                                        break;
                                    if (LEFT(TextLine, 2) != "A0" && LEFT(TextLine, 2) != "A1")
                                    {
                                        A08line = A08line + TextLine;
                                    }
                                } while (LEFT(TextLine, 2) != "A0" && LEFT(TextLine, 2) != "A1");
                                // //WriteLog All, "INF", "Line A08: " + A08line
                                ParseA08(gT5ID, A08line);
                            }
                        }
                        else
                        {

                            break;
                        }

                        if (T5ID > 0)
                        {
                            if (LEFT(TextLine, 3) == "A09")
                            {
                                string A09line = TextLine; ;
                                do
                                {
                                    TextLine = sr.ReadLine();
                                    if (TextLine == null)
                                        break;
                                    if (LEFT(TextLine, 2) != "A1" && LEFT(TextLine, 2) != "A2")
                                    {
                                        A09line = A09line + TextLine;
                                    }
                                } while (LEFT(TextLine, 2) != "A0" && LEFT(TextLine, 2) != "A1");
                                // //WriteLog All, "INF", "Line A09: " + A09line
                                ParseA09(gT5ID, A09line);
                            }
                        }
                        else
                        {

                            break;
                        }

                        if (T5ID > 0)
                        {
                            if (LEFT(TextLine, 3) == "A10")
                            {
                                string A10line = TextLine;
                                do
                                {
                                    TextLine = sr.ReadLine();
                                    if (TextLine == null)
                                        break;
                                    if (LEFT(TextLine, 2) != "A1" && LEFT(TextLine, 2) != "A2")
                                    {
                                        A10line = A10line + TextLine;
                                    }
                                } while (LEFT(TextLine, 2) != "A1" && LEFT(TextLine, 2) != "A2");
                                // //WriteLog All, "INF", "Line A10: " + A10line
                                ParseA10(gT5ID, A10line);
                            }
                        }
                        else
                        {

                            break;
                        }

                        if (T5ID > 0)
                        {
                            if (LEFT(TextLine, 3) == "A11")
                            {
                                string A11line = TextLine;
                                do
                                {
                                    TextLine = sr.ReadLine();
                                    if (TextLine == null)
                                        break;
                                    if (LEFT(TextLine, 2) != "A1" && LEFT(TextLine, 2) != "A2")
                                    {
                                        A11line = A11line + TextLine;
                                    }
                                } while (LEFT(TextLine, 2) != "A1" && LEFT(TextLine, 2) != "A2");
                                // //WriteLog All, "INF", "Line A11: " + A11line
                                ParseA11(gT5ID, A11line);
                                A11OK = true;
                            }
                        }
                        else
                        {

                            break;
                        }

                        if (T5ID > 0)
                        {
                            if (LEFT(TextLine, 3) == "A12")
                            {
                                string A12line = TextLine;
                                do
                                {
                                    TextLine = sr.ReadLine();
                                    if (TextLine == null)
                                        break;
                                    if (LEFT(TextLine, 2) != "A1" && LEFT(TextLine, 2) != "A2")
                                    {
                                        A12line = A12line + TextLine;
                                    }
                                } while (LEFT(TextLine, 2) != "A1" && LEFT(TextLine, 2) != "A2");
                                // //WriteLog All, "INF", "Line A12: " + A12line
                                ParseA12(gT5ID, A12line);
                            }
                        }
                        else
                        {

                            break;
                        }

                        if (T5ID > 0)
                        {
                            if (LEFT(TextLine, 3) == "A13")
                            {
                                string A13line = TextLine;
                                do
                                {
                                    TextLine = sr.ReadLine();
                                    if (TextLine == null)
                                        break;
                                    if (LEFT(TextLine, 2) != "A1" && LEFT(TextLine, 2) != "A2")
                                    {
                                        A13line = A13line + TextLine;
                                    }
                                } while (LEFT(TextLine, 2) != "A1" && LEFT(TextLine, 2) != "A2");
                                // //WriteLog All, "INF", "Line A13: " + A13line
                                ParseA13(gT5ID, A13line);
                            }
                        }
                        else
                        {

                            break;
                        }

                        if (T5ID > 0)
                        {
                            if (LEFT(TextLine, 3) == "A14")
                            {
                                string A14line = TextLine;
                                do
                                {
                                    TextLine = sr.ReadLine();
                                    if (TextLine == null)
                                        break;
                                    if (LEFT(TextLine, 2) != "A1" && LEFT(TextLine, 2) != "A2")
                                    {
                                        A14line = A14line + TextLine;
                                    }
                                } while (LEFT(TextLine, 2) != "A1" && LEFT(TextLine, 2) != "A2");
                                // //WriteLog All, "INF", "Line A14: " + A14line
                                ParseA14(gT5ID, A14line);
                            }
                        }
                        else
                        {

                            break;
                        }

                        if (T5ID > 0)
                        {
                            if (LEFT(TextLine, 3) == "A15")
                            {
                                string A15line = TextLine;
                                do
                                {
                                    TextLine = sr.ReadLine();
                                    if (TextLine == null)
                                        break;
                                    if (LEFT(TextLine, 2) != "A1" && LEFT(TextLine, 2) != "A2")
                                    {
                                        A15line = A15line + TextLine;
                                    }
                                } while (LEFT(TextLine, 2) != "A1" && LEFT(TextLine, 2) != "A2");
                                // //WriteLog All, "INF", "Line A15: " + A14line
                                ParseA15(gT5ID, A15line);
                            }
                        }
                        else
                        {

                            break;
                        }

                        if (T5ID > 0)
                        {
                            if (LEFT(TextLine, 3) == "A16")
                            {
                                string A16line = TextLine;
                                do
                                {
                                    TextLine = sr.ReadLine();
                                    if (TextLine == null)
                                        break;
                                    if (LEFT(TextLine, 2) != "A1" && LEFT(TextLine, 2) != "A2")
                                    {
                                        A16line = A16line + TextLine;
                                    }
                                } while (LEFT(TextLine, 2) != "A1" && LEFT(TextLine, 2) != "A2");
                                // //WriteLog All, "INF", "Line A16: " + A16line
                                ParseA16(gT5ID, A16line);
                            }
                        }
                        else
                        {

                            break;
                        }

                        if (T5ID > 0)
                        {
                            if (LEFT(TextLine, 3) == "A17")
                            {
                                string A17line = TextLine;
                                do
                                {
                                    TextLine = sr.ReadLine();
                                    if (TextLine == null)
                                        break;
                                    if (LEFT(TextLine, 2) != "A1" && LEFT(TextLine, 2) != "A2")
                                    {
                                        A17line = A17line + TextLine;
                                    }
                                } while (LEFT(TextLine, 2) != "A1" && LEFT(TextLine, 2) != "A2");
                                // //WriteLog All, "INF", "Line A17: " + A17line
                                ParseA17(gT5ID, A17line);
                            }
                        }
                        else
                        {

                            break;
                        }

                        if (T5ID > 0)
                        {
                            if (LEFT(TextLine, 3) == "A18")
                            {
                                string A18line = TextLine;
                                do
                                {
                                    TextLine = sr.ReadLine();
                                    if (TextLine == null)
                                        break;
                                    if (LEFT(TextLine, 2) != "A1" && LEFT(TextLine, 2) != "A2")
                                    {
                                        A18line = A18line + TextLine;
                                    }
                                } while (LEFT(TextLine, 2) != "A1" && LEFT(TextLine, 2) != "A2");
                                // //WriteLog All, "INF", "Line A17: " + A18line
                                ParseA18(gT5ID, A18line);
                            }
                        }
                        else
                        {

                            break;
                        }

                        if (T5ID > 0)
                        {
                            if (LEFT(TextLine, 3) == "A19")
                            {
                                string A19line = TextLine;
                                do
                                {
                                    TextLine = sr.ReadLine();
                                    if (TextLine == null)
                                        break;
                                    if (LEFT(TextLine, 2) != "A1" && LEFT(TextLine, 2) != "A2")
                                    {
                                        A19line = A19line + TextLine;
                                    }
                                } while (LEFT(TextLine, 2) != "A1" && LEFT(TextLine, 2) != "A2");
                                // //WriteLog All, "INF", "Line A19: " + A19line
                                ParseA19(gT5ID, A19line);
                            }
                        }
                        else
                        {

                            break;
                        }

                        if (T5ID > 0)
                        {
                            if (LEFT(TextLine, 3) == "A20")
                            {
                                string A20line = TextLine;
                                do
                                {
                                    TextLine = sr.ReadLine();
                                    if (TextLine == null)
                                        break;
                                    if (LEFT(TextLine, 2) != "A1" && LEFT(TextLine, 2) != "A2")
                                    {
                                        A20line = A20line + TextLine;
                                    }
                                } while (LEFT(TextLine, 2) != "A1" && LEFT(TextLine, 2) != "A2");
                                // //WriteLog All, "INF", "Line A20: " + A20line
                                ParseA20(gT5ID, A20line);
                            }
                        }
                        else
                        {

                            break;
                        }

                        if (T5ID > 0)
                        {
                            if (LEFT(TextLine, 3) == "A21")
                            {
                                string A21line = TextLine;
                                do
                                {
                                    TextLine = sr.ReadLine();
                                    if (TextLine == null)
                                        break;
                                    if (LEFT(TextLine, 2) != "A1" && LEFT(TextLine, 2) != "A2")
                                    {
                                        A21line = A21line + TextLine;
                                    }
                                } while (LEFT(TextLine, 2) != "A1" && LEFT(TextLine, 2) != "A2");
                                // //WriteLog All, "INF", "Line A21: " + A21line
                                ParseA21(gT5ID, A21line);
                            }
                        }
                        else
                        {

                            break;
                        }

                        if (T5ID > 0)
                        {
                            if (LEFT(TextLine, 3) == "A22")
                            {
                                string A22line = TextLine;
                                do
                                {
                                    TextLine = sr.ReadLine();
                                    if (TextLine == null)
                                        break;
                                    if (LEFT(TextLine, 2) != "A1" && LEFT(TextLine, 2) != "A2")
                                    {
                                        A22line = A22line + TextLine;
                                    }
                                } while (LEFT(TextLine, 2) != "A1" && LEFT(TextLine, 2) != "A2");
                                // //WriteLog All, "INF", "Line A22: " + A22line
                                ParseA22(gT5ID, A22line);
                            }
                        }
                        else
                        {

                            break;
                        }

                        if (T5ID > 0)
                        {
                            if (LEFT(TextLine, 3) == "A23")
                            {
                                string A23line = TextLine;
                                do
                                {
                                    TextLine = sr.ReadLine();
                                    if (TextLine == null)
                                        break;
                                    if (LEFT(TextLine, 2) != "A1" && LEFT(TextLine, 2) != "A2")
                                    {
                                        A23line = A23line + TextLine;
                                    }
                                } while (LEFT(TextLine, 2) != "A1" && LEFT(TextLine, 2) != "A2");
                                // //WriteLog All, "INF", "Line A23: " + A23line
                                ParseA23(gT5ID, A23line);
                            }
                        }
                        else
                        {

                            break;
                        }

                        if (T5ID > 0)
                        {
                            if (LEFT(TextLine, 3) == "A24")
                            {
                                string A24line = TextLine;
                                do
                                {
                                    TextLine = sr.ReadLine();
                                    if (TextLine == null)
                                        break;
                                    if (LEFT(TextLine, 2) != "A1" && LEFT(TextLine, 2) != "A2")
                                    {
                                        A24line = A24line + TextLine;
                                    }
                                } while (LEFT(TextLine, 2) != "A1" && LEFT(TextLine, 2) != "A2");
                                // //WriteLog All, "INF", "Line A24: " + A24line
                                ParseA24(gT5ID, A24line);
                            }
                        }
                        else
                        {

                            break;
                        }
                        if (T5ID > 0)
                        {
                            if (LEFT(TextLine, 3) == "A26")
                            {
                                string A26line = TextLine;
                                do
                                {
                                    TextLine = sr.ReadLine();
                                    if (TextLine == null)
                                        break;
                                    if (LEFT(TextLine, 2) != "A1" && LEFT(TextLine, 2) != "A2")
                                    {
                                        A26line = A26line + TextLine;
                                    }
                                } while (LEFT(TextLine, 2) != "A1" && LEFT(TextLine, 2) != "A2");
                                // //WriteLog All, "INF", "Line A26: " + A26line
                                ParseA26(gT5ID, A26line);
                            }
                        }
                        else
                        {

                            break;
                        }

                    }
                    MirFile.Close();
                    // 'After processing move the file to success or fail folder
                    if (T5OK && A02OK && A04OK && A07OK && A11OK && !fileError && !databaseError)
                    {
                        success = true;
                    }
                    //d = Now()
                    string suffix = DateTime.Now.ToString();
                    if (success)
                    {
                        //fso.MoveFile mirpath + "\\" + MirFileName, mirpathsuccess + "\\" + MirFileName + "." + suffix  
                    }
                    else
                    {
                        //'Changed by Eric - version 1.01
                        string reason = "";
                        if (!T5OK)
                        {
                            reason = reason + " - Header T5 not present - file incomplete - ";
                        }
                        if (!A02OK)
                        {
                            reason = reason + " - Section A02 not present - file incomplete - ";
                        }
                        if (!A04OK)
                        {
                            reason = reason + " - Section A04 not present - file incomplete - ";
                        }
                        if (!A07OK)
                        {
                            reason = reason + " - Section A07 not present - file incomplete - ";
                        }
                        if (!A11OK)
                        {
                            reason = reason + " - Section A11 not present - file incomplete - ";
                        }
                        if (fileError)
                        {
                            reason = reason + " - File error encountered - ";
                        }
                        if (databaseError)
                        {
                            reason = reason + " - Database error encountered - ";
                        }
                        // removeT5ID(gT5ID);
                        // //WriteLog Error, "Err", " Failed parsing file: " + MirFileName + " - " + reason
                        //'End change - version 1.01 
                        //fso.MoveFile mirpath + "\\" + MirFileName, mirpathfail + "\\" + MirFileName + "." +   suffix
                    }

                    

                    var FilePath = Path.Combine(path, fileName);

                    FileInfo fn = new FileInfo(fileName);
                    string Ref_Filename = fn.Name;

                    string destpath = fn.DirectoryName;

                   


                    destpath = destpath.Remove(destpath.Length - 13) + "ReadFiles\\";

                    string DTime = DateTime.Now.ToString("dd MMMM yyyy");

                    destpath = destpath +DTime+"\\";


                    //Create Folder DateWise
                    if (!Directory.Exists(destpath))
                    {
                        Directory.CreateDirectory(destpath);
                    }

                    destpath = destpath + PNR + "_" + RefFilename;
                    // string Destpath = HttpContext.Current.Server.MapPath();

                    if (System.IO.File.Exists(FilePath))
                    {
                        // System.IO.File.Delete(FilePath);
                        File.Move(FilePath, destpath);
                    }
                    //SqlConnection connection = new SqlConnection(connstr);
                    //connection.Open();
                    //string PnrQuery = "update FileReading set status=1 where FileName='" + fileName + "'";
                    //SqlCommand pnrCommand = new SqlCommand(PnrQuery, connection);
                    //int count = Convert.ToInt32(pnrCommand.ExecuteNonQuery());

                    //if (count != 0)
                    //{
  
                    //}


                }
            }
        }






       














        ///************************************************************************************************************
        ///************************************************************************************************************
        // ParseA26
        //************************************************************************************************************
        void ParseA26(string pT5ID, string sData)
        {
            //Pos = spec + 1


            string table = "a26_non_host_cnt";

            string fields = "(T5ID, LINE)";

            string values = "(" + pT5ID + ",'" + sData + "')";

            WriteToDatabase(table, fields, values);

        }


        //************************************************************************************************************
        //ParseA24
        //************************************************************************************************************
        void ParseA24(string pT5ID, string sData)
        {

            //Pos = spec + 1


            string table = "a24_other_fare";

            string fields = "(T5ID, LINE)";

            string values = "(" + pT5ID + ",'" + sData + "')";

            WriteToDatabase(table, fields, values);

        }


        //************************************************************************************************************
        //ParseA23
        //************************************************************************************************************
        void ParseA23(string pT5ID, string sData)
        {

            //'Pos = spec + 1
            string A23SEC = LEFT(sData, 3);
            string A23TKT = MID(sData, 4, 14);
            string A23CON = MID(sData, 18, 1);
            string A23RTK = MID(sData, 19, 14);
            string A23DOI = MID(sData, 33, 7);
            string A23INV = MID(sData, 40, 9);
            string A23NME = MID(sData, 49, 33);
            string A23CDI = MID(sData, 89, 1);


            string table = "a23_refund_det";

            string fields = "(T5ID, LINE, A23SEC, A23TKT, A23CON, A23RTK, A23DOI, A23INV, A23NME, A23CDI)";

            string values = "(" + pT5ID + ",'" + sData + "','" + A23SEC + "','" + A23TKT + "','" + A23CON + "','" +
                          A23RTK + "','" + A23DOI + "','" + A23INV + "','" + A23NME + "','" + A23CDI + "')";

            WriteToDatabase(table, fields, values);

        }



        //************************************************************************************************************
        //ParseA22
        //************************************************************************************************************
        void ParseA22(string pT5ID, string sData)
        {

            //'Pos = spec + 1


            string table = "a22_1G_seat_det";

            string fields = "(T5ID, LINE)";

            string values = "(" + pT5ID + ",'" + sData + "')";

            WriteToDatabase(table, fields, values);

        }


        //************************************************************************************************************
        //ParseA21
        //************************************************************************************************************
        void ParseA21(string pT5ID, string sData)
        {

            //'Pos = spec + 1
            string A21SEC = LEFT(sData, 3);
            string A21NFC = MID(sData, 4, 3);
            string A21NRT = MID(sData, 7, 12);
            string A21NAI = MID(sData, 19, 20);
            string A21NVC = MID(sData, 39, 20);
            string A21ITC = MID(sData, 59, 12);
            string A21NFR = MID(sData, 71, 3);


            string table = "a21_net_remit";

            string fields = "(T5ID, LINE,A21SEC, A21NFC, A21NRT, A21NAI, A21NVC, A21ITC, A21NFR)";

            string values = "(" + pT5ID + ",'" + sData + "','" + A21SEC + "','" + A21NFC + "','" +
                          A21NRT + "','" + A21NAI + "','" + A21NVC + "','" + A21ITC + "','" +
                          A21NFR + "')";

            WriteToDatabase(table, fields, values);

        }



        //************************************************************************************************************
        //ParseA20
        //************************************************************************************************************
        void ParseA20(string pT5ID, string sData)
        {

            //'Pos = spec + 1


            string table = "a20_ssr_det";

            string fields = "(T5ID, LINE)";

            string values = "(" + pT5ID + ",'" + sData + "')";

            WriteToDatabase(table, fields, values);

        }

        //************************************************************************************************************
        //ParseA19
        //************************************************************************************************************
        void ParseA19(string pT5ID, string sData)
        {

            //'Pos = spec + 1



            string table = "a19_misc_docs";

            string fields = "(T5ID, LINE)";

            string values = "(" + pT5ID + ",'" + sData + "')";

            WriteToDatabase(table, fields, values);

        }


        //************************************************************************************************************
        //ParseA18 
        //************************************************************************************************************
        void ParseA18(string pT5ID, string sData)
        {

            //'Pos = spec + 1



            string table = "a18_etdn_info";

            string fields = "(T5ID, LINE)";

            string values = "(" + pT5ID + ",'" + sData + "')";

            WriteToDatabase(table, fields, values);

        }


        //************************************************************************************************************
        //ParseA17 
        //************************************************************************************************************
        void ParseA17(string pT5ID, string sData)
        {

            //'Pos = spec + 1



            string table = "a17_leisure_det";

            string fields = "(T5ID, LINE)";

            string values = "(" + pT5ID + ",'" + sData + "')";

            WriteToDatabase(table, fields, values);

        }



        //************************************************************************************************************
        //ParseA16 
        //************************************************************************************************************
        void ParseA16(string pT5ID, string sData)
        {

            //'Pos = spec + 1



            string table = "a16_aux_det";

            string fields = "(T5ID, LINE)";

            string values = "(" + pT5ID + ",'" + sData + "')";

            WriteToDatabase(table, fields, values);

        }


        //************************************************************************************************************
        //ParseA15 
        //************************************************************************************************************
        void ParseA15(string pT5ID, string sData)
        {

            //'Pos = spec + 1
            string A15SEC = LEFT(sData, 3);
            string A15SEG = MID(sData, 4, 2);
            string A15RMK = MID(sData, 6, 88);


            string table = "a15_assoc_remarks";

            string fields = "(T5ID, LINE, A15SEC, A15SEG, A15RMK)";

            string values = "(" + pT5ID + ",'" + sData + "','" + A15SEC + "','" + A15SEG + "','" + A15RMK + "')";

            WriteToDatabase(table, fields, values);

        }



        //************************************************************************************************************
        //ParseA14 
        //************************************************************************************************************
        void ParseA14(string pT5ID, string sData)
        {

            var clnt = "";
            if (sData.IndexOf('*') != -1)
            {
                var sss = sData.Split('-');
                var spltCon = sss[1];
                var rmp = spltCon.Split('*');
                var Consul = "";
                if (rmp[1] == "C C" || rmp.Length == 3)
                {
                    Consul = rmp[rmp.Length - 2];
                    if (rmp[1].Length <= 4)
                    {
                        clnt = rmp[rmp.Length - 1];
                    }
                    else
                    {
                        clnt = rmp[1];
                    }
                }
                else
                {
                    Consul = "";
                    if (rmp[1].Length <= 2)
                    {
                        clnt = rmp[rmp.Length - 1];
                    }
                    else
                    {
                        clnt = rmp[1];
                    }
                }

                var Consulname = Consul.Split(' ');
                var cnslnme = Consulname[0];

                string A14SEC = LEFT(sData, 3);
                string A14RMK = MID(sData, 4, 64);


                var totstr = spltCon.Replace('*', '=');


                string table = "a14_tkt_remarks";

                string fields = "(T5ID, LINE, A14SEC, A14RMK,client, A14Consultant)";

                string values = "(" + pT5ID + ",'" + sData + "','" + A14SEC + "','" + totstr + "','" + clnt + "','" + cnslnme + "')";

                WriteToDatabase(table, fields, values);
            }
            else
            {
                clnt = sData;
            }
            //'Pos = spec + 1

            //'Pos = spec + 1
            //string A14SEC = LEFT(sData, 3);
            //string A14RMK = MID(sData, 4, 64);
            //// string client = sData.Split("M*").Last();

            //string client = sData.Substring(sData.IndexOf("M*") + 2);
            //string consultant = sData.Substring(sData.IndexOf("C*") + 2, 2);
            //SqlConnection connection = new SqlConnection(connstr);
            //connection.Open();
            //string PnrQuery = "select Count(T5ID) from a14_tkt_remarks  GROUP BY T5ID  having T5ID='" + pT5ID + "'";
            //SqlCommand pnrCommand = new SqlCommand(PnrQuery, connection);
            //int count = Convert.ToInt32(pnrCommand.ExecuteScalar());
            //if (count != Convert.ToInt32(Ticketremarkcount))
            //{

            //    string table = "a14_tkt_remarks";

            //    string fields = "(T5ID, LINE, A14SEC, A14RMK,client,consultant )";

            //    string values = "(" + pT5ID + ",'" + sData + "','" + A14SEC + "','" + A14RMK + "','" + client + "','" + consultant + "')";

            //    WriteToDatabase(table, fields, values);
            //}
        }


        //************************************************************************************************************
        //ParseA13 
        //************************************************************************************************************
        void ParseA13(string pT5ID, string sData)
        {

            //'Pos = spec + 1
            string A13SEC = LEFT(sData, 3);
            string A13ADT = MID(sData, 4, 2);
            string A13DTA = MID(sData, 6, 223);



            string table = "a13_addr_det";

            string fields = "(T5ID, LINE, A13SEC, A13ADT, A13DTA)";

            string values = "(" + pT5ID + ",'" + sData + "','" + A13SEC + "','" + A13ADT + "','" + A13DTA + "')";

            WriteToDatabase(table, fields, values);

        }


        //************************************************************************************************************
        //ParseA12 
        //************************************************************************************************************
        void ParseA12(string pT5ID, string sData)
        {

            //'Pos = spec + 1
            string A12SEC = LEFT(sData, 3);
            string A12CTY = MID(sData, 4, 3);
            string A12LOC = MID(sData, 7, 2);
            string A12PHN = MID(sData, 9, 64);

            //SqlConnection connection = new SqlConnection(connstr);
            //connection.Open();
            //string PnrQuery = "select Count(T5ID) from a12_phone_det  GROUP BY T5ID  having T5ID='" + pT5ID + "'";
            //SqlCommand pnrCommand = new SqlCommand(PnrQuery, connection);
            //int count = Convert.ToInt32(pnrCommand.ExecuteScalar());
            //if (count != Convert.ToInt32(Phonecount))
            //{

            string table = "a12_phone_det";

            string fields = "(T5ID, LINE, A12SEC, A12CTY, A12LOC, A12PHN)";


            string values = "(" + pT5ID + ",'" + sData + "','" + A12SEC + "','" + A12CTY + "','" +
                         A12LOC + "','" + A12PHN + "')";

            WriteToDatabase(table, fields, values);


        }


        //************************************************************************************************************
        //ParseA11 
        //************************************************************************************************************
        void ParseA11(string pT5ID, string sData)
        {

            //'Pos = spec + 1
            string A11SEC = LEFT(sData, 3);
            string A11TYP = MID(sData, 4, 2);
            string A11AMT = MID(sData, 6, 12);
            string A11REF = MID(sData, 18, 1);
            string A11CCC = MID(sData, 19, 2);
            string A11CCN = MID(sData, 21, 20);
            string A11EXP = MID(sData, 41, 4);
            string A11MAN = MID(sData, 46, 1);
            string A11APC = MID(sData, 47, 8);
            string A11PPO = MID(sData, 53, 3);

            //SqlConnection connection = new SqlConnection(connstr);
            //connection.Open();
            //string PnrQuery = "select Count(T5ID) from a11_payment_form_det  GROUP BY T5ID  having T5ID='" + pT5ID + "'";
            //SqlCommand pnrCommand = new SqlCommand(PnrQuery, connection);
            //int count = Convert.ToInt32(pnrCommand.ExecuteScalar());
            //if (count != Convert.ToInt32(Paymmentitemcount))
            //{


            string table = "a11_payment_form_det";

            string fields = "(T5ID, LINE, A11SEC, A11TYP, A11AMT, A11REF, A11CCC, A11CCN, A11EXP, A11MAN, A11APC," +
                      "A11PPO)";


            string values = "(" + pT5ID + ",'" + sData + "','" + A11SEC + "','" + A11TYP + "','" +
                         A11AMT + "','" + A11REF + "','" + A11CCC + "','" + A11CCN + "','" +
                         A11EXP + "','" + A11MAN + "','" + A11APC + "','" + A11PPO + "')";

            WriteToDatabase(table, fields, values);


        }


        //************************************************************************************************************
        //ParseA10 
        //************************************************************************************************************
        void ParseA10(string pT5ID, string sData)
        {

            //'Pos = spec + 1
            string A10SEC = LEFT(sData, 3);
            string A10EXI = MID(sData, 4, 2);
            string A10OTN = "";
            string AAP = MID(sData, 80, 8);
            string A10OIN = "";
            string A10OTF = "";
            if (AAP.IndexOf("TI:").ToString() == "-1")
            {
                A10OTN = MID(sData, 94, 10);

                string A10OTG = MID(sData, 109, 15);
                if (A10OTG.IndexOf("TI:").ToString() == "-1")
                {
                    A10OTF = MID(sData, 112, 12);
                }
                else
                {
                    A10OTF = MID(sData, 133, 12);
                }
            }

            string A10ONB = MID(sData, 20, 2);
            string A10OCI = MID(sData, 22, 4);

            string A10TYP = MID(sData, 35, 1);
            string A10CUR = MID(sData, 36, 3);

            string A10PEN = MID(sData, 59, 9);

            string A10SCC = "";
            string A10C01 = "";
            string A10TI1 = "";
            string A10TT1 = "";
            string A10TC1 = "";

            string A10TI2 = "";
            string A10TT2 = "";
            string A10TC2 = "";
            string A10TI3 = "";
            string A10TT3 = "";
            string A10TC3 = "";


            string A10TI4 = "";
            string A10TT4 = "";
            string A10TC4 = "";
            string A10TI5 = "";
            string A10TT5 = "";
            string A10TC5 = "";

            string A10IT2 = "";
            string A10IT2C = "";
            var CHKSTR = MID(sData, 58, 30);
            if (CHKSTR.IndexOf("TI:").ToString() == "-1")
            {
                A10SCC = MID(sData, 58, 2);
                A10C01 = MID(sData, 91, 1);

                string A10OTH = MID(sData, 109, 15);
                if (A10OTH.IndexOf("TI:").ToString() == "-1")
                {
                    A10TI1 = MID(sData, 127, 3);
                    A10TT1 = MID(sData, 130, 8);
                    A10TC1 = MID(sData, 138, 2);

                    A10TI2 = MID(sData, 140, 3);
                    A10TT2 = MID(sData, 143, 8);
                    A10TC2 = MID(sData, 151, 2);
                    A10TI3 = MID(sData, 153, 3);
                    A10TT3 = MID(sData, 156, 8);
                    A10TC3 = MID(sData, 164, 2);


                    A10TI4 = MID(sData, 166, 3);
                    A10TT4 = MID(sData, 169, 8);
                    A10TC4 = MID(sData, 177, 2);
                    A10TI5 = MID(sData, 179, 3);
                    A10TT5 = MID(sData, 194, 10);
                    A10TC5 = MID(sData, 190, 2);
                    string OICHK = MID(sData, 216, 3);
                    if (OICHK.IndexOf("OI:").ToString() == "-1")
                    {
                        A10IT2 = MID(sData, 229, 8);
                        A10IT2C = MID(sData, 237, 2);
                    }
                    else
                    {
                        A10IT2 = "0.00";
                        A10IT2C = MID(sData, 237, 2);
                    }


                }



            }
            else
            {
                A10SCC = MID(sData, 73, 8);
                A10C01 = MID(sData, 84, 1);
                string A10DER = MID(sData, 98, 10);
                if (A10DER.IndexOf("TI:").ToString() == "-1")
                {
                    A10TI1 = MID(sData, 120, 3);
                    A10TT1 = MID(sData, 123, 8);
                    A10TC1 = MID(sData, 131, 2);

                    A10TI2 = MID(sData, 133, 3);
                    A10TT2 = MID(sData, 136, 8);
                    A10TC2 = MID(sData, 144, 2);
                    A10TI3 = MID(sData, 146, 3);
                    A10TT3 = MID(sData, 149, 8);
                    A10TC3 = MID(sData, 157, 2);


                    A10TI4 = MID(sData, 159, 3);
                    A10TT4 = MID(sData, 162, 8);
                    A10TC4 = MID(sData, 177, 2);
                    A10TI5 = MID(sData, 172, 3);
                    A10TT5 = MID(sData, 189, 8);
                    A10TC5 = MID(sData, 199, 2);
                    string OICHK = MID(sData, 209, 3);
                    if (OICHK.IndexOf("OI:").ToString() == "-1")
                    {
                        A10IT2 = MID(sData, 222, 8);
                        A10IT2C = MID(sData, 258, 2);
                    }
                    else
                    {
                        A10IT2 = "0.00";
                        A10IT2C = MID(sData, 237, 2);
                    }

                }
                else
                {
                    A10TI1 = MID(sData, 141, 3);
                    A10TT1 = MID(sData, 144, 8);
                    A10TC1 = MID(sData, 152, 2);

                    A10TI2 = MID(sData, 154, 3);
                    A10TT2 = MID(sData, 157, 8);
                    A10TC2 = MID(sData, 165, 2);
                    A10TI3 = MID(sData, 167, 3);
                    A10TT3 = MID(sData, 170, 8);
                    A10TC3 = MID(sData, 178, 2);


                    A10TI4 = MID(sData, 180, 3);
                    A10TT4 = MID(sData, 183, 8);
                    A10TC4 = MID(sData, 198, 2);
                    A10TI5 = MID(sData, 193, 3);
                    A10TT5 = MID(sData, 210, 8);
                    A10TC5 = MID(sData, 220, 2);

                    A10IT2 = MID(sData, 243, 8);
                    A10IT2C = MID(sData, 258, 2);
                }


            }




            string table = "a10_exg_tkt_info";

            string fields = "(T5ID, LINE, A10SEC, A10EXI, A10OTN, A10ONB, A10OCI, A10OIN, A10TYP, A10CUR, A10OTF, A10PEN, A10SCC,A10C01,A10TI1,A10TT1,A10TC1,A10TI2,A10TT2,A10TC2,A10TI3,A10TT3,A10TC3,A10TI4,A10TT4,A10TC4,A10TI5,A10TT5,A10TC5,A10IT2,A10IT2C)";


            string values = "(" + pT5ID + ",'" + sData + "','" + A10SEC + "','" + A10EXI + "','" + A10OTN + "','" + A10ONB + "','" + A10OCI + "','" + A10OIN + "','" + A10TYP + "','" + A10CUR + "','" + A10OTF + "','" + A10PEN + "','" + A10SCC + "','" + A10C01 + "','" + A10TI1 + "','" + A10TT1 + "','" + A10TC1 + "','" + A10TI2 + "','" + A10TT2 + "','" + A10TC2 + "','" + A10TI3 + "','" + A10TT3 + "','" + A10TC3 + "','" + A10TI4 + "','" + A10TT4 + "','" + A10TC4 + "','" + A10TI5 + "','" + A10TT5 + "','" + A10TC5 + "','" + A10IT2 + "','" + A10IT2C + "')";


            WriteToDatabase(table, fields, values);



            //'Pos = spec + 1
            //string A10SEC = LEFT(sData, 3);
            //string A10EXI = MID(sData, 4, 2);
            //string A10OTN = MID(sData, 6, 13);
            //string A10ONB = MID(sData, 20, 2);
            //string A10OCI = MID(sData, 22, 4);
            //string A10OIN = MID(sData, 26, 9);
            //string A10TYP = MID(sData, 35, 1);
            //string A10CUR = MID(sData, 36, 3);
            //string A10OTF = MID(sData, 39, 12);
            //string A10PEN = MID(sData, 51, 9);
            //string A10SCC = MID(sData, 60, 9);



            //string table = "a10_exg_tkt_info";

            //string fields = "(T5ID, LINE, A10SEC, A10EXI, A10OTN, A10ONB, A10OCI, A10OIN, A10TYP, A10CUR, A10OTF, A10PEN, A10SCC)";


            //string values = "(" + pT5ID + ",'" + sData + "','" + A10SEC + "','" + A10EXI + "','" +
            //              A10OTN + "','" + A10ONB + "','" + A10OCI + "','" + A10OIN + "','" +
            //              A10TYP + "','" + A10CUR + "','" + A10OTF + "','" + A10PEN + "','" +
            //              A10SCC + "')";


            //WriteToDatabase(table, fields, values);

        }

        //************************************************************************************************************
        //ParseA09 
        //************************************************************************************************************
        void ParseA09(string pT5ID, string sData)
        {

            //'Pos = spec + 1
            string A09SEC = LEFT(sData, 3);
            string A09FSI = MID(sData, 4, 2);
            string A09TY5 = MID(sData, 6, 1);
            string A09L51 = MID(sData, 7, 61);



            //SqlConnection connection = new SqlConnection(connstr);
            //connection.Open();
            //string PnrQuery = "select Count(*) from a09_fare_construct_det where T5ID='" + pT5ID + "'";
            //SqlCommand pnrCommand = new SqlCommand(PnrQuery, connection);
            //int count = (int)pnrCommand.ExecuteScalar();
            //if (count == 0)
            //{

            string table = "a09_fare_construct_det";

            string fields = "(T5ID, LINE, A09SEC, A09FSI, A09TY5, A09L51)";


            string values = "(" + pT5ID + ",'" + sData + "','" + A09SEC + "','" + A09FSI + "','" +
                          A09TY5 + "','" + A09L51 + "')";

            WriteToDatabase(table, fields, values);

            //    }
        }



        //************************************************************************************************************
        //ParseA08 
        //************************************************************************************************************
        void ParseA08(string pT5ID, string sData)
        {

            //'Pos = spec + 1
            string A08SEC = LEFT(sData, 3);
            string A08FSI = MID(sData, 4, 2);
            string A08ITN = MID(sData, 6, 2);
            string A08FBC = MID(sData, 8, 8);
            string A08VAL = MID(sData, 16, 8);
            string A08NVBC = MID(sData, 24, 7);
            string A08NVAC = MID(sData, 31, 7);
            string A08TDGC = MID(sData, 38, 6);

            //SqlConnection connection = new SqlConnection(connstr);
            //connection.Open();
            //string PnrQuery = "select Count(*) from a08_fare_base_det where T5ID='" + pT5ID + "'";
            //SqlCommand pnrCommand = new SqlCommand(PnrQuery, connection);
            //int count = (int)pnrCommand.ExecuteScalar();
            //if (count == 0)
            //{

            string table = "a08_fare_base_det";

            string fields = "(T5ID, LINE, A08SEC, A08FSI, A08ITN, A08FBC, A08VAL, A08NVBC, A08NVAC, A08TDGC)";


            string values = "(" + pT5ID + ",'" + sData + "','" + A08SEC + "','" + A08FSI + "','" +
                          A08ITN + "','" + A08FBC + "','" + A08VAL + "','" + A08NVBC + "','" +
                          A08NVAC + "','" + A08TDGC + "')";


            WriteToDatabase(table, fields, values);

            //   }
        }



        //************************************************************************************************************
        //ParseA07 
        //************************************************************************************************************
        void ParseA07(string pT5ID, string sData)
        {

            //'Pos = spec + 1
            string A07SEC = LEFT(sData, 3);
            string A07FSI = MID(sData, 4, 2);
            string A07CRB = MID(sData, 6, 3);
            string A07TBF = MID(sData, 9, 12);
            string A07CRT = MID(sData, 21, 3);
            string A07TTA = MID(sData, 24, 12);
            string A07CRE = MID(sData, 36, 3);
            string A07EQV = MID(sData, 39, 12);
            string A07CUR = string.Empty;
            string A07TI1 = string.Empty;
            string A07TT1 = string.Empty;
            string A07TC1 = string.Empty;
            string A07TI2 = string.Empty;
            string A07TT2 = string.Empty;
            string A07TC2 = string.Empty;
            string A07TI3 = string.Empty;
            string A07TT3 = string.Empty;
            string A07TC3 = string.Empty;
            if (sData.Length > 51)
            {
                A07CUR = MID(sData, 51, 3);
                A07TI1 = MID(sData, 54, 3);
                A07TT1 = MID(sData, 57, 8);
                A07TC1 = MID(sData, 65, 2);
                A07TI2 = MID(sData, 67, 3);
                A07TT2 = MID(sData, 70, 8);
                A07TC2 = MID(sData, 78, 2);
                A07TI3 = MID(sData, 80, 3);
                A07TT3 = MID(sData, 83, 8);
                A07TC3 = MID(sData, 91, 2);
            }

            //SqlConnection connection = new SqlConnection(connstr);
            //connection.Open();
            //string PnrQuery = "select Count(*) from a07_fare_value_det where T5ID='" + pT5ID + "'";
            //SqlCommand pnrCommand = new SqlCommand(PnrQuery, connection);
            //int count = (int)pnrCommand.ExecuteScalar();
            //if (count == 0)
            //{


            string table = "a07_fare_value_det";

            string fields = "(T5ID, LINE, A07SEC, A07FSI, A07CRB, A07TBF, A07CRT, A07TTA, A07CRE, A07EQV," +
                     "A07CUR, A07TI1, A07TT1, A07TC1, A07TI2, A07TT2, A07TC2, A07TI3, A07TT3, A07TC3)";


            string values = "(" + pT5ID + ",'" + sData + "','" + A07SEC + "','" + A07FSI + "','" +
                          A07CRB + "','" + A07TBF + "','" + A07CRT + "','" + A07TTA + "','" + A07CRE + "','" +
                          A07EQV + "','" + A07CUR + "','" + A07TI1 + "','" + A07TT1 + "','" +
                          A07TC1 + "','" + A07TI2 + "','" + A07TT2 + "','" + A07TC2 + "','" +
                          A07TI3 + "','" + A07TT3 + "','" + A07TC3 + "')";

            WriteToDatabase(table, fields, values);

            //  }
        }



        //************************************************************************************************************
        //ParseA06 
        //************************************************************************************************************
        void ParseA06(string pT5ID, string sData)
        {

            //'Pos = spec + 1
            string A06SEC = LEFT(sData, 3);
            string A06SEG = MID(sData, 4, 2);
            string A06SEN = MID(sData, 6, 3);
            string A06SMK = MID(sData, 9, 1);



            string table = "a06_apollo_seat_det";

            string fields = "(T5ID, LINE, A06SEC, A06SEG, A06SEN, A06SMK)";


            string values = "(" + pT5ID + ",'" + sData + "','" + A06SEC + "','" + A06SEG + "','" +
                       A06SEN + "','" + A06SMK + "')";

            WriteToDatabase(table, fields, values);

        }



        //************************************************************************************************************
        //ParseA05 
        //************************************************************************************************************
        void ParseA05(string pT5ID, string sData)
        {

            //'Pos = spec + 1
            string A05SEC = LEFT(sData, 3);
            string A05ITN = MID(sData, 4, 2);
            string A05CDE = MID(sData, 6, 2);
            string A05NUM = MID(sData, 8, 3);
            string A05NME = MID(sData, 11, 12);
            string A05FLT = MID(sData, 23, 4);
            string A05CLS = MID(sData, 27, 2);
            string A05STS = MID(sData, 29, 2);
            string A05DTE = MID(sData, 31, 5);
            string A05TME = MID(sData, 36, 5);
            string A05ARV = MID(sData, 41, 5);
            string A05IND = MID(sData, 46, 1);
            string A05OCC = MID(sData, 47, 3);
            string A05OCN = MID(sData, 50, 13);
            string A05DCC = MID(sData, 63, 3);
            string A05DCN = MID(sData, 66, 13);
            string A05SVC = MID(sData, 79, 4);
            string A05STP = MID(sData, 83, 1);




            string table = "a05_waitlist_det";

            string fields = "(T5ID, LINE, A05SEC, A05ITN, A05CDE, A05NUM, A05NME, A05FLT, A05CLS, A05STS, A05DTE," +
                      "A05TME, A05ARV, A05IND, A05OCC, A05OCN, A05DCC, A05DCN, A05SVC, A05STP)";


            string values = "(" + pT5ID + ",'" + sData + "','" + A05SEC + "','" + A05ITN + "','" +
                       A05CDE + "','" + A05NUM + "','" + A05NME + "','" + A05FLT + "','" +
                       A05CLS + "','" + A05STS + "','" + A05DTE + "','" + A05TME + "','" +
                       A05ARV + "','" + A05IND + "','" + A05OCC + "','" + A05OCN + "','" +
                       A05DCC + "','" + A05DCN + "','" + A05SVC + "','" + A05STP + "')";

            WriteToDatabase(table, fields, values);

        }



        //************************************************************************************************************
        //ParseA04 
        //************************************************************************************************************
        void ParseA04(string pT5ID, string sData)
        {

            //'Pos = spec + 1
            string A04SEC = LEFT(sData, 3);
            string A04ITN = MID(sData, 4, 2);
            string A04CDE = MID(sData, 6, 2);
            string A04NUM = MID(sData, 8, 3);
            string A04NME = MID(sData, 11, 12);
            string A04FLT = MID(sData, 23, 4);
            string A04CLS = MID(sData, 27, 2);
            string A04STS = MID(sData, 29, 2);
            string A04DTE = MID(sData, 31, 5);
            string A04TME = MID(sData, 36, 5);
            string A04ARV = MID(sData, 41, 5);
            string A04IND = MID(sData, 46, 1);
            string A04OCC = MID(sData, 47, 3);
            string A04OCN = MID(sData, 50, 13);
            string A04DCC = MID(sData, 63, 3);
            string A04DCN = MID(sData, 66, 13);
            string A04DOM = MID(sData, 79, 1);
            string A04SET = MID(sData, 80, 1);
            string A04SVC = MID(sData, 81, 4);
            string A04STP = MID(sData, 85, 1);
            string A04STO = MID(sData, 86, 1);
            string A04BAG = MID(sData, 87, 3);
            string A04AIR = MID(sData, 90, 4);
            string A04DTR = MID(sData, 94, 3);
            string A04MIL = MID(sData, 97, 5);
            string A04FCI = MID(sData, 102, 1);
            string A04SIC = MID(sData, 103, 1);

            //  SqlConnection connection = new SqlConnection(connstr);
            //connection.Open();
            //string PnrQuery = "select Count(*) from a04_airline_det where T5ID='" + pT5ID + "'";
            //SqlCommand pnrCommand = new SqlCommand(PnrQuery, connection);
            //int count = (int)pnrCommand.ExecuteScalar();
            //if (count == 0)
            //{

            string table = "a04_airline_det";

            string fields = "(T5ID, LINE, A04SEC, A04ITN, A04CDE, A04NUM, A04NME, A04FLT, A04CLS, A04STS, A04DTE, A04TME," +
                      "A04ARV, A04IND, A04OCC, A04OCN, A04DCC, A04DCN, A04DOM, A04SET, A04SVC, A04STP, A04STO," +
                      "A04BAG, A04AIR, A04DTR, A04MIL, A04FCI, A04SIC)";

            string values = "(" + pT5ID + ",'" + sData + "','" + A04SEC + "','" + A04ITN + "','" +
                         A04CDE + "','" + A04NUM + "','" + A04NME + "','" + A04FLT + "','" +
                         A04CLS + "','" + A04STS + "','" + A04DTE + "','" + A04TME + "','" +
                         A04ARV + "','" + A04IND + "','" + A04OCC + "','" + A04OCN + "','" +
                         A04DCC + "','" + A04DCN + "','" + A04DOM + "','" + A04SET + "','" +
                         A04SVC + "','" + A04STP + "','" + A04STO + "','" + A04BAG + "','" +
                         A04AIR + "','" + A04DTR + "','" + A04MIL + "','" + A04FCI + "','" +
                         A04SIC + "')";

            WriteToDatabase(table, fields, values);
            //  }
        }



        //************************************************************************************************************
        //ParseA03 
        //************************************************************************************************************
        void ParseA03(string pT5ID, string sData)
        {

            //'Pos = spec + 1
            string A03SEC = LEFT(sData, 3);
            string A03FFP = MID(sData, 4, 21);
            string A03FCC = MID(sData, 25, 2);
            string A03FSP = MID(sData, 27, 1);
            string A03FFN = MID(sData, 28, 20);
            string A03CAI = MID(sData, 48, 1);
            string A03CAA = MID(sData, 49, 30);


            string table = "a03_freq_flyer_det";

            string fields = "(T5ID, LINE, A03SEC, A03FFP, A03FCC, A03FSP, A03FFN, A03CAI, A03CAA)";

            string values = "(" + pT5ID + ",'" + sData + "','" + A03SEC + "','" + A03FFP + "','" +
                          A03FCC + "','" + A03FSP + "','" + A03FFN + "','" +
                          A03CAI + "','" + A03CAA + "')";
            WriteToDatabase(table, fields, values);

        }


        //************************************************************************************************************
        //ParseA02 
        //************************************************************************************************************
        void ParseA02(string pT5ID, string sData)
        {

            //'Pos = spec + 1
            string A02SEC = LEFT(sData, 3);
            string A02NME = MID(sData, 4, 33);
            string A02TRN = MID(sData, 37, 11);
            string A02YIN = MID(sData, 48, 1);
            string A02TKT = MID(sData, 49, 10);
            string A02NBK = MID(sData, 59, 2);
            string A02INV = MID(sData, 61, 9);
            string A02PIC = MID(sData, 70, 6);
            string A02FIN = MID(sData, 76, 2);
            string A02EIN = MID(sData, 78, 2);
            string A02FFN = MID(sData, 80, 1);
            string A02PN1 = string.Empty;
            string A02PNR = string.Empty;
            if (sData.Length > 80)
            {
                A02PN1 = MID(sData, 82, 3);  //'Only name remark NR: are catered for
                A02PNR = MID(sData, 86, 33);
            }


            ////Chandra Commented
            //SqlConnection connection = new SqlConnection(connstr);
            //connection.Open();
            //string PnrQuery = "select Count(T5ID) from a02_passenger_det  GROUP BY T5ID  having T5ID='" + pT5ID + "'";
            //SqlCommand pnrCommand = new SqlCommand(PnrQuery, connection);
            //int count = Convert.ToInt32(pnrCommand.ExecuteScalar());
            //// Chandra Commented End
            //if (count != Convert.ToInt32(Passengercount))
            //{
            string table = "a02_passenger_det";

            string fields = "(T5ID, LINE, A02SEC, A02NME, A02TRN, A02YIN, A02TKT, A02NBK, A02INV, A02PIC, A02FIN," +
                     " A02EIN, A02FFN, A02PN1, A02PNR)";

            string values = "(" + pT5ID + ",'" + sData + "','" + A02SEC + "','" + A02NME + "','" +
                          A02TRN + "','" + A02YIN + "','" + A02TKT + "','" +
                          A02NBK + "','" + A02INV + "','" + A02PIC + "','" + A02FIN + "','" +
                          A02EIN + "','" + A02FFN + "','" + A02PN1 + "','" + A02PNR + "')";
            WriteToDatabase(table, fields, values);

            // }
        }



        //************************************************************************************************************
        //ParseA01 
        //************************************************************************************************************
        void ParseA01(string pT5ID, string sData)
        {

            //'Pos = spec + 1
            string A01SEC = LEFT(sData, 3);
            string A01CPI = MID(sData, 4, 33);


            string table = "a01_corp_name";

            string fields = "(T5ID, LINE, A01SEC, A01CPI) ";

            string values = "(" + pT5ID + ",'" + sData + "','" + A01SEC + "','" + A01CPI + "')";
            WriteToDatabase(table, fields, values);

        }



        //************************************************************************************************************
        //ParseA00 
        //************************************************************************************************************
        void ParseA00(string pT5ID, string sData)
        {

            //'Pos = spec + 1
            string A00SEC = LEFT(sData, 3);
            string A00CUS = MID(sData, 4, 43);


            string table = "a00_cust_remarks";

            string fields = "(T5ID, LINE, A00SEC, A00CUS) ";

            string values = "(" + pT5ID + ",'" + sData + "','" + A00SEC + "','" + A00CUS + "')";
            WriteToDatabase(table, fields, values);

        }

        //************************************************************************************************************
        //ParseT5 
        //************************************************************************************************************

        string Passengercount = "";
        string Paymmentitemcount = "";
        string Phonecount = "";
        string Ticketremarkcount = "";
        string RefFilename = "";
        string ParseT5(string sData, string Filename)
        {
            //'Pos = spec + 1
            string T50BID = LEFT(sData, 2);
            string T50TRC = MID(sData, 3, 2);
            string T50SPC = MID(sData, 5, 4);
            string T50MIR = MID(sData, 9, 2);
            string T50SZE = MID(sData, 11, 5);
            string T50SEQ = MID(sData, 16, 5);
            string T50DTE = MID(sData, 21, 7);
            string T50TME = MID(sData, 28, 5);
            string T50ISC = MID(sData, 33, 2);
            string T50ISA = MID(sData, 35, 3);
            string T50ISN = MID(sData, 38, 24);
            string T50DFT = MID(sData, 62, 7);
            string T50INP = MID(sData, 69, 6);
            string T50OUT = MID(sData, 75, 6);
            // strat 
            string T50BPC = MID(sData, 81, 4);
            string T50TPC = MID(sData, 85, 4);
            string T50AAN = MID(sData, 89, 9);
            string T50RCL = MID(sData, 98, 6);
            string T50ORL = MID(sData, 104, 6);
            string T50OCC = MID(sData, 110, 2);
            string T50OAM = MID(sData, 112, 1);
            string T50AGS = MID(sData, 113, 6);
            string T50SBI = MID(sData, 119, 1);
            string T50SIN = MID(sData, 120, 2);
            string T50DTY = MID(sData, 122, 2);
            string T50PNR = MID(sData, 124, 7);
            string T50EHT = MID(sData, 131, 3);
            string T50DTL = MID(sData, 134, 7);
            string T50NMC = MID(sData, 141, 3);//CR
            string T50CUR = MID(sData, 144, 3);
            string T50FAR = MID(sData, 147, 12);
            string T50DML = MID(sData, 159, 1);
            string T50CUR2 = MID(sData, 160, 3);
            string T501TX = MID(sData, 163, 8);
            string T501TC = MID(sData, 171, 2);
            string T502TX = MID(sData, 173, 8);
            string T502TC = MID(sData, 181, 2);
            string T503TX = MID(sData, 183, 8);
            string T503TC = MID(sData, 191, 2);
            string T504TX = MID(sData, 193, 8);
            string T504TC = MID(sData, 201, 2);
            string T505TX = MID(sData, 203, 8);
            string T505TC = MID(sData, 211, 2);
            string T50COM = MID(sData, 213, 8);
            string T50RTE = MID(sData, 221, 4);
            string T50ITC = MID(sData, 225, 15);//CR
            string T50IN1 = MID(sData, 240, 1);
            string T50IN2 = MID(sData, 241, 1);
            string T50IN3 = MID(sData, 242, 1);
            string T50IN4 = MID(sData, 243, 1); ;
            string T50IN5 = MID(sData, 244, 1);
            string T50IN6 = MID(sData, 245, 1);
            string T50IN7 = MID(sData, 246, 1);
            string T50IN8 = MID(sData, 247, 1);
            string T50IN9 = MID(sData, 248, 1);
            string T50IN10 = MID(sData, 249, 1);
            string T50IN11 = MID(sData, 250, 1);
            string T50IN12 = MID(sData, 251, 1);
            string T50IN13 = MID(sData, 252, 1);
            string T50IN14 = MID(sData, 253, 1);
            string T50IN15 = MID(sData, 254, 1);
            string T50IN16 = MID(sData, 255, 1);
            string T50IN17 = MID(sData, 256, 1);
            string T50PCC = MID(sData, 257, 3);
            string T50ISO = MID(sData, 260, 3);
            string T50DMI = MID(sData, 263, 2);
            string T50DST = MID(sData, 265, 1);
            string T50DPC = MID(sData, 266, 4);
            string T50DSQ = MID(sData, 270, 5);
            string T50DLN = MID(sData, 275, 6);
            string T50SMI = MID(sData, 281, 2);
            string T50SPC2 = MID(sData, 283, 4);
            string T50SHP = MID(sData, 287, 4); //CR
            // end
            // start
            string T50CRN = MID(sData, 291, 3);
            string T50CPN = MID(sData, 294, 3);
            string T50PGN = MID(sData, 297, 3);
            string T50FFN = MID(sData, 300, 3);
            string T50ARN = MID(sData, 303, 3);
            string T50WLN = MID(sData, 306, 3);
            string T50SDN = MID(sData, 309, 3);
            string T50FBN = MID(sData, 312, 3);
            string T50EXC = MID(sData, 315, 3);
            string T50PYN = MID(sData, 318, 3);
            string T50PHN = MID(sData, 321, 3);
            string T50ADN = MID(sData, 324, 3);
            string T50MSN = MID(sData, 327, 3);
            string T50RRN = MID(sData, 330, 3);
            string T50AXN = MID(sData, 333, 3);
            string T50LSN = MID(sData, 336, 3);

            FileInfo fi = new FileInfo(Filename);
            RefFilename = fi.Name;
            //end
            SqlConnection connection = new SqlConnection(connstr);
            connection.Open();
            string PnrQuery = "SELECT  Count(*) from t5_booking_header where  RefFileName = '" + RefFilename + "'";
            SqlCommand pnrCommand = new SqlCommand(PnrQuery, connection);
            int count = Convert.ToInt32(pnrCommand.ExecuteScalar());
            Passengercount = T50PGN;
            Paymmentitemcount = T50PYN;
            Phonecount = T50PHN;
            Ticketremarkcount = T50MSN;
            PNR = T50RCL;

            if (count == 0)
            {




                string table = "T5_booking_header";

                string fields = "(LINE, T50BID, T50TRC, T50SPC, T50MIR, T50SZE, T50SEQ, T50DTE, T50TME, " +
                          "T50ISC, T50ISA, T50ISN, T50DFT, T50INP, T50OUT, T50BPC, T50TPC, T50AAN," +
                          "T50RCL, T50ORL, T50OCC, T50OAM, T50AGS, T50SBI, T50SIN, T50DTY, T50PNR," +
                          "T50EHT, T50DTL, T50NMC, T50CUR, T50FAR, T50DML, T50CUR2, T501TX, T501TC," +
                          "T502TX, T502TC, T503TX, T503TC, T504TX, T504TC, T505TX, T505TC, T50COM,T50RTE, " +
                          "T50ITC, T50IN1, T50IN2, T50IN3, T50IN4, T50IN5, T50IN6, T50IN7, T50IN8," +
                          "T50IN9, T50IN10, T50IN11, T50IN12, T50IN13, T50IN14, T50IN15, T50IN16," +
                          "T50IN17, T50PCC, T50ISO, T50DMI, T50DST, T50DPC, T50DSQ, T50DLN, T50SMI," +
                          "T50SPC2, T50SHP, T50CRN, T50CPN, T50PGN, T50FFN, T50ARN, T50WLN, T50SDN," +
                          "T50FBN, T50EXC, T50PYN, T50PHN, T50ADN, T50MSN, T50RRN, T50AXN, T50LSN ,StatusId,ReferenceId,RefFileName)";

                string values = "('" + sData + "','" + T50BID + "','" + T50TRC + "','" + T50SPC + "','" + T50MIR + "','" +
                         T50SZE + "','" + T50SEQ + "','" + T50DTE + "','" + T50TME + "','" +
                         T50ISC + "','" + T50ISA + "','" + T50ISN + "','" + T50DFT + "','" +
                         T50INP + "','" + T50OUT + "','" + T50BPC + "','" + T50TPC + "','" +
                         T50AAN + "','" + T50RCL + "','" + T50ORL + "','" + T50OCC + "','" +
                         T50OAM + "','" + T50AGS + "','" + T50SBI + "','" + T50SIN + "','" +
                         T50DTY + "','" + T50PNR + "','" + T50EHT + "','" + T50DTL + "','" +
                         T50NMC + "','" + T50CUR + "','" + T50FAR + "','" + T50DML + "','" +
                         T50CUR2 + "','" + T501TX + "','" + T501TC + "','" + T502TX + "','" +
                         T502TC + "','" + T503TX + "','" + T503TC + "','" + T504TX + "','" +
                         T504TC + "','" + T505TX + "','" + T505TC + "','" + T50COM + "','" + T50RTE + "','" +
                         T50ITC + "','" + T50IN1 + "','" + T50IN2 + "','" + T50IN3 + "','" +
                         T50IN4 + "','" + T50IN5 + "','" + T50IN6 + "','" + T50IN7 + "','" +
                         T50IN8 + "','" + T50IN9 + "','" + T50IN10 + "','" + T50IN11 + "','" +
                         T50IN12 + "','" + T50IN13 + "','" + T50IN14 + "','" + T50IN15 + "','" +
                         T50IN16 + "','" + T50IN17 + "','" + T50PCC + "','" + T50ISO + "','" +
                         T50DMI + "','" + T50DST + "','" + T50DPC + "','" + T50DSQ + "','" +
                         T50DLN + "','" + T50SMI + "','" + T50SPC2 + "','" + T50SHP + "','" +
                         T50CRN + "','" + T50CPN + "','" + T50PGN + "','" + T50FFN + "','" +
                         T50ARN + "','" + T50WLN + "','" + T50SDN + "','" + T50FBN + "','" +
                         T50EXC + "','" + T50PYN + "','" + T50PHN + "','" + T50ADN + "','" +
                         T50MSN + "','" + T50RRN + "','" + T50AXN + "','" + T50LSN + "',1,5,'" + RefFilename + "')";
                WriteToDatabase(table, fields, values);

                string ParseT5 = getT5ID(T50SEQ, T50DTE, T50TME, T50RCL);

                return ParseT5;
            }
            else
            {
                return "0";
            }
        }

        public string MID(string s, int a, int b)
        {
            string temp = "";
            //   temp =s.Substring(s.Length - 0);

            try
            {
                if (s.Length > (a + b))
                {
                    if (s.Length > 0 && s != null) // && temp !="")
                    {
                        temp = s.Substring(a - 1, b);
                    }
                }
            }
            catch
            {

            }
            return temp;
        }

        public string LEFT(string s, int a)
        {
            string temp = "";
            try
            {

                if (s.Length > 0 && s != null)
                {
                    temp = s.Substring(0, a);
                }
            }
            catch
            {

            }
            return temp;
        }


        //************************************************************************************************************
        //Read Line Unix
        //************************************************************************************************************

        string readLineMIR(StreamReader fileObj)
        {
            string readLineMIR = string.Empty;
            string ln = string.Empty;
            char ch = ' ';
            try
            {

                char[] cArray = new char[1];
                int index = 0;

                do
                {
                    if (!fileObj.EndOfStream)
                    {
                        // index = fileObj.Read(cArray, index, 1);
                        ch = (char)fileObj.Read();
                        ln = ln + ch; //cArray[0];
                    }
                } while (((int)ch) != 13 && fileObj.EndOfStream != true);
            }
            catch
            {
                readLineMIR = "";
                fileError = true;
            }
            readLineMIR = ln;

            return readLineMIR;

        }

        //************************************************************************************************************
        //Write Log file
        //************************************************************************************************************

        //void WriteLog(level, sMesgType, sMesg)
        //    Dim LogFile
        //    Dim fs
        //    Set fs = CreateObject("Scripting.FileSystemObject")
        //    if level >= loglevel {
        //        Set LogFile = fs.OpenTextFile(mirlog , 8)
        //        LogFile.WriteLine(sMesgType + " " + sMesg)
        //        LogFile.Close
        //    }
        //} 
        //************************************************************************************************************
        //Write to Database
        //************************************************************************************************************
        void WriteToDatabase(string sTable, string sFields, string sData)
        {
            //Function to write data
            //sTable : Name of the table
            //sFields : List of fields between brackets i.e (field1,field2)
            //sParams : List of data between brackets i.e ('data1', data2) 
            try
            {
                SqlConnection conn = new SqlConnection(connstr);
                conn.Open();
                string strSQL = "INSERT INTO " + sTable + sFields + " VALUES " + sData;
                SqlCommand cmd = new SqlCommand(strSQL, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                databaseError = true;
            }

        }
        //**********************************************************************************************************

        //Changed by Eric - version 1.01
        //************************************************************************************************************
        //Remove T5 ID
        //************************************************************************************************************
        void removeT5ID(string pT5ID)
        {
            //string connstring = "Provider=SqlProv;Data Source=mydb;User Id=root;Password=santosh";

            SqlConnection conn = new SqlConnection(connstr);
            conn.Open();


            string[] sarray = {"MIRINT_A00", "MIRINT_A01", "MIRINT_A02", "MIRINT_A03", "MIRINT_A04", "MIRINT_A05", 
                          "MIRINT_A06","MIRINT_A07","MIRINT_A08", "MIRINT_A09", "MIRINT_A10", "MIRINT_A11", 
                          "MIRINT_A12", "MIRINT_A13", "MIRINT_A14", "MIRINT_A15", "MIRINT_A16", "MIRINT_A17", 
                          "MIRINT_A18", "MIRINT_A19", "MIRINT_A20", "MIRINT_A21", "MIRINT_A22", "MIRINT_A23", 
                          "MIRINT_A24", "MIRINT_A26"};
            string strSQL = "";
            SqlCommand cmd = new SqlCommand(strSQL, conn);
            foreach (string table in sarray)
            {
                strSQL = "Delete from " + table + " where T5ID =" + pT5ID;
                cmd = new SqlCommand(strSQL, conn);
                cmd.ExecuteNonQuery();
            }
            strSQL = "Delete from MIRINT_T5 where ID =" + pT5ID;
            cmd = new SqlCommand(strSQL, conn);
            cmd.ExecuteNonQuery();

            conn.Close();

        }
        //End change - version 1.01
        //************************************************************************************************************
        //Get T5 ID
        //************************************************************************************************************
        string getT5ID(string pT50SEQ, string pT50DTE, string pT50TME, string pT50RCL)
        {
            //Function to write data
            //sTable : Name of the table
            //sFields : List of fields between brackets i.e (field1,field2)
            //sParams : List of data between brackets i.e ('data1', data2) 
            //string connstring = "Provider=SqlProv;Data Source=mydb;User Id=root;Password=santosh";

            SqlConnection conn = new SqlConnection(connstr);
            conn.Open();


            string strSQL = "SELECT MAX(ID) AS ID FROM T5_booking_header WHERE  " +
                     " T50SEQ = '" + pT50SEQ + "' AND" +
                     " T50DTE = '" + pT50DTE + "' AND" +
                     " T50TME = '" + pT50TME + "' AND" +
                     " T50RCL = '" + pT50RCL + "'";
            //WriteLog All, "INF", " getT5ID strSQL :" + strSQL
            SqlCommand cmd = new SqlCommand(strSQL, conn);
            object va = cmd.ExecuteScalar();
            string getT5ID = va.ToString();
            return getT5ID;
        }



        public void chandra(int T5_ID, string PNR)
        {

        }
    }
}

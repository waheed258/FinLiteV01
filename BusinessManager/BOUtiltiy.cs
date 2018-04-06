using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using DataManager;
using System.Globalization;
using System.Xml.Serialization;
using System.Net.Mail;
using System.IO;


namespace BusinessManager
{
    public class BOUtiltiy
    {
        public static string[] formats = { "MM/dd/yyyy", "M/d/yyyy", "dd/MM/yyyy", "yyyy-MM-dd'T'HH:mm:ss'Z'", "dd-MM-yyyy", "yyyy-MM-dd", "yyyy/MM/dd", "yyyy/MM/d", "yyyy/M/d", "yyyy/M/dd", "M/dd/yyyy", "d/MM/yyyy", "d/M/yyyy", "dd/M/yyyy", "MMMM dd, yyyy" };
        private DOUtility _objDOUtility = new DOUtility();


        public string Encrypts(string toEncrypt, bool useHashing)
        {
            byte[] keyArray;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

            //System.Configuration.AppSettingsReader settingsReader = new AppSettingsReader();
            // Get the key from config file
            string key = "dinoosys@!@#";
            //System.Windows.Forms.MessageBox.Show(key);
            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                hashmd5.Clear();
            }
            else
                keyArray = UTF8Encoding.UTF8.GetBytes(key);

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            tdes.Clear();
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);

            string ResultString = string.Empty;
            try
            {
                byte[] encData_byte = new byte[toEncrypt.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(toEncrypt);
                ResultString = Convert.ToBase64String(encData_byte);
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to encrypt.");
            }
            return ResultString;
        }
        public string Decrypts(string cipherString, bool useHashing)
        {
            //space replace to "+" 
            cipherString = cipherString.Replace(" ", "+");

            byte[] keyArray;
            byte[] toEncryptArray = Convert.FromBase64String(cipherString);

            //System.Configuration.AppSettingsReader settingsReader = new AppSettingsReader();
            //Get your key from config file to open the lock!
            string key = "dinoosys@!@#";

            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                hashmd5.Clear();
            }
            else
                keyArray = UTF8Encoding.UTF8.GetBytes(key);

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            tdes.Clear();
            return UTF8Encoding.UTF8.GetString(resultArray);
            string ResultString = string.Empty;
            try
            {
                System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
                System.Text.Decoder utf8Decode = encoder.GetDecoder();
                byte[] todecode_byte = Convert.FromBase64String(cipherString);
                int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
                char[] decoded_char = new char[charCount];
                utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
                ResultString = new String(decoded_char);
            }
            catch (Exception ex)
            {
                throw new Exception("invalid result.");
            }
            return ResultString;
        }

        public string ShowMessage(string sOperation, string TitleMessage, string sMessage)
        {
            StringBuilder sbMessage = new StringBuilder();
            // operations class 1.success,2.danger,3.info4.warning
            sbMessage.Append(" <div class='alert alert-" + sOperation + " fade in'>");
            sbMessage.Append("<a href='#' class='close' data-dismiss='alert'>&times;</a>   <strong>" + TitleMessage + "!</strong>" + sMessage + "</div>");
            return sbMessage.ToString();

        }
        public string GetSubstringByString(string a, string b, string c)
        {
            if (c.IndexOf(a) > 0 && c.IndexOf(b) > 0)
                return c.Substring((c.IndexOf(a) + a.Length), (c.IndexOf(b) - c.IndexOf(a) - a.Length));
            return c;
        }

        public string ConvertDateFormat(string DateString)
        {
            try
            {
                if (DateString != "")
                {
                    if (IsValidDateTime(DateString))
                    {
                        string[] dateString = DateString.Split(' ');
                        return DateTime.ParseExact(dateString[0], formats, CultureInfo.InvariantCulture, DateTimeStyles.None).ToString("yyyy-MM-dd");
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return ex.Message + " " + DateString;
            }
        }
        public string ReverseConvertDateFormat(string DateString)
        {
            try
            {
                string ReturnDate = string.Empty;
                if (DateString != "")
                {
                    if (IsValidDateTime(DateString))
                    {
                        string[] dateString = DateString.Split(' ');
                        ReturnDate = DateTime.ParseExact(dateString[0], formats, CultureInfo.InvariantCulture, DateTimeStyles.None).ToString("dd-MM-yyyy");

                    }
                }
                return ReturnDate;
            }
            catch (Exception ex)
            {
                return ex.Message + " " + DateString;
            }
        }
        public string ReverseConvertDateFormat(string DateString, string DateFormat)
        {
            try
            {
                string ReturnDate = string.Empty;

                if (DateString != "")
                {
                    if (IsValidDateTime(DateString))
                    {
                        string[] dateString = DateString.Split(' ');

                        ReturnDate = DateTime.ParseExact(dateString[0], formats, CultureInfo.InvariantCulture, DateTimeStyles.None).ToString(DateFormat);
                    }
                }
                return ReturnDate;
            }
            catch (Exception ex)
            {
                return ex.Message + " " + DateString;
            }
        }
        public string ReverseConvertDateFormatByDate(string DateString, string DateFormat)
        {
            try
            {
                string ReturnDate = string.Empty;

                if (DateString != "")
                {
                    if (IsValidDateTime(DateString))
                    {
                        string[] dateString = DateString.Split(' ');
                        string[] DateFomat = dateString[0].Split('-');
                        ReturnDate = DateFomat[2] + "-" + DateFomat[1] + "-" + DateFomat[0];
                        //ReturnDate = DateTime.ParseExact(dateString[0], formats, CultureInfo.InvariantCulture, DateTimeStyles.None).ToString(DateFormat);
                    }
                }
                return ReturnDate;
            }
            catch (Exception ex)
            {
                return ex.Message + " " + DateString;
            }
        }
        public static bool IsValidDateTime(string dateTime)
        {
            string[] dateString = dateTime.Split(' ');


            DateTime parsedDateTime;
            return DateTime.TryParseExact(dateString[0], formats, CultureInfo.InvariantCulture,
                                           DateTimeStyles.None, out parsedDateTime);
        }
        public string CurrencyId()
        {
            return "15";
        }
        public string Currencycode()
        {
            return "R";
        }

        public string LogoUrl(string Logo)
        {
            return "http://salesdemo.dinoosystech.com/pdfimages/" + Logo;
        }
        public string LogoUrl()
        {
            return "http://salesdemo.dinoosystech.com/";
        }
        public decimal sumExclVat;
        public string ExcluvatCau(string ExclusisveAmnt, string vat)
        {

            if (ExclusisveAmnt != "" && vat != "")
            {
                decimal exclusiveDecimal = Convert.ToDecimal(ExclusisveAmnt);
                decimal exclusiveVatPer = Convert.ToDecimal(vat);
                sumExclVat = (exclusiveDecimal * exclusiveVatPer) / 100;


            }
            return sumExclVat.ToString(string.Format("0.{0}", new string('0', 2)));
        }

        public decimal SumExcAndVatAmount;
        public string InclusiveAmount(string ExlusiveAmount, string vatAmount)
        {

            if (ExlusiveAmount != "" && vatAmount != "")
            {
                decimal exclusiveDecimal = Convert.ToDecimal(ExlusiveAmount);
                decimal exclusiveVatPer = Convert.ToDecimal(vatAmount);
                SumExcAndVatAmount = (exclusiveDecimal + exclusiveVatPer);


            }
            return SumExcAndVatAmount.ToString(string.Format("0.{0}", new string('0', 2)));
        }
        decimal ExlusiveAmountDec;
        public string ExcluAssignInclu(string ExclusisveAmnt)
        {

            if (ExclusisveAmnt != "")
            {
                ExlusiveAmountDec = Convert.ToDecimal(ExclusisveAmnt);



            }
            return ExlusiveAmountDec.ToString(string.Format("0.{0}", new string('0', 2)));
        }
        public string FormatTwoDecimal(string value)
        {
            decimal decimalAmount = Convert.ToDecimal(value);
            return decimalAmount.ToString(string.Format("0.{0}", new string('0', 2)));
        }

        #region

        public string FormateNumberWithComma(int value)
        {
            string formatevalue = value.ToString("#,##0");
            return formatevalue;
        }

        #endregion

        #region DataBaseRelatedMethods
        public DataSet GetMenus(string RoleId, int CompanyId)
        {
            return _objDOUtility.GetMenus(RoleId, CompanyId);
        }

        //divya Added
        public DataSet getStates()
        {
            return _objDOUtility.getStates();
        }

        public DataSet getCities()
        {
            return _objDOUtility.getCities();
        }

        public DataSet getStatus()
        {
            return _objDOUtility.getStatus();
        }
        public DataSet get_City_State(int state_id)
        {

            return _objDOUtility.get_City_State(state_id);
        }
        public DataSet getGraphics()
        {
            return _objDOUtility.getGraphics();
        }
        public DataSet getType()
        {
            return _objDOUtility.get_Type();
        }
        public DataSet GetNotepadType()
        {
            return _objDOUtility.GetNotepadType();
        }
        public DataSet GetConsultant(int consultantId)
        {
            return _objDOUtility.GetConsultant(consultantId);
        }

        public DataSet GetBookingSource()
        {
            return _objDOUtility.GetBookingSource();
        }
        public DataSet GetBookingDestination()
        {
            return _objDOUtility.GetBookingDestination();
        }

        public DataSet GetServiceTypeByType(string type)
        {
            return _objDOUtility.GetServiceTypeByType(type);
        }
        public DataSet GetClienttype()
        {
            return _objDOUtility.GetClienttype();
        }

        public DataSet InvoiceDdlBinding(int clientTypeId)
        {
            return _objDOUtility.InvoiceDdlBinding(clientTypeId);
        }

        //public DataSet GetGroup()
        //{
        //    return _objDOUtility.GetGroup();
        //}
        public DataSet GetDivisions()
        {
            return _objDOUtility.GetDivisions();
        }


        //Payment Type



        public DataSet GetPaymentTypes(int paymentId)
        {
            return _objDOUtility.GetPaymentTypes(paymentId);
        }

        public DataSet GetAccountTypeOfSuppl()
        {
            return _objDOUtility.GetAccountTypeOfSuppl();
        }


        public DataSet GetAccNoofClientandSuppl(int AccType,string categoryName)
        {
            return _objDOUtility.GetAccNoofClientandSuppl(AccType, categoryName);
        }

        public DataSet BindAirServiceTypes()
        {
            return _objDOUtility.BindAirServiceTypes();
        }

        public DataSet BindLandServiceTypes()
        {
            return _objDOUtility.BindLandServiceTypes();
        }
        public DataSet GetConsultants()
        {
            return _objDOUtility.GetConsultants();
        }
        //General receipt Accounts
        public DataSet GetGeneralReceiptAccounts()
        {
            return _objDOUtility.GetGeneralReceiptAccounts();
        }
        public DataSet GetBankNames()
        {
            return _objDOUtility.GetBankNames();
        }
        public DataSet GetAccountTypes()
        {
            return _objDOUtility.GetAccountTypes();
        }
        public DataSet GetCommissionTypes()
        {
            return _objDOUtility.GetCommissionTypes();
        }
        //public DataSet GetPaymentMethods()
        //{
        //    return _objDOUtility.GetPaymentMethods();
        //}

        public DataSet getCountries()
        {
            return _objDOUtility.getCountries();
        }


        public DataSet get_State_Country(int Id)
        {

            return _objDOUtility.get_State_Country(Id);
        }

        public DataSet GetSupplierInvoice()
        {
            return _objDOUtility.GetSupplierInvoice();
        }

        public DataSet GetItemLoadingType()
        {
            return _objDOUtility.GetItemLoadingType();
        }


        public DataSet GetDepListMethods()
        {
            return _objDOUtility.GetDepListMethods();
        }

        public DataSet get_City_Country(int Id)
        {

            return _objDOUtility.get_City_Country(Id);
        }

        public DataSet GetCommissionPerc(int commId)
        {

            return _objDOUtility.GetCommissionPerc(commId);
        }


        public DataSet GetTransactionAction()
        {
            return _objDOUtility.GetTransactionAction();
        }

        public DataSet GetGroupMaster(int Id)
        {

            return _objDOUtility.GetGroupMaster(Id);
        }




        public DataSet GetStatement()
        {
            return _objDOUtility.GetStatement();
        }

        public DataSet GetCreditTerms()
        {
            return _objDOUtility.GetCreditTerms();
        }

        public DataSet GetLimitTerms()
        {
            return _objDOUtility.GetLimitTerms();
        }

        public DataSet GetDepartment()
        {
            return _objDOUtility.GetDepartment();
        }
        public DataSet GetCalCulAir()
        {
            return _objDOUtility.GetCalCulAir();
        }

        public DataSet GetCalCulLand()
        {
            return _objDOUtility.GetCalCulLand();
        }


        public DataSet GetImportedTicket()
        {
            return _objDOUtility.GetImportedTicket();
        }

        public DataSet GetDirectSettleTrans()
        {
            return _objDOUtility.GetDirectSettleTrans();
        }
        public DataSet GetClientActions()
        {
            return _objDOUtility.GetClientActions();
        }
        public DataSet GetErrorList()
        {
            return _objDOUtility.GetErrorList();
        }



        public DataSet GetGroup(string Type)
        {
            return _objDOUtility.GetGroup(Type);
        }
        public DataSet GetReceiptTypes(int ReciptId)
        {
            return _objDOUtility.GetReceiptTypes(ReciptId);
        }

        public DataSet GetBankAccounts(int BankId)
        {

            return _objDOUtility.GetBankAccounts(BankId);
        }
        public DataSet getMainAccounts()
        {
            return _objDOUtility.BindMainAccount();
        }
        public DataSet GetMainAccountType(int MainAccId)
        {

            return _objDOUtility.GetMainAccountType(MainAccId);
        }

        public DataSet CheckAccCodeExitorNot(string AccountNo, string FormName)
        {

            return _objDOUtility.CheckAccCodeExitorNot(AccountNo, FormName);
        }

        /// <summary>
        /// Key Exist Or Not 
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="FormName"></param>
        /// <returns></returns>
        public DataSet CheckKeyCodeExitorNot(string Key, string FormName)
        {

            return _objDOUtility.CheckKeyCodeExitorNot(Key, FormName);
        }


        // Get Multiple Languages

        public DataSet GetLanguageDescription(int LangId)
        {

            return _objDOUtility.GetLanguageDescription(LangId);
        }

        public DataSet GetLanguages()
        {

            return _objDOUtility.GetLanguages();
        }

        #endregion DataBaseRelatedMethods
        public bool SendEmail(string SmtpHost, int SmtpPort, string MailFrom, string DisplayNameFrom, string FromPassword, string MailTo, string DisplayNameTo, string MailCc, string DisplayNameCc, string MailBcc, string Subject, string MailText, string Attachment)
        {
            MailMessage myMessage = new MailMessage();
            bool IsSucces = false;
            try
            {
                myMessage.From = new MailAddress(MailFrom, DisplayNameFrom);
                if (MailTo != "")
                    myMessage.To.Add(new MailAddress(MailTo, DisplayNameTo));
                if (MailCc != "")
                {
                    string[] arrayMailCC = MailCc.Split(',');
                    if (arrayMailCC.Length > 0)
                    {
                        foreach (string inMailCC in arrayMailCC)
                        {
                            myMessage.CC.Add(new MailAddress(inMailCC));
                        }

                    }
                    else
                    {
                        myMessage.CC.Add(new MailAddress(MailCc));
                    }

                }

                if (MailBcc != "")
                    myMessage.Bcc.Add(MailBcc);

                myMessage.Subject = Subject;
                myMessage.IsBodyHtml = true;
                myMessage.Body = MailText;
                if (Attachment != "")
                {
                    Attachment a = new Attachment(Attachment);
                    myMessage.Attachments.Add(a);
                }
                SmtpClient mySmtpClient = new SmtpClient(SmtpHost, SmtpPort);
                mySmtpClient.Credentials = new System.Net.NetworkCredential(MailFrom, FromPassword);
                mySmtpClient.EnableSsl = true;
                mySmtpClient.Send(myMessage);
                IsSucces = true;

            }
            catch (Exception ex)
            {
                IsSucces = false;
            }
            finally
            {

                myMessage.Dispose();
            }
            return IsSucces;
        }
        public bool TryParseCheckValue(string Value, string Type)
        {
            try
            {
                switch (Type)
                {
                    case "Decimal":
                        Convert.ToDecimal(Value);
                        break;
                    case "Int":
                        Convert.ToInt32(Value);
                        break;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }


        public string Decrypt(string EncryptedText)
        {
            byte[] inputByteArray = new byte[EncryptedText.Length + 1];
            byte[] rgbIV = { 0x21, 0x43, 0x56, 0x87, 0x10, 0xfd, 0xea, 0x1c };
            byte[] key = { };

            try
            {
                key = System.Text.Encoding.UTF8.GetBytes("McaMCAMA");
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                inputByteArray = Convert.FromBase64String(EncryptedText);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(key, rgbIV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                System.Text.Encoding encoding = System.Text.Encoding.UTF8;
                return encoding.GetString(ms.ToArray());
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }


        public string Encrypt(string stringToEncrypt)
        {
            byte[] inputByteArray = Encoding.UTF8.GetBytes(stringToEncrypt);
            byte[] rgbIV = { 0x21, 0x43, 0x56, 0x87, 0x10, 0xfd, 0xea, 0x1c };
            byte[] key = { };
            try
            {
                key = System.Text.Encoding.UTF8.GetBytes("McaMCAMA");
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(key, rgbIV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                return Convert.ToBase64String(ms.ToArray());
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }




}




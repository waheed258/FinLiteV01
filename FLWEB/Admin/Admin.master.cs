using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessManager;
using System.Data;
using System.Web.Configuration;
using System.Configuration;
public partial class Admin_Admin : System.Web.UI.MasterPage
{
    BOUtiltiy _objBOUtiltiy = new BOUtiltiy();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserLoginId"] == null)
        {
            Response.Redirect("../Login.aspx");
        }

        if (Session["UserLoginId"] != null)
        {
            LabelUserName.Text = Session["UserFullName"].ToString();
            GetMenu();
        }

     //   Response.Cache.SetCacheability(HttpCacheability.NoCache);
     //   if (!this.IsPostBack)
     //   {
     //       Session["UserLoginId"] = true;
     //       Configuration config = WebConfigurationManager.OpenWebConfiguration("~/Web.Config");
     //       SessionStateSection section = (SessionStateSection)config.GetSection("system.web/sessionState");
     //       int timeout = (int)section.Timeout.TotalMinutes * 1000 * 60;
     //      // ClientScript.RegisterStartupScript(this.GetType(), "SessionAlert", "SessionExpireAlert(" + timeout + ");", true);

     //       ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(),
     //"SessionAlert", "SessionExpireAlert(" + timeout + ");", true);

     //   }
    }

    private void GetMenu()
    {

       // int langId = Convert.ToInt32(Session["LanguageId"]);
       // string langname = Session["LanguageName"].ToString();

        int langId = 1;
        string langname = "English";

        DataSet ds = _objBOUtiltiy.GetLanguageDescription(langId);

        if (langname == "Arabic")
        {
            string strMenu = string.Empty;
            string strRole = Session["UserRole"].ToString().Trim();
            int CompanyId = Convert.ToInt32(Session["UserCompanyId"]);
            DataSet objdsmenu = _objBOUtiltiy.GetMenus(strRole, CompanyId);
            if (objdsmenu.Tables[0].Rows.Count > 0)
            {
                strMenu = "<ul class='nav nav-main'>";

                foreach (DataRow mainItem in objdsmenu.Tables[0].Rows)
                {
                    if (mainItem["MenuName"].ToString() == "Dashboard" || mainItem["MenuName"].ToString() == "New Bookings" || mainItem["MenuName"].ToString() == "View Latest File")
                    {
                        //strMenu += "<li class='sub-menu'><a href=" + mainItem["Url"].ToString() + "><i class='fa fa-user'></i>" + mainItem["MenuName"].ToString() + "</a>";

                        strMenu += "<li class='nav-active'> <a href='" + mainItem["Url"].ToString() + "'><i class='" + mainItem["MenuIcon"].ToString() + "' aria-hidden='true'></i><span>" + mainItem["MenuName"].ToString() + "</span></a></li>";
                    }
                    else
                    {
                        strMenu += "<li class='nav-parent'><a><i class='" + mainItem["MenuIcon"].ToString() + "' aria-hidden='true'></i><span>" + mainItem["MenuName"].ToString() + "</span></a>";
                    }
                    DataRow[] lstSubMenu;
                    lstSubMenu = objdsmenu.Tables[1].Select("ParentMenuId='" + mainItem["MenuId"] + "'");



                    if (lstSubMenu.Length > 0)
                    {
                        DataRow[] SecondSubMenu;
                        strMenu += "<ul class='nav nav-children'>";
                        foreach (var subMenuItem in lstSubMenu)
                        {
                            SecondSubMenu = objdsmenu.Tables[2].Select("ParentMenuId='" + subMenuItem["MenuId"] + "'");
                            //  strMenu += "<li><a href=" + subMenuItem["Url"].ToString() + ">" + subMenuItem["MenuName"].ToString() + "</a></li>";

                            if (SecondSubMenu.Length > 0)
                            {
                                DataRow[] ThirdSubMenu;
                                strMenu += "<li class='nav-parent'><a><i class='" + subMenuItem["MenuIcon"].ToString() + "' aria-hidden='true'></i><span>" + subMenuItem["MenuName"].ToString() + "</span></a>";
                                strMenu += "<ul class='nav nav-children'>";
                                foreach (var secondMenuItem in SecondSubMenu)
                                {
                                    ThirdSubMenu = objdsmenu.Tables[3].Select("ParentMenuId='" + secondMenuItem["MenuId"] + "'");

                                    if (ThirdSubMenu.Length > 0)
                                    {
                                        strMenu += "<li class='nav-parent'><a><i class='" + secondMenuItem["MenuIcon"].ToString() + "' aria-hidden='true'></i><span>" + secondMenuItem["MenuName"].ToString() + "</span></a>";
                                        strMenu += "<ul class='nav nav-children'>";
                                        foreach (var ThirdMenuItem in ThirdSubMenu)
                                        {
                                            strMenu += "<li><a href='" + ThirdMenuItem["Url"].ToString() + "'>" + ThirdMenuItem["MenuName"].ToString() + "</a></li>";
                                        }
                                        strMenu += "</ul>";
                                    }
                                    else
                                    {

                                        strMenu += "<li><a href='" + secondMenuItem["Url"].ToString() + "'>" + secondMenuItem["MenuName"].ToString() + "</a></li>";
                                    }

                                    strMenu += "</li>";
                                }
                                strMenu += "</ul>";
                            }
                            else
                            {
                                strMenu += "<li><a href='" + subMenuItem["Url"].ToString() + "'>" + subMenuItem["MenuName"].ToString() + "</a></li>";
                            }
                            strMenu += "</li>";
                        }
                        strMenu += "</ul>";

                    }

                    strMenu += "</li>";
                }

                strMenu += "</ul>";
            }





            menu.InnerHtml = strMenu;





            if (objdsmenu.Tables[4].Rows.Count > 0)
            {
                string topMenu = string.Empty;
                topMenu += "<li class='divider'></li>";
                foreach (DataRow topmenu in objdsmenu.Tables[4].Rows)
                {
                    //  topMenu += "<li><a href=" + topmenu["Url"].ToString() + ">" + topmenu["MenuName"].ToString() + "</a></li>";

                    topMenu += "<li><a role='menuitem' tabindex='-1' href='" + topmenu["Url"].ToString() + "'><i class='fa fa-user'></i>" + topmenu["MenuName"].ToString() + "</a></li>";
                }

                ultopmenu.InnerHtml = topMenu;
            }
        }
        else
        {
            string strMenu = string.Empty;
            string strRole = Session["UserRole"].ToString().Trim();
            int CompanyId = Convert.ToInt32(Session["UserCompanyId"]);
            DataSet objdsmenu = _objBOUtiltiy.GetMenus(strRole, CompanyId);
            if (objdsmenu.Tables[0].Rows.Count > 0)
            {
                strMenu = "<ul class='nav nav-main'>";

                foreach (DataRow mainItem in objdsmenu.Tables[0].Rows)
                {
                    if (mainItem["MenuName"].ToString() == "Dashboard" || mainItem["MenuName"].ToString() == "New Bookings" || mainItem["MenuName"].ToString() == "View Latest File")
                    {
                        //strMenu += "<li class='sub-menu'><a href=" + mainItem["Url"].ToString() + "><i class='fa fa-user'></i>" + mainItem["MenuName"].ToString() + "</a>";

                        strMenu += "<li class='nav-active'> <a href='" + mainItem["Url"].ToString() + "'><i class='" + mainItem["MenuIcon"].ToString() + "' aria-hidden='true'></i><span>" + mainItem["MenuName"].ToString() + "</span></a></li>";
                    }
                    else
                    {
                        strMenu += "<li class='nav-parent'><a><i class='" + mainItem["MenuIcon"].ToString() + "' aria-hidden='true'></i><span>" + mainItem["MenuName"].ToString() + "</span></a>";
                    }
                    DataRow[] lstSubMenu;
                    lstSubMenu = objdsmenu.Tables[1].Select("ParentMenuId='" + mainItem["MenuId"] + "'");



                    if (lstSubMenu.Length > 0)
                    {
                        DataRow[] SecondSubMenu;
                        strMenu += "<ul class='nav nav-children'>";
                        foreach (var subMenuItem in lstSubMenu)
                        {
                            SecondSubMenu = objdsmenu.Tables[2].Select("ParentMenuId='" + subMenuItem["MenuId"] + "'");
                            //  strMenu += "<li><a href=" + subMenuItem["Url"].ToString() + ">" + subMenuItem["MenuName"].ToString() + "</a></li>";

                            if (SecondSubMenu.Length > 0)
                            {
                                DataRow[] ThirdSubMenu;
                                strMenu += "<li class='nav-parent'><a><i class='" + subMenuItem["MenuIcon"].ToString() + "' aria-hidden='true'></i><span>" + subMenuItem["MenuName"].ToString() + "</span></a>";
                                strMenu += "<ul class='nav nav-children'>";
                                foreach (var secondMenuItem in SecondSubMenu)
                                {
                                    ThirdSubMenu = objdsmenu.Tables[3].Select("ParentMenuId='" + secondMenuItem["MenuId"] + "'");

                                    if (ThirdSubMenu.Length > 0)
                                    {
                                        strMenu += "<li class='nav-parent'><a><i class='" + secondMenuItem["MenuIcon"].ToString() + "' aria-hidden='true'></i><span>" + secondMenuItem["MenuName"].ToString() + "</span></a>";
                                        strMenu += "<ul class='nav nav-children'>";
                                        foreach (var ThirdMenuItem in ThirdSubMenu)
                                        {
                                            strMenu += "<li><a href='" + ThirdMenuItem["Url"].ToString() + "'>" + ThirdMenuItem["MenuName"].ToString() + "</a></li>";
                                        }
                                        strMenu += "</ul>";
                                    }
                                    else
                                    {

                                        strMenu += "<li><a href='" + secondMenuItem["Url"].ToString() + "'>" + secondMenuItem["MenuName"].ToString() + "</a></li>";
                                    }

                                    strMenu += "</li>";
                                }
                                strMenu += "</ul>";
                            }
                            else
                            {
                                strMenu += "<li><a href='" + subMenuItem["Url"].ToString() + "'>" + subMenuItem["MenuName"].ToString() + "</a></li>";
                            }
                            strMenu += "</li>";
                        }
                        strMenu += "</ul>";

                    }

                    strMenu += "</li>";
                }

                strMenu += "</ul>";
            }





            menu.InnerHtml = strMenu;





            if (objdsmenu.Tables[4].Rows.Count > 0)
            {
                string topMenu = string.Empty;
                topMenu += "<li class='divider'></li>";
                foreach (DataRow topmenu in objdsmenu.Tables[4].Rows)
                {
                    //  topMenu += "<li><a href=" + topmenu["Url"].ToString() + ">" + topmenu["MenuName"].ToString() + "</a></li>";

                    topMenu += "<li><a role='menuitem' tabindex='-1' href='" + topmenu["Url"].ToString() + "'><i class='fa fa-user'></i>" + topmenu["MenuName"].ToString() + "</a></li>";
                }

                ultopmenu.InnerHtml = topMenu;
            }
        }
    }
}

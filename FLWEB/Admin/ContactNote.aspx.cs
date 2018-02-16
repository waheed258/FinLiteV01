using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using EntityManager;
using BusinessManager;
public partial class Admin_ContactNote : System.Web.UI.Page
{
    EMContactNote objemNote = new EMContactNote();
    BAContactNote objBANote = new BAContactNote();
    BOUtiltiy _BOUtility = new BOUtiltiy();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["NotePadId"]))
            {
                string NoteId = Request.QueryString["NotePadId"].ToString();


                GetContactNote(Convert.ToInt32(NoteId));
                cmdSubmit.Text = "Update";

            }
        }
    }
    protected void cmdSubmit_Click(object sender, EventArgs e)
    {
        InsertUpdateNote();
    }
    private void InsertUpdateNote()
    {
        try
        {
            objemNote.NotePadId = Convert.ToInt32(hf_ContactNoteId.Value);
            objemNote.NoteKey = txtNoteKey.Text;
            objemNote.NpName = txtNoteDescription.Text;
            objemNote.Deactivate = Convert.ToInt32(chkNoteDeactivate.Checked);
            objemNote.HelpText = txtHelpText.Text;
            objemNote.AppToClients = Convert.ToInt32(ChkAppClients.Checked);
            objemNote.AppToPrincipals = Convert.ToInt32(ChkAppPrincipals.Checked);
            objemNote.NpType = "";
            objemNote.CreatedBy = 0;

            int Result = objBANote.InsUpdContactNote(objemNote);
            if (Result > 0)
            {
                lblMsg.Text = _BOUtility.ShowMessage("success", "Success", "Contact Notes Added Successfully");
                ClearControls();
                Response.Redirect("ContactNoteList.aspx");
                
            }
            else
            {
                lblMsg.Text = _BOUtility.ShowMessage("info", "Info", "Contact Notes was not created please try again");
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _BOUtility.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }

    private void GetContactNote(int NotePadId)
    {
        try
        {
            DataSet ds = objBANote.GetContactNote(NotePadId);
            if (ds.Tables[0].Rows.Count > 0)
            {
                hf_ContactNoteId.Value = ds.Tables[0].Rows[0]["NotePadId"].ToString();
                txtNoteKey.Text = ds.Tables[0].Rows[0]["NoteKey"].ToString();
                txtNoteKey.Enabled = false;
                txtNoteDescription.Text = ds.Tables[0].Rows[0]["NpName"].ToString();
                chkNoteDeactivate.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["Deactivate"]);
                txtHelpText.Text = ds.Tables[0].Rows[0]["HelpText"].ToString();
                ChkAppClients.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["AppToClients"]);
                ChkAppPrincipals.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["AppToPrincipals"]);
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = _BOUtility.ShowMessage("danger", "Danger", ex.Message);
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("ContactNoteList.aspx");
    }
    protected void btnreset_Click(object sender, EventArgs e)
    {
        Response.Redirect("ContactNote.aspx");
    }
    void ClearControls()
    {
        hf_ContactNoteId.Value = "0";
        txtNoteKey.Text = "";
        txtNoteDescription.Text = "";
        txtHelpText.Text = "";
        chkNoteDeactivate.Checked = false;
        ChkAppPrincipals.Checked = false;
        ChkAppClients.Checked = false;
    }
}
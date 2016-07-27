using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Web.UI;
using Architecture.Foundation.SmartAPI;
using Architecture.Foundation.SmartAPI.Configuration;
using Architecture.Foundation.DataValidator;

namespace AF.SmartAPI.Sample
{
    public partial class SmartAPIDemo : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["employee"] = null;
                BindEmployeeGrid();
            }
            lblMessage.Text = String.Empty;
        }

        private void BindEmployeeGrid()
        {
            IList<SmartAPIEntity> empList;
            if (Session["employee"] == null)
            {
                //Create input parameter collection
                var keyValuePair = AFSmartAPI.CreateKeyValueTypePair<String, object, ParameterType>();

                //Create and call Smart API
                var api = AFSmartAPI.CreateSmartAPI();
                empList = api.GetData<SmartAPIEntity>(keyValuePair, "GetEmployee");
                Session["employee"] = empList;
            }
            else
                empList = Session["employee"] as List<SmartAPIEntity>;

            Employee.DataSource = empList;
            Employee.DataBind();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            var employeeEntity = new SmartAPIEntity();
            employeeEntity.EmpID = Guid.NewGuid();
            employeeEntity.Name = txtName.Text;
            employeeEntity.EmployeeAddress = txtAddress.Text;
            employeeEntity.Phone = txtPhone.Text;
            employeeEntity.EMail = txtEMail.Text;

            employeeEntity.ValidateOperation = ValidateOperations.Add;
            employeeEntity.Operation = APIOperations.Add;

            try
            {

                //Create and call Smart API
                var smartAPI = AFSmartAPI.CreateSmartAPI();
                var result = smartAPI.InsertUpdateDelete(employeeEntity, "InsertEmployee");

                Session["employee"] = null;
                BindEmployeeGrid();

                if (result.IsValid)
                {
                    lblMessage.ForeColor = Color.Black;
                    txtName.Text = String.Empty;
                    txtAddress.Text = String.Empty;
                    txtPhone.Text = String.Empty;
                    txtEMail.Text = String.Empty;
                    lblMessage.Text = "Record added successfuly";
                }
                else
                {
                    lblMessage.ForeColor = Color.Red;
                    String message = String.Empty;
                    foreach (var msg in result.Message)
                        message += msg + "<br/>";
                    lblMessage.Text = message;
                }

            }
            catch (Exception ex)
            {

            }
        }


        protected void Employee_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Employee.PageIndex = e.NewPageIndex;
            BindEmployeeGrid();
        }

        protected void Employee_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            var employeeID = new Guid(e.Keys[0].ToString());


            //Create input parameter collection
            var keyValuePair = AFSmartAPI.CreateKeyValueTypePair<String, object, ParameterType>();
            keyValuePair.Add("EmpID", employeeID, ParameterType.Guid);

            //Create and call Robot API
            var buisnessLayer = AFSmartAPI.CreateSmartAPI();
            var result = buisnessLayer.InsertUpdateDelete(keyValuePair, "DeleteEmployee");

            Session["employee"] = null;
            BindEmployeeGrid();
        }

        protected void Employee_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            var employeeID = new Guid(e.Keys[0].ToString());
            TextBox Address = (TextBox)Employee.Rows[e.RowIndex].FindControl("txtAddress");
            TextBox EMail = (TextBox)Employee.Rows[e.RowIndex].FindControl("txtEMail");
            TextBox Phone = (TextBox)Employee.Rows[e.RowIndex].FindControl("txtPhone");

            var employee = new SmartAPIEntity { EmpID = employeeID, EmployeeAddress = Address.Text, EMail = EMail.Text, Phone = Phone.Text, ValidateOperation = ValidateOperations.Update, Operation = APIOperations.Update };

            //Create and call Generic Service
            var buisnessLayer = AFSmartAPI.CreateSmartAPI();
            var result = buisnessLayer.InsertUpdateDelete(employee, "UpdateEmployee");

            Employee.EditIndex = -1;
            Session["employee"] = null;
            BindEmployeeGrid();
        }

        protected void Employee_RowEditing(object sender, GridViewEditEventArgs e)
        {
            Employee.EditIndex = e.NewEditIndex;
            Session["employee"] = null;
            BindEmployeeGrid();
        }

        protected void Employee_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            Employee.EditIndex = -1;
            Session["employee"] = null;
            BindEmployeeGrid();
        }
    }
}
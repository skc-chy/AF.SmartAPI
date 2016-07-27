using Architecture.Foundation.Core.Interface.Validation;
using Architecture.Foundation.DataValidator;
using Architecture.Foundation.SmartAPI.Configuration;
using Architecture.Foundation.SmartAPI.EntityCore;
using System;
using System.Collections.Generic;

namespace AF.SmartAPI.Sample
{
    /// <summary>
    /// Implement interface 'IAFValidation ' similar to Validation demo
    /// Implement interface 'IAFWriteOperations' for Generic Servic. 
    /// The 'IAFWriteOperations' would add property 'Operation'
    /// Set the property 'Operation' before sending it to Generic Service. This can be Add/Update/Delete
    /// </summary>
    public sealed class SmartAPIEntity :  IAFWriteOperations, IAFValidation
    {
        //Map property with SP parameter for Insert/Update/Delete operations.
        //Pass array of APIOperation where this property would be used
        //if property name and SP parameter name is identical then do not need mapping
        [AFWriteOperations(new[] { APIOperations.Add, APIOperations.Update }, ParameterType.Guid)]
        //Map property with SP parameter for Read operations.
        [AFReadOperation("EmpID")]
        //Add validator similar as Validation demo
       [AFGuidValidator("Employee id could not be blank", new[] { ValidateOperations.Add, ValidateOperations.Update })]
        public Guid EmpID { get; set; }

        //Map property with SP parameter for Insert/Update/Delete operations.
        //Pass array of APIOperation where this property would be used
        //if property name and SP parameter name is identical then do not need mapping
        [AFWriteOperations(new[] { APIOperations.Add }, ParameterType.NVarChar)]
        //Map property with SP parameter for Read operations.
        [AFReadOperation("Name")]
        [AFRequiredFieldValidator("Name could not be blank", new[] { ValidateOperations.Add })]
        public string Name { get; set; }

        //Map property with SP parameter for Insert/Update/Delete operations.
        //Pass array of APIOperation where this property would be used
        //if property name and SP parameter name is different then pass the parameter name
        [AFWriteOperations("Address", new[] { APIOperations.Add, APIOperations.Update }, ParameterType.NVarChar)]
        //Map property with SP parameter for Read operations.
        [AFReadOperation("Address")]
        [AFRequiredFieldValidator("Address could not be blank", new[] { ValidateOperations.Add, ValidateOperations.Update })]
        public string EmployeeAddress { get; set; }

        //Map property with SP parameter for Insert/Update/Delete operations.
        //Pass array of APIOperation where this property would be used
        //if property name and SP parameter name is identical then do not need mapping
        [AFWriteOperations(new[] { APIOperations.Add, APIOperations.Update }, ParameterType.NVarChar)]
        //Map property with SP parameter for Read operations.
        [AFReadOperation("Email")]
        [AFRequiredFieldValidator("Email could not be blank", new[] { ValidateOperations.Add, ValidateOperations.Update })]
        public string EMail { get; set; }

        //Map property with SP parameter for Insert/Update/Delete operations.
        //Pass array of APIOperation where this property would be used
        //if property name and SP parameter name is identical then do not need mapping
        [AFWriteOperations(new[] { APIOperations.Add, APIOperations.Update }, ParameterType.NVarChar)]
        //Map property with SP parameter for Read operations.
        [AFReadOperation("Phone")]
        [AFRequiredFieldValidator("Phone no can't be blank", new[] { ValidateOperations.Add, ValidateOperations.Update })]
        public string Phone { get; set; }


        public IList<string> GetAuditParentKey()
        {
            return new List<string>();
        }

        public IList<string> GetAuditPrimaryKey()
        {
            return new List<string> { "EmpID" };
        }

        public APIOperations Operation { get; set; }

        public ValidateOperations ValidateOperation { get; set; }
        
    }
}
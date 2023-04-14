using AF.SmartAPI.Sample;
using Architecture.Foundation.DataValidator;
using Architecture.Foundation.SmartAPI;
using Architecture.Foundation.SmartAPI.Configuration;

namespace AF.DataAccessor.Sample
{
    public sealed class OperationManager
    {
        public void AddEmployee()
        {
            Console.Clear();
            var employeeEntity = new SmartAPIEntity();
            employeeEntity.EmpID = Guid.NewGuid();

            Console.WriteLine("Enter Name:");
            employeeEntity.Name = Console.ReadLine();

            Console.WriteLine("Enter Address:");
            employeeEntity.EmployeeAddress = Console.ReadLine();

            Console.WriteLine("Enter EMail:");
            employeeEntity.EMail = Console.ReadLine();

            Console.WriteLine("Enter Phone:");
            employeeEntity.Phone = Console.ReadLine();

            employeeEntity.ValidateOperation = ValidateOperations.Add;
            employeeEntity.Operation = APIOperations.Add;

            try
            {
                var smartAPI = AFSmartAPI.CreateSmartAPI();
                var result = smartAPI.InsertUpdateDelete(employeeEntity, "InsertEmployee");

                if (result.IsValid)
                {
                    Console.WriteLine(result.Message[0]);
                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey();
                }
            }
            catch
            {
            }

            
        }

        public void UpdateEmployee()
        {
            Console.Clear();
            var employeeEntity = new SmartAPIEntity();

            Console.WriteLine("Enter employee ID:");
            var empID = Console.ReadLine();
            employeeEntity.EmpID = empID == null ? Guid.Empty : Guid.Parse(empID);

            Console.WriteLine("Enter Address:");
            employeeEntity.EmployeeAddress = Console.ReadLine();

            Console.WriteLine("Enter EMail:");
            employeeEntity.EMail = Console.ReadLine();

            Console.WriteLine("Enter Phone:");
            employeeEntity.Phone = Console.ReadLine();

            employeeEntity.ValidateOperation = ValidateOperations.Update;
            employeeEntity.Operation = APIOperations.Update;

            //Create and call Generic Service
            var buisnessLayer = AFSmartAPI.CreateSmartAPI();
            var result = buisnessLayer.InsertUpdateDelete(employeeEntity, "UpdateEmployee");

            if (result.IsValid)
            {
                Console.WriteLine(result.Message[0]);
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
            }
        }

        public void DeleteEmployee()
        {

            Console.Clear();
            Console.WriteLine("Enter employee ID:");
            var empID = Console.ReadLine();

            //Create input parameter collection
            var keyValuePair = AFSmartAPI.CreateKeyValueTypePair<String, object, ParameterType>();
            keyValuePair.Add("EmpID", empID, ParameterType.Guid);

            //Create and call Smart API
            var buisnessLayer = AFSmartAPI.CreateSmartAPI();
            var result = buisnessLayer.InsertUpdateDelete(keyValuePair, "DeleteEmployee");

            if (result.IsValid)
            {
                Console.WriteLine(result.Message[0]);
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
            }
        }

        public void ListEmployee()
        {
            Console.Clear();
            IList<SmartAPIEntity> empList;

            //Create input parameter collection
            var keyValuePair = AFSmartAPI.CreateKeyValueTypePair<String, object, ParameterType>();

            //Create and call Smart API
            var api = AFSmartAPI.CreateSmartAPI();
            empList = api.GetData<SmartAPIEntity>(keyValuePair, "GetEmployee");

            if (empList.Count == 0)
                Console.WriteLine("No records found");

            foreach (var emp in empList)
            {
                Console.WriteLine("Employee ID: " + emp.EmpID);
                Console.WriteLine("Employee Name: " + emp.Name);
                Console.WriteLine("Employee Address:" + emp.EmployeeAddress);
                Console.WriteLine("Employee Email: " + emp.EMail);
                Console.WriteLine("Employee Phone: " + emp.Phone);

                Console.WriteLine();
                Console.WriteLine();
            }

            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }
    }
}

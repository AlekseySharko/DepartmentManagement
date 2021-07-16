namespace DepartmentManagementModels.OperationResult
{
    public class OperationResult
    {
        public bool Success { get; protected set; }
        public string Message { get; protected set; }

        protected OperationResult () {}

        public static OperationResult Successful()
        {
            return new OperationResult
            {
                Success = true
            };
        }

        public static OperationResult Failed(string errorMessage)
        {
            return new OperationResult
            {
                Success = false,
                Message = errorMessage
            };
        }
    }
}

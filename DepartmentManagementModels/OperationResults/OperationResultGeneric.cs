namespace DepartmentManagementModels.OperationResults.Generic
{
    public class OperationResult<T> : OperationResults.OperationResult
    {
        public T Result { get; private set; }

        protected OperationResult() {}

        public static OperationResult<T> Successful(T result)
        {
            return new OperationResult<T>
            {
                Success = true,
                Result = result
            };
        }

        public new static OperationResult<T> Failed(string errorMessage)
        {
            return new OperationResult<T>
            {
                Success = false,
                Message = errorMessage
            };
        }
    }
}

namespace UserRoles.Models
{
    /*
     * The ErrorViewModel class is a simple data transfer object (DTO) used to capture error information in the application. 
     * It contains a single property for the RequestId, which can be used to track and identify specific requests that resulted in errors.
     * 
     * The ShowRequestId property is a computed property that returns a boolean value indicating whether the RequestId is not null or empty. 
     * This can be useful for displaying error information conditionally in the user interface.
     */
    public class ErrorViewModel
    {
        /*
         * The RequestId property represents the unique identifier for a specific request that resulted in an error. 
         * It is nullable, allowing for cases where a RequestId may not be available.
         */
        public string? RequestId { get; set; }

        /*
         * The ShowRequestId property is a computed property that returns a boolean value indicating whether the RequestId is not null or empty. 
         * This can be useful for displaying error information conditionally in the user interface.
         */
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}

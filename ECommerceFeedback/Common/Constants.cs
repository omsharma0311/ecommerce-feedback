namespace ECommerceFeedback.Common
{
    public class Constants
    {
     
        public const string Price = "price";
        public const string PriceHighToLow = "High to Low";
        public const string PriceLowTohigh = "Low to High";
        public const string Category = "category";
        public const string PriceOrderBy = "priceOrderBy";

        public const string ApiPrefix = "api/";
        public const string ApiVersion = "1.0";
        public const string OrderConfirmed = "OrderConfirmed";
        public const string ProductsAddedSuccessfully = "Products added successfully in cart.";
        public const string ProductsNotAdded = "Error while adding products to cart.";
        public const string ProductsPurchased = "Successfully purchased products";
        public const string ProductsNotPurchased = "Error while purchasing products";
        public const string DeliverySatusUpdated = "Updated";
        public const string DeliverySatusNotUpdated = "Error while updating products delivery status.";
        public const string INTERNAL_API_ERROR_CODE = "450";
        public const string UNAUTHORISED_ERROR_CODE = "401";
        public const string BAD_REQUEST_CODE = "400";
        public const string FORBIDDEN_ERROR_CODE = "403";
        public const string Custom_ERROR_CODE = "430";
        public const string Validation_Failed_ERROR_CODE = "1001";
        public const string DocumentUpload_Status_Code = "201";
        public const string DocumentDeleted_Status_Code = "200";
        public const string DataNotFoundStatusCode = "1001";
        public const string ScriptRegex = @"<script\b[^>]*>([\s\S]*?)<\/script>";
        public const string InvalidRequestParameter = "Invalid Request Parameter";
        public const string NOT_FOUND_STATUS_MESSSAGE = "No Data Found";
        public const string INTERNAL_API_ERROR_CODE_MESSAGE = "Internal Api Failure";
        public const string INTEGRATION_API_ERROR_MESSSAGE = "Integration Api Failure";
        public const string CommaSeparator = ",";
        public const string DateFormat = "dd-MMM-yyyy";
        public const string RequestDateFormat = "yyyy-MM-dd";
        public const string DateFormatHHmm = "yyyy-MM-dd HH:mm";
        public const string DateFormatHHMMSS = "yyyy-MM-dd HH:mm:ss";
        public const int InternalApiFailureStatusCode = 500;
        public const int InternalApiFailureStatusCode450 = 450;
        public const int IntegrationApiFailureStatusCode = 420;
        public const int InternalApiCustomErrorStatusCode = 425;
        public const int BadRequestApiFailureStatusCode = 400;
        public const int UnauthorizedAccessCode = 401;
        public const int ForbiddenAccessCode = 403;
        public const int XoltErrorCode = 25002;
        public const int GatewayTimeoutCode = 408;
        public const int ConflictStatus = 409;
        public const int NotFoundStatusCode = 404;
        public const int DatabaseExceptionStatusCode = 1003;
        public const int DatabaseExceptionHttpStatusCode = 451;  
    }
}

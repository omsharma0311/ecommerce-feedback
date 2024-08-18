
using ECommerceFeedback.Models.Domain.Request;

namespace ECommerceFeedback.Common
{
    public class Validation
    {

        public bool ValidateAddProductRequest(string name, long price, string category, DateTime expiryDate, ref string message)
        {
            Helper.NullOrEmptyCheck(ref message, name, nameof(name));
            Helper.NullOrEmptyCheck(ref message, price, nameof(price));
            Helper.NullOrEmptyCheck(ref message, category, nameof(category));
            Helper.ValidateDateFormat(ref message, expiryDate);

            return (message.Length > 0);
        }

        public bool ValidateProductsListingRequest(string category, string orderBy, ref string message)
        {
            Helper.NullOrEmptyCheck(ref message, category, nameof(category));
            Helper.NullOrEmptyCheck(ref message, orderBy, nameof(orderBy));
            ValidateOrderBy(ref message, orderBy, nameof(orderBy));

            return (message.Length > 0);
        }

        public bool ValidateOrderBy(ref string message, string orderBy, string nameOfField)
        {
            //if (orderBy.ToLower() == "low to high" || orderBy.ToLower() == "high to low")
            //{
            //    return false;
            //}
            //message += " " + nameOfField + "Field is required.";

            return true;
        }
        public bool ValidateAddToBagRequest(List<ProductInBag> productInBags, ref string message)
        {
            foreach (var i in productInBags)
            {
                Helper.NullOrEmptyCheck(ref message, i.Quantity, nameof(i.Quantity));
            }

            return (message.Length > 0);
        }


    }
}

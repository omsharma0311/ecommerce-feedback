using System.Globalization;
using System.Text;

namespace ECommerceFeedback.Common
{
    public static class Helper
    {
  
        public static bool EqualsIgnoreCase(this string objA, string objB)
        {
            return objA.Equals(objB, StringComparison.OrdinalIgnoreCase);
        }

        

        public static bool HasAny<T>(this IEnumerable<T> collection,
            Func<T, bool> predicate = null)
        {
            if (collection == null)
                return false;
            if (predicate != null)
            {
                return collection.Any(predicate);
            }
            return collection.Any();
        }


      

        public static bool NullOrWhiteSpaceCheck(ref string message, string fieldName, string nameOfField)
        {
            var isEmpty = string.IsNullOrWhiteSpace(fieldName);
            if (isEmpty) message += " " + nameOfField + "Field is required.";
            return isEmpty;
        }



        public static bool NullOrEmptyCheck(ref string message, string fieldName, string nameOfField)
        {
            var isEmpty = string.IsNullOrWhiteSpace(fieldName);
            if (isEmpty) message += " " + nameOfField + "Field is required.";
            return isEmpty;
        }

    
        public static bool NullOrEmptyCheck(ref string message, long fieldName, string nameOfField)
        {
            var isEmpty = fieldName == 0;
            if (isEmpty) message += " " + nameOfField + "Field is required.";
            return isEmpty;
        }


        public static bool ValidateDateFormat(ref string message, DateTime date)
        {
            if (date.ToString() == null) return false;
            Dictionary<string, string> queryParams = new()
            {
                ["Date"] = date.ToString()
            };
            StringBuilder messageBuilder = new StringBuilder(message);
            ValidateDates(messageBuilder, queryParams);

            message = messageBuilder.ToString();
            return (message.Length > 0);
        }

       
 
        public static bool ValidateDateFormat(ref string message, string startDate, string endDate)
        {
            if (startDate == null || endDate == null) return false;
            Dictionary<string, string> queryParams = new()
            {
                ["StartDate"] = startDate,
                ["EndDate"] = endDate,
            };
            StringBuilder messageBuilder = new(message);
            ValidateDates(messageBuilder, queryParams);

            if (DateTime.TryParse(startDate, out var startDates) && DateTime.TryParse(endDate, out var endDates) && startDates > endDates)
            {
                messageBuilder.Append($"StartDate cannot be greater than EndDate.");
            }

            message = messageBuilder.ToString();
            return (message.Length > 0);
        }

     
        private static void ValidateDates(StringBuilder builder, Dictionary<string, string> queryParams)
        {
            foreach (var date in queryParams)
            {
                //if (!DateTime.TryParseExact(date.Value, Constants.DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
                //{
                //    builder.Append($"Dates are not in Correct Format.");
                //}
            }
        }
    }
}

using System;
using System.ComponentModel.DataAnnotations;

namespace Hospital_Management_System.Common
{
    public class DateRangeAttribute: RangeAttribute
    {
        public DateRangeAttribute(string minValue) : base( typeof(DateTime), minValue, DateTime.Now.ToShortDateString() )
        {
            //MinimumDate = minValue;
        }

        public DateRangeAttribute(string minValue, string maxValue) : base(typeof(DateTime), minValue, maxValue)
        {
            //MinimumDate = minValue;
        }

    
        public override bool IsValid(object value)
        {
            DateTime date;
            if ((value != null && DateTime.TryParse(value.ToString(), out date)))
            {
                return date.AddYears(18) < DateTime.Now;
            }

            return false;
        }

        //public override string FormatErrorMessage(string name)
        //{
        //    return string.Format(ErrorMessageString, name, 18);
        //}

        public string MinimumDate { get; }
    }
}
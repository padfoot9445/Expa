using System.Globalization;
using Errors;
using Helpers;
using Interfaces;

namespace BackgroundObjects
{
    /*#region Values*/
    public class BackgroundTime : IBackgroundValue
    {
        //parse only mm/yy or yy.q
        public readonly int year;
        public readonly int quarter;
        public readonly int month;
        public BackgroundTime(int year, int quarter)
        {
            this.year = year;
            this.quarter = quarter;
            month = QuarterToMonth(quarter);
        }
        private static int MonthToQuarter(int month) => (int)((month + 2) / 3);
        private static int QuarterToMonth(int quarter) => (int)(quarter * 3 - 2);
        public static BackgroundTime MonthTime(int year, int month) => new(year, MonthToQuarter(month));
        public static BackgroundTime operator +(BackgroundTime self, BackgroundTime other)
        {
            (int newYear, int newQuarter) = WrapTime(self.year + other.year, self.quarter + other.quarter);
            return new BackgroundTime(newYear, newQuarter);
        }
        public static BackgroundTime operator -(BackgroundTime self, BackgroundTime other)
        {
            (int newYear, int newQuarter) = WrapTime(self.year - other.year, self.quarter - other.quarter);
            return new BackgroundTime(newYear, newQuarter);
        }
        public static BackgroundTime operator *(BackgroundTime self, int other)
        {
            (int year, int quarter) = WrapTime((int)Math.Round((double)self.year * other), (int)Math.Round((double)self.quarter * other));
            return new BackgroundTime(year, quarter);
        }
        public static BackgroundTime operator /(BackgroundTime self, int other) => self * (1 / other);
        public string ToString(bool MonthTime) => MonthTime ? $"{year}/{month}" : $"{year}.{quarter}";
        public override string ToString() => ToString(false);
        public static bool operator ==(BackgroundTime self, object? other) => other is BackgroundTime value && value.year == self.year && value.quarter == self.quarter;
        public static bool operator !=(BackgroundTime self, object? other) => !(self == other);
        public override bool Equals(object? other) => this == other;
        public override int GetHashCode() => (year.GetHashCode() + quarter.GetHashCode()) / 2;
        public bool ToBool() => (year + quarter) == 0;
        ///<summary>
        ///returns a new Time object with the given Quarter Notation AC time string input
        ///</summary>
        ///<param name="input"> the string containing Quarter Notation AC time</param>
        public static BackgroundTime ParseAcTime(string input)
        {
            string[] parts = input.Split('.');
            if (parts.Length > 2 || parts.Length < 1)
            {
                throw new MainException($"{input} was an invalid AC time format");
            }
            else if (parts.Length == 1)
            {
                try
                {
                    return new(int.Parse(parts[0]), 0);
                }
                catch (FormatException)
                {
                    throw new MainException($"{input} was an invalid AC time format - could not split into year and month || invalid year");
                }
            }
            else
            {
                try
                {
                    return new(int.Parse(parts[0]), int.Parse(parts[1]));
                }
                catch (FormatException)
                {
                    throw new MainException($"{input} was an invalid AC time format - either month or year could not be converted into an int");
                }
            }
        }
        public static BackgroundTime ParseMonthTime(string input)
        {
            string[] parts = input.Split('/');
            if (parts.Length > 2 || parts.Length < 1)
            {
                throw new MainException($"{input} was an invalid Month AC time format");
            }
            else if (parts.Length == 1)
            {
                try
                {
                    return new(int.Parse(parts[0]), 0);
                }
                catch (FormatException)
                {
                    throw new MainException($"{input} was an invalid Month AC time format - could not split into year and month || invalid year");
                }
            }
            else
            {
                try
                {
                    return BackgroundTime.MonthTime(int.Parse(parts[0]), int.Parse(parts[1]));
                }
                catch (FormatException)
                {
                    throw new MainException($"{input} was an invalid Month AC time format - either month or year could not be converted into an int");
                }
            }
        }
        private static (int, int) WrapTime(int year, int quarter)
        {
            while (quarter > 4)
            {
                quarter -= 4;
                year++;
            }
            while (quarter < 0)
            {
                quarter += 4;
                year--;
            }
            return (year, quarter);
        }
    }
    public class BackgroundNumber : IBackgroundValue
    {
        public int IntegerValue { get; private set; }
        public int DecimalValue { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IntegerValue">Integer portion of the number. Caller has to validate and make sure it fits within an int.</param>
        /// <param name="DecimalValue">Decimal portion of the number. Caller must also validate and make sure it fits into an int.</param>
        public BackgroundNumber(int IntegerValue, int DecimalValue = 0)
        {
            this.IntegerValue = IntegerValue;
            this.DecimalValue = DecimalValue >= 0 ? DecimalValue : throw new ExpaInterpreterError(-1, "Decimal value cannot be negative.");
        }
        public bool ToBool() => (IntegerValue + DecimalValue) != 0;//if they both add to zero, they are both zero. This means they equal zero and we return false as !=. 
        public override bool Equals(object? other) => this == other;
        public int ToInt(bool truncate = false) => truncate ? IntegerValue : (IntegerValue + ((DecimalValue.ToString()[0] >= 5) ? 1 : 0));
        public double ToDouble() => double.Parse($"{IntegerValue}.{DecimalValue}", CultureInfo.InvariantCulture);
        public static BackgroundNumber FromValue(int input) => new(input);
        public static BackgroundNumber FromValue(double input) => new((int)input, int.Parse(input.ToString().Split('.')[1].Truncate(9)));//first parameter is just an int cast - truncating the input. The second param is the part after the decimal point of the float converted to an integer. We truncate to nine decimal places of precision to avoid issues with recurring numbers.
        public override string ToString() => DecimalValue == 0 ? IntegerValue.ToString() : $"{IntegerValue}.{DecimalValue}";
        public static explicit operator int(BackgroundNumber self) => self.ToInt(true);
        public static explicit operator double(BackgroundNumber self) => self.ToDouble();
        public static explicit operator float(BackgroundNumber self) => (float)self.ToDouble();
        //overloads for casting to bool and BackgroundBool
        //// public static explicit operator BackgroundBool(BackgroundNumber self) => new(self.ToBool());
        //// public static explicit operator bool(BackgroundNumber self) => self.ToBool();
        public static BackgroundNumber operator +(BackgroundNumber self, BackgroundNumber other) => self + other.ToDouble();
        public static BackgroundNumber operator +(BackgroundNumber self, double other) => BackgroundNumber.FromValue(self.ToDouble() + other);
        public static BackgroundNumber operator *(BackgroundNumber self, BackgroundNumber other) => self * other.ToDouble();
        public static BackgroundNumber operator *(BackgroundNumber self, double other) => BackgroundNumber.FromValue(self.ToDouble() * other);
        public static BackgroundNumber operator /(BackgroundNumber self, BackgroundNumber other) => self / other.ToDouble();
        public static BackgroundNumber operator /(BackgroundNumber self, double other) => BackgroundNumber.FromValue(self.ToDouble() / other);
        public static BackgroundNumber operator -(BackgroundNumber self, BackgroundNumber other) => self - other.ToDouble();
        public static BackgroundNumber operator -(BackgroundNumber self, double other) => BackgroundNumber.FromValue(self.ToDouble() - other);
        public static BackgroundNumber operator +(BackgroundNumber self, object? other) => other is double value ? self + value : throw new ExpaInterpreterError(-1, $"Invalid operation '+' between type BackgroundNumber and unknown value.");
        public static BackgroundNumber operator -(BackgroundNumber self, object? other) => other is double value ? self - value : throw new ExpaInterpreterError(-1, $"Invalid operation '-' between type BackgroundNumber and unknown value.");
        public static BackgroundNumber operator *(BackgroundNumber self, object? other) => other is double value ? self * value : throw new ExpaInterpreterError(-1, $"Invalid operation '*' between type BackgroundNumber and unknown value.");
        public static BackgroundNumber operator /(BackgroundNumber self, object? other) => other is double value ? self / value : throw new ExpaInterpreterError(-1, $"Invalid operation '/' between type BackgroundNumber and unknown value.");
        public static bool operator ==(BackgroundNumber self, object? other) => other is BackgroundNumber value && self.IntegerValue == value.IntegerValue && self.DecimalValue == value.DecimalValue;
        public static bool operator !=(BackgroundNumber self, object? other) => !(self == other);
        public static bool operator !(BackgroundNumber self) => !self.ToBool();
        public static explicit operator string(BackgroundNumber self) => self.ToString();
        public static explicit operator BackgroundString(BackgroundNumber self) => new(self.ToString());
        public override int GetHashCode() => ((double)this).GetHashCode();
    }
    public class BackgroundBool : IBackgroundValue
    {
        public bool Value { get; private set; }
        public BackgroundBool(bool input) => Value = input;
        public bool ToBool() => Value;
        public static BackgroundBool FromValue(bool input) => new(input);
        public override string ToString() => Value.ToString();
        public static explicit operator bool(BackgroundBool input) => input.Value;
        public static bool operator !(BackgroundBool input) => !input.Value;
        public static explicit operator string(BackgroundBool input) => input.ToString();
        public static explicit operator BackgroundString(BackgroundBool input) => new(input.ToString());
        public static bool operator ==(BackgroundBool self, object? other) => other is BackgroundBool value && value.Value == self.Value;
        public static bool operator !=(BackgroundBool self, object? other) => !(self == other);
        public override bool Equals(object? other) => this == other;
        public override int GetHashCode() => Value.GetHashCode();
    }
    public class BackgroundString : IBackgroundValue
    {
        public string Value { get; private set; }
        public BackgroundString(string input) => Value = input;
        public bool ToBool() => Value.Length != 0;
        public override string ToString() => Value;
        public static bool operator !(BackgroundString input) => !input.ToBool();
        public static BackgroundString operator +(BackgroundString input, string other) => new(input.Value + other);
        public static BackgroundString operator +(BackgroundString input, object? other) => other is string value ? input + value : throw new ExpaInterpreterError(-1, $"Invalid operation '+' between type BackgroundString and unknown value.");
        public static bool operator ==(BackgroundString self, object? other) => other is BackgroundString value && value.Value == self.Value;
        public static bool operator !=(BackgroundString self, object? other) => !(self == other);
        public static explicit operator string(BackgroundString input) => input.ToString();
        public override bool Equals(object? other) => this == other;
        public override int GetHashCode() => Value.GetHashCode() + typeof(BackgroundString).GetHashCode();
    }
}
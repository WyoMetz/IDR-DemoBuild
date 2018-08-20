using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace IDR_Demo_build.ValidationRules
{
	class DataValidation
	{
	}

	/// <summary>
	/// Provides Validation of Diary Number Format
	/// </summary>
	public class DiaryRangeRule : ValidationRule
	{
		private int _min;
		private int _max;

		public DiaryRangeRule()
		{
		}

		public int Min
		{
			get { return _max; }
			set { _min = value; }
		}

		public int Max
		{
			get { return _max; }
			set { _max = value; }
		}

		public override ValidationResult Validate(object value, CultureInfo cultureInfo)
		{
			int DiaryNumber = 00001;

			try
			{
				if (((string)value).Length > 0)
					DiaryNumber = Int32.Parse((String)value);
			}
			catch (Exception e)
			{
				return new ValidationResult(false, "Diary number is not in the correct Format " + e.Message);
			}

			if ((DiaryNumber < Min) || (DiaryNumber > Max))
			{
				return new ValidationResult(false, "Please enter the Diary Number in the format of: " + Min);
			}
			else
			{
				return ValidationResult.ValidResult;
			}
		}
	}
}

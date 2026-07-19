using System.ComponentModel;
using System.Globalization;

namespace vHolidays.Utility.Enumerations
{
	public static class Enumerations
	{
		public static T GetValueFromDescription<T>(string description) where T : Enum
		{
			foreach (var field in typeof(T).GetFields())
			{
				if (Attribute.GetCustomAttribute(field,
				typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
				{
					if (attribute.Description == description)
						return (T)field.GetValue(null);
				}
				else
				{
					if (field.Name == description)
						return (T)field.GetValue(null);
				}
			}

			throw new ArgumentException("Not found.", nameof(description));
			// Or return default(T);
		}

		public static string GetDescription<T>(this T e) where T : IConvertible
		{
			if (e is Enum)
			{
				Type type = e.GetType();
				Array values = System.Enum.GetValues(type);

				foreach (int val in values)
				{
					if (val == e.ToInt32(CultureInfo.InvariantCulture))
					{
						var memInfo = type.GetMember(type.GetEnumName(val));
						var descriptionAttribute = memInfo[0]
							.GetCustomAttributes(typeof(DescriptionAttribute), false)
							.FirstOrDefault() as DescriptionAttribute;

						if (descriptionAttribute != null)
						{
							return descriptionAttribute.Description;
						}
					}
				}
			}

			return null; // could also return string.Empty
		}
	}

	public enum Status
	{
		Active = 1,
		Inactive,
		Blocked,
		Unauthorized,
		Deleted
	}

}

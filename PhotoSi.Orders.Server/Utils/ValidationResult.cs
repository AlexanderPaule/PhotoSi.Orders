using System;
using System.Collections.Generic;
using System.Linq;

namespace PhotoSi.Sales.Utils
{
	public class ValidationResult
	{
		private readonly List<string> _errors;

		private ValidationResult(List<string> errors)
		{
			_errors = errors;
		}

		public bool IsValid => !_errors.Any();

		public string GetErrorMessage()
		{
			return IsValid
				? throw new Exception("There is no error")
				: string.Join(", ", _errors);
		}

		public void AddErrorMessage<T>(string message)
		{
			_errors.Add($"[Type:{typeof(T).Name}] {message}");
		}

		public static ValidationResult New()
		{
			return new ValidationResult(new List<string>());
		}
	}
}
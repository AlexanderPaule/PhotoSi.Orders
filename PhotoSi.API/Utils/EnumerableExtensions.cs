﻿namespace PhotoSi.API.Utils;

public static class EnumerableExtensions
{
	public static string JoinStrings<T>(this IEnumerable<T> source, string separator = ",")
	{
		return string.Join(separator, source);
	}
}

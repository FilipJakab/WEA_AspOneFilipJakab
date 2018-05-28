using System;

namespace WEA_AspOneFilipJakab.Models
{
	public class Response<T> : Response
	{
		public T Data { get; set; }
	}

	public class Response
	{
		public TimeSpan Elapsed { get; set; }

		public bool IsOk { get; set; }

		public string ErrorMessage { get; set; }
	}
}
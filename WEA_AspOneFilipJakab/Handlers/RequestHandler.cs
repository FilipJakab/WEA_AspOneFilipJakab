using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WEA_AspOneFilipJakab.Models;

namespace WEA_AspOneFilipJakab.Handlers
{
	public class RequestHandlers
	{
		public static JsonResult Handle<T>(Func<T> what)
		{
			Response<T> result = new Response<T>();

			try
			{
				Stopwatch watch = Stopwatch.StartNew();

				T data = what();

				watch.Stop();

				result.Data = data;
				result.Elapsed = watch.Elapsed;
				result.IsOk = true;
				result.ErrorMessage = null;
			}
			catch (Exception e)
			{
				result.IsOk = false;
				result.ErrorMessage = e.Message;
			}

			return new JsonResult(result);
		}

		public static JsonResult Handle(Action what)
		{
			Response result = new Response();

			try
			{
				Stopwatch watch = Stopwatch.StartNew();

				what();

				watch.Stop();

				result.IsOk = true;
				result.Elapsed = watch.Elapsed;
				result.ErrorMessage = null;
			}
			catch (Exception e)
			{
				result.IsOk = false;
				result.ErrorMessage = e.Message;
			}

			return new JsonResult(result);
		}
	}
}
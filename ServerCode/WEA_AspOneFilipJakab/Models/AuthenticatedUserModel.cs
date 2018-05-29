using DataProviders.Models;

namespace WEA_AspOneFilipJakab.Models
{
	public class AuthenticatedUserModel
	{
		public UserModel User { get; set; }

		public string Token { get; set; }
	}
}
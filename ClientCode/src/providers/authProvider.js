export default function (http, urlPrefix) {
	this.http = http
	this.urlPrefix = urlPrefix

	this.headers = {
		"Content-Type": "application/json"
	}

	this.Login = (email, password) => {
		if (!this.http)
			console.error("http not specified")
		console.log(this.http)

		return new Promise((resolve, reject) => {
			this.http({
				method: "post",
				url: this.urlPrefix + "/auth/login",
				headers: this.headers,
				data: JSON.stringify({
					email,
					password
				})
			}).then(response => resolve(response.data))
			.catch(err => reject(err))
		})
	}

	this.Register = (registrationModel) => {
		if (!this.http)
			console.error("http not specified")

		return new Promise((resolve, reject) => {
				this.http({
				method: "post",
				headers: this.headers,
				url: this.urlPrefix + "/auth/register",
				data: JSON.stringify(registrationModel)
			}).then(response => resolve(response.data))
			.catch(err => reject(err))
		})
	}
}

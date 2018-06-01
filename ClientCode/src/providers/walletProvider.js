export default function (http, urlPrefix, token) {
	this.http = http
	this.urlPrefix = urlPrefix
	this.token = token

	if (!token)
		console.error("Token is udenfined")

	this.headers = {
		"Content-Type": "application/json",
		"Authorization": "Bearer " + this.token
	}

	this.GetPayload = () => {
		return new Promise((resolve, reject) => {
			this.http({
				method: "get",
				headers: this.headers,
				url: this.urlPrefix + "/wallet"
			}).then(response => resolve(response.data))
			.catch(err => reject(err))
		})
	}

	this.AddTransaction = (transactionModel) => {
		return new Promise((resolve, reject) => {
			this.http({
				method: "post",
				headers: this.headers,
				url: this.urlPrefix + "/wallet",
				data: JSON.stringify(transactionModel)
			}).then(response => resolve(response.data))
			.catch(err => reject(err))
		})
	}

	this.AddTags = (tagsModel) => {
		return new Promise((resolve, reject) => {
			this.http({
				method: "post",
				headers: this.headers,
				url: this.urlPrefix + "/wallet/tags",
				data: JSON.stringify(tagsModel)
			}).then(response => resolve(response.data))
			.catch(err => reject(err))
		})
	}
}
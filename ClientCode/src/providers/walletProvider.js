export default function (http, urlPrefix, token) {
	this.http = http
	this.urlPrefix = urlPrefix

	this.headers = {
		"Content-Type": "application/json",
		"Authorization": "Bearer " + token
	}

	this.GetPayload = () => {
		return new Promise((resolve, reject) => {
			this.http({
				method: "get",
				headers: this.headers,
				url: this.urlPrefix + "/wallet"
			})
		})
	}

	this.AddTransactions = (transactionModels) => {
		return new Promise((resolve, reject) => {
			this.http({
				method: "post",
				headers: this.headers,
				url: this.urlPrefix + "/wallet/transactions",
				data: JSON.strigify(transactionModels)
			})
		})
	}
}
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
			.catch(err => {
				reject(err)
			})
		})
	}

	this.GetGroupedTransactions = () => {
		return new Promise((resolve, reject) => {
			this.http({
				method: "get",
				headers: this.headers,
				url: this.urlPrefix + "/wallet/groupedtransactions"
			}).then(response => resolve(response.data))
			.catch(err => {
				reject(err)
			})
		})
	}

	this.GetGroupedCategories = () => {
		return new Promise((resolve, reject) => {
			this.http({
				method: "get",
				headers: this.headers,
				url: this.urlPrefix + "/wallet/groupedcategories"
			}).then(response => resolve(response.data))
			.catch(err => {
				reject(err)
			})
		})
	}

	this.GetTransactions = () => {
		return new Promise((resolve, reject) => {
			this.http({
				method: "get",
				headers: this.headers,
				url: this.urlPrefix + "/wallet/transactions"
			}).then(response => resolve(response.data))
			.catch(err => {
				reject(err)
			})
		})
	}

	this.GetCategories = () => {
		return new Promise((resolve, reject) => {
			this.http({
				method: "get",
				headers: this.headers,
				url: this.urlPrefix + "/wallet/categories"
			}).then(response => resolve(response.data))
			.catch(err => {
				reject(err)
			})
		})
	}

	this.UpdateTransaction = (transaction) => {
		return new Promise((resolve, reject) => {
			this.http({
				method: "put",
				headers: this.headers,
				url: this.urlPrefix + "/wallet",
				data: JSON.stringify(transaction)
			}).then(response => resolve(response.data))
			.catch(err => {
				reject(err)
			})
		})
	} 

	this.DeleteTransaction = (transactionCode) => {
		return new Promise((resolve, reject) => {
			this.http({
				method: "delete",
				headers: this.headers,
				url: this.urlPrefix + "/wallet",
				data: JSON.stringify(transactionCode)
			}).then(response => resolve(response.data))
			.catch(err => reject(err))
		})
	}

	this.UpdateUser = (model) => {
		return new Promise((resolve, reject) => {
			this.http({
				method: "put",
				headers: this.headers,
				url: this.urlPrefix + "/user",
				data: JSON.stringify(model)
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

	this.DeleteTag = (tagId) => {
		return new Promise((resolve, reject) => {
			this.http({
				method: "delete",
				headers: this.headers,
				url: this.urlPrefix + "/wallet/tag?id=" + tagId
			}).then(response => resolve(response.data))
			.catch(err => reject(err))
		})
	}

	this.AddTag = (tagModel) => {
		return new Promise((resolve, reject) => {
			this.http({
				method: "post",
				headers: this.headers,
				url: this.urlPrefix + "/wallet/tag",
				data: JSON.stringify(tagModel)
			}).then(response => resolve(response.data))
			.catch(err => reject(err))
		})
	}
}
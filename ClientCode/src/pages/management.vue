<template>
	<div class="container full-width">
		<div class="row">
			<div class="col s6 center">
				<button data-target="addTransactionModal" class="btn waves-effect center-align modal-trigger">Add transaction</button>
			</div>
			<div class="col s6 center">
				<button data-target="addTagModal" class="btn waves-effect center-align modal-trigger">Add Tag</button>
			</div>
		</div>
		<div id="addTransactionModal" class="modal">
			<div class="modal-content">
				<h4>Create Transaction</h4>
				<div class="containter">
					<div class="row">
						<div class="col s12">
							<div class="input-field">
								<label for="transactionName">Transaction Name</label>
								<input type="text" id="transactionName" v-model="newTransactionModel.name">
							</div>
						</div>
					</div>
					<div class="row">
						<div class="col s12">
							<div class="input-field">
								<label for="transactionDescription">Transaction description</label>
								<textarea type="text" class="materialize-textarea" id="transactionDescription" v-model="newTransactionModel.description">
								</textarea>
							</div>
						</div>
					</div>
					<div class="row">
						<div class="col s12">
							<div class="input-field">
								<label for="transactionDate">Transaction's date</label>
								<input type="text" class="datepicker" id="transactionDate" v-model="newTransactionModel.date">
							</div>
						</div>
					</div>
					<div class="row">
						<div class="col s12">
							<div class="input-field">
								<select id="newTransactionCategory">
									<option value="1">val 1</option>
									<option value="2">val 2</option>
									<option
									:key="cat.categoryId"
									v-for="cat in categories"
									:value="cat.categoryId">{{cat.name}}</option>
								</select>
							</div>
						</div>
					</div>
					<div class="row">
						<div class="col s12">
							<div class="input-field">
								<div class="chips chips-placeholder chips-autocomplete" id="newTransactionTags"></div>
							</div>
						</div>
					</div>
				</div>
			</div>
			<div class="modal-footer">
				<a class="modal-close waves-effect waves-green btn-flat" @click="onTransactionSubmitted">Create</a>
				<a class="modal-close waves-effect waves-green btn-flat" @click="onTransactionCancelled">Cancel</a>
			</div>
		</div>
		<div id="addTagModal" class="modal">
			<div class="modal-content">
				<h4>Create Tag</h4>
				<div class="container">
					<div class="row">
						<div class="col s12">
							<div class="input-field">
								<label for="newTagName">Name of Tag</label>
								<input type="text" id="newTagName" v-model="newTagModel.name">
							</div>
						</div>
					</div>
				</div>
			</div>
			<div class="modal-footer">
				<a class="modal-close waves-effect waves-green btn-flat" @click="onTagSubmitted">Create</a>
				<a class="modal-close waves-effect waves-green btn-flat" @click="onTagCancelled">Cancel</a>
			</div>
		</div>
		<div class="row">
			<div class="col s7">
				<!-- user -->
				<div class="card" style="padding: 5px;">
					<div class="card-content">
						<span class="card-title">
							User Info
						</span>
						<div class="row">
							<div class="col s2">
								<div class="container">
									<div class="row">
										<div class="col s12">
											<strong>Name </strong>
										</div>
									</div>
									<div class="row">
										<div class="col s12">
											<strong>Email </strong>
										</div>
									</div>
									<div class="row">
										<div class="col s12">
											<strong>Birthdate </strong>
										</div>
									</div>
								</div>
							</div>
							<div class="col s10">
								<div class="container full-width">
									<div class="row">
										<div class="col s12 right-align">
											{{user.firstName}} {{user.lastName}}
										</div>
									</div>
									<div class="row">
										<div class="col s12 right-align">
											{{user.email}}
										</div>
									</div>
									<div class="row">
										<div class="col s12 right-align">
											{{user.birthdate}}
										</div>
									</div>
								</div>
							</div>
						</div>
					</div>
				</div>
			</div>
			<div class="col s5">
				<!-- tags -->
				<div class="card">
					<div class="card-content">
						<span class="card-title">
							Tags
						</span>
						<ul class="collection">
							<li :key="tag.tagId"
							v-for="tag in tags">
								{{tag.name}}
							</li>
						</ul>
					</div>
				</div>
			</div>
		</div>
		<div class="row">
			<div class="col s12">
				<div class="card">
					<div class="card-content">
						<div class="card-title">
							Transactions
						</div>
						<!-- trasactions -->
						<!-- to update transaction show modal -->
						<table class="stripped highlight">
							<thead>
								<tr>
									<td>Name</td>
									<td>Description</td>
									<td>Date</td>
									<td>Category</td>
									<td>Tags</td>
									<td>Transaction Code</td>
								</tr>
							</thead>
							<tbody>
								<tr :key="t.transactionCode"
								v-for="t in transactions">
									<td>{{t.name}}</td>
									<td>{{t.description}}</td>
									<td>{{t.date}}</td>
									<td>
										{{(categories.find(x => x.categoryId == t.categoryId) || {}).name}}
									</td>
									<td>
										{{t.tagModels | stringifyTags}}
									</td>
									<td>{{t.transactionCode}}</td>
								</tr>
							</tbody>
						</table>
						<!-- <div class="container">
							<div class="row">
							</div>
							<div
							class="row"
							v-for="t in transactions"
							:key="t.transactionCode">
								<div class="col s3">{{t.name}}</div>
								<div class="col s3">
									<p>{{t.description}}</p>
								</div>
								<div class="col s2">{{t.date}}</div>
								<div class="col s2">{{t.categoryId}}</div>
							</div>
						</div> -->
					</div>
				</div>
			</div>
		</div>
	</div>
</template>

<script>
// creating transactions & tags
// can retrieve userid from query
import WalletProvider from "../providers/walletProvider.js"
import Constants from "../constants.js"

export default {
	data () {
		return {
			transactions: [],
			tags: [],
			categories: [],
			user: {
				firstName: "",
				lastName: "",
				email: ""
			},

			newTransactionModel: {
				name: "",
				description: "",
				date: ""
			},
			newTagModel: {
				name: ""
			},

			provider: null,
			pageSelects: []
		}
	},
	filters: {
		stringifyTags(tags) {
			return tags.map(x => x.name).join("; ")
		}
	},
	methods: {
		onTagSubmitted() {
			this.provider.AddTag([this.newTransactionModel])
			.then(response => {
				console.log(response)
			}).catch(err => {
				M.toast({html:err})
			})
		},
		onTagCancelled() {
			// erase inputs
		},
		onTransactionSubmitted() {
			this.newTransactionModel.userId = this.user.userId

			// retrieve tags
 			var tagnames = M.Chips
			.getInstance($("#newTransactionTags"))
			.chipsData.map(x => x.tag)

			var tags = []
			this.tagnames.forEach(x => {
				var found = this.tags.find(y => x.name == y)

				if (found)
					tags.push({
						userId: this.user.userId,
						name: found.name,
						tagId: found.tagId
					})
				else
					tags.push({
						userId: this.user.userId,
						name: found.name
					})
			})

			this.newTransactionModel.tagModels = tags
			// M.Chips
			// .getInstance($("#newTransactionTags"))
			// .chipsData.map(x => {
			// 	console.log(x)
			// 	return {
			// 		name: x.tag,
			// 		userId: this.user.userId
			// 	}
			// })

			// normalize date
			this.newTransactionModel.date = M.Datepicker
			.getInstance($("#transactionDate")).toString("yyyy-mm-dd")

			// retrieve category from select
			this.newTransactionModel.categoryId = this.newCategorySelectInstance
			.getSelectedValues()[0]

			console.log(this.newTransactionModel)

			this.provider.AddTransaction(this.newTransactionModel)
			.then(response => {
				console.log(response)
			}).catch(err => {
				M.toast({html:err})
			})
		},
		onTransactionCancelled() {
			// erase inputs
		}
	},
	computed: {
		newCategorySelectInstance() {
			return this.pageSelects.find(x => x.$el[0].id == "newTransactionCategory")
		}
	},
	mounted() {
		this.provider = new WalletProvider(this.$http, Constants.backendUrl, this.$AuthToken())

		$(() => {
			$(".modal").each((index, x) => $(x).modal())

			$(".datepicker").each((i, x) => $(x).datepicker())

			this.pageSelects = M.FormSelect.init($("select"))
		})

		var vm = this

		this.provider
		.GetPayload()
		.then(response => {
			if (response.isOk) {
				var data = response.data
				vm.transactions = data.transactions
				vm.tags = data.tags
				vm.categories = data.categories
				vm.user = data.user

				var autocompleteData = {}
				vm.tags.forEach(x => autocompleteData[x.name] = null)
				$("#newTransactionTags").chips({
					placeholder: "Enter a Tag",
					secondaryPlaceholder: "+ Another Tag",
					autocompleteOptions: {
						data: autocompleteData
					}
				})
			} else {
				M.toast({html: response.errorMessage})
			}
		}).catch(err => M.toast({html: err}))
	}
}
</script>

<style>
	.full-width {
		width: 100%;
	}
</style>

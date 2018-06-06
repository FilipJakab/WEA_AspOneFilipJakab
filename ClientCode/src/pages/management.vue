<template>
	<div class="container full-width">
		<div class="row" v-if="loading">
			<div class="col s12">
				<div class="progress">
					<div class="indeterminate"></div>
				</div>
			</div>
		</div>
		<template v-else>
			<div class="row">
				<div class="col s6 center">
					<button data-target="addTransactionModal" class="btn waves-effect center-align modal-trigger">
						Add transaction
						<i class="material-icons left">
							create
						</i>
					</button>
				</div>
				<div class="col s6 center">
					<button data-target="addTagModal" class="btn waves-effect center-align modal-trigger">
						Add Tag
						<i class="material-icons right">
							label_outline
						</i>
					</button>
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
									<label for="transactionAmount" >Transaction Amount</label>
									<input type="number" id="transactionAmount" v-model="newTransactionModel.amount">
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
									<input type="text" class="datepicker" ref="transactionDate" v-model="newTransactionModel.date">
								</div>
							</div>
						</div>
						<div class="row">
							<div class="col s12">
								<div class="input-field">
									<select ref="newTransactionCategory">
										<option value="" disabled selected> Choose Category </option>
										<option :key="cat.categoryId"
										v-for="cat in categories"
										:value="cat.categoryId">{{cat.name}}</option>
									</select>
									<label>Transactions Category</label>
								</div>
							</div>
						</div>
						<div class="row">
							<div class="col s12">
								<div class="input-field">
									<div class="chips chips-placeholder chips-autocomplete" ref="newTransactionTags"></div>
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
							<a class="btn-floating halfway-fab waves-effect waves-light blue left" @click.prevent="onUserModify">
								<i class="material-icons">
									create
								</i>
							</a>
							<div class="row" :style="{ 'max-height': firstRowMaxHeight + 'px', 'overflow-y': 'auto' }">
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
												<strong>Balance </strong>
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
												{{user.balance}}
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
							<ul class="collection" :style="{ 'max-height': firstRowMaxHeight + 'px', 'overflow-y': 'auto' }">
								<li class="collection-item" :key="tag.tagId"
								v-for="tag in tags">
									{{tag.name}}
									<i class="material-icons right"
									@click="() => {confirmDeletition(); onDeleteConfirmedMethod = deleteTag; deletedItemId = tag.tagId}">
										delete
									</i>
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
										<td>Amount</td>
										<td>Description</td>
										<td>Date</td>
										<td>Category</td>
										<td>Tags</td>
										<td>Transaction Code</td>
										<td>Actions</td>
									</tr>
								</thead>
								<tbody>
									<tr :key="t.transactionCode"
									v-for="t in transactions">
										<td>{{t.name}}</td>
										<td>{{t.amount}}</td>
										<td>{{t.description}}</td>
										<td>{{t.date}}</td>
										<td>
											{{(categories.find(x => x.categoryId == t.categoryId) || {}).name}}
										</td>
										<td>
											{{t.tagModels | stringifyTags}}
										</td>
										<td>
											{{t.transactionCode}}
										</td>
										<td>
											<a class="btn-floating btn-small wavew-effect waves-light blue"
											@click="() => onTransactionModify(t)">
												<i class="material-icons">create</i>
											</a>
											<a class="btn-floating btn-small wavew-effect waves-light red"
											@click="() => {deletedItemId = t.transactionCode; onDeleteConfirmedMethod = deleteTransaction; confirmDeletition()}">
												<i class="material-icons">delete</i>
											</a>
										</td>
									</tr>
								</tbody>
							</table>
						</div>
					</div>
				</div>
			</div>
			<div class="modal" ref="deleteConfirm">
				<div class="modal-content">
					Are you sure, you want to delete this item?
				</div>
				<div class="modal-footer">
					<a class="modal-close waves-effect waves-green btn-flat" @click="onDeleteConfirmedMethod">Delete</a>
					<a class="modal-close waves-effect waves-green btn-flat">Cancel</a>
				</div>
			</div>
			<div class="modal" ref="modifyTransaction">
				<div class="modal-content">
					<h4>Modify Transaction</h4>
					<div class="containter">
						<div class="row">
							<div class="col s12">
								<div class="input-field">
									<label for="updateTransactionName" :class="{'active': modifiedTransaction.name}">New Transaction Name</label>
									<input type="text" id="updateTransactionName" v-model="modifiedTransaction.name">
								</div>
							</div>
						</div>
						<div class="row">
							<div class="col s12">
								<div class="input-field">
									<label for="updateTransactionAmount" :class="{'active': modifiedTransaction.amount}">New Transaction Amount</label>
									<input type="number" id="updateTransactionAmount" v-model="modifiedTransaction.amount">
								</div>
							</div>
						</div>
						<div class="row">
							<div class="col s12">
								<div class="input-field">
									<label for="newTransactionDescription" :class="{'active': modifiedTransaction.description}">
										New Transaction description
									</label>
									<textarea type="text"
									class="materialize-textarea"
									id="newTransactionDescription"
									v-model="modifiedTransaction.description">
									</textarea>
								</div>
							</div>
						</div>
						<div class="row">
							<div class="col s12">
								<div class="input-field">
									<label for="newTransactionDate" :class="{'active': modifiedTransaction.name}">
										New Transaction's date
									</label>
									<input type="text" class="datepicker" id="newDateXY" ref="newTransactionDate" v-model="modifiedTransaction.date">
									<span class="helper-text">Please manually select date even if its not modified</span>
								</div>
							</div>
						</div>
						<div class="row">
							<div class="col s12">
								<div class="input-field">
									<select ref="modifiedTransactionCategory">
										<option value="" disabled selected> Choose New Category </option>
										<option :key="cat.categoryId"
										v-for="cat in categories"
										:value="cat.categoryId">{{cat.name}}</option>
									</select>
									<label>Transactions Category</label>
								</div>
							</div>
						</div>
						<div class="row">
							<div class="col s12">
								<div class="input-field">
									<div class="chips chips-placeholder chips-initial chips-autocomplete" ref="updateTransactionTags"></div>
								</div>
							</div>
						</div>
					</div>
				</div>
				<div class="modal-footer">
					<a class="modal-close waves-effect waves-green btn-flat" @click="modifyTransaction">Modify</a>
					<a class="modal-close waves-effect waves-green btn-flat">Cancel</a>
				</div>
			</div>
			<div class="modal" ref="modifyUserModal">
				<div class="modal-content">
					<h4>Modify user's information</h4>
					<div class="containter">
						<div class="row">
							<div class="input-field col s12">
								<label for="modifyEmail" :class="{'active': modifiedUser.email}">New Email</label>
								<input type="email" class="validate" v-model="modifiedUser.email">
							</div>
						</div>
						<div class="row">
							<div class="input-field col s12">
								<label for="modifyEmail" :class="{'active': modifiedUser.balance}">New Balance</label>
								<input type="number" class="validate" v-model="modifiedUser.balance">
							</div>
						</div>
						<div class="row">
							<div class="input-field col s6">
								<label for="modifyEmail" :class="{'active': modifiedUser.firstName}">New First name</label>
								<input type="text" class="validate" v-model="modifiedUser.firstName">
							</div>
							<div class="input-field col s6">
								<label for="modifyEmail" :class="{'active': modifiedUser.lastName}">New Last name</label>
								<input type="text" class="validate" v-model="modifiedUser.lastName">
							</div>
						</div>
						<div class="row">
							<div class="input-field col s12">
								<label for="modifyEmail" :class="{'active': modifiedUser.birthdate}">New Birthdate</label>
								<input type="text" class="datepicker" ref="modifiedUserBirthdate">
								<span class="helper-text">Please manually select date even if its not modified</span>
							</div>
						</div>
					</div>
				</div>
				<div class="modal-footer">
					<a class="modal-close waves-effect waves-green btn-flat" @click="modifyUser">Modify</a>
					<a class="modal-close waves-effect waves-green btn-flat" @click="onUserModifyCanceled">Cancel</a>
				</div>
			</div>
		</template>
	</div>
</template>

<script>
// creating transactions & tags
// can retrieve userid from query
import WalletProvider from "../providers/walletProvider.js"
import walletProvider from '../providers/walletProvider.js';

export default {
	data () {
		return {
			firstRowMaxHeight: "180",
			loading: false,

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
				amount: null,
				description: "",
				date: ""
			},
			newTagModel: {
				name: ""
			},

			modifiedUser: {
				email: "",
				firstName: "",
				lastName: "",
				birthdate: "",
				userId: ""
			},
			modifiedTransaction: {
			},

			provider: null,
			pageSelects: [],

			deletedItemId: "",
			deletitionConfirmationModalInstance: {},
			onDeleteConfirmedMethod: () => {}
		}
	},
	filters: {
		stringifyTags(tags) {
			return tags.map(x => x.name).join("; ")
		}
	},
	methods: {
		confirmDeletition() {
			this.deletitionConfirmationModalInstance.open()
		},
		deleteTransaction() {
			debugger

			this.provider.DeleteTransaction(this.deletedItemId)
			.then(response => {
				if (!response.isOk)
					M.toast({html: "Error ocured while deleting transaction: " + response.errorMessage})
				else
					M.toast({html: "Transaction sucessfully deleted"})
			}).catch(err => M.toast({html: err}))
			.finally(() => this.load())

			this.onDeleteConfirmedMethod = () => {}
			this.deletedItemId = null
		},
		deleteTag() {
			this.provider.DeleteTag(this.deletedItemId)
			.then(response => {
				if (response.isOk)
					M.toast({html:"Sucessfully deleted Tag"})
				else
					M.toast({html:"Error ocured while deleting tag: " + response.errorMessage})
			}).catch(err => M.toast({html: err}))
			.finally(() => this.load())

			this.onDeleteConfirmedMethod = () => {}
			this.deletedItemId = null
		},
		onUserModify() {
			M.Modal.getInstance(this.$refs["modifyUserModal"]).open()
		},
		onUserModifyCanceled() {
			// erase inputs
		},
		modifyUser() {
			this.modifiedUser.birthdate = M.Datepicker
			.getInstance(this.$refs["modifiedUserBirthdate"]).toString("yyyy-mm-dd")

			this.provider.UpdateUser(this.modifiedUser)
			.then(response => {
				if (response.isOk)
					M.toast({html: "Modification sucessful"})
				else
					M.toast({html: "Modification failed: " + response.errorMessage})
			}).catch(err => M.toast({html: err}))
			.finally(() => this.load())
		},
		modifyTransaction() {
			var tagnames = M.Chips
			.getInstance(this.$refs["updateTransactionTags"])
			.chipsData.map(x => x.tag)
			var tags = []

			tagnames.forEach(x => {
				var found = this.tags.find(y => x == y.name)

				if (found)
					tags.push({
						userId: this.user.userId,
						name: found.name,
						tagId: found.tagId
					})
				else
					tags.push({
						userId: this.user.userId,
						name: x
					})
			}, this)

			this.modifiedTransaction.tagModels = tags
			
			// date
			this.modifiedTransaction.date = M.Datepicker
			.getInstance(this.$refs["newTransactionDate"]).toString("yyyy-mm-dd")

			// category
			// this.modifiedTransaction.categoryId = 
			console.log(M.FormSelect
			.getInstance(this.$refs["modifiedTransactionCategory"])
			.getSelectedValues())

			this.provider
			.UpdateTransaction(this.modifiedTransaction)
			.then(response => {
				if (response.isOk)
					M.toast({html: "Sucessully Modified"})
				else
					M.toast({html: "Error ocured when trying to modify transaction: " + response.errorMessage})
			}).catch(err => M.toast({html: err}))
			.finally(() => this.load())
		},
		onTransactionModify(transaction) {
			this.modifiedTransaction = Object.assign({}, transaction)
			this.modifiedTransaction.tagModels = Object.assign([], transaction.tagModels)

			var initialData = this.modifiedTransaction.tagModels.map(x => {
				return {
					tag: x.name
				}
			})
			var autocompleteData = {}
			this.tags.forEach(x => autocompleteData[x.name] = null)
			M.Chips.init(this.$refs["updateTransactionTags"], {
				placeholder: "Enter new Tag",
				secondaryPlaceholder: " + Another Tag",
				autocompleteOptions: {
					data: autocompleteData
				},
				data: initialData
			})

			M.Datepicker.getInstance(this.$refs["newTransactionDate"]).setDate(new Date(transaction.date))
			M.Datepicker.getInstance(this.$refs["newTransactionDate"]).gotoDate(new Date(transaction.date))

			M.Modal.getInstance(this.$refs["modifyTransaction"]).open()
		},
		InitMaterialHtml() {
			$(() => {
				$(".modal").each((index, x) => $(x).modal())

				this.deletitionConfirmationModalInstance = M.Modal.init(this.$refs["deleteConfirm"])

				$(".datepicker").each((i, x) => $(x).datepicker())

				M.Datepicker.init(this.$refs["modifiedUserBirthdate"])
				.setDate(new Date(this.user.birthdate))

				var autocompleteData = {}
				this.tags.forEach(x => autocompleteData[x.name] = null)
				M.Chips.init(this.$refs["newTransactionTags"], {
					placeholder: "Enter a Tag",
					secondaryPlaceholder: " + Another Tag",
					autocompleteOptions: {
						data: autocompleteData
					}
				})
				this.pageSelects = M.FormSelect.init($("select"))
			})
		},
		onTagSubmitted() {
			this.provider.AddTag(this.newTagModel)
			.then(response => {
				if (!response.isOk)
					M.toast({html: "Error ocured while adding new Tag: " + response.errorMessage})
				else
					M.toast({html: "Sucessfully add new Tag"})
			}).catch(err => {
				M.toast({html:err})
			}).finally(() => this.load())
		},
		onTagCancelled() {
			// erase inputs
		},
		onTransactionSubmitted() {
			this.newTransactionModel.userId = this.user.userId

			// retrieve tags
 			var tagnames = M.Chips
			.getInstance(this.$refs["newTransactionTags"])
			.chipsData.map(x => x.tag)

			var tags = []
			tagnames.forEach(x => {
				var found = this.tags.find(y => x == y.name)

				if (found)
					tags.push({
						userId: this.user.userId,
						name: found.name,
						tagId: found.tagId
					})
				else
					tags.push({
						userId: this.user.userId,
						name: x
					})
			}, this)

			this.newTransactionModel.tagModels = tags

			// normalize date
			this.newTransactionModel.date = M.Datepicker
			.getInstance(this.$refs["transactionDate"]).toString("yyyy-mm-dd")

			// retrieve category from select
			this.newTransactionModel.categoryId = this.newCategorySelectInstance
			.getSelectedValues()[0]

			this.provider.AddTransaction(this.newTransactionModel)
			.then(response => {
				if (response.isOk)
					M.toast({html: "Sucessfully added new Transaction"})
				else
					M.toast({html: "Error ocured while creating new Transaction: " + response.errorMessage})
				setTimeout(() => this.$router.go(), 1000)
			}).catch(err => {
				M.toast({html:err})
			}).finally(() => this.load())
		},
		onTransactionCancelled() {
			// erase inputs
		},
		load() {
			this.loading = true;
			this.provider
			.GetPayload()
			.then(response => {
				if (response.isOk) {
					var data = response.data
					this.transactions = data.transactions
					this.tags = data.tags
					this.categories = data.categories
					this.user = data.user
				} else {
					M.toast({html: response.errorMessage})
				}
			}).catch(err => {
				if ((err.response || {}).status === 401) {
					M.toast({html: "Authenticaction session expired Or you are not Authenticated. Sign in please"})
					setInterval(() => this.$router.push({name: "login"}), 3000)
				} else
					M.toast({html: err})
			}).finally(() => {
				this.InitMaterialHtml()

				this.modifiedUser = Object.assign({}, this.user)

				this.loading = false
			})
		}
	},
	computed: {
		newCategorySelectInstance() {
			return M.FormSelect.getInstance(this.$refs["newTransactionCategory"])
		}
	},
	mounted() {
		this.provider = new WalletProvider(this.$http, config.backendUrl, this.$AuthToken())

		this.load()
	}
}
</script>

<style>
	.full-width {
		width: 100%;
	}
</style>

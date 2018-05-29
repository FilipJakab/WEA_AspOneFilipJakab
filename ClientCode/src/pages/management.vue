<template>
	<div class="container">
		<div class="row">
			<div class="col s7">
				<!-- user -->
			</div>
			<div class="col s5">
				<!-- tags -->
			</div>
		</div>
		<div class="row">
			<div class="col s12">
				<!-- trasactions -->
				<div class="container">
					<div class="row">
						<!-- headers -->
					</div>
					<div
					class="row"
					v-for="t in transactions"
					:key="t.transactionCode">
						<div class="col s3">{{t.name}}</div>
						<div class="col s3">
							<p>
								<!-- Make modal -->
								{{t.description}}
							</p>
						</div>
						<div class="col s2">{{t.date}}</div>
						<div class="col s2">{{t.categoryId}}</div>
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
			user: {},
			provider: null
		}
	},
	mounted() {
		this.provider = new WalletProvider(this.$http, Constants.backendUrl)

		this.provider
		.GetPayload()
		.then(response => {
			if (response.isOk) {
				var data = response.data
				this.transactions = data.transactions
				this.tags = data.tags
				this.categories = data.categories
				this.user = user
			} else {
				M.toast({html: response.errorMessage})
			}
		}).catch(err => M.toast({html: err}))
	}
}
</script>

<style>

</style>

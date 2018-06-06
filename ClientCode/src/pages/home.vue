<template>
	<!-- <canvas ref="demoChart"></canvas> -->
	<div class="container">
		<!-- <div class="row">
			<div class="col s12">
				<canvas ref="demoChart"></canvas>
			</div>
		</div> -->
		<div class="row">
			<div class="col s12">
				<canvas ref="topLeftChart"></canvas>
			</div>
			<div class="col s12">
				<canvas ref="topRightChart"></canvas>
			</div>
		</div>
	</div>
</template>

<script>
import Chart from "chart.js"

import WalletProvider from "../providers/walletProvider.js"

import moment from "moment"

export default {
	data () {
		return {
			topLeftChart: null,
			topRightChart: null,
			bottomLeftChart: null,
			bottomRightChart: null,
			provider: null
		}
	},
	methods: {
		prepareColors(amount) {
			var result = []

			var getVal = () => Math.round(Math.random() * 255)

			for (var x = 0; x < amount; x++) {
				var txt = `rgba(${getVal()}, ${getVal()}, ${getVal()}, 0.7)`
				result.push(txt)
			}

			return result
		}
	},
	mounted() {
		this.provider = new WalletProvider(this.$http, "http://localhost:61057", this.$AuthToken())

		Promise.all([this.provider.GetGroupedTransactions(), this.provider.GetGroupedCategories()])
		.then((responses) => {
			var transactions = responses[0]
			var categories = responses[1]

			if (!transactions.isOk) {
				M.toast({html: transactions.errorMessage})
				return
			}

			// show spent data group by months in past year
			debugger
			var data =  transactions.data.map(x => x.total	)
			var labels = transactions.data.map(x => moment().month(x.month).format("MMMM"))
			var bgColor = this.prepareColors(transactions.data.length)
			var borderColor = this.prepareColors(transactions.data.length)
			this.topLeftChart = new Chart(this.$refs["topLeftChart"], {
				type: "bar",
				data: {
					labels: labels,
					datasets: [{
						label: "# Money spent in each month",
						data,
						backgroundColor: bgColor,
						borderColor: borderColor
					}]
				}
			})

			// show spent money grouped by categories in current year
			debugger
			data = categories.data.map(x => x.total)
			labels = categories.data.map(x => x.category)
			bgColor = this.prepareColors(transactions.data.length)
			borderColor = this.prepareColors(categories.data.length)
			this.topRightChart = new Chart(this.$refs["topRightChart"], {
				type: "doughnut",
				data: {
					labels: labels,
					datasets: [{
						label: "Money spent for each Category of this year",
						data: data,
						backgroundColor: this.prepareColors(categories.data.length),
						borderColor: borderColor
					}]
				}
			})
		}).catch(err => {
			debugger
			M.toast({html: err})
		})

		// var myChart = new Chart(this.$refs.demoChart, {
		// 	type: 'polarArea',
		// 	data: {
		// 			labels: ["Red", "Blue", "Yellow", "Green", "Purple", "Orange"],
		// 			datasets: [{
		// 				label: '# of Votes',
		// 				data: [12, 19, 3, 5, 2, 3],
		// 				backgroundColor: [
		// 					'rgba(255, 99, 132, 0.2)',
		// 					'rgba(54, 162, 235, 0.2)',
		// 					'rgba(255, 206, 86, 0.2)',
		// 					'rgba(75, 192, 192, 0.2)',
		// 					'rgba(153, 102, 255, 0.2)',
		// 					'rgba(255, 159, 64, 0.2)'
		// 				],
		// 				borderColor: [
		// 					'rgba(255,99,132,1)',
		// 					'rgba(54, 162, 235, 1)',
		// 					'rgba(255, 206, 86, 1)',
		// 					'rgba(75, 192, 192, 1)',
		// 					'rgba(153, 102, 255, 1)',
		// 					'rgba(255, 159, 64, 1)'
		// 				],
		// 				borderWidth: 1
		// 		}]
		// 	}
			
		// });
	}
}
</script>

<style>

</style>

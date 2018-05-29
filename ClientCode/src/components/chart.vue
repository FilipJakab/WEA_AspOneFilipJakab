<template>
	<canvas :ref="id" :width="width" :height="height"></canvas>
</template>

<script>
import Chart from "chart.js"

export default {
	props: [
		id,
		height,
		width,
		type,
		data,
		beginAtZero
	],
	data () {
		return {
			chart: null
		}
	},
	methods: {

	},
	mounted() {
		if (this.labels.length !== this.data.length)
			return

		var labels = []
		var data = []

		for (label in this.data) {
			labels.push(label)

			data.push(this.data[label])
		}

		this.chart = new Chart(this.$refs[this.id], {
			type: this.type,
			data: {
				labels: labels,
				datasets: [{
					data: data
				}]
			},
			options: {
				scales: {
					yAxes: [{
						ticks: {
							beginAtZero: this.beginAtZero
						}
					}]
				}
			}
		})
	}
}
</script>

<style>

</style>

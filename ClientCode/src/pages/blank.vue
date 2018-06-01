<template>
	<div class="containter">
		<div class="row">
			<div class="col s12">
				<router-view></router-view>
			</div>
		</div>
	</div>
</template>

<script>
export default {
	mounted() {
		if (!this.$AuthToken)
			this.$router.push({name: "login"})
		else
			this.$router.push(this.$route.path == "/"
				? "/home"
				: this.$route.path)
	}
}
</script>

<style>

</style>

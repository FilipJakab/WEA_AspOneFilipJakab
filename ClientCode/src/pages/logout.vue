<template>
	<div class="container">
		<div class="row">
			<div class="col s6 offset-s3">
				<div class="card blue-grey darken-1">
					<div class="card-content white-text">
						<span class="card-title">You have been signed out</span>
						<p>
							
						</p>
					</div>
					<div class="card-action">
						Back to Sign In <router-link :to="{name:'login'}">page</router-link>
					</div>
				</div>
			</div>
		</div>
	</div>
</template>

<script>
export default {
	mounted() {
		window.localStorage.removeItem(config.authTokenName)
		window.localStorage.removeItem(config.userIdName)
		window.sessionStorage.removeItem(config.authTokenName)
		window.sessionStorage.removeItem(config.userIdName)
	}
}
</script>

<style>

</style>

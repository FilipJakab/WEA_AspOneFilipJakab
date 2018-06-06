<template>
	<nav class="teal lighten-1">
		<div class="nav-wrapper">
			<router-link class="brand-logo" :to="{name: 'home'}">
				<i class="material-icons large">
					home
				</i>
				Home
			</router-link>
			<ul class="right hide-on-med-and-down">
				<li>
					<router-link :to="{name:'management'}" class="tooltipped" data-position="bottom" data-tooltip="Management">
						<i class="material-icons">
							account_balance_wallet
						</i>
					</router-link>
				</li>
				<li>
					<router-link :to="{name:'logout'}" class="tooltipped" data-position="right" data-tooltip="Sign out">
						<i class="material-icons">
							exit_to_app
						</i>
					</router-link>
				</li>
			</ul>
		</div>
	</nav>
</template>

<script>
export default {
	data () {
		return {
		}
	}
}
</script>

<style>

</style>

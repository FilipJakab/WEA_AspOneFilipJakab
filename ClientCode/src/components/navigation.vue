<template>
	<nav class="teal lighten-1">
		<div class="nav-wrapper">
			<router-link class="brand-logo" :to="{name: 'home'}">
				Home
			</router-link>
			<ul class="right hide-on-med-and-down">
				<li>
					<router-link :to="{name:'management'}">Management</router-link>
				</li>
				<li>
					<router-link :to="{name:'logout'}">Sign out</router-link>
				</li>
			</ul>
		</div>
	</nav>
</template>

<script>
import Constants from "../constants.js"

export default {
	data () {
		return {
		}
	}
}
</script>

<style>

</style>

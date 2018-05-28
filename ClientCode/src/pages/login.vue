<template>
	<div class="container">
		<div class="row">
			<div class="col s4 offset-s4 container" id="form">
				<div class="row">
					<div class="input-field col s12">
						<input type="text" id="Email" v-model="email">
						<label for="Email">Email</label>
					</div>
					<div class="input-field col s12">
						<input type="password" id="password" v-model="password">
						<label for="password">Password</label>
					</div>
					<button class="btn waves-effect waves-light" @click="onLoginSubmited">
						Submit
					</button>
				</div>
			</div>
		</div>
	</div>
</template>

<script>
export default {
	data () {
		return {
			email: "",
			password: ""
		}
	},
	methods: {
		onLoginSubmited() {

		}
	}
}
</script>

<style lang="scss">
	#form {
		margin-top: 80px;
	}
</style>

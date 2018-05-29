<template>
	<div class="container">
		<div class="row">
			<div class="col s8 offset-s2 container" id="form">
				<div class="row">
					<div class="input-field col s12">
						<input type="email" id="Email" v-model="email" class="validate">
						<label for="Email">Email</label>
					</div>
					<div class="input-field col s12">
						<input type="password" id="password" v-model="password" @keyup.enter="onLoginSubmited">
						<label for="password">Password</label>
					</div>
					<div class="col s6">
						<label>
							<input id="indeterminate-checkbox" v-model="rememberMe" type="checkbox" />
							<span>Remember me</span>
						</label>
					</div>
					<div class="col s4 offset-s2">
						<button class="btn waves-effect waves-light right" style="width: 100%" @click.prevent="onLoginSubmited">
							Sign in
						</button>
					</div>
					<div class="col s12">
						<small class="right" style="margin-top: 5px;">
							Dont have an account? Register <router-link :to="{name:'register'}">Here</router-link>
						</small>
					</div>
				</div>
			</div>
		</div>
	</div>
</template>

<script>
import AuthProvider from "../providers/authProvider.js"
import Constants from "../constants.js"
import constants from '../constants.js';

export default {
	data () {
		return {
			email: "",
			password: "",
			rememberMe: false,
			provider: undefined
		}
	},
	methods: {
		onLoginSubmited() {
			this.provider
			.Login(this.email, this.password)
			.then((response) => {
				console.log(response)

				if (response.isOk) {
					// store token and user id to localStorage (user Constants)
					if (this.rememberMe) {
						window.localStorage.setItem(Constants.authTokenName, response.data.token)
						window.localStorage.setItem(Constants.userIdName, response.data.user.userId)
					} else {
						window.sessionStorage.setItem(Constants.authTokenName, response.data.token)
						window.sessionStorage.setItem(Constants.userIdName, response.data.user.userId)
					}
					this.$router.push({name: "home"})
				} else
					M.toast({html: response.errorMessage})
			}).catch((err) => {
				M.toast({html: err})
			})
		}
	},
	mounted() {
		this.provider = new AuthProvider(this.$http, constants.backendUrl)
	}
}
</script>

<style lang="scss">
	#form {
		margin-top: 80px;
	}
</style>

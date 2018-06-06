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
					<div class="col s1 offset-s1">
						<div class="preloader-wrapper small active" v-show="loading">
							<div class="spinner-layer spinner-green-only right">
								<div class="circle-clipper left">
									<div class="circle"></div>
								</div><div class="gap-patch">
									<div class="circle"></div>
								</div><div class="circle-clipper right">
									<div class="circle"></div>
								</div>
							</div>
						</div>
					</div>
					<div class="col s4">
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

export default {
	data () {
		return {
			email: "",
			password: "",
			rememberMe: false,
			provider: null,
			loading: false
		}
	},
	methods: {
		onLoginSubmited() {
			this.loading = true
			this.provider
			.Login(this.email, this.password)
			.then((response) => {
				console.log(response)

				if (response.isOk) {
					// store token and user id to localStorage (user config)
					if (this.rememberMe) {
						window.localStorage.setItem(config.authTokenName, response.data.token)
						window.localStorage.setItem(config.userIdName, response.data.user.userId)
					} else {
						window.sessionStorage.setItem(config.authTokenName, response.data.token)
						window.sessionStorage.setItem(config.userIdName, response.data.user.userId)
					}
					this.$router.push({name: "home"})
					// this.$router.push({name: "home"})
				} else
					M.toast({html: response.errorMessage})
			}).catch((err) => {
				M.toast({html: err})
			}).finally(() => this.loading = false)
		}
	},
	mounted() {
		this.provider = new AuthProvider(this.$http, config.backendUrl)
	}
}
</script>

<style lang="scss">
	#form {
		margin-top: 80px;
	}
</style>

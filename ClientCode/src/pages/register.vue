<template>
	<div class="container">
		<div class="row">
			<div class="col s8 offset-s2 container" id="form">
				<div class="row">
					<div class="input-field col s12">
						<input type="email" id="Email" v-model="email" class="validate">
						<label for="Email">Email</label>
					</div>
					<div class="input-field col s6">
						<input type="password" id="password" v-model="password[0]" :class="{validate:true,invalid:!isValidPassword}" minlength="4">
						<label for="password">Password</label>
					</div>
					<div class="input-field col s6">
						<input type="password" id="password" v-model="password[1]" :class="{validate:true,invalid:!isValidPassword}" minlength="4">
						<label for="password">Password again</label>
					</div>
					<div class="input-field col s6 offset-s3">
						<input type="number" id="balance" v-model="balance" class="validate">
						<label for="balance">Initial balance</label>
					</div>
					<div class="input-field col s12">
						<label for="birth">
							Date of birth
						</label>
						<input type="text" class="datepicker" v-model="birthdate" id="birth">
					</div>
					<div class="input-field col s12">
						<label for="firstname">
							First name
						</label>
						<input id="firstname" type="text" v-model="firstname" class="validate" required>
					</div>
					<div class="input-field col s12">
						<label for="lastname">
							Last name
						</label>
						<input id="lastname" type="text" v-model="lastname" class="validate" required>
					</div>
					<button class="btn waves-effect waves-light col s12" @click.prevent="onRegisterSubmitted">
						Register
					</button>
					<div class="col s12">
						<small>
							Already have an account? Sign in <router-link :to="{name:'login'}">Here</router-link>
						</small>
					</div>
				</div>
			</div>
		</div>
	</div>
</template>

<script>
import AuthProvider from "../providers/authProvider.js"

import RegistrationModel from "../models/registrationModel.js"

export default {
	data () {
		return {
			email: "",
			balance: 0,
			password: ["", ""],
			firstname: "",
			lastname: "",
			birthdate: undefined,
			provider: undefined
		}
	},
	computed: {
		isValidPassword() {
			return this.password[0] === this.password[1]
		}
	},
	methods: {
		onRegisterSubmitted() {
			if (this.password[0] !== this.password[1]) {
				alert("Passwords doesnt match")
				return
			}

			this.provider.Register(
				new RegistrationModel(
					this.email,
					this.balance,
					this.password[0],
					M.Datepicker.getInstance($("#birth")).toString("yyyy-mm-dd"),
					this.firstname,
					this.lastname
				)
			).then(response => {
				console.log(response)

				if (response.isOk)
					this.$router.push({ name: "login" })
				else
					M.toast({html: response.errorMessage})
			}).catch((err) => {
				M.toast({html: err})
			})
		}
	},
	mounted() {
		$(() => {
			$(".datepicker").datepicker({
				maxDate: new Date(),
				yearRange: 50,
				firstDay: 1,
				showClearBtn: true
			})
			M.Datepicker.getInstance($("#birth")).setDate(new Date())
		})

		this.provider = new AuthProvider(this.$http, "http://localhost:61057")
	}
}
</script>

<style>
	#form {
		margin-top: 80px;
	}
</style>

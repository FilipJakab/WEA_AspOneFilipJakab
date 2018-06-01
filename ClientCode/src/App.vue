<template>
  <div class="container">
    <div class="row" v-if="token">
			<div class="col s12">
				<navigation></navigation>
			</div>
		</div>
		<div class="row">
			<div class="col s12">
				<router-view></router-view>
			</div>
		</div>
  </div>
</template>

<script>
import Const from "./constants.js"

import Navigation from "./components/navigation.vue"

export default {
  name: "app",
	components: {
		Navigation
	},
  data () {
    return {
    }
  },
	computed: {
		token() {
			return this.$AuthToken()
		}
	},
	mounted() {
		M.AutoInit()

		if (!this.token)
			this.$router.push({name: "login"})
	}
}
</script>

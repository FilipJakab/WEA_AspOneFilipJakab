import Vue from "vue"
import App from "./App.vue"

import Axios from "axios"
import VueRouter from "vue-router"

import Routes from "./routes"

const router = new VueRouter(Routes)

Vue.use(VueRouter)

Vue.prototype.$http = Axios

new Vue({
  el: "#app",
	router,
	http: Axios,
  render: h => h(App)
})

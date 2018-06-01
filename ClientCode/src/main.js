import Vue from "vue"
import Axios from "axios"
import VueRouter from "vue-router"

import App from "./App.vue"

import Routes from "./routes"
import Constants from "./constants"

Vue.use(VueRouter)

Vue.prototype.$http = Axios

Vue.prototype.$AuthToken =
	() => window.localStorage.getItem(Constants.authTokenName) || window.sessionStorage.getItem(Constants.authTokenName)

const router = new VueRouter(Routes)

var myVue = new Vue({
  el: "#app",
	router,
	http: Axios,
  render: h => h(App)
})

if (!myVue.$AuthToken)
	router.replace({name: "login"})

import Vue from "vue"
import Axios from "axios"
import VueRouter from "vue-router"

import Login from "./pages/login.vue"
import App from "./App.vue"

import Routes from "./routes"

Vue.use(VueRouter)

Vue.prototype.$http = Axios

Vue.prototype.$AuthToken =
	() => window.localStorage.getItem(config.authTokenName) || window.sessionStorage.getItem(config.authTokenName)

const router = new VueRouter(Routes)

var myVue = new Vue({
  el: "#app",
	router,
	http: Axios,
  render: h => h(Vue.prototype.$AuthToken() ? App : Login)
})

// if (!myVue.$AuthToken())
// 	router.replace({name: "login"})

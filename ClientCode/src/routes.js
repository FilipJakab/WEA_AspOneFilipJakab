import Blank from "./pages/blank.vue"
import Login from "./pages/login.vue"
import Register from "./pages/register.vue"
import Home from "./pages/home.vue"
import Management from "./pages/management.vue"
import Logout from "./pages/logout.vue"

export default {
	routes: [
		{
			path: "/",
			name: "root",
			component: Blank,
			children: [
				{
					path: "auth",
					component: Blank,
					children: [
						{
							path: "login",
							name: "login",
							component: Login
						},
						{
							path: "register",
							name: "register",
							component: Register
						}
					]
				},
				{
					path: "home",
					name: "home",
					component: Home
				},
				{
					path: "management",
					name: "management",
					component: Management
				},
				{
					path: "logout",
					name: "logout",
					component: Logout
				}
			]
		},
		{
			path: "/*",
			name: "EE",
			redirect: "/home"
		}
	]
}
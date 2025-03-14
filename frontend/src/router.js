import { createRouter, createWebHistory } from "vue-router";
import  Login  from "./components/Login.vue"
import  Register from "./components/Register.vue"
import HomeView from "./views/HomeView.vue"

const routes = [
    { path: '/login', component: () => import("./components/Login.vue")},
    { path: '/register', component: () => import("./components/Register.vue")}
]

const router = createRouter({
    history: createWebHistory(),
    routes
});

export default router;
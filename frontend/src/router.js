import { createRouter, createWebHistory } from "vue-router";
import HomeView from "./views/HomeView.vue"; // Trang chính
import Login from "./components/Login.vue";
import Register from "./components/Register.vue";
import RestaurantDashboard from "./components/RestaurantDashBBoard.vue";
import DrinksList from "./components/DrinksList.vue";
import FoodList from "./components/FoodList.vue";

const routes = [
    {
        path: "/",
        component: HomeView,
        children: [
          { path: "login", component: Login }, // Login sẽ load như modal trong Home
          { path: "register", component: Register }, // Register cũng thế
          { path: "/drinklist", component: DrinksList},
          { path: "/foodlist", component: FoodList},
        ],
    },
    { path: "/customer/:username", component: HomeView }, // Trỏ về Home.vue
    { path: "/restaurant/dashboard", component: RestaurantDashboard }, // Trang nhà hàng
 ];

const router = createRouter({
    history: createWebHistory(),
    routes,
});

export default router;

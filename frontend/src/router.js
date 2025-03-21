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
          { path: "login", component: Login }, 
          { path: "register", component: Register },
          { path: "drinklist", component: DrinksList }, // Mặc định cho khách chưa đăng nhập
          { path: "foodlist", component: FoodList },
        ],
    },
    { path: "/customer/:username", component: HomeView, children: [
        { path: "drinklist", component: DrinksList }, // Dành cho user đã đăng nhập
        { path: "foodlist", component: FoodList },
    ]},
    { path: "/restaurant/dashboard", component: RestaurantDashboard }, // Trang nhà hàng
];

const router = createRouter({
    history: createWebHistory(),
    routes,
});

// Trước khi chuyển route, kiểm tra xem user đã đăng nhập chưa
router.beforeEach((to, from, next) => {
    const username = localStorage.getItem("username"); // Kiểm tra user đã đăng nhập chưa

    if (username) {
        // Nếu user đăng nhập và đang truy cập drinklist/foodlist ở root → Điều hướng đến /customer/:username/
        if (to.path === "/drinklist") {
            return next(`/customer/${username}/drinklist`);
        }
        if (to.path === "/foodlist") {
            return next(`/customer/${username}/foodlist`);
        }
    }

    // Nếu chưa đăng nhập hoặc đường dẫn hợp lệ, tiếp tục bình thường
    next();
});

export default router;

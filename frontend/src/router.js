import { createRouter, createWebHistory } from "vue-router";
import HomeView from "./views/HomeView.vue";
import Login from "./components/Login.vue";
import Register from "./components/Register.vue";
import RestaurantDashboard from "./components/RestaurantDashBBoard.vue";
import DrinksList from "./components/DrinksList.vue";
import FoodList from "./components/FoodList.vue";
import RestaurantProduct from "./components/RestaurantProducts.vue";
import Dashboard from "./components/dashboard.vue"
import AddFood from "./components/AddFood.vue"
import Restaurant from "./components/RestaurantList.vue";
import OderFood from "./components/OderFood.vue";
const routes = [
    {
        path: "/",
        component: HomeView,
        children: [
            { path: "", component: Restaurant }, // Trang chính
            { path: "login", component: Login },
            { path: "register", component: Register },
            { path: "drinklist", component: DrinksList },
            { path: "foodlist", component: FoodList },
            { path: "oderfood", component: OderFood },
            { path: "oderfood/:id", component: OderFood },
        ],
    },
    {
        path: "/customer/:username",
        component: HomeView,
        children: [
            { path: "", component: Restaurant }, // Trang chính
            { path: "drinklist", component: DrinksList },
            { path: "foodlist", component: FoodList },
            { path: "oderfood/:id", component: OderFood },
        ],
    },
    {
        path: "/restaurant/dashboard", // Trang Dashboard chính
        name: 'Dashboard',
        component: RestaurantDashboard,
        children: [
            {
                path: '', 
                name: 'DashboardHome',
                component: Dashboard, 
            },
            {
                path: 'product', // Trang sản phẩm
                name: 'Product',
                component: RestaurantProduct,
                children: [ 
                    {
                        path:'addFood', // Thêm món ăn
                        name: 'AddFood',
                        component: AddFood,
                    }
                ]
            }
        ]
    },
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

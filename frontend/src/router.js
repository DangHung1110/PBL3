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
import OrderHistory from "./components/OrderHistory.vue";
import RestaurantStatistics from "./components/RestaurantStatistics.vue";
import RestaurantOrderList from "./components/RestaurantOderList.vue";
import RestaurantInvoices from "./components/RestaurantInvoices.vue";
import GrabMain from "./components/GrabMain.vue";
import GrabData from "./components/GrabData.vue";
import GrabDashBoard from "./components/GrabDashBoard.vue";
import AdminDashBoard from "./components/AdminDashBoard.vue";
import AdminGrab from "./components/AdminGrab.vue";
import AdminThongke from "./components/AdminThongke.vue";
import AdminRes from "./components/AdminRes.vue";
import AdminWaitRes from "./components/AdminWaitRes.vue";
import AdminGrabWait from "./components/GrabWait.vue";
const routes = [
    {
        path: "/",
        component: HomeView,
      
        children: [
            { path: "", component: Restaurant,  }, // Trang chính
            { path: "login", component: Login,  },
            { path: "register", component: Register,   },
            { path: "drinklist", component: DrinksList,},
            { path: "foodlist", component: FoodList , },
            { path: "oderfood", component: OderFood },
            { path: "oderfood/:id", component: OderFood},
        ],
    },
    {
        path: "/customer/:username",
        component: HomeView,
             meta: { requiresAuth: true }  ,
        children: [
            { path: "", component: Restaurant }, // Trang chính
            { path: "drinklist", component: DrinksList },
            { path: "foodlist", component: FoodList },
            { path: "oderfood/:id", component: OderFood },
        ],
    },
    {
        path: "/restaurant/dashboard",
        name: "Dashboard",
             meta: { requiresAuth: true ,role: 'Restaurant'}  ,
        component: RestaurantDashboard, // layout chứa sidebar + router-view
        children: [
          {
            path: "",
            name: "DashboardHome",
            component: Dashboard,
             meta: { requiresAuth: true ,role: 'Restaurant'}
          },
          {
            path: "product",
            name: "Product",
            component: RestaurantProduct,
                     meta: { requiresAuth: true,role: 'Restaurant' },
            children: [
              {
                path: "addFood",
                name: "AddFood",
                component: AddFood,
                         meta: { requiresAuth: true ,role: 'Restaurant'}
              },
            ],
          },
          {
            path: "statistics",
            name: "statistics",
            component: RestaurantStatistics,
                     meta: { requiresAuth: true ,role: 'Restaurant'}
            
          },
          {
            path: "invoices",
            name: "Invoices",
            component:RestaurantInvoices,
                     meta: { requiresAuth: true }
          },
         
          {
            path: "RestaurantOrderList",
            name: "RestaurantOrderList",
            component: RestaurantOrderList,
                     meta: { requiresAuth: true ,role: 'Restaurant'}
          },
        ],
      }
      ,
   {
    path:"/orderHistory",
    component: OrderHistory,
    name: 'OrderHistory',
             meta: { requiresAuth: true, role: 'Customer' },
   },
   {
    path:"/Grab",
    component: GrabDashBoard,
    name: 'GrabDashBoard',
             meta: { requiresAuth: true ,role: 'Grab'},
     children: [
          {
            path: "",
            name: "GrabMain",
            component: GrabMain,
                     meta: { requiresAuth: true,role: 'Grab' }
          },
           {
            path: "Thongke",
            name: "Thongkegrab",
            component: GrabData,
                     meta: { requiresAuth: true,role: 'Grab'}
          },
        ]
   },
     {
    path:"/Admin",
    component: AdminDashBoard,
    name: 'AdminDashBoard',
             meta: { requiresAuth: true ,role: 'Admin'},
     children: [
          {
            path: "Res",
            name: "AdminRes",
            component: AdminRes,
                     meta: { requiresAuth: true ,role: 'Admin'}
          },
           {
            path: "WaitRes",
            name: "AdminWaitRes",
            component: AdminWaitRes,
                     meta: { requiresAuth: true,role: 'Admin' }
          },
           {
            path: "Thongke",
            name: "Thongke",
            component: AdminThongke,
                     meta: { requiresAuth: true ,role: 'Admin'}
          },
      
         
           {
            path: "Grab",
            name: "AdminGrab",
            component: AdminGrab,
                     meta: { requiresAuth: true,role: 'Admin' }
          },
           {
            path: "GrabWait",
            name: "AdminGrabWait",
            component: AdminGrabWait,
                     meta: { requiresAuth: true,role: 'Admin' }
            
          },
        ]
    
   },

];

const router = createRouter({
    history: createWebHistory(),
    routes,
});


router.beforeEach((to, from, next) => {
  const username = localStorage.getItem("UserName");
  const role = localStorage.getItem("Role");


  if (to.meta.requiresAuth && !username) {
    return next("/login");
  }

  if (username) {
    if (to.path === "/drinklist") {
      return next(`/customer/${username}/drinklist`);
    }
    if (to.path === "/foodlist") {
      return next(`/customer/${username}/foodlist`);
    }
  }

  if (to.meta.requiresAuth && to.meta.role) {
    if (role !== to.meta.role) {
  
      switch (role) {
       
        case "Restaurant":
          return next("/restaurant/dashboard");
        case "Grab":
          return next("/Grab");
        case "Admin":
          return next("/Admin");
        default:
          return next("/login");
      }
    }
  }

  // Nếu không có vấn đề gì → tiếp tục route
  next();
});


export default router;

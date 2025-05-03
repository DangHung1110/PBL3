<template>
  <div>
    <!-- Hiển thị Navbar nếu không phải trang dashboard -->
    <div
      class="container"
      v-if="!$route.path.includes('/restaurant/dashboard')"
    >
      <div class="navbar">
        <div class="logo">
          <div class="logo-food"></div>
          <div class="Name">GRAP FOOD</div>
        </div>

        <div class="home-page">
          <div class="category-buttons">
            <button class="category food" @click="navigateTo('/foodlist')">
              <span class="icon">🍔</span> Đồ ăn
            </button>
            <button class="category drink" @click="navigateTo('/drinklist')">
              <span class="icon">🥤</span> Đồ uống
            </button>
          </div>
          <div class="search-bar">
            <input
              type="text"
              v-model="searchQuery"
              placeholder="Tìm kiếm món ăn..."
              class="search-input"
              @keydown.enter="emitSearch"
            />
            <span class="search-icon">🔍</span>
          </div>
        </div>

        <div v-if="isLoggedIn" class="user-menu">
          <div class="user-info" @click="toggleDropdown">
            <div class="avatar">{{ username.charAt(0).toUpperCase() }}</div>
            <span class="username">{{ username }}</span>
          </div>
          <div v-if="dropdownOpen" class="dropdown-menu">
            <button @click="openOrderHistoryPopup" class="dropdown-item">
              🛒 Thông tin đơn hàng
              <span class="badge" v-if="orderCount > 0">{{ orderCount }}</span>
            </button>
            <button @click="openorderHistory" class="dropdown-item">
              📝 Lịch sử đặt hàng
            </button>
            <button @click="handleLogout" class="dropdown-item logout">
              🚪 Đăng xuất
            </button>
          </div>
        </div>

        <div v-else class="login">
          <button class="login-button">
            <font-awesome-icon
              icon="user"
              style="font-size: 20px; color: white"
            />
          </button>
          <div class="boder-login">
            <router-link to="/login" class="loginn">Đăng nhập</router-link>
            <router-link to="/register" class="register">Đăng ký</router-link>
          </div>
        </div>
      </div>

      <div class="content">
        <router-view></router-view>
      </div>
      <div v-if="isLoggedIn ">
      <div v-if="orderHistoryPopupOpen" class="popup">
        <div class="popup-content">
          <h2 class="popup-title"> 🛒 Đơn hàng</h2>
          <button
            class="close-button"
            @click="closeOrderHistoryPopup"
            aria-label="Đóng popup"
          >
            <span>&times;</span>
          </button>

          <table class="order-table">
            <thead>
              <tr>
                <th>Tên món</th>
                <th>Nhà hàng</th>
                <th>Hình ảnh</th>
                <th>Số lượng</th>
                <th>Tổng tiền</th>
                <th>Trạng thái</th>
                <th>Hành động</th>
                <th>Thời gian đặt hàng</th>
                <th>Thời gian xác nhận</th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="(item, index) in thongkedata" :key="index">
                <td>{{ item.FoodName }}</td>
                <td>{{ item.RestaurantName }}</td>
                <td>
                  <img
                    :src="item.Url_image"
                    alt="Ảnh món ăn"
                    class="food-image"
                  />
                </td>
                <td>{{ item.Quantity }}</td>
                <td>{{ item.TotalPrice }} VNĐ</td>
                <td>
                  <button
                    class="confirm-btn"
                    v-if="item.Status_Restaurant === 'confirmed'"
                    @click="
                      FinishedOrder(
                        item.id,
                        item.TotalPrice,
                        item.IDRes,
                        item.IDCustomer,
                        item.OrderTime,
                        item.OrderConfirmedTime,
                        item.Url_image,
                        item.FoodName,
                        item.RestaurantName,
                        item.Quantity
                      )
                    "
                  >
                    Xác nhận
                  </button>
                  <div v-else>
                     Đang chờ...
                  </div>
                </td>
                <td>
                  <button class="delete-btn" @click="DeleteFFromOD(item.id)">
                    Xóa
                  </button>
                </td>
                <td>{{ formatDate(item.OrderTime) }}</td>
                <td v-if="item.Status_Restaurant === 'confirmed'">
                  {{ formatDate(item.OrderConfirmedTime) }}
                </td>
                <td v-else>Đang chờ...</td>
              </tr>
            </tbody>
          </table>
        </div>
      </div>
      </div>

      <div class="footer"></div>
    </div>
  </div>
</template>

<script setup>
import { FontAwesomeIcon } from "@fortawesome/vue-fontawesome";

import { ref, onMounted, onUnmounted, computed } from "vue";
import { useRouter, useRoute } from "vue-router";
import { ThongkeOrder } from "../api/order.js";
import { DeleteOrder } from "../api/order.js";
import { Thongke } from "../api/thongke.js";
import Swal from "sweetalert2";
import { emitter } from "../api/eventBus.js";
import OrderHistory from "../components/OrderHistory.vue";
import { ProductCount } from "../api/order.js";
const orderHistoryPopupOpen = ref(false);
const openHistory = ref(false);
const router = useRouter();
const orderCount = ref(0);
const isLoggedIn = ref(false);
const username = ref("");
const dropdownOpen = ref(false);
const searchQuery = ref("");
const formatDate = (datetime) => {
  return new Date(datetime).toLocaleString("vi-VN");
};
const thongkedata = ref([]);
const updateOrderCount = async () => {
  const IDUser = localStorage.getItem("IDRes");
  if (IDUser) {
    try {
      const count = await ProductCount(IDUser);
      orderCount.value = count;
    } catch (error) {
      console.error("Lỗi khi cập nhật orderCount:", error);
    }
  }
};

const emitSearch = () => {
  emitter.emit("searchItem", searchQuery.value);
};
const DeleteFFromOD = async (id) => {
  console.log(id);

  try {
    console.log(id);
    await DeleteOrder(id);
    await updateOrderCount();
    await getThongkeData();
    thongkedata.value = thongkedata.value.filter((p) => p.id !== id);

    console.log("Xoá thành công");
  } catch (error) {
    console.error("Xoá thất bại:", error);
    alert("Có lỗi xảy ra khi xoá món ăn.");
  }
};

const getThongkeData = async () => {
  const ID = localStorage.getItem("IDRes");
  try {
    if (!ID) {
      alert("Bạn chưa đăng nhập!");
      return;
    }
    const data = await ThongkeOrder(ID);
    thongkedata.value = data
      .map((item) => ({
        IDCustomer: item.idCustomer,
        IDRes: item.idRes,
        Status_Restaurant: item.status_Restaurant,
        OrderConfirmedTime: item.orderConfirmedTime,
        OrderTime: item.orderTime,
        id: item.idOrder,
        FoodName: item.foodName,
        RestaurantName: item.restaurantName,
        Quantity: item.quantity,
        TotalPrice: item.totalPrice,
        Url_image: item.url_image,
      }))
      .sort((a, b) => new Date(b.OrderTime) - new Date(a.OrderTime));
    console.log(thongkedata.value);
  } catch (error) {
    console.error("Lỗi khi lấy dữ liệu thống kê:", error);
    thongkedata.value = [];
  }
};

emitter.on("UpdateCountProduct", async () => {
  await updateOrderCount();
});
emitter.on("Ordercount", async () => {
  await updateOrderCount();
});

const openOrderHistoryPopup = async () => {
  orderHistoryPopupOpen.value = true;
  await getThongkeData();
};

const closeOrderHistoryPopup = () => {
  orderHistoryPopupOpen.value = false;
};
const openorderHistory = async () => {
  router.push("/orderHistory");
};

const toggleDropdown = () => {
  dropdownOpen.value = !dropdownOpen.value;
};

const handleLogout = () => {
  localStorage.removeItem("IDRes");
  localStorage.removeItem("Role");
  localStorage.removeItem("UserName");
  isLoggedIn.value = false;
  username.value = "";
  router.replace("/login");

};

const navigateTo = (path) => {
  if (path === "/foodlist") {
    searchQuery.value = "";
    emitter.emit("searchItem", "");
  }
  router.push(path);
};

const checkLogin = () => {
  const storedUsername = localStorage.getItem("UserName");
  isLoggedIn.value = !!storedUsername;
  username.value = storedUsername || "";
  const role=localStorage.getItem("Role");
  if(role==="Restaurant")
  {
    router.push("/restaurant/dashboard");
  }};
emitter.on("Login",checkLogin);
const FinishedOrder = async (
  idOrder,
  TotalPrice,
  IDRes,
  IDCustomer,
  OrderTime,
  OrderConfirmedTime,
  Url_image,
  FoodName,
  RestaurantName,
  Quantity
) => {
  console.log(
    idOrder,
    TotalPrice,
    IDRes,
    IDCustomer,
    OrderTime,
    OrderConfirmedTime,
    Url_image,
    FoodName,
    RestaurantName,
    Quantity
  );
  await DeleteOrder(idOrder);
  await updateOrderCount();
  const senddata = {
    Quantity: Quantity,
    IDRes: IDRes,
    DOANHSO: TotalPrice,
    IDOrder: idOrder,
    IDCustomer: IDCustomer,
    OrderTime: OrderTime,
    OrderConfirmedTime: OrderConfirmedTime,
    Url_Image: Url_image,
    FoodName: FoodName,
    RestaurantName: RestaurantName,
  };
  try {
    await Thongke(senddata);
    Swal.fire({
      toast: true,
      icon: "success",
      title: "THÔNG BÁO",
      text: "Xác nhận thành công!",
      timer: 3000,
      position: "bottom-end",
      timerProgressBar: true,
      showConfirmButton: false,
      showClass: {
        popup: "swal2-slide-in-right",
      },
    });
  } catch (error) {
    toast: true, console.error("Xác nhận thất bại:", error);
    Swal.fire({
      icon: "error",
      title: "Lỗi",
      position: "bottom-end",
      text: "Có lỗi xảy ra khi xác nhận!",
    });
  }
  await getThongkeData();

  thongkedata.value = thongkedata.value.filter((p) => p.id !== id);
};
onMounted(async () => {
  checkLogin();
  await updateOrderCount();
  console.log(orderCount);
  window.addEventListener("storage", checkLogin);
});
</script>

<style scoped>
* {
  font-family: "Segoe UI", Tahoma, Geneva, Verdana, sans-serif;
  box-sizing: border-box;
}

.container {
  width: 100vw;
  height: 100vh;
  background: url("https://assets.tronhouse.vn/59185068-4c44-404a-a5b6-493d1d50d13d/origin/chup-anh-mon-an-4.jpeg")
    no-repeat center center/cover;
}

.navbar {
  width: 100%;
  height: 70px;
  display: flex;
  justify-content: space-between;
  align-items: center;
  position: fixed;
  z-index: 10;
  background-color: rgba(0, 0, 0, 0.6);
  color: white;
  padding: 0 30px;
  backdrop-filter: blur(6px);
}

.navbar .logo {
  display: flex;
  align-items: center;
  font-size: 22px;
  font-weight: bold;
  gap: 10px;
}

.logo-food i {
  font-size: 24px;
  color: #ff7f50;
}

.Name {
  letter-spacing: 1px;
  color: white;
}

.home-page {
  display: flex;
  flex-wrap: wrap;
  gap: 20px;
  align-items: center;
  justify-content: space-between;
  padding: 20px;
}

.category-buttons {
  display: flex;
  gap: 12px;
}

.category {
  display: flex;
  align-items: center;
  gap: 8px;
  padding: 10px 20px;
  border: none;
  border-radius: 16px;
  font-size: 16px;
  font-weight: 600;
  color: white;
  cursor: pointer;
  transition: all 0.3s ease;
  box-shadow: 0 4px 12px rgba(0, 0, 0, 0.1);
}

.food {
  background: linear-gradient(to right, #ff7e5f, #feb47b);
}

.drink {
  background: linear-gradient(to right, #4facfe, #00f2fe);
}

.category:hover {
  transform: translateY(-2px);
  opacity: 0.95;
  box-shadow: 0 6px 20px rgba(0, 0, 0, 0.2);
}

.search-bar {
  position: relative;
  display: flex;
  align-items: center;
  width: 300px;
}

.search-input {
  width: 100%;
  padding: 10px 38px 10px 16px;
  border-radius: 12px;
  border: 1px solid #ccc;
  font-size: 15px;
  transition: 0.3s;
}

.search-input:focus {
  outline: none;
  border-color: #007bff;
  box-shadow: 0 0 0 2px rgba(0, 123, 255, 0.2);
}

.search-icon {
  position: absolute;
  right: 12px;
  font-size: 18px;
  color: #666;
  pointer-events: none;
}

.login {
  position: relative;
  display: flex;
  align-items: center;
  gap: 10px;
  cursor: pointer;
  right: 10px;
  left: auto;
}

.login-button {
  width: 50px;
  height: 50px;
  display: flex;
  align-items: center;
  justify-content: center;
  border: none;
  border-radius: 50%;
  background-color: transparent;
  color: white;
  font-size: 20px;
  font-weight: bold;
  cursor: pointer;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.2);
}

.login-button:hover {
  background: linear-gradient(135deg, #ff5722, #ff7f50);
  transform: scale(1.05);
}

.boder-login {
  position: absolute;
  top: 110%;
  left: 50%;
  transform: translateX(-50%);
  display: flex;
  flex-direction: column;
  gap: 6px;
  align-items: center;
  padding: 14px;
  width: 180px;
  background: rgba(255, 255, 255, 0.25);
  backdrop-filter: blur(12px);
  border-radius: 12px;
  border: 1px solid rgba(255, 255, 255, 0.3);
  box-shadow: 0px 6px 16px rgba(0, 0, 0, 0.2);
  opacity: 0;
  visibility: hidden;
  transition: 0.3s ease-in-out;
  z-index: 20;
}

.login:hover .boder-login {
  opacity: 1;
  visibility: visible;
}

.loginn,
.register {
  text-decoration: none;
  font-size: 15px;
  padding: 8px 12px;
  border-radius: 6px;
  transition: 0.3s;
  width: 100%;
  text-align: center;
  font-weight: 500;
}

.loginn {
  color: #6200ea;
  border: 1px solid #6200ea;
  background: transparent;
}

.loginn:hover {
  background: #6200ea;
  color: white;
}

.register {
  color: white;
  background: #ff7f50;
  border: none;
}

.register:hover {
  background: #e66a3e;
}

.user-menu {
  position: relative;
  cursor: pointer;
}

.user-info {
  display: flex;
  align-items: center;
  gap: 10px;
  padding: 8px 12px;
  border-radius: 8px;
  font-weight: bold;
  transition: 0.3s;
}

.user-info:hover {
  background-color: rgba(255, 255, 255, 0.1);
}

.avatar {
  width: 42px;
  height: 42px;
  border-radius: 50%;
  background: linear-gradient(135deg, #007bff, #0056b3);
  color: white;
  font-size: 18px;
  display: flex;
  align-items: center;
  justify-content: center;
  box-shadow: 0 2px 6px rgba(0, 0, 0, 0.2);
}

.dropdown-menu {
  position: absolute;
  top: 100%;
  right: 0;
  background: rgba(255, 255, 255, 0.95);
  border-radius: 12px;
  width: 260px;
  box-shadow: 0 10px 25px rgba(0, 0, 0, 0.15);
  z-index: 100;
  overflow: hidden;
  padding: 10px 0;
  backdrop-filter: blur(8px);
}

.dropdown-item {
  display: flex;
  align-items: center;
  gap: 12px;
  padding: 14px 20px;
  font-size: 15px;
  font-weight: 500;
  color: #333;
  background-color: transparent;
  border: none;
  width: 100%;
  text-align: left;
  cursor: pointer;
  transition: background-color 0.3s, color 0.3s;
}

.dropdown-item:hover {
  background-color: #f0f2f5;
  color: #007bff;
}

.logout {
  color: #d32f2f;
}

.logout:hover {
  background-color: rgba(211, 47, 47, 0.1);
  color: #b71c1c;
}

.content {
  width: 100%;
  height: calc(100vh - 70px);
  transform: translateY(70px);
  padding: 20px;
  overflow-y: auto;
  background: rgba(255, 255, 255, 0.1);
}

.popup {
  position: fixed;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
  background: rgb(252, 238, 220);
  border-radius: 12px;
  box-shadow: 0 4px 20px rgb(0, 0, 0);
  z-index: 1000;
  width: 90vw;
  max-width: 1000px;
}

.popup-content {
  padding: 24px;
  max-height: 80vh;
  overflow-y: auto;
}
.popup-title {
  font-size: 20px;
  font-weight: bold;
  color: #222;
  margin-bottom: 16px;
  text-align: center;
}

@keyframes fadeIn {
  from {
    transform: translateY(-20px);
    opacity: 0;
  }
  to {
    transform: translateY(0);
    opacity: 1;
  }
}

.close-button {
  position: absolute;
  top: 12px;
  right: 12px;
  background: transparent;
  border: none;
  font-size: 24px;
  color: #999;
  cursor: pointer;
  transition: color 0.3s ease;
}

.close-button:hover {
  color: #ff424e;
}

.food-image {
  width: 80px;
  height: auto;
  border-radius: 8px;
}

.order-table {
  width: 100%;
  border-collapse: collapse;
  font-size: 14px;
  color: #000000;
}

.order-table th {
  background-color: #cecaca;
  font-weight: 500;
  padding: 12px;
  border-bottom: 1px solid #eee;
  text-align: center;
  vertical-align: middle;
}

.order-table td {
  text-align: center;
  vertical-align: middle;

  padding: 12px;
  border-top: 1px solid #f0f0f0;
  vertical-align: top;
}

.confirm-btn {
  background-color: #0f9800;
  color: white;
  border: none;
  padding: 6px 12px;
  border-radius: 4px;
  cursor: pointer;
  font-size: 13px;
  transition: background 0.3s ease;

  white-space: nowrap; /* 👉 NGĂN chữ xuống dòng */
}

.delete-btn {
  display: inline-block;
  background-color: #f44336;
  color: white;
  border: none;
  padding: 6px 12px;
  border-radius: 4px;
  cursor: pointer;
  font-size: 13px;
  transition: background 0.3s ease;
}

.delete-btn:hover {
  background-color: #d32f2f;
}

.swal2-slide-in-right {
  animation: slide-in-right 0.5s ease-out;
}
.badge {
  background-color: red;
  color: white;
  border-radius: 50%;
  padding: 2px 6px;
  font-size: 12px;
  margin-left: 6px;
  font-weight: bold;
}

@keyframes slide-in-right {
  from {
    transform: translateX(100%);
    opacity: 0;
  }
  to {
    transform: translateX(0);
    opacity: 1;
  }
}

.footer {
  /* Nếu sau này bạn có thêm nội dung footer, giữ lại class này */
}
</style>

<template>
  <div>
    <!-- Kiểm tra nếu không phải trang restaurant/dashboard thì mới hiển thị Navbar -->
    <div class="container" v-if="!$route.path.includes('/restaurant/dashboard')">
      <div class="navbar">
        <div class="logo">
          <div class="logo-food">
          </div>
          <div class="Name">GRAP FOOD</div>
        </div>

        <div class="home-page">
          <!-- Nút điều hướng đến các route -->
          <button class="category food" @click="navigateTo('/foodlist')">🍔 Đồ ăn</button>
          <button class="category drink" @click="navigateTo('/drinklist')">🥤 Đồ uống</button>
          <input type="text" placeholder="Tìm kiếm món ăn..." class="search-input">
        </div>

        <!-- Kiểm tra trạng thái đăng nhập -->
        <div v-if="isLoggedIn" class="user-menu">
          <div class="user-info" @click="toggleDropdown">
            <div class="avatar">{{ username.charAt(0).toUpperCase() }}</div>
            <span class="username">{{ username }}</span>
          </div>
          <div v-if="dropdownOpen" class="dropdown-menu">
            <!-- Sửa thành button thay vì router-link -->
            <button @click="openOrderHistoryPopup" class="dropdown-item">📦 Lịch sử đơn đặt hàng</button>
            <button @click="handleLogout" class="dropdown-item logout">🚪 Đăng xuất</button>
          </div>
        </div>

        <!-- Nếu chưa đăng nhập thì hiển thị phần đăng nhập -->
     <!-- Nếu chưa đăng nhập thì hiển thị phần đăng nhập -->
<div v-else class="login">
  <button class="login-button">
     <font-awesome-icon icon="user" style="font-size: 20px; color: white;"/> 
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
      <div v-if="orderHistoryPopupOpen" class="popup">
  <div class="popup-content" style="max-height: 80vh; overflow-y: auto;">
    <h2>Lịch sử đơn đặt hàng</h2>
   <button class="close-button" @click="closeOrderHistoryPopup" aria-label="Đóng popup">
  <span>&times;</span>
</button>

    <div class="card-list">
      <div class="card" v-for="(item, index) in thongkedata" :key="index">
        <h3>{{ item.FoodName }}</h3>
        <p><strong>Nhà hàng:</strong> {{ item.RestaurantName }}</p>
        <img :src="item.Url_image" alt="Ảnh món ăn" style="width: 120px; height: auto; border-radius: 8px;" />
        <p><strong>Số lượng:</strong> {{ item.Quantity }}</p>
        <p><strong>Tổng tiền:</strong> {{ item.TotalPrice.toLocaleString() }} VNĐ</p>
        <button @click="DeleteFFromOD(item.id)">Xóa đơn đặt hàng</button>
      </div>
    </div>
  </div>
</div>

      <div class="footer"></div>
    </div>
  </div>
</template>

<script setup>
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome'
import { faUser } from '@fortawesome/free-solid-svg-icons'
import { library } from '@fortawesome/fontawesome-svg-core'
import { ref, onMounted } from "vue";
import { useRouter, useRoute } from "vue-router";
import { ThongkeOrder } from "../api/order.js";
import {DeleteOrder} from "../api/order.js"
const orderHistoryPopupOpen = ref(false);
const router = useRouter();
const route = useRoute();
const isLoggedIn = ref(false);
const username = ref("");
const dropdownOpen = ref(false);


const thongkedata = ref([]);
const ID = localStorage.getItem("IDRes");
const DeleteFFromOD=async(id)=>{
console.log(id);
  try {    console.log(id);
      await DeleteOrder(id);
      await getThongkeData();
thongkedata.value = thongkedata.value.filter(p => p.id !== id);

      console.log("Xoá thành công");
    } catch (error) {
      console.error("Xoá thất bại:", error);
      alert("Có lỗi xảy ra khi xoá món ăn.");
    }
  }
// Hàm lấy thống kê từ API
const getThongkeData = async () => {
  try {
    if (!ID) {
      alert("Bạn chưa đăng nhập!");
      return;
    }
    const data = await ThongkeOrder(ID);
    thongkedata.value = data.map(item => ({
      id: item.idOrder,
      FoodName: item.foodName,
      RestaurantName: item.restaurantName,
      Quantity: item.quantity,
      TotalPrice: item.totalPrice,
      Url_image: item.url_image
    }));
    console.log(thongkedata.value);
  } catch (error) {
    console.error("Lỗi khi lấy dữ liệu thống kê:", error);
  }
};

// Mở popup và gọi API
const openOrderHistoryPopup = async () => {
  orderHistoryPopupOpen.value = true;
  await getThongkeData();
};

const closeOrderHistoryPopup = () => {
  orderHistoryPopupOpen.value = false;
};

const toggleDropdown = () => {
  dropdownOpen.value = !dropdownOpen.value;
};

const handleLogout = () => {
  localStorage.removeItem("IDRes");
  localStorage.removeItem("role");
  localStorage.removeItem("username");
  isLoggedIn.value = false;
  username.value = "";
  router.replace("/login");
};

const navigateTo = (path) => {
  router.push(path);
};

const checkLogin = () => {
  const storedUsername = localStorage.getItem("username");
  isLoggedIn.value = !!storedUsername;
  username.value = storedUsername || "";
};

onMounted(() => {
  checkLogin();
  window.addEventListener("storage", checkLogin);
});
</script>


<style scoped>

* {
  font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
  box-sizing: border-box;
}

 .close-button {
  position: absolute;
  top: 10px;
  right: 10px;
  background: transparent;
  border: none;
  font-size: 22px;
  font-weight: bold;
  cursor: pointer;
  z-index: 1000;
  color: #333;
  transition: 0.2s ease-in-out;
}

.close-button:hover {
  color: #e53935;
  transform: scale(1.2);
}

.popup-content {
   position: relative;
  background: #fff;
  border-radius: 16px;
  padding: 30px;
  width: 800px;
  max-height: 85vh;
  overflow-y: auto;  /* Cho phép cuộn */
  overflow-x: hidden;
  text-align: center;
  box-shadow: 0 10px 30px rgba(0, 0, 0, 0.25);
  animation: fadeIn 0.3s ease-in-out;
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

.popup {
  position: fixed;
  top: 0;
  left: 0;
  width: 100vw;
  height: 100vh;
  background: rgba(0, 0, 0, 0.4);
  display: flex;
  align-items: center;
  justify-content: center;
  z-index: 999;
}

.container {
  width: 100vw;
  height: 100vh;
  background: url("https://watermark.lovepik.com/photo/20211118/large/lovepik-gourmet-background-picture_400152283.jpg") no-repeat center center/cover;

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
  gap: 15px;
  align-items: center;
}

.category {
  padding: 10px 18px;
  border-radius: 8px;
  border: none;
  cursor: pointer;
  font-size: 16px;
  font-weight: 500;
  transition: 0.3s;
  box-shadow: 0 2px 5px rgba(0,0,0,0.1);
}

.food {
  background: linear-gradient(to right, #ff7e5f, #feb47b); /* Cam đào */
  border: none;
  color: white;
  padding: 9px 28px;
  font-size: 16px;
  font-weight: 600;
  border-radius: 12px;
  box-shadow: 0 4px 12px rgba(0, 0, 0, 0.15);
  transition: all 0.3s ease;
}

.drink {
  background: linear-gradient(to right, #4facfe, #00f2fe); /* Xanh nước biển */
  border: none;
  color: white;
  padding: 9px 28px;
  font-size: 16px;
  font-weight: 600;
  border-radius: 12px;
  box-shadow: 0 4px 12px rgba(0, 0, 0, 0.15);
  transition: all 0.3s ease;
}

.food:hover, .drink:hover {
  transform: translateY(-2px);
  box-shadow: 0 8px 24px rgba(0, 0, 0, 0.2);
}





.category:hover {
  transform: scale(1.05);
  opacity: 0.9;
}
.search-input {
  padding: 8px 14px;
  font-size: 16px;
  border: none;
  border-radius: 8px;
  outline: none;
  width: 280px;
  box-shadow: 0 2px 6px rgba(0,0,0,0.15);
  transition: 0.3s;
}
.search-input:focus {
  border: 2px solid #ff7f50;
}
/* Login */
.login {
  position: relative;
  display: flex;
  align-items: center;
  gap: 10px;
  cursor: pointer;
  right: 10px;
  left: auto;
}
/* Nút Nhà Hàng */
.login-button {
  width: 50px;
  height: 50px;
  display: flex;
  align-items: center;
  justify-content: center;
  gap: 8px;
  border: none;
  border-radius: 50%;
  padding: 0;
  background-color: transparent; /* Nền trong suốt */
  color: white; /* Icon màu trắng */
  font-size: 20px;
  font-weight: bold;
  cursor: pointer;
  box-shadow: 0 4px 8px rgba(0,0,0,0.2);
  transition: 0.3s;
}

.login-button:hover {
  background-color: rgba(255, 255, 255, 0.1); /* Khi hover thì hơi mờ nhẹ cho đẹp */
}

.login-button:hover {
  background-color: #2980b9; /* Hover màu đậm hơn chút */
}


.login-button i {
  font-size: 20px;
}

.login-button:hover {
  background: linear-gradient(135deg, #ff5722, #ff7f50);
  transform: scale(1.05);
}

/* Hover box */
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

/* Link login/register */
.loginn, .register {
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



/* User menu */
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
  transition: 0.3s;
  font-weight: bold;
}

.user-info:hover {
  background-color: rgba(255,255,255,0.1);
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
  box-shadow: 0 2px 6px rgba(0,0,0,0.2);
}

/* Dropdown */
/* Dropdown */
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
  font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
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

/* Content */
.content {
  width: 100%;
  height: calc(100vh - 70px);
  transform: translateY(70px);
  padding: 20px;
  overflow-y: auto;
  background: rgba(255,255,255,0.1);
  backdrop-filter:none;
}
.card-list {
  display: grid;
  grid-template-columns: repeat(auto-fill, minmax(350px, 1fr));
  gap: 20px;
  margin-top: 20px;
}

.card {
  padding: 16px;
  border-radius: 12px;
  background: linear-gradient(135deg, #fff8f0, #fefefe);
  box-shadow: 0 4px 12px rgba(0,0,0,0.1);
  text-align: left;
  transition: transform 0.2s ease;
}

.card:hover {
  transform: scale(1.02);
}

.card img {
  margin-top: 10px;
  border-radius: 10px;
  box-shadow: 0 2px 6px rgba(0,0,0,0.15);
  width: 100%;
  max-width: 200px;
  height: auto;
}
 </style>

<template>
  <div>
    <!-- Kiểm tra nếu không phải trang restaurant/dashboard thì mới hiển thị Navbar -->
    <div class="container" v-if="!$route.path.includes('/restaurant/dashboard')">
      <div class="navbar">
        <div class="logo">
          <div class="logo-food">
            <i class="fa-solid fa-utensils"></i>  
          </div>
          <div class="Name">GRAP FOOD</div>
        </div>
        <div class="home-page">
          <button class="category food">🍔 Đồ ăn</button>
          <button class="category drink">🥤 Đồ uống</button>
          <input type="text" placeholder="Tìm kiếm món ăn..." class="search-input">
        </div>

        <!-- Kiểm tra trạng thái đăng nhập -->
        <div v-if="isLoggedIn" class="user-menu">
          <div class="user-info" @click="toggleDropdown">
            <div class="avatar">{{ username.charAt(0).toUpperCase() }}</div>
            <span class="username">{{ username }}</span>
          </div>
          <div v-if="dropdownOpen" class="dropdown-menu">
            <router-link to="/order-history" class="dropdown-item">📦 Lịch sử đơn đặt hàng</router-link>
            <button @click="handleLogout" class="dropdown-item logout">🚪 Đăng xuất</button>
          </div>
        </div>

        <!-- Nếu chưa đăng nhập thì hiển thị phần đăng nhập -->
        <div v-else class="login">
          <i class="fas fa-user"></i>
          <div class="boder-login">
            <router-link to="/login" class="loginn">Đăng nhập</router-link>
            <router-link to="/register" class="register">Đăng ký</router-link>
          </div>
        </div>
      </div>
    </div>

    <!-- Nội dung chính của trang -->
    <router-view></router-view>
    
  </div>
</template>

<script setup>
import { ref, watchEffect, watch } from "vue";
import { useRouter, useRoute } from "vue-router";

const router = useRouter();
const route = useRoute();
const isLoggedIn = ref(false);
const username = ref("");
const dropdownOpen = ref(false);

const toggleDropdown = () => {
  dropdownOpen.value = !dropdownOpen.value;
};

const handleLogout = () => {
  localStorage.removeItem("role");
  localStorage.removeItem("username");
  isLoggedIn.value = false;
  username.value = "";
  router.replace("/login");
};

// Lấy dữ liệu user từ localStorage khi app khởi động
const fetchUserData = () => {
  const storedUsername = localStorage.getItem("username");
  isLoggedIn.value = !!storedUsername;
  username.value = storedUsername || "";
};

// Theo dõi thay đổi trong localStorage
watchEffect(fetchUserData);

// Theo dõi thay đổi của route để cập nhật dữ liệu khi chuyển trang
watch(route, () => {
  fetchUserData();
});

</script>

<style scoped>
.container {
  position: relative;
  width: 100vw;
  height: 100vh;
  background: url("https://watermark.lovepik.com/photo/20211118/large/lovepik-gourmet-background-picture_400152283.jpg") no-repeat center center/cover;
}
.navbar {
  width: 100%;
  height: 70px;
  display: flex;
  justify-content: space-around;
  align-items: center;
  position: fixed;
  z-index: 10;
  background-color: rgba(0, 0, 0, 0.5);
  color: white;
  padding: 10px;
}

.navbar .logo {
  width: 100%;
  display: flex;
  width: 150px;
  justify-content: space-around;
  font-size: 20px;
}

.home-page {
  display: flex;
  gap: 15px;
  align-items: center;
}

.category {
  padding: 10px 15px;
  border-radius: 8px;
  border: none;
  cursor: pointer;
  font-size: 16px;
  transition: 0.3s;
}

.food {
  background-color: #ff7f50; /* Màu cam */
  color: white;
}

.drink {
  background-color: #4682b4; /* Màu xanh dương */
  color: white;
}

.category:hover {
  opacity: 0.8;
}

.search-input {
  padding: 8px;
  font-size: 16px;
  border: 2px solid #ccc;
  border-radius: 5px;
  outline: none;
  width: 200px;
}

.search-input:focus {
  border-color: #ff7f50;
}

/* Định vị khu vực đăng nhập */
/* Vùng chứa icon đăng nhập */
.login {
  position: relative;
  display: flex;
  align-items: center;
  gap: 10px;
  cursor: pointer;
}

.login i {
  font-size: 24px;
  color: #6200ea;
  transition: 0.3s;
}

/* Hộp "Đăng nhập / Đăng ký" */
.boder-login {
  position: absolute;
  top: 100%;
  left: 50%;
  transform: translateX(-50%);
  display: flex;
  flex-direction: column;
  gap: 6px;
  align-items: center;
  padding: 12px 15px;
  width: 160px; /* Giảm độ rộng để thon hơn */
  
  /* Nền mờ giúp nhìn thấy ảnh nền */
  background: rgba(255, 255, 255, 0.2); 
  backdrop-filter: blur(10px); /* Làm mờ nền */
  border-radius: 12px;
  border: 1px solid rgba(255, 255, 255, 0.4);
  box-shadow: 0px 4px 12px rgba(0, 0, 0, 0.15);
  
  opacity: 0;
  visibility: hidden;
  transition: 0.3s ease-in-out;
  z-index: 10;
}

/* Khi hover vào icon, hiện hộp */
.login:hover .boder-login {
  opacity: 1;
  visibility: visible;
}

/* Nút đăng nhập / đăng ký */
.loginn,
.register {
  text-decoration: none;
  font-size: 14px;
  padding: 6px 12px;
  border-radius: 6px;
  transition: 0.3s;
  width: 100%;
  text-align: center;
  font-weight: 500;
}

/* Nút đăng nhập */
.loginn {
  color: #6200ea;
  border: 1px solid #6200ea;
  background: transparent;
}

.loginn:hover {
  background: #6200ea;
  color: white;
}

/* Nút đăng ký */
.register {
  color: white;
  background: #ff7f50;
  border: 1px solid #ff7f50;
}

.register:hover {
  background: #e66a3e;
  border-color: #e66a3e;
}

.user-menu {
  cursor: pointer;
  position: relative;
}

/* Vùng chứa avatar + tên */
.user-info {
  display: flex;
  align-items: center;
  gap: 10px;
  font-weight: bold;
  padding: 8px 12px;
  border-radius: 8px;
  transition: background 0.3s ease;
}

.user-info:hover {
  background: rgba(0, 123, 255, 0.1);
}

.avatar {
  width: 40px;
  height: 40px;
  background: linear-gradient(135deg, #007bff, #0056b3);
  color: white;
  border-radius: 50%;
  display: flex;
  align-items: center;
  justify-content: center;
  font-size: 18px;
  font-weight: bold;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.2);
}

/* Dropdown menu */
.dropdown-menu {
  left: 15px;
  position: absolute;
  top: 50px;
  right: 0;
  background: white;
  border-radius: 10px;
  width: 250px;
  box-shadow: 0 4px 12px rgba(0, 0, 0, 0.15);
  overflow: hidden;
  opacity: 0;
  transform: translateY(-10px);
  transition: opacity 0.3s ease, transform 0.3s ease;
  pointer-events: none;
}

/* Khi hover vào user-menu thì hiển thị dropdown */
.user-menu:hover .dropdown-menu {
  opacity: 1;
  transform: translateY(0);
  pointer-events: auto;
}

/* Item trong dropdown */
.dropdown-item {
  display: flex;
  align-items: center;
  gap: 12px;
  padding: 12px 16px;
  font-size: 15px;
  font-weight: 500;
  color: #333;
  text-decoration: none;
  transition: background 0.2s ease, color 0.2s ease;
  cursor: pointer;
}

.dropdown-item i {
  font-size: 18px;
  color: #007bff;
}

/* Hover */
.dropdown-item:hover {
  background: #f0f2f5;
}

/* Divider (đường kẻ ngăn cách) */
.divider {
  height: 1px;
  background: #e0e0e0;
  margin: 5px 10px;
}

/* Logout thiết kế đẹp */
.logout {
  width: 100%;
  border: none;
  color: red;
  font-weight: 500;
}

.logout i {
  color: red;
}

.logout:hover {
  background: rgba(255, 0, 0, 0.1);
  color: red;
}


</style>

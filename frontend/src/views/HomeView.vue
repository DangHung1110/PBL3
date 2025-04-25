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

      <div class="content">
        <router-view></router-view>
      </div>

      <div class="footer"></div>
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted } from "vue";
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

// Điều hướng đến trang đồ ăn hoặc đồ uống
const navigateTo = (path) => {
  router.push(path);
};

// Hàm kiểm tra đăng nhập
const checkLogin = () => {
  const storedUsername = localStorage.getItem("username");
  isLoggedIn.value = !!storedUsername;
  username.value = storedUsername || "";
};

// Khi có thay đổi trong localStorage, cập nhật UI ngay lập tức
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

.container {
  width: 100vw;
  height: 100vh;
  background: url("https://watermark.lovepik.com/photo/20211118/large/lovepik-gourmet-background-picture_400152283.jpg") no-repeat center center/cover;
  overflow: hidden;
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
  background-color: #ff7f50;
  color: white;
}

.drink {
  background-color: #4682b4;
  color: white;
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
}

.login i {
  font-size: 24px;
  color: white;
}

/* Hover box login */
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
.dropdown-menu {
  position: absolute;
  top: 120%;
  right: 0;
  background: white;
  border-radius: 10px;
  width: 240px;
  box-shadow: 0 8px 16px rgba(0,0,0,0.2);
  transform: translateY(0); /* Không dùng animation khi mở */
  opacity: 1;               /* Mặc định là hiển thị, điều khiển bằng v-if */
  pointer-events: auto;
  transition: 0.3s;
  overflow: hidden;
}



.dropdown-item {
  display: flex;
  align-items: center;
  gap: 12px;
  padding: 12px 18px;
  font-size: 15px;
  color: #333;
  text-decoration: none;
  transition: background 0.2s;
}

.dropdown-item:hover {
  background: #f0f2f5;
}

.logout {
  border: none;
  background: none;
  color: red;
  width: 100%;
  text-align: left;
}

.logout:hover {
  background: rgba(255,0,0,0.1);
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
 </style>

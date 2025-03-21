<template>
  <div class="dashboard-container">
    <!-- Sidebar -->
    <aside class="sidebar">
      <div class="logo">
        <div class="logo-food">
          <i class="fa-solid fa-utensils"></i>  
        </div>
        <div class="Name">GRAP FOOD</div>
      </div>
      <nav class="menu">
        <router-link to="/restaurant/dashboard">Trang chủ</router-link>
        <router-link to="/restaurant/orders">Sản phẩm</router-link>
        <router-link to="/restaurant/invoices">Hóa đơn</router-link>
        <router-link to="/restaurant/statistics">Thống kê</router-link>
      </nav>
    </aside>

    <!-- Main Content -->
    <div class="main-content">
      <!-- Header -->
      <header class="header">
        <div class="menu-toggle">&#9776;</div>
        <div class="user-info">
        <button class="logout-button" @click="handleLogout">Đăng xuất</button>
        </div>
      </header>

      <!-- Dashboard Content -->
      <div class="dashboard-overview">
        <div class="overview-card yellow"> Đặt hàng</div>
        <div class="overview-card blue"> Đang ship</div>
        <div class="overview-card purple"> Tổng bài viết</div>
      </div>

      <h2>Thu nhập 30 ngày gần đây nhất</h2>
      <div class="chart-placeholder">Biểu đồ</div>
    </div>
  </div>
</template>

<script setup>
import { useRouter } from 'vue-router';

const router = useRouter();

const handleLogout = () => {
  localStorage.removeItem("role");
  localStorage.removeItem("username");
  router.replace("/login"); 
};
</script>


<style>
.dashboard-container {
  display: flex;
  height: 100vh;
}

.sidebar {
  width: 250px;
  background: #f8f9fa;
  padding: 20px;
  display: flex;
  flex-direction: column;
}

.sidebar .logo {
  display: flex;
  gap: 10px;
  font-size: 20px;
  font-weight: bold;
  margin-bottom: 20px;
}

.sidebar .menu {
  display: flex;
  flex-direction: column;
  justify-content: space-evenly;
}

.sidebar .menu a {
  display: block;
  padding: 10px;
  text-decoration: none;
  color: black;
  margin: 5px 0;
}

.main-content {
  flex: 1;
  padding: 20px;
  background: #fff;
}

.header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 10px;
  border-bottom: 1px solid #ddd;
}

.user-info {
  display: flex;
  align-items: center;
  gap: 15px;
}

.badge {
  background: red;
  color: white;
  border-radius: 50%;
  padding: 2px 6px;
  font-size: 12px;
}

.dashboard-overview {
  display: flex;
  gap: 15px;
  margin: 20px 0;
}

.overview-card {
  flex: 1;
  padding: 20px;
  color: white;
  border-radius: 8px;
  text-align: center;
}

.yellow { background: #f4b400; }
.blue { background: #4285f4; }
.purple { background: #7e57c2; }
.violet { background: #ab47bc; }

.chart-placeholder {
  background: #ddd;
  height: 300px;
  display: flex;
  align-items: center;
  justify-content: center;
}
</style>
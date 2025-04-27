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
        <router-link to="/restaurant/dashboard/product">Sản phẩm</router-link>
        <router-link to="/restaurant/dashboard/invoices">Hóa đơn</router-link>
        <router-link to="/restaurant/dashboard/statistics">Thống kê</router-link>
      </nav>
    </aside>

    <!-- Main Content -->
    <div class="main-content">
      <router-view></router-view>
    </div>
  </div>
</template>

<script setup>

</script>
<style scoped>
.dashboard-container {
  display: flex;
  height: 100vh;
  background: #f0f2f5;
}

.sidebar {
  width: 260px;
  background: #ffffff;
  padding: 30px 20px;
  display: flex;
  flex-direction: column;
  box-shadow: 2px 0 8px rgba(0, 0, 0, 0.1);
  border-top-right-radius: 16px;
  border-bottom-right-radius: 16px;
}

.logo {
  display: flex;
  align-items: center;
  gap: 10px;
  font-size: 24px;
  font-weight: bold;
  color: #4caf50;
  margin-bottom: 40px;
}

.logo-food {
  font-size: 28px;
}

.Name {
  font-family: 'Poppins', sans-serif;
  letter-spacing: 1px;
}

.menu {
  display: flex;
  flex-direction: column;
  gap: 15px;
}

.menu a {
  display: block;
  padding: 12px 20px;
  text-decoration: none;
  color: #555;
  font-weight: 500;
  border-radius: 12px;
  transition: all 0.3s ease;
}

.menu a:hover {
  background-color: #4caf50;
  color: white;
}

.main-content {
  flex: 1;
  padding: 40px;
  overflow-y: auto;
}
</style>

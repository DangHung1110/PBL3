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
      <!-- Nội dung sẽ được thay thế bởi router-view -->
      <router-view></router-view> <!-- Khi chuyển sang /product sẽ hiển thị trang sản phẩm -->
    </div>
  </div>
</template>

<script setup>

</script>

<style scoped>
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

</style>

<template>
    <!-- Header -->
    <header class="header">
      <div class="menu-toggle">&#9776;</div>
      <div class="user-info">
        <button class="logout-button" @click="handleLogout">Đăng xuất</button>
      </div>
    </header>
  
    <!-- Dashboard Content -->
    <div class="main-content">
      <div class="dashboard-overview">
        <div class="overview-card yellow" @click="viewOderDetail">Đặt hàng</div>
        <!-- <div class="overview-card blue">Đang ship</div>
        <div class="overview-card purple">Tổng bài viết</div> -->
      </div>
  
      <h2>Thu nhập 30 ngày gần đây nhất</h2>
      <div class="chart-placeholder">Biểu đồ</div>
    </div>
  </template>
  
  <script setup>
  import { useRouter } from 'vue-router';
  
  // Lấy router từ Vue Router
  const router = useRouter();
  
  // Hàm đăng xuất
  const handleLogout = () => {
    localStorage.removeItem("role");
    localStorage.removeItem("username");
    router.replace("/login");  // Điều hướng về trang đăng nhập
  };

  const viewOderDetail = () => {
    router.push({ name: 'RestaurantOrderList' }); // Điều hướng đến trang chi tiết đơn hàng
  };

  </script>
  
  <style>
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
  
  .logout-button {
    background: #dc3545;
    color: white;
    padding: 8px 16px;
    border-radius: 6px;
    cursor: pointer;
  }
  
  .logout-button:hover {
    background: #c82333;
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
  
  .yellow {
    background: #f4b400;
  }
  
  .blue {
    background: #4285f4;
  }
  
  .purple {
    background: #7e57c2;
  }
  
  .chart-placeholder {
    background: #ddd;
    height: 450px;
    display: flex;
    align-items: center;
    justify-content: center;
  }
  </style>
  
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
         <OrderList />
      </div>
  
      <h2>Thu nhập 30 ngày gần đây nhất</h2>
      <div class="chart-placeholder">Biểu đồ</div>
    </div>
  </template>
  
  <script setup>
  import { useRouter } from 'vue-router';
  import OrderList from './RestaurantOderList.vue';
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
  
  <style scoped>
/* Header */
.header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 16px 24px;
  background-color: #ff8818; /* màu đỏ Shopee */
  color: white;
  box-shadow: 0 2px 6px rgba(0, 0, 0, 0.08);
  font-size: 16px;
  font-weight: 500;
}

.menu-toggle {
  font-size: 24px;
  cursor: pointer;
}

/* User Info */
.user-info {
  display: flex;
  align-items: center;
  gap: 16px;
}

/* Logout Button */
.logout-button {
  background-color: white;
  color: #f94c43;
  border: 1.5px solid #f94c43;
  padding: 8px 18px;
  font-size: 14px;
  font-weight: 600;
  border-radius: 6px;
  cursor: pointer;
  transition: 0.2s ease;
}

.logout-button:hover {
  background-color: #fef0ef;
}

/* Main Content */
.main-content {
  padding: 30px 24px;
  background: #f5f5f5;
  min-height: 100vh;
  font-family: 'Segoe UI', 'Poppins', sans-serif;
}

/* Dashboard Overview */
.dashboard-overview {
  display: flex;
  flex-wrap: wrap;
  gap: 16px;
  margin-bottom: 32px;
}

.overview-card {
  flex: 1;
  min-width: 260px;
  background: white;
  padding: 20px;
  color: #444;
  border-radius: 12px;
  text-align: center;
  font-size: 18px;
  font-weight: 600;
  box-shadow: 0 1px 6px rgba(0, 0, 0, 0.05);
  border: 1px solid #eee;
  transition: all 0.2s ease;
}

.overview-card:hover {
  box-shadow: 0 4px 12px rgba(0, 0, 0, 0.08);
  transform: translateY(-2px);
}

/* Card Colors */
.yellow {
  background: linear-gradient(135deg, #ffe57f, #ff9a8b);
  color: #4a4a4a;
}

.blue {
  background: linear-gradient(135deg, #b2ebf2, #81d4fa);
  color: #2c3e50;
}

.purple {
  background: linear-gradient(135deg, #e1bee7, #ce93d8);
  color: #3f3f3f;
}

/* Section Title */
h2 {
  font-size: 20px;
  color: #333;
  margin-bottom: 20px;
  font-weight: 600;
}

/* Chart Placeholder */
.chart-placeholder {
  background: white;
  height: 400px;
  display: flex;
  align-items: center;
  justify-content: center;
  border-radius: 12px;
  box-shadow: 0 1px 6px rgba(0, 0, 0, 0.05);
  font-size: 20px;
  color: #888;
  font-weight: 500;
  border: 1px solid #e0e0e0;
}

/* Responsive */
@media (max-width: 768px) {
  .dashboard-overview {
    flex-direction: column;
  }

  .overview-card {
    font-size: 16px;
    padding: 16px;
  }

  .logout-button {
    font-size: 13px;
    padding: 6px 14px;
  }
}

</style>

  
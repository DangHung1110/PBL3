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
  padding: 18px 30px;
  background-color: #2c3e50;
  color: white;
  box-shadow: 0 4px 10px rgba(0, 0, 0, 0.1);
}

.menu-toggle {
  font-size: 28px;
  cursor: pointer;
}

/* User Info */
.user-info {
  display: flex;
  align-items: center;
  gap: 20px;
}

/* Logout Button */
.logout-button {
  background: linear-gradient(135deg, #ff4b2b, #ff416c);
  border: none;
  padding: 10px 20px;
  color: white;
  font-weight: 600;
  border-radius: 12px;
  cursor: pointer;
  font-size: 16px;
  transition: background 0.3s, transform 0.2s;
}

.logout-button:hover {
  background: linear-gradient(135deg, #ff416c, #ff4b2b);
  transform: scale(1.05);
}

/* Main Content */
.main-content {
  flex: 1;
  padding: 40px 30px;
  background: #f8f9fa;
  min-height: 100vh;
  font-family: 'Poppins', 'Segoe UI', sans-serif;
}

/* Dashboard Overview */
.dashboard-overview {
  display: flex;
  gap: 20px;
  margin: 30px 0;
}

.overview-card {
  flex: 1;
  padding: 30px;
  color: white;
  border-radius: 16px;
  text-align: center;
  font-size: 20px;
  font-weight: 600;
  cursor: pointer;
  transition: all 0.3s ease;
  box-shadow: 0 8px 20px rgba(0, 0, 0, 0.1);
}

.overview-card:hover {
  transform: translateY(-5px);
}

/* Card Colors */
.yellow {
  background: linear-gradient(135deg, #f9d423, #ff4e50);
}

.blue {
  background: linear-gradient(135deg, #42a5f5, #478ed1);
}

.purple {
  background: linear-gradient(135deg, #a18cd1, #fbc2eb);
}

/* Section Title */
h2 {
  font-size: 24px;
  color: #2c3e50;
  margin-bottom: 20px;
  font-weight: 700;
}

/* Chart Placeholder */
.chart-placeholder {
  background: white;
  height: 450px;
  display: flex;
  align-items: center;
  justify-content: center;
  border-radius: 16px;
  box-shadow: 0 8px 20px rgba(0, 0, 0, 0.05);
  font-size: 24px;
  color: #7f8c8d;
  font-weight: 600;
}

/* Responsive */
@media (max-width: 768px) {
  .dashboard-overview {
    flex-direction: column;
  }

  .overview-card {
    font-size: 18px;
    padding: 20px;
  }

  .logout-button {
    font-size: 14px;
    padding: 8px 16px;
  }
}
</style>

  
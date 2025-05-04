<template>
    <div class="container">

   
    <header class="header">
      <div class="menu-toggle">&#9776;</div>
      <div class="user-info">
        <button class="logout-button" @click="handleLogout">Đăng xuất</button>
      </div>
    </header>
  

    <div class="main-content">
      <div class="dashboard-overview">
         <OrderList />
      </div>
  
     
    </div>
  </div>
  </template>
  
  <script setup>
  import { useRouter } from 'vue-router';
  import OrderList from './RestaurantOderList.vue';
  const router = useRouter();
  

  const handleLogout = () => {
    localStorage.removeItem("Role");
    localStorage.removeItem("UserName");
    localStorage.removeItem("IDRes");
    router.replace("/login");  
  };



  </script>
  
  <style scoped>
.container {
 
  background: url("https://xoaimedia.com/wp-content/uploads/2021/08/composition-delicious-indonesian-bakso.jpg" ) no-repeat center center/cover;
 
    display:flex;
    flex-direction:column;
    margin-top:0px;
  
}
.header {
  display: flex;
  justify-content: space-between;

  align-items: center;
  padding: 16px 24px;
  background-color: #0eac09; /* màu đỏ Shopee */
  color: white;
  box-shadow: 0 2px 6px rgba(0, 0, 0, 0.08);
  font-size: 16px;
  font-weight: 500;
}
.container{
  display:flex;
}
.menu-toggle {
  font-size: 24px;
  cursor: pointer;
  border-radius:50%;
}


.user-info {
  display: flex;
  align-items: center;
  gap: 16px;
}


.logout-button {
  background-color: rgb(255, 149, 0);
  color: #ffffff;
  border-radius:20%;
  border: 1.5px solid #ffffff;
  padding: 8px 18px;
  font-size: 14px;
  font-weight: 600;
  border-radius: 6px;
  cursor: pointer;
  transition: 0.2s ease;
}

.logout-button {
  background-color:orange;
  color: #ffffff;
  border: 1px solid #ffffff;
  padding: 10px 24px;
  font-size: 15px;
  font-family: 'Poppins', 'Segoe UI', sans-serif;
  border-radius: 30px;
  cursor: pointer;
  transition: all 0.3s ease;
  box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);
  backdrop-filter: blur(4px);
}

.logout-button:hover {
  background-color: #ff0000;
  color: white;
  border-color: #111;
  transform: translateY(-1px);
}

.logout-button:active {
  transform: translateY(0);
  box-shadow: none;
}




.main-content {
  padding: 0px 0px;

 
  min-height: 200vh;
  font-family: 'Segoe UI', 'Poppins', sans-serif;
  border-radius: 12px;
  backdrop-filter: blur(0px); /* hiệu ứng mờ nền đẹp */
}


/* Dashboard Overview */
.dashboard-overview {
  display: flex;
  flex-wrap: wrap;
  gap: 16px;
  margin-bottom: 32px;
  margin-left:0
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

h2 {
  font-size: 20px;
  color: #333;
  margin-bottom: 20px;
  font-weight: 600;
}


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

  
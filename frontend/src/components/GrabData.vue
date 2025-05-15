<template>

   
</template>

<script setup>
import { ref, onMounted } from 'vue';

const UserName = ref('');

onMounted(() => {
  UserName.value = localStorage.getItem("UserName");
});
</script>
 
<style scoped>
.dashboard-container {
  display: flex;
  height: 100vh;
  background: #f0f2f5;
}

.sidebar {
  width: 270px;
  background: #1e1e2f;
  padding: 30px 20px;
  display: flex;
  flex-direction: column;
  box-shadow: 2px 0 12px rgba(0, 0, 0, 0.2);
  border-top-right-radius: 16px;
  border-bottom-right-radius: 16px;
}

/* Logo style */
.logo {
  display: flex;
  align-items: center;
  gap: 10px;
  font-size: 24px;
  font-weight: bold;
  color: #00e676;
  margin-bottom: 40px;
}

.logo-food {
  font-size: 28px;
}

.Name {
  font-family: "Poppins", sans-serif;
  letter-spacing: 1px;
}

.menu {
  display: flex;
  flex-direction: column;
  gap: 12px;
}

.menu a {
  display: block;
  padding: 12px 20px;
  text-decoration: none;
  color: #e0e0e0;
  font-weight: 500;
  border-radius: 10px;
  transition: all 0.3s ease;
}

.menu a:hover {
  background-color: #00e676;
  color: #1e1e2f;
}

/* Main content */
.main-content {
  flex: 1;
  padding: 40px;
  overflow-y: auto;
}
.RestaurantName {
  display: flex;
  align-items: center;
  gap: 12px;
  background-color: #2c2c3e;
  padding: 16px;
  border-radius: 12px;
  margin-bottom: 30px;
  color: #ffffff;
  box-shadow: 0 4px 12px rgba(0, 0, 0, 0.15);
}


.restaurant-text {
  display: flex;
  flex-direction: column;
  font-family: 'Poppins', sans-serif;
}

.restaurant-title {
  font-size: 14px;
  font-weight: 600;
  margin: 0;
  color: #aaa;
}

.restaurant-name {
  font-size: 16px;
  font-weight: 700;
  margin: 8px 0px;
  color: #00e676;
}

.welcome-text {
  font-size: 13px;
  color: #ccc;
  margin: 0;
}

</style>

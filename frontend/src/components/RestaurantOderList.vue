<template>
    <div class="overlay" @click="closePopup">
      <div class="popup" @click.stop>
        <h3>Danh sách món ăn đã đặt</h3>
        <div v-for="(item, index) in orderDetails" :key="index" class="order-item">
          <img :src="item.imageUrl" alt="food image" class="order-item-image" />
          <div class="order-item-details">
            <span class="food-name">{{ item.foodName }}</span>
            <span class="quantity">{{ item.quantity }} x</span>
            <span class="price">{{ item.totalPrice.toLocaleString() }} VND</span>
            <button class="confirm-btn" @click="confirmOrder(item)">Xác nhận</button>
          </div>
        </div>
  
        <button class="close-btn" @click="close">Đóng</button>
      </div>
    </div>
  </template>
  <script setup>
  import { ref, onMounted } from 'vue';
  import { useRouter } from 'vue-router';
  import { ResOrderList } from '../api/order.js';  
  const router = useRouter();
  const orderDetails = ref([]);
  
  // Lấy IDRes từ sessionStorage
  const IDRes = localStorage.getItem("IDRes");
  console.log(IDRes);
  
  onMounted(async () => {
    try {
      if (IDRes) {
        const data = await ResOrderList(IDRes);
        orderDetails.value = data.map(item => ({
          foodName: item.foodName,
          quantity: item.quantity,
          totalPrice: item.totalPrice,
          imageUrl: item.url_Image
        }));
      }
    } catch (error) {
      console.error('Lỗi khi lấy dữ liệu đơn hàng:', error);
    }
  });
  
  // Đóng popup
  const close = () => {
  router.back()
}
  
  // Xác nhận đơn hàng
  const confirmOrder = (item) => {
    alert(`Đã xác nhận món: ${item.foodName}`);
    // TODO: gọi API xác nhận nếu cần
  };
  </script>
<style scoped>
.overlay {
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background-color: rgba(0, 0, 0, 0.5);
  display: flex;
  justify-content: center;
  align-items: center;
  z-index: 1000;
}

.popup {
  background-color: white;
  padding: 20px;
  border-radius: 8px;
  width: 80%;
  max-width: 500px;
  box-shadow: 0 4px 10px rgba(0, 0, 0, 0.1);
  position: relative;
  max-height: 80vh;
  overflow-y: auto;
}

h3 {
  margin-top: 0;
  text-align: center;
}

.order-item {
  display: flex;
  align-items: flex-start;
  gap: 10px;
  margin: 10px 0;
  border-bottom: 1px solid #ddd;
  padding-bottom: 10px;
}

.order-item-image {
  width: 60px;
  height: 60px;
  object-fit: cover;
  border-radius: 5px;
}

.order-item-details {
  flex: 1;
  display: flex;
  justify-content: space-between;
  align-items: center;
  flex-wrap: wrap;
  gap: 5px;
}

.food-name,
.quantity,
.price {
  font-size: 14px;
}

.food-name {
  font-weight: bold;
  font-size: 16px;
  width: 100%;
}

.confirm-btn {
  background-color: #28a745;
  color: white;
  border: none;
  padding: 5px 10px;
  border-radius: 5px;
  cursor: pointer;
  margin-left: auto;
}

.confirm-btn:hover {
  background-color: #218838;
}

.close-btn {
  background-color: #dc3545;
  color: white;
  border: none;
  padding: 10px 20px;
  border-radius: 5px;
  cursor: pointer;
  display: block;
  margin: 20px auto 0;
}

.close-btn:hover {
  background-color: #c82333;
}
</style>
  
  
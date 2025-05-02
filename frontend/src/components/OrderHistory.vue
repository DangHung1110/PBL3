<template> 
  <div class="popup-content cart-style"> 
    <h2>Lịch sử đặt hàng</h2>

    <div v-for="item in data" :key="item.IDOrder" class="cart-item">
      <div class="item-image">
        <img :src="item.Url_image" alt="Ảnh món ăn" />
      </div>
      <div class="item-details">
        <div class="info-row">
          <span class="label">Món </span>
          <span class="value item-name">: {{ item.FoodName }}</span>
        </div>
        <div class="info-row">
          <span class="label">Nhà hàng </span>
          <span class="value item-meta">
            <span class="restaurant-name">: {{ item.RestaurantName }}</span>
            <span class="quantity">x{{ item.Quantity }}</span>
          </span>
        </div>
        <div class="info-row">
          <span class="label">Thành tiền </span>
          <span class="value price">: {{ item.Doanhso }} VNĐ</span>
        </div>
        <div class="time-info">
          <span>Thời gian đặt hàng: {{ formatDate(item.OrderTime) }}</span>  |
          <span>Thời gian xác nhận: {{ formatDate(item.OrderConfirmedTime) }}</span> |
          <span>Thời gian giao hàng: {{ formatDate(item.CusConfirmedTime) }}</span>  |
        </div>
      </div>
    </div>
  </div>
</template>




<script setup>
import { ref, onMounted } from 'vue';
import { getTKData } from '../api/thongke.js';

const data = ref([]);

const formatDate = (datetime) => {
  return new Date(datetime).toLocaleString("vi-VN");
};

onMounted(async () => {
  try {
    const IDCustomer = localStorage.getItem("IDRes");
    if (!IDCustomer) return;

    const dulieu = await getTKData(IDCustomer);

    data.value = dulieu.map(item => ({
      IDOrder: item.id,
      IDCustomer: item.idCustomer,
      IDRes: item.idRes,
      OrderTime: item.orderTime,
      CusConfirmedTime: item.cusConfirmedTime,
      OrderConfirmedTime: item.orderConfirmedTime,
      FoodName: item.foodName,
      RestaurantName: item.restaurantName,
      Quantity: item.quantity,
      Doanhso: item.doanhso,
      Url_Image: item.url_Image,
    }));
  } catch (error) {
    console.error("Lỗi khi tải dữ liệu lịch sử:", error);
  }
});
</script>


<style scoped>
/* Optional: Load a more elegant font */
@import url('https://fonts.googleapis.com/css2?family=Inter:wght@400;600;800&display=swap');

* {
  font-family: 'Inter', sans-serif;
}


.cart-style {
  padding: 16px;
  background-color: #f5f5f5;
  border-radius: 16px;
  max-height: 80vh;
  overflow-y: auto;
}

.cart-style h2 {
  font-size: 20px;
  margin-bottom: 16px;
  font-weight: bold;
  color: #333;
}

.cart-item {
  display: flex;
  align-items: flex-start;
  background-color: #fff;
  border-radius: 12px;
  box-shadow: 0 2px 8px rgba(0,0,0,0.05);
  padding: 16px;
  margin-bottom: 16px;
}

.item-image img {
  width: 80px;
  height: 80px;
  object-fit: cover;
  border-radius: 8px;
  margin-right: 16px;
}

.item-details {
  flex: 1;
}

.item-meta {
  display: flex;
  justify-content: space-between;
  width: 100%;
}

.item-name {
  font-size: 16px;
  font-weight: 600;
  color: #222;
}

.price {
  font-size: 15px;
  font-weight: 600;
  color: #d0021b;
  margin-bottom: 4px;
}

.time-info {
  font-size: 12px;
  color: #999;
  margin-top: 8px;
}
.item-actions {
  display: flex;
  flex-direction: column;
  gap: 8px;
  margin-left: 12px;
}

.confirm-btn,
.delete-btn {
  padding: 6px 12px;
  border: none;
  border-radius: 6px;
  font-size: 14px;
  cursor: pointer;
  color: #fff;
  white-space: nowrap;
}

.confirm-btn {
  background-color: #28a745;
}

.delete-btn {
  background-color: #d9534f;
}
.info-row {
  display: flex;
  align-items: center;
  margin-bottom: 8px;
}

.label {
  width: 100px;
  font-weight: 500;
  color: #444;
  flex-shrink: 0;
}




</style>



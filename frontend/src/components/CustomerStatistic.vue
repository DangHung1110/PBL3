<template>
  <div class="container">
    <h2>Thống kê món ăn</h2>
    <div class="card-list">
      <div class="card" v-for="(item, index) in thongkedata" :key="index">
        <h3>{{ item.FoodName }}</h3>
        <p><strong>Nhà hàng:</strong> {{ item.RestaurantName }}</p>
      <img :src="item.Url_image" alt="Ảnh món ăn" style="width: 150px; height: auto;" />

        <p><strong>Số lượng:</strong> {{ item.Quantity }}</p>
        <p><strong>Tổng tiền:</strong> {{ item.TotalPrice }} VNĐ</p>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue';
import { ThongkeOrder} from '../api/order.js';

const thongkedata = ref([]);
const ID=localStorage.getItem("IDRes");
const getdata = async (ID) => {
  try {
    console.log('ID:', ID); 
    if (!ID) {
      alert("Bạn chưa đăng nhập!");
      return;
    }
    const data = await ThongkeOrder(ID);
    console.log('Dữ liệu thống kê:', data);
    thongkedata.value = data.map(item => ({
      FoodName: item.foodName,
      RestaurantName: item.restaurantName,
      Quantity: item.quantity,
      TotalPrice: item.totalPrice,
      Url_image :item.url_image
    }));
  } catch (error) {
    console.error('Lỗi khi lấy dữ liệu:', error);
  }
};

onMounted(() => {
  getdata(ID);
});
</script>

<style scoped>
.container {
  padding: 20px;
}

.card-list {
  display: flex;
  flex-wrap: wrap;
  gap: 20px;
}

.card {
  background-color: #f9f9f9;
  border-radius: 12px;
  box-shadow: 0 2px 5px rgba(0,0,0,0.1);
  padding: 16px;
  width: 250px;
}

.card h3 {
  margin: 0 0 10px;
}

.card p {
  margin: 4px 0;
}
</style>

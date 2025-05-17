<template>
  <div class="container">
    <h2>Lịch sử đặt hàng</h2>
    <input type="date" v-model="selectedDate" @change="filterByDate" />
   

 <div v-for="item in thongkedata" :key="item.OrderTime" class="order-card">

      <div class="order-left">

        <div class="order-details">
      
          <p><strong>Thành tiền</strong>: <span class="price">{{ formatCurrency(item.Revenue) }}</span></p>
          <p class="time">
            Thời gian đặt hàng: {{ formatDate(item.OrderTime) }} |
            Thời gian xác nhận: {{ formatDate(item.OrderConfirmedTime) }} |
            Thời gian giao hàng: {{ formatDate(item.CusConfirmedTime) }}
          </p>
        </div>
      </div>
 
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue';
import { getthongkegrab } from "../api/Grab.js";
const selectedDate = ref('');
const UserName = ref('');
const thongkedata = ref([]);
const allData = ref([]);

onMounted(async () => {
  UserName.value = localStorage.getItem("UserName");
  const IDGrab = parseInt(localStorage.getItem("IDRes"));

  try {
    const response = await getthongkegrab(IDGrab);
    console.log(response);
    thongkedata.value =response.map(item => ({
      CusConfirmedTime: item.cusConfirmedTime,
      OrderTime: item.orderTime,
      OrderConfirmedTime: item.orderConfirmedTime,
      Revenue: item.revenue
    }));
    allData.value=response.map(item => ({
      CusConfirmedTime: item.cusConfirmedTime,
      OrderTime: item.orderTime,
      OrderConfirmedTime: item.orderConfirmedTime,
      Revenue: item.revenue
    }));
    console.log(thongkedata.value);
  } catch (error) {
    console.error("Lỗi khi lấy dữ liệu thống kê:", error);
  }
});


const formatDate = (datetime) => {
  const date = new Date(datetime);
  return date.toLocaleTimeString('vi-VN') + ' ' + date.toLocaleDateString('vi-VN');
};


const formatCurrency = (value) => {
  return new Intl.NumberFormat('vi-VN', { style: 'currency', currency: 'VND' }).format(value);
};
const filterByDate = () => {
  if (!selectedDate.value) {
    thongkedata.value = allData.value;
    return;
  }

  thongkedata.value = allData.value.filter(item => {
    const date = new Date(item.CusConfirmedTime);
    const yyyyMMdd = date.toISOString().split('T')[0];
    return yyyyMMdd === selectedDate.value;
  });
};
</script>

<style scoped>
.container {
  max-width: 900px;
  margin: 0 auto;
  padding: 20px;
  font-family: sans-serif;
}

h2 {
  margin-bottom: 20px;
}

.order-card {
  display: flex;
  justify-content: space-between;
  background: #fff;
  padding: 16px;
  border-radius: 10px;
  margin-bottom: 16px;
  box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);
}

.order-left {
  display: flex;
  gap: 16px;
}

.dish-img {
  width: 100px;
  height: 100px;
  object-fit: cover;
  border-radius: 10px;
  background: #f0f0f0;
}

.order-details p {
  margin: 4px 0;
}

.highlight {
  color: black;
  font-weight: 600;
}

.price {
  color: red;
  font-weight: bold;
}

.time {
  color: gray;
  font-size: 0.9em;
}

.quantity {
  font-weight: bold;
  font-size: 1.2em;
  align-self: center;
}
</style>

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
@import url('https://fonts.googleapis.com/css2?family=Inter:wght@400;600&display=swap');

.container {
  max-width: 900px;
  margin: 0 auto;
  padding: 32px 20px;
  font-family: 'Inter', sans-serif;
  background-color: #f9f9f9;
  border-radius: 20px;
  box-shadow: 0 0 12px rgba(0, 0, 0, 0.05);
}

h2 {
  margin-bottom: 24px;
  font-size: 28px;
  font-weight: 600;
  color: #333;
  text-align: center;
}

input[type="date"] {
  padding: 10px 14px;
  border: 1px solid #ddd;
  border-radius: 10px;
  font-size: 16px;
  width: 100%;
  max-width: 250px;
  margin: 0 auto 24px;
  display: block;
  background-color: #fff;
  transition: border 0.3s ease;
}

input[type="date"]:focus {
  outline: none;
  border-color: #007bff;
  box-shadow: 0 0 6px rgba(0, 123, 255, 0.2);
}

.order-card {
  background: #ffffff;
  padding: 20px 24px;
  border-radius: 16px;
  margin-bottom: 20px;
  box-shadow: 0 6px 16px rgba(0, 0, 0, 0.05);
  transition: transform 0.2s ease, box-shadow 0.2s ease;
}

.order-card:hover {
  transform: translateY(-4px);
  box-shadow: 0 10px 25px rgba(0, 0, 0, 0.08);
}

.order-left {
  display: flex;
  flex-direction: column;
  gap: 12px;
}

.order-details p {
  margin: 6px 0;
  font-size: 15px;
  color: #444;
}

.price {
  color: #d6336c;
  font-weight: 600;
  font-size: 16px;
}

.time {
  color: #666;
  font-size: 14px;
}

@media (max-width: 600px) {
  .order-card {
    padding: 16px;
  }

  h2 {
    font-size: 24px;
  }

  .price {
    font-size: 15px;
  }
}
</style>

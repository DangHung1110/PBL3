<template>
  <div class="order-list">
    <h3>Danh sách món ăn đã đặt</h3>
    <div v-for="(item, index) in orderDetails" :key="index" class="order-item">
      <img :src="item.imageUrl" alt="food image" class="order-item-image" />
      <div class="order-item-details">
        <span class="food-name">{{ item.foodName }}</span>
        <div class="order-item-extra">
          <span class="quantity">{{ item.quantity }} x</span>
          <span class="price">{{ item.totalPrice.toLocaleString() }} VND</span>
        </div>
        <button class="confirm-btn" @click="confirmOrder(item.IDOrder)">Xác nhận</button>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue';
import { useRouter } from 'vue-router';
import { ResOrderList, UpdateConfirmedTime} from '../api/order.js';  

const router = useRouter();
const orderDetails = ref([]);

const IDRes = localStorage.getItem("IDRes");
console.log(IDRes);


onMounted(async () => {
  try {
    if (IDRes) {
      const data = await ResOrderList(IDRes);
      orderDetails.value = data.map(item => ({
      IDOrder: item.idOrder,
     IDFood: item.idFood,
    IDCustomer: item.idCustomer, // thêm
    RestaurantName: item.restaurantName, // thêm
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


const confirmOrder = async (IDOrder) => {


  try {
    const response = await UpdateConfirmedTime(IDOrder);
    console.log(response);
    alert("Đơn hàng đã được xác nhận thành công!");
  } catch (error) {
    console.error('Lỗi khi xác nhận đơn hàng:', error);
    alert("Đã xảy ra lỗi trong quá trình xác nhận đơn hàng!");
  }
};
</script>


<style scoped>
.order-list {
  margin: 40px auto;
  max-width: 800px;
  font-family: 'Arial', sans-serif;
}

h3 {
  text-align: center;
  font-size: 2em;
  color: #333;
  margin-bottom: 30px;
  font-weight: 600;
  letter-spacing: 1px;
}

.order-item {
  display: flex;
  align-items: center;
  gap: 20px;
  margin-bottom: 20px;
  border-bottom: 1px solid #eaeaea;
  padding-bottom: 20px;
  transition: transform 0.2s ease, box-shadow 0.3s ease;
}

.order-item:hover {
  transform: translateY(-5px);
  box-shadow: 0 4px 20px rgba(0, 0, 0, 0.1);
}

.order-item-image {
  width: 80px;
  height: 80px;
  object-fit: cover;
  border-radius: 10px;
  border: 2px solid #f1f1f1;
  box-shadow: 0 2px 8px rgba(0, 0, 0, 0.1);
}

.order-item-details {
  flex: 1;
  display: flex;
  justify-content: space-between;
  align-items: center;
  flex-wrap: wrap;
  gap: 15px;
  color: #333;
}

.food-name {
  font-weight: bold;
  font-size: 18px;
  color: #333;
  flex: 2;
}

.order-item-extra {
  display: flex;
  gap: 10px;
  font-size: 14px;
  color: #888;
}

.price {
  font-size: 16px;
  font-weight: bold;
  color: #2c3e50;
}

.confirm-btn {
  background-color: #28a745;
  color: white;
  border: none;
  padding: 8px 20px;
  border-radius: 5px;
  cursor: pointer;
  font-size: 14px;
  box-shadow: 0 3px 10px rgba(0, 0, 0, 0.1);
  transition: background-color 0.3s ease, box-shadow 0.3s ease;
}

.confirm-btn:hover {
  background-color: #218838;
  box-shadow: 0 4px 15px rgba(0, 0, 0, 0.15);
}

.confirm-btn:focus {
  outline: none;
}

@media (max-width: 600px) {
  .order-item {
    flex-direction: column;
    text-align: center;
  }
}
</style>

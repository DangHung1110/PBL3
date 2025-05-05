<template> 
  <div class="cart-container">
    <h3>📝Đơn hàng chờ xác nhận</h3>

    <div class="cart-header">
      <span class="col-name">Sản Phẩm</span>
      <span class="col-unit">Đơn Giá</span>
      <span class="col-quantity">Số Lượng</span>
      <span class="col-total">Thành Tiền</span>
      <span class="col-action">Thao Tác</span>
    </div>

    <div class="cart-item" v-for="(item, index) in orderDetails" :key="index">

  <div class="item-info">
    <img :src="item.imageUrl" alt="Hình món ăn" class="item-image" />
    <div class="item-desc">
      <div class="item-name" style="text-align:center">{{ item.foodName }}</div>
      <div class="item-restaurant">Nhà hàng : {{ item.RestaurantName }}</div>
    </div>
  </div>


  <div class="item-price">{{ formatCurrency(item.totalPrice / item.quantity) }}</div>


  <div class="item-quantity">{{ item.quantity }}</div>


  <div class="item-total">{{ formatCurrency(item.totalPrice) }}</div>


  <div class="item-action">
    <button v-if="item.Status_Restaurant === 'pending'" @click="confirmOrder(item.IDOrder)">Xác nhận đơn</button>
    <div v-else class="delivery-status">
      <span class="delivery-icon">🚚</span>
      <span class="delivery-text">Đang giao...</span>
    </div>
  </div>
</div>

  </div>
</template>



<script setup>
import { ref, onMounted } from 'vue';
import { useRouter } from 'vue-router';
import { ResOrderList, UpdateConfirmedTime} from '../api/order.js';  
import Swal from 'sweetalert2';
const formatCurrency = (value) => {
  return new Intl.NumberFormat('vi-VN', {
    style: 'currency',
    currency: 'VND' }).format(value);
  }


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
     Status_Restaurant: item.status_Restaurant,
    IDCustomer: item.idCustomer, 
    RestaurantName: item.restaurantName, 
        foodName: item.foodName,
        quantity: item.quantity,
        totalPrice: item.totalPrice,
        imageUrl: item.url_Image
      }));
      console.log(orderDetails.value);
    }
  } catch (error) {
    console.error('Lỗi khi lấy dữ liệu đơn hàng:', error);
  }
});


const confirmOrder = async (IDOrder) => {


  try {
    const response = await UpdateConfirmedTime(IDOrder);
    const updatedOrder = orderDetails.value.find(item => item.IDOrder === IDOrder);
    if (updatedOrder) {
      updatedOrder.Status_Restaurant = 'confirmed'; 
    }
    console.log(response);
    Swal.fire({
      toast: true,   
    icon: 'success',
    title: 'THÔNG BÁO',
    text: 'Đơn hàng đã được xác nhận thành công!',
    timer: 3000,
    position: 'bottom-end',
    timerProgressBar: true,
    showConfirmButton: false,
    showClass: {
      popup: 'swal2-slide-in-right' }}
    );
  } catch (error) {
    console.error('Lỗi khi xác nhận đơn hàng:', error);
     Swal.fire({
          toast: true,   
        icon: 'error',
        title: 'THÔNG BÁO',
        text: 'Lỗi xác nhận đơn hàng',
        timer: 3000,
        position: 'bottom-end',
        timerProgressBar: true,
        showConfirmButton: false,
        showClass: {
          popup: 'swal2-slide-in-right' }}
        );
  }
};
</script>

<style>
.item-desc
.item-price
.item-quantity
.item-total {
  display: flex;
  flex-direction: column;
  gap: 4px;
  align-items: center; /* <- Thêm dòng này để căn giữa ngang */
}

.cart-container {
  max-width: 1100px;
  margin: 40px auto;
  padding: 24px;
  background: #ffffff;
  border-radius: 12px;
  font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
}
.col-name{
   margin-left:20%;
}
h3 {
  font-size: 24px;
  color: #087404;
  font-weight: bold;
  margin-bottom: 24px;
  text-align: center;
}

/* Tiêu đề bảng */
.cart-header {
  display: grid;
  grid-template-columns: 7fr 2fr 2fr 2fr 2fr;
  align-items: center;
  padding: 14px 16px;
  background-color: #f5f5f5;
  border: 1px solid #e6e6e6;
  font-weight: 600;
  color: #333;
  border-radius: 8px;
  margin-bottom: 8px;
}

/* Dòng item */
.cart-item {
  display: grid;
  grid-template-columns: 3fr 1fr 1fr 1fr 1fr;
  align-items: center;
  background-color: #fff;
  padding: 14px 16px;
  border: 1px solid #eee;
  margin-bottom: 12px;
  border-radius: 8px;
  transition: box-shadow 0.2s;
}

.cart-item:hover {
  box-shadow: 0 2px 8px rgba(0, 0, 0, 0.05);
}

/* Cột sản phẩm (ảnh + tên + nhà hàng) */
.item-info {
  display: flex;
  align-items: center;
  gap: 12px;
}

.item-image {
  width: 64px;
  height: 64px;
  object-fit: cover;
  border-radius: 6px;
  border: 1px solid #ddd;
}

.item-desc {
  display: flex;
  flex-direction: column;
  justify-content: center;
}

.item-name {
  font-weight: 600;
  font-size: 15px;
  color: #222;
}

.item-restaurant {
  font-size: 13px;
  color: #888;
}

/* Các cột còn lại */
.item-price,
.item-quantity,
.item-total,
.item-action {
  text-align: center;
  font-size: 14px;
  color: #333;
}

/* Nút xác nhận */
.item-action button {
  padding: 6px 12px;
  background-color: #0b7904;
  color: #fff;
  border: none;
  border-radius: 4px;
  cursor: pointer;
  font-size: 13px;
  font-weight: 500;
  transition: background-color 0.2s ease;
}

.item-action button:hover {
  background-color: #0f9106;
}

/* Trạng thái đang giao */
.delivery-status {
  background-color: #ffa113;
  color: #ffffff;
  padding: 6px 10px;
  border-radius: 4px;
  display: inline-flex;
  align-items: center;
  gap: 6px;
  font-size: 13px;
  font-weight: 500;
  justify-content: center;
}

.delivery-icon {
  font-size: 15px;
}
.swal2-slide-in-right {
  animation: slide-in-right 0.5s ease-out;
}

@keyframes slide-in-right {
  from {
    transform: translateX(100%);
    opacity: 0;
  }
  to {
    transform: translateX(0);
    opacity: 1;
  }
}




</style>
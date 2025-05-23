<template>
  <div class="cart-container">
    <h3>📝 Đơn hàng chờ xác nhận</h3>

    <div class="cart-header">
      <span class="col-name">Product</span>
      <span class="col-unit">UPrice</span>
      <span class="col-quantity">SL</span>
      <span class="col-total">Total</span>
      <span class="col-action">Confirm</span>
      <span class="col-contact">GrabPhone</span>
      <span class="col-address">Address</span>
      <span class="col-contact2">CusPhone</span>
    </div>

    <div class="cart-item" v-for="(item, index) in orderDetails" :key="index">
      <div class="item-info">
        <img :src="item.imageUrl" alt="Hình món ăn" class="item-image" />
        <div class="item-desc">
          <div class="item-name">{{ item.foodName }}</div>
          <div class="item-restaurant">Nhà hàng: {{ item.RestaurantName }}</div>
        </div>
      </div>

      <div class="item-price">{{ formatCurrency(item.totalPrice / item.quantity) }}</div>
      <div class="item-quantity">{{ item.quantity }}</div>
      <div class="item-total">{{ formatCurrency(item.totalPrice) }}</div>
      <div class="item-action">
        <button v-if="item.Status_Restaurant === 'pending'" @click="confirmOrder(item.IDOrder)">
          Xác nhận
        </button>
        <div v-else class="delivery-status">
          <span class="delivery-icon">🚚</span>
          <span class="delivery-text">Đang giao...</span>
        </div>
      </div>
      <div class="item-contact">{{ item.GrabPhone }}</div>
      <div class="item-address">{{ item.Address }}</div>
      <div class="item-contact">{{ item.CustomerPhone }}</div>
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue';
import { useRouter } from 'vue-router';
import { ResOrderList, UpdateConfirmedTime } from '../api/order.js';
import Swal from 'sweetalert2';

const formatCurrency = (value) => {
  return new Intl.NumberFormat('vi-VN', {
    style: 'currency',
    currency: 'VND',
  }).format(value);
};

const router = useRouter();
const orderDetails = ref([]);
const IDRes = localStorage.getItem('IDRes');

onMounted(async () => {
  try {
    if (IDRes) {
      const data = await ResOrderList(IDRes);
      orderDetails.value = data.map((item) => ({
        IDOrder: item.idOrder,
        IDFood: item.idFood,
        Status_Restaurant: item.status_Restaurant,
        IDCustomer: item.idCustomer,
        RestaurantName: item.restaurantName,
        foodName: item.foodName,
        quantity: item.quantity,
        totalPrice: item.totalPrice,
        imageUrl: item.url_Image,
        Address: item.address,
        GrabPhone: item.grabPhone,
        CustomerPhone: item.customerPhone,
      }));
    }
  } catch (error) {
    console.error('Lỗi khi lấy dữ liệu đơn hàng:', error);
  }
});

const confirmOrder = async (IDOrder) => {
  try {
    const response = await UpdateConfirmedTime(IDOrder);
    const updatedOrder = orderDetails.value.find((item) => item.IDOrder === IDOrder);
    if (updatedOrder) {
      updatedOrder.Status_Restaurant = 'confirmed';
    }
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
        popup: 'swal2-slide-in-right',
      },
    });
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
        popup: 'swal2-slide-in-right',
      },
    });
  }
};
</script>

<style scoped>
.cart-container {
  max-width: 1400px;
  margin: 40px auto;
  padding: 32px;
  background: #ffffff;
  border-radius: 16px;
  box-shadow: 0 4px 20px rgba(0, 0, 0, 0.08);
  font-family: 'Poppins', -apple-system, BlinkMacSystemFont, sans-serif;
}

h3 {
  font-size: 22px;
  color: #1a3c34;
  font-weight: 600;
  margin-bottom: 32px;
  padding:0px 34%;
  text-align: center;
  display: flex;
  align-items: center;
  gap: 8px;
}

/* Tiêu đề bảng */
.cart-header {
  display:flex;

  align-items: center;
  padding: 16px 20px;
  background: linear-gradient(135deg, #f8fafc 0%, #e2e8f0 100%);
  border-radius: 12px;
  font-weight: 600;
  color: #1a3c34;
  text-transform: uppercase;
  font-size: 13px;
  letter-spacing: 0.8px;
  margin-bottom: 16px;
  border: 1px solid #e2e8f0;
}

.cart-header span {
  text-align: center;
  display: flex;
  justify-content: center;
  align-items: center;
}

.col-name {
  font-size:11px;
  justify-content:left;
  padding-left: 20px;
}
.col-unit{
   font-size:11px;
   padding-left:18%;
}

.col-quantity{
   font-size:11px;
    padding-left:6.4%;
}
.col-total{
   font-size:11px;
    padding-left:6%;
}
.col-action{
   font-size:11px;
   padding-left:8%;
}

.col-contact{
   font-size:11px;
 padding-left:5.5%;}

.col-address{
   font-size:11px;
  padding-left:6%;
}
.col-contact2{
   font-size:11px;
 padding-left:6.8%;}








/* Dòng item */
.cart-item {
  display: grid;
  grid-template-columns: 4fr 1.5fr 1fr 1.5fr 1.5fr 1.5fr 2.5fr 1.5fr;
  grid-column-gap: 24px;
  align-items: center;
  background: #fff;
  padding: 16px 20px;
  border-radius: 12px;
  margin-bottom: 12px;
  border: 1px solid #f1f5f9;
  transition: transform 0.2s ease, box-shadow 0.2s ease;
}

.cart-item:hover {
  transform: translateY(-2px);
  box-shadow: 0 6px 12px rgba(0, 0, 0, 0.06);
}

/* Cột sản phẩm */
.item-info {
  display: flex;
  align-items: center;
  gap: 16px;
}

.item-image {
  width: 80px;
  height: 80px;
  object-fit: cover;
  border-radius: 10px;
  border: 1px solid #e2e8f0;
  transition: transform 0.2s ease;
}

.item-image:hover {
  transform: scale(1.05);
}

.item-desc {
  display: flex;
  flex-direction: column;
  gap: 6px;
}

.item-name {
  font-weight: 600;
  font-size: 16px;
  color: #1a3c34;
  text-align: left;
}

.item-restaurant {
  font-size: 13px;
  color: #64748b;
  font-weight: 400;
}

/* Các cột khác */
.item-price,
.item-quantity,
.item-total,
.item-contact,
.item-address {
  text-align: center;
  font-size: 14px;
  color: #1a3c34;
  display: flex;
  justify-content: center;
  align-items: center;
}

.item-address {
  font-size: 13px;
  color: #64748b;
  line-height: 1.4;
}

/* Nút xác nhận */
.item-action button {
  width: 120px;
  height: 36px;
  background: #318801;
  color: #ffffff;
  border: none;
  border-radius: 10px;
  cursor: pointer;
  font-size: 13px;
  font-weight: 500;
  font-family: 'Poppins', -apple-system, BlinkMacSystemFont, sans-serif;
  display: flex;
  justify-content: center;
  align-items: center;
  transition: transform 0.2s ease, box-shadow 0.2s ease, background 0.3s ease;
  box-shadow: 0 2px 6px rgba(0, 0, 0, 0.1);
}

.item-action button:hover {
  transform: translateY(-2px);
  box-shadow: 0 4px 12px rgba(0, 0, 0, 0.15);
  background:#318801; 
}

/* Trạng thái đang giao */
.delivery-status {
  width: 120px;
  height: 36px;
  background:#efb300 ;
  color: #ffffff;
  border-radius: 10px;
  display: flex;
  flex-direction: row;
  justify-content: center;
  align-items: center;
  gap: 6px;
  font-size: 13px;
  font-weight: 500;
  font-family: 'Poppins', -apple-system, BlinkMacSystemFont, sans-serif;
  box-shadow: 0 2px 6px rgba(0, 0, 0, 0.1);
  transition: transform 0.2s ease, box-shadow 0.2s ease;
}

.delivery-status:hover {
  transform: translateY(-2px);
  box-shadow: 0 4px 12px rgba(0, 0, 0, 0.15);
}

.delivery-icon {
  font-size: 16px;
  line-height: 1;
}

/* Animation cho Swal */
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
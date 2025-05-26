<template> 
  <transition name="popup-fade">
    <div class="popup-overlay" @click.self="closePopup">
      <div class="popup-content horizontal-layout">
  <!-- Ảnh đồ ăn bên trái -->
  <img :src="food?.image" alt="Food Image" class="popup-image" />

  <!-- Thông tin và hành động bên phải -->

  <div class="right-section">
    <div class="popup-text">
         <h2 class="title">{{ food?.name }}</h2>
      <p><strong>Giảm giá:</strong> {{ food?.discount }}%</p>
      <p><strong>Giá:</strong> {{ food?.price }} VND</p>
      <p><strong>Số lượng:</strong> {{ food?.quantity }}</p>
      <p><strong>Nhà hàng:</strong> {{ restaurant?.name }}</p>
      <p><strong>Địa chỉ:</strong> {{ restaurant?.address }}</p>
    </div>

    <div class="popup-actions">
      <div class="quantity-control">
        <button @click="quantityToAdd > 1 && quantityToAdd--">-</button>
        <input type="number" v-model="quantityToAdd" min="1" :max="food?.quantity" />
        <button @click="quantityToAdd < food?.quantity && quantityToAdd++">+</button>
      </div>
      <p class="totalprice"><strong>Tổng giá:</strong> {{ calculatedPrice.toLocaleString() }} VND</p>
      <div class="chinhbut">
    <button class="add-to-cart-btn" @click="addToCart">Đặt Hàng</button>
      </div>
  
    </div>
  </div>

  <button class="close-btn" @click="closePopup">×</button>
</div>

    </div>
  </transition>
</template>
  
  <script setup>
  import { addOrder } from '../api/order.js';
  import { computed } from 'vue';
  import { onMounted, ref } from 'vue';
  import { useRoute, useRouter } from 'vue-router';
  import { GetFoodById, GetRestaurantById,changefoodnum} from '../api/FoodSevice.js';
  import Swal from 'sweetalert2';
  import {emitter} from '../api/eventBus.js';
 
const route = useRoute();
  const router = useRouter();
  const food = ref(null);
  const quantityToAdd = ref(1);
  const restaurant = ref(null);
   const closePopup = () => {
    router.replace(`/customer/:username/foodlist`);
  };
  onMounted(async () => {
    const foodId = route.params.id;
    const data = await GetFoodById(foodId);
    food.value = {
      ...data,
      image: data.url_Image,
      url_image: data.url_Image
    };
  
    const resData = await GetRestaurantById(food.value.idRes);
    restaurant.value = resData;
    console.log("Restaurant data:", resData);
  });
  const calculatedPrice = computed(() => {
  if (!food.value) return 0;
  const originalPrice = food.value.price || 0;
  const discount = food.value.discount || 0;
  const discountedPrice = originalPrice * (1 - discount / 100);
  return (discountedPrice * quantityToAdd.value); // Giữ 2 số lẻ
});
const addToCart = async() => {
   const senddata={
    IDFood:route.params.id,
    FoodName:food.value.name,
    RestaurantName:restaurant.value.name,
    IDRes:food.value.idRes,
    Quantity:quantityToAdd.value,
    TotalPrice:parseInt(calculatedPrice.value),
    Url_image:food.value.url_image,
    IDCustomer:localStorage.getItem("IDRes"),
    Phone:localStorage.getItem("Phone"),
    Address:localStorage.getItem("Address"),
    Status_User:"pending",
    Status_Restaurant:"pending",
    };
     console.log(senddata);
     console.log(food.value.quantity);
     if(food.value.quantity>=1)
     {  console.log(food.value.quantity);
     try {
    const result = await addOrder(senddata);
    console.log(route.params.id);
    console.log("Food quantity before update:", food.value.quantity);
    console.log("Quantity to add:", quantityToAdd.value);
    console.log(food.value.quantity-quantityToAdd.value);
    await changefoodnum(route.params.id,food.value.quantity-quantityToAdd.value);
    console.log("Item added to cart:", result);
    emitter.emit('UpdateCountProduct',' ');
    Swal.fire({
      toast: true,   
    icon: 'success',
    title: 'THÔNG BÁO',
    text: 'Thêm đơn hàng thành công!',
    timer: 3000,
    position: 'bottom-end',
    timerProgressBar: true,
    showConfirmButton: false,
    showClass: {
      popup: 'swal2-slide-in-right' }}
    );
  } catch (error) {
    console.log(food.value.quantity);
    Swal.fire({
      toast: true,   
    icon: 'error',
    title: 'THÔNG BÁO',
    text: 'Lỗi chọn đơn hàng!',
    timer: 3000,
    position: 'bottom-end',
    timerProgressBar: true,
    showConfirmButton: false,
    showClass: {
      popup: 'swal2-slide-in-right' }}
    );
  }}
  else{
    Swal.fire({
      toast: true,   
    icon: 'error',
    title: 'THÔNG BÁO',
    text: 'Đã hết hàng!',
    timer: 3000,
    position: 'bottom-end',
    timerProgressBar: true,
    showConfirmButton: false,
    showClass: {
      popup: 'swal2-slide-in-right' }}
    );
  };}
  </script>
  
  <style scoped>
.popup-overlay {
  position: fixed;
  inset: 0;
  background: rgba(0, 0, 0, 0.6);
  display: flex;
  justify-content: center;
  align-items: center;
  z-index: 10000;
}

.popup-content {
  background: #fff8f1;
  padding: 16px;
  border-radius: 20px;
  width: 300px;
  max-width: 90%;
  box-shadow: 0 12px 30px rgba(0, 0, 0, 0.25);
  display: flex;
  flex-direction: column;
  gap: 12px;
  position: relative;
  animation: scaleIn 0.4s ease;
  font-family: 'Playfair Display', serif;
}

.popup-image {
  width: 160px;
  height: 160px;
  object-fit: cover;
  border-radius: 16px;
  box-shadow: 0 8px 20px rgba(0, 0, 0, 0.3);
  align-self: center;
  transition: transform 0.3s ease;
}


.popup-image:hover {
  transform: scale(1.05);
}

.popup-text {
  text-align: center;
}

.popup-text h2 {
  font-size: 30px;
  color: #8b4513;
  margin-bottom: 12px;
}

.popup-text p {
  margin: 6px 0;
  font-size: 16px;
  color: #5c4033;
}

.quantity-control {
  display: flex;
  align-items: center;
  justify-content: center;
  gap: 12px;
  padding-left:20%;
}

.quantity-control button {
  width: 40px;
  height: 40px;
  border-radius: 50%;
  border: none;
  background: #f7d9c4;
  font-size: 22px;
  font-weight: bold;
  cursor: pointer;
  transition: background 0.3s, transform 0.2s;
}

.quantity-control button:hover {
  background: #f2c29d;
  transform: scale(1.15);
}

.quantity-control input {
  width: 60px;
  text-align: center;
  padding: 8px;
  border: 1px solid #d3a675;
  border-radius: 8px;
  font-size: 16px;
}

.popup-actions {
  display: flex;
  flex-direction: column;
  align-items: center;
  gap: 14px;
}

.popup-actions p {
  font-size: 18px;
  font-weight: bold;
  color: #a0522d;
}

.add-to-cart-btn {
  background: linear-gradient(135deg, #d2691e, #8b4513);
  color: white;
  font-size: 16px;
  padding: 12px 30px;

  border: none;
  border-radius: 30px;
  cursor: pointer;
  transition: background 0.3s, transform 0.2s;
}

.add-to-cart-btn:hover {
  background: linear-gradient(135deg, #8b4513, #5c4033);
  transform: scale(1.08);
}

.close-btn {
  position: absolute;
  top: 12px;
  right: 16px;
  background: transparent; /* chắc chắn không có màu nền */
  border: none;            /* bỏ border */
  padding: 0;              /* xóa padding */
  margin: 0;               /* xóa margin */
  font-size: 28px;
  cursor: pointer;
  color: #555;
  transition: color 0.3s ease, transform 0.2s ease;
  box-shadow: none;        /* bỏ shadow nếu có */
  line-height: 1;          /* đảm bảo không dư khoảng cách dòng */
}
.close-btn:hover {
  color: #e74c3c;
  transform: scale(1.1);
  background-color: rgba(0, 0, 0, 0.05);
}



@keyframes scaleIn {
  0% {
    transform: scale(0.85);
    opacity: 0;
  }
  100% {
    transform: scale(1);
    opacity: 1;
  }
}

.popup-fade-enter-active,
.popup-fade-leave-active {
  transition: opacity 0.3s ease;
}

.popup-fade-enter-from,
.popup-fade-leave-to {
  opacity: 0;
}
.swal2-slide-in-right {
  animation: slide-in-right 0.5s ease-out;
}
.horizontal-layout {
  flex-direction: row;
  align-items: flex-start;
  gap: 20px;
  width: 600px;
  padding: 24px;
}

.popup-image {
  width: 180px;
  height: 180px;
  object-fit: cover;
  border-radius: 12px;
  box-shadow: 0 8px 20px rgba(0, 0, 0, 0.3);
}

.right-section {
  flex: 1;
  display: flex;
  flex-direction: column;
  gap: 12px;
}

.popup-text {
  text-align:left;
  padding-left:20%;
}

.popup-actions {
  align-items: flex-start;
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
.title{
  padding-right:10%;
}
.totalprice{
  padding-left:20%;
}
.chinhbut{
  padding-left:25%;
}

</style>
  
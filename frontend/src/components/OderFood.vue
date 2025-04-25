<template>
    <transition name="popup-fade">
      <div class="popup-overlay" @click.self="closePopup">
        <div class="popup-content">
          <img :src="food?.image" alt="Food Image" class="popup-image" />
          <div class="popup-text">
            <h2>{{ food?.name }}</h2>
            <p><strong>Giảm giá:</strong> {{ food?.discount }}%</p>
            <p><strong>Giá:</strong>{{food?.price}}</p>
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
  <p><strong>Tổng giá:</strong> {{ calculatedPrice }} VND</p>


</div>

          <div class="popup-actions">
            <button class="add-to-cart-btn" @click="addToCart">Thêm vào giỏ hàng</button>
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
  import { GetFoodById, GetRestaurantById } from '../api/FoodSevice.js';
 const addToCart = async() => {
   const senddata={
    IDFood:route.params.id,
    FoodName:food.value.name,
    RestaurantName:restaurant.value.name,
    IDRes:food.value.idRes,
    Quantity:quantityToAdd.value,
    TotalPrice:parseInt(calculatedPrice.value),
    Url_image:food.value.url_image,
    IDCustomer:localStorage.getItem("IDRes")
    };
     console.log(senddata);
     try {
    const result = await addOrder(senddata);
    console.log("Item added to cart:", result);
    alert("Thêm vào giỏ hàng thành công!");
  } catch (error) {
    alert("Đã xảy ra lỗi khi thêm vào giỏ hàng.");
  }
  };
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
  </script>
  
  <style scoped>
  .popup-overlay {
  position: fixed;
  inset: 0;
  background-color: rgba(0, 0, 0, 0.6);
  display: flex;
  justify-content: center;
  align-items: center;
  z-index: 9999;
}

.popup-content {
  background-color: #ffffff;
  padding: 24px;
  border-radius: 16px;
  width: 400px;
  max-width: 90%;
  position: relative;
  box-shadow: 0 12px 40px rgba(0, 0, 0, 0.2);
  animation: scaleIn 0.3s ease-out;
  display: flex;
  flex-direction: column;
  gap: 16px;
  font-family: 'Segoe UI', sans-serif;
}

.popup-image {
  width: 200px;
  height: 200px;
  object-fit: cover;
  border-radius: 12px;
  box-shadow: 0 6px 16px rgba(0, 0, 0, 0.15);
  align-self: center;
}


.popup-text {
  text-align: center;
}

.popup-text h2 {
  font-size: 28px;
  margin-bottom: 8px;
  color: #333;
}

.popup-text p {
  margin: 4px 0;
  font-size: 16px;
  color: #555;
}

.quantity-control {
  display: flex;
  align-items: center;
  justify-content: center;
  gap: 8px;
  margin-bottom: 10px;
}

.quantity-control button {
  width: 36px;
  height: 36px;
  border-radius: 8px;
  border: none;
  background-color: #f0f0f0;
  font-size: 20px;
  font-weight: bold;
  cursor: pointer;
  transition: background-color 0.2s ease;
}

.quantity-control button:hover {
  background-color: #ddd;
}

.quantity-control input {
  width: 60px;
  text-align: center;
  padding: 6px 10px;
  border: 1px solid #ccc;
  border-radius: 8px;
  font-size: 16px;
  outline: none;
}

.popup-actions {
  display: flex;
  flex-direction: column;
  align-items: center;
  gap: 14px;
}

.add-to-cart-btn {
  background-color: #ff5722;
  color: white;
  font-size: 16px;
  padding: 10px 24px;
  border: none;
  border-radius: 12px;
  cursor: pointer;
  transition: background-color 0.3s ease, transform 0.2s ease;
}

.add-to-cart-btn:hover {
  background-color: #e64a19;
  transform: scale(1.05);
}

.close-btn {
  position: absolute;
  top: 14px;
  right: 18px;
  background: transparent;
  border: none;
  font-size: 28px;
  cursor: pointer;
  color: #999;
  transition: color 0.2s, transform 0.2s;
}

.close-btn:hover {
  color: #ff3b3b;
  transform: scale(1.2);
}

.popup-actions p {
  font-size: 18px;
  font-weight: bold;
  color: #2e7d32;
}

/* Animation */
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

  </style>
  
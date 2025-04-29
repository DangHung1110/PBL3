<template> 
  <transition name="popup-fade">
    <div class="popup-overlay" @click.self="closePopup">
      <div class="popup-content">
        <img :src="food?.image" alt="Food Image" class="popup-image" />
        <div class="popup-text">
          <h2>{{ food?.name }}</h2>
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
          <p><strong>Tổng giá:</strong> {{ calculatedPrice.toLocaleString() }} VND</p>
        </div>

        <div class="popup-actions">
          <button class="add-to-cart-btn" @click="addToCart">Đặt Hàng</button>
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
    IDCustomer:localStorage.getItem("IDRes"),
    Status_User:"pending",
    Status_Restaurant:"pending",
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
  background: rgba(0, 0, 0, 0.6);
  display: flex;
  justify-content: center;
  align-items: center;
  z-index: 10000;
}

.popup-content {
  background: linear-gradient(135deg, #ffffff, #f7f7f7);
  padding: 15px;
  border-radius: 16px;
  width: 320px; /* ↓ nhỏ lại */
  max-width: 90%;
  position: relative;
  box-shadow: 0 10px 25px rgba(0, 0, 0, 0.15);
  display: flex;
  flex-direction: column;
  gap: 14px; /* ↓ giảm gap */
  animation: scaleIn 0.35s ease;
  font-family: 'Poppins', sans-serif;
}


.popup-image {
  width: 180px;
  height: 180px;
  object-fit: cover;
  border-radius: 16px;
  box-shadow: 0 8px 20px rgba(0, 0, 0, 0.25);
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
  font-size: 26px;
  margin-bottom: 8px;
  color: #2c3e50;
}

.popup-text p {
  margin: 6px 0;
  font-size: 15px;
  color: #555;
}

.quantity-control {
  display: flex;
  align-items: center;
  justify-content: center;
  gap: 10px;
}

.quantity-control button {
  width: 38px;
  height: 38px;
  border-radius: 10px;
  border: none;
  background: #f0f0f0;
  font-size: 20px;
  font-weight: bold;
  cursor: pointer;
  transition: background 0.2s, transform 0.2s;
}

.quantity-control button:hover {
  background: #ddd;
  transform: scale(1.1);
}

.quantity-control input {
  width: 65px;
  text-align: center;
  padding: 6px;
  border: 1px solid #ccc;
  border-radius: 10px;
  font-size: 16px;
}

.popup-actions {
  display: flex;
  flex-direction: column;
  align-items: center;
  gap: 12px;
}

.popup-actions p {
  font-size: 18px;
  font-weight: 600;
  color: #388e3c;
}

.add-to-cart-btn {
  background: linear-gradient(135deg, #ff5722, #e64a19);
  color: #fff;
font-size: 14px;
  padding: 8px 20px;
  border-radius: 10px;
  padding: 12px 28px;.popup-content {
  background: linear-gradient(135deg, #ffffff, #f7f7f7);

 
  width: 320px; /* ↓ nhỏ lại */
  max-width: 90%;
  position: relative;
  box-shadow: 0 10px 25px rgba(0, 0, 0, 0.15);
  display: flex;
  flex-direction: column;
  gap: 14px; /* ↓ giảm gap */
  animation: scaleIn 0.35s ease;
  font-family: 'Poppins', sans-serif;
}
.popup-content {
  background: linear-gradient(135deg, #ffffff, #f7f7f7);
  padding: 20px;
  border-radius: 16px;
  width: 320px; /* ↓ nhỏ lại */
  max-width: 90%;
  position: relative;
  box-shadow: 0 10px 25px rgba(0, 0, 0, 0.15);
  display: flex;
  flex-direction: column;
  gap: 14px; /* ↓ giảm gap */
  animation: scaleIn 0.35s ease;
  font-family: 'Poppins', sans-serif;
}

  border: none;
  border-radius: 14px;
  cursor: pointer;
  transition: background 0.3s, transform 0.2s;
}

.add-to-cart-btn:hover {

  background: linear-gradient(135deg, #e64a19, #d84315);
  transform: scale(1.05);
}

.close-btn {
  position: absolute;
  top: 14px;
  right: 18px;
  background: transparent;
  border: none;
  font-size: 30px;
  cursor: pointer;
  color: #aaa;
  transition: color 0.2s, transform 0.2s;
}

.close-btn:hover {
  color: #ff5252;
  transform: scale(1.2);
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
</style>
  
<template>
    <transition name="popup-fade">
      <div class="popup-overlay" @click.self="closePopup">
        <div class="popup-content">
          <img :src="food?.image" alt="Food Image" class="popup-image" />
          <div class="popup-text">
            <h2>{{ food?.name }}</h2>
            <p><strong>Giảm giá:</strong> {{ food?.discount }}%</p>
            <p><strong>Số lượng:</strong> {{ food?.quantity }}</p>
            <p><strong>Nhà hàng:</strong> {{ restaurant?.name }}</p>
            <p><strong>Địa chỉ:</strong> {{ restaurant?.address }}</p>
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
  import { onMounted, ref } from 'vue';
  import { useRoute, useRouter } from 'vue-router';
  import { GetFoodById, GetRestaurantById } from '../api/FoodSevice.js';   
  
  const route = useRoute();
  const router = useRouter();
  const food = ref(null);
  const restaurant = ref(null);
  
  const closePopup = () => {
    router.replace(`/customer/:username/foodlist`);
  };
  
  const addToCart = () => {
    // Logic to add item to cart
    console.log("Item added to cart");
  };
  
  onMounted(async () => {
    const foodId = route.params.id;
    const data = await GetFoodById(foodId);
    food.value = {
      ...data,
      image: data.url_Image
    };
  
    const resData = await GetRestaurantById(food.value.idRes);
    restaurant.value = resData;
    console.log("Restaurant data:", resData);
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
    background-color: #fff;
    padding: 30px;
    border-radius: 16px;
    width: 500px;
    max-width: 90%;
    position: relative;
    box-shadow: 0 8px 24px rgba(0, 0, 0, 0.2);
    animation: scaleIn 0.3s ease-out;
    display: flex;
    flex-direction: column;
    gap: 20px;
  }
  
  .popup-image {
    width: 250px;
    height: 250px;
    object-fit: cover;
    border-radius: 12px;
    box-shadow: 0 4px 12px rgba(0, 0, 0, 0.2);
    align-self: center;
  }
  
  .popup-text {
    text-align: center;
  }
  
  .popup-text h2 {
    font-size: 26px;
    margin-bottom: 10px;
  }
  
  .popup-text p {
    margin: 4px 0;
    font-size: 16px;
  }
  
  .popup-actions {
    display: flex;
    justify-content: center;
  }
  
  .add-to-cart-btn {
    background-color: #28a745;
    color: white;
    font-size: 16px;
    padding: 10px 20px;
    border: none;
    border-radius: 8px;
    cursor: pointer;
    transition: background-color 0.3s ease;
  }
  
  .add-to-cart-btn:hover {
    background-color: #218838;
  }
  
  .close-btn {
    position: absolute;
    top: 14px;
    right: 16px;
    background: transparent;
    border: none;
    font-size: 28px;
    cursor: pointer;
    color: #888;
    transition: color 0.2s;
  }
  
  .close-btn:hover {
    color: #ff5733;
  }
  
  /* Animation */
  @keyframes scaleIn {
    0% {
      transform: scale(0.8);
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
  
<template>
  <div class="drink-list">
    <div v-for="drink in drinks" :key="drink.id" class="drink-item">
      <img :src="drink.image" alt="Drink Image" class="drink-image" />
      <div class="drink-info">
        <h3 class="drink-name">{{ drink.name }}</h3>
        <p class="drink-shop">{{ drink.shop }}</p>
        <p class="drink-price">Giá: {{ drink.price }}đ</p>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref } from 'vue';

// Dữ liệu giả, sau này sẽ lấy từ database
const drinks = ref([
  { id: 1, name: "Trà Sữa Matcha", shop: "Tea House", price: 35000, image: "https://via.placeholder.com/150" },
  { id: 2, name: "Cà Phê Sữa", shop: "Coffee King", price: 25000, image: "https://via.placeholder.com/150" },
  { id: 3, name: "Nước Cam", shop: "Fresh Juice", price: 30000, image: "https://via.placeholder.com/150" },
  { id: 4, name: "Trà Sữa Matcha", shop: "Tea House", price: 35000, image: "https://via.placeholder.com/150" },
  { id: 5, name: "Cà Phê Sữa", shop: "Coffee King", price: 25000, image: "https://via.placeholder.com/150" },
  { id: 6, name: "Nước Cam", shop: "Fresh Juice", price: 30000, image: "https://via.placeholder.com/150" },
]);
</script>

<style scoped>
.drink-list {
  display: grid;
  grid-template-columns: repeat(2, 1fr); /* 2 cột */
  gap: 16px;
  width: 100%;
  max-width: 800px; /* Giới hạn chiều rộng */
  margin: 0 auto; /* Căn giữa */
  padding: 20px;
  transform: translateX(200px);
}

.drink-item {
  display: flex;
  align-items: center;
  width: 100%;
  padding: 16px;
  border-radius: 12px;
  background: #fff;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
  transition: transform 0.2s ease-in-out;
  height: 150px;
  width: calc(100% - 20px);
}

.drink-item:hover {
  transform: scale(1.02);
}

.drink-image {
  width: 120px;
  height: 120px;
  border-radius: 12px;
  object-fit: cover;
  margin-right: 20px;
}

.drink-info {
  display: flex;
  flex-direction: column;
  justify-content: center;
  flex: 1;
}

.drink-name {
  font-size: 22px;
  font-weight: bold;
}

.drink-shop {
  color: gray;
  font-size: 16px;
}

.drink-price {
  color: #ff5733;
  font-size: 18px;
  font-weight: bold;
}
</style>

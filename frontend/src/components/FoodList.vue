<template>
    <div class="food-list">
      <div v-for="food in foods" :key="food.id" class="food-item">
        <img :src="food.image" alt="food.name" class="food-image" />
        <div class="food-info">
          <h3 class="food-name">{{ food.restaurant }} - {{ food.name }}</h3>
          <p class="food-price">Giá: {{ food.price }}k</p>
        </div>
      </div>
    </div>
  </template>
  
  <script setup>
  import { ref } from "vue";
  
  const foods = ref([
  { id: 1, name: "Pizza Phô Mai", shop: "Italian Pizza", price: 120000, image: "../assets/doAn" },
  { id: 2, name: "Bánh Mì Thịt", shop: "Bánh Mì Sài Gòn", price: 30000, image: "../assets/doAn" },
  { id: 3, name: "Cơm Gà", shop: "Cơm Gà Hội An", price: 50000, image: "../assets/doAn" },
  { id: 4, name: "Bún Bò Huế", shop: "Quán Huế", price: 45000, image: "../assets/doAn" },
  { id: 5, name: "Phở Bò", shop: "Phở Hà Nội", price: 40000, image: "../assets/doAn" },
  { id: 6, name: "Gỏi Cuốn", shop: "Ẩm Thực Việt", price: 25000, image: "../assets/doAn" },
]);

  </script>
  
<style scoped>
  .food-list {
  display: grid;
  grid-template-columns: repeat(2, 1fr); /* 2 cột */
  gap: 16px;
  width: 100%;
  max-width: 800px; /* Giới hạn chiều rộng */
  margin: 0 auto; /* Căn giữa */
  padding: 20px;
  transform: translateX(200px);
}

.food-item {
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

.food-item:hover {
  transform: scale(1.02);
}

.food-image {
  width: 120px;
  height: 120px;
  border-radius: 12px;
  object-fit: cover;
  margin-right: 20px;
}

.food-info {
  display: flex;
  flex-direction: column;
  justify-content: center;
  flex: 1;
}

.food-name {
  font-size: 22px;
  font-weight: bold;
}

.food-shop {
  color: gray;
  font-size: 16px;
}

.food-price {
  color: #ff5733;
  font-size: 18px;
  font-weight: bold;
}
</style>
  
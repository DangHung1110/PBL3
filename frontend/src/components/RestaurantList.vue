<template>
    <div class="food-list-container">
      <div class="pagination">
        <button @click="prevPage" class="arrow-button left">&#8592;</button>
        <button @click="nextPage" class="arrow-button right">&#8594;</button>
      </div>
  
      <div class="food-list">
        <div class="food-item" v-for="res in paginatedRestaurants" :key="res.id">
          <img
            :src="res.image"
            class="food-image"
            alt="Restaurant Image"
            @error="handleImageError"
          />
          <div class="food-info">
            <div class="food-name">{{ res.name }}</div>
            <div class="food-shop">{{ res.address }}</div>
          </div>
        </div>
      </div>
    </div>
  </template>
  
  <script setup>
  import { ref, onMounted, computed } from 'vue';
  import  { getAllRes }  from '../api/restaurant.js'; // Import your API function
  
  const restaurants = ref([]);
  const currentPage = ref(1);
  const resPerPage = 6;
  
  onMounted(async () => {
  try {
    const data = await getAllRes();
    console.log('Danh sách nhà hàng:', data);
    if (Array.isArray(data)) {
      restaurants.value = data.map(item => ({
        id: item.idRes,
        name: item.name,
        address: item.address,
        image: item.url_Image
      }));
    } else {
      console.warn('Dữ liệu không hợp lệ:', data);
    }
  } catch (error) {
    console.error('Lỗi khi lấy danh sách nhà hàng:', error);
  }
});

  
  const paginatedRestaurants = computed(() => {
    const start = (currentPage.value - 1) * resPerPage;
    return restaurants.value.slice(start, start + resPerPage);
  });
  
  const totalPages = computed(() => {
    return Math.ceil(restaurants.value.length / resPerPage);
  });
  
  const nextPage = () => {
    if (currentPage.value < totalPages.value) currentPage.value++;
  };
  
  const prevPage = () => {
    if (currentPage.value > 1) currentPage.value--;
  };
  
  const handleImageError = (event) => {
    event.target.src = 'https://via.placeholder.com/120';
  };
  </script>
  
  <style scoped>
  .food-list-container {
    position: relative;
    padding: 20px;
    max-width: 1000px;
    margin: 0 auto;
  }
  
  .food-list {
    display: grid;
    grid-template-columns: repeat(2, 1fr);
    gap: 30px;
    transform: translateX(100px);
  }
  
  .food-item {
    width: 400px;
    display: flex;
    align-items: center;
    padding: 16px;
    border-radius: 12px;
    background: #fff;
    box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
    transition: transform 0.2s ease-in-out;
    height: 150px;
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
    min-width: 0;
    word-break: break-word;
    gap: 4px;
  }
  
  .food-name {
    font-size: 22px;
    font-weight: bold;
  }
  
  .food-shop {
    color: gray;
    font-size: 15px;
  }
  
  .pagination {
    position: relative;
    top: 50%;
    height: 0;
    transform: translateX(100px);
  }
  
  .arrow-button {
    position: absolute;
    top: -40px;
    background-color: white;
    border: 2px solid #ff5733;
    color: #ff5733;
    width: 40px;
    height: 40px;
    border-radius: 50%;
    font-size: 22px;
    display: flex;
    align-items: center;
    justify-content: center;
    box-shadow: 0 4px 8px rgba(0, 0, 0, 0.15);
    cursor: pointer;
    transition: all 0.2s ease;
  }
  
  .arrow-button:hover {
    background-color: #ff5733;
    color: white;
  }
  
  .arrow-button.left {
    left: -50px;
  }
  
  .arrow-button.right {
    right: -50px;
  }
  </style>
  
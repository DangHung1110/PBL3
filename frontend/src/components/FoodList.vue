<template>
  <div class="food-list-container">
    <div class="pagination">
      <button @click="prevPage" class="arrow-button left">&#8592;</button>
      <button @click="nextPage" class="arrow-button right">&#8594;</button>
    </div>

    <div class="food-list">
      <div class="food-item" v-for="food in paginatedFoods" :key="food.id" @click="handleFoodDetail(food.id)">
        <img
          :src="food.image"
          class="food-image"
          alt="Food Image"
          @error="handleImageError"
        />
        <div class="food-info">
          <div class="food-name">{{ food.name }}</div>
          <div class="food-shop">{{ food.restaurantId }}</div>
          <div class="bottom-info">
            <div class="food-price">Giá: {{ food.price }}đ</div>
            <div class="food-discount">Giảm giá: {{ food.discount }}%</div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>


<script setup>
import { ref, onMounted, onUnmounted, computed } from "vue";
import { getDoAn } from "../api/FoodSevice.js";
import { useRouter } from "vue-router";
import {emitter} from "../api/eventBus.js";
import {GetFoodByName} from '../api/FoodSevice.js'
const foods = ref([]);
const currentPage = ref(1);
const foodsPerPage = 6;
const router = useRouter();



const fetchAllFoods = async () => {
  const data = await getDoAn();
  foods.value = data.map(item => ({
    id: item.idFood,
    name: item.name,
    restaurant: item.idRes,
    price: item.price,
    discount: item.discount,
    image: item.url_Image
  }));
};

const fetchFoodsByName = async (searchText) => {
  const data = await GetFoodByName(searchText);
  foods.value = data.map(item => ({
    id: item.idFood,
    name: item.name,
    restaurant: item.idRes,
    price: item.price,
    discount: item.discount,
    image: item.url_Image
  }));
};

const handleSearch = async (searchText) => {
  currentPage.value = 1;
  if (!searchText || searchText.trim() === '') {
    await fetchAllFoods();
  } else {
    await fetchFoodsByName(searchText);
  }
};

onMounted(async () => {
  await fetchAllFoods();
  emitter.on('searchItem', handleSearch);
});

onUnmounted(() => {
  emitter.off('searchItem', handleSearch);
});
const paginatedFoods = computed(() => {
  const start = (currentPage.value - 1) * foodsPerPage;
  return foods.value.slice(start, start + foodsPerPage);
});

const totalPages = computed(() => {
  return Math.ceil(foods.value.length / foodsPerPage);
});

const nextPage = () => {
  if (currentPage.value < totalPages.value) currentPage.value++;
};

const prevPage = () => {
  if (currentPage.value > 1) currentPage.value--;
};

const formatPrice = (value) => {
  return value.toLocaleString("vi-VN") + "đ";
};

const handleFoodDetail = (id) => {
  router.replace(`/oderfood/${id}`);
  console.log("ID món ăn:", id);
};
</script>

  
<style scoped>
/* Cập nhật CSS phần <style scoped> */

.food-list-container {
  position: relative;
  padding: 32px;
  max-width: 1080px;
  margin: 0 auto;
  font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
 
  border-radius: 16px;
}

.food-list {
  margin-top: 24px;
  display: grid;
  grid-template-columns: repeat(2, 1fr);
  gap: 36px;
}

.food-item {
  display: flex;
  align-items: center;
  background: #ffffff;
  padding: 20px;
  border-radius: 16px;
  box-shadow: 0 6px 18px rgba(0, 0, 0, 0.08);
  transition: transform 0.25s ease, box-shadow 0.25s ease;
  cursor: pointer;
}

.food-item:hover {
  transform: scale(1.03);
  box-shadow: 0 8px 24px rgba(0, 0, 0, 0.12);
}

.food-image {
  width: 110px;
  height: 110px;
  border-radius: 12px;
  object-fit: cover;
  margin-right: 24px;
  border: 1px solid #eee;
}

.food-info {
  display: flex;
  flex-direction: column;
  justify-content: space-between;
  flex: 1;
  gap: 6px;
}

.food-name {
  font-size: 20px;
  font-weight: 600;
  color: #333;
}

.food-shop {
  color: #888;
  font-size: 15px;
}

.bottom-info {
  display: flex;
  justify-content: space-between;
  align-items: center;
}

.food-price {
  color: #d62828;
  font-size: 17px;
  font-weight: 600;
}

.food-discount {
  color: #0077cc;
  font-size: 15px;
  font-weight: 500;
}

.pagination {
  position: relative;
  height: 0;
}

.arrow-button {
  position: absolute;
  top: 250px;
  width: 44px;
  height: 44px;
  font-size: 20px;
  background-color: #ffffff;
  border: 2px solid #343a40;
  color: #343a40;
  border-radius: 50%;
  box-shadow: 0 4px 12px rgba(0, 0, 0, 0.1);
  display: flex;
  justify-content: center;
  align-items: center;
  transition: all 0.2s ease;
  z-index: 10;
}

.arrow-button:hover {
  background-color:#ff5733;
  color: #fff;
}

.arrow-button.left {
  left: -52px;
}

.arrow-button.right {
  right: -52px;
}

</style>

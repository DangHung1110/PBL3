<template>
  
</template>

<script setup>

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
  justify-content: space-between;
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

.food-price {
  color: #ff5733;
  font-size: 18px;
  font-weight: bold;
}

.food-discount {
  color: #007bff;
  font-size: 16px;
  font-weight: 500;
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

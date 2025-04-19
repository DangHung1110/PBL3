<template>
  <div class="popup-overlay" @click.self="closePopup">
    <div class="add-food-container">
      <button class="close-btn" @click="closePopup">×</button>
      <h2>Thêm món ăn mới</h2>
      <form @submit.prevent="handleSubmit" class="form">
        <div class="form-group">
          <label for="name">Tên món ăn</label>
          <input type="text" id="name" v-model="name" required />
        </div>

        <div class="form-group">
          <label for="price">Giá (VNĐ)</label>
          <input type="number" id="price" v-model="price" required />
        </div>

        <div class="form-group">
          <label for="discount">Khuyến mãi (%)</label>
          <input type="number" id="discount" v-model="discount" />
        </div>

        <div class="form-group">
          <label for="category">Loại</label>
          <select id="category" v-model="category" required>
            <option value="">-- Chọn loại --</option>
            <option value="Đồ ăn">Đồ ăn</option>
            <option value="Đồ uống">Đồ uống</option>
          </select>
        </div>

        <div class="form-group">
          <label for="quantity">Số lượng</label>
          <input type="number" id="quantity" v-model="quantity" min="0" required />
        </div>

        <div class="form-group">
          <label for="image">Ảnh</label>
          <input type="file" id="image" accept="image/*" @change="handleImageChange" required />
          <img v-if="imagePreview" :src="imagePreview" class="image-preview" />
        </div>

        <button type="submit" class="submit-btn">Lưu món ăn</button>
      </form>
    </div>
  </div>
</template>

<script setup>
import { ref } from 'vue';
import router from '../router';
import { addFood } from '../api/FoodSevice.js'; // Import your API function

const name = ref('');
const price = ref('');
const discount = ref('');
const category = ref('');
const image = ref(null);
const quantity = ref(0);
const imagePreview = ref(null);
const emit = defineEmits(['close']);

const handleImageChange = (event) => {
  const file = event.target.files[0];
  if (file) {
    image.value = file;
    imagePreview.value = URL.createObjectURL(file);
  }
};

const handleSubmit = async () => {
  const ID = localStorage.getItem('IDRes');
  if (!ID) {
    alert("Bạn chưa đăng nhập!");
    return;
  }

  const foodData = {
    name: name.value,
    price: price.value,
    discount: discount.value,
    category: category.value,
    quantity: quantity.value,
    image: image.value,
    idRes: ID
  };

  try {
    await addFood(foodData);
    alert("Đã thêm món ăn thành công!");
    emit('close');

    setTimeout(() => {
      router.push({ path: '/restaurant/dashboard/product', query: { reload: Date.now() } });
    }, 30);

  } catch (error) {
    console.error(error);
    alert("Thêm món ăn thất bại!");
  }
};


const closePopup = () => {
  router.push('/restaurant/dashboard/product');
};
</script>

<style scoped>
.popup-overlay {
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background: rgba(0, 0, 0, 0.4);
  display: flex;
  justify-content: center;
  align-items: center;
  z-index: 9999;
}

.add-food-container {
  background: #fff;
  padding: 24px;
  border-radius: 12px;
  box-shadow: 0 0 12px rgba(0, 0, 0, 0.2);
  max-width: 600px;
  width: 100%;
  position: relative;
}

.close-btn {
  position: absolute;
  top: 12px;
  right: 12px;
  background: transparent;
  border: none;
  font-size: 24px;
  cursor: pointer;
  color: #888;
}

.close-btn:hover {
  color: #333;
}

.form-group {
  margin-bottom: 16px;
}

label {
  display: block;
  margin-bottom: 6px;
  color: #444;
}

input,
select {
  width: 100%;
  padding: 10px;
  font-size: 16px;
  border: 1px solid #ccc;
  border-radius: 8px;
}

.image-preview {
  margin-top: 10px;
  width: 100px;
  height: 100px;
  object-fit: cover;
  border-radius: 6px;
  border: 1px solid #ccc;
}

.submit-btn {
  background-color: #28a745;
  color: white;
  padding: 12px 20px;
  font-size: 16px;
  border: none;
  border-radius: 8px;
  cursor: pointer;
}

.submit-btn:hover {
  background-color: #218838;
}
</style>

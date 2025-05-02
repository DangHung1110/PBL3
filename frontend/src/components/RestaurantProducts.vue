<template>
  <div class="popup-container">
    <main class="content">
      <h2 class="title">Danh mục sản phẩm</h2>

      <div class="add-product-btn-container">
        <button @click="addProduct" class="btn-add-product">+ Thêm món ăn</button>
      </div>

      <div class="table-wrapper">
        <table class="product-table">
          <thead>
            <tr>
              <th>ID</th>
              <th>Tên</th>
              <th>Loại</th>
              <th>Giá</th>
              <th>KM</th>
              <th>Số lượng</th>
              <th>Ảnh</th>
              <th>Hành động</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="product in products" :key="product.id">
              <td>{{ product.id }}</td>
              <td>{{ product.name }}</td>
              <td>{{ product.category }}</td>
              <td>{{ product.price }}</td>
              <td>{{ product.discount }}</td>
              <td>{{ product.quantity }}</td>
              <td><img :src="product.image" alt="product image" class="product-image" /></td>
              <td class="action-buttons">
                <button @click="editProduct(product)" class="btn-edit">Sửa</button>
                <button @click="deleteProduct(product.id)" class="btn-delete">Xóa</button>
              </td>
            </tr>
          </tbody>
        </table>
      </div>

      <UpdateFoodPopup
        v-if="showPopup"
        :visible="showPopup"
        :food="selectedProduct"
        @close="showPopup = false"
        @updated="fetchFoods"
      />
    </main>
  </div>
</template>

<script setup>
import { ref, watch, onMounted } from 'vue';
import { useRouter, useRoute } from 'vue-router';
import { getFoodsByRestaurantId, deleteFood } from '../api/FoodSevice.js'; 
import UpdateFoodPopup from '../components/UpdateFood.vue'; // Import the popup component

const router = useRouter();
const products = ref([]);
const showPopup = ref(false);
const selectedProduct = ref(null);
const route = useRoute();

const fetchFoods = async () => {
  const IDRes = localStorage.getItem("IDRes");
  try {
    const data = await getFoodsByRestaurantId(IDRes);
    products.value = data.map(item => ({
      id: item.idFood,
      name: item.name,
      category: item.category,
      price: item.price.toLocaleString('vi-VN') + 'đ',
      discount: item.discount + '%',
      image: item.url_Image,
      quantity: item.quantity
    }));
  } catch (error) {
    console.error("Lỗi khi lấy danh sách món ăn:", error);
  }
};

const deleteProduct = async (id) => {
  if (confirm("Bạn có chắc muốn xoá món này không?")) {
    try {
      await deleteFood(id);
      products.value = products.value.filter(p => p.id !== id);
    } catch (error) {
      alert("Có lỗi xảy ra khi xoá món ăn.");
    }
  }
};

const addProduct = () => {
  router.push({ path: '/restaurant/dashboard/product/addFood' });
};

const editProduct = (product) => {
  selectedProduct.value = product;
  showPopup.value = true;
};

onMounted(fetchFoods);

watch(() => route.query.reload, () => {
  fetchFoods(); // Gọi lại API khi query `reload` thay đổi
});
</script>
<style scoped>
.popup-container {
  display: flex;
  justify-content: center;
  align-items: center;
  height: auto;
  background: #f0f2f5; /* Nền nhẹ nhàng */
  padding: 40px 0;
}

.content {
  width: 90%;
  max-width: 1200px;
  padding: 30px;
  background: #ffffff;
  border-radius: 16px;
  box-shadow: 0 12px 24px rgba(0, 0, 0, 0.1);
  overflow-y: auto;
  font-family: 'Poppins', 'Segoe UI', sans-serif;
}

.title {
  font-size: 28px;
  font-weight: 600;
  color: #2c3e50;
  margin-bottom: 25px;
  text-align: center;
}

.add-product-btn-container {
  display: flex;
  justify-content: flex-end;
  margin-bottom: 25px;
}

.btn-add-product {
  background-color: #4CAF50;
  color: white;
  padding: 10px 24px;
  border-radius: 8px;
  font-size: 16px;
  cursor: pointer;
  border: none;
  transition: background 0.3s, transform 0.2s;
}

.btn-add-product:hover {
  background-color: #45a049;
  transform: scale(1.05);
}

.product-table {
  width: 100%;
  border-collapse: separate;
  border-spacing: 0 12px;
}

.product-table th, .product-table td {
  padding: 14px 18px;
  text-align: center;
  background: white;
}

.product-table th {
  background-color: #34495e;
  color: white;
  font-weight: 600;
  font-size: 14px;
  text-transform: uppercase;
  border: none;
}

.product-table td {
  background-color: #ecf0f1;
  color: #2c3e50;
  font-size: 15px;
  border-bottom: 2px solid #dfe6e9;
  vertical-align: middle;
}

.product-table tr:hover td {
  background-color: #dfe6e9;
}

.product-image {
  width: 60px;
  height: 60px;
  object-fit: cover;
  border-radius: 8px;
  box-shadow: 0 2px 6px rgba(0, 0, 0, 0.15);
}

/* Các nút trong bảng */
button {
  padding: 8px 14px;
  font-size: 14px;
  border-radius: 8px;
  cursor: pointer;
  transition: all 0.3s;
  border: none;
}

.btn-edit {
  background-color: #f39c12;
  color: white;
}

.btn-edit:hover {
  background-color: #e67e22;
}

.btn-delete {
  background-color: #e74c3c;
  color: white;
}

.btn-delete:hover {
  background-color: #c0392b;
}

/* Responsive */
@media (max-width: 768px) {
  .product-table th, .product-table td {
    padding: 10px;
    font-size: 13px;
  }

  .popup-container {
    padding: 20px;
  }
}
</style>

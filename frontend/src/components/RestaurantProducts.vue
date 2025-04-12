<template>
  <div class="popup-container">
    <main class="content">
      <h2 class="title">Danh mục sản phẩm</h2>

      <!-- Nút Thêm Món Ăn -->
      <div class="add-product-btn-container">
        <button @click="addProduct" class="btn-add-product">Thêm món ăn</button>
      </div>

      <!-- Bảng sản phẩm -->
      <table class="product-table">
        <thead>
          <tr>
            <th>ID</th>
            <th>Tên</th>
            <th>Loại</th>
            <th>Giá</th>
            <th>KM</th>
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
            <td><img :src="product.image" alt="product image" class="product-image" /></td>
            <td>
              <button @click="editProduct(product.id)" class="btn-edit">Sửa</button>
              <button @click="deleteProduct(product.id)" class="btn-delete">Xóa</button>
            </td>
          </tr>
        </tbody>
      </table>

      <RouterView />
    </main>
  </div>
</template>


<script setup>
import { ref, watch, onMounted} from 'vue';
import { useRouter, useRoute } from 'vue-router';
import { getFoodsByRestaurantId, deleteFood } from '../api/FoodSevice.js'; // Import your API function

const router = useRouter();
const products = ref([]);
const route = useRoute();

const fetchFoods = async () => {
  const IDRes = localStorage.getItem("IDRes");
  console.log("IDRes:", IDRes); // log IDRes để kiểm tra
  try {
    const data = await getFoodsByRestaurantId(IDRes);
    console.log("Dữ liệu từ API:", data); // log kết quả

    products.value = data.map(item => ({
      id: item.idFood,
      name: item.name,
      category: item.category,
      price: item.price.toLocaleString('vi-VN') + 'đ',
      discount: item.discount + '%',
      image: item.url_Image
    }));
  } catch (error) {
    console.error("Lỗi khi lấy danh sách món ăn:", error);
  }
};

const deleteProduct = async (id) => {
  console.log("Đang xoá món:", id); // kiểm tra có bấm đúng không

  if (confirm("Bạn có chắc muốn xoá món này không?")) {
    try {
      await deleteFood(id);
      products.value = products.value.filter(p => p.id !== id);
      console.log("Xoá thành công");
    } catch (error) {
      console.error("Xoá thất bại:", error);
      alert("Có lỗi xảy ra khi xoá món ăn.");
    }
  }
};

const addProduct = () => {
  router.push({ path: '/restaurant/dashboard/product/addFood' });
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
  background: rgba(0, 0, 0, 0.3); /* Background overlay */
  padding: 40px 0;
}

.content {
  width: 90%;
  max-width: 1200px;
  padding: 20px;
  background: white;
  border-radius: 12px;
  box-shadow: 0 8px 16px rgba(0, 0, 0, 0.1);
  overflow-y: auto;
}

.title {
  font-size: 24px;
  font-weight: bold;
  color: #333;
  margin-bottom: 20px;
}

.add-product-btn-container {
  display: flex;
  justify-content: flex-end;
  margin-bottom: 20px;
}

.btn-add-product {
  background-color: #007bff;
  color: white;
  padding: 10px 20px;
  border-radius: 6px;
  font-size: 16px;
  cursor: pointer;
  transition: background-color 0.3s;
}

.btn-add-product:hover {
  background-color: #0056b3;
}

.product-table {
  width: 100%;
  border-collapse: collapse;
  margin-top: 20px;
}

.product-table button {
  margin-left: 10px;
}

.product-table th, .product-table td {
  padding: 12px 15px;
  text-align: center;
  border-bottom: 1px solid #ddd;
  color: #555;
}

.product-table th {
  background-color: #007bff;
  color: white;
  text-transform: uppercase;
}

.product-table td {
  background-color: #f9f9f9;
}

.product-table tr:hover {
  background-color: #f1f1f1;
}

.product-image {
  width: 50px;
  height: 50px;
  object-fit: cover;
  border-radius: 6px;
}

button {
  padding: 8px 12px;
  font-size: 14px;
  border-radius: 6px;
  cursor: pointer;
  transition: all 0.3s;
  border: none;
}

.btn-detail {
  background-color: #28a745;
  color: white;
}

.btn-detail:hover {
  background-color: #218838;
}

.btn-edit {
  background-color: #ffc107;
  color: white;
}

.btn-edit:hover {
  background-color: #e0a800;
}

.btn-delete {
  background-color: #dc3545;
  color: white;
}

.btn-delete:hover {
  background-color: #c82333;
}

@media (max-width: 768px) {
  .product-table th, .product-table td {
    padding: 8px;
    font-size: 12px;
  }
  
  .popup-container {
    padding: 10px;
  }
}
</style>

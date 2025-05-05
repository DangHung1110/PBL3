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
      <RouterView />
    </main>
  </div>
</template>



<script setup>
import { ref, watch, onMounted} from 'vue';
import { useRouter, useRoute } from 'vue-router';
import { getFoodsByRestaurantId, deleteFood } from '../api/FoodSevice.js'; 
import UpdateFoodPopup from '../components/UpdateFood.vue';
import Swal from 'sweetalert2';
const router = useRouter();
const products = ref([]);
const route = useRoute();
const selectedProduct = ref(null);

const showPopup = ref(false);
const editProduct = (product) => {
  selectedProduct.value = product;
  showPopup.value = true;
};
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
      image: item.url_Image,
      quantity: item.quantity
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
      Swal.fire({
      toast: true,   
    icon: 'success',
    title: 'THÔNG BÁO',
    text: 'Xóa hàng thành công!',
    timer: 3000,
    position: 'bottom-end',
    timerProgressBar: true,
    showConfirmButton: false,
    showClass: {
      popup: 'swal2-slide-in-right' }}
    );
    } catch (error) {
      console.error("Xoá thất bại:", error);
      Swal.fire({
      toast: true,   
    icon: 'error',
    title: 'THÔNG BÁO',
    text: 'Không thể xóa loại này do vẫn còn khách đang đặt!',
    timer: 3000,
    position: 'bottom-end',
    timerProgressBar: true,
    showConfirmButton: false,
    showClass: {
      popup: 'swal2-slide-in-right' }}
    );
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

.btn-edit, .btn-delete {
  padding: 6px 16px;
  border-radius: 4px;
  font-weight: 500;
  font-size: 14px;
  border: 1px solid transparent;
  cursor: pointer;
  transition: all 0.25s ease;
  box-shadow: 0 1px 2px rgba(0,0,0,0.05);
}

/* Nút Sửa - màu cam Shopee */
.btn-edit {
  background-color: #f57224;
  color: white;
  border-color: #f57224;
  margin-right: 12px;
}

.btn-edit:hover {
  background-color: white;
  color: #f57224;
  border: 1px solid #f57224;
}

/* Nút Xóa - màu đỏ Shopee */
.btn-delete {
  background-color: #ee4d2d;
  color: white;
  border-color: #ee4d2d;
}

.btn-delete:hover {
  background-color: white;
  color: #ee4d2d;
  border: 1px solid #ee4d2d;
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
.swal2-slide-in-right {
  animation: slide-in-right 0.5s ease-out;
}

@keyframes slide-in-right {
  from {
    transform: translateX(100%);
    opacity: 0;
  }
  to {
    transform: translateX(0);
    opacity: 1;
  }
}
</style>

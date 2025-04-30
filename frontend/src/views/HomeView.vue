<template>
  <div>
    <!-- Hiển thị Navbar nếu không phải trang dashboard -->
    <div class="container" v-if="!$route.path.includes('/restaurant/dashboard')">
      <div class="navbar">
        <div class="logo">
          <div class="logo-food"></div>
          <div class="Name">GRAP FOOD</div>
        </div>

        <div class="home-page">
          <button class="category food" @click="navigateTo('/foodlist')">🍔 Đồ ăn</button>
          <button class="category drink" @click="navigateTo('/drinklist')">🥤 Đồ uống</button>
          <input  type="text" v-model="searchQuery" placeholder="Tìm kiếm món ăn..." class="search-input"    @keydown.enter="emitSearch">
        </div>

        <div v-if="isLoggedIn" class="user-menu">
          <div class="user-info" @click="toggleDropdown">
            <div class="avatar">{{ username.charAt(0).toUpperCase() }}</div>
            <span class="username">{{ username }}</span>
          </div>
          <div v-if="dropdownOpen" class="dropdown-menu">
            <button @click="openOrderHistoryPopup" class="dropdown-item">📦 Lịch sử đơn đặt hàng</button>
            <button @click="handleLogout" class="dropdown-item logout">🚪 Đăng xuất</button>
          </div>
        </div>

        <div v-else class="login">
          <button class="login-button">
            <font-awesome-icon icon="user" style="font-size: 20px; color: white;" />
          </button>
          <div class="boder-login">
            <router-link to="/login" class="loginn">Đăng nhập</router-link>
            <router-link to="/register" class="register">Đăng ký</router-link>
          </div>
        </div>
      </div>

      <div class="content">
        <router-view></router-view>
      </div>

      <div v-if="orderHistoryPopupOpen" class="popup">
        <div class="popup-content" style="max-height: 80vh; overflow-y: auto;">
          <h2>Đơn hàng</h2>
          <button class="close-button" @click="closeOrderHistoryPopup" aria-label="Đóng popup">
            <span>&times;</span>
          </button>

          <table class="order-table" style="width: 100%; border-collapse: collapse; text-align: left;">
            <thead>
              <tr>
                <th>Tên món</th>
                <th>Nhà hàng</th>
                <th>Hình ảnh</th>
                <th>Số lượng</th>
                <th>Tổng tiền</th>
                <th>Trạng thái</th>
                <th>Hành động</th>
                <th>Thời gian đặt hàng</th>
                <th>
                  Thời gian xác nhận
                </th>
               </tr>
            </thead>
            <tbody>
              <tr v-for="(item, index) in thongkedata" :key="index" style="border-top: 1px solid #ccc;">
                <td>{{ item.FoodName }}</td>
                <td>{{ item.RestaurantName }}</td>
                <td>
                  <img :src="item.Url_image" alt="Ảnh món ăn" style="width: 80px; border-radius: 8px;" />
                </td>
                <td>{{ item.Quantity }}</td>
                <td>{{ item.TotalPrice}} VNĐ</td>
                <td>
                  <button class="confirm-btn" v-if="item.Status_Restaurant === 'confirmed'" @click="FinishedOrder(item.id,item.TotalPrice,item.IDRes)">Xác nhận</button>
                </td>
                <td>
                  <button class="delete-btn" @click="DeleteFFromOD(item.id)">Xóa</button>
                </td>
                <td>{{formatDate(item.OrderTime)}}</td>
                <td v-if="item.Status_Restaurant==='confirmed'">{{formatDate(item.OrderConfirmedTime)}}</td>
              </tr>
            </tbody>
          </table>
        </div>
      </div>

      <div class="footer"></div>
    </div>
  </div>
</template>




<script setup>
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome'
import { faUser } from '@fortawesome/free-solid-svg-icons'
import { library } from '@fortawesome/fontawesome-svg-core'
import { ref, onMounted, onUnmounted, computed } from "vue";
import { useRouter, useRoute } from "vue-router";
import { ThongkeOrder} from "../api/order.js";
import {DeleteOrder} from "../api/order.js";
import {Thongke} from "../api/thongke.js";
import Swal from 'sweetalert2';
import {emitter} from "../api/eventBus.js";
const orderHistoryPopupOpen = ref(false);
const router = useRouter();
const route = useRoute();
const isLoggedIn = ref(false);
const username = ref("");
const dropdownOpen = ref(false);
const searchQuery = ref('');
const formatDate = (datetime) => {
  return new Date(datetime).toLocaleString("vi-VN");
};
const thongkedata = ref([]);

const goToFoodPage = () => {
  emitter.emit('searchItem', ''); 
};
const emitSearch = () => {
  emitter.emit('searchItem', searchQuery.value);
};
const DeleteFFromOD=async(id)=>{
console.log(id);
  try {    console.log(id);
      await DeleteOrder(id);
      await getThongkeData();
thongkedata.value = thongkedata.value.filter(p => p.id !== id);

      console.log("Xoá thành công");
    } catch (error) {
      console.error("Xoá thất bại:", error);
      alert("Có lỗi xảy ra khi xoá món ăn.");
    }
  }
// Hàm lấy thống kê từ API
const getThongkeData = async () => {
const ID = localStorage.getItem("IDRes");
  try {
    if (!ID) {
      alert("Bạn chưa đăng nhập!");
      return;
    }
    const data = await ThongkeOrder(ID);
    thongkedata.value = data.map(item => ({
      IDRes:item.idRes,
      Status_Restaurant:item.status_Restaurant,
      OrderConfirmedTime:item.orderConfirmedTime,
      OrderTime:item.orderTime,
      id: item.idOrder,
      FoodName: item.foodName,
      RestaurantName: item.restaurantName,
      Quantity: item.quantity,
      TotalPrice: item.totalPrice,
      Url_image: item.url_image
    }));
    console.log(thongkedata.value);
  } catch (error) {
    console.error("Lỗi khi lấy dữ liệu thống kê:", error);
    thongkedata.value = [];
  }
};

// Mở popup và gọi API
const openOrderHistoryPopup = async () => {
  orderHistoryPopupOpen.value = true;
  await getThongkeData();
};

const closeOrderHistoryPopup = () => {
  orderHistoryPopupOpen.value = false;
};

const toggleDropdown = () => {
  dropdownOpen.value = !dropdownOpen.value;
};

const handleLogout = () => {
  localStorage.removeItem("IDRes");
  localStorage.removeItem("role");
  localStorage.removeItem("username");
  isLoggedIn.value = false;
  username.value = "";
  router.replace("/login");
};

const navigateTo = (path) => {
  if (path === '/foodlist') {
    searchQuery.value = ''; 
    emitter.emit('searchItem', ''); 
  }
  router.push(path);
};

const checkLogin = () => {
  const storedUsername = localStorage.getItem("username");
  isLoggedIn.value = !!storedUsername;
  username.value = storedUsername || "";
};
const FinishedOrder=async(idOrder,TotalPrice,IDRes)=>{
  console.log(idOrder,TotalPrice,IDRes);
  await DeleteOrder(idOrder);
  const senddata={
    IDRes:IDRes,
    DOANHSO:TotalPrice,
    IDOrder:idOrder
  }
  try{
    await Thongke(senddata);
    Swal.fire({
      toast: true,
    icon: 'success',
    title: 'THÔNG BÁO',
    text: 'Xác nhận thành công!',
    timer: 3000,
    position: 'bottom-end',
    timerProgressBar: true,
    showConfirmButton: false,
    showClass: {
      popup: 'swal2-slide-in-right' }}
    );
  }
  catch(error){
    toast: true,   
    console.error("Xác nhận thất bại:", error);
    Swal.fire({
    icon: 'error',
    title: 'Lỗi',
    position: 'bottom-end',
    text: 'Có lỗi xảy ra khi xác nhận!',
  });
  }
  await getThongkeData();

 thongkedata.value = thongkedata.value.filter(p => p.id !== id);}
onMounted(() => {
  checkLogin();
  window.addEventListener("storage", checkLogin);
});
</script>


<style scoped>

* {
  font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
  box-sizing: border-box;
}

.container {
  width: 100vw;
  height: 100vh;
  background: url("https://watermark.lovepik.com/photo/20211118/large/lovepik-gourmet-background-picture_400152283.jpg") no-repeat center center/cover;
}

.navbar {
  width: 100%;
  height: 70px;
  display: flex;
  justify-content: space-between;
  align-items: center;
  position: fixed;
  z-index: 10;
  background-color: rgba(0, 0, 0, 0.6);
  color: white;
  padding: 0 30px;
  backdrop-filter: blur(6px);
}

.navbar .logo {
  display: flex;
  align-items: center;
  font-size: 22px;
  font-weight: bold;
  gap: 10px;
}

.logo-food i {
  font-size: 24px;
  color: #ff7f50;
}

.Name {
  letter-spacing: 1px;
  color: white;
}

.home-page {
  display: flex;
  gap: 15px;
  align-items: center;
}

.category {
  padding: 10px 18px;
  border-radius: 8px;
  border: none;
  cursor: pointer;
  font-size: 16px;
  font-weight: 500;
  transition: 0.3s;
  box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);
}

.food {
  background: linear-gradient(to right, #ff7e5f, #feb47b);
  color: white;
  padding: 9px 28px;
  font-size: 16px;
  font-weight: 600;
  border-radius: 12px;
  box-shadow: 0 4px 12px rgba(0, 0, 0, 0.15);
}

.drink {
  background: linear-gradient(to right, #4facfe, #00f2fe);
  color: white;
  padding: 9px 28px;
  font-size: 16px;
  font-weight: 600;
  border-radius: 12px;
  box-shadow: 0 4px 12px rgba(0, 0, 0, 0.15);
}

.food:hover,
.drink:hover,
.category:hover {
  transform: translateY(-2px);
  box-shadow: 0 8px 24px rgba(0, 0, 0, 0.2);
  opacity: 0.9;
}

.search-input {
  padding: 8px 14px;
  font-size: 16px;
  border: none;
  border-radius: 8px;
  outline: none;
  width: 280px;
  box-shadow: 0 2px 6px rgba(0, 0, 0, 0.15);
}

.search-input:focus {
  border: 2px solid #ff7f50;
}

.login {
  position: relative;
  display: flex;
  align-items: center;
  gap: 10px;
  cursor: pointer;
  right: 10px;
  left: auto;
}

.login-button {
  width: 50px;
  height: 50px;
  display: flex;
  align-items: center;
  justify-content: center;
  border: none;
  border-radius: 50%;
  background-color: transparent;
  color: white;
  font-size: 20px;
  font-weight: bold;
  cursor: pointer;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.2);
}

.login-button:hover {
  background: linear-gradient(135deg, #ff5722, #ff7f50);
  transform: scale(1.05);
}

.boder-login {
  position: absolute;
  top: 110%;
  left: 50%;
  transform: translateX(-50%);
  display: flex;
  flex-direction: column;
  gap: 6px;
  align-items: center;
  padding: 14px;
  width: 180px;
  background: rgba(255, 255, 255, 0.25);
  backdrop-filter: blur(12px);
  border-radius: 12px;
  border: 1px solid rgba(255, 255, 255, 0.3);
  box-shadow: 0px 6px 16px rgba(0, 0, 0, 0.2);
  opacity: 0;
  visibility: hidden;
  transition: 0.3s ease-in-out;
  z-index: 20;
}

.login:hover .boder-login {
  opacity: 1;
  visibility: visible;
}

.loginn,
.register {
  text-decoration: none;
  font-size: 15px;
  padding: 8px 12px;
  border-radius: 6px;
  transition: 0.3s;
  width: 100%;
  text-align: center;
  font-weight: 500;
}

.loginn {
  color: #6200ea;
  border: 1px solid #6200ea;
  background: transparent;
}

.loginn:hover {
  background: #6200ea;
  color: white;
}

.register {
  color: white;
  background: #ff7f50;
  border: none;
}

.register:hover {
  background: #e66a3e;
}

.user-menu {
  position: relative;
  cursor: pointer;
}

.user-info {
  display: flex;
  align-items: center;
  gap: 10px;
  padding: 8px 12px;
  border-radius: 8px;
  font-weight: bold;
  transition: 0.3s;
}

.user-info:hover {
  background-color: rgba(255, 255, 255, 0.1);
}

.avatar {
  width: 42px;
  height: 42px;
  border-radius: 50%;
  background: linear-gradient(135deg, #007bff, #0056b3);
  color: white;
  font-size: 18px;
  display: flex;
  align-items: center;
  justify-content: center;
  box-shadow: 0 2px 6px rgba(0, 0, 0, 0.2);
}

.dropdown-menu {
  position: absolute;
  top: 100%;
  right: 0;
  background: rgba(255, 255, 255, 0.95);
  border-radius: 12px;
  width: 260px;
  box-shadow: 0 10px 25px rgba(0, 0, 0, 0.15);
  z-index: 100;
  overflow: hidden;
  padding: 10px 0;
  backdrop-filter: blur(8px);
}

.dropdown-item {
  display: flex;
  align-items: center;
  gap: 12px;
  padding: 14px 20px;
  font-size: 15px;
  font-weight: 500;
  color: #333;
  background-color: transparent;
  border: none;
  width: 100%;
  text-align: left;
  cursor: pointer;
  transition: background-color 0.3s, color 0.3s;
}

.dropdown-item:hover {
  background-color: #f0f2f5;
  color: #007bff;
}

.logout {
  color: #d32f2f;
}

.logout:hover {
  background-color: rgba(211, 47, 47, 0.1);
  color: #b71c1c;
}

.content {
  width: 100%;
  height: calc(100vh - 70px);
  transform: translateY(70px);
  padding: 20px;
  overflow-y: auto;
  background: rgba(255, 255, 255, 0.1);
}

.popup {
  position: fixed;
  top: 0;
  left: 0;
  width: 100vw;
  height: 100vh;
  background: rgba(0, 0, 0, 0.4);
  display: flex;
  align-items: center;
  justify-content: center;
  z-index: 999;
}

.popup-content {
  background: linear-gradient(145deg, #fffaf0, #f7efe5); /* trắng kem ánh vàng */
box-shadow: 0 10px 30px rgba(0, 0, 0, 0.15);
border-radius: 20px;
  padding: 24px;
  max-width: 1000px;
  margin: 32px auto;
  font-family: 'Segoe UI', sans-serif;
  position: relative;
}

.popup-content h2 {
  color: #2c3e50; /* xanh than đậm */
font-weight: 700;
font-size: 1.5rem;
  text-align: center;
}

@keyframes fadeIn {
  from {
    transform: translateY(-20px);
    opacity: 0;
  }
  to {
    transform: translateY(0);
    opacity: 1;
  }
}

.close-button {
  position: absolute;
  top: 10px;
  right: 10px;
  background: transparent;
  border: none;
  font-size: 22px;
  font-weight: bold;
  cursor: pointer;
  z-index: 1000;
  color: #333;
  transition: 0.2s ease-in-out;
}

.close-button:hover {
  color: #e53935;
  transform: scale(1.2);
}

.order-table img {
  width: 80px;
  height: auto;
  border-radius: 8px;
  box-shadow: 0 2px 8px rgba(0, 0, 0, 0.1);
}

.order-table {
  width: 100%;
  border-collapse: collapse;
  background: #fdfdfd;
  border-radius: 12px;
  overflow: hidden;
  box-shadow: 0 8px 24px rgba(0, 0, 0, 0.12);
  font-size: 15px;
  color: #333;
}

.order-table thead {
  background: linear-gradient(to right, #4b6cb7, #182848); /* xanh navy gradient */
color: white;
font-weight: bold;
  text-transform: uppercase;
}

.order-table th,
.order-table td {
  padding: 12px 16px;
  border-bottom: 1px solid #e0e0e0;
  text-align: left;
}

.order-table tbody tr:hover {
  background-color: #f9f9f9;
  transition: background 0.3s;
}

.order-table td button {
  padding: 6px 12px;
  border: none;
  border-radius: 6px;
  cursor: pointer;
  font-weight: 500;
}

.confirm-btn,
.delete-btn {

  border: none;
  border-radius: 8px; 
  color: white;
  font-weight: 600;
  cursor: pointer;
  box-shadow: 0 2px 6px rgba(0, 0, 0, 0.15); 
  transition: background-color 0.3s, transform 0.2s, box-shadow 0.2s;
  padding: 6px 14px; 
  font-size: 14px;    
  white-space: nowrap; 
  display: inline-block;
  text-align: center;
}


.confirm-btn {
  background-color: #4caf50;
}

.confirm-btn:hover {
  background-color: #388e3c;
  transform: translateY(-2px); /* Hiệu ứng nổi lên khi hover */
  box-shadow: 0 4px 10px rgba(56, 142, 60, 0.4);
}

.delete-btn {
  background-color: #f44336;
}

.delete-btn:hover {
  background-color: #d32f2f;
  transform: translateY(-2px);
  box-shadow: 0 4px 10px rgba(211, 47, 47, 0.4);
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

.footer {
  /* Nếu sau này bạn có thêm nội dung footer, giữ lại class này */
}

 </style>
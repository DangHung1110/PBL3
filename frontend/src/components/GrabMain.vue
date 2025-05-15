<template>
  <div class="popup-content">
            <h2 class="popup-title">🛒 Đơn hàng</h2>
              <button @click="handleLogout" class="dropdown-item logout">
              🚪 Đăng xuất
            </button>
            

            <table class="order-table">
              <thead>
                <tr>
                  <th>Tên món</th>
                  <th>Nhà hàng</th>
                  <th>Hình ảnh</th>
                  <th>Số lượng</th>
                  <th>Tổng tiền</th>
                  <th>Trạng thái</th>
                         <th>Số điện thoại</th>
                
                     <th>Thời gian đặt hàng</th>
                 <th>Thời gian xác nhận</th>
                   <th>Địa chỉ</th>
               
           
                
                </tr>
              </thead>
              <tbody>
                <tr v-for="(item, index) in thongkedata" :key="index">
                  <td>{{ item.FoodName }}</td>
                  <td>{{ item.RestaurantName }}</td>
                  <td>
                    <img
                      :src="item.Url_image"
                      alt="Ảnh món ăn"
                      class="food-image"
                    />
                  </td>
                  <td>{{ item.Quantity }}</td>
                  <td>{{ item.Doanhso }} VNĐ</td>
                  <td>
                    <button
                      class="confirm-btn"
                      v-if="item.Status_Restaurant === 'confirmed'"
                      @click="
                        FinishedOrder(
                          item.IDOrder,
                          item.Doanhso,
                          item.IDRes,
                          item.IDCustomer,
                          item.OrderTime,
                          item.OrderConfirmedTime,
                          item.Url_Image,
                          item.FoodName,
                          item.RestaurantName,
                          item.Quantity
                        )
                      "
                    >
                      Xác nhận
                    </button>
                    <button v-else class="wait">
                      <span class="spinner"></span>
                      Đang chờ...
                    </button>
                  </td>
                 

                  <td>{{ item.Phone }}</td>
            
                  <td class="handletime">{{ formatDate(item.OrderTime) }}</td>
                  <td v-if="item.Status_Restaurant === 'confirmed'" class="handletime">
                    {{ formatDate(item.OrderConfirmedTime) }}
                  </td>
                  <button v-else class="wait2">
                    <span class="spinner"></span>
                    Đang chờ...
                  </button>
                    <td>{{ item.Address }}</td>
                </tr>
              </tbody>
            </table>
          </div>
</template>
<script setup>
import { DeleteOrder } from "../api/order.js";
import { Thongke } from "../api/thongke.js";
import Swal from "sweetalert2";
import {Thongkegrab} from "../api/Grab.js"
import { useRouter, useRoute } from "vue-router";
import { ProductCount } from "../api/order.js";
const router = useRouter();
import { ref, onMounted, onUnmounted, computed } from "vue";
import {GetGrabData} from "../api/Grab";
const isLoggedIn = ref(false);
const formatDate=(dateString)=> {
    const date = new Date(dateString);
    const time = date.toLocaleTimeString('vi-VN', { hour12: false });
    const datePart = date.toLocaleDateString('vi-VN');
    return `${time} | ${datePart}`;
  }

  const FinishedOrder = async (
  IDOrder,
  TotalPrice,
  IDRes,
  IDCustomer,
  OrderTime,
  OrderConfirmedTime,
  Url_image,
  FoodName,
  RestaurantName,
  Quantity
) => {
  console.log(
    IDOrder,
    TotalPrice,
    IDRes,
    IDCustomer,
    OrderTime,
    OrderConfirmedTime,
    Url_image,
    FoodName,
    RestaurantName,
    Quantity
  );
  console.log("idOrder:", IDOrder)
  await DeleteOrder(IDOrder);

  const senddata = {
    Quantity: Quantity,
    IDRes: IDRes,
    DOANHSO: TotalPrice,
    IDOrder:  IDOrder,
    IDCustomer: IDCustomer,
    OrderTime: OrderTime,
    OrderConfirmedTime: OrderConfirmedTime,
    Url_Image: Url_image,
    FoodName: FoodName,
    RestaurantName: RestaurantName,
  };
  const DataTKGrab={
      OrderTime: OrderTime,
    OrderConfirmedTime: OrderConfirmedTime,
  Revenue:3000,
   IDGrab:parseInt(localStorage.getItem("IDRes"))


  }
  console.log(DataTKGrab);
  try {
    const response=await Thongke(senddata);
    const response2=await Thongkegrab(DataTKGrab);
    console.log(response.data);
    Swal.fire({
      toast: true,
      icon: "success",
      title: "THÔNG BÁO",
      text: "Xác nhận thành công!",
      timer: 3000,
      position: "bottom-end",
      timerProgressBar: true,
      showConfirmButton: false,
      showClass: {
        popup: "swal2-slide-in-right",
      },
    });
  } catch (error) {
    toast: true, console.error("Xác nhận thất bại:", error);
    Swal.fire({
      icon: "error",
      title: "Lỗi",
      position: "bottom-end",
      text: "Có lỗi xảy ra khi xác nhận!",
    });
  }
  await FetchGrabData();

  thongkedata.value = thongkedata.value.filter((p) => p.IDOrder!=IDOrder);
};
  const handleLogout = () => {
  localStorage.removeItem("IDRes");
  localStorage.removeItem("Role");
  localStorage.removeItem("UserName");
  isLoggedIn.value = false;

  router.replace("/login");
};

  const thongkedata=ref([]);
  const FetchGrabData=async()=>{
    try{
        const ID=localStorage.getItem("IDRes");
    const data= await GetGrabData(ID);
    console.log(data);
    thongkedata.value=data.map(item=>({
        IDOrder: item.idOrder,
      IDCustomer: item.idCustomer,
      IDRes: item.idRes,
      OrderTime: item.orderTime,
      Status_Restaurant: item.status_Restaurant,
      CusConfirmedTime: item.cusConfirmedTime,
      OrderConfirmedTime: item.orderConfirmedTime,
      FoodName: item.foodName,
      Phone:item.phone,
      Address:item.address,
      RestaurantName: item.restaurantName,
      Quantity: item.quantity,
      Doanhso: item.totalPrice,
      Url_Image: item.url_image,
      IDGrab:item.idGrab

      
  
      
    }))
    console.log(thongkedata.value);

    }
    catch(error)
    {
         console.error("Lỗi khi tải dữ liệu đơn Grab:", error);
    }
   
  }
onMounted(async () => {
  await FetchGrabData();
});



  
</script>
<style>
/* Layout chính */
.popup-content {
  padding: 24px;
  max-height: 80vh;
  overflow-y: auto;
  background: #fff;
  border-radius: 16px;
  box-shadow: 0 4px 20px rgba(0, 0, 0, 0.08);
  font-family: 'Segoe UI', sans-serif;
}

/* Tiêu đề */
.popup-title {
  font-size: 24px;
  font-weight: bold;
  color: #222;
  margin-bottom: 20px;
  text-align: center;
}

/* Nút đăng xuất */
.logout {
  background-color: #e74c3c;
  color: #fff;
  padding: 8px 16px;
  border-radius: 8px;
  margin-bottom: 20px;
  float: right;
  font-size: 14px;
  transition: background 0.3s ease;
}
.logout:hover {
  background-color: #c0392b;
}

/* Bảng đơn hàng */
.order-table {
  width: 100%;
  border-collapse: separate;
  border-spacing: 0 10px;
  font-size: 14px;
  color: #333;
}

.order-table th {
  background-color: #f5f5f5;
  font-weight: 600;
  padding: 12px;
  text-align: center;
  border-bottom: 2px solid #e0e0e0;
  border-top: 2px solid #e0e0e0;
  color: #555;
}

.order-table td {
  background-color: #fff;
  text-align: center;
  padding: 16px 12px;
  border-top: 1px solid #eee;
  border-bottom: 1px solid #eee;
  vertical-align: middle;
  box-shadow: 0 2px 4px rgba(0,0,0,0.04);
}

/* Ảnh món ăn */
.food-image {
  width: 64px;
  height: 64px;
  object-fit: cover;
  border-radius: 12px;
  border: 1px solid #ddd;
}

/* Nút xác nhận */
.confirm-btn {
  background-color: #288500;
  color: #fff;
  border: none;
  padding: 8px 20px;
  border-radius: 6px;
  font-size: 14px;
  cursor: pointer;
  transition: background 0.3s ease;
    white-space: nowrap; /* 👉 NGĂN chữ xuống dòng */
}
.confirm-btn:hover {
  background-color: #016411;
}

/* Nút chờ xác nhận */
.wait {
  display: inline-flex;
  align-items: center;
  white-space: nowrap;
  background-color: #ffa200;
  color: white;
  border: none;
  padding: 6px 12px;
  border-radius: 4px;
  cursor: default;
  font-size: 13px;
  gap: 6px;
  margin-left: 0.5%;
  margin-top: 0.7%;
}
.wait2 {
  display: inline-flex;
  align-items: center;
  white-space: nowrap;
  background-color: #ffa200;
  color: white;
  border: none;
  padding: 6px 12px;
  border-radius: 4px;
  cursor: default;
  font-size: 13px;
  gap: 6px;
  margin-left: 10%;
  margin-top: 17%;
}

/* Spinner */
.spinner {
  width: 14px;
  height: 14px;
  border: 2px solid rgba(255, 255, 255, 0.5);
  border-top-color: white;
  border-radius: 50%;
  animation: spin 0.8s linear infinite;
}

/* Thời gian */
.handletime {
  font-size: 13px;
  color: #555;
  font-style: italic;
  white-space: nowrap;
}

/* Animation */
@keyframes spin {
  to {
    transform: rotate(360deg);
  }
}

/* Responsive */
@media (max-width: 768px) {
  .order-table {
    font-size: 12px;
  }
  .food-image {
    width: 48px;
    height: 48px;
  }
}

</style>
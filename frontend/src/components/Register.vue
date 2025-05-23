<template> 
  <div class="modal"> 
    <div class="modal-content">
      <h2>Đăng ký</h2>
      <form @submit.prevent="handleSignup">

      
        <div class="input-wrapper">
          <select v-model="role" class="input-field">
            <option value="user">Người dùng</option>
            <option value="restaurant">Nhà hàng</option>
          </select>
        </div>

     
        <template v-if="role === 'user'">
          <div class="input-wrapper">
            <input v-model="username" type="text" placeholder="Tên đăng nhập" class="input-field" required />
          </div>

          <div class="input-wrapper">
            <input v-model="phone" type="text" placeholder="Số điện thoại" class="input-field" required />
          </div>

          <div class="input-wrapper">
            <input v-model="address" type="text" placeholder="Địa chỉ" class="input-field" required />
          </div>

          <div class="input-wrapper">
            <input v-model="password" type="password" placeholder="Mật khẩu" class="input-field" required />
          </div>
        </template>

        
        <template v-if="role === 'restaurant'">
  <div class="restaurant-form-grid">
    <!-- Cột trái: thông tin -->
    <div class="info-column">
      <div class="input-wrapper">
        <input v-model="restaurantName" type="text" placeholder="Tên nhà hàng" class="input-field" required />
      </div>

      <div class="input-wrapper">
        <input v-model="phone" type="text" placeholder="Số điện thoại" class="input-field" required />
      </div>

      <div class="input-wrapper">
        <input v-model="address" type="text" placeholder="Địa chỉ nhà hàng" class="input-field" required />
      </div>

      <div class="input-wrapper">
        <input v-model="password" type="password" placeholder="Mật khẩu" class="input-field" required />
      </div>
    </div>

    <!-- Cột phải: ảnh -->
    <div class="file-column">
      <div class="input-wrapper">
        <label>Căn cước công dân người đại diện pháp lý</label>
        <input type="file"@change="handleImageUpload" class="input-field" />
      </div>

      <div class="input-wrapper">
        <label>Ảnh chụp khu vực chế biến</label>
        <input type="file" @change="handleImageUpload2" class="input-field" />
      </div>

      <div class="input-wrapper">
        <label>Menu quán (Chụp ảnh các mặt)</label>
        <input type="file" @change="handleImageUpload3" class="input-field" />
      </div>
    </div>
  </div>
</template>


        <button type="submit" class="btn">Đăng ký</button>
            <button @click="closeModal" class="modal-close-x">x</button>
      </form>

  
    </div>
  </div>
</template>


<script setup>
import { ref } from "vue";
import { useRouter } from "vue-router";
import { SignUpUser,SignUpRes } from "../api/signup.api.js"; // Import API
import {ImageDeal} from "../api/signup.api.js"; // Import API
const router = useRouter();

    const username = ref("");
    const phone = ref("");
    const address = ref("");
    const password = ref("");
    const restaurantName = ref("");
    const imageUrl = ref(null);
    const imageUrl2 = ref(null);
    const imageUrl3 = ref(null);

    const role = ref("user");

    const closeModal = () => {
      router.push("/");
    };

  const handleImageUpload = (event) => {
  imageUrl.value = event.target.files[0] || null;
};
const handleImageUpload2 = (event) => {
  imageUrl2.value = event.target.files[0] || null;
};
const handleImageUpload3 = (event) => {
  imageUrl3.value = event.target.files[0] || null;
};
   

   const handleSignup = async () => {
  try {
    if (role.value === "user") {
    
      await SignUpUser(role.value, username.value, password.value, phone.value, address.value);
    } else if (role.value === "restaurant") {
      if (!imageUrl.value || !imageUrl2.value || !imageUrl3.value) {
        throw new Error("Vui lòng chọn tất cả các ảnh yêu cầu");
      }
      const Image1=await ImageDeal(imageUrl.value);
      const Image2=await ImageDeal(imageUrl2.value);
      const Image3=await ImageDeal(imageUrl3.value);
      console.log(role.value, username.value, password.value, phone.value, address.value, restaurantName.value,Image1,Image2, Image3);
      await SignUpRes(role.value, username.value, password.value, phone.value, address.value, restaurantName.value,Image1,Image2, Image3);
    }
    alert("Đăng ký thành công!");
    router.push("/"); // chuyển hướng sau khi đăng ký thành công
  } catch (error) {
    alert("Đăng ký thất bại. Vui lòng kiểm tra lại thông tin.");
  }
};


 

   


</script>

<style>
.modal {
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background-color: rgba(0, 0, 0, 0.5);
  display: flex;
  align-items: center;
  justify-content: center;
}


/* Đảm bảo file input gọn gàng */
.file-column .input-wrapper input[type="file"] {
  padding: 8px 12px;
  margin-left:20px;
  border: 1px solid #ccc;
  border-radius: 8px;
  font-size: 14px;
  background-color: #fff;
}

/* Canh trái label để khớp với input */
.file-column .input-wrapper label {
  margin-bottom: 6px;
    margin-left:20px;
  font-weight: 500;
  font-size: 14px;
  color: #333;
}
.info-column {
  margin-top:25px;
}
.info-column .input-wrapper {
  margin-bottom: 16px;
}

.modal-content {
  width: 100%;
  max-width: 1000px;
  background: white;
  padding: 30px;
  border-radius: 12px;
  box-shadow: 0 8px 24px rgba(0, 0, 0, 0.1);
}

h2 {
  font-size: 24px;
  font-weight: 600;
  color: #ee4d2d;
  text-align: center;
  margin-bottom: 24px;
}

.restaurant-form-grid {
  display: grid;
  grid-template-columns: 0.8fr 1fr;
  gap: 24px;
}

.input-wrapper {
  display: flex;
  flex-direction: column;
  margin-bottom: 16px;
}

.input-wrapper label {
  margin-bottom: 6px;
  font-weight: 500;
  font-size: 14px;
  color: #333;
}

.input-field {
  padding: 10px 14px;
  border: 1px solid #ccc;
  border-radius: 8px;
  font-size: 14px;
  width: 100%;
  transition: border-color 0.2s;
}

.input-field:focus {
  border-color: #ee4d2d;
  outline: none;
}

.btn {
  background-color: #ee4d2d;
  color: white;
  padding: 12px;
  border: none;
  border-radius: 8px;
  width: 100%;
  font-size: 16px;
  cursor: pointer;
  margin-top: 20px;
  transition: background-color 0.3s;
}

.btn:hover {
  background-color: #d4391d;
}

.close-btn {
  margin-top: 12px;
  background: transparent;
  border: none;
  color: #555;
  text-decoration: underline;
  cursor: pointer;
  font-size: 14px;
}
.styled-close-btn {
 margin-top: 12px;
 background-color: #ccc;
 color: #333;
padding: 10px;
border: none;
 border-radius: 8px;
 width: 100%;
font-size: 16px;
cursor: pointer;
transition: background-color 0.3s;
}

.styled-close-btn:hover {
background-color: #aaa;
}
.modal-close-x {
 position: absolute;
 top: 16px;
 right: 16px;
background: none;
border: none;
 font-size: 24px;
font-weight: bold;
 color: #999;
cursor: pointer;
transition: color 0.2s ease;
}

.modal-close-x:hover {
color: #ee4d2d;
}



</style>

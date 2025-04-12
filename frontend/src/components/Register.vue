<template>
  <div class="modal">
    <div class="modal-content">
      <h2>Đăng ký</h2>
      <form @submit.prevent="handleSignup">
        <!-- Chọn vai trò -->
        <div class="input-wrapper">
          <select v-model="role" class="input-field">
            <option value="user">Người dùng</option>
            <option value="restaurant">Nhà hàng</option>
          </select>
        </div>

        <!-- Hiển thị thông tin Nhà hàng khi chọn role là "restaurant" -->
        <div v-if="role === 'restaurant'" class="input-wrapper">
          <input v-model="restaurantName" type="text" placeholder="Tên nhà hàng" class="input-field" required />
        </div>

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

        <!-- Chọn ảnh cho Nhà hàng, có thể bỏ qua nếu không cần -->
        <div v-if="role === 'restaurant'" class="input-wrapper">
          <input type="file" @change="handleImageUpload" class="input-field" />
        </div>

        <button type="submit" class="btn">Đăng ký</button>
      </form>
      <button @click="closeModal" class="close-btn">Đóng</button>
    </div>
  </div>
</template>

<script>
import { ref } from "vue";
import { useRouter } from "vue-router";
import { signup } from "../api/signup.api.js"; // Import API

export default {
  setup() {
    const router = useRouter();

    const username = ref("");
    const phone = ref("");
    const address = ref("");
    const password = ref("");
    const restaurantName = ref("");
    const imageUrl = ref(null);
    const role = ref("user");

    const closeModal = () => {
      router.push("/");
    };

    const handleImageUpload = (event) => {
      const file = event.target.files[0];
      if (file) {
        // Here, you can convert the file to base64 or upload it to a server and get the URL
        const reader = new FileReader();
        reader.onload = () => {
          imageUrl.value = reader.result; // Save base64 string
        };
        reader.readAsDataURL(file);
      } else {
        imageUrl.value = null; // No image selected
      }
    };

    const handleSignup = async () => {
    const response = await signup(
    username.value,
    password.value,
    role.value,
    imageUrl.value,
    phone.value,
    address.value,
    restaurantName.value
  );

  if (response.success) {
    console.log("Đăng ký thành công", response);
      alert("Đăng ký thành công! Mời bạn đăng nhập.");

      router.push("/login");
  } else {
    console.error("Đăng ký thất bại", response.message);
    alert("Đăng ký thất bại. Vui lòng thử lại!");
    console.error("Chi tiết:", response.message);
  }
};

    return { role, username, phone, address, password, restaurantName, imageUrl, handleImageUpload, handleSignup, closeModal,};
  },
};
</script>

<style>
/* Định dạng chung */
.modal {
  position: fixed;
  top: 0;
  left: 0;
  width: 100vw;
  height: 100vh;
  background: rgba(0, 0, 0, 0.5);
  display: flex;
  justify-content: center;
  align-items: center;
}

.modal-content {
  background: white;
  padding: 25px;
  border-radius: 12px;
  min-width: 350px;
  text-align: center;
  position: relative;
  box-shadow: 0px 4px 12px rgba(0, 0, 0, 0.2);
}

/* Bọc input để đồng nhất với Login */
.input-wrapper {
  width: 100%;
  display: flex;
  justify-content: center;
}

/* Ô nhập liệu */
.input-field {
  width: 100%;
  padding: 12px;
  font-size: 16px;
  border: 2px solid #ddd;
  border-radius: 8px;
  outline: none;
  margin-bottom: 12px;
  transition: 0.3s;
  background: white;
}

/* Định dạng select giống input */
select.input-field {
  appearance: none;
  -webkit-appearance: none;
  -moz-appearance: none;
  cursor: pointer;
}

/* Hiệu ứng focus */
.input-field:focus {
  border-color: #6200ea;
  box-shadow: 0 0 5px rgba(98, 0, 234, 0.5);
}

/* Nút Đăng ký */
.btn {
  width: 100%;
  padding: 12px;
  background: #ea6200;
  color: white;
  border: none;
  border-radius: 8px;
  font-size: 16px;
  cursor: pointer;
  transition: 0.3s;
}

.btn:hover {
  background: #cf5803;
}

/* Nút Đóng */
.close-btn {
  margin-top: 12px;
  padding: 8px 14px;
  background: red;
  color: white;
  border: none;
  border-radius: 8px;
  cursor: pointer;
  transition: 0.3s;
}

.close-btn:hover {
  background: darkred;
}
</style>

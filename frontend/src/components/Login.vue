<template>
  <div class="modal">
    <div class="modal-content">
      <h2>Đăng nhập</h2>
      <form @submit.prevent="handleLogin" class="login-form">
        <!-- Chọn vai trò -->
        <div class="input-wrapper">
          <select v-model="role" class="input-field">
            <option value="user">Người dùng</option>
            <option value="restaurant">Nhà hàng</option>
          </select>
        </div>

        <div class="input-wrapper">
          <input v-model="username" type="text" placeholder="Tên đăng nhập" class="input-field" required />
        </div>

        <div class="input-wrapper">
          <input v-model="password" type="password" placeholder="Mật khẩu" class="input-field" required />
        </div>

        <button type="submit" class="btn">Đăng nhập</button>
      </form>
      <p v-if="errorMessage" class="error-message">{{ errorMessage }}</p>
      <button @click="closeModal" class="close-btn">Đóng</button>
    </div>
  </div>
</template>

<script>
import { ref } from "vue";
import { useRouter } from "vue-router";
import { login } from "../api/api.js"; // Import API

export default {
  setup() {
    const router = useRouter();
    const role = ref("user");
    const username = ref("");
    const password = ref("");
    const errorMessage = ref("");

    const handleLogin = async () => {
      const response = await login(username.value, password.value);
      if (response) {
        console.log("Đăng nhập thành công!", response);
        router.push("/home"); // Điều hướng sau khi đăng nhập thành công
      } else {
        errorMessage.value = "Sai tên đăng nhập hoặc mật khẩu!";
      }
    };

    const closeModal = () => {
      router.push("/");
    };

    return { role, username, password, handleLogin, closeModal, errorMessage };
  },
};
</script>

<style>
/* Modal overlay */
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
  animation: fadeIn 0.3s ease-in-out;
}

/* Nội dung modal */
.modal-content {
  background: white;
  padding: 30px;
  border-radius: 12px;
  min-width: 350px;
  text-align: center;
  position: relative;
  box-shadow: 0px 4px 10px rgba(0, 0, 0, 0.2);
  animation: slideDown 0.3s ease-in-out;
}

/* Form login */
.login-form {
  display: flex;
  flex-direction: column;
  gap: 12px;
}

/* Bọc input để đồng bộ giao diện */
.input-wrapper {
  width: 100%;
  display: flex;
  justify-content: center;
}

/* Ô nhập dữ liệu */
.input-field {
  width: 100%;
  padding: 12px;
  font-size: 16px;
  border: 2px solid #ddd;
  border-radius: 8px;
  outline: none;
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

/* Nút đăng nhập */
.btn {
  width: 100%;
  padding: 12px;
  font-size: 16px;
  background: #ea6200;
  color: white;
  border: none;
  border-radius: 8px;
  cursor: pointer;
  transition: 0.3s;
}

.btn:hover {
  background:  #ea6200;
}

/* Nút đóng */
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

/* Hiệu ứng mở modal */
@keyframes fadeIn {
  from {
    opacity: 0;
  }
  to {
    opacity: 1;
  }
}

@keyframes slideDown {
  from {
    transform: translateY(-20px);
  }
  to {
    transform: translateY(0);
  }
}
</style>

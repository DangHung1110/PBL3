<template>
  <div class="modal">
    <div class="modal-content">
      <h2>Đăng nhập</h2>
      <form @submit.prevent="handleLogin" class="login-form">
       

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

<script setup>
import { ref } from "vue";
import { useRouter } from "vue-router";
import { login } from "../api/api.js"; 
import { emitter } from "../api/eventBus.js"; // Import event bus
import Swal from 'sweetalert2';

    const router = useRouter();
    const role = ref("user");
    const username = ref("");
    const password = ref("");
    const errorMessage = ref("");
    const closeModal=()=>{
      router.push("/");
    }
    const handleLogin = async () => {
  try {
    const response = await login(username.value, password.value);
    console.log(response);

    if (response.role === "Restaurant") {
      localStorage.setItem("Role", response.role);
      localStorage.setItem("IDRes", response.userID);
      localStorage.setItem("UserName", response.userName);
      router.replace("/restaurant/dashboard");
    } else if (response.role === "Customer") {
      localStorage.setItem("Role", response.role);
      localStorage.setItem("IDRes", response.userID);
      localStorage.setItem("UserName", response.userName);
      localStorage.setItem("Phone",response.phone);
      localStorage.setItem("Address",response.address);
      emitter.emit("Login", ' ');
      emitter.emit("Ordercount", ' ');
      router.replace(`/customer/${response.userName}`);
    }
    else if( response.role ==="Grab")
    {localStorage.setItem("Role", response.role);
      localStorage.setItem("IDRes", response.userID);
      localStorage.setItem("UserName", response.userName);
      router.replace("/Grab");

    }
    else if( response.role ==="Admin")
    {
      localStorage.setItem("Role", response.role);
   
      router.replace("/admin");
    }
    else {
      errorMessage.value = "Tên đăng nhập hoặc mật khẩu không đúng";
    }
    }
   catch (error) {
    console.error("Lỗi đăng nhập:", error);
    Swal.fire({
      toast: true,   
    icon: 'error',
    title: 'THÔNG BÁO',
    text: 'Sai tài khoản hoặc mật khẩu!',
    timer: 3000,
    position: 'bottom-end',
    timerProgressBar: true,
    showConfirmButton: false,
    showClass: {
      popup: 'swal2-slide-in-right' }}
    )
    errorMessage.value = "Tên đăng nhập hoặc mật khẩu không đúng";
  }
};


</script>


<style>



.modal {
  position: fixed;
  top: 0;
  left: 0;
  width:10vw;
  height:10vh;
  background: rgba(0, 0, 0, 0.4);
  display: flex;
  justify-content: center;
  align-items: center;
  z-index: 999;
}


.modal-content {
background: #fff;
padding: 24px 24px;
 width: 30%;
max-width: 90%;
 border-radius: 12px;
 box-shadow: 0 8px 24px rgba(0,0,0,0.15);
animation: slideDown 0.3s ease;
}

.modal-content h2 {
 font-size: 20px;
 font-weight: 600;
 color: #333;
margin-bottom: 16px;
 text-align: center;
}

.input-field {
width: 100%;
padding: 10px 12px;
font-size: 14px;
 border: 1px solid #ddd;
 border-radius: 8px;
 background-color: #f9f9f9;
 outline: none;
 transition: border-color 0.3s, box-shadow 0.3s;
}

.input-field:focus {
border-color: #ee4d2d;
 box-shadow: 0 0 0 2px rgba(238, 77, 45, 0.1);
}

.btn {
 background-color: #ee4d2d;
color: white;
 padding: 10px;
 font-size: 14px;
 border: none;
 border-radius: 8px;
 cursor: pointer;
font-weight: 600;
transition: background-color 0.3s;
}

.btn:hover {
background-color: #d84423;
}

.close-btn {
margin-top: 10px;
 background: none;
 border: none;
 color: #888;
 font-size: 13px;
 cursor: pointer;
 text-align: center;
 width: 100%;
 text-decoration: underline;
}

.close-btn:hover {
color: #333;
}

.error-message {
 color: red;
 font-size: 13px;
margin-top: 6px;
text-align: center;
}



@keyframes slideDown {
  from {
    opacity: 0;
    transform: translateY(-20px);
  }
  to {
    opacity: 1;
    transform: translateY(0);
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

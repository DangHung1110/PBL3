<script>
import { computed } from "vue";
import { useRoute } from "vue-router";
import Login from "../components/Login.vue";
import Register from "../components/Register.vue";

export default {
  components: { Login, Register },
  setup() {
    const route = useRoute();

    // Kiểm tra nếu đang ở trang login hoặc register
    const showLogin = computed(() => route.path === "/login");
    const showRegister = computed(() => route.path === "/register");

    return { showLogin, showRegister };
  },
};
</script>

<template>
  <div class="container">
    <div class="navbar">
      <div class="logo">Grap food</div>
      <div class="home-page"></div>
      <div class="login">
        <i class="fas fa-user"></i>
        <div class="boder-login">
          <router-link to="/login" class="loginn">Đăng nhập</router-link>
          <router-link to="/register" class="register">Đăng ký</router-link>
        </div>
      </div>
    </div>
    <div class="content"></div>
    <div class="footer"></div>
    <Login v-if="showLogin" />
    <Register v-if="showRegister" />
  </div>
</template>

<style>
.container  {
  padding: 0;
  margin: 0;
  width: 100%;
  height: 100%;
}

.navbar {
  display: flex;
  width: 100%;
  height: 70px;
  justify-content: space-around;
  align-items: center;
  background-color: rebeccapurple;
}

.boder-login {
  position: absolute;
  height: 100px;
  width: 80px;
  background-color: aqua;
  display: flex;
  flex-direction: column;
  justify-content: center;
  gap: 10px;
  opacity: 0;
  transition: all 0.5s ease;
}

.boder-login div {

}

.loginn, 
.register {
  cursor: pointer;
  position: relative;
  height: 30px;
  width: 100%;
  border-radius: 8px;
  background-color: red;
  display: flex;
  justify-content: center;
  align-items: center;
  cursor: pointer;
}

.login i:hover + .boder-login,
.boder-login:hover {
  opacity: 1;
}


</style>
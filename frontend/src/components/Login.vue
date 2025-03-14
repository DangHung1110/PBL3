<template>
  <div class="modal">
    <div class="modal-content">
      <h2>Đăng nhập</h2>
      <form>
        <input type="text" placeholder="Tên đăng nhập" />
        <input type="password" placeholder="Mật khẩu" />
        <button type="submit">Đăng nhập</button>
      </form>
      <button @click="closeModal" class="close-btn">Đóng</button>
    </div>
  </div>
</template>

<script>
import { useRouter } from "vue-router";

export default {
  setup() {
    const router = useRouter();
    const closeModal = () => {
      router.push("/"); //close and back Home
    };
    return { closeModal };
  },
};
</script>

<style>

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
  padding: 20px;
  border-radius: 10px;
  min-width: 300px;
  text-align: center;
  position: relative;
}

.close-btn {
  margin-top: 10px;
  padding: 5px 10px;
  background: red;
  color: white;
  border: none;
  cursor: pointer;
}

</style>

<template>
  <div class="modal">
    <div class="modal-content">
      <h2>Đăng ký</h2>
      <form>
        <input type="text" placeholder="Tên đăng nhập" required />
        <input type="text" placeholder="Số điện thoại" required />
        <input type="text" placeholder="Địa chỉ" required />
        <input type="password" placeholder="Mật khẩu" required />
        <button type="submit">Đăng ký</button>
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
      router.push("/"); // close and back Home
    };
    return { closeModal };
  },
};
</script>

<style>
</style>

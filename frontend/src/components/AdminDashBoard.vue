<template>
  <div class="bg-overlay bg-full">
    <div class="admin-dashboard">
      <!-- Header -->
      <header class="header relative">
        <div id="particles-js"></div>
        <div class="header-content">
          <div class="flex items-center space-x-2">
            <h1 class="header-text text-2xl font-extrabold">Grab Food Corp</h1>
          </div>
          <div class="tag-cloud">
            <span class="tag" @click="searchTag('Pizza')">Pizza</span>
            <span class="tag" @click="searchTag('Sushi')">Sushi</span>
            <span class="tag" @click="searchTag('Coffee')">Coffee</span>
            <span class="tag" @click="searchTag('Burger')">Burger</span>
            <span class="tag" @click="searchTag('Noodles')">Noodles</span>
          </div>
          <button class="logout-btn" @click="logout">Đăng xuất</button>
        </div>
      </header>

      <!-- Tabs -->
      <div class="tab-menu-container">
        <router-link to="/Admin/Thongke" class="tab-button" active-class="tab-active">
          <span class="tab-icon">📊</span>
          <span class="tab-text">Thống kê</span>
        </router-link>
        <router-link to="/Admin/Grab" class="tab-button" active-class="tab-active">
          <span class="tab-icon">🚗</span>
          <span class="tab-text">Grab đối tác</span>
        </router-link>
        <router-link to="/Admin/WaitConfirmedRes" class="tab-button" active-class="tab-active">
          <span class="tab-icon">📥</span>
          <span class="tab-text">Đăng ký nhà hàng</span>
        </router-link>
        <router-link to="/Admin/LinkingRes" class="tab-button" active-class="tab-active">
          <span class="tab-icon">🏪</span>
          <span class="tab-text">Nhà hàng đối tác</span>
        </router-link>
      </div>

      <div class="main-content">
        <router-view></router-view>
      </div>
    </div>
  </div>
</template>

<script setup>
import { onMounted } from "vue";

function logout() {
  alert("Đăng xuất thành công!");
}

function searchTag(tag) {
  alert(`Tìm kiếm: ${tag}`);
}

onMounted(() => {
  if (window.particlesJS) {
    window.particlesJS("particles-js", {
      particles: {
        number: { value: 50, density: { enable: true, value_area: 800 } },
        color: { value: "#ffffff" },
        shape: { type: "circle" },
        opacity: { value: 0.5, random: true },
        size: { value: 3, random: true },
        line_linked: { enable: false },
        move: { enable: true, speed: 2, direction: "none", random: true },
      },
      interactivity: {
        detect_on: "canvas",
        events: {
          onhover: { enable: true, mode: "repulse" },
          onclick: { enable: true, mode: "push" },
        },
        modes: {
          repulse: { distance: 100, duration: 0.4 },
          push: { particles_nb: 4 },
        },
      },
      retina_detect: true,
    });
  }
});
</script>

<style>
@import url("https://fonts.googleapis.com/css2?family=Montserrat:wght@800&display=swap");

body {
  font-family: "Inter", sans-serif;
  margin: 0;
  min-height: 100vh;
}

.bg-overlay {
  background: #fff;
  background-image: url('https://cdnmedia.baotintuc.vn/Upload/0gYjdiNY41wQIbPeRYyPvA/files/2021/12/grap-rung-chuong.jpeg');
  background-size: cover;
  background-position: center;
  background-repeat: no-repeat;
  height:100vh;
  width:100%;
}

.header {
  background: #1e1e2f;
  padding: 1.5rem 0;
  position: relative;
  overflow: hidden;
}

#particles-js {
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  z-index: 0;
}

.header-text {
  font-family: "Montserrat", sans-serif;
  color: #07b124;
  font-size: 23px;
  text-transform: uppercase;
  letter-spacing: 2px;
}

.header-content {
  display: flex;
  align-items: center;
  justify-content: space-between;
  padding: 0 1.5rem;
  position: relative;
  z-index: 1;
}

.tag-cloud {
  padding-right: 7%;
  flex: 1;
  display: flex;
  justify-content: center;
  align-items: center;
  position: relative;
  max-width: 400px;
  height: 40px;
}

.tag {
  position: absolute;
  font-family: "Montserrat", sans-serif;
  font-size: 0.9rem;
  font-weight: 600;
  border-radius: 12px;
  padding: 0.3rem 0.8rem;
  cursor: pointer;
  background: rgba(0, 177, 79, 0.2);
  color: #fff;
  text-shadow: 0 0 5px rgba(0, 177, 79, 0.8);
  animation: rotateTags 10s linear infinite;
  transition: all 0.3s ease;
}
.tag:hover {
  background: rgba(0, 177, 79, 0.8);
  box-shadow: 0 0 10px rgba(0, 177, 79, 0.5);
}
.tag:nth-child(1) { transform: translate(0, 0); animation-delay: 0s; }
.tag:nth-child(2) { transform: translate(80px, 10px); animation-delay: -2s; }
.tag:nth-child(3) { transform: translate(-80px, -10px); animation-delay: -4s; }
.tag:nth-child(4) { transform: translate(40px, -20px); animation-delay: -6s; }
.tag:nth-child(5) { transform: translate(-40px, 20px); animation-delay: -8s; }

@keyframes rotateTags {
  0% { transform: translate(0, 0) rotate(0deg); }
  100% { transform: translate(0, 0) rotate(360deg); }
}

.logout-btn {
  background: #00b14f;
  color: #fff;
  border-radius: 10px;
  padding: 10px 20px;
}
.logout-btn:hover {
  background: #006633;
}

.tab-menu-container {
  display: flex;
  justify-content: space-between;
  padding: 20px;
  gap: 1rem;
  background-color: rgba(255, 253, 240, 0.7); /* màu trắng kem nhạt, mờ 70% */
  border-bottom: 2px solid #00b14f;
  backdrop-filter: blur(2px); /* tùy chọn: làm mờ nhẹ phía sau */
}


.tab-button {
  flex: 1;
  display: flex;
  justify-content: center;
  flex-direction: column;
  align-items: center;
  gap: 0.5rem;
  padding: 0.9rem 0.8rem;
  background-color: #fff;
  color: #00b14f;
  border: 2px solid #00b14f;
  border-radius: 20px;
  text-decoration: none;
  font-weight: 600;
  transition: all 0.3s ease;
  text-align: center;
  white-space: nowrap;
  min-width: 0;
  box-shadow: 0 2px 5px rgba(255, 87, 34, 0.1);
}
.tab-button:hover {
  background-color: #00b14f;
  color: #fff;
  transform: translateY(-2px);
}
.tab-active {
  background-color: #00b14f;
  color: #fff;
  box-shadow: 0 4px 10px rgba(0, 177, 79, 0.3);
}
.tab-icon {
  font-size: 1.2rem;
}
.tab-text {
  font-size: 0.95rem;
}
</style>

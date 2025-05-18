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
          <button class="logout-btn" @click="logout">
            <span>🔓</span> Đăng xuất
          </button>
        </div>
      </header>

      <!-- Tabs -->
      <div class="tab-menu">
        <!-- Tabs -->
<div class="tab-menu">
  <router-link
    to="/Admin/Thongke"
    class="tab-item"
    active-class="active-tab"
  >
    <span class="tab-icon">🕒</span>
    <span class="tab-text">Thống kê</span>
  </router-link>

  <router-link
    to="/Admin/Grab"
    class="tab-item"
    active-class="active-tab"
  >
    <span class="tab-icon">🕒</span>
    <span class="tab-text">Grab đối tác</span>
  </router-link>
</div>

      </div>
       <div class="main-content">
      <router-view></router-view>
    </div>
     
    </div>

    <!-- Content -->
   
  </div>
</template>

<script setup>
import { ref, onMounted } from "vue";



function logout() {
  alert("Đăng xuất thành công!");
}

function searchTag(tag) {
  alert(`Tìm kiếm nhà hàng với danh mục: ${tag}`);
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
  min-height: 100vh;
}

.header {
  background: linear-gradient(180deg, rgba(255, 87, 34, 0.9), rgba(255, 152, 0, 0.7));
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
  color: #fff;
  font-size: 23px;
  text-transform: uppercase;
  letter-spacing: 2px;
  text-shadow: 0 0 10px rgba(255, 152, 0, 0.8), 0 0 20px rgba(255, 87, 34, 0.6);
  animation: neonPulse 2s ease-in-out infinite;
}

@keyframes neonPulse {
  0%, 100% {
    text-shadow: 0 0 10px rgba(255, 152, 0, 0.8), 0 0 20px rgba(255, 87, 34, 0.6);
  }
  50% {
    text-shadow: 0 0 15px rgba(255, 152, 0, 1), 0 0 30px rgba(255, 87, 34, 0.8);
  }
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
  color: #fff;
  font-size: 0.9rem;
  font-weight: 600;
  background: rgba(255, 152, 0, 0.2);
  border-radius: 12px;
  padding: 0.3rem 0.8rem;
  cursor: pointer;
  text-shadow: 0 0 5px rgba(255, 152, 0, 0.8);
  animation: rotateTags 10s linear infinite;
  transition: all 0.3s ease;
}
.tag:hover {
  transform: scale(1.2);
  background: rgba(255, 87, 34, 0.8);
  box-shadow: 0 0 10px rgba(255, 87, 34, 0.5);
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
  background: rgba(255, 87, 34, 0.9);
  color: #fff;
  padding: 0.5rem 1.5rem;
  font-weight: 600;
  display: flex;
  align-items: center;
  gap: 0.5rem;
  transition: background 0.3s ease;
}
.logout-btn:hover {
  background: #751400;
}

.tab-menu {
  display: flex;
  justify-content: space-around;
  background: #fff;
  border-bottom: 1px solid #eee;
}

.tab-item {
  flex: 1;
  padding: 12px 0;
  text-align: center;
  font-weight: 500;
  color: #555;
  border-bottom: 3px solid transparent;
  background: transparent;
  transition: all 0.3s ease;
}
.tab-item.active {
  color: #ee4d2d;
  font-weight: 600;
  border-bottom: 3px solid #ee4d2d;
}
.tab-item:hover {
  background-color: #fdf2ec;
  color: #ee4d2d;
}

.tab-icon {
  display: block;
  font-size: 18px;
  margin-bottom: 4px;
}

.tab-text {
  font-family: "Montserrat", sans-serif;
  font-size: 14px;
}

.content-card {
  backdrop-filter: blur(8px);
  background: #fff;
  border: 1px solid #fff;
}
</style>

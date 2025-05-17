<template>
  <div class="bg-overlay">
    <div class="admin-dashboard max-w-7xl mx-auto">
      <!-- Header -->
      <header class="header relative">
        <div id="particles-js"></div>
        <div class="header-content">
          <!-- Left: Grab Food Title -->
        <div class="flex items-center space-x-2">
  <img src="https://innhanhhcm.vn/wp-content/uploads/2024/05/logo-Vinfast-01-1024x774.jpg" width="30" height="30" alt="Grab Food Logo" class="header-logo">
  
  <div class="flex items-center space-x-2">
    <span class="text-sm">Hân hạnh là nhà tài trợ chính thức</span>
    <h1 class="header-text text-2xl font-extrabold">Grab Food</h1>
  </div>
</div>

          <!-- Center: Tag Cloud -->
          <div class="tag-cloud">
            <span class="tag" @click="searchTag('Pizza')">Pizza</span>
            <span class="tag" @click="searchTag('Sushi')">Sushi</span>
            <span class="tag" @click="searchTag('Coffee')">Coffee</span>
            <span class="tag" @click="searchTag('Burger')">Burger</span>
            <span class="tag" @click="searchTag('Noodles')">Noodles</span>
          </div>
          <!-- Right: Logout Button -->
          <button class="logout-btn" @click="logout">
            <span>🔓</span> Đăng xuất
          </button>
        </div>
      </header>

      <!-- Navigation -->
      <!-- Navigation Tabs -->
       <div class="Part2"></div>
<nav class="navbar flex justify-center bg-white bg-opacity-95 p-4 shadow-lg rounded-xl space-x-4 mt-12 ">

  <button
    class="nav-item flex items-center gap-2 px-5 py-3 rounded-xl transition-all duration-200 font-medium text-gray-700 hover:bg-orange-100"
    :class="{ 'bg-orange-500 text-white': currentTab === 'waiting' }"
    @click="currentTab = 'waiting'"
  >
    <span>🕒</span> <span>Chờ xác nhận</span>
  </button>

  <button
    class="nav-item flex items-center gap-2 px-5 py-3 rounded-xl transition-all duration-200 font-medium text-gray-700 hover:bg-orange-100"
    :class="{ 'bg-orange-500 text-white': currentTab === 'partners' }"
    @click="currentTab = 'partners'"
  >
    <span>🏢</span> <span>Nhà hàng hợp tác</span>
  </button>

  <button
    class="nav-item flex items-center gap-2 px-5 py-3 rounded-xl transition-all duration-200 font-medium text-gray-700 hover:bg-orange-100"
    :class="{ 'bg-orange-500 text-white': currentTab === 'revenue' }"
    @click="currentTab = 'revenue'"
  >
    <span>📊</span> <span>Doanh thu</span>
  </button>

  <button
    class="nav-item flex items-center gap-2 px-5 py-3 rounded-xl transition-all duration-200 font-medium text-gray-700 hover:bg-orange-100"
    :class="{ 'bg-orange-500 text-white': currentTab === 'team' }"
    @click="currentTab = 'team'"
  >
    <span>👥</span> <span>Đội ngũ Grab</span>
  </button>
</nav>
</div>

<!-- Content -->
<div class="content p-6">
  <div v-if="currentTab === 'waiting'" class="content-card bg-gradient-to-r from-orange-400 to-orange-500 text-white p-6 rounded-lg shadow-xl">
    <h2 class="text-2xl font-semibold mb-2">Danh sách nhà hàng chờ xác nhận</h2>
    <p>Quản lý các nhà hàng đang chờ duyệt để tham gia nền tảng.</p>
  </div>

  <div v-else-if="currentTab === 'partners'" class="content-card bg-gradient-to-r from-blue-400 to-blue-500 text-white p-6 rounded-lg shadow-xl">
    <h2 class="text-2xl font-semibold mb-2">Danh sách nhà hàng đang hợp tác</h2>
    <p>Xem và quản lý các nhà hàng đối tác hiện tại.</p>
  </div>

  <div v-else-if="currentTab === 'revenue'" class="content-card bg-gradient-to-r from-green-400 to-green-500 text-white p-6 rounded-lg shadow-xl">
    <h2 class="text-2xl font-semibold mb-2">Doanh thu</h2>
    <p>Phân tích doanh thu và hiệu suất kinh doanh.</p>
  </div>

  <div v-else-if="currentTab === 'team'" class="content-card bg-gradient-to-r from-purple-400 to-purple-500 text-white p-6 rounded-lg shadow-xl">
    <h2 class="text-2xl font-semibold mb-2">Thống kê đội ngũ Grab</h2>
    <p>Xem và phân tích dữ liệu về đội ngũ tài xế và nhân viên Grab.</p>
  </div>
</div>

    </div>

</template>

<script setup>
import { ref, onMounted } from 'vue'

const currentTab = ref('waiting')

function logout() {
  alert('Đăng xuất thành công!')
}

function searchTag(tag) {
  alert(`Tìm kiếm nhà hàng với danh mục: ${tag}`)
}

onMounted(() => {
  // Load particles.js if it's globally available
  if (window.particlesJS) {
    window.particlesJS('particles-js', {
      particles: {
        number: { value: 50, density: { enable: true, value_area: 800 } },
        color: { value: '#ffffff' },
        shape: { type: 'circle' },
        opacity: { value: 0.5, random: true },
        size: { value: 3, random: true },
        line_linked: { enable: false },
        move: { enable: true, speed: 2, direction: 'none', random: true }
      },
      interactivity: {
        detect_on: 'canvas',
        events: {
          onhover: { enable: true, mode: 'repulse' },
          onclick: { enable: true, mode: 'push' }
        },
        modes: {
          repulse: { distance: 100, duration: 0.4 },
          push: { particles_nb: 4 }
        }
      },
      retina_detect: true
    })
  }
})
</script>

<style scoped>
@import url('https://fonts.googleapis.com/css2?family=Montserrat:wght@800&display=swap');

body {
  margin: 0;
  font-family: 'Inter', sans-serif;
  background-image: url('https://images.unsplash.com/photo-1515003197210-e0cd71810b5f?ixlib=rb-4.0.3&auto=format&fit=crop&w=1920&q=80');
  background-size: cover;
  background-attachment: fixed;
  background-position: center;
}
.Part2{
    margin-top:5%;
}
.bg-overlay {
  background: rgb(255, 255, 255);
  min-height: 100vh;
}

.nav-item {
  transition: all 0.3s ease;
}
.nav-item:hover {
  transform: translateY(-2px);
}
.content-card {
  backdrop-filter: blur(8px);
  background: rgb(255, 255, 255);
  border: 1px solid rgb(255, 255, 255);
}
.header {
  background: linear-gradient(180deg, rgba(255, 87, 34, 0.9), rgba(255, 152, 0, 0.7));
  position: relative;
  clip-path: polygon(0 0, 100% 0, 100% 85%, 50% 100%, 0 85%);
  padding: 1.5rem 0;
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
  font-family: 'Montserrat', sans-serif;
  color: #fff;
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
.header-logo {
  filter: drop-shadow(0 0 8px rgba(255, 255, 255, 0.856));
  transition: transform 0.3s ease;
}
.header-logo:hover {
  transform: rotate(15deg) scale(1.1);
}
.header-content {
  display: flex;
  align-items: center;
  justify-content: space-between;
  max-width: 7xl;
  margin: 0 auto;
  padding: 0 1.5rem;
  z-index: 1;
  position: relative;
}
.tag-cloud {
  flex: 1;
  max-width: 400px;
  margin-top:80px;
  display: flex;
  justify-content: center;
  align-items: center;
  position: relative;
  height: 40px;
}
.tag {
  position: absolute;
  font-family: 'Montserrat', sans-serif;
  color: #fff;
  font-size: 0.9rem;
  font-weight: 600;
  background: rgba(255, 152, 0, 0.2);
  border-radius: 12px;
  padding: 0.3rem 0.8rem;
  cursor: pointer;
  transition: all 0.3s ease;
  text-shadow: 0 0 5px rgba(255, 152, 0, 0.8);
  animation: rotateTags 10s linear infinite;
}
.tag:hover {
  transform: scale(1.2) !important;
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
  border-radius: 9999px;
  font-weight: 600;
  transition: all 0.3s ease;
  display: flex;
  align-items: center;
  gap: 0.5rem;
}
.logout-btn:hover {
  background: #ff2e63;
  transform: translateY(-2px);
  box-shadow: 0 0 10px rgba(255, 87, 34, 0.5);
}
.text-sm{
margin-bottom:40%;
justify-content: center;
text-align: center;
}
</style>

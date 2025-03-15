<template>
  <div class="container">
    <div class="navbar">
      <div class="logo">
        <div class="logo-food">
          <i class="fa-solid fa-utensils"></i>  
        </div>
        <div class="Name">GRAP FOOD</div>
      </div>
      <div class="home-page">
        <button class="category food">🍔 Đồ ăn</button>
        <button class="category drink">🥤 Đồ uống</button>
        <input type="text" placeholder="Tìm kiếm món ăn..." class="search-input">
      </div>
      <div class="login">
        <i class="fas fa-user"></i>
        <div class="boder-login">
          <router-link to="/login" class="loginn">Đăng nhập</router-link>
          <router-link to="/register" class="register">Đăng ký</router-link>
        </div>
      </div>
    </div>
    <router-view></router-view>
    <div class="content"></div>
    <div class="footer"></div>
  </div>
</template>

<style scoped>
.container {
  position: relative;
  width: 100%;
  height: 100vh;
  background: url("https://watermark.lovepik.com/photo/20211118/large/lovepik-gourmet-background-picture_400152283.jpg") no-repeat center center/cover;
}
.navbar {
  position: fixed;
  z-index: 10;
  background-color: rgba(0, 0, 0, 0.5);
  color: white;
  padding: 10px;
}

.navbar .logo {
  display: flex;
  width: 150px;
  justify-content: space-around;
  font-size: 20px;
}

.home-page {
  display: flex;
  gap: 15px;
  align-items: center;
}

.category {
  padding: 10px 15px;
  border-radius: 8px;
  border: none;
  cursor: pointer;
  font-size: 16px;
  transition: 0.3s;
}

.food {
  background-color: #ff7f50; /* Màu cam */
  color: white;
}

.drink {
  background-color: #4682b4; /* Màu xanh dương */
  color: white;
}

.category:hover {
  opacity: 0.8;
}

.search-input {
  padding: 8px;
  font-size: 16px;
  border: 2px solid #ccc;
  border-radius: 5px;
  outline: none;
  width: 200px;
}

.search-input:focus {
  border-color: #ff7f50;
}

/* Định vị khu vực đăng nhập */
/* Vùng chứa icon đăng nhập */
.login {
  position: relative;
  display: flex;
  align-items: center;
  gap: 10px;
  cursor: pointer;
}

.login i {
  font-size: 24px;
  color: #6200ea;
  transition: 0.3s;
}

/* Hộp "Đăng nhập / Đăng ký" */
.boder-login {
  position: absolute;
  top: 100%;
  left: 50%;
  transform: translateX(-50%);
  display: flex;
  flex-direction: column;
  gap: 6px;
  align-items: center;
  padding: 12px 15px;
  width: 160px; /* Giảm độ rộng để thon hơn */
  
  /* Nền mờ giúp nhìn thấy ảnh nền */
  background: rgba(255, 255, 255, 0.2); 
  backdrop-filter: blur(10px); /* Làm mờ nền */
  border-radius: 12px;
  border: 1px solid rgba(255, 255, 255, 0.4);
  box-shadow: 0px 4px 12px rgba(0, 0, 0, 0.15);
  
  opacity: 0;
  visibility: hidden;
  transition: 0.3s ease-in-out;
  z-index: 10;
}

/* Khi hover vào icon, hiện hộp */
.login:hover .boder-login {
  opacity: 1;
  visibility: visible;
}

/* Nút đăng nhập / đăng ký */
.loginn,
.register {
  text-decoration: none;
  font-size: 14px;
  padding: 6px 12px;
  border-radius: 6px;
  transition: 0.3s;
  width: 100%;
  text-align: center;
  font-weight: 500;
}

/* Nút đăng nhập */
.loginn {
  color: #6200ea;
  border: 1px solid #6200ea;
  background: transparent;
}

.loginn:hover {
  background: #6200ea;
  color: white;
}

/* Nút đăng ký */
.register {
  color: white;
  background: #ff7f50;
  border: 1px solid #ff7f50;
}

.register:hover {
  background: #e66a3e;
  border-color: #e66a3e;
}


</style>

<template>
  <div class="grabfood-container">
    <div class="overlay"></div> <!-- Thêm lớp overlay mờ -->

    <div class="content">
      <h1 class="headline">Mang đến cho bạn món ăn ưa thích, nóng hổi và ngon lành</h1>
      <p class="description">
        Đặt đồ ăn giao hàng tận nhà nhanh chóng lấp đầy chiếc bụng đói của bạn với những món ngon yêu thích và dịch vụ giao hàng “thần tốc”. GrabFood hiện đang có mặt tại nhiều tỉnh thành ở Việt Nam: Thành phố Hồ Chí Minh, Hà Nội, Đà Nẵng, Vũng Tàu, Bình Dương, Đồng Nai, Cần Thơ, Đà Lạt,…. Chúng tôi đang dần mở rộng thêm nhiều khu vực trong thời gian tới!
      </p>

      <div class="circle-section">
        <div class="circle-item">
          <img src="/GrabFood-DAX-Desktop-Content-1-1.png" alt="Mobile Ordering" class="circle-image" />
          <h3 class="circle-title">Đặt đồ ăn online chỉ sau vài cú chạm.</h3>
          <p class="circle-text">GrabFood giao hàng nhanh thần tốc, đảm bảo mang cho bạn bữa ăn nóng hổi và ngon lành, dù bạn đang ở đâu.</p>
        </div>

        <div class="circle-item">
          <img src="/GrabFood-DAX-Desktop-Content-3.png" alt="Mobile Ordering" class="circle-image" />
          <h3 class="circle-title">Đa dạng lựa chọn.</h3>
          <p class="circle-text">Danh sách đa dạng các món ăn của chúng tôi có thể phục vụ cho mọi nhu cầu ăn uống của bạn.</p>
        </div>
      </div>

      <div class="food-list">
        <div class="food-item" v-for="res in paginatedRestaurants" :key="res.id">
          <img
            :src="res.image"
            class="food-image"
            alt="Restaurant Image"
            @error="handleImageError"
          />
          <div class="food-info">
            <div class="food-name">{{ res.name }}</div>
            <div class="food-shop">{{ res.address }}</div>
          </div>
        </div>
      </div>

      <!-- Pagination -->
     
    </div>
  </div>
</template>

<style scoped>
.grabfood-container {
  position: relative;
  max-width: 1100px;
  margin: 0 auto;
  padding: 40px 20px;
  text-align: center;
  background: url('/path/to/your/background.png') no-repeat center/cover; /* Background ở đây */
  border-radius: 20px;
  overflow: hidden;
}

/* lớp overlay đen mờ */
.overlay {
  position: absolute;
  inset: 0;
  background: rgba(0, 0, 0, 0.3); /* mờ 50% */
  z-index: 1;
}

/* nội dung nằm trên overlay */
.content {
  position: relative;
  z-index: 2;
}

/* headline */
.headline {
  font-size: 32px;
  font-weight: bold;
  color: #fff; /* đổi thành trắng */
  margin-bottom: 16px;
  text-shadow: 1px 1px 5px rgba(0, 0, 0, 0.7); /* thêm bóng chữ */
}

/* mô tả */
.description {
  font-size: 18px;
  color: #eee;
  margin-bottom: 40px;
  max-width: 700px;
  margin-left: auto;
  margin-right: auto;
  text-shadow: 1px 1px 5px rgba(0, 0, 0, 0.7);
}

/* Circle Section */
.circle-section {
  display: flex;
  justify-content: center;
  gap: 50px;
  margin-bottom: 60px;
  flex-wrap: wrap;
}

.circle-item {
  max-width: 300px;
  text-align: center;
}

.circle-image {
  width: 150px;
  height: 150px;
  object-fit: cover;
  border-radius: 50%;
  margin-bottom: 16px;
  box-shadow: 0 4px 8px rgba(0,0,0,0.15);
}

.circle-title {
  font-size: 20px;
  font-weight: bold;
  margin-bottom: 8px;
  color: #fff;
  text-shadow: 1px 1px 5px rgba(0, 0, 0, 0.7);
}

.circle-text {
  font-size: 16px;
  color: #ddd;
  text-shadow: 1px 1px 5px rgba(0, 0, 0, 0.7);
}

/* Food list */
.food-list {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(280px, 1fr));
  gap: 30px;
}

.food-item {
  background: #fff;
  border-radius: 16px;
  box-shadow: 0 4px 12px rgba(0, 0, 0, 0.08);
  overflow: hidden;
  transition: transform 0.3s;
}

.food-item:hover {
  transform: translateY(-8px);
}

.food-image {
  width: 100%;
  height: 200px;
  object-fit: cover;
}

.food-info {
  padding: 16px;
  text-align: left;
}

.food-name {
  font-size: 20px;
  font-weight: bold;
  color: #333;
  margin-bottom: 8px;
}

.food-shop {
  font-size: 16px;
  color: #777;
}

/* Pagination */
.pagination {
  margin-top: 30px;
  display: flex;
  justify-content: center;
  gap: 20px;
}

.arrow-button {
  background-color: #00b14f;
  border: none;
  color: white;
  width: 48px;
  height: 48px;
  border-radius: 50%;
  font-size: 24px;
  cursor: pointer;
  transition: background 0.3s;
}

.arrow-button:hover {
  background-color: #008f3f;
}
</style>

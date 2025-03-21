<template>
    <div class="dashboard-container">
      <!-- Sidebar -->
      <aside class="sidebar">
        <div class="logo">
        <div class="logo-food">
          <i class="fa-solid fa-utensils"></i>  
        </div>
        <div class="Name">GRAP FOOD</div>
      </div>
        <nav>
          <router-link to="/dashboard" class="nav-item">Trang chủ</router-link>
          <router-link to="/dashboard/products" class="nav-item">Sản phẩm</router-link>
          <router-link to="/dashboard/invoices" class="nav-item">Hóa đơn</router-link>
          <router-link to="/dashboard/statistics" class="nav-item">Thống kê</router-link>
        </nav>
      </aside>
  
      <!-- Main Content -->
      <main class="content">
        <h2>Danh mục sản phẩm</h2>
        <table class="product-table">
          <thead>
            <tr>
              <th>ID</th>
              <th>Tên</th>
              <th>Loại</th>
              <th>Mô tả</th>
              <th>Giá</th>
              <th>KM</th>
              <th>Ảnh</th>
              <th>Chi tiết</th>
              <th>Sửa - Xóa</th>
            </tr>
          </thead>
          <tbody>
            <!-- Dữ liệu từ database sẽ được bind vào đây -->
          </tbody>
        </table>
      </main>
    </div>
  </template>
  
  <style scoped>
  .dashboard-container {
    display: flex;
  }
  
  .sidebar {
    width: 250px;
    background: #f8f9fa;
    padding: 20px;
    height: 100vh;
    box-shadow: 2px 0 5px rgba(0, 0, 0, 0.1);
  }
  
  .logo {
    display: flex;
    gap: 10px;
    font-size: 20px;
    font-weight: bold;
    margin-bottom: 20px;
}
  
  .nav-item {
    display: block;
    padding: 10px;
    text-decoration: none;
    color: #333;
    font-weight: 500;
    transition: background 0.3s;
  }
  
  .nav-item:hover {
    background: #ddd;
    border-radius: 5px;
  }
  
  .content {
    flex-grow: 1;
    padding: 20px;
  }
  
  .product-table {
    width: 100%;
    border-collapse: collapse;
    margin-top: 20px;
  }
  
  .product-table th, .product-table td {
    border: 1px solid #ddd;
    padding: 10px;
    text-align: center;
  }
  
  .product-table th {
    background: #007bff;
    color: white;
  }
  </style>
  
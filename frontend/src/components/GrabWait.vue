<template>
  <div class="wrapper">
    <h1 class="title">Danh sách Grab chờ duyệt</h1>

    <div v-for="item in getdata" :key="item.IDGrabWait" class="card">
      <div class="card-content">
        <!-- Cột trái: Thông tin -->
        <div class="info">
          <h2 class="res-name">{{ item.Name }}</h2>
          <div class="contact-info">
            <div class="contact-row">
              <span class="contact-label">📍 Địa chỉ:</span>
              <span class="contact-value">{{ item.Address }}</span>
            </div>
            <div class="contact-row">
              <span class="contact-label">📞 SĐT:</span>
              <span class="contact-value">{{ item.Phone }}</span>
            </div>
          </div>
        </div>

        <!-- Cột phải: Hình ảnh -->
        <div class="image-gallery">
          <div class="image-block">
            <div class="image-label">CCCD mặt trước</div>
            <img :src="item.Url_Image" alt="Ảnh CCCD" @click="openPreview(item.Url_Image)" />
          </div>
          <div class="image-block">
            <div class="image-label">CCCD mặt sau</div>
            <img :src="item.Url_Image2" alt="Ảnh khu chế biến" @click="openPreview(item.Url_Image2)" />
          </div>
         
        </div>
      </div>

      <!-- Hành động -->
      <div class="actions">
        <button class="btn btn-approve" @click="approve(item.IDGrabWait)">Duyệt</button>
        <button class="btn btn-reject" @click="reject(item.IDGrabWait)">Từ chối</button>
      </div>
      <!-- Ảnh phóng to -->
<div v-if="previewImage" class="image-overlay" @click.self="closePreview">
  <div class="overlay-content">
    <button class="close-btn" @click="closePreview">✕</button>
    <img :src="previewImage" alt="Xem ảnh lớn" />
  </div>
</div>

    </div>
    <div v-if="getdata.length===0" class="card">
      <div class="info">
        <h2 class="res-name">Không có nhà hàng nào chờ duyệt</h2>
      </div>

    </div>
  </div>
</template>


<script setup>
import { onMounted, ref } from "vue";
import { getwaitgrabdata} from "../api/Admin.js";

import {deletewaitgrabdata} from "../api/Admin.js";
import { insertvalueingrabdata } from "../api/Admin.js";
const getdata = ref([]);
const previewImage = ref(null)

function openPreview(url) {
  previewImage.value = url
}

function closePreview() {
  previewImage.value = null
}

onMounted(async () => {
  try {
    const data = await getwaitgrabdata();
    console.log(data);
    getdata.value = data.map((item) => ({
      IDGrabWait: item.idGrabWait,
      Name: item.name,
      Address: item.address,
      Phone: item.phone,
      Pass:item.pass,
      Url_Image: item.url_Image,
      Url_Image2: item.url_Image2,
   
    }));
    console.log(getdata.value);
  } catch (error) {
    console.error("Lỗi khi lấy dữ liệu:", error);
  }
});


const approve=async (id)=>{
  console.log(id);
  const data={
    IDGrab: id,
    Name:getdata.value.find(item=>item.IDGrabWait===id).Name,
    Address:getdata.value.find(item=>item.IDGrabWait===id).Address,
    Phone:getdata.value.find(item=>item.IDGrabWait===id).Phone,

    Pass:getdata.value.find(item=>item.IDGrabWait===id).Pass,
    Role:"Grab",


  }
  console.log(data);
    
    try{
        const response=await insertvalueingrabdata(data);
     
            getdata.value = getdata.value.filter(item => item.IDGrabWait !== id);


           
    console.log("Duyệt thành công:", response.data);

        console.log(response.data);
        
    }
    catch(error){
        console.error("Lỗi khi duyệt nhà hàng:", error);
    }

}
const reject=async(id)=>{
  try{
    const response=await deletewaitgrabdata(id);
    getdata.value = getdata.value.filter(item => item.IDGrabWait !== id);
  }
  catch(error){
    console.error("Lỗi khi từ chối nhà hàng:", error);
  }}

</script>

<style>
.wrapper {
  padding: 30px;
  background-color: #f3f3f3;
  min-height: 100vh;
  font-family: Arial, sans-serif;
}

.title {
  text-align: center;
  font-size: 26px;
  font-weight: bold;
  margin-bottom: 30px;
  color: #222;
}

.card {
  background: #fff;
  border-radius: 16px;
  padding: 20px;
  margin-bottom: 24px;
  box-shadow: 0 4px 12px rgba(0, 0, 0, 0.08);
}

.info {
  margin-bottom: 16px;
}

.res-name {
  font-size: 20px;
  font-weight: bold;
  color: #00b14f;
  margin-bottom: 8px;
}




.actions {
  display: flex;
  gap: 10px;
}

.btn {
  padding: 10px 16px;
  border: none;
  border-radius: 6px;
  font-weight: bold;
  cursor: pointer;
  font-size: 14px;
  transition: background 0.3s;
}

.btn-approve {
  background-color: #00b14f;
  color: white;
}

.btn-approve:hover {
  background-color: #019c47;
}

.btn-reject {
  background-color: #e74c3c;
  color: white;
}

.btn-reject:hover {
  background-color: #c0392b;
}.contact-info {
 background-color: #f9f9f9;
border-radius: 12px;
padding: 12px 16px;
 border: 1px solid #e0e0e0;
 box-shadow: inset 0 1px 3px rgba(0, 0, 0, 0.03);
}

.contact-row {
 display: flex;
align-items: center;
margin-bottom: 8px;
}

.contact-label {
 font-weight: 600;
 width: 100px;
color: #333;
}

.contact-value {
 color: #555;
flex: 1;
 word-break: break-word;
}
.card-content {
  display: flex;
  flex-wrap: wrap;
  gap: 24px;
  justify-content: space-between;
  margin-bottom: 16px;
}

.info {
  flex: 1 1 300px;
}

.image-gallery {
  flex: 1 1 300px;
  display: flex;
  flex-wrap: nowrap;
  gap: 24px;
  justify-content: flex-start;
  align-items: flex-start;
}

.image-block {
  flex: 1;
  max-width: 180px;
  text-align: center;
}

.image-label {
  font-size: 14px;
  color: #444;
  margin-bottom: 8px;
  padding-right:20%;
  font-weight: 500;
}

.image-block img {
  width: 80%;
  aspect-ratio: 1 / 1; /* đảm bảo hình vuông */
  object-fit: cover;
  border-radius: 12px;
  box-shadow: 0 4px 12px rgba(0, 0, 0, 0.08);
  border: 1px solid #ddd;
}
.image-overlay {
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background-color: rgba(0, 0, 0, 0.8);
  display: flex;
  justify-content: center;
  align-items: center;
  z-index: 999;
}

.overlay-content {
  position: relative;
  max-width: 90%;
  max-height: 90%;
  overflow: hidden;
  border-radius: 12px;
}


.overlay-content img {
  width: 100%;
  height: auto;
  border-radius: 12px;
  box-shadow: 0 8px 24px rgba(0, 0, 0, 0.4);
}

.close-btn {
  position: absolute;
  top: 8px;
  width:3%;
  right: 8px;
  background: rgba(255, 255, 255, 0.85);
  border: none;
    font-size: 20px;
  font-weight: bold;
  line-height: 1;
  border-radius: 100px;
  padding: 4px 8px;
  cursor: pointer;
  box-shadow: 0 2px 6px rgba(0, 0, 0, 0.2);
  transition: background 0.3s;
}

.close-btn:hover {
  background: rgba(255, 255, 255, 1);
}







</style>

<template>
  <div class="min-h-screen bg-gray-50 font-sans">
    <!-- Header -->
    <header class="bg-white shadow-md sticky top-0 z-50">
      <div class="container mx-auto px-4 py-4 flex flex-col md:flex-row items-center justify-between gap-4">
        <h1 class="text-2xl font-bold text-green-600">📋 Tổng Grab đối tác: {{ dulieu.length }}</h1>
        <div class="relative w-full max-w-md">
          <input
            type="text"
            placeholder="🔍 Tìm kiếm đối tác..."
            class="w-full py-2 px-4 pr-10 rounded-full bg-gray-100 text-gray-800 placeholder-gray-500 focus:outline-none focus:ring-2 focus:ring-green-400 shadow-sm"
            @input="searchRestaurant"
          />
          <svg
            class="absolute right-3 top-1/2 transform -translate-y-1/2 h-5 w-5 text-gray-500 pointer-events-none"
            fill="none"
            stroke="currentColor"
            viewBox="0 0 24 24"
          >
            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2"
              d="M21 21l-6-6m2-5a7 7 0 11-14 0 7 7 0 0114 0z" />
          </svg>
        </div>
      </div>
    </header>

    <!-- Content -->
    <main class="container mx-auto px-4 py-8">
      <h2 class="text-3xl font-semibold text-gray-800 mb-6 text-center">📦 Danh sách đối tác</h2>

      <!-- Danh sách -->
      <!-- Danh sách dạng danh bạ -->
<ul v-if="paginatedData.length > 0" class="space-y-4">
 <li
v-for="item in paginatedData"
:key="item.ID"
class="bg-white rounded-xl shadow p-4 flex items-center justify-between hover:bg-gray-50 transition"
>
<div>
<h3 class="text-lg font-semibold text-gray-800">{{ item.Name }}</h3>
 <p class="text-gray-600 text-sm">{{ item.Address || 'Địa chỉ chưa cập nhật' }}</p>
<p class="text-gray-500 text-sm">📞 {{ item.Phone }}</p>
</div>
 <button
 class="ml-4 bg-green-500 text-white px-4 py-1.5 rounded-full text-sm hover:bg-green-600 transition"
>
Grab đối tác
</button>
</li>
</ul>


      <!-- Đang tải -->
      <div v-else class="text-center text-gray-600 mt-10">
        <p>⏳ Đang tải dữ liệu...</p>
      </div>

      <!-- Pagination -->
      <div class="mt-10 flex justify-center flex-wrap gap-2" v-if="totalPages > 1">
        <button
          @click="goToPage(currentPage - 1)"
          :disabled="currentPage === 1"
          class="px-4 py-2 rounded-full bg-gray-200 hover:bg-gray-300 disabled:opacity-50"
        >
          ← Trước
        </button>

        <button
          v-for="page in totalPages"
          :key="page"
          @click="goToPage(page)"
          :class="[
            'px-4 py-2 rounded-full',
            currentPage === page ? 'bg-green-500 text-white' : 'bg-gray-100 hover:bg-gray-200'
          ]"
        >
          {{ page }}
        </button>

        <button
          @click="goToPage(currentPage + 1)"
          :disabled="currentPage === totalPages"
          class="px-4 py-2 rounded-full bg-gray-200 hover:bg-gray-300 disabled:opacity-50"
        >
          Tiếp →
        </button>
      </div>
    </main>

    <!-- Footer -->
    <footer class="bg-gray-800 text-white py-6 mt-10">
      <div class="container mx-auto px-4 text-center text-sm">
        &copy; 2025 GrabFood | Thiết kế bởi bạn 
      </div>
    </footer>
  </div>
</template>


<script setup>
import { onMounted, ref, computed } from 'vue'
import {  GetAllGrab } from '../api/grab.js' 

const dulieu = ref([])
const filteredData = ref([]) 
const currentPage = ref(1)
const itemsPerPage = 6

const totalPages = computed(() =>
  Math.ceil(filteredData.value.length / itemsPerPage)
)

const searchRestaurant = () => {
  const searchInput = document.querySelector('input[type="text"]');
  filteredData.value = dulieu.value.filter(item =>
    item.Name.toLowerCase().includes(searchInput.value.toLowerCase())
  )
  currentPage.value = 1 
}

const paginatedData = computed(() => {
  const start = (currentPage.value - 1) * itemsPerPage
  return filteredData.value.slice(start, start + itemsPerPage)
})
const goToPage = (page) => {
 if (page >= 1 && page <= totalPages.value) {
currentPage.value = page
}
}


onMounted(async () => {
  try {
    const data = await  GetAllGrab()
    console.log(data);
    dulieu.value = data.map(item => ({
      ID: item.idRes,
      Name: item.name,
        Address: item.address,
      Phone: item.phone,
   
    }))
    console.log(dulieu);
    filteredData.value = dulieu.value,
    console.log(dulieu.value.length);
  } catch (error) {
    console.error('Lỗi khi lấy dữ liệu:', error)
  }
})

</script>

<style>
@import 'https://cdnjs.cloudflare.com/ajax/libs/tailwindcss/2.2.19/tailwind.min.css';
</style>

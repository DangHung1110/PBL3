<template>
  <div class="min-h-screen bg-gray-100 font-sans">
    <!-- Header -->
    <header class="bg-gradient-to-r from-orange-500 to-red-500 text-white shadow-lg">
      <div class="container mx-auto px-4 py-6 flex items-center justify-between">
        <h1 class="text-xl md:text-2xl font-bold tracking-tight" style="color: #00B14F;">
          THE BEST OR NOTHING
        </h1>
        <div class="relative w-full max-w-md">
        <input
  type="text"
  placeholder="Tìm kiếm nhà hàng..."
 class="w-full py-2 px-4 rounded-full bg-white text-black placeholder-gray-500 focus:outline-none focus:ring-2 focus:ring-orange-400"
  @input="searchRestaurant"
/>

          <svg
            class="absolute right-3 top-1/2 -translate-y-1/2 h-5 w-5 text-white"
            fill="none"
            stroke="currentColor"
            viewBox="0 0 24 24"
          >
            <path
              stroke-linecap="round"
              stroke-linejoin="round"
              stroke-width="2"
              d="M21 21l-6-6m2-5a7 7 0 11-14 0 7 7 0 0114 0z"
            />
          </svg>
        </div>
      </div>
    </header>

    <!-- Restaurant List -->
    <main class="container mx-auto px-4 py-8">
      <h2 class="text-2xl md:text-3xl font-semibold text-gray-800 mb-6">Danh sách nhà hàng</h2>

      <div v-if="paginatedData.length > 0" class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
        <div
          v-for="item in paginatedData"
          :key="item.ID"
          class="bg-white rounded-xl shadow-lg overflow-hidden transform hover:scale-105 transition-transform duration-300"
        >
          <div class="h-48 bg-gray-200">
            <img
              src="https://via.placeholder.com/400x200?text=Restaurant+Image"
              alt="Restaurant Image"
              class="w-full h-full object-cover"
            />
          </div>
          <div class="p-6">
            <h3 class="text-xl font-semibold text-gray-800">{{ item.Name }}</h3>
            <p class="text-gray-600 mt-2">{{ item.Address }}</p>
            <p class="text-gray-500 mt-1">SĐT: {{ item.Phone }}</p>
            <button
              class="mt-4 bg-orange-500 text-white px-4 py-2 rounded-full hover:bg-orange-600 transition-colors duration-200"
            >
              Xem Menu
            </button>
          </div>
        </div>
      </div>
      <div v-else class="text-center text-gray-600">
        <p>Đang tải dữ liệu...</p>
      </div>

      <!-- Pagination -->
      <div class="mt-8 flex justify-center space-x-2" v-if="totalPages > 1">
        <button
          @click="goToPage(currentPage - 1)"
          :disabled="currentPage === 1"
          class="px-4 py-2 rounded-full bg-gray-300 hover:bg-gray-400 disabled:opacity-50"
        >
          Trước
        </button>

        <button
          v-for="page in totalPages"
          :key="page"
          @click="goToPage(page)"
          :class="[
            'px-4 py-2 rounded-full',
            currentPage === page ? 'bg-orange-500 text-white' : 'bg-gray-200 hover:bg-gray-300'
          ]"
        >
          {{ page }}
        </button>

        <button
          @click="goToPage(currentPage + 1)"
          :disabled="currentPage === totalPages"
          class="px-4 py-2 rounded-full bg-gray-300 hover:bg-gray-400 disabled:opacity-50"
        >
          Tiếp
        </button>
      </div>
    </main>

    <!-- Footer -->
    <footer class="bg-gray-800 text-white py-6">
      <div class="container mx-auto px-4 text-center">
        <p>&copy; 2025 Ẩm Thực Việt. All rights reserved.</p>
      </div>
    </footer>
  </div>
</template>

<script setup>
import { onMounted, ref, computed } from 'vue'
import { getAllRes } from '../api/restaurant.js' 

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
    const data = await getAllRes()
    dulieu.value = data.map(item => ({
      ID: item.idRes,
      Name: item.name,
      Address: item.address,
      Phone: item.phone
    }))
    filteredData.value = dulieu.value 
  } catch (error) {
    console.error('Lỗi khi lấy dữ liệu:', error)
  }
})

</script>

<style>
@import 'https://cdnjs.cloudflare.com/ajax/libs/tailwindcss/2.2.19/tailwind.min.css';
</style>

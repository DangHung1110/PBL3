<template>
        <div class="overlay" @click="close"></div>
    <div v-if="visible" class="popup-container">
        <div class="popup-content">
            <h2 class="title">Cập nhật món ăn</h2>

            <form @submit.prevent="handleSubmit">
                <!-- Hiển thị tên món ăn mà không cho phép sửa -->
                <div class="mb-3">
                    <label class="block text-gray-700">Tên</label>
                    <p class="w-full border p-2 rounded bg-gray-100 text-gray-600">{{ food.name }}</p>
                </div>

                <div class="mb-3">
                    <label class="block text-gray-700">Giá</label>
                    <input type="number" v-model.number="food.price" class="w-full border p-2 rounded"
                        placeholder="Nhập giá món ăn" />
                </div>

                <div class="mb-3">
                    <label class="block text-gray-700">Khuyến mãi (%)</label>
                    <input type="number" v-model.number="food.discount" class="w-full border p-2 rounded"
                        placeholder="Nhập khuyến mãi" />
                </div>

                <div class="mb-3">
                    <label class="block text-gray-700">Số lượng</label>
                    <input type="number" v-model.number="food.quantity" class="w-full border p-2 rounded"
                        placeholder="Nhập số lượng món ăn" />
                </div>

                <div class="flex justify-end gap-2 mt-4">
                    <button type="button" @click="close" class="px-4 py-2 bg-gray-400 text-white rounded">
                        Hủy
                    </button>
                    <button type="submit" class="px-4 py-2 bg-blue-500 text-white rounded">
                        Cập nhật
                    </button>
                </div>
            </form>
        </div>
    </div>
</template>

<script setup>
import { reactive } from 'vue'
import { updateFood } from '../api/FoodSevice.js'

const props = defineProps({
    visible: Boolean,
    food: Object
})
const emit = defineEmits(['close', 'updated'])

// Clone food object to avoid mutating props directly
const food = reactive({ ...props.food })

// Handle form submission
const handleSubmit = async () => {
    const updateData = {}

    if (food.price !== undefined) {
    const priceStr = food.price.toString().replace('đ', '').replaceAll('.', '').trim();
    updateData.Price = parseInt(priceStr, 10);
}

if (food.discount !== undefined) {
    const discountStr = food.discount.toString().replace('%', '').trim();
    updateData.Discount = parseInt(discountStr, 10);
}

if (food.quantity !== undefined) {
    updateData.Quantity = parseInt(food.quantity, 10);
}


    console.log("Dữ liệu gửi lên:", updateData); // Debug log để xem dữ liệu gửi lên

    try {
        await updateFood(food.id, updateData)
        console.log('id:', food.id)
        alert('Cập nhật thành công!')
        emit('updated')
        close()
    } catch (err) {
        alert('Cập nhật thất bại!')
        console.error(err)
    }
}

// Close the popup
const close = () => {
    emit('close')
}
</script>

<style scoped>
.popup-container {
    position: fixed;
    top: 0;
    left: 0;
    right: 0;
    bottom: 0;
    display: flex;
    justify-content: center;
    align-items: center;
    background: rgba(0, 0, 0, 0.5);  /* Màu nền mờ */
    z-index: 9999;  /* Đảm bảo popup nổi bật */
    animation: fadeIn 0.3s ease-out; /* Hiệu ứng khi popup xuất hiện */
}

@keyframes fadeIn {
    from {
        opacity: 0;
    }
    to {
        opacity: 1;
    }
}

.popup-content {
    background: #fff;
    padding: 20px;
    border-radius: 12px; /* Bo góc mềm mại */
    width: 100%;
    max-width: 500px; /* Giới hạn chiều rộng */
    box-shadow: 0 12px 24px rgba(0, 0, 0, 0.1);
    overflow-y: auto;
    transition: transform 0.3s ease-in-out; /* Hiệu ứng chuyển động */
    transform: scale(1.1);
}

.popup-content.show {
    transform: scale(1); /* Hiển thị popup với hiệu ứng */
}

.title {
    font-size: 24px;
    font-weight: bold;
    color: #333;
    margin-bottom: 20px;
    text-align: center;
}

.mb-3 {
    margin-bottom: 20px;
    width: 95%;
}

button {
    padding: 10px 20px;
    border-radius: 8px;
    cursor: pointer;
    border: none;
    transition: background 0.3s, transform 0.3s;
    font-weight: bold;
    text-transform: uppercase;
}

button:hover {
    opacity: 0.8;
    transform: translateY(-2px); /* Hiệu ứng nhấc lên khi hover */
}

.bg-gray-400 {
    background-color: #BDC3C7;
}

.bg-blue-500 {
    background-color: #3498db;
}

input, p {
    width: 100%;
    padding: 12px;
    border-radius: 6px;
    border: 1px solid #ccc;
    margin-top: 8px;
    font-size: 16px;
}

input:focus, p:focus {
    outline: none;
    border-color: #3498db; /* Đổi màu border khi focus */
    box-shadow: 0 0 4px rgba(52, 152, 219, 0.6); /* Tạo hiệu ứng sáng khi focus */
}

input {
    background-color: #fafafa;
    color: #333;
}

p {
    background-color: #f1f1f1;
    color: #666;
    cursor: not-allowed;
    font-weight: 600;
}

.flex {
    display: flex;
    justify-content: flex-end;
    gap: 15px;
}

button[type="submit"], button[type="button"] {
    width: 120px;
    padding: 10px;
}

@media (max-width: 768px) {
    .popup-container {
        padding: 20px;
    }

    .popup-content {
        max-width: 90%;
    }
}
</style>
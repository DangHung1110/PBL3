<template>
    <div class="overlay" @click="close"></div>
<div v-if="visible" class="popup-container">
    <div class="popup-content">
        <h2 class="title">Cập nhật món ăn</h2>

        <form @submit.prevent="handleSubmit">
          
            

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
    background: rgba(0, 0, 0, 0.6);
    z-index: 9999;
    animation: fadeIn 0.3s ease-out;
    font-family: 'Helvetica Neue', Helvetica, Arial, sans-serif;
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
    padding: 24px;
    border-radius: 12px;
    width: 100%;
    max-width: 500px;
    box-shadow: 0 10px 25px rgba(0, 0, 0, 0.15);
    transform: scale(1.05);
    transition: all 0.3s ease-in-out;
    border: 1px solid #f6f6f6;
}

.title {
    font-size: 22px;
    font-weight: 600;
    color: #159d03;
    text-align: center;
    margin-bottom: 24px;
}

.mb-3 {
    margin-bottom: 18px;
    width: 96%;
}

label {
    font-size: 14px;
    font-weight: 500;
    color: #333;
    margin-bottom: 6px;
    display: block;
}

input {
    width: 100%;
    padding: 10px 14px;
    border-radius: 8px;
    border: 1px solid #ccc;
    background: #fafafa;
    font-size: 15px;
    transition: border-color 0.3s, box-shadow 0.3s;
}

input:focus {
    border-color: #33b200;
    box-shadow: 0 0 0 2px rgba(238, 77, 45, 0.2);
    outline: none;
}

.flex {
    display: flex;
    justify-content: flex-end;
    gap: 12px;
    margin-top: 20px;
}

button {
    border: none;
    border-radius: 6px;
    padding: 10px 16px;
    font-weight: 600;
    cursor: pointer;
    transition: background-color 0.3s, transform 0.2s;
    text-transform: uppercase;
    font-size: 14px;
}

button:hover {
    transform: translateY(-1px);
    opacity: 0.9;
}

.bg-gray-400 {
    background-color: #b0b0b0;
    color: #fff;
}

.bg-blue-500 {
    background-color: #157e00;
    color: #fff;
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
import axios from "axios";

export const getAllRes = async () => {
    try {
        const response = await axios.get("http://localhost:5299/api/Restaurant/GetAllRestaurants");
        return response.data;
    } catch (error) {
        console.error("Lỗi khi gọi API lấy danh sách nhà hàng:", error);
        throw error;
    }
}

export const GetRestaurantById = async (IDRes) => {
    try {
        const response = await axios.get(`http://localhost:5299/api/Restaurant/GetRestaurantById/${IDRes}`);
        return response.data;
    } catch (error) {
        console.error("Lỗi khi gọi API lấy nhà hàng:", error);
        throw error;
    }
}
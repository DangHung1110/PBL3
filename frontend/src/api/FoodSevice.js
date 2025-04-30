import axios from 'axios';
export const GetFoodByName=async(Name)=>{
  try{
    const response=await axios.get(`http://localhost:5299/api/Restaurant/GetFoodByName/${Name}`);
    return response.data;
  }
  catch(error)
  {
    console.error("Lỗi khi gọi API lấy món ăn: theo tên",error);
    throw error;
  }
}
export const getFoodsByRestaurantId = async (IDRes) => {
  try {
    const response = await axios.get(`http://localhost:5299/api/restaurant/foods/${IDRes}`);
    return response.data;
  } catch (error) {
    console.error('Lỗi khi gọi API lấy danh sách món ăn:', error);
    throw error;
  }
};

export const getDoAn = async () => {
    try {
      const response = await axios.get(`http://localhost:5299/api/restaurant/DoAn`);
      return response.data;
    } catch (error) {
      console.error("Lỗi khi gọi API lấy Đồ ăn:", error);
      throw error;
    }
  };
  
  export const getDoUong = async () => {
    try {
      const response = await axios.get(`http://localhost:5299/api/restaurant/DoUong`);
      return response.data;
    } catch (error) {
      console.error("Lỗi khi gọi API lấy Đồ uống:", error);
      throw error;
    }
  };

export const deleteFood = async (id) => {
    const response = await fetch(`http://localhost:5299/api/Restaurant/deletefood/${id}`, {
      method: 'DELETE'
    });
  
    if (!response.ok) {
      throw new Error("Xoá món ăn thất bại");
    }
  
    return await response.json();
};

export const addFood = async (foodData) => {
    const formData = new FormData();
    formData.append('Name', foodData.name);
    formData.append('Price', foodData.price);
    formData.append('Discount', foodData.discount);
    formData.append('Category', foodData.category);
    formData.append('Quantity', foodData.quantity);
    formData.append('IDRes', foodData.idRes);
    formData.append('Url_Image', foodData.image); // phải đúng key backend mong đợi
  
    const response = await axios.post('http://localhost:5299/api/restaurant/uploadfood', formData);
    return response.data;
};
  
export const GetFoodById = async (id) => {
    try {
      const response = await axios.get(`http://localhost:5299/api/restaurant/GetFoodById/${id}`);
      return response.data;
    } catch (error) {
      console.error("Lỗi khi gọi API lấy món ăn:", error);
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
export const changefoodnum=async(id,quantity)=>{
  try{
    const response=await axios.put(`http://localhost:5299/api/Restaurant/ChangeFoodNum/${id}`,quantity);
    return response.data;
  }
  catch(error)
  {
    console.error("Lỗi khi gọi API thay đổi số lượng món ăn:",error);
    throw error;
  }
}

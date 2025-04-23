import axios from 'axios';
export const deleteFood = async (id) => {
    const response = await fetch(`http://localhost:5299/api/Customer/order`, {
      method: 'DELETE'
    });
  
    if (!response.ok) {
      throw new Error("Xoá món ăn thất bại");
    }
  
    return await response.json();
};
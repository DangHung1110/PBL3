import axios from 'axios';

export const addOrder = async (orderData) => {
  try {
    console.log(orderData);
    const response = await axios.post('http://localhost:5299/api/Customer/order', orderData);
    return response.data;
  } catch (error) {
    console.error('Error when sending order:', error);
    throw error;
  }
};
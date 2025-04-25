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

export const ThongkeOrder=async (ID)=>{
  try{
    console.log(ID);
    const response=await axios.get(`http://localhost:5299/api/Customer/Thongkeorder/${ID}`);
    console.log(response.data);
    return response.data;
  }
  catch(error)
  {
    console.error('Error when listing order',error);
    throw error;
  }
};

export const ResOrderList = async (IDRes) => {
  try {
    const response = await axios.get(`http://localhost:5299/api/Restaurant/GetOrderDetailsByRestaurant/${IDRes}`);
    return response.data;
  } catch (error) {
    console.error('Error when listing order:', error);
    throw error;
  }
}

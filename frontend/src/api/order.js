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
  }}
export const DeleteOrder= async (id) => {
  console.log(id);
  const response = await fetch(`http://localhost:5299/api/Customer/DeleteOrderDetail/${id}`, {
    method: 'DELETE'
  });

  if (!response.ok) {
    throw new Error("Xoá món ăn thất bại");
  }

  return true;
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
export const UpdateConfirmedTime=async(IDOrder)=>{
  try{
    const response=await axios.put(`http://localhost:5299/api/Restaurant/UpdateConfirmedTime/${IDOrder}`);
    return response.data;
}
catch(error)
{
    console.error('Error when updating confirmed time:',error);
    throw error;
}}
export const ProductCount=async(IDCus)=>{
  try{
    const response=await axios.get(`http://localhost:5299/api/Customer/ProductCount/${IDCus}`);
    console.log(response.data);
    return response.data;
  }
  catch(error)
  {
    console.error('Error when getting product count:',error);
    throw error;
  }
}
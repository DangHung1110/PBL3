import axios from 'axios';
export const Thongke=async (thongke)=>{
    try{
        const response=await axios.post(`http://localhost:5299/api/Restaurant/Thongke`,thongke);
        return response.data;
    }
    catch(error)
    {
        console.error("Lỗi khi gọi API lấy thống kê:",error);
        throw error;
    }


}
export const getTKData=async (IDCustomer)=>{
    try{
        const response=await axios.get(`http://localhost:5299/api/Restaurant/GetTKData/${IDCustomer}`);
        return response.data;
    }
    catch(error)
    {
        console.error("Lỗi khi gọi API lấy thống kê:",error);
        throw error;
    }
}
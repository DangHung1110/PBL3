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
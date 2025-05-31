import axios from "axios";
export const GetRevenueData= async(IDCus)=>{
    try{
        const response= await axios.get(`http://localhost:5299/api/Restaurant/GetRevenue/${IDCus}`);
        return response.data;

    }
    catch{error}
    {
        console.error("Lỗi khi gọi API lấy doanh thu:",error);
        throw error;
    }
}
export const GetRevenueAdmin=async()=>{
    try{
        const response= await axios.get("http://localhost:5299/Admin/GetRevenueAdmin");
        return response.data;
    }
    catch(error){
        console.error("Lỗi khi gọi API lấy doanh thu admin:",error);
        throw error;
    }
}
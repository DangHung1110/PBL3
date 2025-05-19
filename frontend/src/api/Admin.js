import axios from 'axios';
export const getwaitresdata=async()=>{
    try{
        const response=await axios.get("http://localhost:5299/Admin/restaurantwait");
        console.log(response.data);
        return response.data;
    }
    catch(error){
        console.error("Lỗi lấy dữ liệu nhà hàng chờ:",error);
        return null;
    }
}
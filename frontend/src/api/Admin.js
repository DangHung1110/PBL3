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
export const insertvalueinresdata=async(data)=>{
    try{
        const response=await axios.post("http://localhost:5299/api/auth/restaurant/signupinresafterwait",data);
        console.log(response.data);
        return response.data;
    }
    catch(error){
        console.error("Lỗi thêm dữ liệu nhà hàng:",error);
        return null;
    }
}
export const deletewaitresdata=async(id)=>{
    try{
        const response=await axios.delete(`http://localhost:5299/Admin/deletewaitresdata/${id}`);
        console.log(response.data);
        return response.data;
    }
    catch(error){
        console.error("Lỗi xóa dữ liệu nhà hàng chờ:",error);
        return null;
    }
}
export const getwaitgrabdata=async()=>{
 try{
    const response=await axios.get("http://localhost:5299/Admin/grabwait");
    console.log(response.data);
    return response.data;
 }
    catch(error){
        console.error("Lỗi lấy dữ liệu grab chờ:",error);
        return null;
    }
}
export const insertvalueingrabdata =async(data)=>{
    try{
        const response=await axios.post("http://localhost:5299/Admin/grab/signupingrabafterwait",data);
        console.log(response.data);
        return response.data;
    }
    catch(error){
        console.error("Lỗi thêm dữ liệu grab:",error);
        return null;
    }

}
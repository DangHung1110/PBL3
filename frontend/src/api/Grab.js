import axios from 'axios';
export const GetGrabData=async(IDGrab)=>{
    try{
        const response=await axios.get(`http://localhost:5299/api/Grab/ThongkeGrab/${IDGrab}`);
        console.log(response.data);
        return response.data;

    }
    catch(error)
  {
    console.error("Lỗi khi gọi API lấy món ăn của Grab",error);
    throw error;
  }

}
export const Thongkegrab=async(DataTKGrab)=>{
  try{
    const response=await axios.post(`http://localhost:5299/api/Grab/PostTKGrab`,DataTKGrab);
    console.log(response.data);
    return response.data;
  }
      catch(error)
  {
    console.error("Lỗi khi đẩy dữ liêụ lên  Grab",error);
    throw error;
  }
}
export const getthongkegrab=async(IDGrab)=>{
  try{
    const response=await axios.get(`http://localhost:5299/api/Grab/ThongkeGrabStatistic/${IDGrab}`);
    console.log(response.data);
    return response.data;
  }
  catch(error)
  {
    console.error("Lỗi khi lấy thống kê từ Grab",error);
    throw error;
  }
}
export const GetAllGrab=async()=>{
  try{
    const response=await axios.get(`http://localhost:5299/api/Grab/GetAllGrab`);
    console.log(response.data);
    return response.data;
  }
  catch(error)
  {
    console.error("Lỗi khi lấy tất cả Grab",error);
    throw error;
  }
}
export const DeleteGrab=async(IDGrab)=>{
  try{
    const response=await axios.delete(`http://localhost:5299/api/Grab/DeleteGrab/${IDGrab}`)
    console.log(response);
    return response.data;
  }
  catch(error)
  {
     console.error("Lỗi khi xóa đi Grab",error);
    throw error;
  }
}
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
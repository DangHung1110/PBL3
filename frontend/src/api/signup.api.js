import axios from "axios";
export async function SignUpUser(role,username,password,phone,address) {
  const data={
    Role:role,
    Name:username,
    Pass:password,
    Phone:phone,
    Address:address
  }
    try {
        const response = await axios.post("http://localhost:5299/api/auth/customer/signup",data);
        return response.data;
    } catch (error) {
        console.error("Lỗi khi gọi API đăng ký:", error);
        throw error;
    }                             
}
export async function SignUpRes(role, username, password, phone, address,restaurantName, imageUrl, imageUrl2, imageUrl3) {
  const data = {
    Role: role,
    Name: restaurantName,
    Phone: phone,
    Address: address,
    Pass: password,
    Url_Image: imageUrl,
    Url_Image2: imageUrl2,
    Url_Image3: imageUrl3
  };
  console.log(data);

  try {
    const response = await axios.post("http://localhost:5299/api/auth/restaurant/signup", data);
    console.log(response.data);
    return response.data;
  } catch (error) {
    console.error("Lỗi khi gọi API đăng ký:", error);
    throw error;
  }
}
export async function SignUpGrab(role, username, password, phone, address,restaurantName, imageUrl, imageUrl2) {
  const data = {
    Role: role,
    Name: restaurantName,
    Phone: phone,
    Address: address,
    Pass: password,
    Url_Image: imageUrl,
    Url_Image2: imageUrl2,

  };
  console.log(data);

  try {
    const response = await axios.post("http://localhost:5299/api/auth/grab/signupwait", data);
    console.log(response.data);
    return response.data;
  } catch (error) {
    console.error("Lỗi khi gọi API đăng ký:", error);
    throw error;
  }
}
export const ImageDeal = async (restaurantImageFile) => {
  if (!restaurantImageFile) {
    throw new Error("No file selected");
  }
  const formData = new FormData();
  formData.append("resImage", restaurantImageFile); 

  try {
    const response = await axios.post(
      "http://localhost:5299/api/auth/restaurant/Image_Deal",
      formData,
      {
        headers: {
          "Content-Type": "multipart/form-data",
        },
      }
    );
    console.log(response.data);
    return response.data;
  } catch (error) {
    console.error("Lỗi khi đẩy dữ liệu lên Deal", error);
    throw error;
  }
};




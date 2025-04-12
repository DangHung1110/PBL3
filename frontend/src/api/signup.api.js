import axios from "axios";
export async function signup(username, password, role, imageUrl, phone, address, restaurantName) {
  try {
    if (role === "user") {
      const data = {
        Name: username,
        Address: address,
        Phone: phone,
        Pass: password
      };
      console.log("Dữ liệu gửi cho customer:", data); // 👈 LOG để kiểm tra

      const res = await axios.post("http://localhost:5299/api/auth/customer/signup", data);
      return { success: true, data: res.data };
    } else if (role === "restaurant") {
      const data = {
        Name: restaurantName,
        Address: address,
        Phone: phone,
        Pass: password,
        Image: imageUrl
      };
      console.log("Dữ liệu gửi cho restaurant:", data);

      const res = await axios.post("http://localhost:5299/api/auth/restaurant/signup", data);
      return { success: true, data: res.data };
    }
  } catch (error) {
    console.error("Lỗi đăng ký:", error.response?.data || error.message);
    return { success: false, message: error.message };
  }
}


import axios from "axios";

const API_URL = "http://localhost:5299/api/auth"; // API của bạn

export const login = async (username, password, role) => {
  try {
    const response = await axios.post(`${API_URL}/login`, {
      Name: username,
      Pass: password,
      Role: role, // Gửi role lên server để xác định bảng
    });
    return response.data;
  } catch (error) {
    console.error("Lỗi đăng nhập:", error);
    return null;
  }
};

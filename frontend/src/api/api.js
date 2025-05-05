import axios from "axios";

const API_URL = "http://localhost:5299/api/auth"; // API của bạn

export const login = async (username, password) => {
  try {
    console.log(username, password);
    const response = await axios.post(`${API_URL}/login`, {
      Name: username,
      Password: password,
    });
    console.log(response.data);
    return response.data;
  } catch (error) {
    console.error("Lỗi đăng nhập:", error);
    return null;
  }
};


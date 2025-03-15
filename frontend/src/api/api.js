import axios from "axios";

const API_URL = "http://localhost:5299/api"; // Thay bằng API của bạn

export const login = async (name, pass) => {
    try {
        const response = await axios.post(`${API_URL}/Customer/login`, {
            Name: name,
            Pass: pass
        });
        return response.data;
    } catch (error) {
        console.error("Login error:", error);
        return null;
    }
};

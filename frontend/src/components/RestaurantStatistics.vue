<template>
<div class="dashboard">
  <div class="filter-section">
    <div class="date-picker">
      <label>Ngày bắt đầu:</label>
      <input type="date" v-model="startDate" />
    </div>
    <div class="date-picker">
      <label>Ngày kết thúc:</label>
      <input type="date" v-model="endDate" />
    </div>
    <div class="sort-options">
      <select @change="handleSort" v-model="selectedSort">
        <option value="hour">Giờ</option>
        <option value="day">Ngày</option>
        <option value="month">Tháng</option>
        <option value="year">Năm</option>
      </select>
      <button @click="handleSort">Thống kê</button>
    </div>
  </div>

  <div v-if="chartData.labels.length > 0" class="chart-container">
    <Line
      :key="chartData.labels.join(',')"
      :data="chartData"
      :options="chartOptions"
      style="height: 400px; width: 100%"
    />
  </div>
  <div v-else class="loading">Đang tải dữ liệu...</div>

  <table class="shopee-table">
    <thead>
      <tr>
        <th>Thời gian</th>
        <th>Doanh thu (VNĐ)</th>
      </tr>
    </thead>
    <tbody>
      <tr v-for="(label, index) in chartData.labels" :key="index">
        <td>{{ label }}</td>
        <td>{{ chartData.datasets[0].data[index].toLocaleString() }}</td>
      </tr>
    </tbody>
  </table>
</div>

</template>

<script setup>
import { ref, onMounted } from "vue";
import { Line } from "vue-chartjs";
import {
  Chart as ChartJS,
  Title,
  Tooltip,
  Legend,
  LineElement,
  PointElement,
  LinearScale,
  CategoryScale,
} from "chart.js";
import { GetRevenueData } from "../api/Revenue.js";

ChartJS.register(
  Title,
  Tooltip,
  Legend,
  LineElement,
  PointElement,
  LinearScale,
  CategoryScale
);

const chartData = ref({
  labels: [],
  datasets: [
    {
      label: "Doanh thu",
      data: [],
      borderColor: "rgba(75,192,192,1)",
      fill: false,
    },
  ],
});

const chartOptions = {
  responsive: true,
  plugins: {
    legend: {
      position: "top",
      labels: {
        font: {
          size: 18,
          weight: "bold",
        },
        color: "#000",
        padding: 20,
      },
    },
    title: {
      display: true,
      text: "Thống kê doanh thu",
      font: {
        size: 24,
        weight: "bold",
        family: "'Helvetica Neue', sans-serif",
      },
      color: "#333",
      padding: {
        top: 20,
      },
      align: "center",
    },
  },
};

const selectedSort = ref("day");
const startDate = ref("");
const endDate = ref("");
const resData = ref([]);

const filterByDateRange = (data) => {
  if (!startDate.value || !endDate.value) return data;

  const start = new Date(startDate.value);
  const end = new Date(endDate.value);
  end.setHours(23, 59, 59, 999); // bao gồm hết ngày

  if (start.toDateString() === end.toDateString()) {
    return data.filter((item) => {
      const itemDate = new Date(item.datetime);
      return itemDate.toDateString() === start.toDateString();
    });
  }

  return data.filter((item) => {
    const itemDate = new Date(item.datetime);
    return itemDate >= start && itemDate <= end;
  });
};

const sortByHour = () => {
  const grouped = {};
  const filteredData = filterByDateRange(resData.value);

  filteredData.forEach((item) => {
    const date = new Date(item.datetime);
    const hourStr = date.getHours().toString().padStart(2, "0") + ":00";
    if (!grouped[hourStr]) grouped[hourStr] = 0;
    grouped[hourStr] += item.revenue;
  });

  const sortedHours = Object.keys(grouped).sort((a, b) => {
    return parseInt(a) - parseInt(b);
  });

  chartData.value.labels = sortedHours;
  chartData.value.datasets[0].data = sortedHours.map((h) => grouped[h]);
};

const sortByDay = () => {
  const grouped = {};
  const filteredData = filterByDateRange(resData.value);

  filteredData.forEach((item) => {
    const dateStr = new Date(item.datetime).toLocaleDateString("en-GB");
    if (!grouped[dateStr]) grouped[dateStr] = 0;
    grouped[dateStr] += item.revenue;
  });

  const sortedDates = Object.keys(grouped).sort((a, b) => {
    const [da, ma, ya] = a.split("/").map(Number);
    const [db, mb, yb] = b.split("/").map(Number);
    return new Date(ya, ma - 1, da) - new Date(yb, mb - 1, db);
  });

  chartData.value.labels = sortedDates;
  chartData.value.datasets[0].data = sortedDates.map((d) => grouped[d]);
};

const sortByMonth = () => {
  const grouped = {};
  const filteredData = filterByDateRange(resData.value);

  filteredData.forEach((item) => {
    const date = new Date(item.datetime);
    const monthYear = `${(date.getMonth() + 1)
      .toString()
      .padStart(2, "0")}/${date.getFullYear()}`;

    if (!grouped[monthYear]) grouped[monthYear] = 0;
    grouped[monthYear] += item.revenue;
  });

  const sortedMonths = Object.keys(grouped).sort((a, b) => {
    const [ma, ya] = a.split("/").map(Number);
    const [mb, yb] = b.split("/").map(Number);
    return new Date(ya, ma - 1) - new Date(yb, mb - 1);
  });

  chartData.value.labels = sortedMonths;
  chartData.value.datasets[0].data = sortedMonths.map((m) => grouped[m]);
};

const sortByYear = () => {
  const grouped = {};
  const filteredData = filterByDateRange(resData.value);

  filteredData.forEach((item) => {
    const date = new Date(item.datetime);
    const year = date.getFullYear().toString();

    if (!grouped[year]) grouped[year] = 0;
    grouped[year] += item.revenue;
  });

  const sortedYears = Object.keys(grouped).sort((a, b) => Number(a) - Number(b));

  chartData.value.labels = sortedYears;
  chartData.value.datasets[0].data = sortedYears.map((y) => grouped[y]);
};

const handleSort = () => {
  if (selectedSort.value === "hour") sortByHour();
  else if (selectedSort.value === "day") sortByDay();
  else if (selectedSort.value === "month") sortByMonth();
  else if (selectedSort.value === "year") sortByYear();
};

onMounted(async () => {
  const IDCus = localStorage.getItem("IDRes");
  resData.value = await GetRevenueData(IDCus);
  sortByDay();
});
</script>

  
<style scoped>
.shopee-table {
  margin-top:10%;
  width: 100%;
  border-collapse: separate;
  border-spacing: 0;
  font-family: 'Helvetica Neue', sans-serif;
  background-color: #fff;
  border: 1px solid #f0f0f0;
  border-radius: 8px;
  box-shadow: 0 2px 8px rgba(0, 0, 0, 0.05);
  overflow: hidden;
}

.shopee-table thead {
  background: #1e1e2f;
  color: #0fc306;
  font-weight: 600;
}

.shopee-table th,
.shopee-table td {
  padding: 12px 16px;
  text-align: left;
  border-bottom: 1px solid #f0f0f0;
}

.shopee-table tbody tr:hover {
  background: #1e1e2f;
  color: #0fc306;
  transition: background-color 0.3s;
}

.shopee-table th:first-child,
.shopee-table td:first-child {
  border-left: none;
}

.shopee-table th:last-child,
.shopee-table td:last-child {
  border-right: none;
}
.dashboard {
  padding: 20px;
  font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
  background: #f9f9f9;
  border-radius: 10px;
}

.filter-section {
  display: flex;
  flex-wrap: wrap;
  align-items: flex-end;
  gap: 16px;
  margin-bottom: 20px;
  background-color: #fff;
  padding: 16px;
  border-radius: 10px;
  box-shadow: 0 2px 8px rgba(0,0,0,0.05);
}

.date-picker label {
  display: block;
  font-weight: bold;
  margin-bottom: 6px;
}

.date-picker input {
  padding: 6px 12px;
  border: 1px solid #ccc;
  border-radius: 6px;
  font-size: 14px;
  width: 180px;
}

.sort-options {
  display: flex;
  gap: 10px;
  align-items: flex-end;
}

.sort-options select,
.sort-options button {
  padding: 8px 14px;
  font-size: 14px;
  border-radius: 6px;
  border: 1px solid #ccc;
  cursor: pointer;
  transition: 0.3s;
}

.sort-options button {
  background-color: #1e88e5;
  color: white;
  border: none;
}

.sort-options button:hover {
  background-color: #1669bb;
}

.sort-options select {
  background-color: #fff;
}

.chart-container {
  background-color: #fff;
  padding: 20px;
  border-radius: 10px;
  box-shadow: 0 2px 8px rgba(0,0,0,0.05);
  margin-bottom: 20px;
}

.loading {
  font-style: italic;
  color: #666;
  margin: 20px 0;
}

</style>

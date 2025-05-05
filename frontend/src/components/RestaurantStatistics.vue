d<template>
    <div v-if="chartData.labels.length > 0">
      <Line
        :key="chartData.labels.join(',')"
        :data="chartData"
        :options="chartOptions"
        style="height: 400px; width: 100%"
      />
    </div>
    <div v-else>Đang tải dữ liệu...</div>
  
    <select @change="handleSort" v-model="selectedSort">
      <option value="hour">Hour</option>
      <option value="day">Day</option>
      <option value="month">Month</option>
      <option value="year">Year</option>
    </select>
 
    <table class="shopee-table">
  <thead>
    <tr>
      <th>Datetime</th>
      <th>Revenue</th>
    </tr>
  </thead>
  <tbody>
    <tr v-for="(label, index) in chartData.labels" :key="index">
    <td>{{ label }}</td>
    <td>{{ chartData.datasets[0].data[index] }}</td>
  </tr>
  </tbody>
</table>




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
      weight: 'bold',
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
  const resData = ref([]); 
  

  const handleSort = () => {
    if (selectedSort.value === "hour") sortByHour();
    else if (selectedSort.value === "day") sortByDay();
    else if (selectedSort.value === "month") sortByMonth();
    else if (selectedSort.value === "year") sortByYear();
  };
 
  
  
  const sortByHour = () => {
    const grouped = {}; 
  
    resData.value.forEach((item) => {
      const date = new Date(item.datetime);
      const hourStr = date.getHours().toString().padStart(2, "0") + ":00";
  
      if (!grouped[hourStr]) grouped[hourStr] = 0;
      grouped[hourStr] += item.revenue;
    });
  
    const sortedHours = Object.keys(grouped).sort((a, b) => {
      return parseInt(a.split(":")[0]) - parseInt(b.split(":")[0]);
    });
  
    chartData.value.labels = sortedHours;
    chartData.value.datasets[0].data = sortedHours.map((hour) => grouped[hour]);
  };
  

  const sortByDay = () => {
    const grouped = {};
  
    resData.value.forEach((item) => {
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

  resData.value.forEach((item) => {
    const date = new Date(item.datetime);
    const monthYear = `${(date.getMonth() + 1).toString().padStart(2, "0")}/${date.getFullYear()}`; // MM/YYYY

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

  resData.value.forEach((item) => {
    const date = new Date(item.datetime);
    const year = date.getFullYear().toString(); 

    if (!grouped[year]) grouped[year] = 0;
    grouped[year] += item.revenue;
  });

  const sortedYears = Object.keys(grouped).sort((a, b) => Number(a) - Number(b));

  chartData.value.labels = sortedYears;
  chartData.value.datasets[0].data = sortedYears.map((y) => grouped[y]);
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
</style>

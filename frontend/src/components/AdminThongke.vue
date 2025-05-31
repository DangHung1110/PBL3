d<template>
     <div class="filter-container">
 <div class="filter-item">
 <label for="sort" style="margin-bottom:8%">Sắp xếp theo:</label>
<select id="sort" @change="handleSort" v-model="selectedSort">
<option value="hour">Hour</option>
<option value="day">Day</option>
 <option value="month">Month</option>
 <option value="year">Year</option>
 </select>
 </div>

<div class="filter-item">
<label for="start">Ngày bắt đầu:</label>
<input id="start" type="date" v-model="startDate" />
</div>

<div class="filter-item">
<label for="end">Ngày kết thúc:</label>
<input id="end" type="date" v-model="endDate" />
</div>

 <div class="filter-item">
 <button @click="applyDateFilter">Lọc</button>
 </div>
</div>


    <div v-if="chartData.labels.length > 0">
      <Line
        :key="chartData.labels.join(',')"
        :data="chartData"
        :options="chartOptions"
        style="height: 400px; width: 100%"
      />
    </div>
    <div v-else>Đang tải dữ liệu...</div>
  
   
 
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
import { GetRevenueAdmin } from "../api/Revenue.js";

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
const resData = ref([]);
const startDate = ref("");
const endDate = ref("");
const filteredData = ref([]);

const handleSort = () => {
  if (startDate.value && endDate.value) {
    applyDateFilter();
  } else {
    switch (selectedSort.value) {
      case "hour":
        sortByHour(resData.value);
        break;
      case "day":
        sortByDay(resData.value);
        break;
      case "month":
        sortByMonth(resData.value);
        break;
      case "year":
        sortByYear(resData.value);
        break;
    }
  }
};

const applyDateFilter = () => {
  if (!startDate.value || !endDate.value) {
    alert("Vui lòng chọn cả ngày bắt đầu và ngày kết thúc");
    return;
  }

  const start = new Date(startDate.value);
  const end = new Date(endDate.value);
  end.setHours(23, 59, 59, 999);

  filteredData.value = resData.value.filter((item) => {
    const date = new Date(item.orderconfirmedtime);
    if (isNaN(date.getTime())) {
      console.error("Invalid date format:", item.orderconfirmedtime);
      return false;
    }
    return date >= start && date <= end;
  });

  if (filteredData.value.length === 0) {
    chartData.value.labels = [];
    chartData.value.datasets[0].data = [];
    alert("Không có dữ liệu trong khoảng thời gian đã chọn");
    return;
  }

  if (startDate.value === endDate.value) {
    sortByHour(filteredData.value);
  } else {
    handleSortFiltered();
  }
};

const handleSortFiltered = () => {
  switch (selectedSort.value) {
    case "hour":
      sortByHour(filteredData.value);
      break;
    case "day":
      sortByDay(filteredData.value);
      break;
    case "month":
      sortByMonth(filteredData.value);
      break;
    case "year":
      sortByYear(filteredData.value);
      break;
  }
};

const sortByHour = (data) => {
  const grouped = {};

  data.forEach((item) => {
    const date = new Date(item.orderconfirmedtime);
    const hourStr = date.getHours().toString().padStart(2, "0") + ":00";

    if (!grouped[hourStr]) grouped[hourStr] = 0;
    grouped[hourStr] += item.cost;
  });

  const sortedHours = Object.keys(grouped).sort((a, b) => {
    return parseInt(a.split(":")[0]) - parseInt(b.split(":")[0]);
  });

  chartData.value.labels = sortedHours;
  chartData.value.datasets[0].data = sortedHours.map((hour) => grouped[hour]);
};

const sortByDay = (data) => {
  const grouped = {};

  data.forEach((item) => {
    const dateStr = new Date(item.orderconfirmedtime).toLocaleDateString("en-GB");
    if (!grouped[dateStr]) grouped[dateStr] = 0;
    grouped[dateStr] += item.cost;
  });

  const sortedDates = Object.keys(grouped).sort((a, b) => {
    const [da, ma, ya] = a.split("/").map(Number);
    const [db, mb, yb] = b.split("/").map(Number);
    return new Date(ya, ma - 1, da) - new Date(yb, mb - 1, db);
  });

  chartData.value.labels = sortedDates;
  chartData.value.datasets[0].data = sortedDates.map((d) => grouped[d]);
};

const sortByMonth = (data) => {
  const grouped = {};

  data.forEach((item) => {
    const date = new Date(item.orderconfirmedtime);
    const monthYear = `${(date.getMonth() + 1).toString().padStart(2, "0")}/${date.getFullYear()}`;

    if (!grouped[monthYear]) grouped[monthYear] = 0;
    grouped[monthYear] += item.cost;
  });

  const sortedMonths = Object.keys(grouped).sort((a, b) => {
    const [ma, ya] = a.split("/").map(Number);
    const [mb, yb] = b.split("/").map(Number);
    return new Date(ya, ma - 1) - new Date(yb, mb - 1);
  });

  chartData.value.labels = sortedMonths;
  chartData.value.datasets[0].data = sortedMonths.map((m) => grouped[m]);
};

const sortByYear = (data) => {
  const grouped = {};

  data.forEach((item) => {
    const date = new Date(item.orderconfirmedtime);
    const year = date.getFullYear().toString();

    if (!grouped[year]) grouped[year] = 0;
    grouped[year] += item.cost;
  });

  const sortedYears = Object.keys(grouped).sort((a, b) => Number(a) - Number(b));

  chartData.value.labels = sortedYears;
  chartData.value.datasets[0].data = sortedYears.map((y) => grouped[y]);
};

onMounted(async () => {
  resData.value = await GetRevenueAdmin();
  sortByDay(resData.value);
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
.filter-container {
  display: flex;
  flex-wrap: wrap;
  gap: 16px;
  margin: 20px 0;
  align-items: center;
  justify-content: flex-start;
  background-color: #f9f9f9;
  padding: 16px;
  border-radius: 8px;
  box-shadow: 0 2px 6px rgba(0, 0, 0, 0.05);
}

.filter-item {
  display: flex;
  flex-direction: column;
  min-width: 150px;
}

.filter-item label {
  margin-bottom: 6px;
  font-weight: 600;
  font-size: 14px;
  color: #333;
}

.filter-item select,
.filter-item input[type="date"] {
  padding: 8px 12px;
  border: 1px solid #ccc;
  border-radius: 6px;
  font-size: 14px;
  font-family: 'Helvetica Neue', sans-serif;
}

.filter-item button {
  margin-top: 22px;
  padding: 10px 20px;
  background-color: #1e1e2f;
  color: #0fc306;
  border: none;
  border-radius: 6px;
  cursor: pointer;
  font-weight: bold;
  transition: background-color 0.3s;
}

.filter-item button:hover {
  background-color: #0fc306;
  color: #1e1e2f;
}


</style>

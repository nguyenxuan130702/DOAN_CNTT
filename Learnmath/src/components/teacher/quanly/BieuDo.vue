<template>
    <div class="bieudo-ngaynghi" id="bieudo-ngaynghi">
        <div class="bieudo_ngaynghi-title">
            <h1>Tiến độ làm bài của tài khoản {{ user.userAccout }}</h1>
            <div class="icon_close" v-on:click="closeBieuDo()"></div>
        </div>
        <canvas ref="chart" :style="{ width: chartWidth + 'px', height: chartHeight + 'px' }"></canvas>
    </div>
</template>
<script>
import Chart from 'chart.js/auto'
export default{
    props: ['propBieuDo'], 
    data(){
        return {
            user: {}, 
            chartWidth: 300, // Đặt chiều rộng mặc định của biểu đồ
            chartHeight: 300, 
            dataBieuDo: [], 
        }
    }, 
    watch: {
        propBieuDo: function() {
            this.renderChart(this.propBieuDo);
        }
    }, 
    mounted(){
        this.renderChart(this.propBieuDo);
    }, 
    methods: {
        // renderChart() {
        //     const ctx = this.$refs.chart.getContext('2d');
        //     new Chart(ctx, {
        //         type: 'doughnut',
        //         data: this.chartData, 
        //         options: {
        //             responsive: false // Tắt tính năng phản ứng tự động của biểu đồ
        //         }
        //     });
        // }

        renderChart(data) {
            const ctx = this.$refs.chart.getContext('2d');
            new Chart(ctx, {
                type: 'doughnut',
                data: {
                labels: ['Hoàn thành', 'Nộp muộn', 'Chưa hoàn thành'],
                datasets: [{
                    label: 'Sample Data',
                    data: data,
                    backgroundColor: [
                    'blue',
                    'green',
                    'red'
                    ]
                }]
                },
                options: {
                responsive: false // Tắt tính năng phản ứng tự động của biểu đồ
                }
            }); 
        }, 
        closeBieuDo: function(){
            document.getElementById("bieudo-ngaynghi").style.display = "none"; 
            document.getElementById("course_back").style.display = "none"; 
        }
    }
}
</script>
<style scoped>
.bieudo-ngaynghi {
    position: absolute; 
    top: 120px; 
    left: 35%; 
    width: 550px; 
    background-color: #fff; 
    z-index: 1; 
    display: none; 
}
.bieudo-ngaynghi > canvas {
    margin: auto; 
    margin-top: 24px;
    margin-bottom: 24px; 
}
.bieudo_ngaynghi-title {
    display: flex; 
    justify-content: space-between;
    align-items: center;
    padding-top: 24px; 
    padding-left: 24px; 
    padding-right: 24px; 
}
</style>

<template>
    <Navbar></Navbar>
    <Sidebar></Sidebar>
    <ChiTietKhoaHoc :propGetCourse="getCourse"></ChiTietKhoaHoc>
    <LichHoc :propGetCourse="getCourse"></LichHoc>
    <UpdateCourse :propGetCourse="getCourse"></UpdateCourse>
    <ThemMoi></ThemMoi>
    <!-- <Chart></Chart> -->
    <div class="course-teacher" id="course-teacher">
        <h1>Quản lý lớp học</h1>
        <div class="a-course_table">
            <div class="a-course_table-title">
                <div class="a-course_table-title-search">
                    <input type="text" placeholder="Tìm kiếm theo mã, chủ đề khóa học" id="search-course">
                    <div class="search-course" v-on:click="searchCourse()">
                        <div class="icon_search"></div>
                    </div>
                </div>
                <div class="a-course_table-title-btn">
                    <button v-on:click="openFormPostCourse()">Thêm mới</button>
                    <div class="icon_loaddata_sty">
                        <div class="icon_loaddata" v-on:click="loadDataCourse()"></div>
                    </div>
                </div>
            </div>
            <div class="a-course_table-content">
                <table>
                    <thead>
                        <tr>
                            <td style="width: 100px;">Mã lớp học</td>
                            <td style="width: 300px">Tên lớp học</td>
                            <td style="width: 200px">Thời gian bắt đầu</td>
                            <td style="width: 100px">Xem chi tiết</td>
                            <td style="width: 100px;">Sửa</td>
                            <td style="width: 100px">Học sinh</td>
                            <td style="width: 150px">Lịch học</td>
                            <td >Xóa lớp học</td>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-for="(item, index) in teacher_course" :key="index">
                            <td style="width: 100px;">{{ item.teacher_CourseCode }}</td>
                            <td style="width: 300px">{{ item.teacher_CourseName }}</td>
                            <td style="width: 200px">{{ renderDate(item.startTime)}}</td>
                            <td style="width: 100px; text-align: center" class="xemchitiet" v-on:click="openChiTiet(item)">
                                <button>Xem</button>
                            </td>
                            <td style="width: 100px; text-align: center" class="xemchitiet" v-on:click="openSua(item)">
                                <button>Xem</button>
                            </td>
                            <td style="width: 100px; text-align: center" class="hocsinh" v-on:click="openHocSinh(item)">
                                <button>Chi tiết</button>
                            </td>
                            <td style="width: 150px;" class="lichhoc" v-on:click="openLichHoc(item)">
                                <button>Xem chi tiết</button>
                            </td>
                            <td style="text-align: center" class="xoalophoc">
                                <button>Xóa</button>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <div class="a-course_table-page">
                <div style="margin-left: 24px;">
                    Tổng: 10
                </div>
                <div style="margin-right: 24px" class="a-course_table-page1">
                    <div>Số bản ghi/trang: 
                        <select name="" id="">
                            <option value="5">5</option>
                            <option value="10">10</option>
                            <option value="15">15</option>
                        </select>
                    </div>
                    <div class="icon_back" v-on:click="backPageCourse()"></div>
                    <div class="icon_next" v-on:click="nextPageCourse()"></div>
                </div>
            </div>
        </div>
    </div>

    <!--Danh sách học sinh-->
    <div id="hocsinh-teacher">
        <div class="hocsinh-teacher-title">
            <h1>Danh sách tài khoản đăng ký</h1>
            <div class="lichhoa_back" v-on:click="backQuanLyHocSinh()">
                <div class="icon_backup"></div>
                <div>Quay lại</div>
            </div>
        </div>
        <div class="a-course_table">
            <div class="a-course_table-title">
                <div class="a-course_table-title-search">
                    <input type="text" placeholder="Tìm kiếm theo mã, chủ đề khóa học" id="search-course">
                    <div class="search-course" v-on:click="searchCourse()">
                        <div class="icon_search"></div>
                    </div>
                </div>
                <div class="a-course_table-title-btn">
                    <div class="icon_loaddata_sty">
                        <div class="icon_loaddata" v-on:click="loadDataCourse()"></div>
                    </div>
                </div>
            </div>
            <div class="a-course_table-content">
                <table>
                    <thead>
                        <tr>
                            <td style="width: 200px;">Tên học sinh</td>
                            <td style="width: 300px">Email</td>
                            <td style="width: 200px">Tên tài khoản</td>
                            <td style="width: 150px">Ngày đăng ký</td>
                            <td style="width: 150px">Tình trạng</td>
                            <td >Xóa học viên</td>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-for="(item, index) in hocsinh" :key="index">
                            <td style="width: 200px;">{{ item.userName }}</td>
                            <td style="width: 300px">{{ item.userEmail }}</td>
                            <td style="width: 200px">{{ item.userAccout }}</td>
                            <td style="width: 150px; text-align: center" class="xemchitiet">
                                {{ renderDate(item.date)}}
                            </td>
                            <td style="width: 150px; text-align: center" class="hocsinh" v-on:click="tinhTrang(item)">
                                <button>Chi tiết</button>
                            </td>
                            <td style="text-align: center" class="xoalophoc">
                                <button>Xóa</button>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <div class="a-course_table-page">
                <div style="margin-left: 24px;">
                    Tổng: 4
                </div>
                <div style="margin-right: 24px" class="a-course_table-page1">
                    <div>Số bản ghi/trang: 
                        <select name="" id="" v-model="pageSize">
                            <option value="5">5</option>
                            <option value="10">10</option>
                            <option value="15">15</option>
                        </select>
                    </div>
                    <div class="icon_back" v-on:click="backPageCourse()"></div>
                    <div class="icon_next" v-on:click="nextPageCourse()"></div>
                </div>
            </div>
        </div>
    </div>

    <div class="course_back" id="course_back"></div>

    <div id="bieudo-ngaynghi-course">
        <div class="bieudo-btvn">
            <h3>Tình trạng làm bài</h3>
            <div class="icon_close" v-on:click="closeChamDiem()"></div>
        </div>
        <div class="bieudo-ngaynghi">
            <canvas ref="chart" :style="{ width: chartWidth + 'px', height: chartHeight + 'px' }"></canvas>
        </div>
    </div>
    <!--Form sửa thông tin -->
</template>
<script>
import Navbar from '@/components/teacher/layout/NavbarTeacher.vue'
import Sidebar from '@/components/teacher/layout/SidebarTeacher.vue'
import BaseRequest from '@/core/BaseRequest'
import ChiTietKhoaHoc from '@/components/teacher/quanly/khoahoc/ChiTietKhoaHoc.vue'
import LichHoc from '@/components/teacher/quanly/khoahoc/LichHoc.vue'
import UpdateCourse from '@/components/teacher/quanly/khoahoc/UpdateCourse.vue'
import ThemMoi from '@/components/teacher/quanly/khoahoc/ThemCourse.vue'
import Chart from 'chart.js/auto'
export default {
    components: {
        Navbar, 
        Sidebar, 
        ChiTietKhoaHoc, 
        LichHoc, 
        UpdateCourse, 
        ThemMoi
    }, 
    data(){
        return {
            teacher_course: [], 
            getCourse: {}, 
            hocsinh: [], 
            hocsinh_class: [], 
            hocsinh_submit: [], 
            bieudo: [0, 0, 0], 
            user: {}, 

            //biểu đồ 
            data: [100, 300, 100],
            chart: null, 
            chartWidth: 300, // Đặt chiều rộng mặc định của biểu đồ
            chartHeight: 300,
        }
    }, 
    mounted(){
        BaseRequest.get("teacher_course")
        .then(response => {
            this.teacher_course = response.data; 
        })
        .catch(error => {
            console.log(error.message); 
        })
        this.renderChart(this.data); 
    }, 
    methods: { 
        closeChamDiem: function(){
            document.getElementById("bieudo-ngaynghi-course").style.display = "none"; 
            document.getElementById("course_back").style.display = "none";
        }, 
        tinhTrang: function(item){
            this.user = item; 
            BaseRequest.get("submit/submit_user?userId=" + item.userId)
            .then(response => {
                this.hocsinh_submit = response.data; 
                if(this.hocsinh_submit.length === 0){
                    this.data[0] = 0; 
                    this.data[1] = 0; 
                    this.data[2] = 100; 
                    console.log("bugg" + this.bieudo) 
                    this.renderChart(this.data); 
                    document.getElementById("bieudo-ngaynghi-course").style.display = "block"; 
                    document.getElementById("course_back").style.display = "block";
                }
                else {
                    let dung = 0; 
                    let muon = 0; 
                    for (const item1 of this.hocsinh_class) {
                        for (const item2 of this.hocsinh_submit) {
                            if(item1.assignmentId === item2.assignmentId && item2.date <= item1.deadline ){
                                dung++; 
                                break; 
                            }
                            else if(item1.assignmentId === item2.assignmentId && item2.date > item1.deadline){
                                muon++; 
                                break; 
                            }
                        }
                    }

                    this.data[0] = (dung/this.hocsinh_class.length)*100; 
                    this.data[1] = (muon/this.hocsinh_class.length)*100; 
                    this.data[2] = 100 - this.data[0] - this.data[1]; 
                    console.log('bugg' + this.data); 
                    this.renderChart(this.data); 
                    document.getElementById("bieudo-ngaynghi-course").style.display = "block"; 
                    document.getElementById("course_back").style.display = "block"; 

                }
            })


        }, 
        loadDataCourse: function(){
            BaseRequest.get("teacher_course")
            .then(response => {
                this.teacher_course = response.data; 
            })
            .catch(error => {
                console.log(error.message); 
            })
        }, 
        openFormPostCourse: function(){
            document.getElementById("post_course").style.display = "block"; 
            document.getElementById("course_back").style.display = "block"; 
        }, 
        openLichHoc: function(item){
            this.getCourse = item; 
            document.getElementById("course-teacher").style.display = "none"; 
            document.getElementById("hocsinh-teacher").style.display = "none"; 
            document.getElementById("lichhoc_course").style.display = "block"; 
        }, 
        backQuanLyHocSinh: function(){
            document.getElementById("course-teacher").style.display = "block"; 
            document.getElementById("hocsinh-teacher").style.display = "none"; 
            document.getElementById("lichhoc_course").style.display = "none"; 
        }, 
        openChiTiet: function(item){
            this.getCourse = item; 
            document.getElementById("chitiet_course").style.display = "block"; 
            document.getElementById("course_back").style.display = "block"; 
        }, 
        openHocSinh: function(item){
            document.getElementById("course-teacher").style.display = "none"; 
            document.getElementById("hocsinh-teacher").style.display = "block"; 
            document.getElementById("lichhoc_course").style.display = "none"; 

            BaseRequest.get("enrollment/user_enrollment_teacher_course?teacher_CourseId=" + item.teacher_CourseId)
            .then(response => {
                console.log(response.data); 
                this.hocsinh = response.data; 
            })
            .catch(error => {
                console.log(error.message); 
            })

            BaseRequest.get("view_class_assignment/teacher_course?teacher_CourseId=" + item.teacher_CourseId)
            .then(response => {
                this.hocsinh_class = response.data; 
            })
            .catch(error => {
                console.log(error.message); 
            })

        }, 
        renderDate: function(item){
            let dateObject = new Date(item);

            let formattedDate = `${dateObject.getDate()}/${dateObject.getMonth() + 1}/${dateObject.getFullYear()}`;

            return formattedDate; 
        }, 
        renderHours: function(item){
            let dateObject = new Date(item); 
            let gio = dateObject.getHours(); 
            let phut = dateObject.getMinutes(); 
            let giay = dateObject.getSeconds(); 
            return gio + " giờ " + phut + " phút " + giay; 
        }, 
        openSua: function(item){
            this.getCourse = item; 
            document.getElementById("update_course").style.display = "block"; 
            document.getElementById("course_back").style.display = "block"; 
        }, 

        //bieur đo 
        renderChart(data) {
            const ctx = this.$refs.chart.getContext('2d');
            if (this.chart) {
                this.chart.destroy();
            }
            this.chart = new Chart(ctx, {
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
        beforeUnmount() {
            if (this.chart) {
                this.chart.destroy(); // Phá hủy biểu đồ khi component bị tháo gỡ
            }
        }
    }
}
</script>
<style scoped>
.bieudo-btvn {
    display: flex; 
    width: 400px; 
    justify-content: space-between;
    padding: 24px; 
}
.bieudo-ngaynghi {
    margin-left: 50px; 
    margin-bottom: 24px; 
}
#bieudo-ngaynghi-course {
    display: none; 
    position: absolute; 
    top: 100px; 
    left: 38%; 
    background-color: #fff; 
}
.icon_backup {
    background: url('@/assets/math/back-square-svgrepo-com.svg') no-repeat -5px -5px;
	width: 54px;
	height: 54px;
    transform: scale(0.6);
}
.hocsinh-teacher-title {
    display: flex; 
    width: 100%; 
    justify-content: space-between;
    padding-top: 24px; 
    padding-left: 24px; 
    padding-right: 24px; 
}
#hocsinh-teacher > h1 {
    margin-top: 24px; 
    margin-left: 24px; 
}
.lichhoa_back {
    display: flex; 
    align-items: center;
    margin-left: 16px; 
    margin-bottom: -16px; 
}
.lichhoa_back:hover {
    text-decoration: underline;
    cursor: pointer;
}
.course-teacher {
    width: calc(100% - 250px); 
    float: left;
    background-color: #f5f6f7; 
    height: calc(100vh - 56px);
    display: block; 
}
.course-teacher > h1 {
    padding-top: 24px; 
    padding-left: 24px; 
    font-weight: 700;
}
.xemchitiet > button {
    background-color: orange; 
    padding: 8px; 
    border: none; 
    color: #fff; 
    border-radius: 6px;
}
.hocsinh > button {
    padding: 8px; 
    background-color: blue; 
    border: none; 
    color: #fff; 
    border-radius: 6px;
}
.xoalophoc > button {
    padding: 8px; 
    background-color: red; 
    border: none; 
    color: #fff; 
    border-radius: 6px;
}
#hocsinh-teacher {
    width: calc(100% - 250px); 
    float: left;
    background-color: #f5f6f7; 
    height: calc(100vh - 56px);
    display: none; 
}
.lichhoc > button {
    background-color: blue; 
    padding: 8px; 
    border: none; 
    color: #fff; 
    border-radius: 6px;
} 
</style>

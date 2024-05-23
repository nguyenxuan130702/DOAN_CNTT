<template>
    <NavBar></NavBar>
    <Sidebar></Sidebar>
    <div class="course-teacher" id="course-danhgia-teacher">
        <h1>Đánh giá học sinh</h1>
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
                            <td style="width: 100px;">Mã lớp học</td>
                            <td style="width: 500px">Tên lớp học</td>
                            <td style="width: 200px">Thời gian bắt đầu</td>
                            <td >Lịch học</td>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-for="(item, index) in course" :key="index">
                            <td style="width: 100px;">{{ item.teacher_CourseCode }}</td>
                            <td style="width: 500px">{{ item.teacher_CourseName }}</td>
                            <td style="width: 200px">{{ renderDate(item.startTime)}}</td>
                            <td style="text-align: center" class="xemchitiet" v-on:click="openClassFile(item)">
                                <button>Xem</button>
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


    <div class="course-teacher" id="class-danhgia-teacher">
        <div class="hocsinh-teacher-title">
            <h1>Lịch học</h1>
            <div class="lichhoa_back" v-on:click="backFile1()">
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
                            <td style="width: 100px;">Mã buổi học</td>
                            <td style="width: 500px">Nội dung mô tả</td>
                            <td style="width: 200px">Thời gian học</td>
                            <td >Quản lý điểm</td>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-for="(item, index) in classList" :key="index">
                            <td style="width: 100px;">{{ item.classCode }}</td>
                            <td style="width: 500px">{{ item.className }}</td>
                            <td style="width: 200px; text-align: center" class="hocsinh">
                                {{ item.time }} {{ renderDate(item.date)}}
                            </td>
                            <td style="text-align: center" class="xemchitiet" v-on:click="openHocSinh(item)">
                                <button>Xem</button>
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



    <div class="course-teacher" id="hocsinh-danhgia-teacher">
        <div class="hocsinh-teacher-title">
            <h1>Danh sách học sinh</h1>
            <div class="lichhoa_back" v-on:click="backFile2()">
                <div class="icon_backup"></div>
                <div>Quay lại</div>
            </div>
        </div>
        <div class="a-course_table">
            <div class="a-course_table-title">
                <div class="a-course_table-title-search">
                    <select name="" id="" class="file_submit" v-model="assignmentid">
                        <option :value="item.assignmentId" v-for="(item, index) in list1" :key="index">{{item.assignmentName}}</option>
                    </select>
                    <div class="search-course" v-on:click="searchHocSinhBTVN()">
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
                            <td style="width: 200px;">Tên học viên</td>
                            <td style="width: 200px">Email</td>
                            <td style="width: 200px">Tên tài khoản</td>
                            <td style="width: 200px">File bài làm mới nhất</td>
                            <td style="width: 200px">Quản lý điểm</td>
                            <td>Trạng thái chấm điểm</td>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-for="(item, index) in hocsinh" :key="index">
                            <td style="width: 200px;">{{ item.userName }}</td>
                            <td style="width: 200px">{{ item.userEmail }}</td>
                            <td style="width: 200px">{{ item.userAccout }}</td>
                            <td style="width: 200px">
                                <a :href="submit[index].submitLink" v-if="check[index]" target="_blank">File bài làm</a>
                                <p v-else>Chưa làm</p>
                            </td>
                            <td style="width: 200px; text-align: center" class="xemchitiet">
                                <button v-on:click="chamDiem(item, submit[index])">Xem</button>
                            </td>
                            <td style="text-align: center" class="xemchitiet">
                                <button v-if="ketqua_diem[index]" style="background-color: blue;">{{ ketqua_diem[index].score }}</button>
                                <button v-else style="background-color: red;">Chưa chấm</button>
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

    <!--Chấm điểm-->
    <div class="form-chamdiem" id="form-chamdiem">
        <div class="chamdiem-teacher">
            <div>
                <div class="bieudo-btvn">
                    <h3>Điểm bài làm</h3>
                    <div class="icon_close" v-on:click="closeChamDiem()"></div>
                </div>
                <div>
                    <label for="">
                        <b>Điểm: </b> <br>
                        <input type="text" id="diem" v-model="diem">
                    </label> 
                    <!-- <label for="">
                        <b>Tất cả các file đã nộp: </b> <br> 
                        file 1, file 2, file 3
                    </label> -->
                </div>
                <br>
                <label for="">
                    <b>Nhận xét: </b> <br>
                    <textarea name="" id="feedback" cols="30" rows="10" v-model="feedback"></textarea>
                </label>
            </div>
            <!-- <div>
                <div class="bieudo-btvn">
                    <h3>Tình trạng làm bài</h3>
                    <div class="icon_close" v-on:click="closeChamDiem()"></div>
                </div>
                <div class="bieudo-ngaynghi" id="bieudo-ngaynghi">
                    <canvas ref="chart" :style="{ width: chartWidth + 'px', height: chartHeight + 'px' }"></canvas>
                </div>
            </div> -->
        </div>
        <div>
            <button v-on:click="capNhatDiem()">Cập nhật</button>
        </div>
    </div>
    <div class="course_back" id="diem_back"></div>
</template>
<script>
import NavBar from '@/components/teacher/layout/NavbarTeacher.vue'
import Sidebar from '@/components/teacher/layout/SidebarTeacher.vue'
import BaseRequest from '@/core/BaseRequest'
//import Chart from 'chart.js/auto'
export default {
    components: {
        NavBar, 
        Sidebar, 
    }, 
    data(){
        return {
            course: [], 
            classList: [], 
            hocsinh: [], 
            getCourse: {}, 
            list1: [], 
            assignmentid: '',
            nopbai: [],  
            submit: [], 
            check: [], 
            ketqua_diem: [], 

            //cham điểm
            diem: 0, 
            feedback: "", 
            capnhat: 0, 
            k: {}, 
            result: {},
            assgnId: '',  
            user: {}, 

            //biểu đồ 
            chartWidth: 300, // Đặt chiều rộng mặc định của biểu đồ
            chartHeight: 300,
            data: [100, 300, 100],
            hocsinh_submit: [], 
            hocsinh_class: [], 
            chart: null
        }
    }, 
    mounted(){
        this.teacher = JSON.parse(localStorage.getItem("teacherLogin"))
        BaseRequest.get("teacher_course/teacher?teacherId=" + this.teacher.teacherId)
        .then(response => {
            this.course = response.data; 
            console.log(response.data); 
        })
        .catch(error => {
            console.log(error.message); 
        })

    }, 
    methods: {
        searchHocSinhBTVN: function(){
            if (this.hocsinh.length === 0) {
                alert("Không có học sinh đăng ký khóa học.");
            } 
            else {
                // Tạo một mảng các promise
                const promises = this.hocsinh.map(student => {
                    return BaseRequest.get("submit/submit_user_assignment?userId=" + student.userId + "&assignmentId=" + this.assignmentid)
                        .then(response => {
                            return response.data[0];
                        });
                });

                const propmises1 = this.hocsinh.map(student1 => {
                    return BaseRequest.get("result/user_assignment?userId=" + student1.userId + "&assignmentId=" + this.assignmentid)
                    .then(response => {
                        return response.data; 
                    }); 
                }); 

                // Đợi tất cả các yêu cầu hoàn thành trước khi xử lý dữ liệu
                Promise.all(promises)
                    .then(submits => {
                        this.submit = submits;

                        // Kiểm tra xem mỗi học sinh đã nộp bài hay chưa
                        this.check = this.submit.map(submit => {
                            return submit !== null && submit !== undefined && submit !== "";
                        });

                        console.log(this.submit);
                        console.log(this.check);
                    })
                    .catch(error => {
                        console.log(error.message);
                    });

                ///
                Promise.all(propmises1)
                    .then(results => {
                        this.ketqua_diem = results;

                        // Kiểm tra xem mỗi học sinh đã nộp bài hay chưa
                    })
                    .catch(error => {
                        console.log(error.message);
                    });
            }
        }, 
        // async tinhTrang(item) {
        //     const response = await BaseRequest.get("submit/submit_user?userId=" + item.userId); 

        //     this.hocsinh_submit = response.data; 

        //     console.log("Ít: "); 
        //     console.log(this.hocsinh_submit); 

        //         // if (!Array.isArray(this.hocsinh_class)) {
        //         //     console.error('this.hocsinh_class is not an array');
        //         //     return;
        //         // }

        //         if(this.hocsinh_submit.length === 0){
        //             this.data[0] = 0; 
        //             this.data[1] = 0; 
        //             this.data[2] = 100; 
        //         }
        //         else {
        //             let dung = 0; 
        //             let muon = 0; 
        //             let khong = 0; 
        //             for (const item1 of this.hocsinh_class) {
        //                 let tg = 0; 
        //                 for (const item2 of this.hocsinh_submit) {
        //                     if(item1.assignmentId === item2.assignmentId && this.renderDate(item2.date) <= this.renderDate(item1.deadline)){
        //                         dung++; 
        //                         break; 
        //                     }
        //                     else if(item1.assignmentId === item2.assignmentId && this.renderDate(item2.date) > this.renderDate(item1.deadline)){
        //                         muon++; 
        //                         break; 
        //                     }
        //                     else {
        //                         tg++; 
        //                     }
        //                 }
        //                 if(tg === this.hocsinh_submit){
        //                     khong++; 
        //                 }
        //             }
        //             console.log("d" + dung); 
        //             console.log("d" + muon); 
        //             console.log('k' + khong); 


        //             this.data[0] = (dung/this.hocsinh_class.length)*100; 
        //             console.log(this.data[0]); 
        //             this.data[1] = (muon/this.hocsinh_class.length)*100; 
        //             console.log(this.data[1]); 
        //             this.data[2] = 100 - this.data[0] - this.data[1]; 

        //         }
        // }, 
        async chamDiem(item1, item2) {
            document.getElementById("form-chamdiem").style.display = "block"; 
            document.getElementById("diem_back").style.display = "block"; 
            //await this.tinhTrang(item1); 
            //this.renderChart(this.data);
            this.user = item1; 

            if(item2 === null || item2 === undefined || item2 === ""){
                this.diem = 0; 
                this.capnhat = 0; 
            }
            else {
                // this.capnhat = 1; 
                this.assgnId = item2.assignmentId; 
                BaseRequest.get("result/user_assignment?userId=" + item1.userId + "&assignmentId=" + item2.assignmentId)
                .then(response => {
                    this.k = response.data; 
                    if(this.k === null || this.k === undefined || this.k === ""){
                        //Chưa chấm lần nào 
                        this.capnhat = 0; 
                    }
                    else {
                        //đã chấm 
                        this.capnhat = 1; 
                        this.result = this.k; 
                    }
                })
            }
        }, 
        capNhatDiem: function(){
            const input1 = document.getElementById("diem"); 
            const input2 = document.getElementById("feedback"); 

            const _delete = document.querySelectorAll(".error-diem"); 
            this.isCheck = true;
            for (const item of _delete) {
                item.parentNode.removeChild(item);
            }


            input1.style.borderColor = "#d0d0d0"; 
            input2.style.borderColor = "#d0d0d0";
            var isCheck = true; 

            if(this.diem === "" || this.diem === null || this.diem === undefined){
                this.thongbao(input1, "Điểm số không được để trống."); 
                isCheck = false; 
            }

            if(isCheck){
                //Thêm mới hoặc chỉnh sửa vào bảng result 
                if(this.capnhat === 0){
                    //api thêm mới 
                    this.result.resultCode = "string"; 
                    this.result.assignmentId = this.assgnId; 
                    this.result.userId = this.user.userId; 
                    this.result.score = this.diem; 
                    this.result.feedback = this.feedback; 

                    BaseRequest.post("result", this.result)
                    .then(response => {
                        console.log(response.data); 
                        alert("Cập nhật điểm thành công!"); 
                        document.getElementById("form-chamdiem").style.display = "none"; 
                        document.getElementById("diem_back").style.display = "none";
                    })
                }
                else {
                    //api sua doi 
                    this.result.score = this.diem; 
                    this.result.feedback = this.feedback; 

                    BaseRequest.put("result", this.result)
                    .then(response => {
                        console.log(response.data); 
                        alert("Cập nhật điểm thành công!"); 
                        document.getElementById("form-chamdiem").style.display = "none"; 
                        document.getElementById("diem_back").style.display = "none";
                    })
                    .catch(error => {
                        console.log(error.message); 
                    })
                }
            }

        }, 
        thongbao: function(item, mes){
            item.style.borderColor = "red"; 
            let p = document.createElement("p"); 
            let node = document.createTextNode(mes); 
            p.appendChild(node); 
            p.classList.add("error-diem");
            p.style.color = "red"; 
            p.style.fontStyle = "italic"; 
            p.style.fontSize = "0.9rem"; 
            item.after(p);
            //item.setAttribute("title", mes); 
        },
        closeChamDiem: function(){
            document.getElementById("form-chamdiem").style.display = "none"; 
            document.getElementById("diem_back").style.display = "none"; 
        }, 
        openClassFile: function(item){
            this.getCourse = item; 
            document.getElementById("class-danhgia-teacher").style.display = "block"; 
            document.getElementById("course-danhgia-teacher").style.display = "none"; 
            BaseRequest.get("class/teacher_course?teacher_CourseId=" + item.teacher_CourseId)
            .then(response => {
                this.classList = response.data; 
            })
            .catch(error => {
                console.log(error.message); 
            })

            BaseRequest.get("view_class_assignment/teacher_course?teacher_CourseId=" + item.teacher_CourseId)
            .then(response => {
                this.hocsinh_class = response.data; 
                console.log("Tong: "); 
                console.log(this.hocsinh_class); 
            })
            .catch(error => {
                console.log(error.message); 
            })
        }, 
        backFile1: function(){
            document.getElementById("class-danhgia-teacher").style.display = "none"; 
            document.getElementById("course-danhgia-teacher").style.display = "block"; 
            document.getElementById("hocsinh-danhgia-teacher").style.display = "none"; 
        }, 
        backFile2: function(){
            document.getElementById("class-danhgia-teacher").style.display = "block"; 
            document.getElementById("course-danhgia-teacher").style.display = "none"; 
            document.getElementById("hocsinh-danhgia-teacher").style.display = "none"; 
        }, 
        renderDate: function(item){
            let dateObject = new Date(item);

            let formattedDate = `${dateObject.getDate()}/${dateObject.getMonth() + 1}/${dateObject.getFullYear()}`;

            return formattedDate; 
        }, 
        openHocSinh: function(item){

            document.getElementById("hocsinh-danhgia-teacher").style.display = "block"; 
            document.getElementById("class-danhgia-teacher").style.display = "none"; 
            document.getElementById("course-danhgia-teacher").style.display = "none"; 
            BaseRequest.get("assignment/class?classId=" + item.classId)
            .then(response => {
                this.list1 = response.data;

                console.log(this.assignmentid); 

                if(this.list1.length > 0){
                    this.assignmentid = this.list1[0].assignmentId;  
                    BaseRequest.get("enrollment/user_enrollment_teacher_course?teacher_CourseId=" + this.getCourse.teacher_CourseId)
                    .then(response => {
                        this.hocsinh = response.data;

                        if (this.hocsinh.length === 0) {
                            alert("Không có học sinh đăng ký khóa học.");
                        } else {
                            // Tạo một mảng các promise
                            const promises = this.hocsinh.map(student => {
                                return BaseRequest.get("submit/submit_user_assignment?userId=" + student.userId + "&assignmentId=" + this.assignmentid)
                                    .then(response => {
                                        return response.data[0];
                                    });
                            });

                            const propmises1 = this.hocsinh.map(student1 => {
                                return BaseRequest.get("result/user_assignment?userId=" + student1.userId + "&assignmentId=" + this.assignmentid)
                                .then(response => {
                                    return response.data; 
                                }); 
                            }); 

                            // Đợi tất cả các yêu cầu hoàn thành trước khi xử lý dữ liệu
                            Promise.all(promises)
                                .then(submits => {
                                    this.submit = submits;

                                    // Kiểm tra xem mỗi học sinh đã nộp bài hay chưa
                                    this.check = this.submit.map(submit => {
                                        return submit !== null && submit !== undefined && submit !== "";
                                    });

                                    console.log(this.submit);
                                    console.log(this.check);
                                })
                                .catch(error => {
                                    console.log(error.message);
                                });

                            ///
                            Promise.all(propmises1)
                                .then(results => {
                                    this.ketqua_diem = results;

                                    // Kiểm tra xem mỗi học sinh đã nộp bài hay chưa
                                })
                                .catch(error => {
                                    console.log(error.message);
                                });
                        }
                    console.log(this.hocsinh);
                    })
                    .catch(error => {
                        console.log(error.message);
                    });
                }
                else {
                    alert("Không có bài tập về nhà."); 
                }

            })
        }, 
        // renderChart(data) {
        //     const ctx = this.$refs.chart.getContext('2d');
        //     if (this.chart) {
        //         this.chart.destroy();
        //     }
        //     this.chart = new Chart(ctx, {
        //         type: 'doughnut',
        //         data: {
        //         labels: ['Hoàn thành', 'Nộp muộn', 'Chưa hoàn thành'],
        //         datasets: [{
        //             label: 'Sample Data',
        //             data: data,
        //             backgroundColor: [
        //             'blue',
        //             'green',
        //             'red'
        //             ]
        //         }]
        //         },
        //         options: {
        //         responsive: false // Tắt tính năng phản ứng tự động của biểu đồ
        //         }
        //     }); 
        // },
        // beforeUnmount() {
        //     if (this.chart) {
        //         this.chart.destroy(); // Phá hủy biểu đồ khi component bị tháo gỡ
        //     }
        // }
    }

}
</script>
<style scoped>
.bieudo-btvn {
    display: flex; 
    justify-content: space-between;
}
.form-chamdiem {
    position: absolute; 
    top: 50px; 
    left: 25%; 
    width: 50%; 
    display: none;
    z-index: 1;
}
.form-chamdiem >div:last-child {
    background-color: #d0d0d0; 
    padding-top: 16px; 
    padding-bottom: 16px; 
    padding-right: 24px; 
    display: flex; 
    justify-content: flex-end;
}
.form-chamdiem >div:last-child > button {
    padding: 8px; 
    border: none; 
    background-color: #50B83C; 
    color: #fff; 
    border-radius: 6px;
}
.chamdiem-teacher {
    background-color: #fff; 
    display: flex; 
    justify-content: space-between;
    padding-top: 24px; 
    width: 100%;
    padding-left: 24px; 
    padding-right: 24px; 
}
.chamdiem-teacher > div {
    width: 100%; 
}
.chamdiem-teacher > div > label {
    width: 100%; 
}
.chamdiem-teacher > div > div > label > input {
    width: 60px; 
    height: 56px;
    border: 1px solid #d0d0d0; 
    outline: none; 
    margin-top: 8px; 
    margin-bottom: 16px;
    padding-left: 8px; 
}
.chamdiem-teacher > div > div {
    display: flex; 
    flex-direction: row;
    gap: 56px; 
}
.chamdiem-teacher > div > label > input:focus {
    border: 1px solid #50B83C;  
}
.chamdiem-teacher > div > label > textarea {
    width: 100%; 
    border: 1px solid #d0d0d0; 
    outline: none; 
    margin-top: 8px; 
    padding-left: 8px; 
}
.chamdiem-teacher > div > label > textarea:focus {
    border: 1px solid #50B83C; 
}
.file_submit {
    width: 200px; 
    height: 36px;
    border: 1px solid #d0d0d0; 
    outline: none; 
}
.file_submit:focus {
    border: 1px solid rgb(23, 193, 23); 
}
.diem-form {
    width: 45%; 
}
.bieudo-form {
    width: 45%; 
}
.trang3 {
    width: 100%; 
}

.trang3 > table > tbody {
    max-height: 300px;
}
#class-danhgia-teacher {
    display: none; 
}
#hocsinh-danhgia-teacher {
    display: none;
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
.hocsinh-teacher-title {
    display: flex; 
    width: 100%; 
    justify-content: space-between;
    padding-top: 24px; 
    padding-left: 24px; 
    padding-right: 24px; 
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
.icon_backup {
    background: url('@/assets/math/back-square-svgrepo-com.svg') no-repeat -5px -5px;
	width: 54px;
	height: 54px;
    transform: scale(0.6);
}

.bieudo-ngaynghi {
    width: 100%; 
    z-index: 1; 
    display: block; 
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

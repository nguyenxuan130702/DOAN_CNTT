<template>
    <ThemMoi :propCourse="getCourse" :propListClass="schedule1"></ThemMoi>
    <ChiTietLop :propGetClass="getClass"></ChiTietLop>
    <div class="lichhoc_course" id="lichhoc_course">
        <div>
            <h3>Lịch học lớp {{ getCourse.teacher_CourseName }}</h3>
            <div class="lichhoa_back" v-on:click="backQuanLyKhoaHoc()">
                <div class="icon_backup"></div>
                <div>Quay lại</div>
            </div>
        </div>
        <div class="a-course_table">
            <div class="a-course_table-title">
                <div class="a-course_table-title-btn">
                    <button v-on:click="openFormPostCourse()">Thêm mới</button>
                </div>
            </div>
            <div class="a-course_table-content">
                <table>
                    <thead>
                        <tr>    
                        <th>Giờ</th>
                        <th v-for="day in weekDates" :key="day.date">{{ day.dateString }}</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-for="hour in hoursOfDay" :key="hour">
                            <td>{{ hour }}</td>
                            <td v-for="day in weekDates" :key="day.date" v-bind:class="{'greenclass': isClassScheduled(day.date, hour)}">
                                <div v-if="isClassScheduled(day.date, hour)">
                                    <p>{{ trunggian[day.date].className }}</p>
                                    <div class="class_select">
                                        <div>
                                            <div class="seehover" title="Xem chi tiết" v-on:click="seeDetailCourse(trunggian[day.date])">
                                                <div class="icon_zoom"></div>
                                            </div>
                                            <div class="updatehover" title="Sửa khóa học" v-on:click="openFormPutCourse(trunggian[day.date])">
                                                <div class="icon_update"></div>
                                            </div>
                                        </div>
                                        <div class="deletehover" title="Xóa khóa học" v-on:click="deleteCourse(trunggian[day.date])">
                                            <div class="icon_delete"></div>
                                        </div>
                                    </div>
                                </div>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <div class="a-course_table-page">
                <div style="margin-right: 24px" class="a-course_table-page1">
                    <div v-on:click="backPageCourse()">
                        <div class="icon_back"></div>
                        <div>Tuần trước</div>
                    </div>
                    <div v-on:click="nextPageCourse()">
                        <div>Tuần sau</div>
                        <div class="icon_next"></div>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <!--Lịch học-->
    <div class="update_class" id="update_class">
        <div class="update_class-title">
            <h1>Sửa thông tin buổi học</h1>
            <div class="icon_close" v-on:click="closeSuaClass()"></div>
        </div>
        <div class="update_class-form">
            <div class="update_class-form1">
                <label for="">
                    <b>Nội dung mô tả: </b> <br>
                    <input type="text" id="mota-uclass" v-model="ten">
                </label> <br>
                <label for="" class="update_class-time">
                    <b>Thời gian học: </b> <br>
                    <div>
                        <input type="time" id="time-uclass" v-model="gio"> <br>
                        <input type="date" id="date-uclass" v-model="ngay">
                    </div>
                </label>
            </div>
            <div class="update_class-form2">
                <label for="">
                    <b>Link lớp học</b> <br>
                    <input type="text" id="lop-uclass" v-model="link_lop">
                </label> <br>
                <label for="">
                    <a :href="linktailieu" target="_blank">File tài liệu</a> <br>
                    <b>Tài liệu tham khảo</b> <br>
                    <input type="file" id="tailieu-uclass" @change="checkFileUploadAdmin">
                </label>
            </div>
        </div>
        <div class="update_class-button" v-on:click="capNhatLop()">
            <button>Cập nhật</button>
        </div>
    </div>
    <CanhBao></CanhBao>
</template>
<script>
import CanhBao from '@/components/admin/layout/DialogCanhbao.vue'
import BaseRequest from '@/core/BaseRequest'
import ChiTietLop from '@/components/teacher/quanly/khoahoc/ChiTietLop.vue'
import ThemMoi from '@/components/teacher/quanly/khoahoc/ThemLop.vue'
import moment from 'moment'
export default{
    components: {
        CanhBao, 
        ChiTietLop, 
        ThemMoi
    }, 
    props: ['propGetCourse'], 
    data(){
        return {
            weekDates: [],
            hoursOfDay: ['08:00:00', '09:00:00', '10:00:00', '11:00:00', '12:00:00', '13:00:00', '14:00:00', '15:00:00', '16:00:00', '17:00:00',
                '18:00:00', '19:30:00', '20:30:00', '21:00:00', '22:00:00' 
            ],
            schedule: [], 
            schedule1: [], 
            getCourse: {}, 
            trunggian: [], 
            getClass: {}, 
            updateClass: {}, 

            //dành cho update 
            ten: '', 
            gio: '', 
            ngay: '',
            link_lop: '', 
            linktailieu: '', 
            file: '', 
            listClass: [],
            timeDifference: {
                hours: 0,
                minutes: 0
            }, 

            assing: [], 
            assing_code: [], 
            submit: [], 
            submit_code: [], 
            result: [], 
            result_code: [], 
        }
    }, 
    mounted(){
        this.setWeekDates();
    }, 
    methods: { 
        deleteCourse: function(item){
            document.getElementById("thognbao").innerHTML = "Bạn có chắc chắn muốn xóa lích học ngày " + this.renderDate(item.date) + "?"; 
            document.getElementById("ad-dialog-delete").style.display = "block"; 
            document.getElementById("ad-dialog-btn").addEventListener("click", () => {
                BaseRequest.get("assignment/class?classId=" + item.classId)
                .then(response => {
                    this.assing = response.data;
                    if(this.assing.length > 0){
                        //Xóa bảng submit và result
                        for (const item of this.assing) {
                            this.assing_code.push(item.assignmentCode); 
                            BaseRequest.get("submit/submit_assignment?assignmentId=" + item.assignmentId)
                            .then(response => {
                                this.submit = response.data;
                                if(this.submit.length > 0){
                                    for (const item of this.submit) {
                                        this.submit_code.push(item.submitCode); 
                                    }

                                    //Xóa submit
                                    BaseRequest.delete("submit/any", this.submit_code)
                                    .then(response => {
                                        console.log(response.data); 
                                    })
                                    .catch(error => {
                                        console.log(error.message); 
                                    })
                                }
                            })
                            .catch(error => {
                                console.log(error.message); 
                            })


                            BaseRequest.get("result/assignment?assignmentId=" + item.assignmentId)
                            .then(response => {
                                this.result = response.data; 
                                if(this.result.length > 0){
                                    for (const item of this.result) {
                                        this.result_code.push(item.resultCode); 
                                    }

                                    BaseRequest.delete("result/any", this.result_code)
                                    .then(response => {
                                        console.log(response.data); 
                                    })
                                    .catch(error => {
                                        console.log(error.message); 
                                    })
                                }
                            })
                            .catch(error => {
                                console.log(error.message); 
                            })
                        }
                        BaseRequest.delete("assignment/any", this.assing_code)
                        .then(response => {
                            console.log(response.data); 
                        })
                        .catch(error => {
                            console.log(error.message); 
                        })
                    } 
                })
                .catch(error => {
                    console.log(error.message); 
                })

                //Xoa class 
                BaseRequest.delete("class?code=" + item.classCode)
                .then(response => {
                    console.log(response.data); 
                    document.getElementById("ad-dialog-delete").style.display = "none";
                    alert("Xóa lịch học thành công!"); 
                })
                .catch(error => {
                    console.log(error.message); 
                })
            })
        }, 
        nextPageCourse: function(){
            this.nextweek(); 
        }, 
        backPageCourse: function(){
            this.setbackWeekDates(); 
        }, 
        closeSuaClass: function(){
            document.getElementById("update_class").style.display = "none"; 
            document.getElementById("course_back").style.display = "none"; 
        }, 

        //Update 
        capNhatLop: function(){
            const input1 = document.getElementById("mota-uclass"); 
            const input2 = document.getElementById("time-uclass"); 
            const input3 = document.getElementById("date-uclass"); 
            const input4 = document.getElementById('lop-uclass'); 
            const input5 = document.getElementById("tailieu-uclass"); 

            const _delete = document.querySelectorAll(".error-update-class"); 
            this.isCheck = true;
            for (const item of _delete) {
                item.parentNode.removeChild(item);
            }

            input1.style.borderColor = "#d0d0d0"; 
            input2.style.borderColor = "#d0d0d0"; 
            input3.style.borderColor = "#d0d0d0"; 
            input4.style.borderColor = "#d0d0d0"; 
            input5.style.borderColor = "#d0d0d0";

            var isCheck = true; 
            if(this.ten === "" || this.ten === undefined || this.ten === null){
                this.thongbao(input1, "Nội dung mô tả không được để trống."); 
                isCheck = false; 
            }
            
            if(this.gio === "" || this.gio === undefined || this.gio === null){
                this.thongbao(input2, "Giờ học không được để trống."); 
                isCheck = false; 
            }
            else {
                if(this.ngay === "" || this.ngay === undefined || this.ngay === null){
                    this.thongbao(input3, "Ngày học không được để trống."); 
                    isCheck = false; 
                }
                else {
                    const datenew = new Date(); 
                    console.log(this.renderDate(datenew)); 

                    this.listClass = this.schedule; 
                    
                    for (const item of this.listClass) {
                        this.sosanhGio(item.time, this.gio); 
                        if(this.timeDifference.hours < 1 && this.ngay === this.renderDate(item.date) && this.ngay > this.renderDate(datenew)){
                            this.thongbao(input2, "Trùng lịch với ngày " + this.ngay); 
                            isCheck = false;
                            break;  
                        }
                        if(this.gio === item.time && this.ngay === this.renderDate(item.date) && this.ngay > this.renderDate(datenew)){
                            this.thongbao(input2, "Trùng lịch với ngày " + this.ngay); 
                            isCheck = false;
                            break;  
                        }
                    }
                }
            }

            if(this.ngay === "" || this.ngay === undefined || this.ngay === null){
                this.thongbao(input3, "Ngày học không được để trống."); 
                isCheck = false; 
            }


            if(this.link_lop === "" || this.link_lop === undefined || this.link_lop === null){
                this.thongbao(input4, "Link lớp học không được để trống."); 
                isCheck = false; 
            }

            if(isCheck){
                //chinh sua

                if(this.file === null || this.file === undefined || this.file === ""){

                    //goi api khong file 
                    this.updateClass.className = this.ten; 
                    this.updateClass.date = this.ngay; 
                    this.updateClass.time = this.gio; 
                    this.updateClass.classLink = this.link_lop; 

                    console.log(this.updateClass); 

                    BaseRequest.put("class", this.updateClass)
                    .then(response => {
                        console.log(response.data); 
                        alert("Cập nhật buổi học thành công!"); 
                        document.getElementById("update_class").style.display = "none"; 
                        document.getElementById("course_back").style.display = "none";
                    })
                    .catch(error => {
                        console.log(error.message); 
                    })
                }
                else {
                    //gọi api update file 
                    const formData = new FormData(); 
                    formData.append("ClassId", this.updateClass.classId); 
                    formData.append("ClassCode", this.updateClass.classCode); 
                    formData.append("Teacher_CourseId", this.updateClass.teacher_CourseId); 
                    formData.append("Date", this.ngay); 
                    formData.append("Time", this.gio); 
                    formData.append("ClassLink", this.link_lop); 
                    formData.append("ClassName", this.ten); 
                    formData.append("File", this.file); 

                    BaseRequest.put("class/FilePdf", formData)
                    .then(response => {
                        console.log(response.data); 
                        alert("Cập nhật buổi học thành công!"); 
                        document.getElementById("update_class").style.display = "none"; 
                        document.getElementById("course_back").style.display = "none";
                    })
                    .catch(error => {
                        console.log(error.data.message); 
                    })
                }
            }
        }, 
        sosanhGio: function(item1, item2){
            const start = moment(item1, 'HH:mm');
            const end = moment(item2, 'HH:mm');
            const duration = moment.duration(end.diff(start));
            this.timeDifference.hours = duration.hours();
            this.timeDifference.minutes = duration.minutes();


            // const start = moment(item1); 
            // const end = moment(item2); 
            // const duration = moment.duration(end.diff(start)); 
            // return duration.days; 
        }, 
        /**
         * Hàm thêm mới thông báo khi có lỗi vào sau muỗi input lỗi. 
         * created by: NTXuan - 22/01/2024. 
         */
        thongbao: function(item, mes){
            item.style.borderColor = "red"; 
            let p = document.createElement("p"); 
            let node = document.createTextNode(mes); 
            p.appendChild(node); 
            p.classList.add("error-update-class");
            p.style.color = "red"; 
            p.style.fontStyle = "italic"; 
            p.style.fontSize = "0.9rem"; 
            item.after(p);
            //item.setAttribute("title", mes); 
        },
        checkFileUploadAdmin(event){
            this.file = event.target.files[0]; 
            if(this.file){
                //đọc URL của file và gán vào imageUrl để hiển thị 
                const reander = new FileReader(); 
                reander.readAsDataURL(this.file); 
            }
        },


        openFormPostCourse: function(){
            document.getElementById("post_class").style.display = "block"; 
            document.getElementById("course_back").style.display = "block";
        }, 
        seeDetailCourse: function(item){
            this.getClass = item; 
            document.getElementById("detail_class").style.display = "block"; 
            document.getElementById("course_back").style.display = "block"; 
        }, 
        openFormPutCourse: function(item){
            this.updateClass = item; 
            this.ten = item.className; 
            this.gio = item.time; 
            this.ngay = item.date; 
            this.link_lop = item.classLink;
            this.linktailieu = item.documentLink; 
            console.log(item);  
            document.getElementById("update_class").style.display = "block"; 
            document.getElementById("course_back").style.display = "block"; 
        }, 
        setWeekDates() {
        const today = new Date();
        const startOfWeek = today.getDate() - today.getDay();
        this.weekDates = [...Array(7)].map((_, i) => {
            const date = new Date(today.setDate(startOfWeek + i));
            const dateString = `${date.getDate()}/${date.getMonth() + 1}/${date.getFullYear()}`;
            return {
            date: date.toISOString().split('T')[0], // format YYYY-MM-DD
            dateString: dateString
            };
        });
        },
        nextweek() {
            // Move to the previous week
            // Example logic: decrement the start date of the week by 7 days
            const startDate = new Date(this.weekDates[0].date);
            startDate.setDate(startDate.getDate() + 7);

            this.weekDates = [...Array(7)].map((_, i) => {
                const date = new Date(startDate);
                date.setDate(startDate.getDate() + i);
                const dateString = `${date.getDate()}/${date.getMonth() + 1}/${date.getFullYear()}`;
                return {
                    date: date.toISOString().split('T')[0], // format YYYY-MM-DD
                    dateString: dateString
                };
            });

    // Fetch events for the previous week
        }, 
        setbackWeekDates() {
            // Move to the previous week
            // Example logic: decrement the start date of the week by 7 days
            const startDate = new Date(this.weekDates[0].date);
            startDate.setDate(startDate.getDate() - 7);

            this.weekDates = [...Array(7)].map((_, i) => {
                const date = new Date(startDate);
                date.setDate(startDate.getDate() + i);
                const dateString = `${date.getDate()}/${date.getMonth() + 1}/${date.getFullYear()}`;
                return {
                    date: date.toISOString().split('T')[0], // format YYYY-MM-DD
                    dateString: dateString
                };
            });
        },
        isClassScheduled(date, time) {
            for (const item of this.schedule) {
                if(date === this.renderDate(item.date) && time === item.time){
                    this.trunggian[date] = item; 
                    return true;  
                }
            }
            return false; 
        }, 
        renderDate: function(item){
            let dateObject = new Date(item);
            let y = dateObject.getFullYear(); 
            let m = dateObject.getMonth() + 1; 
            if(m < 10){
                m = '0' + m; 
            }

            let d = dateObject.getDate(); 
            if(d < 10){
                d = '0' + d; 
            }

            let formattedDate = y + "-" + m + "-" + d; 

            return formattedDate;  
        },
        backQuanLyKhoaHoc: function(){
            document.getElementById("course-teacher").style.display = "block"; 
            document.getElementById("hocsinh-teacher").style.display = "none"; 
            document.getElementById("lichhoc_course").style.display = "none"; 
        }, 
    }, 
    watch: {
        propGetCourse: function(){
            this.getCourse = this.propGetCourse; 
            BaseRequest.get("view_class_assignment/teacher_course?teacher_CourseId=" + this.getCourse.teacher_CourseId)
            .then(response => {
                this.schedule1  = response.data;
                console.log(this.schedule1); 
            })
            .catch(error => {
                console.log(error.message); 
            })

            BaseRequest.get("class/teacher_course?teacher_CourseId=" + this.getCourse.teacher_CourseId)
            .then(response => {
                this.schedule = response.data; 
                console.log(this.schedule); 
            })
            .catch(error => {
                console.log(error.message); 
            })
        }
    }
}
</script>
<style scoped>
.a-course_table-page1 {
    margin: auto; 
}
.a-course_table-page1 > div {
    display: flex; 
    gap: 16px; 
    align-items: center;
}
.a-course_table-page1 > div:hover {
    cursor: pointer;
    color: green;
}
.class_select {
    position: absolute; 
    top: 0; 
    left: 0;  
    text-align: center;
    display: none; 
}
.class_select>div:first-child {
    display: flex; 
    gap: 16px; 
    margin-bottom: 16px; 
}
.greenclass {
    background-color: green;
    color: #fff; 
}
tbody > tr {
    min-height: 200px;
}
.greenclass:hover {
    background-color: #50B83C; 
    cursor: pointer;
}
.greenclass:hover .class_select{
    display: block; 
}
.lichhoc_course {
    width: calc(100% - 250px);
    height: calc(100vh - 56px);
    background-color: #f5f6f7; 
    float: left;
    display: none; 
}
.lichhoc_course > div:first-child > h1 {
    font-weight: 700;
}
.lichhoc_course > div:first-child {
    display: flex; 
    width: 100%; 
    justify-content: space-between;
    padding-top: 24px; 
    padding-left: 24px; 
    padding-right: 24px; 
}
.xemchitiet > button {
    background-color: orange; 
    padding: 8px; 
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
.icon_link {
    background: url('@/assets/math/link-alt-1-svgrepo-com.svg') no-repeat -4px -11px;
	width: 64px;
	height: 42px;
    transform: scale(0.4);
}
.icon_document {
    background: url('@/assets/math/document-medicine-svgrepo-com.svg') no-repeat -6px -3px;
	width: 64px;
	height: 58px;
    transform: scale(0.4);
}
.lienket {
    display: flex; 
    gap: 16px; 
    align-items: center;
}
.lienket > a {
    display: flex; 
    align-items: center;
    justify-content: center;
    width: 50px;
    height: 50px;
    background-color: #f5f6f7; 
    border: 1px solid #0a0a23; 
    border-radius: 50%;
}
.lienket > a:hover {
    background-color: #d0d0d0; 
    cursor: pointer;
}
.icon_backup {
    background: url('@/assets/math/back-square-svgrepo-com.svg') no-repeat -5px -5px;
	width: 54px;
	height: 54px;
    transform: scale(0.6);
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

/**update */
.update_class {
    position: absolute; 
    top: 100px; 
    left: 20%; 
    background-color: #fff; 
    width: 60%;
    z-index: 1;
    display: none; 
}
.update_class-title {
    display: flex; 
    justify-content: space-between;
    align-items: center;
    padding-top: 24px; 
    padding-left: 24px;
    padding-right: 24px; 
}
.update_class-form {
    display: flex; 
    justify-content: space-between;
    padding: 24px; 
}
.update_class-time {
    display: flex; 
    gap: 16px; 
    margin-top: 16px; 
}
.update_class-time > div {
    width: 80%; 
}
.update_class-time > div > input {
    width: 100%; 
    margin-bottom: 16px; 
}
.update_class-time > div > input {
    width: 100%; 
    margin-top: 8px; 
    height: 36px;
    border: 1px solid #d0d0d0; 
    border-radius: 6px;
    outline: none;
    padding-left: 8px;
}
.update_class-time > div > input:focus {
    border: 1px solid #50B83C; 
}
.update_class-form1 {
    width: 45%;
}
.update_class-form2 {
    width: 45%; 
}
.update_class-form1 > label {
    width: 100%; 
}
.update_class-form2 > label{
    width: 100%; 
    margin-bottom: 16px; 
}
.update_class-form1 > label > input{
    width: 100%; 
    margin-top: 8px; 
    height: 36px;
    border: 1px solid #d0d0d0; 
    border-radius: 6px;
    outline: none;
    padding-left: 8px;
}
.update_class-form2 > label > input {
    width: 100%; 
    margin-top: 8px; 
    height: 36px;
    border: 1px solid #d0d0d0; 
    border-radius: 6px;
    outline: none;
    padding-left: 8px; 
}
.update_class-form2 > label:last-child > input {
    border: none; 
}
.update_class-form1 > label > input:focus {
    border: 1px solid #50B83C; 
}
.update_class-form2 > label > input:focus {
    border: 1px solid #50B83C; 
}
.update_class-form2 > label > input {
    width: 100%; 
    margin-top: 8px; 
}
.update_class-button {
    width: 100%; 
    padding: 24px; 
    background-color: #d0d0d0; 
    display: flex; 
    justify-content: flex-end;
}
.update_class-button > button {
    padding: 8px; 
    background-color: #50B83C; 
    border: none; 
    color: #fff; 
}
</style>

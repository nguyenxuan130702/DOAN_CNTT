<template>
    <div class="update_course" id="update_course">
        <div class="update_course-title">
            <h1>Sửa thông tin lớp học</h1>
            <div class="icon_close" v-on:click="closeUpdateCourse()"></div>
        </div>
        <div class="update_course-input">
            <div>
                <label for="">
                    <b>Tên lớp học: </b> <br>
                    <input type="text" id="ten-utcourse" v-model="ten">
                </label> <br>
                <label for="">
                    <b>Mô tả khóa học: </b> <br>
                    <textarea name="" id="mota-utcourse" cols="30" rows="10" v-model="mota"></textarea>
                </label>
                <label for="">
                    <b>Thời gian bắt đầu đăng ký: </b>
                    <input type="date" id="date1" v-model="tgbddk">
                </label> 
            </div>
            <div>
                <label for="">
                    <img :src="imgUrl" alt=""> <br>
                    <input type="file" id="file1" @change="checkFileUploadAdmin">
                </label> <br>
                <label for="">
                    <b>Thời gian bắt đầu học: </b>
                    <input type="date" id="date2" v-model="tgbd">
                </label> <br>
                <label for="">
                    <b>Thời gian kết thúc học dự kiện: </b>
                    <input type="date" id="date3" v-model="tgkt">
                </label> <br>
                <label for="">
                    <b>Thời gian kết thúc đăng ký: </b>
                    <input type="date" id="date4" v-model="tgktdk">
                </label>
            </div>
        </div>
        <div class="update_class-button" v-on:click="capNhatKhoaHoc()">
            <button>Cập nhật</button>
        </div>
    </div>
</template>
<script>
import BaseRequest from '@/core/BaseRequest';
export default {
    props: ["propGetCourse"], 
    data(){
        return {
            ten: "", 
            mota: "", 
            tgbddk: "", 
            tgbd: "", 
            tgkt: "", 
            tgktdk: "", 
            course: {}, 
            file: "", 
            imgUrl: "", 
        }
    }, 
    methods: {
        capNhatKhoaHoc: function(){
            const input1 = document.getElementById("ten-utcourse"); 
            const input2 = document.getElementById("mota-utcourse"); 
            const input3 = document.getElementById("date1");
            const input4 = document.getElementById("file1");
            const input5 = document.getElementById("date2");
            const input6 = document.getElementById("date3");
            const input7 = document.getElementById("date4");

            const _delete = document.querySelectorAll(".error-course-te"); 
            this.isCheck = true;
            for (const item of _delete) {
                item.parentNode.removeChild(item);
            }

            input1.style.borderColor = "#d0d0d0"; 
            input2.style.borderColor = "#d0d0d0"; 
            input3.style.borderColor = "#d0d0d0"; 
            input4.style.borderColor = "#d0d0d0"; 
            input5.style.borderColor = "#d0d0d0";
            input6.style.borderColor = "#d0d0d0";
            input7.style.borderColor = "#d0d0d0";

            var isCheck = true; 
            if(this.ten === "" || this.ten === null || this.ten === undefined){
                this.thongbao(input1, "Tên lớp học không được để trống."); 
                isCheck = false; 
            }

            if(this.mota === "" || this.mota === null || this.mota === undefined){
                this.thongbao(input2, "Mô tả khóa học không được phép để trống."); 
                isCheck = false; 
            }

            if(this.tgbd > this.tgkt){
                this.thongbao(input6, "Thời gian kết thúc phải sau thời gian học."); 
                isCheck = false; 
            }

            if(this.tgbddk > this.tgktdk){
                this.thongbao(input7, "Thời gian kết thúc đăng ký phải sau thời gian học."); 
                isCheck = false; 
            }

            if(this.tgkt < this.tgktdk){
                this.thongbao(input6, "Thời gian kết thúc đăng ký phải trước thời gian kết thúc."); 
                isCheck = false; 
            }

            if(isCheck){
                //Update 
                if(this.file === "" || this.file === undefined || this.file === null){
                    //Gọi api ko co anh 
                    this.course.teacher_CourseName = this.ten; 
                    this.course.teacher_CourseDescription = this.mota; 
                    this.course.startTime = this.tgbd; 
                    this.course.endTime = this.tgkt; 
                    this.course.registrationStartTime = this.tgbddk;
                    this.course.registrationEndTime = this.tgktdk; 

                    console.log(this.course); 

                    BaseRequest.put("teacher_course", this.course)
                    .then(response => {
                        console.log(response.data);
                        alert("Cập nhật thành công lớp học có mã " + this.course.teacher_CourseCode); 
                        document.getElementById("update_course").style.display = "none";
                        document.getElementById("course_back").style.display = "none";
                        
                         
                    })
                    .catch(error => {
                        console.log(error.message); 
                    })
                }
            }
            
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
            p.classList.add("error-course-te");
            p.style.color = "red"; 
            p.style.fontStyle = "italic"; 
            p.style.fontSize = "0.9rem"; 
            item.after(p);
            //item.setAttribute("title", mes); 
        },
        closeUpdateCourse: function(){
            document.getElementById("update_course").style.display = "none";
            document.getElementById("course_back").style.display = "none"; 
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

            // Format dateObject to desired date format
            let formattedDate = y + "-" + m + "-" + d; 

            // Assign the formatted date to thoiGian
            return formattedDate;  
        },
        checkFileUploadAdmin(event){
            this.file = event.target.files[0]; 
            if(this.file){
                //đọc URL của file và gán vào imageUrl để hiển thị 
                const reander = new FileReader(); 
                reander.onload = (e) => {
                    this.imgUrl = e.target.result; 
                }; 
                reander.readAsDataURL(this.file); 
            }
        },
    }, 
    watch: {
        propGetCourse: function(){
            this.course = this.propGetCourse; 
            this.ten = this.propGetCourse.teacher_CourseName; 
            this.mota = this.propGetCourse.teacher_CourseDescription; 
            this.tgbddk = this.renderDate(this.propGetCourse.registrationStartTime);
            this.tgbd = this.renderDate(this.propGetCourse.startTime); 
            this.tgkt = this.renderDate(this.propGetCourse.endTime); 
            this.tgktdk = this.renderDate(this.propGetCourse.registrationEndTime); 
            this.imgUrl = this.propGetCourse.teacher_CourseImage; 
        }
    }, 
}
</script>
<style scoped>
.update_course {
    background-color: #fff; 
    position: absolute; 
    top: 50px; 
    left: 20%; 
    z-index: 1;
    width: 60%; 
    display: none; 
}
.update_course-input {
    display: flex; 
    justify-content: space-between;
    padding-left: 24px; 
    padding-right: 24px; 
}
.update_course-input > div {
    width: 45%; 
}
.update_course-input > div > label {
    width: 100%; 
}
.update_course-input > div > label > img {
    margin-bottom: 8px; 
    width: 300px; 
    height: 150px;
}
.update_course-input > div > label > input {
    width: 100%; 
    height: 36px;
    border: 1px solid #d0d0d0; 
    padding-left: 8px; 
    margin-top: 8px; 
    margin-bottom: 16px; 
    outline: none; 
}
.update_course-input > div > label > input:focus {
    border: 1px solid #50B83C; 
}
.update_course-input > div > label > textarea {
    width: 100%;
    border: 1px solid #d0d0d0; 
    margin-top: 8px; 
    margin-bottom: 16px; 
    outline: none; 
    padding-left: 8px; 
}
.update_course-input > div > label > textarea:focus {
    border: 1px solid #50B83C; 
}
.update_course-title {
    display: flex; 
    justify-content: space-between;
    align-items: center;
    padding: 24px; 
}
.update_class-button {
    width: 100%; 
    padding-right: 24px;
    padding-top: 16px; 
    padding-bottom: 16px; 
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

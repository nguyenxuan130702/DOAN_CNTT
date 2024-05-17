<template>
    <div class="update_course" id="post_course">
        <div class="update_course-title">
            <h1>Thêm mới lớp học</h1>
            <div class="icon_close" v-on:click="closePostCourse()"></div>
        </div>
        <div class="update_course-input">
            <div>
                <label for="">
                    <b>Tên lớp học: </b> <br>
                    <input type="text" id="ten-tcourse" v-model="ten">
                </label> <br>
                <label for="">
                    <b>Mô tả khóa học: </b> <br>
                    <textarea name="" id="mota-tcourse" cols="30" rows="10" v-model="mota"></textarea>
                </label>
                <label for="">
                    <b>Thời gian bắt đầu đăng ký: </b>
                    <input type="date" id="t-date1" v-model="tgbddk">
                </label> 
            </div>
            <div>
                <label for="">
                    <b>Ảnh mô tả</b> <br>
                    <img :src="imgUrl" alt="" v-if="selectImage"> <br>
                    <input type="file" id="t-file1" @change="checkFileUploadAdmin">
                </label> <br>
                <label for="">
                    <b>Thời gian bắt đầu học: </b>
                    <input type="date" id="t-date2" v-model="tgbd">
                </label> <br>
                <label for="">
                    <b>Thời gian kết thúc học dự kiện: </b>
                    <input type="date" id="t-date3" v-model="tgkt">
                </label> <br>
                <label for="">
                    <b>Thời gian kết thúc đăng ký: </b>
                    <input type="date" id="t-date4" v-model="tgktdk">
                </label>
            </div>
        </div>
        <div class="update_class-button" v-on:click="themKhoaHoc()">
            <button>Thêm mới</button>
        </div>
    </div>
</template>
<script>
import BaseRequest from '@/core/BaseRequest';
export default {
    data(){
        return {
            ten: "", 
            mota: "", 
            tgbddk: "", 
            tgbd: "", 
            tgktdk: "", 
            tgkt: "",
            imgUrl: "", 
            file: "", 
            selectImage: false, 
            teacher: {}, 
        }
    }, 
    mounted(){
        this.teacher = JSON.parse(localStorage.getItem("teacherLogin")); 
    }, 
    methods: {
        closePostCourse: function(){
            document.getElementById("post_course").style.display = "none"; 
            document.getElementById("course_back").style.display = "none"; 
        }, 
        themKhoaHoc: function(){
            const input1 = document.getElementById("ten-tcourse"); 
            const input2 = document.getElementById("mota-tcourse"); 
            const input3 = document.getElementById("t-date1");
            const input4 = document.getElementById("t-file1");
            const input5 = document.getElementById("t-date2");
            const input6 = document.getElementById("t-date3");
            const input7 = document.getElementById("t-date4");

            const _delete = document.querySelectorAll(".error-course-tte"); 
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

            if(this.ten === "" || this.ten === undefined || this.ten === null){
                this.thongbao(input1, "Tên lớp học không được để trống."); 
                isCheck = false; 
            }

            if(this.mota === "" || this.mota === undefined || this.mota === null){
                this.thongbao(input2, "Nội dung mô tả không được để trống."); 
                isCheck = false; 
            }

            if(this.file === null || this.file === undefined || this.file === null){
                this.thongbao(input4, "File ảnh không được để trống."); 
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
                //Tiến hành thêm mới 
                const formData = new FormData();
                formData.append("Teacher_CourseCode", "string"); 
                formData.append("Teacher_CourseName", this.ten); 
                formData.append("Teacher_CourseDescription", this.mota); 
                formData.append("Teacher_CourseDescription", this.mota); 
                formData.append("TeacherId", this.teacher.teacherId);
                formData.append("StartTime", this.tgbd); 
                formData.append("EndTime", this.tgkt); 
                formData.append("RegistrationStartTime", this.tgbddk); 
                formData.append("RegistrationEndTime", this.tgktdk); 
                formData.append("Image", this.file);

                BaseRequest.post("teacher_course/PostImage", formData) 
                .then(response => {
                    console.log(response.data); 
                    alert("Thêm mới lớp học thành công!"); 
                    document.getElementById("post_course").style.display = "none"; 
                    document.getElementById("course_back").style.display = "none";
                })
                .catch(error => {
                    console.log(error.message); 
                })
                
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
            p.classList.add("error-course-tte");
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
                reander.onload = (e) => {
                    this.selectImage = true; 
                    this.imgUrl = e.target.result; 
                }; 
                reander.readAsDataURL(this.file); 
            }
        },
    }
}
</script>
<style scoped>
#post_course {
    display: none; 
}
.update_course {
    background-color: #fff; 
    position: absolute; 
    top: 30px; 
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

<template>

    <div class="form_course" id="form_video-course">
        <div class="form_course_title">
            <h1>Thêm mới khóa học video</h1>
            <div id="check"></div>
            <div class="icon_close" v-on:click="closeFormCourse()"></div>
        </div>
        <div class="form_course-content-x">
            <div class="a-course-title">
                <div>
                    <b>Tên khóa học </b><br>
                    <input type="text" placeholder="Tên khóa học" id="tenkh-video-course" name="tenkh-video-course" v-model="tenkhoahoc">
                </div>
                <div class="course_radio">
                    <b>Ảnh khóa học: </b>
                    <input type="file" name="img-video-course" id="img-video-course" @change="checkFileUploadAdmin" accept="image/*">
                    <img v-if="checkImg" :src="imageUrl" alt="">
                </div>
            </div>
            <div class="a-course-title">
                <b>Mô tả </b><br>
                <textarea name="motakh" id="mota-video-course" cols="30" rows="10" placeholder="Mô tả" v-model="mota"></textarea>
            </div>
        </div>
        <div class="course_btn" v-on:click="validatePushCourse()">
            <button>Thêm mới</button>
        </div>
    </div>

    <!--Thông báo thêm mới thành công-->
    <div class="dialog_dangky" id="dialog_themmoivideocourse" >
        <div class="dialog_dangky_title">
            <div class="icon_thanhcong"></div>
            <p id="dialog_videocourse_content">Thêm mới thành công.</p>
        </div>
        <div class="icon_close" v-on:click="closeDialogPushCourse()"></div>
    </div>
</template>
<script>
import BaseRequest from '@/core/BaseRequest';
//import BaseRequest from '@/core/BaseRequest';

export default{
    props: ['propVideo_Course'], 
    data(){
        return { 
            tenkhoahoc: "", 
            mota: "", 
            indexCode: 0, 
            file: "", 
            checkImg: false, 
            imageUrl: "", 
            currentDate: "", 
        }
    }, 
    watch: {
        propVideo_Course: function(){
            this.indexCode = this.propVideo_Course.length + 1; 
        }
    },
    methods: {
        closeDialogPushCourse: function(){
            document.getElementById("dialog_themmoivideocourse").style.opacity = "0";
        }, 
        closeFormCourse: function(){
            document.getElementById("form_video-course").style.display = "none"; 
            document.getElementById("video-course_back").style.display = "none"; 
        }, 
        checkFileUploadAdmin: function(event){
            this.file = event.target.files[0]; 
            if(this.file){
                //đọc URL của file và gán vào imageUrl để hiển thị 
                const reander = new FileReader(); 
                reander.onload = (e) => {
                    this.imageUrl = e.target.result; 
                }; 
                this.checkImg = true; 
                reander.readAsDataURL(this.file); 
            }
        },
        validatePushCourse: function(){
            const _delete = document.querySelectorAll(".error-video-course"); 
            for (const item of _delete) {
                item.parentNode.removeChild(item);
            }

            const inputTen = document.getElementById("tenkh-video-course"); 
            const inputMota = document.getElementById("mota-video-course"); 
            const inputImg = document.getElementById("img-video-course"); 

            inputTen.style.borderColor = "#d0d0d0"; 
            inputMota.style.borderColor = "#d0d0d0";
            inputImg.style.borderColor = "#d0d0d0";  

            var isCheck = true; 

            if(this.tenkhoahoc === null || this.tenkhoahoc === undefined || this.tenkhoahoc === ""){
                this.thongbao(inputTen, "Tên khóa học không được phép để trống."); 
                isCheck = false; 
            }

            if(this.mota === null || this.mota === undefined || this.mota === ""){
                this.thongbao(inputMota, "Nội dung mô tả không được để trống."); 
                isCheck = false; 
            }
            
            if(this.file === null || this.file === undefined || this.file === ""){
                this.thongbao(inputImg, "Ảnh mô tả không được để trống."); 
                isCheck = false; 
            }

            if(isCheck){
                const datenow = new Date(); 
                var d = datenow.getDate();  
                var m = datenow.getMonth() + 1; 
                var y = datenow.getFullYear(); 
                if(d < 10){
                    if(m < 10){
                        this.currentDate = y + "-0" + m + "-0" + d;
                    }
                    else {
                        this.currentDate = y + "-" + m + "-0" + d;
                    }
                }
                else {
                    if(m < 10){
                        this.currentDate = y + "-0" + m + "-" + d;
                    }
                    else {
                        this.currentDate = y + "-" + m + "-" + d;
                    }
                }


                this.indexCode = this.propVideo_Course.length + 1;
                const code = "VC" + this.indexCode; 
                document.getElementById('check').innerHTML = code; 
                const formData = new FormData(); 
                formData.append("Video_CourseCode", code); 
                formData.append("Video_CourseTitle", this.tenkhoahoc); 
                formData.append("Video_CourseDescription", this.mota); 
                formData.append("Created_at", this.currentDate); 
                formData.append("Image", this.file); 

                BaseRequest.post("video_course/image_video", formData)
                .then(response => {
                    //thông báo thêm mới thành công 
                    console.log(response.data); 
                    document.getElementById("dialog_videocourse_content").innerHTML = "Thêm mới thành công."; 
                    document.getElementById("dialog_themmoivideocourse").style.opacity = "1"; 
                    document.getElementById("form_video-course").style.display = "none"; 
                    document.getElementById("video-course_back").style.display = "none"; 
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
            p.classList.add("error-video-course");
            p.style.color = "red"; 
            p.style.fontStyle = "italic"; 
            p.style.fontSize = "0.9rem"; 
            item.after(p);
            //item.setAttribute("title", mes); 
        },
    }
}
</script>
<style>
.a-course-title > div > input[type="text"]::placeholder {
    font-style: italic;
    font-size: 0.9rem; 
}
.form-course-title > div {
    width: 100%; 
}
.form-course-title > div > input[type="text"] {
    width: 100%; 
    height: 36px;
    border: 1px solid #d0d0d0; 
    padding-left: 8px; 
    margin-top: 16px; 
    outline: none;
    margin-bottom: 16px; 
}


.form-course-title > div > input[type="text"]:focus{
    border: 1px solid #50B83C; 
}
.form_course_title > h1 {
    font-size: 1.75rem; 
    font-weight: 700;
}
.form_course_title {
    width: calc(100% - 24px - 24px);  
    display: flex; 
    justify-content: space-between;
    margin: auto; 
}
* {
    position: relative; 
}
.form_course-content-x {
    width: calc(100% - 24px - 24px); 
    margin: auto; 
     display: flex;
    flex-wrap: wrap;
    justify-content: space-between; 
    margin-top: 24px; 
}
.form_course-content-x > div > input[type="text"] {
    height: 36px;
    border: 1px solid #d0d0d0; 
    width: 100%; 
    padding-left: 8px; 
    outline: none;
    margin-top: 8px; 
}
.form_course-content-x > div > input[type="text"]::placeholder{
    font-style: italic;
    font-size: 0.9rem; 
}
.form_course-content-x > div > input:focus {
    border: 1px solid #50B83C; 
}
.form_course-content-x > div > textarea{
    height: 300px;
    width: 100%; 
    border: 1px solid #d0d0d0; 
    margin-top: 8px; 
    padding-left: 8px; 
    outline: none; 
}
.form_course-content-x > div > textarea::placeholder{
    font-style: italic;
    font-size: 0.9rem;
}
.form_course-content-x > div > textarea:focus {
    border: 1px solid #50B83C; 
}

.course_radio > input[type="radio"]{
    width: 20px; 
    height: 20px;
    margin-top: 8px; 
}
.course_radio > label {
    padding-left: 16px;  
}
.form_course_title {
    padding-top: 24px; 
}
.course_btn {
    width: 100%; 
    background-color: #d0d0d0; 
    padding: 16px; 
    display: flex; 
    justify-content: flex-end;
    align-items: center;
    margin-top: 16px; 
}
.course_btn > button {
    height: 36px;
    border: 1px solid #d0d0d0; 
    background-color: #50B83C; 
    color: #fff; 
    padding-left: 16px;
    padding-right: 16px; 
    margin-right: 16px; 
}
.form_course {
    background-color: #fff; 
    position: absolute; 
    top: 30px; 
    left: 25%; 
    z-index: 3;
}
.error-course {
    color: red !important; 
    font-size: 1.1rem !important; 
    font-style: italic !important;
}
.form_course {
    display: none;
}
.form-course-title {
    width: 48%; 
}
.form-course-title > div {
    width: 100%; 
}
.a-course-title {
    width: 48% !important; 
}
.a-course-title > div {
    width: 100%; 
}
.a-course-title > div > input {
    width: 100%; 
}
.a-course-title > textarea {
    width: 100%; 
} 
.a-course-title > div > input[type="text"]{
    height: 36px;
    width: 100%; 
    border: 1px solid #d0d0d0; 
    padding-left: 8px; 
    margin-top: 8px; 
    margin-bottom: 16px; 
    outline: none; 
}
.a-course-title > div > input[type="text"]:focus {
    border: 1px solid #50B83C; 
}
.course_radio > img {
    height: 200px;
    width: 100%; 
}
</style>
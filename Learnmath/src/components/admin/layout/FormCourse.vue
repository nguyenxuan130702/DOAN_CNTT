<template>

    <div class="form_course" id="form_course">
        <div class="form_course_title">
            <h1>Thêm mới khóa học</h1>
            <div class="icon_close" v-on:click="closeFormCourse()"></div>
        </div>
        <div class="form_course-content-x">
            <div class="a-course-title">
                <div>
                    <b>Tên khóa học </b><br>
                    <input type="text" placeholder="Tên khóa học" id="tenkh" name="tenkh" v-model="tenkhoahoc">
                </div>
                <div class="course_radio">
                    <b>Loại khóa học </b><br>
                    <input type="radio" name="coursetype" id="1" value="0" v-model="loaiKh" checked/>
                    <label for="1">Đại số</label> <br>
                    <input type="radio" id = "2" name="coursetype" value="1" v-model="loaiKh"/>
                    <label for="2">Hình học</label> <br>
                    <input type="radio" id = "3" name="coursetype" value="2" v-model="loaiKh"/>
                    <label for="3">Đề thi</label> <br>
                </div>
            </div>
            <div class="a-course-title">
                <b>Mô tả </b><br>
                <textarea name="motakh" id="motakh" cols="30" rows="10" placeholder="Mô tả" v-model="mota"></textarea>
            </div>
        </div>
        <div class="course_btn" v-on:click="validatePushCourse()">
            <button>Thêm mới</button>
        </div>
    </div>

    <!--Thông báo thêm mới thành công-->
    <div class="dialog_dangky" id="dialog_themmoicourse" >
        <div class="dialog_dangky_title">
            <div class="icon_thanhcong"></div>
            <p id="dialog_dangky_content">Thêm mới thành công.</p>
        </div>
        <div class="icon_close" v-on:click="closeDialogPushCourse()"></div>
    </div>
</template>
<script>
import BaseRequest from '@/core/BaseRequest';

export default{
    props: ['propCourse'], 
    data(){
        return { 
            tenkhoahoc: "", 
            mota: "", 
            loaiKh: 0, 
            coursePost: {}, 
            indexCode: 0, 
        }
    }, 
    watch: {
        propCourse: function(){
            this.indexCode = this.propCourse.length + 1; 
        }
    },
    methods: {
        closeDialogPushCourse: function(){
            document.getElementById("dialog_themmoicourse").style.opacity = "0";
        }, 
        closeFormCourse: function(){
            document.getElementById("form_course").style.display = "none"; 
            document.getElementById("course_back").style.display = "none"; 
        }, 
        validatePushCourse: function(){
            const _delete = document.querySelectorAll(".error-course"); 
            for (const item of _delete) {
                item.parentNode.removeChild(item);
            }

            const inputTen = document.getElementById("tenkh"); 
            const inputMota = document.getElementById("motakh"); 

            inputTen.style.borderColor = "#d0d0d0"; 
            inputMota.style.borderColor = "#d0d0d0"; 

            var isCheck = true; 

            if(this.tenkhoahoc === null || this.tenkhoahoc === undefined || this.tenkhoahoc === ""){
                this.thongbao(inputTen, "Tên khóa học không được phép để trống."); 
                isCheck = false; 
            }

            if(this.mota === null || this.mota === undefined || this.mota === ""){
                this.thongbao(inputMota, "Nội dung mô tả không được để trống."); 
                isCheck = false; 
            }

            if(isCheck){
                var code = this.indexCode; 
                this.coursePost.courseCode = "C" + code; 

                this.coursePost.courseName = this.tenkhoahoc; 
                this.coursePost.courseContent = this.mota; 
                this.coursePost.courseType = this.loaiKh; 
                
                BaseRequest.post("course", this.coursePost)
                .then(response => {
                    console.log(response.data);
                    document.getElementById("dialog_dangky_content").innerHTML = "Thêm mới khóa học thành công."
                    document.getElementById("form_course").style.display = "none"; 
                    document.getElementById("course_back").style.display = "none"; 
                    document.getElementById("dialog_themmoicourse").style.opacity = "1"; 
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
            p.classList.add("error-course");
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
</style>
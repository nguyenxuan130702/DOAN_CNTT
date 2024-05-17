<template>
    <div class="form_course" id="form-put-videocourse">
        <div class="form_course_title">
            <h1>Sửa thông tin khóa học video</h1>
            <div class="icon_close" v-on:click="closeFormPutCourse()"></div>
        </div>
        <div class="form_course-content-x">
            <div class="form-course-title a-course-title" >
                <div for="">
                    <b>Tên khóa học </b><br>
                    <input type="text" placeholder="tên khóa học" id="u-tenkh-video" name="u-tenkh" v-model="u_tenkhoahoc">
                </div>
                <div for="" class="course_radio">
                    Loại khóa học <br>
                    <img :src="imageUrl" alt="">
                    <input type="file" id="img-uvideo-course" @change="checkFileUploadAdmin" accept="image/*">
                </div>
            </div>
            <div for="" class="a-course-title">
                Mô tả <br>
                <textarea name="u-motakh" id="u-motakh-video" cols="30" rows="10" placeholder="Mô tả" v-model="u_mota"></textarea>
            </div>
        </div>
        <div class="course_btn" v-on:click="validatePutCourse()">
            <button>Cập nhật</button>
        </div>
    </div>
</template>
<script>
import BaseRequest from '@/core/BaseRequest';
//import BaseRequest from '@/core/BaseRequest';
export default {
    props: ['propGetVideo_Course'], 
    data(){
        return {
            u_tenkhoahoc: "", 
            u_loaiKh: 0, 
            u_mota: "",
            listCourse: [],
            video_course: {}, 
            imageUrl: "", 
            file: "", 
        }
    }, 
    watch: {
        propGetVideo_Course: function(){
            this.u_tenkhoahoc = this.propGetVideo_Course.video_CourseTitle; 
            this.u_mota = this.propGetVideo_Course.video_CourseDescription; 
            this.imageUrl = this.propGetVideo_Course.video_CourseImage; 
            this.video_course = this.propGetVideo_Course; 
        }, 
        
    },
    methods: {
        checkFileUploadAdmin: function(event){
            this.file = event.target.files[0]; 
            if(this.file){
                //đọc URL của file và gán vào imageUrl để hiển thị 
                const reander = new FileReader(); 
                reander.onload = (e) => {
                    this.imageUrl = e.target.result; 
                }; 
                reander.readAsDataURL(this.file); 
            }
        }, 
        closeFormPutCourse: function(){
            document.getElementById("video-course_back").style.display = "none";
            document.getElementById("form-put-videocourse").style.display = "none"; 
        }, 
        validatePutCourse: function(){
            const _delete = document.querySelectorAll(".error-ucourse-video"); 
            for (const item of _delete) {
                item.parentNode.removeChild(item);
            }

            const inputTen = document.getElementById("u-tenkh-video"); 
            const inputMota = document.getElementById("u-motakh-video");  

            inputTen.style.borderColor = "#d0d0d0"; 
            inputMota.style.borderColor = "#d0d0d0"; 

            var isCheck = true; 

            if(this.u_tenkhoahoc === null || this.u_tenkhoahoc === undefined || this.u_tenkhoahoc === ""){
                this.thongbao(inputTen, "Tên khóa học không được phép để trống."); 
                isCheck = false; 
            }
            else {
                for (const item of this.listCourse) {
                    if(item.courseName === this.u_tenkhoahoc && this.u_tenkhoahoc !== this.propGetCourse.courseName){
                        this.thongbao(inputTen, "Khóa học đã tồn tại."); 
                        isCheck = false; 
                        break; 
                    }
                }
            }

            if(this.u_mota === null || this.u_mota === undefined || this.u_mota === ""){
                this.thongbao(inputMota, "Nội dung mô tả không được để trống."); 
                isCheck = false; 
            }

            if(isCheck){
                if(this.file === null || this.file === undefined || this.file === ""){
                    this.video_course.video_CourseTitle = this.u_tenkhoahoc; 
                    this.video_course.video_CourseDescription = this.u_mota; 

                    BaseRequest.put("video_course", this.video_course)
                    .then(response => {
                        console.log(response.data); 
                        document.getElementById("dialog_videocourse_content").innerHTML = "Cập nhật thành công."; 
                        document.getElementById("dialog_themmoivideocourse").style.opacity = "1";
                        document.getElementById("form-put-videocourse").style.display = "none"; 
                        document.getElementById("video-course_back").style.display = "none";
                    })
                    .catch(error => {
                        console.log(error.message); 
                    })
                }
                else {
                    const formData = new FormData(); 

                    formData.append("Video_CourseId", this.video_course.video_CourseId); 
                    formData.append("Video_CourseCode", this.video_course.video_CourseCode); 
                    formData.append("Video_CourseTitle", this.u_tenkhoahoc); 
                    formData.append("Video_CourseDescription", this.u_mota); 
                    formData.append("Created_at", this.video_course.created_at); 
                    formData.append("Image", this.file);

                    BaseRequest.put("video_course/update_video", formData)
                    .then(response => {
                        console.log(response.data); 
                        document.getElementById("dialog_videocourse_content").innerHTML = "Cập nhật thành công."; 
                        document.getElementById("dialog_themmoivideocourse").style.opacity = "1";
                        document.getElementById("form-put-videocourse").style.display = "none"; 
                        document.getElementById("video-course_back").style.display = "none";
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
            p.classList.add("error-ucourse-video");
            p.style.color = "red"; 
            p.style.fontStyle = "italic"; 
            p.style.fontSize = "0.9rem"; 
            item.after(p);
            //item.setAttribute("title", mes); 
        },
    }
}
</script>
<style scoped>
#form-put-videocourse {
    display: none; 
}
.form_course {
    z-index: 3;
}
</style>

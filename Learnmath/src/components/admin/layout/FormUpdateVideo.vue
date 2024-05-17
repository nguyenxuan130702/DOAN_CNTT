<template>
    <div class="form_course" id="form_update-video">
        <div class="form_course_title">
            <h1 id="title_user_form">Sửa video bài giảng</h1>
            <div class="icon_close" v-on:click="closeFormUpdateVideo()"></div>
        </div>
        <div class="form_course-content-x">
            <div class="style-image-admin-create form_video-item">
                <div>
                    <b>Khóa học</b> <br>
                    <select name="" id="" v-model="videoCourseId">
                        <option :value="item.video_CourseId" v-for="(item, index) in video_course" :key="index">{{ item.video_CourseTitle }}</option>
                    </select>
                </div>
                <div>
                    <b>Chọn file bài tập</b> <br>
                    <a :href="pdfQuestion" target="_blank">Xem chi tiết</a>
                    <input type="file" @change="checkFileUploadAdmin1" id="file-uquestion">
                </div>
                <div>
                    <b>Chọn file đáp án</b> <br>
                    <a :href="pdfAnswer" target="_blank">Xem chi tiết</a>
                    <input type="file" @change="checkFileUploadAdmin2" id="file-uanswer">
                </div>
            </div>
            <div class="style-info-admin-create form_video-item">
                <div for="">
                    <b>Tiêu đề</b> <br>
                    <input type="text" placeholder="Tiêu đề" id="title-uvideo" name="title-uvideo" v-model="title">
                </div>
                <div>
                    <b>Link video</b> <br>
                    <input type="text" placeholder="Link video" id="link-uvideo" name="link-uvideo" v-model="linkvideo">
                </div>
            </div>
        </div>
        <div class="course_btn" v-on:click="validatePutVideo()">
            <button id="btn-user-form">Cập nhật</button>
        </div>
    </div>
</template>
<script>
import BaseRequest from '@/core/BaseRequest';
export default {
    props: ["propGetVideoUpdate"], 
    data(){
        return {
            title: "", 
            linkvideo: '',
            video: {},
            file1: "", 
            file2: "", 
            pdfQuestion: "", 
            pdfAnswer: "", 
            videoCourseId: "", 
            video_course: [], 
        }
    }, 
    watch: {
        propGetVideoUpdate: function(){
            this.video = this.propGetVideoUpdate; 
            this.title = this.propGetVideoUpdate.videoTitle; 
            this.linkvideo = this.propGetVideoUpdate.videoLink; 
            this.imageUrl = this.propGetVideoUpdate.videoImage; 
            this.pdfQuestion = this.propGetVideoUpdate.videoPdfQuestion; 
            this.pdfAnswer = this.propGetVideoUpdate.videoPdfAnswer; 
            this.videoCourseId = this.propGetVideoUpdate.video_CourseId; 
        }
    }, 
    mounted(){
        BaseRequest.get("video_course")
        .then(response => {
            this.video_course = response.data; 
        })
        .catch(error => {
            console.log(error.message); 
        })
    }, 
    methods: {
        checkFileUploadAdmin1: function(event){
            this.file1 = event.target.files[0]; 
            if(this.file1){
                //đọc URL của file và gán vào imageUrl để hiển thị 
                const reander = new FileReader(); 

                reander.readAsDataURL(this.file1); 
            }
        },
        checkFileUploadAdmin2: function(event){
            this.file2 = event.target.files[0]; 
            if(this.file2){
                //đọc URL của file và gán vào imageUrl để hiển thị 
                const reander = new FileReader(); 

                reander.readAsDataURL(this.file2); 
            }
        },
        closeFormUpdateVideo: function(){
            document.getElementById("form_update-video").style.display = "none"; 
            document.getElementById("video_back").style.display = "none"; 
        }, 
        validatePutVideo: function(){
            const inputTitle = document.getElementById("title-uvideo"); 
            const inputLink = document.getElementById("link-uvideo"); 

            const _delete = document.querySelectorAll(".error-video"); 
            this.isCheck = true;
            for (const item of _delete) {
                item.parentNode.removeChild(item);
            }
            var isCheck = true; 

            inputTitle.style.borderColor = "#d0d0d0"; 
            inputLink.style.borderColor = "#d0d0d0"; 

            if(this.title === "" || this.title === undefined || this.title === null){
                this.thongbao(inputTitle, "Tiêu đề video không được để trống."); 
                isCheck = false; 
            }

            if(this.linkvideo === "" || this.linkvideo === undefined || this.linkvideo === null){
                this.thongbao(inputLink, "Link video không được để trống."); 
                isCheck = false; 
            }

            if(isCheck){

                if(this.file1 === undefined || this.file1 === null || this.file1 === ""){
                    if(this.file2 === undefined || this.file2 === null || this.file2 === ""){
                        //chinh sua goi api ko co image 
                        this.video.videoTitle = this.title; 
                        this.video.videoLink = this.linkvideo; 
                        BaseRequest.put("videos", this.video)
                        .then(response => {
                            console.log(response.data); 
                            document.getElementById("dialog_dangky_content").innerHTML = "Cập nhật thành công."
                            document.getElementById("video_back").style.display = "none";
                            document.getElementById("form_update-video").style.display = "none"; 
                            document.getElementById("dialog_video").style.opacity = "1";
                        })
                        .catch(error => {
                            console.log(error.message); 
                        })
                    }
                    else {
                        const formData = new FormData();

                        formData.append("VideoId", this.video.videoId); 
                        formData.append("VideoCode", this.video.videoCode); 
                        formData.append("VideoTitle", this.title); 
                        formData.append("VideoLink", this.linkvideo); 
                        formData.append("VideoPdfQuestion", this.video.videoPdfQuestion); 
                        formData.append("File_Answer", this.file2); 
                        formData.append("Video_CourseId", this.video.video_CourseId); 
                        formData.append("CreatedDate", this.video.createdDate); 

                        BaseRequest.put("videos/update_file_answer", formData)
                        .then(response => {
                            console.log(response.data); 
                            document.getElementById("dialog_dangky_content").innerHTML = "Cập nhật thành công."
                            document.getElementById("video_back").style.display = "none";
                            document.getElementById("form_update-video").style.display = "none"; 
                            document.getElementById("dialog_video").style.opacity = "1";
                        })
                        .catch(error => {
                            console.log(error.message); 
                        })
                    }
                }
                else {
                    if(this.file2 === undefined || this.file2 === null || this.file2 === ""){

                        const formData = new FormData();

                        formData.append("VideoId", this.video.videoId); 
                        formData.append("VideoCode", this.video.videoCode); 
                        formData.append("VideoTitle", this.title); 
                        formData.append("VideoLink", this.linkvideo); 
                        formData.append("File_Question", this.file1); 
                        formData.append("VideoPdfAnswer", this.video.videoPdfAnswer); 
                        formData.append("Video_CourseId", this.video.video_CourseId); 
                        formData.append("CreatedDate", this.video.createdDate); 

                        BaseRequest.put("videos/update_file_question", formData)
                        .then(response => {
                            console.log(response.data); 
                            document.getElementById("dialog_dangky_content").innerHTML = "Cập nhật thành công."
                            document.getElementById("video_back").style.display = "none";
                            document.getElementById("form_update-video").style.display = "none"; 
                            document.getElementById("dialog_video").style.opacity = "1";
                        })
                        .catch(error => {
                            console.log(error.message); 
                        })
                    }
                    else {
                        //Tiến hành sửa thông tin video. 
                        const formData = new FormData();

                        formData.append("VideoId", this.video.videoId); 
                        formData.append("VideoCode", this.video.videoCode);
                        formData.append("VideoTitle", this.title); 
                        formData.append("VideoLink", this.linkvideo);
                        formData.append("CreatedDate", this.video.createdDate); 
                        formData.append("File_Question", this.file1); 
                        formData.append("File_Answer", this.file2); 
                        formData.append("Video_CourseId", this.video.video_CourseId); 
                        
                        BaseRequest.put("videos/updatefile", formData)
                        .then(response => {
                            console.log(response.data); 
                            document.getElementById("dialog_dangky_content").innerHTML = "Cập nhật thành công."
                            document.getElementById("video_back").style.display = "none";
                            document.getElementById("form_update-video").style.display = "none"; 
                            document.getElementById("dialog_video").style.opacity = "1";
                        })
                        .catch(error => {
                            console.log(error.message); 
                        })
                    }
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
            p.classList.add("error-video");
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
#form_update-video { 
    top: 80px; 
}
.form_video-item {
    width: 48% !important; 
}
#title-uvideo {
    width: 100%; 
    height: 36px;
    border: 1px solid #d0d0d0; 
    border-radius: 4px;
    outline: none;
    padding-left: 8px; 
    margin-top: 8px; 
    margin-bottom: 16px; 
}
#link-uvideo {
    width: 100%; 
    height: 36px;
    border: 1px solid #d0d0d0; 
    border-radius: 4px;
    outline: none;
    padding-left: 8px;
    margin-top: 8px; 
    margin-bottom: 16px;
}
#link-uvideo:focus {
    border: 1px solid #50B83C; 
}
#title-uvideo:focus {
    border: 1px solid #50B83C; 
}
.style-image-admin-create > div {
    width: 100%; 
}
.style-image-admin-create > div > select {
    width: 100%; 
    height: 36px;
    margin-top: 8px; 
    margin-bottom: 24px; 
    border: 1px solid #d0d0d0; 
    outline: none;
}
.style-image-admin-create > div > select:focus {
    border: 1px solid #50B83C; 
}
.style-image-admin-create > div > input {
    margin-top: 8px; 
    margin-bottom: 16px; 
}
</style>

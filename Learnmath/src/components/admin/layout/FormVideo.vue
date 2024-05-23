<template>
    <div class="form_course" id="form_video">
        <div class="form_course_title">
            <h1 id="title_user_form">Thêm mới video bài giảng</h1>
            <div class="icon_close" v-on:click="closeFormVideo()"></div>
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
                    <b>Chọn file bài tập</b>
                    <input type="file" @change="checkFileUploadAdmin1" id="file-question">
                </div>
                <div>
                    <b>Chọn file đáp án</b>
                    <input type="file" @change="checkFileUploadAdmin2" id="file-answer">
                </div>
            </div>
            <div class="style-info-admin-create form_video-item">
                <div for="">
                    <b>Tiêu đề</b> <br>
                    <input type="text" placeholder="Tiêu đề" id="title-video" name="title-video" v-model="title">
                </div>
                <div>
                    <b>Link video</b> <br>
                    <input type="text" placeholder="Link video" id="link-video" name="link-video" v-model="linkvideo">
                </div>
            </div>
        </div>
        <div class="course_btn" v-on:click="validatePushVideo()">
            <button id="btn-user-form">Thêm mới</button>
        </div>
    </div>

    <!--Thông báo thêm mới thành công-->
    <div class="dialog_dangky" id="dialog_video" >
        <div class="dialog_dangky_title">
            <div class="icon_thanhcong"></div>
            <p id="dialog_dangky_content">Thêm mới thành công.</p>
        </div>
        <div class="icon_close" v-on:click="closeDialogPushVideo()"></div>
    </div>
</template>
<script>
import BaseRequest from '@/core/BaseRequest';
export default{
    props: ["propVideo"], 
    data(){
        return {
            imageUrl: null, 
            title: "", 
            linkvideo: "", 
            checkImg: false,
            file1: "",
            file2: "", 
            index: 0,  
            currentDate: "", 
            video_course: [], 
            videoCourseId: "",
        }
    }, 
    mounted(){
        BaseRequest.get("videos")
        .then(response => {
            this.index = response.data.length + 1; 
        })
        .catch(error => {
            console.log(error.message); 
        })

        //Lấy danh sách khóa hoc video 
        BaseRequest.get("video_course")
        .then(response => {
            this.video_course = response.data; 
            this.videoCourseId = this.video_course[0].video_CourseId; 
        })
        .catch(error => {
            console.log(error.message); 
        })
    },
    methods: {
        closeDialogPushVideo: function(){
            document.getElementById("dialog_video").style.opacity = 0; 
        }, 
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
        closeFormVideo: function(){
            document.getElementById("form_video").style.display = "none"; 
            document.getElementById("video_back").style.display = "none"; 
        }, 
        validatePushVideo: function(){
            const inputTitle = document.getElementById("title-video"); 
            const inputLink = document.getElementById("link-video");
            const inputFileQuestion = document.getElementById("file-question"); 
            const inputFileAnswer = document.getElementById("file-answer"); 

            var isCheck = true; 

            const _delete = document.querySelectorAll(".error-video"); 
            this.isCheck = true;
            for (const item of _delete) {
                item.parentNode.removeChild(item);
            }

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

            if(this.file1 === "" || this.file1 === undefined || this.file1 === null){
                this.thongbao(inputFileQuestion, "File câu hỏi không được để trống."); 
                isCheck = false; 
            }

            if(this.file2 === "" || this.file2 === undefined || this.file2 === null){
                this.thongbao(inputFileAnswer, "File đáp án không được để trống."); 
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
                var code = "V" + this.index; 
                const formData = new FormData(); 

                formData.append("VideoCode", code); 
                formData.append("VideoTitle", this.title); 
                formData.append("VideoLink", this.linkvideo); 
                formData.append("File_Question", this.file1); 
                formData.append("File_Answer", this.file2); 
                formData.append("Video_CourseId", this.videoCourseId); 
                formData.append("CreatedDate", this.currentDate); 
                //tiến hành thêm  mới vào csdl 
                
                BaseRequest.post("videos/uploadfile", formData)
                .then(response => {
                    console.log(response.data); 
                    //Tien hanh thong bao them moi thanh cong. 
                    document.getElementById("dialog_dangky_content").innerHTML = "Thêm mới thành công."
                    document.getElementById("form_video").style.display = "none"; 
                    document.getElementById("video_back").style.display = "none"; 
                    document.getElementById("dialog_video").style.display = "1";
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
#dialog_video {
    opacity: 0;
}
.form_video-item > img {
    width: 100%;
    height: 150px;
}
#form_video {
    top: 100px;
}
.form_video-item {
    width: 48% !important; 
}
#title-video {
    width: 100%; 
    height: 36px;
    border: 1px solid #d0d0d0; 
    border-radius: 4px;
    outline: none;
    padding-left: 8px; 
    margin-top: 8px; 
    margin-bottom: 16px; 
}
#link-video {
    width: 100%; 
    height: 36px;
    border: 1px solid #d0d0d0; 
    border-radius: 4px;
    outline: none;
    padding-left: 8px;
    margin-top: 8px; 
    margin-bottom: 16px;
}
#link-video:focus {
    border: 1px solid #50B83C; 
}
#title-video:focus {
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

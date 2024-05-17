<template>
    <div class="form_course form_question" id="form_update_video-question">
        <div class="form_course_title">
            <h1>Sửa thông tin câu hỏi</h1>
            <p id="fixbugq"></p>
            <div class="icon_close" v-on:click="closeFormUpQuestion()"></div>
        </div>
        <div class="form_course-content-x bug_question" style="margin-top: 16px; padding-bottom: 24px;">
            <div class="ad-course-form">
                <div for="" class="replace_text_question">
                    <b>Đề bài</b> <br>
                    <textarea name="u-debai" id="u-vdebai" cols="30" rows="10" v-model="debai" placeholder="Đề bài"></textarea>
                </div>
                <div style="margin-top: 120px;">{{ debai }}</div>
            </div>
            <div class="ad-course-form">
                <div>
                    <b>Đáp án</b> 
                    <input type="text" id="u-vdapan" name="u-vdapan" v-model="dapan" placeholder="đáp án">
                </div>
                <div>
                    <b>Đáp án A</b> 
                    <input type="text" id="u-vdapana" name="u-vdapana" v-model="dapana" placeholder="đáp án A">
                </div>
                <div>
                    <b>Đáp án B</b> 
                    <input type="text" id="u-vdapanb" name="u-vdapanb" v-model="dapanb" placeholder="đáp án B">
                </div>
                <div>
                    <b>Đáp án C</b> 
                    <input type="text" id="u-vdapanc" name="u-vdapanc" v-model="dapanc" placeholder="đáp án C">
                </div>
                <div>
                    <b>Đáp án D</b>
                    <input type="text" id="u-vdapand" name="u-vdapand" v-model="dapand" placeholder="đáp án D">
                </div>
            </div>
            <div class="ad-course-form">
                <div class="ad-course-form-img">
                    <b>Ảnh câu hỏi</b> <br>
                    <img v-show="checkImg1" :src="urlDebai" alt="">
                    <input type="file" id="u-anhcauhoi" name="u-vanhcauhoi" placeholder="Chọn file ảnh câu hỏi" @change="checkFileUploadQuestion1"/>
                </div>
                <div class="ad-course-form-img">
                    <b>Ảnh gợi ý câu trả lời</b> 
                    <img v-show="checkImg2" :src="urlGoiy" alt="">
                    <input type="file" id="u-anhgoiy" name="u-vanhgoiy" placeholder="Chọn file ảnh gợi ý câu trả lời" @change="checkFileUploadQuestion2"/>
                </div>
            </div>
            <div for="" class="course_radio select_lesson" style="margin-top: 16px;">
                <b>Bài học</b> <br>
                <select name="" id="u-voption_lesson" style="margin-top: 8px;" v-model="lessonid">
                    <option value="" disabled>Chọn bài học</option>
                    <option :value="item.videoId" v-for="item in lesson" :key="item.videoId">{{ item.videoTitle }}</option>
                </select>
            </div>
        </div>
        <div class="course_btn small-margin-top-question" v-on:click="validatePutQuestion()">
            <button>Cập nhật</button>
        </div>
    </div>
</template>
<script>
import BaseRequest from '@/core/BaseRequest';
export default{
    props: ['propGetVideoQuestion'], 
    data(){
        return {
            getItem: {}, 
            debai: "", 
            dapan: "", 
            dapana: "", 
            dapanb: "", 
            dapanc: "", 
            dapand: "", 
            question: [], 
            lessonid: "", 
            lesson: [], 
            urlDebai: "", 
            urlGoiy: "", 
            putItem: {}, 
            checkImg1: false, 
            checkImg2: false, 
            file1: "", 
            file2: "", 
        }
    },  
    watch: {
        propGetVideoQuestion: function(){
            this.getItem = this.propGetVideoQuestion; 
            this.debai = this.propGetVideoQuestion.video_QuestionContent, 
            this.dapan = this.propGetVideoQuestion.video_QuestionResult; 
            this.dapana = this.propGetVideoQuestion.video_QuestionAnswerA; 
            this.dapanb = this.propGetVideoQuestion.video_QuestionAnswerB; 
            this.dapanc = this.propGetVideoQuestion.video_QuestionAnswerC; 
            this.dapand = this.propGetVideoQuestion.video_QuestionAnswerD; 
            this.lessonid = this.propGetVideoQuestion.videoId; 
            
            if(this.propGetVideoQuestion.video_QuestionSuggest === null){
                this.checkImg2 = false; 
            }
            else {
                this.urlGoiy = this.propGetVideoQuestion.video_QuestionSuggest; 
                this.checkImg2 = true; 
            }

            if(this.propGetVideoQuestion.video_QuestionImage === null){
                this.checkImg1 = false;
            }
            else {
                this.urlDebai = this.propGetVideoQuestion.video_QuestionImage; 
                this.checkImg1 = true; 
            }

        }
    }, 
    created(){
        BaseRequest.get("videos")
        .then(response => {
            this.lesson = response.data; 
        })
        .catch(error => {
            console.log(error.message); 
        })
    },
    methods: {
        closeFormUpQuestion: function(){
            document.getElementById("form_update_video-question").style.display = "none";
            document.getElementById("video-question_back").style.display = "none";
        }, 
        checkFileUploadQuestion1: function(event){
            this.file1 = event.target.files[0]; 
            if(this.file1){
                //đọc URL của file và gán vào imageUrl để hiển thị 
                const reander = new FileReader(); 
                // reander.onload = (e) => {
                //     this.imageUrl = e.target.result; 
                // }; 
                reander.readAsDataURL(this.file1); 
            }
        }, 
        checkFileUploadQuestion2: function(event){
            this.file2 = event.target.files[0]; 
            if(this.file2){
                //đọc URL của file và gán vào imageUrl để hiển thị 
                const reander = new FileReader(); 
                // reander.onload = (e) => {
                //     this.imageUrl = e.target.result; 
                // }; 
                reander.readAsDataURL(this.file2); 
            }
        }, 
        validatePutQuestion: function(){
            const inputDebai = document.getElementById("u-vdebai"); 
            const inputDapan = document.getElementById("u-vdapan"); 
            const inputDanA = document.getElementById("u-vdapana"); 
            const inputDanB = document.getElementById("u-vdapanb");
            const inputDanC = document.getElementById("u-vdapanc");
            const inputDanD = document.getElementById("u-vdapand");

            const _delete = document.querySelectorAll(".error-videoquestion-u"); 
            for (const item of _delete) {
                item.parentNode.removeChild(item);
            }

            inputDebai.style.borderColor = "#d0d0d0"; 
            inputDapan.style.borderColor = "#d0d0d0";
            inputDanA.style.borderColor = "#d0d0d0";
            inputDanB.style.borderColor = "#d0d0d0";
            inputDanC.style.borderColor = "#d0d0d0";
            inputDanD.style.borderColor = "#d0d0d0";

            var isCheck = true; 

            //validate đề bài câu hỏi 
            if(this.debai === "" || this.debai === undefined || this.debai === ""){
                this.thongbao(inputDebai, "Đề bài không được để trống."); 
                isCheck = false; 
            }

            //validate đáp án câu hỏi 
            if(this.dapan === "" || this.dapan === null || this.dapan === undefined){
                this.thongbao(inputDapan, "Đáp án câu hỏi không được để trống."); 
                isCheck = false; 
            }
            else {
                if(this.dapan.toLowerCase() !== "a" && this.dapan.toLowerCase() !== "b" && this.dapan.toLowerCase() !== "c" && this.dapan.toLowerCase() !== "d"){
                    this.thongbao(inputDapan, "Đáp án không tồn tại.");
                    isCheck = false;  
                }
            }

            //validte đáp án a 
            if(this.dapana === "" || this.dapana === undefined || this.dapana === ""){
                this.thongbao(inputDanA, "Đáp án lựa chọn không được để trống."); 
                isCheck = false; 
            }

            //validate đáp án b 
            if(this.dapanb === "" || this.dapanb === undefined || this.dapanb === ""){
                this.thongbao(inputDanB, "Đáp án lựa chọn không được để trống.");
                isCheck = false;  
            }

            //validate đáp án c 
            if(this.dapanc === "" || this.dapanc === undefined || this.dapanc === ""){
                this.thongbao(inputDanC, "Đáp án lựa chọn không được để trống."); 
                isCheck = false;
            }

            //validate đáp án d 
            if(this.dapand === "" || this.dapand === undefined || this.dapand === ""){
                this.thongbao(inputDanD, "Đáp án lựa chọn không được để trống.");
                isCheck = false;  
            }
            //check xem có thay đổi ảnh của câu hỏi hay không. 
            

            if(isCheck){
                //Tien hanh luu vao api. 
                console.log(this.getItem); 
                if(this.file1 === "" && this.file2 === ""){
                    //Tuc la khong thay doi file anh thi tien hanh luu goi api ko luu anh 
                    this.getItem.video_QuestionContent = this.debai; 
                    this.getItem.video_QuestionResult = this.dapan; 
                    this.getItem.video_QuestionAnswerA = this.dapana; 
                    this.getItem.video_QuestionAnswerB = this.dapanb; 
                    this.getItem.video_QuestionAnswerC = this.dapanc; 
                    this.getItem.video_QuestionAnswerD = this.dapand; 
                    this.getItem.videoId = this.lessonid; 

                    console.log(this.getItem); 

                    //Tien hanh luu 
                    BaseRequest.put("video_question", this.getItem)
                    .then(response => {
                        console.log(response.data); 
                        //Hiển thị thông bao 
                        document.getElementById("form_update_video-question").style.display = "none"; 
                        document.getElementById("video-question_back").style.display = "none"; 
                        document.getElementById("dialog_video-question_content").innerHTML = "Cập nhật thành công."
                        document.getElementById("dialog_video-question").style.opacity = "1"; 
                    })
                    .catch(error => {
                        console.log(error.message); 
                    })
                }
                else {
                    //goi api luu anh. 
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
            p.classList.add("error-videoquestion-u");
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
#form_update_video-question {
    display: none; 
    width: 1400px;
    left: 90px;  
    top: 20px; 
    max-height: 100vh;
}
.ad-course-form-img > img {
    width: 250px; 
    height: 100px;
}
#u-voption_lesson {
    height: 36px;
    width: 100%; 
    margin-top: 16px; 
    border: 1px solid #d0d0d0;
    outline: none; 
}
#u-voption_lesson:focus {
    border: 1px solid #50B83C;  
}
#u-vdebai {
    font-size: 0.9rem; 
    font-style: italic;
}
#u-vdapan {
    font-size: 0.9rem; 
    font-style: italic;
}
#u-vdapana {
    font-size: 0.9rem; 
    font-style: italic;
}
#u-vdapanc {
    font-size: 0.9rem; 
    font-style: italic;
}
#u-vdapand {
    font-size: 0.9rem; 
    font-style: italic; 
}
</style>

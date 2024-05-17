<template>
    <div class="form_course form_question" id="form_video-question">
        <div class="form_course_title">
            <h1>Thêm mới câu hỏi</h1>
            <div class="icon_close" v-on:click="closeFormQuestion()"></div>
        </div>
        <div class="form_course-content-x bug_question" style="margin-top: 16px; padding-bottom: 24px;">
            <div class="ad-course-form">
                <div for="" class="replace_text_question">
                    <b>Đề bài</b> <br>
                    <textarea name="v-debai" id="v-debai" cols="30" rows="10" v-model="debai" placeholder="Đề bài"></textarea>
                </div>
                <div style="margin-top: 120px;">{{ debai }}</div>
            </div>
            <div class="ad-course-form">
                <div>
                    <b>Đáp án </b>
                    <input type="text" id="v-dapan" name="v-dapan" v-model="dapan" placeholder="Đáp án">
                </div>
                <div>
                    <b>Đáp án A </b>
                    <input type="text" id="v-dapana" name="v-dapana" v-model="dapana" placeholder="Đáp án A">
                </div>
                <div>
                    <b>Đáp án B </b>
                    <input type="text" id="v-dapanb" name="v-dapanb" v-model="dapanb" placeholder="Đáp án B">
                </div>
                <div>
                    <b>Đáp án C </b>
                    <input type="text" id="v-dapanc" name="v-dapanc" v-model="dapanc" placeholder="Đáp án C">
                </div>
                <div>
                    <b>Đáp án D</b>
                    <input type="text" id="v-dapand" name="v-dapand" v-model="dapand" placeholder="Đáp án D">
                </div>
            </div>
            <div class="ad-course-form">
                <div>
                    <b>Ảnh câu hỏi </b>
                    <input type="file" id="v-anhcauhoi" name="v-anhcauhoi" placeholder="Chọn file ảnh câu hỏi" @change="checkFileUploadQuestion1"/>
                </div>
                <div>
                    <b>Ảnh gợi ý câu trả lời</b> 
                    <input type="file" id="v-anhgoiy" name="v-anhgoiy" placeholder="Chọn file ảnh gợi ý câu trả lời" @change="checkFileUploadQuestion2"/>
                </div>
            </div>
            <div for="" class="course_radio select_lesson" style="margin-top: 16px;">
                <b>Bài học</b> <br>
                <select name="" id="v-option_lesson" style="margin-top: 8px;" v-model="lessonid">
                    <option :value="item.videoId" v-for="item in lesson" :key="item.videoId">{{ item.videoTitle }}</option>
                </select>
            </div>
        </div>
        <div class="course_btn small-margin-top-question" v-on:click="validatePushQuestion()">
            <button>Thêm mới</button>
        </div>
    </div>

    <!--Thông báo thêm mới thành công-->
    <div class="dialog_dangky" id="dialog_video-question" >
        <div class="dialog_dangky_title">
            <div class="icon_thanhcong"></div>
            <p id="dialog_video-question_content">Thêm mới thành công.</p>
        </div>
        <div class="icon_close" v-on:click="closeDialogPushQuestion()"></div>
    </div>
</template>
<script>
import BaseRequest from '@/core/BaseRequest';
export default{
    props: ['propVideoQuestion'], 
    data(){
        return {
            macauhoi: '', 
            debai: '', 
            dapan: '', 
            dapana:'', 
            dapanb: '', 
            dapanc: '', 
            dapand: '', 
            lesson: [], 
            lessonid: '',
            index: 0, 
            question_new: {},
            file1: '', 
            file2: '', 
            file3: '',
            getItem: {},   
        }
    }, 
    mounted(){
        BaseRequest.get("videos")
        .then(response => {
            this.lesson = response.data; 
            this.lessonid = this.lesson[0].videoId; 
        })
        .catch(error => {
            console.log(error.message);
        })
    }, 
    watch: {
        propVideoQuestion: function(){
            this.index = this.propVideoQuestion.length + 1; 
        }
    }, 
    methods: { 
        closeDialogPushQuestion: function(){
            document.getElementById("dialog_video-question").style.opacity = "0"; 
        },
        validatePushQuestion: function(){
            const inputDebai = document.getElementById("v-debai"); 
            const inputDapan = document.getElementById("v-dapan"); 
            const inputDanA = document.getElementById("v-dapana"); 
            const inputDanB = document.getElementById("v-dapanb");
            const inputDanC = document.getElementById("v-dapanc");
            const inputDanD = document.getElementById("v-dapand");

            const _delete = document.querySelectorAll(".error-video-question"); 
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

            if(isCheck){
                if(this.file1 === "" && this.file2 === ""){
                    //Tiến hành thêm mới vào api. database. 
                    this.index = this.propVideoQuestion.length + 1; 
                    var code = "VQ" + this.index; 
                    this.getItem.video_QuestionCode = code; 
                    this.getItem.video_QuestionContent = this.debai; 
                    this.getItem.video_QuestionImage = null; 
                    this.getItem.video_QuestionAnswerA = this.dapana; 
                    this.getItem.video_QuestionAnswerB = this.dapanb; 
                    this.getItem.video_QuestionAnswerC = this.dapanc; 
                    this.getItem.video_QuestionAnswerD = this.dapand;
                    this.getItem.video_QuestionResult = this.dapan; 
                    this.getItem.videoId = this.lessonid; 
                    this.getItem.video_QuestionSuggest = null; 
                    console.log(this.getItem); 
                    BaseRequest.post("video_question", this.getItem)
                    .then(response => {
                        console.log(response.data); 
                        //Hiển thị thông báo thêm mới thành công. 
                        document.getElementById("form_video-question").style.display = "none"; 
                        document.getElementById("video-question_back").style.display = "none"; 
                        document.getElementById("dialog_video-question_content").innerHTML = "Thêm mới thành công."
                        document.getElementById("dialog_video-question").style.opacity = "1"; 
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
            p.classList.add("error-video-question");
            p.style.color = "red"; 
            p.style.fontStyle = "italic"; 
            p.style.fontSize = "0.9rem"; 
            item.after(p);
            //item.setAttribute("title", mes); 
        },
        closeFormQuestion: function(){
            document.getElementById("video-question_back").style.display = "none"; 
            document.getElementById("form_video-question").style.display = "none";
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
    }
}
</script>
<style>
#form_video-question {
    display: none; 
}
#v-debai {
    font-size: 0.9rem; 
    font-style: italic;
}
#v-dapan {
    font-size: 0.9rem; 
    font-style: italic;
}
#v-dapana {
    font-size: 0.9rem; 
    font-style: italic;
}
#v-dapanc {
    font-size: 0.9rem; 
    font-style: italic;
}
#v-dapand {
    font-size: 0.9rem; 
    font-style: italic;
    margin-bottom: 24px; 
}
#form_video-question {
    width: 1400px;
    left: 90px;  
    top: 20px; 
    max-height: 100vh;
}
.replace_text_question {
    height: 36px;
    margin-bottom: 20px; 
}
.replace_text_question > textarea {
    height: 100%;
}
#v-option_lesson {
    height: 36px;
    width: 100%; 
    margin-top: 16px; 
    border: 1px solid #d0d0d0; 
}
.form_course-content-x > div {
    width: 30%; 
}
.bug_question {
    flex-wrap: wrap;
}
.bug_question > div {
    width: 22%;
}
.ad-course-form > div {
    width: 100%; 
}
.ad-course-form > div > input{
    width: 100%; 
    height: 36px;
    border: 1px solid #d0d0d0; 
    outline: none; 
    padding-left: 8px; 
    margin-top: 8px; 
}
.ad-course-form > div > textarea {
    width: 100%; 
    height: 100px; 
    border: 1px solid #d0d0d0; 
    outline: none; 
    padding-left: 8px; 
    margin-top: 8px; 
}
.ad-course-form > div > textarea:focus {
    border: 1px solid #50B83C; 
}
.ad-course-form > div > input:focus {
    border: 1px solid #50B83C; 
}
.ad-course-form > div {
    margin-top: 16px; 
}
#v-option_lesson {
    outline: none; 
}
#v-option_lesson:focus {
    border: 1px solid #50B83C; 
}
.small-margin-top-question {
    margin-top: -24px; 
}
</style>

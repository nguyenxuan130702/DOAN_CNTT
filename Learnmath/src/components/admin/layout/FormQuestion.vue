<template>
    <div class="form_course form_question" id="form_question">
        <div class="form_course_title">
            <h1>Thêm mới câu hỏi</h1>
            <div class="icon_close" v-on:click="closeFormQuestion()"></div>
        </div>
        <div class="form_course-content-x bug_question" style="margin-top: 16px; padding-bottom: 24px;">
            <div class="ad-course-form">
                <div for="" class="replace_text_question">
                    <b>Đề bài</b> <br>
                    <textarea name="debai" id="debai" cols="30" rows="10" v-model="debai" placeholder="Đề bài"></textarea>
                </div>
                <div style="margin-top: 120px;">{{ debai }}</div>
            </div>
            <div class="ad-course-form">
                <div>
                    <b>Đáp án </b>
                    <input type="text" id="dapan" name="dapan" v-model="dapan" placeholder="Đáp án">
                </div>
                <div>
                    <b>Đáp án A </b>
                    <input type="text" id="dapana" name="dapana" v-model="dapana" placeholder="Đáp án A">
                </div>
                <div>
                    <b>Đáp án B </b>
                    <input type="text" id="dapanb" name="dapanb" v-model="dapanb" placeholder="Đáp án B">
                </div>
                <div>
                    <b>Đáp án C </b>
                    <input type="text" id="dapanc" name="dapanc" v-model="dapanc" placeholder="Đáp án C">
                </div>
                <div>
                    <b>Đáp án D</b>
                    <input type="text" id="dapand" name="dapand" v-model="dapand" placeholder="Đáp án D">
                </div>
            </div>
            <div class="ad-course-form">
                <div>
                    <b>Ảnh câu hỏi </b>
                    <input type="file" id="anhcauhoi" name="anhcauhoi" placeholder="Chọn file ảnh câu hỏi" @change="checkFileUploadQuestion1"/>
                </div>
                <div>
                    <b>Ảnh gợi ý câu trả lời</b> 
                    <input type="file" id="anhgoiy" name="anhgoiy" placeholder="Chọn file ảnh gợi ý câu trả lời" @change="checkFileUploadQuestion2"/>
                </div>
                <div>
                    <b>Ảnh đáp án (Nếu có)</b>
                    <input type="file" id="anhdapan" name="anhdapan" @change="checkFileUploadQuestion3">
                </div>
            </div>
            <div for="" class="course_radio select_lesson" style="margin-top: 16px;">
                <b>Bài học</b> <br>
                <select name="" id="option_lesson" style="margin-top: 8px;" v-model="lessonid">
                    <option value="" disabled>Chọn bài học</option>
                    <option :value="item.lessonId" v-for="item in lesson" :key="item.lessonId">{{ item.lessonName }}</option>
                </select>
            </div>
        </div>
        <div class="course_btn small-margin-top-question" v-on:click="validatePushQuestion()">
            <button>Thêm mới</button>
        </div>
    </div>

    <!--Thông báo thêm mới thành công-->
    <div class="dialog_dangky" id="dialog_question" >
        <div class="dialog_dangky_title">
            <div class="icon_thanhcong"></div>
            <p id="dialog_dangky_content">Thêm mới thành công.</p>
        </div>
        <div class="icon_close" v-on:click="closeDialogPushQuestion()"></div>
    </div>
</template>
<script>
import BaseRequest from '@/core/BaseRequest';
export default{
    props: ['propQuestion'], 
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
        }
    }, 
    mounted(){
        BaseRequest.get("lesson")
        .then(response => {
            this.lesson = response.data; 
            this.lessonid = this.lesson[0].lessonId; 
        })
        .catch(error => {
            console.log(error.message);
        })
    }, 
    watch: {
        propQuestion: function(){
            this.index = this.propQuestion.length + 1; 
        }
    }, 
    methods: { 
        closeDialogPushQuestion: function(){
            document.getElementById("dialog_question").style.opacity = "0"; 
        },
        validatePushQuestion: function(){
            const inputDebai = document.getElementById("debai"); 
            const inputDapan = document.getElementById("dapan"); 
            const inputDanA = document.getElementById("dapana"); 
            const inputDanB = document.getElementById("dapanb");
            const inputDanC = document.getElementById("dapanc");
            const inputDanD = document.getElementById("dapand");

            const _delete = document.querySelectorAll(".error-lesson"); 
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
                this.index = this.propQuestion.length + 1; 
                var code = "Q" + this.index; 
                const formData = new FormData(); 
                formData.append("QuestionCode", code);
                formData.append("QuestionContent", this.debai);
                formData.append("QuestionResult", this.dapan); 
                formData.append("AnswerA", this.dapana);
                formData.append("AnswerB", this.dapanb); 
                formData.append("AnswerC", this.dapanc); 
                formData.append("AnswerD", this.dapand);
                formData.append("LessonId", this.lessonid); 
                formData.append("ImageQuestion", this.file1);
                formData.append("QuestionSuggest", this.file2); 
                formData.append("ResultImage", this.file3);

                BaseRequest.post("question/PostImage", formData)
                .then(response => {
                    console.log(response.data); 
                    document.getElementById("dialog_dangky_content").innerHTML = "Thêm mới thành công"; 
                    document.getElementById("dialog_question").style.opacity = "1"; 
                    document.getElementById("form_question").style.display = "none";
                    document.getElementById("question_back").style.display = "none";  
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
            p.classList.add("error-lesson");
            p.style.color = "red"; 
            p.style.fontStyle = "italic"; 
            p.style.fontSize = "0.9rem"; 
            item.after(p);
            //item.setAttribute("title", mes); 
        },
        closeFormQuestion: function(){
            document.getElementById("question_back").style.display = "none"; 
            document.getElementById("form_question").style.display = "none";
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
        checkFileUploadQuestion3: function(event){
            this.file3 = event.target.files[0]; 
            if(this.file3){
                //đọc URL của file và gán vào imageUrl để hiển thị 
                const reander = new FileReader(); 
                // reander.onload = (e) => {
                //     this.imageUrl = e.target.result; 
                // }; 
                reander.readAsDataURL(this.file3); 
            }
        }
    }
}
</script>
<style>
#debai {
    font-size: 0.9rem; 
    font-style: italic;
}
#dapan {
    font-size: 0.9rem; 
    font-style: italic;
}
#dapana {
    font-size: 0.9rem; 
    font-style: italic;
}
#dapanc {
    font-size: 0.9rem; 
    font-style: italic;
}
#dapand {
    font-size: 0.9rem; 
    font-style: italic;
}
#form_question {
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
#option_lesson {
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
#option_lesson {
    outline: none; 
}
#option_lesson:focus {
    border: 1px solid #50B83C; 
}
.small-margin-top-question {
    margin-top: -24px; 
}
</style>

<template>
    <div class="form_course form_question" id="form_update_question">
        <div class="form_course_title">
            <h1>Sửa thông tin câu hỏi</h1>
            <p id="fixbugq"></p>
            <div class="icon_close" v-on:click="closeFormUpQuestion()"></div>
        </div>
        <div class="form_course-content-x bug_question" style="margin-top: 16px; padding-bottom: 24px;">
            <div class="ad-course-form">
                <div for="" class="replace_text_question">
                    <b>Đề bài</b> <br>
                    <textarea name="u-debai" id="u-debai" cols="30" rows="10" v-model="debai" placeholder="Đề bài"></textarea>
                </div>
                <div style="margin-top: 120px;">{{ debai }}</div>
            </div>
            <div class="ad-course-form">
                <div>
                    <b>Đáp án</b> 
                    <input type="text" id="u-dapan" name="u-dapan" v-model="dapan" placeholder="đáp án">
                </div>
                <div>
                    <b>Đáp án A</b> 
                    <input type="text" id="u-dapana" name="u-dapana" v-model="dapana" placeholder="đáp án A">
                </div>
                <div>
                    <b>Đáp án B</b> 
                    <input type="text" id="u-dapanb" name="u-dapanb" v-model="dapanb" placeholder="đáp án B">
                </div>
                <div>
                    <b>Đáp án C</b> 
                    <input type="text" id="u-dapanc" name="u-dapanc" v-model="dapanc" placeholder="đáp án C">
                </div>
                <div>
                    <b>Đáp án D</b>
                    <input type="text" id="u-dapand" name="u-dapand" v-model="dapand" placeholder="đáp án D">
                </div>
            </div>
            <div class="ad-course-form">
                <div class="ad-course-form-img">
                    <b>Ảnh câu hỏi</b> <br>
                    <img :src="urlDebai" alt="">
                    <input type="file" id="u-anhcauhoi" name="u-anhcauhoi" placeholder="Chọn file ảnh câu hỏi" @change="checkFileUploadQuestion1"/>
                </div>
                <div class="ad-course-form-img">
                    <b>Ảnh gợi ý câu trả lời</b> 
                    <img :src="urlGoiy" alt="">
                    <input type="file" id="u-anhgoiy" name="u-anhgoiy" placeholder="Chọn file ảnh gợi ý câu trả lời" @change="checkFileUploadQuestion2"/>
                </div>
                <div class="ad-course-form-img">
                    <b>Ảnh đáp án (Nếu có)</b>
                    <img :src="urlDapan" alt="">
                    <input type="file" id="u-anhdapan" name="u-anhdapan" @change="checkFileUploadQuestion3">
                </div>
            </div>
            <div for="" class="course_radio select_lesson" style="margin-top: 16px;">
                <b>Bài học</b> <br>
                <select name="" id="u-option_lesson" style="margin-top: 8px;" v-model="lessonid">
                    <option value="" disabled>Chọn bài học</option>
                    <option :value="item.lessonId" v-for="item in lesson" :key="item.lessonId">{{ item.lessonName }}</option>
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
    props: ['propQuestion', 'propGetQuestion'], 
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
            urlDapan: "", 
            putItem: {}, 
        }
    },  
    watch: {
        propQuestion: function(){
            this.question = this.propQuestion; 
        }, 
        propGetQuestion: function(){
            this.getItem = this.propGetQuestion; 
            this.debai = this.propGetQuestion.questionContent, 
            this.dapan = this.propGetQuestion.questionResult; 
            this.dapana = this.propGetQuestion.answerA; 
            this.dapanb = this.propGetQuestion.answerB; 
            this.dapanc = this.propGetQuestion.answerC; 
            this.dapand = this.propGetQuestion.answerD; 
            this.lessonid = this.propGetQuestion.lessonId; 
            
            if(this.propGetQuestion.resultImage === null){
                this.urlDapan = "https://png.pngtree.com/png-vector/20190820/ourmid/pngtree-no-image-vector-illustration-isolated-png-image_1694547.jpg"; 
            }
            else {
                this.urlDapan = this.propGetQuestion.resultImage; 
            }

            if(this.propGetQuestion.questionImage === null){
                this.urlDebai = "https://png.pngtree.com/png-vector/20190820/ourmid/pngtree-no-image-vector-illustration-isolated-png-image_1694547.jpg"; 
            }
            else {
                this.urlDebai = this.propGetQuestion.questionImage; 
            }

            if(this.propGetQuestion.questionSuggest === null){
                this.urlGoiy = "https://png.pngtree.com/png-vector/20190820/ourmid/pngtree-no-image-vector-illustration-isolated-png-image_1694547.jpg"; 
            }
            else {
                this.urlGoiy = this.propGetQuestion.questionSuggest; 
            }

        }
    }, 
    created(){
        BaseRequest.get("lesson")
        .then(response => {
            this.lesson = response.data; 
        })
        .catch(error => {
            console.log(error.message); 
        })
    },
    methods: {
        closeFormUpQuestion: function(){
            document.getElementById("form_update_question").style.display = "none";
            document.getElementById("question_back").style.display = "none";
        }, 
        validatePutQuestion: function(){
            const inputDebai = document.getElementById("u-debai"); 
            const inputDapan = document.getElementById("u-dapan"); 
            const inputDanA = document.getElementById("u-dapana"); 
            const inputDanB = document.getElementById("u-dapanb");
            const inputDanC = document.getElementById("u-dapanc");
            const inputDanD = document.getElementById("u-dapand");

            const _delete = document.querySelectorAll(".error-question-u"); 
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
            var check = true; 
            if(this.file1 === null || this.file1 === "" || this.file1 === undefined){
                check = false; 
            }
            if(this.file2 === null || this.file2 === "" || this.file2 === undefined){
                check = false; 
            }
            if(this.file3 === null || this.file3 === "" || this.file3 === undefined){
                check = false; 
            }

            if(isCheck){
                if(!check){
                    this.putItem.questionContent = this.debai; 
                    this.putItem.questionResult = this.dapan; 
                    this.putItem.answerA = this.dapana; 
                    this.putItem.answerB = this.dapanb; 
                    this.putItem.answerC = this.dapanc; 
                    this.putItem.answerD = this.dapand; 
                    this.putItem.lessonId = this.lessonid; 
                    this.putItem.questionId = this.propGetQuestion.questionId; 
                    this.putItem.questionCode = this.propGetQuestion.questionCode; 
                    this.putItem.questionImage = this.propGetQuestion.questionImage; 
                    this.putItem.questionSuggest = this.propGetQuestion.questionSuggest; 
                    this.putItem.resultImage = this.propGetQuestion.resultImage; 
                    BaseRequest.put("question", this.putItem)
                    .then(response => {
                        console.log(response.data); 
                        document.getElementById("dialog_dangky_content").innerHTML = "Cập nhật thành công";
                        document.getElementById("dialog_question").style.opacity = "1"; 
                        document.getElementById("form_update_question").style.display = "none";
                        document.getElementById("question_back").style.display = "none"; 
                    })
                    .catch(error => {
                        console.log(error.message); 
                    })
                }
                else {
                    const formData = new FormData(); 
                    formData.append("QuestionId", this.propGetQuestion.questionId); 
                    formData.append("QuestionCode", this.propGetQuestion.questionCode);
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

                    //document.getElementById("fixbugq").innerHTML = this.lessonid; 
                    BaseRequest.post("question/PostImage", formData)
                    .then(response => {
                        console.log(response.data); 
                        document.getElementById("dialog_question").style.opacity = "1"; 
                        document.getElementById("form_question").style.display = "none";
                        document.getElementById("question_back").style.display = "none";  
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
            p.classList.add("error-question-u");
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
#form_update_question {
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
#u-option_lesson {
    height: 36px;
    width: 100%; 
    margin-top: 16px; 
    border: 1px solid #d0d0d0; 
}
#u-option_lesson:focus {
    border: 1px solid #50B83C;  
}
</style>

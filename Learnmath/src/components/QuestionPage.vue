<template>
    <div>
        <div class="question" >
        <div class="menu_question">
            <p>{{ course.courseName }}</p>
            <p v-on:click="goBacktoLesson()">{{ lessonName }}</p>
        </div>
        <div class="question_content">
            <h2>Câu hỏi </h2>
            <p id="questionContent">{{ question.questionContent }}</p>
            <img :src="question.questionImage" alt=""/>
            <div class="question_answer">
                <div class="question_answer_sty">
                    <label for="" class="style_radio">
                        <input type="radio" id="a" name="dapan" value="A"/> 
                        <label for="a" id="answerA">
                            {{ question.answerA }}
                        </label>
                    </label>
                    <label for="" class="style_radio">
                        <input type="radio" id="b" name="dapan" value="B"/>
                        <label for="b" id="answerB">
                            {{ question.answerB }}
                        </label>
                    </label>
                    <label for="" class="style_radio">
                        <input type="radio" id="c" name="dapan" value="C"/>
                        <label for="c" id="answerC">
                            {{ question.answerC }}
                        </label>
                    </label>
                    <label for="" class="style_radio">
                        <input type="radio" id="d" name="dapan" value="D"/>
                        <label for="d" id="answerD">
                            {{ question.answerD }}
                        </label>
                    </label>
                </div>
            </div>
            <div class="check_answer" v-show="check_answer">{{ thongbao }}</div>
            <div v-show="check_answer" class="suggest_answer">
                Gợi ý trả lời.
                <img :src="question.questionSuggest" alt = "">
            </div>
            <div class="check-answer" v-on:click="checkAnswer(question.questionResult, question.questionId)"> 
                <div class="check-answer-content" lesson>Kiểm tra đáp án</div>
                <div class="check-answer-back"></div>
            </div>
        </div>
    </div>

    <div class="thongbao_answer" v-show="openthongbao">
        <div class="answer_back"></div>
        <div class="answer_font">
            <div class="answer_content">
                <div class="answer_fixbug">
                    <div class="answer_title">
                        <div></div>
                        <h3>Chính xác.</h3>
                        <div class="icon_close_answer" v-on:click="closeTBAnswer()"></div>
                    </div>
                </div>
                <div class="answer_main">
                    <div class="icon_corract"></div>
                    <h3>Hàm số mũ và hàm số logarit.</h3>
                    <div class="ketqua">
                        <div class="hoanthanh" id="hoanthanh"></div>
                        <div class="chuaht" id="chuaht"></div>
                    </div>
                    <p>Hoàn thành {{ this.cautraloidung }}%</p>
                </div>
            </div>
            <div class="answer_button">
                <div class="check-answer" v-on:click="nextQuestion()"> 
                    <div class="check-answer-content" lesson>Câu hỏi tiếp theo</div>
                    <div class="check-answer-back"></div>
                </div>
            </div>
        </div>
    </div>
    </div>
</template>
<script>
import BaseRequest from '@/core/BaseRequest';

export default {
    data(){
        return{
            thongbao: "", 
            lessionQuestion: "", 
            openthongbao: false, 
            question: {}, 
            course: {}, 
            element: "", 
            check_answer: false,
            cautraloidung: 0, 
            phantram: 0, 
            userLogin: JSON.parse(localStorage.getItem("userLogin")), 
            sa_code: "A", 
            i: 0,
            lessonName: "", 
            postAnswer: {}, 
        }
    },  
    created(){
        this.details(); 
    }, 

    mounted(){
        this.$nextTick(() => {
            this.renderMath();
            if(window.MathJax){
                window.MathJax.typeset(); 
            }
        })
    }, 
    watch: {
        //Khi dữ liệu thay đổi thì gọi lại hàm render MathJax. 
        element: function(){
            this.$nextTick(() => {
                this.renderMath();
                if(window.MathJax){
                    window.MathJax.typeset(); 
                }
            }); 
            //lấy ra thoong tin của số câu hỏi sau đó xóa luôn cái local. 
        }, 
    }, 
    methods: {
        details:function(){
            this.question = JSON.parse(localStorage.getItem("question")); 
            this.element = this.question.questionContent; 
            this.course = JSON.parse(localStorage.getItem("course")); 
            this.lessionQuestion = JSON.parse(localStorage.getItem("listquestion"));

            console.log(this.lessionQuestion); 
            localStorage.removeItem("question");
            this.cautraloidung = JSON.parse(localStorage.getItem("socautraloidung")); 
            this.phantram = (this.cautraloidung/this.lessionQuestion.length)*100; 

            this.lessonName = JSON.parse(localStorage.getItem("lessonName")); 
            
        },
        checkAnswer: function(item, questionIdNow){
            var radios = document.getElementsByName("dapan"); 
            var kq = ""; 
            for(var i = 0; i<radios.length; i++){
                if(radios[i].checked){
                    kq = radios[i].value; 
                    break; 
                }
            }

            if(kq === item){
                this.cautraloidung = this.cautraloidung + 1; 
                this.phantram = (this.cautraloidung/this.lessionQuestion.length)*100;
                //ý tưởng là luu cái giá trị hiện tại vào local sao đó lên cái watch để lấy ra giá trị local sau đó là hiển thị 
                //lấy xong thì hiển thị giá trị. 
                this.openthongbao = true;  
                console.log(this.userLogin);  
                //Nếu không đăng nhập tài khoản thì bỏ qua bước lưu vào database
                if(this.userLogin !== undefined && this.userLogin !== null){
                    this.postAnswer.studentAnswerCode = "string"; 
                    this.postAnswer.userId = this.userLogin.userId;
                    this.postAnswer.questionId = questionIdNow; 
                    this.postAnswer.answer = item; 
                    this.postAnswer.checked = "true"; 
                    console.log(this.postAnswer); 
                    BaseRequest.post("student_answer", this.postAnswer)
                    .then(response => {
                        console.log(response.data); 
                    })
                    .catch(error => {
                        console.log(error); 
                    })
                }
            }
            else {
                this.thongbao = "Xin lỗi, câu trả lời của bạn không chính xác."; 
                this.check_answer = true; 
            }
        }, 
        closeTBAnswer: function(){
            this.openthongbao = false; 
        }, 
        renderMath() {
            const output1 = document.getElementById("questionContent"); 
            const output2 = document.getElementById("answerA"); 
            const output3 = document.getElementById("answerB"); 
            const output4 = document.getElementById("answerC"); 
            const output5 = document.getElementById("answerD")
            //this.$refs.mathjax.innerHTML = this.formula;
            if (this.$MathJax) {
                this.$MathJax.typesetPromise([output1])
                .catch((err) => console.log('Error rendering MathJax:', err));
                this.$MathJax.typesetPromise([output2])
                .catch((err) => console.log('Error rendering MathJax:', err)); 
                this.$MathJax.typesetPromise([output3])
                .catch((err) => console.log('Error rendering MathJax:', err)); 
                this.$MathJax.typesetPromise([output4])
                .catch((err) => console.log('Error rendering MathJax:', err)); 
                this.$MathJax.typesetPromise([output5])
                .catch((err) => console.log('Error rendering MathJax:', err)); 
            }
        }, 

        nextQuestion: function(){
            this.check_answer = false; 
            //Lấy ra index của câu hỏi đang hiển thị hiện tại
            var index; 
            for (var i = 0; i<this.lessionQuestion.length; i++) {
                if(this.lessionQuestion[i].questionCode === this.question.questionCode){
                    index = i; 
                }
            }

            //Nếu như đó là câu hỏi cuối cùng thì sẽ hiển thị về trang các bài học của khóa học. 
            if(index === this.lessionQuestion.length - 1){
                this.$router.push("/Lesson/" + this.course.courseId); 
            }
            else {
                BaseRequest.get("question/code?code="+this.lessionQuestion[index+1].questionCode)
                .then((response)=>{
                    localStorage.setItem("question", JSON.stringify(response.data));
                    //console.log(response.data); 
                    this.details(); 
                    this.openthongbao = false; 
                    this.$router.push("/Question/" + this.lessionQuestion[index+1].questionCode);  
                })
                .catch((error) => {
                    if(error.response.status === 442){
                        this.$router.push({ name: "404" });
                    }
                })
            }
        }, 
        goBacktoLesson: function(){
            this.$router.push('/Lesson/' + this.course.courseId); 
        }, 
    }, 
}
</script>
<style scoped>
.check-answer {
    width: 100%; 
    height: 56px;
    border: 3px solid #d0d0d0; 
    position: relative; 
    margin-top: 24px; 
}
.check-answer-content {
    height: 100%;
    width: 100%; 
    display: flex;
    justify-content: center;
    align-items: center;
    font-size: 1.5rem; 
    color: #fff; 
    z-index: 2;
}
.check-answer-back {
    height: 100%;
    width: 100%; 
    background: #d0d0d0; 
    opacity: 0.3;
    position: absolute; 
    top: 0;
    left: 0; 
}
.check-answer:hover .check-answer-content{
    color: #0a0a23; 
}
.check-answer:hover .check-answer-back{
    opacity: 1;
    cursor: pointer;
}
body {
    position: relative; 
}
.thongbao_answer {
    width: 100%;  
}
.icon_close_answer {
    background: url('@/assets/Sprites.64af8f61.svg') no-repeat -147px -147px;
	width: 18px;
	height: 18px;
}
.answer_back {
    width: 100%; 
    height: 100vh;
    background-color:#0a0a23; 
    opacity: 0.3;
    z-index: 2; 
    top: 0; 
    position: fixed
}
.answer_content {
    text-align: center;
}
.answer_title {
    width: 90%; 
    display: flex; 
    justify-content: space-between; 
    align-items: center;
    margin: auto; 
}
.answer_fixbug {
    width: 100%;
    border-bottom: 1px solid #fff;
}
.answer_title > h3 {
    font-size: 2.3rem; 
    font-weight: 700;
    padding-top: 20px; 
    padding-bottom: 20px; 
}
.answer_button {
    padding-top: 15px; 
}
.ketqua {
    height: 10px;
    width: 60%; 
    display: flex; 
    margin: auto; 
    margin-top: 10px; 
    margin-bottom: 10px; 
}
.hoanthanh {
    height: 100%;
    width: calc(100% - 90%); 
    background-color: #fff;
}
.chuaht {
    height: 100%;
    width: 90%;
    background-color: #cdcdce;
    opacity: 0.3;
}
.answer_main {
    width: 100%; 
    display: flex; 
    flex-direction: column;
    text-align: center;
    border-bottom: 1px solid #fff; 
}
.answer_main > h3 {
    margin-top: 100px; 
}
.icon_corract {
    background: url('@/assets/iconlesson/icon_tick.svg') no-repeat 0 0;
	width: 20px;
	height: 20px;
    transform: scale(8);
    margin-left: 450px; 
    margin-top: 100px; 
}
.answer_button {
    width: calc(100% - 48px); 
    margin: auto; 
}
.answer_font {
    width: 60%; 
    height: calc(100vh - 200px);
    margin: auto; 
    border: 1px solid #fff; 
    background-color: #1b1b32;
    color: #fff; 
    position: fixed; 
    top: 56px; 
    z-index: 3;
    left: 300px; 

}
.question {
    width: 100%; 
    background-color: #1b1b32;
    padding-top: 100px; 
    color: #fff; 
    padding-bottom: 40px;
    min-height: 100vh; 
}
.menu_question {
    width: 100%; 
    display: flex; 
    justify-content: space-between;
    align-items: center;
    height: 30px;
    text-align: center;
    position: fixed; 
    top: 56px; 
    left: 0; 
    right: 0;
    z-index: 2;  
    background-color: #1b1b32; 
    border: 1px solid #cdcdce; 
}
.menu_question > p {
    width: 50%; 
    display: flex; 
    align-items: center; 
    padding-left: 16px; 
    height: 100%;
    margin: auto; 
    justify-content: center;
}
.menu_question > p:first-child{
    background-color: #cdcdce; 
    color: #0a0a23;
    
}
.menu_question > p:hover {
    cursor: pointer;
}
.question_content {
    width: 60%; 
    margin: auto; 
    margin-top: 80px;
    color: #fff;  
}
.question_content > h2 {
    font-size: 1.75rem; 
    font-weight: 700;
    text-align: center;
}
.question_content > p {
    font-size: 1.15rem; 
    display: flex;
    gap: 10px; 
    align-items: center;
    flex-wrap: wrap;
}
.question_content > img {
    margin-bottom: 20px; 
}
.question_answer {
    width: 100%; 

}
.style_radio {
    width: 100%;
    display: flex; 
    gap: 20px; 
    align-items: center;
    font-size: 1.15rem; 
}
.style_radio > input {
    vertical-align: middle;
}
input[type="radio"]{
    display: none; 
}
input[type="radio"] + label::before{
    content: "";
    display: inline-block;
    width: 20px; /* Độ rộng của nút radio */
    height: 20px; /* Độ cao của nút radio */
    border: 3px solid #fff; /* Độ dày và màu sắc của viền */
    border-radius: 50%; /* Hình dạng của nút radio */
    margin-right: 8px; 
    margin-top: 20px; 
}
input[type="radio"]:checked + label::before {
  background-color: #fff; /* Màu nền khi nút radio được chọn */
}
label {
  cursor: pointer; /* Biến con trỏ thành dấu chỉ tay khi di chuột qua */
}

.style_radio {
    width: 100%; 
    border: 4px solid #232336; 
    border-bottom: none; 
    height: 60px;
    padding-left: 26px; 
    background-color: #0a0a23; 
    display: flex; 
    flex-direction: row;
}
.style_radio:last-child {
    border-bottom: 4px solid #232336; 
}
.style_radio > label {
    display: flex; 
    flex-direction: row;
}
.check_answer {
    padding-top: 20px; 
    padding-bottom: 20px; 
    font-size: 1.25rem; 
}
.suggest_answer {
    display: flex;
    flex-direction: column;
    margin-top: 20px; 
    margin-bottom: 20px; 
    font-size: 1.25rem; 
}
.button_start {
    margin-top: 40px;
}
</style>

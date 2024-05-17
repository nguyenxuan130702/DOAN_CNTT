<template>
    <div class="exam-page">
        <div class="exam-page-title">
            <h1>Thi Thử Đề Thi THPT Quốc Gia</h1>
            <div>{{ lesson.lessonName }}</div>
            <div>Người làm bài: {{ userLogin.userName }}</div>
            <div v-show="showCheck" style="font-size: 1.25rem; font-weight: 700;">Số câu đúng: {{ returnTraLoiDung() }}</div>
            <div v-show="showCheck" style="font-size: 1.25rem; font-weight: 700;">Điểm thi: {{ renderNumber() }}</div>
            <!-- <div v-show="showCheck">Thời gian làm bài: {{ totalTime }}</div> -->
        </div>
        <div>
            <div class="exam-page-content">
                <div class="exam-page-main">
                    <div class="exam-page-question" v-for="(item, index) in question" :key="index" :id="item.questionCode" v-bind:class="{ 'hover_check': !check_answer[index]}">
                        <div>Câu số {{index + 1}}: 
                            <span v-show="showCheck">
                                <span v-if="check_answer[index]" style="color: black; font-size: 1.25rem; color: green;">(Đúng)</span> <span style="color: black; font-size: 1.25rem; color: red;" v-else>(Sai)</span>
                            </span>
                        </div>
                        <div class= "ex-question" id="ex-question">{{ item.questionContent }}</div>
                        <img :src="item.questionImage" alt="">
                        <div class="exam-page-question-answer">
                            <div>
                                <input type="radio" value="A" :name="item.questionCode" v-model="select[index]" v-on:click="selectAnswer(index)"/>
                                <label for="" id="ex-answerA">{{ item.answerA }} </label>
                            </div>
                            <div>
                                <input type="radio" value="B" :name="item.questionCode" v-model="select[index]" v-on:click="selectAnswer(index)"/>
                                <label for="" id="ex-answerB">{{ item.answerB }}</label>
                            </div>
                            <div>
                                <input type="radio" value="C" :name="item.questionCode" v-model="select[index]" v-on:click="selectAnswer(index)"/>
                                <label for="" id="ex-answerC">{{ item.answerC }} </label>
                            </div>
                            <div>
                                <input type="radio" value="D" :name="item.questionCode" v-model="select[index]" v-on:click="selectAnswer(index)"/>
                                <label for="" id="ex-answerD">{{ item.answerD }}</label>
                            </div>
                        </div>

                        <div v-show="showCheck">
                            <div style="font-style: italic; ">Đáp án đúng: {{ item.questionResult }}</div>
                        </div>
                    </div>
                </div>
                <div class="exam-page-content-check" id="fixedElement">
                    <div class="exam-page-content-title">
                        <div class="exam-question-time">
                            <div>{{ question.length }} câu hỏi</div>
                            <div>90 phút</div>
                        </div>
                        <div class="exam-question-button">
                            <button id="time">{{ time }}</button>
                        </div>
                    </div>
                    <div class="dapan-exam">Đáp án</div>
                    <div class="exam-page-select">
                        <div class="answer-select-item" v-for="(item, index) in question" :key="index" v-bind:class="{'done-answer': done_answer[index], 'fail-answer': fail_answer[index]}">
                            <a :href="'#' + item.questionCode">{{ index + 1 }}</a>
                        </div>
                    </div>
                    <div class="exam-submit" v-on:click="submitExam()" v-show="exam_submit">
                        <button>Nộp bài</button> 
                    </div>
                </div>
            </div>
        </div>
    </div>


    <!--Dialog thông báo-->
    <div class="ad-dialog-delete" id="ad0-dialog-nopbai-exam">
        <div class="ad-dialog">
            <div class="ad-dialog-main">
                <div class="ad-dialog-title">
                    <div id="dialog-title">Thông báo</div>
                    <div class="icon_close" v-on:click="closeDialogDelete()"></div>
                </div>
                <div class="ad-dialog-content">
                    <div class="icon_cauhoi"></div>
                    <div id="thognbao">Bạn vẫn còn câu hỏi chưa trả lời. Bạn có chắc chắn muốn nộp bài?</div>
                </div>
            </div>
            <div class="ad-dialog-btn" id="ad-dialog-btn-exam">
                <button>Đồng ý</button>
            </div>
        </div>

        <div id="ad-dialog-exam-back"></div>
    </div>
</template>
<script>
import BaseRequest from '@/core/BaseRequest';
export default {
    data(){
        return {
            time: '00:00', 
            secondsRemaining: 300*18, 
            question: [], 
            answer: [], 
            lesson: {},
            select: [],  
            check_answer: [], 
            showCheck: false, 
            diem: 0, 
            score_user: {}, 
            index: 0, 
            userLogin: {}, 
            currentDate: '', 
            traloidung: 0, 
            done_answer: [], 
            option: false, 
            //Biến này để xử lý khi nộp bài thì nó sẽ hiển thị lỗi background-color: red. 
            fail_answer: [], 
            exam_submit: true, 
            totalTime: "", 
        }; 
    }, 
    mounted(){
        this.time = setInterval(this.updateTime, 1000); 
        this.question = JSON.parse(localStorage.getItem("listquestion")); 

        //Luu dap an dung vao 1 mang. 
        for (const item of this.question) {
            this.answer.push(item.questionResult); 
        }

        for(var j = 0; j<this.question.length; j++){
            this.check_answer[j] = true; 
            this.done_answer[j] = false; 
            this.fail_answer[j] = false; 
        }


        this.lesson = JSON.parse(localStorage.getItem("getLesson")); 
        this.$nextTick(() => {
            this.renderMath();
            if(window.MathJax){
                window.MathJax.typeset(); 
            }
        }); 

        //Lay tổng số kêt quả 
        BaseRequest.get("score")
        .then(response => {
            this.index = response.data.length + 1;
        })
        .catch(error => {
            console.log(error.message); 
        })

        //Lay thong tin nguoi lam bai 
        this.userLogin = JSON.parse(localStorage.getItem("userLogin")); 

        //Cuộn trang cố định thời gian 
        window.addEventListener('scroll', function() {
            var element = document.getElementById('fixedElement');
            var threshold = 260; // Vị trí mà bạn muốn cố định phần tử
            var currentPosition = window.pageYOffset || document.documentElement.scrollTop;

            if (element !== null) {
                if (currentPosition >= threshold) {
                    element.classList.add('fixed');
                } else {
                    element.classList.remove('fixed');
                }
            }
        });

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
        
        closeDialogDelete: function(){
            document.getElementById("ad0-dialog-nopbai-exam").style.display = "none"; 
        }, 
        updateTime: function(){
            if(this.secondsRemaining === 300){
                alert("Thời gian làm bài của bạn chỉ còn lại 5 phút.");
            }
            if(this.secondsRemaining > 0){
                let minutes = Math.floor(this.secondsRemaining/60); 
                let seconds = this.secondsRemaining % 60;
                this.time = `${minutes < 10 ? '0' : ''}${minutes}:${seconds < 10 ? '0' : ''}${seconds}`; 
                this.secondsRemaining--; 
            }
            else {
                //Sau khi het thoi gian lam bai
                clearInterval(this.time); 
            }
        }, 
        beforeDestroy() {
            clearInterval(this.time); // Dừng bộ đếm khi component bị hủy
        },
        renderMath() {
            const output1 = document.getElementById("ex-question"); 
            const output2 = document.getElementById("ex-answerA"); 
            const output3 = document.getElementById("ex-answerB"); 
            const output4 = document.getElementById("ex-answerC"); 
            const output5 = document.getElementById("ex-answerD"); 
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
        submitExam: function(){
            var fail = 0; 
            //Hiển thị thông báo nếu như vẫn chưa làm hết các câu hỏi. 
            for (const item of this.done_answer) {
                if(!item){
                    fail++;  
                }
            }

            if(fail > 0){
                document.getElementById("ad0-dialog-nopbai-exam").style.display = "block"; 
                document.getElementById("ad-dialog-btn-exam").addEventListener("click", ()=>{
                    var dem = 0;
                    this.showCheck = true; 
                    for(var i = 0; i<this.answer.length; i++){
                        if(this.answer[i] === this.select[i]){
                            this.check_answer[i] = true; 
                            document.getElementById(`${this.question[i].questionCode}`).style.borderColor = "green"; 
                            dem++; 
                        }
                        else {
                            this.check_answer[i] = false; 
                            this.fail_answer[i] = true; 
                        }
                    }
                    this.traloidung = dem; 
                    clearInterval(this.time);
                    document.getElementById("time").style.display = "none"; 
                    this.diem = (10/this.question.length)*dem; 
                    this.exam_submit = false; 
                    

                    window.scrollTo({
                        top: 0,
                        behavior: "smooth" // Cuộn mượt
                    });

                    //Luu ket qua thi thu cua hoc sinh vao bang Score 


                    // const diff = 300*18 - this.secondsRemaining; 
                    // const seconds = Math.floor(diff/1000); 
                    // const minutes = Math.floor(seconds/60); 

                    // this.totalTime = `${minutes < 10 ? '0' : ''}${minutes}:${seconds < 10 ? '0' : ''}${seconds}`; 
                    //setInterval(this.updateTime, 1000);
                    //Lấy ngày giờ hôm nay 
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
                    var code = "S" + this.index; 
                    this.score_user.scoreCode = code; 
                    this.score_user.result = this.diem.toFixed(2);  
                    this.score_user.time = this.currentDate; 
                    this.score_user.userId = this.userLogin.userId; 
                    this.score_user.lessonId = this.lesson.lessonId; 

                    // BaseRequest.post("score", this.score_user)
                    // .then(response => {
                    //     console.log(response.data); 
                    // })
                    // .catch(error => {
                    //     console.log(error.message); 
                    // })
                })
            }
            else {
                var dem = 0;
                this.showCheck = true; 
                for(var i = 0; i<this.answer.length; i++){
                    if(this.answer[i] === this.select[i]){
                        this.check_answer[i] = true; 
                        document.getElementById(`${this.question[i].questionCode}`).style.borderColor = "green"; 
                        dem++; 
                    }
                    else {
                        this.check_answer[i] = false; 
                        this.fail_answer[i] = true; 
                    }
                }
                this.traloidung = dem; 
                clearInterval(this.time);
                document.getElementById("time").style.display = "none"; 
                this.diem = (10/this.question.length)*dem; 
                this.exam_submit = false; 

                // const diff = 5400 - this.secondsRemaining; 
                // const seconds = Math.floor(diff/1000); 
                // const minutes = Math.floor(seconds/60); 

                // this.totalTime = `${minutes < 10 ? '0' : ''}${minutes}:${seconds < 10 ? '0' : ''}${seconds}`;

                window.scrollTo({
                    top: 0,
                    behavior: "smooth" // Cuộn mượt
                });

                //Luu ket qua thi thu cua hoc sinh vao bang Score 


                //Lấy ngày giờ hôm nay 
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
                var code = "S" + this.index; 
                this.score_user.scoreCode = code; 
                this.score_user.result = this.diem.toFixed(2);  
                this.score_user.time = this.currentDate; 
                this.score_user.userId = this.userLogin.userId; 
                this.score_user.lessonId = this.lesson.lessonId; 

                // BaseRequest.post("score", this.score_user)
                // .then(response => {
                //     console.log(response.data); 
                // })
                // .catch(error => {
                //     console.log(error.message); 
                // })
            }
        }, 
        renderNumber: function(){
            return this.diem.toFixed(2); 
        }, 
        selectAnswer: function(index){
            this.done_answer[index] = true; 
            console.log(index); 
        }, 
        returnTraLoiDung: function(){
            return this.traloidung; 
        }
    },
}
</script>
<style scoped>
#ad0-dialog-nopbai-exam {
    display: none; 
    position: fixed; 
    top: 0; 
    z-index: 2;
    left: 0; 
    right: 0; 
    bottom: 0; 
}
#ad-dialog-exam-back{
    position: fixed;
    top: 0; 
    bottom: 0; 
    left: 0; 
    right: 0; 
    background-color: #0a0a23; 
    opacity: 0.3;
}
.ad-dialog {
    left: 30%; 
}
.exam-submit {
    width: 100%;
    text-align: center; 
    margin-bottom: 24px; 
}
.exam-submit > button {
    height: 36px;
    width: 200px;
    border-radius: 4px;
    background-color: #f1be32 !important;
    box-shadow: -10px -10px 15px rgba(0, 0, 0, 0.1),
    10px 10px 15px rgba(0, 0, 0, 0.1);
    border: none;
}
.exam-page {
    height: auto;
}
.exam-page-question {
    background-color: #fff; 
    padding: 24px; 
    border-top: 5px solid #0a0a23;
    margin-bottom: 16px; 
    width: 100%; 
}
.exam-page-question > div:first-child {
    font-weight: 600;
}
.exam-page-question-answer {
    display: flex; 
    flex-direction: column;
    margin-top: 16px; 
}
.exam-page-question-answer > div > label {
    padding-left: 16px; 
}
.ex-question {
    display: flex; 
    align-items: center;
    flex-wrap: wrap;
    gap: 8px; 
}
.hover_check {
    border-color: red; 
}
.exam-page-content-title {
    width: 100%; 
    padding-top: 8px; 
    padding-bottom: 8px; 
    padding-left: 8px; 
    padding-right: 8px; 
    background-color: #0a0a23; 
    z-index: 1;
    display: flex;
    justify-content: space-between;
    margin-top: 0px; 
    color: #fff; 
    border-top-left-radius: 6px;
    border-top-right-radius: 6px;
}
.exam-page-content-check {
    width: 28%; 
    background: #fff;
    display: inline-block;
    vertical-align: top;
    margin-left: 24px;  
    border-radius: 6px;
}
.exam-question-time {
    width: 60%; 
}
.exam-question-button {
    width: 40%; 
    display: flex; 
    justify-content: flex-end;
}
.fixed {
    position: fixed;
    width: 24%; 
    top: 66px;
    /* top: 30px;
    left: 20%;
    width: 60%;  */
    /* Các thuộc tính CSS khác nếu cần */
}
.exam-page-content {
    /* display: flex; */
    width: 85%; 
    margin-top: 24px; 
    /* justify-content: space-between; */
}
.exam-page-main {
    width: 70%; 
    display: inline-block;
    z-index: 1;
    vertical-align: top;
}
.exam-page-select {
    width: 100%;
    padding-left: 8px; 
    padding-right: 8px; 
    display: flex;
    flex-wrap: wrap;
    gap: 8px; 
    padding-bottom: 24px; 
}
.answer-select-item {
    width: 50px; 
    height: 50px;
    border: 1px solid #0a0a23; 
    border-radius: 50%;
    display: flex;
    justify-content: center;
    align-items: center;
}
.answer-select-item > a {
    text-decoration: none;
    color: black; 
}
.dapan-exam {
    text-align: center;
    width: 100%;
    margin-top: 16px; 
    margin-bottom: 16px; 
    font-size: 1.75rem; 
}
.done-answer {
    background-color: green; 
}
.fail-answer {
    background-color: red; 
}
</style>

<template>
    <div class="lesson">
        <div class="lesson_style">
            <h1>{{ course.courseName }}</h1>
            <div class="icon_lesson"></div>
            <p class="lesson_content">{{ course.courseContent }}</p>
            <h1 class="course_style">Bài học</h1>
            <div class="lesson_course">
                <div class="o-lesson-coure-q" v-for="(item, index) in lesson" :key="index" v-on:click="openLesson(item.lessonId, index, item.lessonName, item)">
                    <div class="o-lesson-course">
                        <div class="o-lesson-title">
                            <div class="icon_untick"></div>
                            <div class="icon_tick" v-show="false"></div>
                            <h2>{{ item.lessonName }}</h2>
                        </div>
                        <div class="o-lesson-menu">
                            <div class="icon_down"></div>
                            <div class="icon_up"></div>
                        </div>
                        <div class="o-leson-back"></div>
                    </div>
                    <div class="o-less_question" v-show="lessonquestion[index]">
                        <div class="o-less_question-sty">
                            <p>{{ item.lessonContent }}</p>
                            <div class="button_start">
                                <a :href="item.lessonPdf" target="_blank">Tài liệu tham khảo</a>
                            </div>
                            <div class="button_start" v-on:click="openQuestion(question[index][0].questionCode)"> 
                                <div class="button_save_content" lesson>Bắt đầu</div>
                            </div>
                            <div class="lesson_question" v-show="checkExam">
                                <div v-for="(item, index1) in question[index]" :key="index1" class="question_back" v-on:click="openQuestion(item.questionCode)" v-bind:class="{question_after_done: checked[index][index1]}">
                                    {{ index1 + 1 }}
                                    <div class="question_back_font"></div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <ChatBot></ChatBot>
</template>
<script>
import BaseRequest from '@/core/BaseRequest';
import ChatBot from './mathjax/ChatBox.vue'
export default {
    name: 'LessonPage',
    components:{
        ChatBot
    }, 
    data(){
        return {
            lessonquestion: [], 
            lesson: [], 
            question: [], 
            course: {},  
            userLogin: {},
            questionUser: [], 
            checked: Array.from({length: 100}, () => Array(3).fill(false)),  
            checkExam: true, //mang nay dung de an cac cau hoi khi no la de thi. 
            score: [], 
            getLesson: {}, 
            ketqua: [], 
        }
    }, 
    mounted(){
        this.loadData(); 
    }, 
    methods: { 
        loadData(){
            this.course = JSON.parse(localStorage.getItem("course")); 

            //Kiem tra xem no co phai la de thi khong. Neu là de thi thi an so luong cau hoi. 
            if(this.course.courseType === 3){
                this.checkExam = false; 
            }
            //console.log(this.bug); 
            BaseRequest.get("lesson/course?id="+this.$route.params.id)
            .then(response => {
                this.lesson = response.data; 
                this.lessonquestion = this.lessonquestion.map(() => false);
            })
            .catch(error => {
                console.log("Đã xảy ra lỗi: " + error.message); 
            })

            //kiểm tra xem đã đăng nhập chưa. 
            this.userLogin = JSON.parse(localStorage.getItem("userLogin"));
            //Nếu chưa đăng nhập thì mảng câu hỏi đã làm của người dùng trả về null 
            if(this.userLogin === null || this.userLogin === undefined ){
                this.questionUser = null; 
                localStorage.setItem("socautraloidung", JSON.stringify(0)); 
            }
            else {
                //Nếu đã đăng nhập thì mảng câu hỏi đã làm sẽ được lấy từ API rồi gán vào JSON chiều dài của mảng. 
                BaseRequest.get("student_answer/byuser?userId=" + this.userLogin.userId)
                .then(response => {
                    this.questionUser = response.data; 
                    localStorage.setItem("socautraloidung", JSON.stringify(this.questionUser.length)); 
                    console.log(this.questionUser); 
                })
                .catch(error => {
                    console.log(error.message); 
                })
            }


            
        }, 
        openLesson: function(id, index, name, item){
            localStorage.setItem("getLesson", JSON.stringify(item)); 

            this.getLesson = item; 
            localStorage.setItem("lessonName", JSON.stringify(name)); 
            BaseRequest.get("question/lessonId?id=" + id)
            .then(response => {
                this.question[index] = response.data; 
                localStorage.setItem("listquestion", JSON.stringify(this.question[index]));
                this.lessonquestion[index] = !this.lessonquestion[index]; 
                //console.log(this.question[index]); 
                if(this.questionUser !== null){
                    var x = 0; 
                    for (const item1 of this.question[index]) {
                        for (const item2 of this.questionUser) {
                            if(item1.questionId === item2.questionId){
                                this.checked[index][x] = true;  
                            }
                        }
                        x++; 
                    }
                }
    
            })
        }, 
        openQuestion: function(item){
            if(this.course.courseType === 3){
                if(this.userLogin === null || this.userLogin === undefined || this.userLogin === ""){
                    this.$router.push("/Login"); 
                }
                else {
                    BaseRequest.get("score/lesson_user?userId=" + this.userLogin.userId + "&lessonId=" + this.getLesson.lessonId)
                    .then(response => {
                        this.ketqua = response.data; 
                        console.log(this.ketqua); 
                        localStorage.setItem("ketqua", JSON.stringify(this.ketqua));
                        
                        if(this.ketqua.length === 0){
                            this.$router.push("/ExamPage");
                        }
                        else {
                            this.$router.push("/ScorePage"); 
                        }
                    }) 
                }
            }
            else {
                BaseRequest.get("question/code?code="+item)
                .then((response)=>{
                    localStorage.setItem("question", JSON.stringify(response.data));
                    console.log(this.question); 
                    this.$router.push("/Question/" + item); 
                    })
                .catch((error) => {
                    if(error.response.status === 442){
                        this.$router.push({ name: "404" });
                    }
                    })
            }
        }, 
        startQuestion: function(){

        }
    }
}
</script>
<style>
.button_start > a {
    color: #fff; 
    font-size: 1.25rem;
}
.lesson {
    padding-top: 150px; 
    background-color: #1b1b32; 
    color: #fff; 
    padding-bottom: 40px; 
}
.lesson_style {
    width: 60%; 
    margin: auto; 
}
.icon_lesson {
    background: url('@/assets/math/microscope-svgrepo-com.svg') no-repeat -1px 0;
	width: 62px;
	height: 64px;
    margin: auto; 
    transform: scale(1.4);
    margin-top: 40px; 
}
.lesson_content {
    font-size: 1.25rem; 
    white-space: pre-wrap;
    margin-top: 80px; 
}
.course_style {
    margin-top: 100px; 
    text-align: center;
    margin-bottom: 40px; 
}
.o-lesson-course {
    height: 80px;
    display: flex; 
    justify-content: space-between;
    background-color: #0a0a23; 
    color: #fff; 
    align-items: center;
    margin-bottom: 3px; 
    position: relative; 
}
.icon_untick {
    background: url('@/assets/iconlesson/circle-svgrepo-com.svg') no-repeat 0 0;
	width: 20px;
	height: 20px;
    margin-left: 24px; 
}
.o-lesson-title {
    display: flex; 
    height: 100%;
    align-items: center;
    gap: 16px; 
}
.o-lesson-title > h2 {
    font-size: 1.5rem; 
    font-weight: 700;   
}
.icon_tick {
    background: url('@/assets/iconlesson/icon_tick.svg') no-repeat 0 0;
	width: 20px;
	height: 20px;
}
.icon_down {
    background: url('@/assets/iconlesson/211687_down_arrow_icon.svg') no-repeat -9px -37px;
	width: 113px;
	height: 66px; 
    margin-top: 50px; 
    margin-right: -80px; 
}
.o-lesson-menu {
    width: 100px; 
    height: 100%;
    display: flex; 
    justify-content: center;
    align-items: center; 
}
.lesson_style > h1 {
    font-size: 2.5rem;
    font-weight: 700;
    text-align: center;
}
.lesson_style > p {
    text-align: center;
}
.o-less_question {
    background-color: #0a0a23; 
    margin-bottom: 10px; 

}
.o-less_question-sty {
    width: calc(100% - 50px); 
    margin: auto; 
    padding-top: 20px; 
    padding-bottom: 20px; 
}
.o-less_question-sty > p{
    font-size: 1.25rem; 
}
.button_start {
    width: 200px; 
    height: 50px; 
    margin-left: 0px; 
    position: relative; 
}
.button_start_back {
    width: 100%; 
    height: 46px;
    background-color: #1b1b32;
    opacity: 0.3; 
    z-index: 1; 
    position: absolute; 
    top: 0; 
    left: 0; 
}
.lesson_question {
    display: flex; 
    flex-wrap: wrap;
    gap: 16px; 
    justify-content: space-between;
    margin-top: 40px; 
    padding-bottom: 40px; 
}
.question_back {
    width: 80px; 
    height: 80px;
    border: 1px dashed #cdcdce; 
    display: flex; 
    justify-content: center;
    align-items: center;
    position: relative; 
}
.o-lesson-coure-q {
    margin-bottom: 10px; 
}
.question_back_font{
    position: absolute;
    width: 100%;
    height: 100%;
    background-color: #cdcdce; 
    opacity: 0.3;
    display: none; 
    transition: opacity 3s ease; 
}
.question_back:hover > .question_back_font {
    display: block;
    cursor: pointer;
}
.o-leson-back {
    position: absolute;
    width: 100%;
    height: 100%;
    background-color: #cdcdce; 
    opacity: 0.3; 
    display: none; 
    top: 0; 
    left: 0; 
    right: 0; 
}
.button_save_content {
    border: 3px solid #d0d0d0; 
    height: 56px;
    font-size: 1.25rem; 
    display: flex; 
    justify-content: center;
    align-items: center;
    background-color: #1b1b32; 
    opacity: 0.6;
}
.button_save_content:hover {
    cursor: pointer;
    opacity: 1;
}
.o-lesson-coure-q :hover > .o-leson-back{
    cursor: pointer;
    display: block; 
}
.question_after_done {
    width: 80px; 
    height: 80px;
    border: 1px solid #cdcdce; 
    display: flex; 
    background-color: blue;
    justify-content: center;
    align-items: center;
    position: relative; 
}
.lesson_course {
    padding-bottom: 100px; 
}
</style>

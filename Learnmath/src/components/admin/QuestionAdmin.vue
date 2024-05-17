<template>
    <Navbar></Navbar>
    <Sidebar></Sidebar>
    <FormQuestion :propQuestion="question"></FormQuestion>
    <FormUpdateQuestion :propQuestion="question" :propGetQuestion="getQuestion"></FormUpdateQuestion>
    <DetailQuestion :propGetQuestion="getQuestion"></DetailQuestion>
    <ImportExcel></ImportExcel>
    <div class="a-course">
        <h1>
            <router-link to="/MenuQuestion" class="menu-question">Quản lý câu hỏi</router-link> / 
            <router-link to="/QuestionAdmin" class="menu-question">Câu hỏi luyện tập</router-link>
        </h1>
        <div class="a-course_table fix-height-question">
            <div class="a-course_table-title">
                <div class="a-course_table-title-search">
                    <input type="text" placeholder="Tìm kiếm theo mã, chủ đề khóa học" id="search-question">
                    <div class="search-course" v-on:click="searchQuestion()">
                        <div class="icon_search"></div>
                    </div>
                </div>
                <div class="a-course_table-title-btn">
                    <button v-on:click="openFormPostQuestion()">Thêm mới</button>
                    <div class="icon_loaddata_sty" v-on:click="importQuestion()">
                        <div class="icon_excel"></div>
                    </div>
                    <div class="icon_loaddata_sty">
                        <div class="icon_loaddata" v-on:click="loadDataQuestion()"></div>
                    </div>
                </div>
            </div>
            <div class="a-course_table-content buggg_in">
                <table>
                    <thead>
                        <tr>
                            <td style="width: 80px;">Mã câu hỏi</td>
                            <td style="width: 200px">Đề bài</td>
                            <td style="width: 80px">Đáp án đúng</td>
                            <td style="width: 150px;">Đáp án A</td>
                            <td style="width: 150px;">Đáp án B</td>
                            <td style="width: 150px;">Đáp án C</td>
                            <td style="width: 150px;">Đáp án D</td>
                            <td>Bài học</td>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-for="item in question" :key="item.questionId">
                            <td style="width: 80px; text-align: center;">{{ item.questionCode }}</td>
                            <td style="width: 200px">{{ item.questionContent }}</td>
                            <td style="width: 80px;">{{ item.questionResult }}</td>
                            <td style="width: 150px;">{{ item.answerA }}</td>
                            <td style="width: 150px;">{{ item.answerB }}</td>
                            <td style="width: 150px;">{{ item.answerC }}</td>
                            <td style="width: 150px;">{{ item.answerD }}</td>
                            <td class="chucnang_course">{{ item.lessonName }}
                                <div class="chucnang_course-style">
                                    <div class="seehover" title="Xem chi tiết" v-on:click="seeDetailQuestion(item)">
                                        <div class="icon_zoom"></div>
                                    </div>
                                    <div class="updatehover" title="Sửa khóa học" v-on:click="openFormPutQuestion(item)">
                                        <div class="icon_update"></div>
                                    </div>
                                    <div class="deletehover" title="Xóa khóa học" v-on:click="deleteQuestion(item)">
                                        <div class="icon_delete"></div>
                                    </div>
                                </div>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <div class="a-course_table-page bugggg">
                <div style="margin-left: 24px;">
                    Tổng: {{ question.length }}
                </div>
                <div style="margin-right: 24px" class="a-course_table-page1">
                    <div>Số bản ghi/trang: 
                        <select name="" id="" v-model="pageSize">
                            <option value="10">10</option>
                            <option value="20">20</option>
                            <option value="30">30</option>
                        </select>
                    </div>
                    <div class="icon_back" v-on:click="backPageQuestion()"></div>
                    <div class="icon_next" v-on:click="nextPageQuestion()"></div>
                </div>
            </div>
        </div>
    </div>
    <div class="course_back" id="question_back"></div>

    <CanhBao></CanhBao>
</template>
<script>
import BaseRequest from '@/core/BaseRequest'
import FormQuestion from './layout/FormQuestion.vue'
import Navbar from './layout/NavbarAdmin.vue'
import Sidebar from './layout/SidebarAdmin.vue'
import FormUpdateQuestion from './layout/FormUpdateQuestion.vue'
import DetailQuestion from './layout/DetailQuestion.vue'
import CanhBao from './layout/DialogCanhbao.vue'
import ImportExcel from './excel/ImportQuestion.vue'
export default{
    components: {
        FormQuestion, 
        Navbar, 
        Sidebar, 
        FormUpdateQuestion, 
        DetailQuestion, 
        CanhBao, 
        ImportExcel
    }, 
    data(){
        return{
            question: [],
            getQuestion: {},
            tg: [],  
            pageSize: 10,
            page: 1, 
            student_answer: [], 
            answer_code: [], 
        }
    }, 
    created(){
        BaseRequest.get("viewquestionlesson/page?page=" + this.page + "&pageSize=" + this.pageSize)
        .then(response => {
            this.question = response.data; 
        })
        .catch(error => {
            console.log(error.message); 
        })
    }, 
    watch: {
        pageSize: function(){
            BaseRequest.get("viewquestionlesson/page?page=" + this.page + "&pageSize=" + this.pageSize)
            .then(response => {
                this.question = response.data; 
            })
            .catch(error => {
                console.log(error.message); 
            })
        }
    }, 
    methods: {
        importQuestion: function(){
            document.getElementById("import-excel").style.display = "block"; 
            document.getElementById("question_back").style.display = "block"; 
        }, 
        nextPageQuestion: function(){
            this.page++; 
            BaseRequest.get("viewquestionlesson/page?page=" + this.page + "&pageSize=" + this.pageSize)
            .then(response => {
                this.question = response.data; 
            })
            .catch(error => {
                console.log(error.message); 
            })
        }, 
        backPageQuestion: function(){
            this.page--; 
            BaseRequest.get("viewquestionlesson/page?page=" + this.page + "&pageSize=" + this.pageSize)
            .then(response => {
                this.question = response.data; 
            })
            .catch(error => {
                console.log(error.message); 
            })
        }, 
        loadDataQuestion: function(){
            BaseRequest.get("viewquestionlesson")
            .then(response => {
                this.question = response.data; 
            })
            .catch(error => {
                console.log(error.message); 
            })
        },
        openFormPostQuestion: function(){
            document.getElementById("question_back").style.display = "block"; 
            document.getElementById("form_question").style.display = "block"; 
        }, 
        openFormPutQuestion: function(item){
            this.getQuestion = item; 
            document.getElementById("form_update_question").style.display = "block"; 
            document.getElementById("question_back").style.display = "block";
        }, 
        seeDetailQuestion: function(item){
            this.getQuestion = item;
            document.getElementById("detail_question").style.display = "block"; 
            document.getElementById("question_back").style.display = "block";
        }, 
        deleteQuestion: function(item){
            document.getElementById("thognbao").innerHTML = "Bạn có chắc chắn muốn xóa câu hỏi có mã " +  item.questionCode; 

            document.getElementById("ad-dialog-delete").style.display = "block"; 

            document.getElementById("ad-dialog-btn").addEventListener("click", ()=>{

                //Tiến hành xóa các dữ liệu ở bảng có liên quan tới câu hỏi. 
                //Xóa ở bảng studentanswer trước. 
                BaseRequest.get("student_answer/byquestion?questionId=" + item.questionId)
                .then(response => {
                    this.student_answer = response.data; 
                    for (const item of this.student_answer) {
                        this.answer_code.push(item.answerCode); 
                    }

                    if(this.answer_code.length === 0){
                        BaseRequest.delete("question?code=" + item.questionCode)
                        .then(response => {
                            console.log(response.data); 
                            document.getElementById("dialog_dangky_content").innerHTML = "Xóa người dùng thành công."
                            document.getElementById("ad-dialog-delete").style.display = "none"; 
                            document.getElementById("dialog_question").style.opacity = 1; 
                        })
                        .catch(error => {
                            console.log(error.message); 
                        }) 
                    }
                    else {
                        //Tien hanh xoa danh sach cau tra loi co lien quan toi cau hoi do. 
                        BaseRequest.delete("student_answer", this.answer_code)
                        .then(response => {
                            console.log(response.data); 
                            //Neu xoa thanh cong thi xoa cau hoi. 
                            BaseRequest.delete("question?code=" + item.questionCode)
                            .then(response => {
                                console.log(response.data); 
                                document.getElementById("dialog_dangky_content").innerHTML = "Xóa người dùng thành công."
                                document.getElementById("ad-dialog-delete").style.display = "none"; 
                                document.getElementById("dialog_question").style.opacity = 1; 
                            })
                            .catch(error => {
                                console.log(error.message); 
                            }) 
                        })
                        .catch(error => {
                            console.log(error.message); 
                        })
                    }
                })
                .catch(error => {
                    console.log(error.message); 
                })
            })
        }, 
        searchQuestion: function(){
            var x = document.getElementById("search-question").value; 
            for (const item of this.question) {
                if(item.lessonName === x || item.questionCode === x){
                    this.tg.push(item); 
                }
            }
            this.question = this.tg; 
        }
    }
}
</script>
<style>
.icon_excel {
    background: url('@/assets/Sprites.64af8f61.svg') no-repeat -705px -202px;
	width: 23px;
	height: 20px;
}
.image_question > img {
    width: 100%; 
    height: 100%;
}
.fix-height-question {
    height: 550px;
}
.buggg_in > table > tbody {
    height: calc(100vh - 56px - 200px - 98px - 16px - 50px);
}
.style_image_question > img {
    width: 100%; 
    height: 100%;
}
.menu-question {
    color: #0a0a23; 
    text-decoration: none;
}
.menu-question:hover {
    text-decoration: underline;
}
</style>
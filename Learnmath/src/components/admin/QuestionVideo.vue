<template>
    <Navbar></Navbar>
    <Sidebar></Sidebar>
    <DetaiVideoQuestion :propGetVideoQuestion="getQuestion"></DetaiVideoQuestion>
    <PostVideoQuestion :propVideoQuestion="question"></PostVideoQuestion>
    <UpdateVideoQuestion :propGetVideoQuestion="getQuestion"></UpdateVideoQuestion>
    <ImportExcel></ImportExcel>
    <div class="a-course">
        <h1>
            <router-link to="/MenuQuestion" class="menu-question">Quản lý câu hỏi</router-link> / 
            <router-link to="/QuestionVideo" class="menu-question">Câu hỏi ôn tập trong video</router-link>
        </h1>
        <div class="a-course_table fix-height-question">
            <div class="a-course_table-title">
                <div class="a-course_table-title-search">
                    <input type="text" placeholder="Tìm kiếm theo mã, chủ đề khóa học" id="search-video-question">
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
                        <tr v-for="item in question" :key="item.video_QuestionId">
                            <td style="width: 80px; text-align: center;">{{ item.video_QuestionCode }}</td>
                            <td style="width: 200px">{{ item.video_QuestionContent }}</td>
                            <td style="width: 80px;">{{ item.video_QuestionResult }}</td>
                            <td style="width: 150px;">{{ item.video_QuestionAnswerA }}</td>
                            <td style="width: 150px;">{{ item.video_QuestionAnswerB }}</td>
                            <td style="width: 150px;">{{ item.video_QuestionAnswerC }}</td>
                            <td style="width: 150px;">{{ item.video_QuestionAnswerD }}</td>
                            <td class="chucnang_course">{{ item.videoTitle }}
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
    <div class="course_back" id="video-question_back"></div>

    <CanhBao></CanhBao>
</template>
<script>
import BaseRequest from '@/core/BaseRequest'
import Navbar from './layout/NavbarAdmin.vue'
import Sidebar from './layout/SidebarAdmin.vue'
import CanhBao from './layout/DialogCanhbao.vue'
import DetaiVideoQuestion from './video/DetailVideo_Question.vue'
import PostVideoQuestion from './video/PostVideo_Question.vue'
import UpdateVideoQuestion from './video/UpdateVideo_Question.vue'
import ImportExcel from './excel/ImportQVideo.vue'
export default{
    components: {
        Navbar, 
        Sidebar, 
        CanhBao, 
        DetaiVideoQuestion, 
        PostVideoQuestion, 
        UpdateVideoQuestion, 
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
        BaseRequest.get("view_video_question")
        .then(response => {
            this.question = response.data; 
        })
        .catch(error => {
            console.log(error.message); 
        })
    }, 
    // watch: {
    //     pageSize: function(){
    //         BaseRequest.get("viewquestionlesson/page?page=" + this.page + "&pageSize=" + this.pageSize)
    //         .then(response => {
    //             this.question = response.data; 
    //         })
    //         .catch(error => {
    //             console.log(error.message); 
    //         })
    //     }
    // }, 
    methods: {
        importQuestion: function(){
            document.getElementById("import-excel-video").style.display = "block"; 
            document.getElementById("video-question_back").style.display = "block"; 
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
            BaseRequest.get("view_video_question")
            .then(response => {
                this.question = response.data; 
            })
            .catch(error => {
                console.log(error.message); 
            })
        },
        openFormPostQuestion: function(){
            document.getElementById("video-question_back").style.display = "block"; 
            document.getElementById("form_video-question").style.display = "block"; 
        }, 
        openFormPutQuestion: function(item){
            this.getQuestion = item; 
            document.getElementById("form_update_video-question").style.display = "block"; 
            document.getElementById("video-question_back").style.display = "block";
        }, 
        seeDetailQuestion: function(item){
            this.getQuestion = item;
            document.getElementById("detail_video-question").style.display = "block"; 
            document.getElementById("video-question_back").style.display = "block";
        }, 
        deleteQuestion: function(item){
            document.getElementById("thognbao").innerHTML = "Bạn có chắc chắn muốn xóa câu hỏi có mã " +  item.video_QuestionCode; 

            document.getElementById("ad-dialog-delete").style.display = "block"; 

            document.getElementById("ad-dialog-btn").addEventListener("click", ()=>{
                //cai nay xoa thang luon 
                BaseRequest.delete("video_question?code=" + item.video_QuestionCode)
                .then(response => {
                    //hien thi thong bao xoa thanh cong
                    console.log(response.data);
                    document.getElementById("ad-dialog-delete").style.display = "none"; 
                    document.getElementById("dialog_video-question_content").innerHTML = "Xóa câu hỏi thành công."
                    document.getElementById("dialog_video-question").style.opacity = "1";  
                })
                .catch(error => {
                    console.log(error.message); 
                })
            })
        }, 
        searchQuestion: function(){
            var x = document.getElementById("search-video-question").value; 
            for (const item of this.question) {
                if(item.videoTitle === x || item.video_QuestionCode === x){
                    this.tg.push(item); 
                }
            }
            this.question = this.tg; 
        }
    }
}
</script>
<style scoped>
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
.a-course_table-content > table > tbody > tr {
    min-height: 66px;
}
</style>
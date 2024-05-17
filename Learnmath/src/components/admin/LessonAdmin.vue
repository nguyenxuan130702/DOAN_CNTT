<template>
    <Navbar></Navbar>
    <Sidebar></Sidebar>
    <FormLesson :propLesson="lesson"></FormLesson>
    <FormUpdateLesson :propLesson="lesson" :propGetLesson="getLesson"></FormUpdateLesson>
    <div class="a-course">
        <h1>Quản lý bài học</h1>
        <div class="a-course_table">
            <div class="a-course_table-title">
                <div class="a-course_table-title-search">
                    <input type="text" placeholder="Tìm kiếm theo mã, chủ đề khóa học" id="search-new">
                    <div class="search-course" v-on:click="searchNew()">
                        <div class="icon_search"></div>
                    </div>
                </div>
                <div id="qbugg"></div>
                <div class="a-course_table-title-btn">
                    <button v-on:click="openFormPostLesson()">Thêm mới</button>
                    <div class="icon_loaddata_sty">
                        <div class="icon_loaddata" v-on:click="loadDataLesson()"></div>
                    </div>
                </div>
            </div>
            <div class="a-course_table-content">
                <table>
                    <thead>
                        <tr>
                            <td style="width: 100px;">Mã bài học</td>
                            <td style="width: 200px">Tên bài học</td>
                            <td style="width: 400px">Mô tả</td>
                            <td>Khóa học</td>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-for="item in lesson" :key="item.lessonId">
                            <td style="width: 100px; text-align: center;">{{ item.lessonCode }}</td>
                            <td style="width: 200px">{{ item.lessonName }}</td>
                            <td style="width: 400px">{{ item.lessonContent }}</td>
                            <td class="chucnang_course">{{ item.courseName }}
                                <div class="chucnang_course-style">
                                    <div class="updatehover"  title="Sửa thông tin" v-on:click="openFormUpdateLesson(item)">
                                        <div class="icon_update"></div>
                                    </div>
                                    <div class="deletehover" title="Xóa thông tin" v-on:click="deleteLesson(item)">
                                        <div class="icon_delete"></div>
                                    </div>
                                </div>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <div class="a-course_table-page">
                <div style="margin-left: 24px;">
                    Tổng: {{ lesson.length }}
                </div>
                <div style="margin-right: 24px" class="a-course_table-page1">
                    <div>Số bản ghi/trang: 
                        <select name="" id="" v-model="pageSize">
                            <option value="10">10</option>
                            <option value="20">20</option>
                            <option value="30">30</option>
                        </select>
                    </div>
                    <div class="icon_back" v-on:click="backPageLesson()"></div>
                    <div class="icon_next" v-on:click="nextPageLesson()"></div>
                </div>
            </div>
        </div>
    </div>
    <div class="course_back" id="lesson_back"></div>
    <CanhBao></CanhBao>
</template>
<script>
import Navbar from './layout/NavbarAdmin.vue'
import Sidebar from './layout/SidebarAdmin.vue'
import FormLesson from './layout/FormLesson.vue'
import BaseRequest from '@/core/BaseRequest'
import FormUpdateLesson from './layout/FormUpdateLesson.vue'
import CanhBao from './layout/DialogCanhbao.vue'
export default{
    components: {
        FormLesson,
        Navbar, 
        Sidebar, 
        FormUpdateLesson, 
        CanhBao
    }, 
    data(){
        return{
            lesson: [], 
            getLesson: {},
            questionLesson: [], 
            question_code: [], 
            pageSize: 10, 
            page: 1, 
        }
    }, 
    created(){
        BaseRequest.get("viewlessoncourse/page?page=" + this.page + "&pageSize=" + this.pageSize)
        .then(response => {
            this.lesson = response.data; 
        })
        .catch(error => {
            console.log(error.message); 
        })
    },
    watch: {
        pageSize: function(){
            BaseRequest.get("viewlessoncourse/page?page=" + this.page + "&pageSize=" + this.pageSize)
            .then(response => {
                this.lesson = response.data; 
            })
            .catch(error => {
                console.log(error.message); 
            })
        }
    }, 
    methods: {
        nextPageLesson: function(){
            this.page++; 
            BaseRequest.get("viewlessoncourse/page?page=" + this.page + "&pageSize=" + this.pageSize)
            .then(response => {
                this.lesson = response.data; 
            })
            .catch(error => {
                console.log(error.message); 
            })
        }, 
        backPageLesson: function(){
            this.page--; 
            BaseRequest.get("viewlessoncourse/page?page=" + this.page + "&pageSize=" + this.pageSize)
            .then(response => {
                this.lesson = response.data; 
            })
            .catch(error => {
                console.log(error.message); 
            })
        },
        loadDataLesson: function(){
            BaseRequest.get("viewlessoncourse")
            .then(response => {
                this.lesson = response.data; 
            })
            .catch(error => {
                console.log(error.message); 
            })
        },
        openFormUpdateLesson: function(item){
            this.getLesson = item; 
            document.getElementById("form_update_lesson").style.display = "block"; 
            document.getElementById("lesson_back").style.display = "block"; 
        }, 
        openFormPostLesson: function(){
            document.getElementById("lesson_back").style.display = "block"; 
            document.getElementById("form_lesson").style.display = "block"; 
        }, 
        deleteLesson: function(item){
            document.getElementById("thognbao").innerHTML = "Bạn có chắc chắn muốn xóa bài học có mã " +  item.lessonCode;

            document.getElementById("ad-dialog-delete").style.display = "block";

            document.getElementById("ad-dialog-btn").addEventListener("click", ()=>{
                //lấy ra danh sách mã câu hỏi thuộc vào bài học đó. 
                BaseRequest.get("question/lessonId?id=" + item.lessonId)
                .then(response => {
                    this.questionLesson = response.data; 
                    console.log(response.data); 
                    //lưu mã câu hỏi vào trong mảng mã 
                    for (const x of this.questionLesson) {
                        this.question_code.push(x.questionCode); 
                    }
                    console.log(this.question_code); 
                    
                    if(this.question_code.length === 0){
                        BaseRequest.delete("lesson?code=" + item.lessonCode)
                        .then(response => {
                            console.log(response.data);
                            document.getElementById("dialog_dangky_content").innerHTML = "Xóa khóa học thành công."
                            document.getElementById('dialog_lesson').style.opacity = "1"; 
                            document.getElementById("ad-dialog-delete").style.display = "none"; 
                        })
                        .catch(error => {
                            console.log(error.message); 
                        })
                    }
                    else {
                        //thực hiện xóa danh sách câu hỏi khỏi APi 
                        BaseRequest.delete("question/DeleteAny", this.question_code)
                        .then(response => {
                            console.log(response.data); 
                            //Nếu xóa thành công thì bắt đầu xóa khóa học 
                            BaseRequest.delete("lesson?code=" + item.lessonCode)
                            .then(response => {
                                console.log(response.data);
                                document.getElementById("dialog_dangky_content").innerHTML = "Xóa khóa học thành công."
                                document.getElementById('dialog_lesson').style.opacity = "1"; 
                                document.getElementById("ad-dialog-delete").style.display = "none"; 
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
        }
    }
}
</script>
<style scoped>
</style>

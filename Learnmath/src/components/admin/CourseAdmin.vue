<template>
    <Navbar></Navbar>
    <Sidebar></Sidebar>
    <FormCourse :propCourse="course"></FormCourse>
    <FormUpdateCourse :propCourse="course" :propGetCourse="getItem"></FormUpdateCourse>
    <DetailCourse :propGetCourse="getItem"></DetailCourse>
    <div>
        <div class="a-course">
        <h1>
            <router-link to="/MenuCourse" class="menu-question">Quản lý khóa học</router-link> / 
            <router-link to="/MenuCourse" class="menu-question">Khóa học ôn tập</router-link>
        </h1>
        <div class="a-course_table">
            <div class="a-course_table-title">
                <div class="a-course_table-title-search">
                    <input type="text" placeholder="Tìm kiếm theo mã, chủ đề khóa học" id="search-course">
                    <div class="search-course" v-on:click="searchCourse()">
                        <div class="icon_search"></div>
                    </div>
                </div>
                <div class="a-course_table-title-btn">
                    <button v-on:click="openFormPostCourse()">Thêm mới</button>
                    <div class="icon_loaddata_sty">
                        <div class="icon_loaddata" v-on:click="loadDataCourse()"></div>
                    </div>
                </div>
            </div>
            <div class="a-course_table-content">
                <table>
                    <thead>
                        <tr>
                            <td style="width: 200px;">Mã khóa học</td>
                            <td style="width: 300px">Tên khóa học</td>
                            <td >Mô tả</td>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-for="item in course" :key="item.courseId">
                            <td style="width: 200px; text-align: center;">{{ item.courseCode }}</td>
                            <td style="width: 300px">{{ item.courseName }}</td>
                            <td class="chucnang_course">{{ renderCourse(item.courseType) }}
                                <div class="chucnang_course-style">
                                    <div class="seehover" title="Xem chi tiết" v-on:click="seeDetailCourse(item)">
                                        <div class="icon_zoom"></div>
                                    </div>
                                    <div class="updatehover" title="Sửa khóa học" v-on:click="openFormPutCourse(item)">
                                        <div class="icon_update"></div>
                                    </div>
                                    <div class="deletehover" title="Xóa khóa học" v-on:click="deleteCourse(item)">
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
                    Tổng: {{ course.length }}
                </div>
                <div style="margin-right: 24px" class="a-course_table-page1">
                    <div>Số bản ghi/trang: 
                        <select name="" id="" v-model="pageSize">
                            <option value="5">5</option>
                            <option value="10">10</option>
                            <option value="15">15</option>
                        </select>
                    </div>
                    <div class="icon_back" v-on:click="backPageCourse()"></div>
                    <div class="icon_next" v-on:click="nextPageCourse()"></div>
                </div>
            </div>
        </div>
    </div>
    </div>
    <div class="course_back" id="course_back"></div>

    <CanhBao></CanhBao>
</template>
<script>
import Navbar from './layout/NavbarAdmin.vue'
import Sidebar from './layout/SidebarAdmin.vue'
import FormCourse from './layout/FormCourse.vue'
import BaseRequest from '@/core/BaseRequest'
import FormUpdateCourse from './layout/FormUpdateCourse.vue'
import DetailCourse from './layout/DetailCourse.vue'
import CanhBao from './layout/DialogCanhbao.vue'
export default{
    components: {
        FormCourse, 
        Navbar, 
        Sidebar, 
        FormUpdateCourse, 
        DetailCourse, 
        CanhBao
    }, 
    data(){
        return {
            makhoahoc: '', 
            tenkhoahoc: '', 
            mota: '',
            course: [], 
            getItem: {}, 
            tg: [], 
            lesson: [], 
            code_question: [], 
            question_lesson: [], 
            page: 1,
            pageSize: 5, 
        }
    }, 
    created(){
        BaseRequest.get("course/page?page=" + this.page + "&pageSize=" + this.pageSize)
        .then(response => {
            this.course = response.data; 
        })
        .catch(error => {
            console.log(error.message); 
        })
    }, 
    watch: {
        pageSize: function(){
            BaseRequest.get("course/page?page=" + this.page + "&pageSize=" + this.pageSize)
            .then(response => {
                this.course = response.data; 
            })
            .catch(error => {
                console.log(error.message); 
            })
        }
    }, 
    methods: {
        nextPageCourse: function(){
            this.page++; 
            BaseRequest.get("course/page?page=" + this.page + "&pageSize=" + this.pageSize)
            .then(response => {
                this.course = response.data; 
            })
            .catch(error => {
                console.log(error.message); 
            })
        },
        backPageCourse: function(){
            this.page--; 
            BaseRequest.get("course/page?page=" + this.page + "&pageSize=" + this.pageSize)
            .then(response => {
                this.course = response.data; 
            })
            .catch(error => {
                console.log(error.message); 
            })
        }, 
        searchCourse: function(){
            var x = document.getElementById("search-course").value;
            var c1 = "Đại số"; 
            var c2 = "Hình học"; 
            var c3 = "Đề thi thử."; 
            if(c1.toLowerCase() === x.toLowerCase()){
                x = 0; 
            }
            else if(c2.toLowerCase() === x.toLowerCase()){
                x = 1; 
            }
            else if(c3.toLowerCase() === x.toLowerCase()){
                x = 2; 
            }

            for (const item of this.course) {
                if(item.courseCode === x || item.courseType === x){
                    this.tg.push(item); 
                }
            }
            this.course = this.tg; 
        }, 
        renderCourse: function(item){
            if(item === 0){
                return "Đại số"; 
            }
            else if(item === 1){
                return "Hình học"; 
            }
            else {
                return "Đề thi thử"
            }
        }, 
        openFormPutCourse: function(item){
            this.getItem = item; 
            document.getElementById("form-put-course").style.display = "block"; 
            document.getElementById("course_back").style.display = "block"; 
        }, 
        openFormPostCourse: function(){
            document.getElementById("course_back").style.display = "block"; 
            document.getElementById("form_course").style.display = "block"; 
        }, 
        loadDataCourse: function(){
            BaseRequest.get("course")
            .then(response => {
                this.course = response.data; 
            })
            .catch(error => {
                console.log(error.message); 
            })
        }, 
        seeDetailCourse: function(item){
            this.getItem = item;
            document.getElementById("course_back").style.display = "block"; 
            document.getElementById("detail-course").style.display = "block"; 
        }, 
        deleteCourse: function(item){

            document.getElementById("thognbao").innerHTML = "Bạn có chắc chắn muốn xóa khóa học có mã " +  item.courseCode; 

            document.getElementById("ad-dialog-delete").style.display = "block"; 

            document.getElementById("ad-dialog-btn").addEventListener("click", ()=>{
                //kiểm tra xem trong course có bao nhiêu lesson. 
                BaseRequest.get("lesson/course?id=" + item.courseId)
                .then(response => {
                    this.lesson = response.data;
                    //Nếu như trong course không có lesson thì thực hiện xóa course.
                    if(this.lesson.length === 0){
                        BaseRequest.delete("course?code=" + item.courseCode)
                        .then(response => {
                            console.log(response.data); 
                            document.getElementById("dialog_dangky_content").innerHTML = "Xóa khóa học thành công."
                            document.getElementById("ad-dialog-delete").style.display = "none"; 
                            document.getElementById("dialog_themmoicourse").style.opacity = "1";  
                        })
                        .catch(error => {
                            console.log(error.message); 
                        }) 
                    } 
                    else {
                        //Nếu có thì thực hiện xóa các lesson trước. 
                        //Đầu tiên phải kiểm tra xem trong leson có question ko. 
                        for (const x of this.lesson) {
                            //kiểm tra có question hay ko. 
                            BaseRequest.get("question/lessonId?id=" + x.lessonId)
                            .then(response => {
                                this.question_lesson = response.data; 
                                //lưu mã câu hỏi vào trong mảng mã 
                                for (const z of this.question_lesson) {
                                    this.code_question.push(z.questionCode); 
                                }
                                //Nếu ko có question thì xóa lesson. 
                                if(this.code_question.length === 0){
                                    //Thực hiện xóa bản ghi lesson 
                                    BaseRequest.delete("lesson?code=" + x.lessonCode)
                                    .then(response => {
                                        console.log(response.data); 
                                    })
                                    .catch(error => {
                                        console.log(error.message); 
                                    })
                                }
                                else {
                                    //Thực hiện xóa các question khỏi bảng question sau đó xóa lesson. 
                                    BaseRequest.delete("question/DeleteAny", this.code_question)
                                    .then(response => {
                                        //thực hiện xóa lesson khỏi database 
                                        console.log(response.data); 
                                        BaseRequest.delete("lesson?code=" + x.lessonCode)
                                        .then(response => {
                                            console.log(response.data);
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
                        }
                        //Thực hiện xóa course khỏi database 

                        console.log(item.courseCode); 
                        BaseRequest.delete("course?code=" + item.courseCode)
                        .then(response => {
                            console.log(response.data); 
                            document.getElementById("dialog_dangky_content").innerHTML = "Xóa khóa học thành công."
                            document.getElementById("ad-dialog-delete").style.display = "none"; 
                            document.getElementById("dialog_themmoicourse").style.opacity = "1";  
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
<style>
.icon_search {
    background: url('@/assets/Sprites.64af8f61.svg') no-repeat -1041px -89px;
	width: 22px;
	height: 22px;
}
.a-course_table-title-search {
    display: flex; 
    gap: 5px; 
}
.a-course_table-title-search > input {
    height: 36px;
    width: 250px;
    border: 1px solid #d0d0d0; 
    padding-left: 8px; 
    border-radius: 4px; 
    outline: none;
}
.a-course_table-title-search > input:focus {
    border: 1px solid #50B83C; 
}
.search-course {
    width: 36px; 
    height: 36px;
    /* background-color: #50B83C;  */
    border: 1px solid #d0d0d0; 
    display: flex; 
    justify-content: center;
    align-items: center; 
}
.search-course:hover {
    cursor: pointer;
    background-color: #50B83C;
}
.icon_loaddata {
    background: url('@/assets/Sprites.64af8f61.svg') no-repeat -1098px -90px;
	width: 20px;
	height: 21px;
}
* {
    position: relative; 
}
.course_back {
    position: absolute; 
    height: 100vh;
    top: 0; 
    right: 0; 
    left: 0; 
    bottom: 0; 
    background-color: #0a0a23;
    opacity: 0.8;
    display: none; 
} 
.chucnang_course {
    border: none; 
    position: relative;
}
.chucnang_course-style {
    width: 100%; 
    height: 100%;
    display: flex;
    align-items: center;
    gap: 12px; 
    justify-content: flex-end;
    margin-right: 16px; 
    visibility:hidden;
    position: absolute; 
    top: 50%;
    transform: translateY(-50%);
    right: 20px;  
}

.updatehover {
    width: 50px;
    height: 50px;
    border-radius: 50%;
    background-color: #fff;
    display: flex; 
    align-items: center;
    justify-content: center;
    box-shadow: rgba(0,0,0,0.078) 0px 3px 6px 0px;
}
.seehover {
    width: 50px;
    height: 50px;
    border-radius: 50%;
    background-color: #fff;
    display: flex; 
    align-items: center;
    justify-content: center;
    box-shadow: rgba(0,0,0,0.078) 0px 3px 6px 0px;
}
.seehover:hover {
    background-color: #E0E0E0;
    cursor: pointer;
}
.updatehover:hover {
    background-color: #E0E0E0;
    cursor: pointer;
}
.icon_zoom {
    background: url('@/assets/admin/zoom-in-svgrepo-com.svg') no-repeat -7px -7px;
	width: 48px;
	height: 45px;
    transform: scale(0.4);
}
.deletehover {
    width: 50px;
    height: 50px;
    border-radius: 50%;
    background-color: #fff;
    display: flex; 
    align-items: center;
    justify-content: center;
    box-shadow: rgba(0,0,0,0.078) 0px 3px 6px 0px;
}
.deletehover:hover {
    background-color: #E0E0E0; 
    cursor: pointer;
}
.a-course_table-content > table > tbody > tr:hover {
    background-color: #F8F8F8; 
}
.a-course_table-content > table > tbody > tr:hover .chucnang_course-style {
    visibility: visible;
}
.trtable {
    position: relative; 
}
.tableupdate {
    display: flex; 
    align-items: center;
    gap: 20px; 
    position: absolute; 
    margin-top: 120px;  
}
.icon_update {
    background: url('@/assets/Sprites.64af8f61.svg') no-repeat -1658px -90px;
	width: 21px;
	height: 20px;
}
.icon_delete {
    background: url('@/assets/admin/delete-1-svgrepo-com.svg') no-repeat -8px -5px;
	width: 48px;
	height: 54px;
    transform: scale(0.4);
}
.a-course {
    width: calc(100% - 250px);
    float: left;
    background-color: #EEEFF1; 
    height: calc(100vh - 56px); 
}
.a-course > h1 {
    font-size: 2.5rem; 
    font-weight: 700;
    margin-top: 24px; 
    margin-left: 24px; 
}
.a-course_table{
    width: calc(100% - 24px - 24px); 
    background-color: #fff; 
    padding: 24px; 
    margin: auto; 
    margin-top: 24px; 
    height: 550px; 
}
.a-course_table-title {
    width: 100%; 
    display: flex; 
    justify-content: space-between;
}
.a-course_table-title > input {
    height: 36px;
    width: 250px; 
    border-radius: 4px;
    border: 1px solid #d0d0d0; 
    padding-left: 8px; 
    outline: none;
}
.a-course_table-title > input:focus {
    border: 1px solid #50B83C; 
}
.a-course_table-title-btn {
    display: flex; 
    gap: 16px; 
    align-items: center;
}
.a-course_table-title-search {
    width: 100%; 
    display: flex; 
    gap: 5px; 
    align-items: center;
}
.a-course_table-title-btn > button {
    width: 140px; 
    height: 36px;
    border: none; 
    background-color: #50B83C;
    padding-left: 16px; 
    padding-right: 16px; 
    border-radius: 4px;
    color: #fff;  
}
.a-course_table-content {
    width: 100%; 
    margin-top: 24px; 
}
.a-course_table-content  > table {
    width: 100%; 
    border: 1px solid #E0E0E0;
}
.a-course_table-content > table, tr, td {
    border: 1px solid #E0E0E0; 
}
.a-course_table-content > table, th, td {
    padding: 8px; 
}
.a-course_table-content > table > thead {
    position: sticky; 
    top: 0; 
    background-color: #f2f2f2;
}
.content_course {
    height: 200px;
}
.a-course_table-content > table {
    table-layout: fixed;
    border: none; 

}
.a-course_table-content > table > thead {
    display: table;
    width: 100%;
    text-align: center;
    font-size: 1.1rem;
    font-weight: 700;
}
.a-course_table-content > table > tbody {
    width: 100%;
    height: calc(100vh - 56px - 200px - 98px - 16px);
    overflow-y: scroll;
    overflow-x: hidden; 
    display: block;
    border: none; 
    border-color: white;
}
.a-course_table-content > table > tbody > tr {
    display: table;
    width: 100%; 
    table-layout: fixed;
}
.a-course_table-content > table > tbody > tr > td {
    padding-left: 16px; 
    font-size: 1.1rem;
}
.a-course_table-page {
    width: calc(80% + 9px); 
    position: fixed; 
    display: flex; 
    height: 50px;
    align-items: center;
    background-color: #fff; 
    margin-left: -24px; 
    border-top: 1px solid #d0d0d0; 
    justify-content: space-between;
    margin-top: 10px; 
}
.icon_back {
    background: url('@/assets/Sprites.64af8f61.svg') no-repeat -36px -361px;
	width: 8px;
	height: 14px;
}
.icon_next {
    background: url('@/assets/Sprites.64af8f61.svg') no-repeat -84px -361px;
	width: 8px;
	height: 14px;
}
.a-course_table-page1 {
    width: 250px;
    display: flex;
    justify-content: space-between; 
    align-items: center;
}

.icon_close:hover {
    cursor: pointer;
}
.form_course {
    width: 50%; 
    border-radius: 5px;
    box-shadow: 5px 5px 10px rgba(248, 248, 248, 0.1)
}

.icon_loaddata_sty{
    width: 36px; 
    height: 36px;
    display: flex;
    justify-content: center;
    align-items: center;
    border: 2px solid #d0d0d0; 
}
.icon_loaddata_sty:hover {
    background-color: #50B83C;
    border: none; 
    cursor: pointer;
}

/**----------------------------------------------------------------------------- */
.menu-question {
    text-decoration: none;
    color: #0a0a23; 
}
.menu-question:hover {
    text-decoration: underline;
}
</style>

<template>
    <Navbar></Navbar>
    <Sidebar></Sidebar>
    <DetailVideoCourse :propGetVideo_Course="getItem"></DetailVideoCourse>
    <PostVideoCourse :propVideo_Course="video_course"></PostVideoCourse>
    <UpdateVideoCourse :propGetVideo_Course="getItem"></UpdateVideoCourse>
    <div>
        <div class="a-course">
        <h1>
            <router-link to="/MenuCourse" class="menu-question">Quản lý khóa học</router-link> / 
            <router-link to="/MenuCourse" class="menu-question">Khóa học có bài giảng</router-link>
        </h1>
        <div class="a-course_table">
            <div class="a-course_table-title">
                <div class="a-course_table-title-search">
                    <input type="text" placeholder="Tìm kiếm theo mã, chủ đề khóa học" id="search-video-course">
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
                            <td >Ngày tạo</td>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-for="item in video_course" :key="item.courseId">
                            <td style="width: 200px; text-align: center;">{{ item.video_CourseCode }}</td>
                            <td style="width: 300px">{{ item.video_CourseTitle }}</td>
                            <td class="chucnang_course">{{ renderDate(item.created_at) }}
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
                    Tổng: {{ video_course.length }}
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
    <div class="course_back" id="video-course_back"></div>
    <CanhBao></CanhBao>
</template>
<script>
import BaseRequest from '@/core/BaseRequest'
import Navbar from './layout/NavbarAdmin.vue'
import Sidebar from './layout/SidebarAdmin.vue'
import DetailVideoCourse from './video/DetailVideoCourse.vue'
import PostVideoCourse from './video/PostVideoCourse.vue'
import UpdateVideoCourse from './video/UpdateVideoCourse.vue'
import CanhBao from './layout/DialogCanhbao.vue'
export default{
    components: {
        Navbar, 
        Sidebar, 
        DetailVideoCourse, 
        PostVideoCourse, 
        UpdateVideoCourse, 
        CanhBao
    }, 
    data(){
        return {
            video_course: [], 
            getItem: {}, 
            //Biến này để lưu các video thuộc vào khóa học video muốn xóa. 
            videos: [], 
            //Biến này dùng để lưu kết quả học các video đã học. 
            learning: [], 
            //Biến này dùng để lưu các mã kết quả phục vụ việc xóa kết quả. 
            learn_code: [], 
            //Biến này dùng để lưu các mã video phục vụ việc xóa danh sách video. 
            video_code: [], 
            //Bien nay dung de tim kiem theo ma 
            video_course_code: [], 
            //Biến này dùng để kiểm tra câu hỏi thuộc video đang xét. 
            video_question: [], 
            video_question_code: [], 
        }
    }, 
    mounted() {
        BaseRequest.get("video_course")
        .then(response => {
            this.video_course = response.data; 
        })
        .catch(error => {
            console.log(error.message); 
        })
    }, 
    methods: {
        renderDate: function(date1){
            const date = new Date(date1);
            const day = date.getDay(); 
            const month = date.getMonth(); 
            const year = date.getFullYear()
            return day + '/' + month + '/' + year; 
        },
        seeDetailCourse: function(item) {
            this.getItem = item; 
            document.getElementById("video-course_back").style.display = "block"; 
            document.getElementById("detail-video-course").style.display = "block"; 
        }, 
        openFormPostCourse: function(){
            document.getElementById("form_video-course").style.display = "block"; 
            document.getElementById("video-course_back").style.display = "block"; 
        }, 
        openFormPutCourse: function(item){
            this.getItem = item; 
            document.getElementById("form-put-videocourse").style.display = "block"; 
            document.getElementById("video-course_back").style.display = "block"; 
        }, 
        deleteCourse: function(item){
            document.getElementById("thognbao").innerHTML = "Bạn có chắc chắn muốn xóa khóa học có mã " +  item.video_CourseCode; 

            document.getElementById("ad-dialog-delete").style.display = "block";

            document.getElementById("ad-dialog-btn").addEventListener("click", ()=>{
                //Tiến hành xóa khóa học video đi. 
                //Kiểm tra xem trong bảng video có bao nhiêu video thuộc vào khóa học này. 
                //Nếu có tiến hành xóa các video trước. 
                //Mà để xóa các video thì mình phải xóa các kết quả của người dùng trước. 
                BaseRequest.get("videos/video_Course?video_CourseId=" + item.video_CourseId)
                .then(response => {
                    this.videos = response.data;

                    for (const item of this.videos) {
                        this.video_code.push(item.videoCode); 
                    }
                    //Kiểm tra xem có video trong khóa học không. 
                    if(this.videos.length > 0){
                        //Tiến hành xóa các video khỏi bảng videos. 
                        //Kiểm tra xem có kết quả nào liên qua tới các video hay không. 
                        //Kiểm tra cả các câu hỏi thuộc vào video. 
                        for (const item of this.videos) {
                            BaseRequest.get("learning/video?videoId=" + item.videoId)
                            .then(response => {
                                this.learning = response.data; 
                                for (const item of this.learning) {
                                    this.learn_code.push(item.learningCode); 
                                }

                                if(this.learning.length > 0){

                                    //Nếu có tồn tại người dùng đã học xong video thì xóa toàn bộ kết quả. 
                                    BaseRequest.delete("learning/any", this.learn_code)
                                    .then(response => {
                                        console.log(response.data); 

                                    })
                                    .catch(error => {
                                        console.log(error.message); 
                                    })
                                }
                            })
                            .catch(error => {
                                console.log(error.message); 
                            })

                            //Tiến hành kiểm tra câu hỏi 
                            BaseRequest.get("video_question/video?videoId=" + item.videoId)
                            .then(response => {
                                this.video_question = response.data; 
                                for (const item of this.video_question) {
                                    this.video_question_code.push(item.video_QuestionCode); 
                                }

                                if(this.video_question_code.length > 0){
                                    BaseRequest.delete("video_question/any", this.video_question_code)
                                    .then(response => {
                                        console.log(response.data); 
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

                        //Sau khi xóa toàn bộ kết quả thuộc các video rồi thì tiến hành xóa danh sách video.
                        BaseRequest.delete("videos/any", this.video_code)
                        .then(response => {
                            console.log(response.data); 
                            //Nếu xóa xong rồi thì xóa khóa học video 
                            BaseRequest.delete("video_course?code=" + item.video_CourseCode)
                            .then(response => {
                                //Thông báo xóa thành công. 
                                console.log(response.data); 
                                document.getElementById("dialog_videocourse_content").innerHTML = "Xóa khóa học video thành công."; 
                                document.getElementById("ad-dialog-delete").style.display = "none"; 
                                document.getElementById("dialog_themmoivideocourse").style.opacity = 1; 
                            })
                            .catch(error => {
                                console.log(error.message); 
                            })
                        }) 
                        .catch(error => {
                            console.log(error.message); 
                        })
                    }
                    else {
                        //Nếu không có video nào thì xóa luôn ko cần nghĩ ngợi gì nữa. 
                        BaseRequest.delete("video_course?code=" + item.video_CourseCode)
                        .then(response => {
                            //Thông báo xóa thành công. 
                            console.log(response.data); 
                            document.getElementById("dialog_videocourse_content").innerHTML = "Xóa khóa học video thành công."; 
                            document.getElementById("ad-dialog-delete").style.display = "none"; 
                            document.getElementById("dialog_themmoivideocourse").style.opacity = 1; 
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
        searchCourse: function(){
            var input =  document.getElementById("search-video-course").value; 
            for (const item of this.video_course) {
                if(item.video_CourseCode === input){
                    this.video_course_code.push(item);
                }
            }
            this.video_course = this.video_course_code; 
        }, 
        loadDataCourse: function(){
            BaseRequest.get("video_course")
            .then(response => {
                this.video_course = response.data; 
            })
            .catch(error => {
                console.log(error.message); 
            })
        }
    }
}
</script>
<style scoped>
#video-course_back {
    display: none; 
}
.a-course_table-content > table > tbody > tr{
    min-height: 66px;
}
.menu-question {
    text-decoration: none;
    color: #0a0a23; 
}
.menu-question:hover {
    text-decoration: underline;
}
</style>

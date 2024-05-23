<template>
    <Navbar></Navbar>
    <Sidebar></Sidebar>
    <FormVideo></FormVideo>
    <FormUpdateVideo :propGetVideoUpdate="updateVideo"></FormUpdateVideo>
    <DetailVideo :propGetVideo="getVideo"></DetailVideo>
    <div>
        <div class="a-course">
        <h1>Quản lý video bài giảng</h1>
        <div class="a-course_table">
            <div class="a-course_table-title">
                <div class="a-course_table-title-search">
                    <input type="text" placeholder="Tìm kiếm theo mã hoặc khóa học" id="search-video">
                    <div class="search-course">
                        <div class="icon_search" v-on:click="searchVideo()"></div>
                    </div>
                </div>
                <div class="a-course_table-title-btn">
                    <button v-on:click="openFormVideo()">Thêm mới</button>
                    <div class="icon_loaddata_sty">
                        <div class="icon_loaddata" v-on:click="loadDataAfterReplace()"></div>
                    </div>
                </div>
            </div>
            <div class="a-course_table-content user-admin">
                <table>
                    <thead>
                        <tr>
                            <td style="width: 200px;">Mã video</td>
                            <td style="width: 400px;">Khóa học</td>
                            <td style="width: 400px;">Tiêu đề video</td>
                            <td >Ngày tạo</td>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-for="item in video" :key="item.videoId">
                            <td style="width: 200px;" class="img_video">{{ item.videoCode }}</td>
                            <td style="width: 400px" class="img_video">
                                {{ item.video_CourseTitle }}
                            </td>
                            <td style="width: 400px;">{{ item.videoTitle }}</td>
                            <td class="img_video">{{ renderDate(item.createdDate)}}
                                <div class="chucnang_course-style hover_user">
                                    <div class="seehover" title="Xem chi tiết" v-on:click="seeDetailItem(item)">
                                        <div class="icon_zoom"></div>
                                    </div>
                                    <div class="updatehover" title="Sửa thông tin" v-on:click="updateItem(item)">
                                        <div class="icon_update"></div>
                                    </div>
                                    <div class="deletehover" title="Xóa người dùng" v-on:click="deleteItem(item)">
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
                    Tổng: {{ video.length }}
                </div>
                <div style="margin-right: 24px" class="a-course_table-page1">
                    <div>Số bản ghi/trang: 
                        <select name="" id="" v-model="pageSize">
                            <option value="10">10</option>
                            <option value="20">20</option>
                            <option value="30">30</option>
                        </select>
                    </div>
                    <div class="icon_back" v-on:click="backPageUser()"></div>
                    <div class="icon_next" v-on:click="nextPageUser()"></div>
                </div>
            </div>
        </div>
    </div>
    </div>
    <div class="course_back" id="video_back"></div>

    <CanhBao></CanhBao>
</template>
<script>
import BaseRequest from '@/core/BaseRequest'
import Navbar from './layout/NavbarAdmin.vue'
import Sidebar from './layout/SidebarAdmin.vue'
import FormVideo from './layout/FormVideo.vue'
import FormUpdateVideo from './layout/FormUpdateVideo.vue'
import CanhBao from './layout/DialogCanhbao.vue'
import DetailVideo from './layout/DetailVideo.vue'
export default {
    components: {
        Navbar, 
        Sidebar, 
        FormVideo, 
        FormUpdateVideo, 
        CanhBao, 
        DetailVideo
    }, 
    data(){
        return {
            video: [], 
            getVideo: {}, 
            show_video: false, 
            tg: [], 
            video_question: [], 
            video_question_code: [], 
            video_learning: [], 
            video_learning_code: [], 
            updateVideo: {}, 
        }
    }, 
    mounted() {
        BaseRequest.get("view_video_course")
        .then(response => {
            this.video = response.data; 
        })
        .catch(error => {
            console.log(error.message); 
        })
    }, 
    methods: {
        loadDataAfterReplace: function(){
            BaseRequest.get("view_video_course")
            .then(response => {
                this.video = response.data; 
            })
            .catch(error => {
                console.log(error.message); 
            })
        }, 
        renderDate: function(date1){
            let dateObject = new Date(date1);

            let formattedDate = `${dateObject.getDate()}/${dateObject.getMonth() + 1}/${dateObject.getFullYear()}`;

            return formattedDate; 
        },
        updateItem: function(item){
            this.updateVideo = item; 
            document.getElementById("form_update-video").style.display = "block"; 
            document.getElementById("video_back").style.display = "block"; 
        }, 
        openFormVideo: function(){
            document.getElementById("form_video").style.display = "block"; 
            document.getElementById("video_back").style.display = "block"; 
        }, 
        seeDetailItem: function(item){
            this.getVideo = item; 
            document.getElementById("video_back").style.display = "block"; 
        }, 
        deleteItem: function(item){
            document.getElementById("thognbao").innerHTML = "Bạn có chắc chắn muốn xóa video có mã " +  item.videoCode; 

            document.getElementById("ad-dialog-delete").style.display = "block";
            //Để xóa được video thì đầu tiên phải xóa các câu hỏi trong video đó trước. 
            document.getElementById("ad-dialog-btn").addEventListener("click", ()=>{
                //Kiểm tra xem có câu hỏi trong video không. 
                BaseRequest.get("video_question/video?videoId=" + item.videoId)
                .then(response => {
                    this.video_question = response.data;
                    for (const item of this.video_question) {
                        this.video_question_code.push(item.video_QuestionCode); 
                    }

                    //Kiểm tra nếu câu hỏi trong video không có thì xóa luôn cái video còn nếu có thì xóa câu hỏi trước rồi xóa video 
                    if(this.video_question_code.length > 0){
                        BaseRequest.delete("video_question/any", this.video_question_code)
                        .then(response => {
                            console.log(response.data); 
                            //Tiến hành kiểm tra video có trong bảng learning không. 
                            BaseRequest.get("learning/video?videoId=" + item.videoId)
                            .then(response => {
                                this.video_learning = response.data; 
                                for (const item of this.video_learning) {
                                    this.video_learning_code.push(item.learningCode); 
                                }
                                //Kiểm tra xem có kết quả học video không 
                                if(this.video_learning_code.length > 0){
                                    //Tiến hành xóa các kết quả. 
                                    BaseRequest.delete("learning/any", this.video_learning_code)
                                    .then(response => {
                                        console.log(response.data); 
                                        //Tiến hành xóa video 
                                        //Tiến hành xóa video 
                                        BaseRequest.delete("videos?code=" + item.videoCode)
                                        .then(response => {
                                            console.log(response.data); 
                                            document.getElementById("dialog_dangky_content").innerHTML = "Xóa video thành công."
                                            document.getElementById("video_back").style.display = "none"; 
                                            document.getElementById("ad-dialog-delete").style.display = "none"; 
                                            document.getElementById("dialog_video").style.opacity = "1";
                                        })
                                        .catch(error => {
                                            console.log(error.message); 
                                        })
                                    })
                                }
                                else {
                                    //Tiến hành xóa video 
                                    BaseRequest.delete("videos?code=" + item.videoCode)
                                    .then(response => {
                                        console.log(response.data); 
                                        document.getElementById("dialog_dangky_content").innerHTML = "Xóa video thành công."
                                        document.getElementById("video_back").style.display = "none"; 
                                        document.getElementById("ad-dialog-delete").style.display = "none"; 
                                        document.getElementById("dialog_video").style.opacity = "1";
                                    })
                                    .catch(error => {
                                        console.log(error.message); 
                                    })
                                }
                            })
                        })
                        .catch(error => {
                            console.log(error.message); 
                        })
                    }
                    else {
                        //Tiến hành kiểm tra video có trong bảng learning không. 
                        BaseRequest.get("learning/video?videoId=" + item.videoId)
                        .then(response => {
                            this.video_learning = response.data; 
                            for (const item of this.video_learning) {
                                this.video_learning_code.push(item.learningCode); 
                            }
                            //Kiểm tra xem có kết quả học video không 
                            if(this.video_learning_code.length > 0){
                                //Tiến hành xóa các kết quả. 
                                BaseRequest.delete("learning/any", this.video_learning_code)
                                .then(response => {
                                    console.log(response.data); 
                                    //Tiến hành xóa video 
                                    //Tiến hành xóa video 
                                    BaseRequest.delete("videos?code=" + item.videoCode)
                                    .then(response => {
                                        console.log(response.data); 
                                        document.getElementById("dialog_dangky_content").innerHTML = "Xóa video thành công."
                                        document.getElementById("video_back").style.display = "none"; 
                                        document.getElementById("ad-dialog-delete").style.display = "none"; 
                                        document.getElementById("dialog_video").style.opacity = "1";
                                    })
                                    .catch(error => {
                                        console.log(error.message); 
                                    })
                                })
                            }
                            else {
                                //Tiến hành xóa video 
                                BaseRequest.delete("videos?code=" + item.videoCode)
                                .then(response => {
                                    console.log(response.data); 
                                    document.getElementById("dialog_dangky_content").innerHTML = "Xóa video thành công."
                                    document.getElementById("video_back").style.display = "none"; 
                                    document.getElementById("ad-dialog-delete").style.display = "none"; 
                                    document.getElementById("dialog_video").style.opacity = "1";
                                })
                                .catch(error => {
                                    console.log(error.message); 
                                })
                            }
                        })
                    }
                })
                .catch(error => {
                    console.log(error.message); 
                }) 
            })
        }, 
        searchVideo: function(){
            var x = document.getElementById("search-video").value; 
            for (const item of this.video) {
                if(item.video_CourseTitle === x || item.videoCode === x){
                    this.tg.push(item); 
                }
            }
            this.video = this.tg; 
        }
    }
}
</script>
<style scoped>
.img_video {
    text-align: center;
}
.img_video > img {
    height: 50px;
    width: 80px;
}
#video_back {
    display: none; 
}
</style>

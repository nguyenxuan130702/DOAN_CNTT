<template>
<Navbar></Navbar>
<Sidebar></Sidebar>
<FormUpdateComment :propGetComment="getComment"></FormUpdateComment>
<DetailComment :propGetComment="getComment"></DetailComment>
    <div class="a-course">
        <h1>Quản lý bình luận</h1>
        <div class="a-course_table">
            <div class="a-course_table-title">
                <div class="a-course_table-title-search">
                    <input type="text" placeholder="Tìm kiếm theo mã bài đăng" id="search-comment">
                    <div class="search-course" v-on:click="searchComment()">
                        <div class="icon_search"></div>
                    </div>
                </div>
                <div class="a-course_table-title-btn">
                    <div class="icon_loaddata_sty">
                        <div class="icon_loaddata" v-on:click="loadDataComment()"></div>
                    </div>
                </div>
            </div>
            <div class="a-course_table-content">
                <table>
                    <thead>
                        <tr>
                            <td style="width: 100px;">Mã bình luận</td>
                            <td style="width: 450px">Nội dung bình luận</td>
                            <td style="width: 100px">Người bình luận</td>
                            <td>Bài đăng được bình luận</td>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-for="item in comment" :key="item.commentId">
                            <td style="width: 100px; text-align: center;">{{ item.commentCode }}</td>
                            <td style="width: 450px">{{ item.commentContent }}</td>
                            <td style="width: 100px" class="a-comment-img">
                                <img :src="item.userImage" alt="">
                            </td>
                            <td class="chucnang_course"> <div class="a-category-thread">{{ item.threadsTitle }}</div>
                                <div class="chucnang_course-style">
                                    <div class="seehover" title="Xem chi tiết" v-on:click="seeDetailComment(item)">
                                        <div class="icon_zoom"></div>
                                    </div>
                                    <div class="updatehover" title="Sửa khóa học" v-on:click="openFormPutComment(item)">
                                        <div class="icon_update"></div>
                                    </div>
                                    <div class="deletehover" title="Xóa khóa học" v-on:click="deleteComment(item)">
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
                    Tổng: {{ comment.length }}
                </div>
                <div style="margin-right: 24px" class="a-course_table-page1">
                    <div>Số bản ghi/trang: 
                        <select name="" id="" v-model="pageSize">
                            <option value="10">10</option>
                            <option value="20">20</option>
                            <option value="30">30</option>
                        </select>
                    </div>
                    <div class="icon_back" v-on:click="backPageComment()"></div>
                    <div class="icon_next" v-on:click="nextPageComment()"></div>
                </div>
            </div>
        </div>
    </div>

    <div class="course_back" id="comment_back"></div>
    <CanhBao></CanhBao>
</template>
<script>
import Navbar from './layout/NavbarAdmin.vue'
import Sidebar from './layout/SidebarAdmin.vue'
import BaseRequest from '@/core/BaseRequest'
import FormUpdateComment from './layout/FormUpdateComment.vue'
import DetailComment from './layout/DetailComment.vue'
import CanhBao from './layout/DialogCanhbao.vue'
export default {
    components: {
        Navbar, 
        Sidebar, 
        FormUpdateComment, 
        DetailComment, 
        CanhBao
    }, 
    data(){
        return {
            comment: [], 
            getComment: {}, 
            tg: [], 
            pageSize: 10, 
            page: 1, 
        }
    },
    mounted(){
        BaseRequest.get("commentuser/page?page=" + this.page + "&pageSize=" + this.pageSize)
        .then(response => {
            this.comment = response.data; 
        })
        .catch(error => {
            console.log(error.message); 
        })
    }, 
    watch: {
        pageSize: function(){
            BaseRequest.get("commentuser/page?page=" + this.page + "&pageSize=" + this.pageSize)
            .then(response => {
                this.comment = response.data; 
            })
            .catch(error => {
                console.log(error.message); 
            })
        }
    }, 
    methods: {
        backPageComment: function(){
            this.page--; 
            BaseRequest.get("commentuser/page?page=" + this.page + "&pageSize=" + this.pageSize)
            .then(response => {
                this.comment = response.data; 
            })
            .catch(error => {
                console.log(error.message); 
            })
        }, 
        nextPageComment: function(){
            this.page++; 
            BaseRequest.get("commentuser/page?page=" + this.page + "&pageSize=" + this.pageSize)
            .then(response => {
                this.comment = response.data; 
            })
            .catch(error => {
                console.log(error.message); 
            })
        },
        searchComment: function(){
            var x = document.getElementById("search-comment").value; 
            for (const item of this.comment) {
                if(item.commentCode === x){
                    this.tg.push(item); 
                }
            }
            this.comment = this.tg; 
        }, 
        loadDataComment: function(){
            BaseRequest.get("commentuser")
            .then(response => {
                this.comment = response.data; 
            })
            .catch(error => {
                console.log(error.message); 
            })
        }, 
        openFormPutComment: function(item){
            this.getComment = item; 
            document.getElementById("form_update_thread").style.display = "block"; 
            document.getElementById("comment_back").style.display = "block"; 
        }, 
        seeDetailComment: function(item){
            this.getComment = item; 
            document.getElementById("form_detail_thread").style.display = "block"; 
            document.getElementById("comment_back").style.display = "block";
        }, 
        deleteComment: function(item){
            document.getElementById("thognbao").innerHTML = "Bạn có chắc chắn muốn xóa chủ đề có mã " +  item.commentCode;

            document.getElementById("ad-dialog-delete").style.display = "block";

            document.getElementById("ad-dialog-btn").addEventListener("click", ()=>{
                //Thực hiện xóa bản ghi thread 
                BaseRequest.delete("comment?code=" + item.commentCode)
                .then(response => {
                    //thông báo thành công. 
                    console.log(response.data); 
                    document.getElementById("ad-dialog-delete").style.display = "none"; 
                    document.getElementById("dialog_comment").style.opacity = 1;
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
.a-user-thread {
    display: flex; 
    justify-content: center;
    align-items: center;
    border-top: none; 
    border-bottom: none; 
    border-left: none; 
}
.a-comment-img > img {
    width: 50px;
    height: 50px;
    border-radius: 50%;
}
</style>

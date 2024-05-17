<template>
<Navbar></Navbar>
<Sidebar></Sidebar>
<FormUser :propUser="user"></FormUser>
<FormUpdateUser :propName="getUser" :propUser="user"></FormUpdateUser>
<DetailUser :propName="getUser"></DetailUser>
    <div>
        <div class="a-course">
        <h1>Quản lý tài khoản</h1>
        <div class="a-course_table">
            <div class="a-course_table-title">
                <div class="a-course_table-title-search">
                    <input type="text" placeholder="Tìm kiếm theo mã hoặc vai trò" id="search-user">
                    <div class="search-course">
                        <div class="icon_search" v-on:click="searchUser()"></div>
                    </div>
                </div>
                <div class="a-course_table-title-btn">
                    <button v-on:click="openFormUser()">Thêm mới</button>
                    <div class="icon_loaddata_sty">
                        <div class="icon_loaddata" v-on:click="loadDataAfterReplace()"></div>
                    </div>
                </div>
            </div>
            <div class="a-course_table-content user-admin">
                <table>
                    <thead>
                        <tr>
                            <td style="width: 100px;">Mã tài khoản</td>
                            <td style="width: 300px">Họ và tên</td>
                            <td style="width: 150px;">Tên tài khoản</td>
                            <td style="width: 300px">Email</td>
                            <td>Vai trò</td>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-for="item in user" :key="item.userId">
                            <td style="width: 100px; text-align: center;">{{ item.userCode }}</td>
                            <td style="width: 300px">{{ item.userName }}</td>
                            <td style="width: 150px">{{ item.userAccout }}</td>
                            <td style="width: 300px">{{ item.userEmail }}</td>
                            <td class="chucnang_course">{{ renderRole(item.userType) }}
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
                    Tổng: {{ this.user.length }}
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
    <div class="course_back" id="user_back"></div>


    <CanhBao></CanhBao>

</template>
<script>
import Navbar from './layout/NavbarAdmin.vue'
import FormUser from './layout/FormUser.vue'
import Sidebar from './layout/SidebarAdmin.vue'
import BaseRequest from '@/core/BaseRequest'
import FormUpdateUser from './layout/FormUpdateUser.vue'
import DetailUser from './layout/DetailUser.vue'
import CanhBao from './layout/DialogCanhbao.vue'
export default{
    data(){
        return {
            user: [], 
            getUser: {}, 
            thongbao: "Bạn có chắc chắc muôn xóa", 
            showDelete: true, 
            tg: [], 
            pageSize: 10, 
            page: 1, 

            //Biến dùng để luu các kết quả thi thử của người dùng. 
            user_score: [], 
            user_score_code: [], 

            //Biến dùng để lưu các kết quả làm câu hỏi của người dùng 
            user_answer: [], 
            user_answer_code:[], 

            //Biến dùng để lưu các kết quả học video. 
            user_learning: [], 
            user_learning_code: [], 

            //Biến dùng để lưu comment 
            user_comment: [], 
            user_comment_code: [], 

            //Biến dùng để lưu thread
            user_thread: [], 
            user_thread_code: [], 
            thread_comment: [], 
            thread_comment_code: [], 
        }
    }, 
    components: {
        FormUser, 
        Sidebar, 
        Navbar, 
        FormUpdateUser, 
        DetailUser, 
        CanhBao, 
    }, 
    created(){
        this.loadData(); 
    }, 
    watch: {
        pageSize: function(){
            BaseRequest.get("user/page?page=" + this.page + "&pageSize=" + this.pageSize)
            .then(response => {
                this.user = response.data; 
                console.log(this.user); 
            })
            .catch(error => {
                console.log(error.message); 
            })
        }
    },
    methods: {
        nextPageUser: function(){
            this.page++; 
            BaseRequest.get("user/page?page=" + this.page + "&pageSize=" + this.pageSize)
            .then(response => {
                this.user = response.data; 
                console.log(this.user); 
            })
            .catch(error => {
                console.log(error.message); 
            })
        }, 
        backPageUser: function(){
            this.page--; 
            BaseRequest.get("user/page?page=" + this.page + "&pageSize=" + this.pageSize)
            .then(response => {
                this.user = response.data; 
                console.log(this.user); 
            })
            .catch(error => {
                console.log(error.message); 
            })
        }, 
        loadData(){
            BaseRequest.get("user/page?page=" + this.page + "&pageSize=" + this.pageSize)
            .then(response => {
                this.user = response.data; 
                console.log(this.user); 
            })
            .catch(error => {
                console.log(error.message); 
            })
        },
        loadDataAfterReplace: function(){
            BaseRequest.get("user")
            .then(response => {
                this.user = response.data; 
                console.log(this.user); 
            })
            .catch(error => {
                console.log(error.message); 
            })
        }, 
        openFormUser: function(){
            document.getElementById("user_back").style.display = "block"; 
            document.getElementById("form_user").style.display = "block"; 
        }, 
        renderRole: function(item){
            if(item === 0){
                return "Người dùng"; 
            }
            else {
                return "Người quản trị"; 
            }
        },
        updateItem: function(item){
            this.getUser = item; 
            document.getElementById("form_update_user").style.display = "block"; 
            document.getElementById("user_back").style.display = "block"; 
        }, 
        seeDetailItem: function(item){
            this.getUser = item; 
            document.getElementById("detail-user").style.display = "block"; 
            document.getElementById("user_back").style.display = "block";
        }, 
        deleteItem: function(item){
            
            document.getElementById("thognbao").innerHTML = "Bạn có chắc chắn muốn xóa tài khoản có mã " +  item.userCode; 

            document.getElementById("ad-dialog-delete").style.display = "block"; 

            document.getElementById("ad-dialog-btn").addEventListener("click", ()=>{
                //Thực hiện xóa các dữ liệu ở các bảng liên quan. 
                //1. Bảng Score. 
                BaseRequest.get("score/user?userId=" + item.userId)
                .then(response => {
                    this.user_score = response.data; 
                    for (const item of this.user_score) {
                        this.user_score_code.push(item.scoreCode); 
                    }

                    //Kiểm tra xem có kết quả thi thử ko. 
                    if(this.user_score_code.length > 0){
                        BaseRequest.delete("score/any", this.user_score_code)
                        .then(response => {
                            console.log(response.data); 
                        })
                        .catch(error => {
                            console.log(error.message); 
                        })
                    }
                })

                //2. Bảng StudentAnswer 
                BaseRequest.get("student_answer/byuser?userId=" + item.userId)
                .then(response => {
                    this.user_answer = response.data; 
                    for (const item of this.user_answer) {
                        this.user_answer_code.push(item.answerCode); 
                    }

                    //Kiểm tra 
                    if(this.user_answer_code.length > 0){
                        BaseRequest.delete("student_answer", this.user_answer_code)
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

                //3.Bảng learning. 
                BaseRequest.get("learning/user?userId=" + item.userId)
                .then(response => {
                    this.user_learning = response.data; 
                    for (const item of this.user_learning) {
                        this.user_learning_code.push(item.learningCode); 
                    }

                    if(this.user_learning_code.length > 0){
                        BaseRequest.delete("learning/any", this.user_learning_code)
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

                //4. Bảng comment. 
                BaseRequest.get("comment/user?userId=" + item.userId)
                .then(response => {
                    this.user_comment = response.data; 
                    for (const item of this.user_comment) {
                        this.user_comment_code.push(item.commentCode); 
                    }

                    if(this.user_comment_code.length > 0){
                        BaseRequest.delete("comment/DeleteAny", this.user_comment_code)
                        .then(response => {
                            console.log(response.data); 
                        })
                        .catch(error => {
                            console.log(error.message); 
                        })
                    }
                })

                //Bảng thread
                BaseRequest.get("thread/user?userId=" + item.userId)
                .then(response => {
                    this.user_thread = response.data; 
                    if(this.user_thread.length > 0){
                        for (const item of this.user_thread) {
                            this.user_thread_code.push(item.threadsCode); 
                            //Xóa các comment trước. 
                            BaseRequest.get("comment/threadsId?threadsId=" + item.threadsId)
                            .then(response => {
                                this.thread_comment = response.data; 
                                if(this.thread_comment.length > 0){
                                    for (const item of this.thread_comment) {
                                        this.thread_comment_code.push(item.commentCode); 
                                    }
                                    BaseRequest.delete("comment/DeleteAny", this.thread_comment_code)
                                    .then(response => {
                                        console.log(response.data); 
                                    })
                                    .catch(error => {
                                        console.log(error.message); 
                                    })
                                }
                            })
                        }
                        //Sau khi xoa het comment của thread 
                        //Xoa thread 
                        BaseRequest.delete("thread/any", this.user_thread_code)
                        .then(response => {
                            console.log(response.data);
                        })
                        .catch(error => {
                            console.log(error.message); 
                        })
                    }
                })


                BaseRequest.delete("user?code=" + item.userCode)
                .then(response => {
                    console.log(response.data); 
                    document.getElementById("dialog_dangky_content").innerHTML = "Xóa người dùng thành công."
                    document.getElementById("ad-dialog-delete").style.display = "none"; 
                    document.getElementById("dialog_suauser").style.opacity = 1; 
                })
                .catch(error => {
                    console.log(error.message); 
                }) 
            })
        }, 
        searchUser: function(){
            var x = document.getElementById("search-user").value; 
            for (const item of this.user) {
                if(item.userCode === x || item.userType === x){
                    this.tg.push(item); 
                }
            }
            this.user = this.tg; 
        }, 
    }
}
</script>
<style>
/**Style thong bao xoa */
.success_deluser {
    width: 500px; 
    height: 56px;
    display: flex; 
    justify-content: space-between;
    align-items: center;
    position: absolute; 
    top: 20px; 
    left: 30%; 
    background-color: #fff;
    border-radius: 4px;
    font-size: 1.25rem;
    padding-left: 24px; 
    padding-right: 24px; 
    box-shadow: rgba(0,0,0,0.078) 0px 3px 6px 0px;
}
.success_deluser_title {
    height: 100%;
    display: flex; 
    z-index: 1; 
    width: 100%; 
    align-items: center; 
    gap: 16px; 
}
.hover_user {
    bottom: 5px; 
}
.user-admin > table > tbody > tr {
    height: 66px;
}
#user_back {
    display: none; 
}
.dialog_delete {
    width: 500px; 
    /* height: 100px; */
    position: absolute; 
    top: 200px; 
    left: 35%; 
    background-color: #fff;  
    padding: 24px; 
    border-radius: 4px;
    box-shadow: rgba(0,0,0,0.078) 0px 3px 6px 0px; 
    opacity: 0;
    transition: ease-out 0.3s;
    z-index: 2;
}
.icon_cauhoi {
    background: url('@/assets/Sprites.64af8f61.svg') no-repeat -752px -462px;
	width: 36px;
	height: 36px;
}
/* .dialog-delete-title {
    display: flex; 
    height: 100%;
    align-items: center;
    gap: 16px; 
    font-size: 1.25rem; 
} */
.dialog-delete-title-close {
    width: calc(100% - 24px -24px); 
    display: flex; 
    justify-content: space-between;
    margin: auto; 
}
.dialog-delete-title-close > div:first-child {
    font-size: 1.5rem;
    font-weight: 700; 
}
.dialog_delete-title {
    display: flex; 
    gap: 16px; 
    font-size: 1.25rem; 
    margin-top: 24px; 
}
.dialog-delete-btn {
    display: flex; 
    justify-content: flex-end;
    margin-top: 24px; 
    border-top: 1px solid #d0d0d0;
}
.dialog-delete-btn > button {
    height: 36px;
    border: none; 
    background-color: #50B83C; 
    padding-left: 16px; 
    padding-right: 16px; 
    color: #fff; 
    border-radius: 4px; 
    margin-top: 24px; 
}
#dialog_xoauser {
    display: block; 
}
</style>

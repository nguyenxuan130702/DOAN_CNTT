<template>
    <Navbar></Navbar>
    <Sidebar></Sidebar>
    <FormThread :propThread="thread"></FormThread>
    <FormUpdateThread :propGetThread="getThread"></FormUpdateThread>
    <div class="a-course">
        <h1>Quản lý bài đăng</h1>
        <div class="a-course_table">
            <div class="a-course_table-title">
                <div class="a-course_table-title-search">
                    <!-- <input type="date" id="search-course"> - 
                    <input type="date">
                    <div class="search-course" v-on:click="searchThread()">
                        <div class="icon_search"></div>
                    </div> -->
                    <button v-on:click="newThread()">Mới nhất</button>
                    <button v-on:click="oldThread()">Cũ nhất</button>
                </div>
                <div class="a-course_table-title-btn">
                    <button v-on:click="openFormPostThread()">Thêm mới</button>
                    <div class="icon_loaddata_sty">
                        <div class="icon_loaddata" v-on:click="loadDataThread()"></div>
                    </div>
                </div>
            </div>
            <div class="a-course_table-content">
                <table>
                    <thead>
                        <tr>
                            <td style="width: 100px;">Mã bài đăng</td>
                            <td style="width: 200px">Tên bài đăng</td>
                            <td style="width: 300px">Nội dung</td>
                            <td style="width: 200px">Người đăng</td>
                            <td>Chủ đề bài đăng</td>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-for="item in thread" :key="item.threadsId">
                            <td style="width: 100px; text-align: center;">{{ item.threadsCode }}</td>
                            <td style="width: 200px">{{ item.threadsTitle }}</td>
                            <td style="width: 300px">{{ item.threadsContent }}</td>
                            <td style="width: 200px" class="a-user-thread">
                                <img :src="item.userImage" alt="">
                            </td>
                            <td class="chucnang_course"> <div class="a-category-thread">{{ item.categoryName }}</div>
                                <div class="chucnang_course-style">
                                    <div class="updatehover" title="Sửa khóa học" v-on:click="openFormPutThread(item)">
                                        <div class="icon_update"></div>
                                    </div>
                                    <div class="deletehover" title="Xóa khóa học" v-on:click="deleteThread(item)">
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
                    Tổng: {{ thread.length }}
                </div>
                <div style="margin-right: 24px" class="a-course_table-page1">
                    <div>Số bản ghi/trang: 
                        <select name="" id="" v-model="pageSize">
                            <option value="10">10</option>
                            <option value="20">20</option>
                            <option value="30">30</option>
                        </select>
                    </div>
                    <div class="icon_back" v-on:click="backPageCourse()"></div>
                    <div class="icon_next" v-on:click="nextPageCourse()"></div>
                </div>
            </div>
        </div>
    </div>
    <div class="course_back" id="thread_back"></div>

    <CanhBao></CanhBao>
</template>
<script>
import Navbar from './layout/NavbarAdmin.vue'
import Sidebar from './layout/SidebarAdmin.vue'
import FormThread from './layout/FormThread.vue'
import BaseRequest from '@/core/BaseRequest'
import FormUpdateThread from './layout/FormUpdateThread.vue'
import CanhBao from './layout/DialogCanhbao.vue'
export default{
    components: {
        Navbar, 
        Sidebar, 
        FormThread, 
        FormUpdateThread, 
        CanhBao
    }, 
    data(){
        return{
            thread: [], 
            getThread: {}, 
            code_comment: [],
            comment: [],
            pageSize: 10, 
            page: 1,   
        }
    }, 
    mounted(){
        BaseRequest.get("threaduser/page?page=" + this.page + "&pageSize=" + this.pageSize)
        .then(response  => {
            this.thread = response.data; 
        }).catch(err => {
            console.log(err.message); 
        });
    }, 
    watch: {
        pageSize: function(){
            BaseRequest.get("threaduser/page?page=" + this.page + "&pageSize=" + this.pageSize)
            .then(response  => {
                this.thread = response.data; 
            }).catch(err => {
                console.log(err.message); 
            });
        }
    }, 
    methods: {
        backPageCourse: function(){
            this.page--; 
            BaseRequest.get("threaduser/page?page=" + this.page + "&pageSize=" + this.pageSize)
            .then(response  => {
                this.thread = response.data; 
            }).catch(err => {
                console.log(err.message); 
            });
        }, 
        nextPageCourse: function(){
            this.page++; 
            BaseRequest.get("threaduser/page?page=" + this.page + "&pageSize=" + this.pageSize)
            .then(response  => {
                this.thread = response.data; 
            }).catch(err => {
                console.log(err.message); 
            });
        }, 
        newThread: function(){
            BaseRequest.get("threaduser")
            .then(response  => {
                this.thread = response.data; 
            }).catch(err => {
                console.log(err.message); 
            });
        }, 
        oldThread: function(){
            this.thread.reverse(); 
        }, 
        loadDataThread: function(){
            BaseRequest.get("threaduser")
            .then(response  => {
                this.thread = response.data; 
            }).catch(err => {
                console.log(err.message); 
            });
        }, 
        openFormPostThread: function(){
            document.getElementById("form_thread").style.display = "block"; 
            document.getElementById("thread_back").style.display = "block"; 
        }, 
        openFormPutThread: function(item){
            this.getThread = item; 
            document.getElementById("form_update_thread").style.display = "block"; 
            document.getElementById("thread_back").style.display = "block"; 
        }, 
        deleteThread: function(item){
            document.getElementById("thognbao").innerHTML = "Bạn có chắc chắn muốn xóa chủ đề có mã " +  item.threadsCode;

            document.getElementById("ad-dialog-delete").style.display = "block";
            document.getElementById("ad-dialog-btn").addEventListener("click", ()=>{
                //Đầu tiên là phải kiểm tra xem trong thread có bao nhiêu comment, nếu có comment thì phải xóa các comment đi trước. 
                BaseRequest.get("comment/threadsId?threadsId=" + item.threadsId)
                .then(response => {
                    this.comment = response.data; 
                    //lưu mã câu hỏi vào trong mảng mã 
                    for (const x of this.comment) {
                        this.code_comment.push(x.commentCode); 
                    }
                    if(this.code_comment.length === 0){
                        //Thực hiện xóa bản ghi thread 
                        BaseRequest.delete("thread?code=" + item.threadsCode)
                        .then(response => {
                            //thông báo thành công. 
                            console.log(response.data); 
                            document.getElementById("ad-dialog-delete").style.display = "none"; 
                            document.getElementById("dialog_thread").style.opacity = 1;
                        })
                        .catch(error => {
                            console.log(error.message); 
                        })
                    }
                    else {
                        //Thực hiện xóa các comment khỏi bảng comment sau đó xóa thread. 
                        BaseRequest.delete("comment/DeleteAny", this.code_comment)
                        .then(response => {
                            //thực hiện xóa thread khỏi database 
                            console.log(response.data); 
                            BaseRequest.delete("thread?code=" + item.threadsCode)
                            .then(response => {
                                //thông báo thành công. 
                                console.log(response.data); 
                                document.getElementById("ad-dialog-delete").style.display = "none"; 
                                document.getElementById("dialog_thread").style.opacity = 1;
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
.a-course_table-title-search > button {
    height: 36px;
    width: 120px; 
    border-radius: 4px;
    border: 1px solid #d0d0d0; 
}
.a-course_table-title-search > button:hover {
    background-color: #50B83C;
    color: #fff; 
}
.a-user-thread {
    display: flex; 
    justify-content: center;
    align-items: center;
    border-top: none; 
    border-bottom: none; 
    border-left: none; 
}
.a-user-thread > img {
    width: 50px;
    height: 50px;
    border-radius: 50%;
}
.a-category-thread {
    margin-left: -84px; 
}
</style>

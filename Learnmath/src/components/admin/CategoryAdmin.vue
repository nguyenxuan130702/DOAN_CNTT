<template>
    <Navbar></Navbar>
    <Sidebar></Sidebar>
    <FormCategory :propCategory="category"></FormCategory>
    <FormUpdateCategory :propGetCategory="getCategory"></FormUpdateCategory>
    <div class="a-course">
        <h1>Quản lý chủ đề bàn luận</h1>
        <div class="a-course_table">
            <div class="a-course_table-title">
                <div class="a-course_table-title-search">
                    <input type="text" placeholder="Tìm kiếm theo mã, tên chủ đề" id="search-category">
                    <div class="search-course" v-on:click="searchCourse()">
                        <div class="icon_search"></div>
                    </div>
                </div>
                <div class="a-course_table-title-btn">
                    <button v-on:click="openFormPostCategory()">Thêm mới</button>
                    <div class="icon_loaddata_sty">
                        <div class="icon_loaddata" v-on:click="loadDataCategory()"></div>
                    </div>
                </div>
            </div>
            <div class="a-course_table-content">
                <table>
                    <thead>
                        <tr>
                            <td style="width: 200px;">Mã chủ đề</td>
                            <td style="width: 300px">Tên chủ đề</td>
                            <td >Mô tả</td>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-for="item in category" :key="item.categoryId">
                            <td style="width: 200px; text-align: center;">{{ item.categoryCode }}</td>
                            <td style="width: 300px">{{ item.categoryName }}</td>
                            <td class="chucnang_course"> {{ item.categoryDescription }}
                                <div class="chucnang_course-style">
                                    <div class="updatehover" title="Sửa khóa học" v-on:click="openFormPutCategory(item)">
                                        <div class="icon_update"></div>
                                    </div>
                                    <div class="deletehover" title="Xóa khóa học" v-on:click="deleteCategory(item)">
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
                    Tổng: {{ category.length }}
                </div>
                <div style="margin-right: 24px" class="a-course_table-page1">
                    <div>Số bản ghi/trang: 
                        <select name="" id="" v-model="pageSize">
                            <option value="10">10</option>
                            <option value="20">20</option>
                            <option value="30">30</option>
                        </select>
                    </div>
                    <div class="icon_back" v-on:click="backPageCategory()"></div>
                    <div class="icon_next" v-on:click="nextPageCategory()"></div>
                </div>
            </div>
        </div>
    </div>

    <div class="course_back" id="category_back"></div>
    <CanhBao></CanhBao>
</template>
<script>
import BaseRequest from '@/core/BaseRequest'
import FormCategory from './layout/FormCategory.vue'
import Navbar from './layout/NavbarAdmin.vue'
import Sidebar from './layout/SidebarAdmin.vue'
import FormUpdateCategory from './layout/FormUpdateCategory.vue'
import CanhBao from './layout/DialogCanhbao.vue'
export default {
    components: {
        Navbar, 
        Sidebar, 
        FormCategory, 
        FormUpdateCategory, 
        CanhBao
    }, 
    data(){
        return {
            category: [], 
            getCategory: {},
            tg: [],  
            thread: [],
            code_thread: [], 
            comment_thread: [], 
            code_comment: [],  
            pageSize: 10, 
            page: 1, 
        }
    }, 
    mounted(){
        this.loadData(); 
    }, 
    watch: {
        pageSize: function(){
            BaseRequest.get("category/page?page=" + this.page + "&pageSize=" + this.pageSize)
            .then(response => {
                this.category = response.data; 
            })
            .catch(error => {
                console.log(error.message); 
            })
        }
    }, 
    methods: {
        backPageCategory: function(){
            this.page--; 
            BaseRequest.get("category/page?page=" + this.page + "&pageSize=" + this.pageSize)
            .then(response => {
                this.category = response.data; 
            })
            .catch(error => {
                console.log(error.message); 
            })
        }, 
        nextPageCategory: function(){
            this.page++; 
            BaseRequest.get("category/page?page=" + this.page + "&pageSize=" + this.pageSize)
            .then(response => {
                this.category = response.data; 
            })
            .catch(error => {
                console.log(error.message); 
            })
        }, 
        loadDataCategory: function(){
            BaseRequest.get("category/page?page=" + this.page + "&pageSize=" + this.pageSize)
            .then(response => {
                this.category = response.data; 
            })
            .catch(error => {
                console.log(error.message); 
            })
        }, 
        loadData: function(){
            BaseRequest.get("category")
            .then(response => {
                this.category = response.data; 
            })
            .catch(error => {
                console.log(error.message); 
            })
        }, 
        searchCourse: function(){
            var x = document.getElementById("search-category").value; 
            for (const item of this.category) {
                if(item.categoryCode === x || item.categoryName === x){
                    this.tg.push(item); 
                }
            }
            this.category = this.tg; 
        }, 
        openFormPostCategory: function(){
            document.getElementById("form_category").style.display = "block"; 
            document.getElementById("category_back").style.display = "block"; 
        }, 
        openFormPutCategory: function(item){
            this.getCategory = item; 
            document.getElementById("form_update_category").style.display = "block";
            document.getElementById("category_back").style.display = "block"; 
        }, 
        deleteCategory: function(item){
            document.getElementById("thognbao").innerHTML = "Bạn có chắc chắn muốn xóa chủ đề có mã " +  item.categoryCode;

            document.getElementById("ad-dialog-delete").style.display = "block";

            document.getElementById("ad-dialog-btn").addEventListener("click", ()=>{
                //Kiểm tra xem trong category có bao nhiêu thread 
                //Nếu có thread thì phải xóa các thread trước. 
                BaseRequest.get("thread/categoryId?categoryId=" + item.categoryId)
                .then(response => {
                    this.thread = response.data; 
                    //gán danh sách mã code của thread vào mảng 
                    for (const x of this.thread) {
                        this.code_thread.push(x.threadsCode); 
                    }
                    //thực hiển xóa từng cái comment trong thread 1. 
                    if(this.code_thread.length !== 0){
                        for (const y of this.thread) {
                            //kiểm tra có comment hay ko. 
                            BaseRequest.get("comment/threadsId?threadsId=" + y.threadsId)
                            .then(response => {
                                this.comment_thread = response.data; 
                                //lưu mã câu hỏi vào trong mảng mã 
                                for (const z of this.comment_thread) {
                                    this.code_comment.push(z.commentCode); 
                                }
                                //Nếu ko có comment thì xóa thread. 
                                if(this.code_comment.length === 0){
                                    //Thực hiện xóa bản ghi thread 
                                    BaseRequest.delete("thread?code=" + y.threadsCode)
                                    .then(response => {
                                        console.log(response.data); 
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
                                        BaseRequest.delete("thread?code=" + y.threadsCode)
                                        .then(response => {
                                            //thông báo thành công. 
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

                        //Sau khi xóa hết các thread có trong category thì thực hiện xóa category. 
                        BaseRequest.delete("category?code=" + item.categoryCode)
                        .then(response => {
                            console.log(response.data); 
                            document.getElementById("dialog_dangky_content").innerHTML = "Xóa thành công."; 
                            document.getElementById("ad-dialog-delete").style.display = "none"; 
                            document.getElementById("dialog_category").style.opacity = 1;
                        })
                        .catch(error => {
                            console.log(error.message); 
                        })
                    }
                    else {
                        //Nếu ko có thread nào thì thực hiện xóa category khỏi database 
                        BaseRequest.delete("category?code=" + item.categoryCode)
                        .then(response => {
                            console.log(response.data); 
                            document.getElementById("dialog_dangky_content").innerHTML = "Xóa thành công."; 
                            document.getElementById("ad-dialog-delete").style.display = "none"; 
                            document.getElementById("dialog_category").style.opacity = 1;
                        })
                        .catch(error => {
                            console.log(error.message); 
                        })
                    }
                })
            })
        }
    }
}
</script>
<style scoped>
.category-admin {
    width: calc(100% - 250px); 
    float: left;
}
</style>

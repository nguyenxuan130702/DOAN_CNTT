<template>
    <Navbar></Navbar>
    <Sidebar></Sidebar>
    <FormNew :propNews="news"></FormNew>
    <FormUpdateNew :propGetNew="getNew"></FormUpdateNew>
    <DetailNew :propGetNew="getNew"></DetailNew>
    <div class="a-course">
        <h1>Quản lý bản tin</h1>
        <div class="a-course_table">
            <div class="a-course_table-title">
                <div class="a-course_table-title-search">
                    <input type="text" placeholder="Tìm kiếm theo mã, chủ đề khóa học" id="search-new">
                    <div class="search-course" v-on:click="searchNew()">
                        <div class="icon_search"></div>
                    </div>
                </div>
                <div class="a-course_table-title-btn">
                    <button v-on:click="openFormPostNews()">Thêm mới</button>
                    <div class="icon_loaddata_sty">
                        <div class="icon_loaddata" v-on:click="loadDataNew()"></div>
                    </div>
                </div>
            </div>
            <div class="a-course_table-content">
                <table>
                    <thead>
                        <tr>
                            <td style="width: 100px;">Mã bài viết</td>
                            <td style="width: 400px">Tiêu đề bài viết</td>
                            <td style="width: 120px">Ngày tạo</td>
                            <td style="text-align: left; padding-left: 16px;">Ảnh bài viết</td>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-for="item in news" :key="item.newId">
                            <td style="width: 100px; text-align: center;">{{ item.newCode }}</td>
                            <td style="width: 400px">{{ item.newTitle }}</td>
                            <td style="width: 120px">{{ renderDate(item.newDate )}}</td>
                            <td class="chucnang_course image_new">
                                <img :src="item.newImage" alt="">
                                <div class="chucnang_course-style">
                                    <div class="seehover" title="Xem chi tiết" v-on:click="seeDetailPut(item)">
                                        <div class="icon_zoom"></div>
                                    </div>
                                    <div class="updatehover" title="Sửa khóa học" v-on:click="openFormPutNew(item)">
                                        <div class="icon_update"></div>
                                    </div>
                                    <div class="deletehover" title="Xóa khóa học" v-on:click="deletePut(item)">
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
                    Tổng: {{ news.length }}
                </div>
                <div style="margin-right: 24px" class="a-course_table-page1">
                    <div>Số bản ghi/trang: 
                        <select name="" id="" v-model="pageSize">
                            <option value="10">10</option>
                            <option value="20">20</option>
                            <option value="30">30</option>
                        </select>
                    </div>
                    <div class="icon_back" v-on:click="backPageNew()"></div>
                    <div class="icon_next" v-on:click="nextPageNew()"></div>
                </div>
            </div>
        </div>
    </div>
    <div class="course_back" id="news_back"></div>
    <CanhBao></CanhBao>
</template>
<script>
import BaseRequest from '@/core/BaseRequest'
import FormNew from './layout/FormNew.vue'
import Navbar from './layout/NavbarAdmin.vue'
import Sidebar from './layout/SidebarAdmin.vue'
import FormUpdateNew from './layout/FormUpdateNew.vue'
import CanhBao from './layout/DialogCanhbao.vue'
import DetailNew from './layout/DetailNew.vue'
export default{
    components: {
        FormNew,
        Navbar, 
        Sidebar, 
        FormUpdateNew, 
        CanhBao, 
        DetailNew
    }, 
    data(){
        return {
            news: [], 
            getNew: {}, 
            new_code: '', 
            new_title: '', 
            new_date: '', 
            new_image: '', 
            pageSize: 10, 
            page: 1, 
            new_search: [],
            tg: [],  
        }
    }, 
    created(){
        BaseRequest.get("news/page?page=" + this.page + "&pageSize=" + this.pageSize)
        .then(response => {
            this.news = response.data; 
        })
        .catch(error => {
            console.log(error.message); 
        })
    }, 
    watch: {
        pageSize: function(){
            BaseRequest.get("news/page?page=" + this.page + "&pageSize=" + this.pageSize)
            .then(response => {
                this.news = response.data; 
            })
            .catch(error => {
                console.log(error.message); 
            })
        }
    }, 
    methods: {
        searchNew: function(){
            var x = document.getElementById("search-new").value; 
            BaseRequest.get("news")
            .then(response => {
                this.search_new = response.data;
                this.tg = [];  
                for (const item of this.search_new) {
                    if(item.newCode === x || item.newTitle === x){
                        this.tg.push(item); 
                    }
                }
                this.news = this.tg; 
                console.log(this.news); 
            })
            .catch(error => {
                console.log(error.message); 
            })
        }, 
        seeDetailPut: function(item){
            this.getNew = item; 
            document.getElementById("form_news_detail").style.display = "block"; 
            document.getElementById("news_back").style.display = "block"; 
        }, 
        backPageNew: function(){
            this.page--; 
            BaseRequest.get("news/page?page=" + this.page + "&pageSize=" + this.pageSize)
            .then(response => {
                this.news = response.data; 
            })
            .catch(error => {
                console.log(error.message); 
            })
        }, 
        nextPageNew: function(){
            this.page++; 
            BaseRequest.get("news/page?page=" + this.page + "&pageSize=" + this.pageSize)
            .then(response => {
                this.news = response.data; 
            })
            .catch(error => {
                console.log(error.message); 
            })
        }, 
        openFormPostNews: function(){
            document.getElementById("news_back").style.display = "block"; 
            document.getElementById("form_news").style.display = "block"; 
        },
        /**
         * Hàm chuyển đổi ngày tháng. 
         * created by: Nguyễn Thị Xuân - 22/1/2024
         */
        renderDate: function(date1){
            let dateObject = new Date(date1);

            let formattedDate = `${dateObject.getDate()}/${dateObject.getMonth() + 1}/${dateObject.getFullYear()}`;

            return formattedDate; 
        },
        openFormPutNew: function(item){
            this.getNew = item; 
            document.getElementById("form_news_put").style.display = "block";
            document.getElementById("news_back").style.display = "block"; 
        }, 
        deletePut: function(item){
            document.getElementById("thognbao").innerHTML = "Bạn có chắc chắn muốn xóa bản tin có mã " +  item.newCode; 

            document.getElementById("ad-dialog-delete").style.display = "block"; 

            document.getElementById("ad-dialog-btn").addEventListener("click", ()=>{
                BaseRequest.delete("news?code=" + item.newCode)
                .then(response => {
                    console.log(response.data); 
                    document.getElementById("dialog_dangky_content").innerHTML = "Xóa bản tin thành công."
                    document.getElementById("news_back").style.display = "none"; 
                    document.getElementById("ad-dialog-delete").style.display = "none"; 
                    document.getElementById("dialog_new").style.opacity = "1";
                })
                .catch(error => {
                    console.log(error.message); 
                }) 
            })
        }, 
        loadDataNew: function(){
            BaseRequest.get("news")
            .then(response => {
                this.news = response.data; 
            })
            .catch(error => {
                console.log(error.message); 
            })
        }
        
    }, 
}
</script>
<style scoped>
.image_new > img {
    width: 200px; 
    height: 150px; 
}
.update_new {
    bottom: 400px !important; 
}
</style>

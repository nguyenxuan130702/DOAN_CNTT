<template>
    <div class="forum">
        <NavbarForum></NavbarForum>
        <div class="forum-page-loc">
            <div class="forum-page-option-item" v-on:click="newForm()">Mới nhất</div>
            <div class="forum-page-option-item" v-on:click="oldForm()">Cũ nhất</div>
        </div>
        <div class="forum-page">
            <div class="forum-chude" data-aos="fade-down" data-aos-duration="1500">
                <div class="forum-chude-mota">
                    <p>Chủ đề</p>
                    <p>Số bài đăng</p>
                </div>
                <div class="forum-chude-mota" v-for="item in category" :key="item.categoryId">
                    <div class="forum-chude-mota-title">
                        <router-link :to="/Category/ + item.categoryId" class="forum-link">{{ item.categoryName }}</router-link>
                        <div>{{ item.categoryDescription }}</div>
                    </div>
                    <div>{{ item.countThread }}</div>
                </div>
            </div>
            <div class="forum-baidang" data-aos="fade-up" data-aos-duration="1500">
                <div class="forum-baidang-mota">
                    <p>Bài đăng</p>
                    <p>Bình luận/Thời gian</p>
                </div>
                <div class="forum-baidang-mota" v-for="item in thread" :key="item.threadsId">
                    <div class="forum-baidang-mota-face">
                        <img :src="item.userImage" alt="">
                    </div>
                    <div class="forum-baidang-mota-title">
                        <router-link :to="/DetailThread/ + item.threadsId" class="forum-link">{{ item.threadsTitle }}</router-link>
                        <div class="forum-baidang-mota-title-chude">{{ item.categoryName }}</div>
                    </div>
                    <div>
                        <p>{{ item.countComment }}</p>
                        <p style="margin-top: -16px;">{{ renderDate(item.created_at) }}</p>
                    </div>
                </div>
                <div class="thread-forum-page">
                    <div>Tổng: 10</div>
                    <div class="a-course_table-page1">
                        <div>Số bản ghi/trang: 
                            <select name="" id="" v-model="page">
                                <option value="10">10</option>
                                <option value="15">15</option>
                                <option value="20">20</option>
                            </select>
                        </div>
                        <div class="icon_back" v-on:click="backPageNew(pagenow)"></div>
                        <div class="icon_next" v-on:click="nextPageNew(pagenow)"></div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <ChatBot></ChatBot>
</template>
<script>
import BaseRequest from '@/core/BaseRequest'
import NavbarForum from './layout/NavbarForum.vue'
import ChatBot from './mathjax/ChatBox.vue'
export default{
    components: {
        NavbarForum, 
        ChatBot
    }, 
    data(){
        return {
            category: [],
            category_thread: [], 
            thread_comment: [],
            thread: [],    
            page: 10, 
            pagenow: 1, 
            tg: [], 
        }
    }, 
    created(){
        //Lay ra thong tin cua chu de
        BaseRequest.get("category")
        .then(response => {
            this.category = response.data; 
        })
        .catch(error => {
            console.log(error.message); 
        })

        //Lay ra thong tin cua chu de va dem so bai dang co trong chu de 
        BaseRequest.get("viewthread")
        .then(response => {
            this.category_thread = response.data;

            for (const item of this.category) {
                item.countThread = 0; 
                for (const item1 of this.category_thread) {
                    if(item.categoryId === item1.categoryId){
                        item.countThread = item1.countThread; 
                    }
                }
            }
        })
        .catch(error => {
            console.log(error.message); 
        })


        //lấy ra danh sách các thread có trong hệ thống 
        BaseRequest.get("threaduser/page?page=" + this.pagenow + "&pageSize=" + this.page)
        .then(response => {
            this.thread = response.data; 
        })
        .catch(error => {
            console.log(error.message); 
        }); 

        //lay ra thong tin cua bai dang cung voi so luong nguoi tra loi 
        BaseRequest.get("threadscomment")
        .then(response => {
            this.thread_comment = response.data; 
            for (const item of this.thread) {
                item.countComment = 0; 
                for (const item1 of this.thread_comment) {
                    if(item.threadsId === item1.threadsId){
                        item.countComment = item1.countComment; 
                    }
                }
            }
        })
        .catch(error => {
            console.log(error.message); 
        })

        //Kiểm tra xem có threadnew ko 
        
    },
    methods: {
        newForm: function(){
            BaseRequest.get("threaduser/page?page=" + this.pagenow + "&pageSize=" + this.page)
            .then(response => {
                this.thread = response.data; 
            })
            .catch(error => {
                console.log(error.message); 
            }); 
        }, 
        oldForm: function(){
            for (var i = this.thread.length - 1; i>=0; i--) {
                this.tg.push(this.thread[i]); 
            }
            this.thread = this.tg; 
        }, 
        renderDate: function(item){
            var date = new Date(item); 
            var d = date.getDate(); 
            var m = date.getMonth() + 1; 
            var y = date.getFullYear();
            return d + "/" + m + "/" + y; 
        }, 
        nextPageNew: function(item){
            item++; 
            BaseRequest.get("threaduser/page?page=" + item + "&pageSize=" + this.page)
            .then(response => {
                this.thread = response.data; 
            })
            .catch(error => {
                console.log(error.message); 
            }); 

            //lay ra thong tin cua bai dang cung voi so luong nguoi tra loi 
            BaseRequest.get("threadscomment")
            .then(response => {
                this.thread_comment = response.data; 
                for (const item of this.thread) {
                    item.countComment = 0; 
                    for (const item1 of this.thread_comment) {
                        if(item.threadsId === item1.threadsId){
                            item.countComment = item1.countComment; 
                        }
                    }
                }
            })
            .catch(error => {
                console.log(error.message); 
            })
        }, 
        backPageNew: function(item){
            item--; 
            BaseRequest.get("threaduser/page?page=" + item + "&pageSize=" + this.page)
            .then(response => {
                this.thread = response.data; 
            })
            .catch(error => {
                console.log(error.message); 
            }); 

            //lay ra thong tin cua bai dang cung voi so luong nguoi tra loi 
            BaseRequest.get("threadscomment")
            .then(response => {
                this.thread_comment = response.data; 
                for (const item of this.thread) {
                    item.countComment = 0; 
                    for (const item1 of this.thread_comment) {
                        if(item.threadsId === item1.threadsId){
                            item.countComment = item1.countComment; 
                        }
                    }
                }
            })
            .catch(error => {
                console.log(error.message); 
            })
        }
    }
}
</script>
<style scoped>
.thread-forum-page {
    display: flex; 
    justify-content: space-between;
    width: 100%; 
    color: #fff; 
    padding-top: 16px; 
}
.forum-link {
    text-decoration: none;
    color: #fff; 
}
.forum {
    width: 100%; 
    background-color: #36364b; 
}

.forum-page {
    width: 70%; 
    display: flex; 
    justify-content: space-between;
    margin: auto; 
    padding-bottom: 80px; 
}
.forum-page-option {
    width: 70%; 
    padding-top: 120px; 
    padding-bottom: 24px; 
    display: flex;
    gap: 16px;  
    margin: auto; 
}
.forum-chude {
    width: 45%; 
}
.forum-baidang{
    width: 45%; 
}
.forum-chude-mota {
    width: 100%; 
    display: flex; 
    justify-content: space-between;
    padding-top: 16px; 
    padding-bottom: 16px; 
    border-bottom: 3px solid #878893; 
}
.forum-chude-mota > div {
    font-size: 1.25rem; 
    color: #878893; 
}
.forum-chude-mota-title > h1 {
    font-size: 1.5rem; 
    font-weight: 700;
    color: #fff; 
}
.forum-chude-mota-title {
    text-decoration: none;
}
.forum-chude-mota-title > div {
    font-size: 1.25rem; 
    color: #878893; 
}
.forum-chude-mota > p {
    font-size: 1.25rem; 
    color: #878893; 
}
.forum-baidang-mota {
    width: 100%; 
    display: flex; 
    align-items: center;
    gap: 16px; 
    font-size: 1.25rem; 
    color: #878893; 
    padding-bottom: 16px; 
    padding-top: 16px; 
    border-bottom: 3px solid #878893;
}
.forum-baidang-mota:first-child {
    justify-content: space-between;
}
.forum-baidang-mota-face > img {
    width: 50px; 
    height: 50px;
}
.forum-baidang-mota-title {
    width: 60%; 
}
.forum-baidang-mota-title-chude { 
    margin-top: 8px;  
    width: auto; 
    display: flex; 
    justify-content: center; 
    background-color: #8FE0FF;
    color: black; 
}
.forum-page-option-item {
    width: 200px; 
    border: 5px solid #a9aab2; 
    position: relative; 
    display: flex;
    align-items: center;
    justify-content: center;
    font-size: 1.25rem; 
    color: #fff; 
}
.forum-page-option-item:hover {
    cursor: pointer;
}
.forum-page-option-item-cd {
    width: 100%; 
    position: absolute; 
    top: 40px; 
    left: 0; 
    border: 1px solid #878893; 
    background-color: #36364b;
    z-index: 1;
}
.forum-page-option-item-cd > div {
    padding-top: 16px; 
    padding-bottom: 16px; 
    padding-left: 8px; 
    color: #878893;
}
.forum-page-option-item-cd > div:hover {
    cursor: pointer;
    background-color: #a9aab2; 
    color: #36364b; 
}
.forum-page-loc {
    display: flex; 
    gap: 24px; 
    width: 70%; 
    justify-content: flex-end;
    margin: auto; 
    margin-bottom: 24px; 
}
</style>

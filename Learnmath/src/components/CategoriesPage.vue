<template>

    <div class="category">
        <!---Navbar forum -->
        <div class="forum-page-option">
            <!--Chọn chủ đề-->
            <div class="forum-page-option-item" style="width: 300px" v-on:click="openChuDe()">
                <div id="">{{ chude }}</div>
                <div class="forum-page-option-item-cd" v-show="open_chude">
                    <div v-show="hover_chude">
                        <router-link to="/Forum" class="category-link-cd">Chủ đề</router-link>
                    </div>
                    <div v-on:click="showChude(item.categoryName, item.categoryId)" v-for="item in category" :key="item.categoryId">
                        <router-link :to="/Category/ + item.categoryId" class="category-link-cd">{{ item.categoryName }}</router-link>
                    </div>
                </div>
            </div>
            <!--Chọn lọc ra bài đăng mới nhất-->
            <div class="forum-page-option-item" v-on:click="newThread()">Mới nhất</div>
            <!--Chọn lọc ra bài đăng cũ nhất-->
            <div class="forum-page-option-item" v-on:click="oldThread()">Cũ nhất</div>
            <!--Click vào tạo bài đăng.--->
            <div class="forum-page-option-item push-chude" v-on:click="openCreateTopic()" v-show="threadbutton">Tạo bài đăng</div>
        </div>
        <!--Form cho phép người dùng nhập thông tin thêm mới bài đăng.-->
        <div class="forum-page-post" v-show="showPostTopic">
            <div class="forum-page-post-option">
                <div class="forum-page-post-post">
                    <div>
                        <div class="icon_post"></div>
                    </div>
                    <div>Tạo một bài đăng mới</div>
                </div>
                <div id="buggtopic"></div>
                <div>
                    <div class="icon_close" v-on:click="closePostTopic()"></div>
                </div>
            </div>
            <div class="forum-page-post-content">
                <div class="post-content-insert">
                    <div style="width: 64%">
                        <input type="text" name="c-title" id="c-title" placeholder="Hãy tạo một chủ đề ngắn để thảo luận" v-model="thread_title">
                    </div>
                    <div style="width: 34%">
                        <select name="c-category" id="c-category" v-model="thread_category">
                            <option :value="item.categoryId" v-for="item in category" :key="item.categoryId">{{ item.categoryName }}</option>
                        </select>
                    </div>
                    <div style="width: 100%">
                        <textarea name="c-content" id="c-content" cols="30" rows="10" placeholder="Nhập nội dung ở đây" v-model="topic_content"></textarea>
                    </div>
                    <div style="width: 40%; font-size: 1.25rem;">
                        Chọn ảnh (nếu có) <br>
                        <input type="file" @change="checkFileUploadNew">
                    </div>
                </div>
                <div class="post-content-visible" style="width: 50%">{{ topic_content}}</div>
            </div>
            <div class="post-content-dang">
                <div class="post-content-dang-icon" v-on:click="postThread()">
                    <div>
                        <div class="icon_post"></div>
                    </div>
                    <div>Tạo chủ đề</div>
                </div>
                <div>Làm mới</div>
            </div>
        </div>



        <!--Navbar forum--->
        <!-- <NavbarForum></NavbarForum> -->
        <div class="category-page">
            <div class="category-topic">
                <p>Topic</p>
                <div style="display: flex; gap: 84px">
                    <p>Replies</p>
                    <p>Activity</p>
                </div>
            </div>
            <div class="category-topic" v-for="item in threads" :key="item.threadsId">
                <div>
                    <router-link :to="/DetailThread/ + item.threadsId" class="category-link-cd">{{ item.threadsTitle }}</router-link>
                    <div class="category_chude-topic">{{ item.categoryName }}</div>
                </div>
                <div class="category-topic-info">
                    <div>
                        <img :src="item.userImage" alt="">
                    </div>
                    <div>{{ item.countComment }}</div>
                    <div>{{ renderDate(item.created_at) }}</div>
                </div>
            </div>
            <div class="category-forum-page">
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

        <ChatBot></ChatBot>
    </div>
</template>
<script>
import BaseRequest from '@/core/BaseRequest'
import ChatBot from './mathjax/ChatBox.vue'
export default{
    components: {
        ChatBot
    }, 
    data(){
        return {
            threads: [],
            replace: "", 
            threads_comment: [], 
            category: [], 
            threadbutton: true,
            chude: JSON.parse(localStorage.getItem("categoryName")), 
            open_chude: false, 
            hover_chude: true, 
            showPostTopic: false, 
            thread_title: "", 
            thread_category: "", 
            topic_content: "", 
            file: "", 
            thread_code: "", 
            index: 0, 
            user: JSON.parse(localStorage.getItem("userLogin")), 
            tg: [], 
        }
    }, 
    created(){
        this.loadData(); 
        if(this.user === null || this.user === undefined || this.user === ""){
            this.threadbutton = false; 
        }
    }, 
    methods: {
        /**
         * Hàm mở form thêm mới bài đăng phía người dùng. 
         */
        openCreateTopic: function(){
            this.showPostTopic = true; 
        }, 
        /**
         * Hàm đóng form thêm mới bài đăgn phía người dùng. 
         */
        closePostTopic: function(){
            this.showPostTopic = false; 
        },
        /**
         * Hàm mở lựa chọn chủ đề muốn xem 
         */
        openChuDe: function(){
            this.open_chude = !this.open_chude; 
        },  
        /**
         * Khi click vào tên chủ đề muốn xem các bài đăng thì nó sẽ load lại dữ liệu threads và hiển thị lại dữ liệu lên màn hình.
         */
        showChude: function(name, id){
            this.chude = name; 
            BaseRequest.get("threaduser/categoryId?categoryId=" + id)
            .then(response => {
                this.threads = response.data;
                console.log(this.threads); 
            })
            .catch(error => {
                console.log(error.message); 
            }); 

            //lấy ra danh sách các thread có comment rồi trong chủ đề đang xét. 
            BaseRequest.get("threadscomment/categoryId?categoryId=" + id)
            .then(response => {
                this.threads_comment = response.data;
                 
                for (const item of this.threads) {
                    item.countComment = 0; 
                    for (const item1 of this.threads_comment) {
                        if(item.threadsId === item1.threadsId){
                            item.countComment = item1.countComment;
                            break;  
                        }
                    }
                } 
            })
            .catch(error => {
                console.log(error.message); 
            })
        }, 
        /**
         * Hàm này dùng để load dữ liệu. 
         */
        loadData: function(){
            //Lấy ra danh sách chủ đề có trong hệ thống. 
            BaseRequest.get("category")
            .then(response => {
                this.category = response.data; 
                this.thread_category = this.category[0].categoryId; 
            })
            .catch(error => {
                console.log(error.message); 
            })

            //Lấy ra số lượng thread hiện có nhằm mục đích để tạo mã thread tự động tăng khi thêm mới 1 thread. 
            BaseRequest.get("thread")
            .then(response => {
                this.index = response.data.length + 1; 
            })
            .catch(error => {
                console.log(error.message); 
            })

            //lấy ra danh sách các thread thuộc vào chủ đề hiện tại. 
            BaseRequest.get("threaduser/categoryId?categoryId=" + this.$route.params.id)
            .then(response => {
                this.threads = response.data;
                console.log(this.threads); 
            })
            .catch(error => {
                console.log(error.message); 
            }); 

            //lấy ra danh sách các thread có comment rồi trong chủ đề đang xét. 
            BaseRequest.get("threadscomment/categoryId?categoryId=" + this.$route.params.id)
            .then(response => {
                this.threads_comment = response.data;
                 
                for (const item of this.threads) {
                    item.countComment = 0; 
                    for (const item1 of this.threads_comment) {
                        if(item.threadsId === item1.threadsId){
                            item.countComment = item1.countComment;
                            break;  
                        }
                    }
                } 
            })
            .catch(error => {
                console.log(error.message); 
            })

        }, 
        /**
         * Hàm này dùng để render dữ liệu kiểu datetime nhận được từ phía server sang định dạng người dùng. 
         */
        renderDate: function(item){
            var date = new Date(item); 
            var d = date.getDate(); 
            var m = date.getMonth() + 1; 
            var y = date.getFullYear();
            if(d < 10){
                if(m < 10){
                    return "0" + d + "/0" + m + "/" + y; 
                }
                else {
                    return "0" + d + "/" + m + "/" + y; 
                }
            }
            else {
                if(m < 10){
                    return d + "/0" + m + "/" + y; 
                }
                else {
                    return d + "/" + m + "/" + y; 
                }
            }
        }, 
        /**
         * Hàm này dùng để thêm mới bài đăng. 
         */
        postThread: function(){
            //validate rỗng
            const inputTitle = document.getElementById("c-title"); 
            const inputContent = document.getElementById("c-content"); 

            const _delete = document.querySelectorAll(".error-thread"); 
            this.isCheck = true;
            for (const item of _delete) {
                item.parentNode.removeChild(item);
            }

            inputTitle.style.borderColor = "#d0d0d0"; 
            inputContent.style.borderColor = "#d0d0d0";
            var isCheck = true; 

            //validate rỗng 
            if(this.thread_title === "" || this.thread_title === null || this.thread_title === undefined){
                this.thongbao(inputTitle, "Tiêu đề bài đăng không được để trống."); 
                isCheck = false; 
            }

            if(this.topic_content === "" || this.topic_content === null || this.topic_content === undefined){
                this.thongbao(inputContent, "Nội dung bài đăng không được để trống."); 
                isCheck = false; 
            }

            if(isCheck){
                //Nếu như không upload ảnh thì gọi api thêm mới như bình thường. 
                if(this.file === undefined || this.file === null || this.file === ""){
                    this.thread_code = "TH" + this.index; 
                    this.thread_new.threadsCode = this.thread_code; 
                    this.thread_new.threadsTitle = this.thread_title; 
                    this.thread_new.threadsContent = this.topic_content; 
                    this.thread_new.categoryId = this.thread_category; 
                    this.thread_new.userId = this.user.userId; 
                    var date = new Date(); 
                    var x = date.getDate(); 
                    var z = date.getMonth() + 1; 
                    var y = date.getFullYear(); 
                    var d = ""; 
                    var m = ""; 
                    if(x < 10){
                        d = "0" + x; 
                    }
                    if(z < 10){
                        m = "0" + z; 
                    }
                    this.thread_new.created_at = y + "-" + m + "-" + d;
                    this.thread_new.threadsImage = ""; 

                    //document.getElementById("buggtopic").innerHTML = this.thread_new.threadsCode + "//" + this.thread_new.threadsTitle + "//" + this.thread_new.threadsContent + "//" + this.thread_new.categoryId + "//" + this.thread_new.userId + "//" + this.thread_new.created_at; 

                    BaseRequest.post("thread", this.thread_new)
                    .then(response => {
                        console.log(response.data); 
                        //sau khi thêm mới thành công thì điều hướng tới trang DetailThreads có mã của cái thread vừa thêm.
                        this.$router.push("/Forum"); 
                    })
                    .catch(error => {
                        console.log(error.message); 
                    })

                }
            }
        }, 
        /**
         * Hàm thêm mới thông báo khi có lỗi vào sau muỗi input lỗi. 
         * created by: NTXuan - 22/01/2024. 
         */
        thongbao: function(item, mes){
            item.style.borderColor = "red"; 
            let p = document.createElement("p"); 
            let node = document.createTextNode(mes); 
            p.appendChild(node); 
            p.classList.add("error-thread");
            p.style.color = "red"; 
            p.style.fontStyle = "italic"; 
            p.style.fontSize = "0.9rem"; 
            item.after(p);
            //item.setAttribute("title", mes); 
        },
        newThread: function(){
            //Lấy ra số lượng thread hiện có nhằm mục đích để tạo mã thread tự động tăng khi thêm mới 1 thread. 
            BaseRequest.get("thread")
            .then(response => {
                this.index = response.data.length + 1; 
            })
            .catch(error => {
                console.log(error.message); 
            })

            //lấy ra danh sách các thread thuộc vào chủ đề hiện tại. 
            BaseRequest.get("threaduser/categoryId?categoryId=" + this.$route.params.id)
            .then(response => {
                this.threads = response.data;
                console.log(this.threads); 
            })
            .catch(error => {
                console.log(error.message); 
            }); 

            //lấy ra danh sách các thread có comment rồi trong chủ đề đang xét. 
            BaseRequest.get("threadscomment/categoryId?categoryId=" + this.$route.params.id)
            .then(response => {
                this.threads_comment = response.data;
                 
                for (const item of this.threads) {
                    item.countComment = 0; 
                    for (const item1 of this.threads_comment) {
                        if(item.threadsId === item1.threadsId){
                            item.countComment = item1.countComment;
                            break;  
                        }
                    }
                } 
            })
            .catch(error => {
                console.log(error.message); 
            })
        }, 
        oldThread: function(){
            for(var i = this.threads.length - 1; i>=0; i--){
                this.tg.push(this.threads[i]); 
            }
            this.threads = this.tg; 
        }
    }
}
</script>
<style scoped>
.forum-page-option {
    background-color: #36364b; 
}
.category {
    width: 100%; 
    background-color: #36364b; 
    padding-bottom: 80px; 
}
.category-page {
    width: 70%; 
    margin: auto; 
}
.category-topic-info > div > img {
    width: 30px; 
    height: 30px;
}
.category-topic-info {
    display: flex; 
    gap: 84px; 
}
.category-topic {
    width: 100%; 
    display: flex;
    justify-content: space-between;
    color: #878893;  
    font-size: 1.25rem;
    padding-bottom: 16px; 
    padding-top: 16px; 
    border-bottom: 3px solid #878893;
}
.category-topic > p {
    color: #878893; 
}
.category-topic > div:first-child {
    color: #fff; 
}
.category_chude-topic{
    margin-top: 8px; 
    width: 150px; 
    background-color: #8FE0FF;
    display: flex; 
    justify-content: center;
    align-items: center;
    color: black;  
}
.category-link-cd {
    color: #fff; 
    text-decoration: none;
}
.category-forum-page {
    color: #fff; 
    display: flex; 
    justify-content: space-between;
    padding-top: 24px; 
}
</style>

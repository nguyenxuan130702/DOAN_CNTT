<template>
    <div class="forum-page-option">
        <div class="forum-page-option-item" style="width: 300px" v-on:click="openChuDe()">
            <div id="">{{ chude }}</div>
            <div class="forum-page-option-item-cd" v-show="open_chude">
                <div v-show="hover_chude">
                    <router-link to="/Forum" class="forum-link">Chủ đề</router-link>
                </div>
                <div v-on:click="showChude(item.categoryName)" v-for="item in category" :key="item.categoryId">
                    <router-link :to="/Category/ + item.categoryId" class="forum-link">{{ item.categoryName }}</router-link>
                </div>
            </div>
        </div>
        <div class="forum-page-option-item push-chude" v-on:click="openCreateTopic()" v-show="threadbutton">
            <div class="icon_post"></div>
            <div>Tạo bài đăng</div>
        </div>
    </div>
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
                    <input type="text" name="t-title" id="t-title" placeholder="Hãy tạo một chủ đề ngắn để thảo luận" v-model="thread_title">
                </div>
                <div style="width: 34%">
                    <select name="t-category" id="t-category" v-model="thread_category">
                        <option :value="item.categoryId" v-for="item in category" :key="item.categoryId">{{ item.categoryName }}</option>
                    </select>
                </div>
                <div style="width: 100%">
                    <textarea name="t-content" id="t-content" cols="30" rows="10" placeholder="Nhập nội dung ở đây" v-model="topic_content"></textarea>
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
</template>
<script>
import BaseRequest from '@/core/BaseRequest'
export default{
    data(){
        return {
            open_chude: false,
            hover_chude: false,
            chude: "Chủ đề", 
            showPostTopic: false, 
            topic_content: "",  
            category: [], 
            threadbutton: true,  
            thread_category: "", 
            thread_title: "", 
            file: "", 
            urlThread: "",
            thread_new: {},  
            index: 0, 
            thread_code: "", 
            user: JSON.parse(localStorage.getItem("userLogin")), 
        }
    }, 
    created(){
        BaseRequest.get("category")
        .then(response => {
            this.category = response.data; 
            console.log(this.category); 
            this.thread_category = this.category[0].categoryId; 
        })
        .catch(error => {
            console.log(error.message); 
        }); 

        //Lấy ra số lượng thread hiện có nhằm mục đích để tạo mã thread tự động tăng khi thêm mới 1 thread. 
        BaseRequest.get("thread")
        .then(response => {
            this.index = response.data.length + 1; 
        })
        .catch(error => {
            console.log(error.message); 
        })

        //Kiểm tra xem người dùng đã đăng nhập hay chưa, nếu chưa thì không cho hiển thị button thêm mới bài đăng. 
        
        if(this.user === undefined || this.user === null || this.user === ""){
            this.threadbutton = false; 
        }
    }, 
    methods: {
        openChuDe: function(){
            this.open_chude = !this.open_chude; 
        }, 
        showChude: function(item){
            this.hover_chude = true; 
            localStorage.setItem("categoryName", JSON.stringify(item));
        }, 
        openCreateTopic: function(){
            this.showPostTopic = true; 
        }, 
        closePostTopic: function(){
            this.showPostTopic = false; 
        }, 
        checkFileUploadNew(event){
            this.file = event.target.files[0]; 
            if(this.file){
                //đọc URL của file và gán vào imageUrl để hiển thị 
                const reander = new FileReader(); 
                reander.readAsDataURL(this.file); 
            }
        },
        postThread: function(){
            //validate rỗng
            const inputTitle = document.getElementById("t-title"); 
            const inputContent = document.getElementById("t-content"); 

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
                        this.showPostTopic = false;  
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
        }
    }
}
</script>
<style>
.forum-page-option {
    width: 70%; 
    padding-top: 120px; 
    padding-bottom: 24px; 
    display: flex;
    gap: 16px;  
    margin: auto; 
    justify-content: space-between;
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
    background-color: #8d8e96;
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
    background-color: #878893; 
    color: black; 
}
.push-chude {
    margin-left: 300px; 
    width: 250px; 
}
.forum-link {
    color: #fff; 
    text-decoration: none;
}

/**--------------------------------------------- */
.forum-page-post {
    width: 98%; 
    padding-top: 24px; 
    padding-bottom: 24px; 
    padding-left: 24px; 
    padding-right: 24px; 
    position: fixed; 
    bottom: 0;  
    left: 16px; 
    background-color: #36364b;
    z-index: 3;
    border-top: 5px solid #fff; 
}
.icon_post {
    background: url('@/assets/nav/plus-svgrepo-com (1).svg') no-repeat -13px -13px;
	width: 38px;
	height: 38px;
    transform: scale(0.7);
}
.forum-page-post-post > div:first-child {
    width: 42px; 
    height: 40px;
    border: 3px solid #fff; 
    display: flex; 
    justify-content: center;
    align-items: center;
}
.forum-page-post-post {
    display: flex; 
    font-size: 1.25rem; 
    align-items: center;
    gap: 8px; 
    color: #fff; 
}
.forum-page-post-option {
    display: flex; 
    justify-content: space-between;
    align-items: center;
    margin-bottom: 16px; 
}
.post-content-insert {
    width: 50%; 
    display: flex; 
    flex-wrap: wrap;
    color: #fff; 
    justify-content: space-between;
}
.post-content-insert > div > input[type="text"] {
    width: 100%; 
    height: 46px;
    outline: none;
    border: 1px solid gray; 
    background-color: #36364b; 
    padding-left: 8px; 
    color: #fff; 
}
.post-content-insert > div > input[type="text"]:focus {
    border: 1px solid #d0d0d0; 
}
.post-content-insert > div > select {
    width: 100%; 
    height: 46px;
    background-color: #36364b; 
    color: gray;
    border: 1px solid gray; 
    padding-left: 8px;  
    outline: none; 
}
.post-content-insert > div > select:focus {
    border: 1px solid #d0d0d0; 
}
.post-content-insert > div > textarea {
    width: 100%; 
    margin-top: 16px; 
    background-color: #36364b; 
    border: 1px solid gray; 
    padding-left: 8px; 
    padding-top: 8px; 
    outline: none;
    height: 130px;
    color: #fff; 
}
.post-content-insert > div > textarea:focus {
    border: 1px solid #d0d0d0; 
}
.post-content-dang {
    width: 100%; 
    font-size: 1.25rem; 
}
.post-content-dang-icon {
    display: flex; 
    border: 3px solid #fff; 
    width: 200px; 
    gap: 8px; 
    font-size: 1.25rem; 
    color: #fff; 
    align-items: center;
}
.post-content-dang-icon:hover {
    background-color: #878893; 
    /* color: #36364b;  */
    cursor: pointer;
}
.post-content-dang {
    display: flex; 
    font-size: 1.25rem; 
    color: #d0d0d0; 
    align-items: center;
    gap: 16px; 
    margin-top: 16px; 
}
.forum-page-post-content {
    display: flex; 
}
.post-content-visible {
    color: #fff; 
    padding-left: 16px; 
}
</style>

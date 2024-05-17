<template>
    <div class="u-thread">
        <div class="u-thread-form">
            <div class="u-thread-title">
                <h1>{{ thread.threadsTitle }}</h1>
                <div class="u-thread-forum">{{ thread.categoryName }}</div>
            </div>
            <div class="u-thread-question">
                <div class="u-thread-question-style">
                    <div>
                        <div class="u-thread-question-face">
                            <img :src="thread.userImage" alt="">
                        </div>
                        <div class="u-thread-question-content">
                            <h3>{{ thread.threadsTitle }}</h3>
                            <p id="threadcontent">{{ element }}</p>
                            <img :src="thread.threadsImage" alt="">
                        </div>
                    </div>
                    <div>
                        <div>{{ renderDate(thread.created_at)}}</div>
                    </div>
                </div>
                <div class="u-thread-question-reply">
                    <div class="u-thread-question-reply-sty" v-show="replybutton" v-on:click="openPostComment(thread.threadsTitle)">
                        <div class="icon_reply"></div>
                        <div>Reply</div>
                    </div>
                </div>
            </div>
            <div class="u-thread-question" v-for="item in commentuser" :key="item.commentId">
                <div class="u-thread-question-style">
                    <div>
                        <div class="u-thread-question-face">
                            <img :src="item.userImage" alt="">
                        </div>
                        <div class="u-thread-question-content">
                            <h3>{{ item.userAccout }}</h3>
                            <p>{{ item.commentContent }}</p>
                            <img :src="item.commentImage" alt="">
                        </div>
                    </div>
                    <div>
                        <div>{{ renderDate(item.created_at) }}</div>
                    </div>
                </div>
                <div class="u-thread-question-reply" v-show="replybutton" v-on:click="openPostComment(item.userAccout)">
                    <div class="u-thread-question-reply-sty">
                        <div class="icon_reply"></div>
                        <div>Reply</div>
                    </div>
                </div>
            </div>

            <!--Khi comment mới được post thì nó sẽ được hiển thị-->
            <!-- <div class="u-thread-question" v-show="postcomment">
                <div class="u-thread-question-style">
                    <div>
                        <div class="u-thread-question-face">
                            <img :src="user.userImage" alt="">
                        </div>
                        <div class="u-thread-question-content">
                            <h3>{{ user.userAccout }}</h3>
                            <p>{{ topic_content }}</p>
                            <img :src="urlImage" alt="">
                        </div>
                    </div>
                    <div>
                        <div>{{ date }}</div>
                    </div>
                </div>
                <div class="u-thread-question-reply" v-show="replybutton" v-on:click="openPostComment(item.userAccout)">
                    <div class="u-thread-question-reply-sty">
                        <div class="icon_reply"></div>
                        <div>Reply</div>
                    </div>
                </div>
            </div>
            <div class="thread-back" v-on:click="backCategory()">
                <div>Quay lại</div>
            </div> -->
        </div>
    </div>

    <div class="forum-page-post" v-show="showPostComment">
        <div class="forum-page-post-option">
            <div class="forum-page-post-post">
                <div class="thread-reply">
                    <div class="icon_reply reply-left"></div>
                </div>
                <div>{{ reply_title }}</div>
            </div>
            <div>
                <div class="icon_close" v-on:click="closePostComment()"></div>
            </div>
        </div>
        <div class="forum-page-post-content">
            <div class="post-content-insert">
                <div style="width: 100%">
                    <textarea name="reply-thread" id="reply-thread" cols="30" rows="10" placeholder="Nhập nội dung ở đây" v-model="topic_content"></textarea>
                </div>
                <div style="width: 40%; font-size: 1.25rem;">
                    Chọn ảnh (nếu có) <br>
                    <input type="file" @change="checkFileUploadNew">
                </div>
            </div>
            <div class="post-content-visible" style="width: 50%">{{ topic_content}}</div>
        </div>
        <div class="post-content-dang">
            <div class="post-content-dang-icon thread-post-comment" v-on:click="postComment()">
                <div>
                    <div class="icon_reply"></div>
                </div>
                <div>Trả lời</div>
            </div>
            <div v-on:click="resetContent()" class="btn-reset-comment">Làm mới</div>
        </div>
    </div>
</template>
<script>
import BaseRequest from '@/core/BaseRequest';
export default{
    data(){
        return {
            showPostComment: false, 
            thread: {},  
            commentuser: [],
            element: "", 
            reply_title: "",
            topic_content: "", 
            file: "", 
            imageUrl: "", 
            replybutton: true, 
            post_comment: {}, 
            index: 0, 
            comment_code: "", 
            user: JSON.parse(localStorage.getItem("userLogin")), 
            commentnew: false, 
            date: "", 
            postcomment: false, 
            id_thread: "", 
        }
    }, 
    created(){
        this.loadData(); 
    }, 
    mounted(){
        this.$nextTick(() => {
            this.renderMath();
            if(window.MathJax){
                window.MathJax.typeset(); 
            }
        })
    },
    watch: {
        //Khi dữ liệu thay đổi thì gọi lại hàm render MathJax. 
        element: function(){
            this.$nextTick(() => {
                this.renderMath();
                if(window.MathJax){
                    window.MathJax.typeset(); 
                }
            }); 
        }, 
    }, 
    methods: {
        loadData(){
            //lay ra comment cua thread đang xét. 
            BaseRequest.get("commentuser/threadId?threadId=" + this.$route.params.id)
            .then(response => {
                this.commentuser = response.data;  
            })
            .catch(error => {
                console.log(error.message); 
            }); 

            //lấy ra thông tin thread đang xét. 
            BaseRequest.get("threaduser/threadsId?threadsId=" + this.$route.params.id)
            .then(response => {
                this.id_thread = this.$route.params.id
                this.thread = response.data; 
                this.element = this.thread.threadsContent; 
                console.log(this.thread); 
            })
            .catch(error => {
                console.log(error.message);
            })

            //Kiểm tra xem người dùng đăng nhập chưa. Nếu chưa thì không cho hiển thị nút button reply và nút button tạo mới bài đăng. 
            if(this.user === undefined || this.user === null || this.user === ""){
                this.replybutton = false; 
                this.showPostComment = false; 
            }

            //Đếm số comment đã có trong api mục đích là để tự tăng mã comment 
            BaseRequest.get("comment")
            .then(response => {
                this.index = response.data.length + 1; 
            })
            .catch(error => {
                console.log(error.message); 
            })
        }, 
        closePostComment: function(){
            this.showPostComment = false; 
        }, 
        openPostComment: function(item){
            this.showPostComment = true; 
            this.reply_title = item; 
        }, 
        backCategory: function(){
            this.$router.push("/Category"); 
        }, 
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
         * Render mathjax để hiển thị công thức toán học 
         */
        renderMath: function() {
            const output1 = document.getElementById("threadcontent"); 
            //this.$refs.mathjax.innerHTML = this.formula;
            if (this.$MathJax) {
                this.$MathJax.typesetPromise([output1])
                .catch((err) => console.log('Error rendering MathJax:', err));
            }
        }, 
        checkFileUploadNew(event){
            this.file = event.target.files[0]; 
            if(this.file){
                //đọc URL của file và gán vào imageUrl để hiển thị 
                const reander = new FileReader(); 
                reander.onload = (e) => {
                    this.imageUrl = e.target.result; 
                }; 
                reander.readAsDataURL(this.file); 
            }
        },
        postComment: function(){
            this.post_comment.commentContent = this.topic_content; 
            if(this.file === undefined || this.file === null || this.file === ""){
                
                var date = new Date(); 
                var x = date.getDate(); 
                var z = date.getMonth() + 1; 
                var y = date.getFullYear(); 
                var d = ""; 
                var m = ""; 
                if(x < 10){
                    d = "0" + x; 
                }
                else {
                    d = x; 
                }
                if(z < 10){
                    m = "0" + z; 
                }
                else {
                    m = z; 
                }
                this.date = d + "/" + m + "/" + y;
                this.comment_code = "CM" + this.index; 
                this.post_comment.commentCode = this.comment_code; 
                this.post_comment.commentContent = this.topic_content; 
                this.post_comment.commentImage = "";
                this.post_comment.userId = this.user.userId; 
                this.post_comment.threadsId = this.thread.threadsId; 
                this.post_comment.created_at = y + "-" + m + "-" + d; 

                console.log(this.post_comment); 

                BaseRequest.post("comment", this.post_comment)
                .then(response => {
                    console.log(response.data); 
                    //Sau khi thêm mới thành công thì hiển thị comment thành công.
                    BaseRequest.get("commentuser/threadId?threadId=" + this.thread.threadsId)
                    .then(response => {
                        this.commentuser = response.data;  
                    })
                    .catch(error => {
                        console.log(error.message); 
                    }); 
                    this.showPostComment = false; 
                })
                .catch(error => {
                    console.log(error.message); 
                }); 
            }
            
        },
        resetContent: function(){
            this.topic_content = ""; 
        }
    }
}
</script>
<style scoped>
.icon_reply {
    background: url('@/assets/nav/reply-svgrepo-com.svg') no-repeat -1px 0;
	width: 62px;
	height: 64px;
    transform: scale(0.3);
}
.u-thread {
    padding-top: 80px; 
    background-color: #36364b;
    color: #d0d0d0;  
}
.u-thread-form {
    width: 60%; 
    margin: auto; 
    padding-bottom: 80px; 
}
.u-thread-title > h1 {
    font-size: 2.5rem; 
    font-weight: 700;
}
.u-thread-question {
    padding-bottom: 16px; 
    border-bottom: 1px solid #d0d0d0; 
    padding-top: 16px; 
}
.u-thread-question-face > img{
    width: 50px; 
    height: 50px;
}
.u-thread-question-style > div:first-child {
    display: flex; 
    gap: 16px; 
}
.u-thread-question-style {
    display: flex;
    justify-content: space-between;
}
.u-thread-question-content > img{
    width: 100%;  
}
.u-thread-question-content > p {
    font-size: 1.25rem; 
}
.u-thread-question-content > h3 {
    font-size: 1.5rem; 
    font-weight: 700;
}
.u-thread-question-reply {
    display: flex;
    justify-content: flex-end;
}
.u-thread-question-reply-sty {
    display: flex;
    align-items: center;
    font-size: 1.25rem; 
    /* justify-content: flex-end; */
    margin-right: 100px; 
    width: 150px; 
    border-radius: 4px;
    margin-top: 16px; 
    height: 46px;
}
.u-thread-question-reply-sty:hover {
    background-color: #878893; 
}
.u-thread-forum {
    background-color: yellow; 
    color: black; 
    width: 100px; 
    text-align: center;
    margin-bottom: 16px; 
}
.thread-post-comment {
    height: 46px;
    width: 160px;
    transition: background-color 0.3s; 
}
.thread-post-comment:hover {
    background-color: #878893; 
    cursor: pointer;
}
.thread-reply{
    width: 50px !important; 
    border: 1px solid gray !important; 
    transition: background-color 0.5s ;
}
.thread-reply:hover {
    background-color: #878893;
    cursor: pointer;
}
.thread-back {
    width: 140px; 
    height: 46px;
    border: 3px solid #d0d0d0; 
    display: flex; 
    justify-content: center;
    align-items: center;
    font-size: 1.25rem; 
    transition: background-color 0.3s;
    margin-top: 40px; 
}
.thread-back:hover {
    background-color: #878893; 
    cursor: pointer;
}
.btn-reset-comment:hover {
    cursor: pointer;
    color: orangered; 
}
</style>

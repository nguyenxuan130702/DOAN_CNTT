<template>
    <div class="form-comment" id="form_update_thread">
        <div class="form-comment-title">
            <h1>Sửa thông tin bình luận</h1>
            <div class="icon_close" v-on:click="closeFormUpdateComment()"></div>
        </div>
        <div class="form-comment-content">
            <div class="form-comment-p1">
                <div class="form-comment-fomr1">
                    <div>
                        <b>Bài đăng được bình luận</b> <br>
                        <select name="a-ucooment-thread" id="a-ucomment-thread" v-model="thread_id">
                            <option :value="item.threadsId" v-for="item in thread" :key="item.threadsId">{{ item.threadsTitle }}</option>
                            
                        </select>
                    </div>
                    <div class="form-comment-content-img">
                        <b>Ảnh đính kèm (Nếu có)</b> <br>
                        <input type="file" @change="checkFileUploadNew">
                        <img v-if="loadImg" :src="imgComment" alt="">
                        <!-- <p v-else>Chưa có ảnh nào</p> -->
                    </div>
                </div>
                <div class="form-comment-fomr1">
                    <b>Nội dung bình luận</b> <br>
                    <textarea name="a-ucomment-content" id="a-ucomment-content" cols="30" rows="10" placeholder="Mô tả" v-model="comment_content"></textarea>
                </div>
            </div>
            <div class="form-comment-p1">
                <div class="form-comment-user">
                    <b>Người dùng bình luận</b> <br>
                    <img :src="userImage" alt="">
                    <div>{{ userAccout }}</div>
                </div>
                <div style="width: 48%; margin-top: 16px;">
                    <b>Ngày đăng bình luận</b>
                    <div>{{ renderDate(date) }}</div>
                </div>
            </div>
        </div>
        <div class="course_btn" v-on:click="validatePutComment()">
            <button>Cập nhật</button>
        </div>
    </div>

    <!--Thông báo thêm mới thành công-->
    <div class="dialog_dangky" id="dialog_comment" >
        <div class="dialog_dangky_title">
            <div class="icon_thanhcong"></div>
            <p id="dialog_dangky_comment">Cập nhật thành công.</p>
        </div>
        <div class="icon_close" v-on:click="closeDialogThread()"></div>
    </div>
</template>
<script>
import BaseRequest from '@/core/BaseRequest';
export default {
    props: ['propGetComment'],
    methods: {
        checkFileUploadNew(event){
            this.file = event.target.files[0]; 
            if(this.file){
                //đọc URL của file và gán vào imageUrl để hiển thị 
                const reander = new FileReader(); 
                reander.onload = (e) => {
                    this.imgComment = e.target.result; 
                }; 
                reander.readAsDataURL(this.file); 
            }
        }, 
        /**
         * Hàm đóng form update thông tin comment. 
         */
        closeFormUpdateComment: function(){
            document.getElementById("form_update_thread").style.display = "none"; 
            document.getElementById("comment_back").style.display = "none";
        }, 
        /**
         * Hàm thực hiện click vào nút cập nhật. 
         */
        validatePutComment: function(){
            const inputContent = document.getElementById("a-ucomment-content"); 

            const _delete = document.querySelectorAll(".error-comment"); 
            this.isCheck = true;
            for (const item of _delete) {
                item.parentNode.removeChild(item);
            }

            inputContent.style.borderColor = "#d0d0d0"; 
            var isCheck = true; 

            if(this.commentContent === "" || this.commentContent === undefined || this.commmentContent === null){
                this.thongbao(inputContent,"Nội dung comment không được để trống."); 
                isCheck = false; 
            }

            if(isCheck){
                //Tiến hành chỉnh sửa
                if(this.file === "" || this.file === null || this.file === undefined){
                    this.comment.commentContent = this.commentContent; 
                    BaseRequest.put("comment", this.comment)
                    .then(response => {
                        console.log(response.data); 
                        //hiển thị thông báo chỉnh sửa thành công. 
                        document.getElementById("dialog_comment").style.opacity = 1; 
                        document.getElementById("form_update_thread").style.display = "none"; 
                        document.getElementById("comment_back").style.display = "none";
                    })
                    .catch(error => {
                        console.log(error.message); 
                    })
                }
            }
        }, 
        thongbao: function(item, mes){
            item.style.borderColor = "red"; 
            let p = document.createElement("p"); 
            let node = document.createTextNode(mes); 
            p.appendChild(node); 
            p.classList.add("error-comment");
            p.style.color = "red"; 
            p.style.fontStyle = "italic"; 
            p.style.fontSize = "0.9rem"; 
            item.after(p);
            //item.setAttribute("title", mes); 
        },
        renderDate: function(item){
            var x = new Date(item); 
            var d = x.getDate(); 
            var m = x.getMonth() + 1; 
            var y = x.getFullYear(); 
            return d + "/" + m + "/" + y; 
        }
    }, 
    data(){
        return {
            thread: [],
            thread_id: "", 
            imgComment: "", 
            comment_content: "", 
            userImage: "", 
            userAccout: "", 
            loadImg: false, 
            comment: {}, 
            file: "", 
            date: "", 
        }
    }, 
    mounted(){
        BaseRequest.get("thread")
        .then(response => {
            this.thread = response.data; 
        })
        .catch(error => {
            console.log(error.message); 
        })
    }, 
    watch: {
        propGetComment: function(){
            this.thread_id = this.propGetComment.threadsId; 
            this.imgComment = this.propGetComment.commentImage; 
            if(this.imgComment === null || this.imgComment === ""){
                this.loadImg = false; 
            }
            else {
                this.loadImg = true; 
            }
            this.comment_content = this.propGetComment.commentContent; 
            this.userImage = this.propGetComment.userImage; 
            this.userAccout = this.propGetComment.userAccout; 
            this.comment = this.propGetComment; 
            this.date = this.propGetComment.created_at; 
        }
    }
}
</script>
<style >
.form-comment {
    width: 60%;
    background-color: #fff; 
    position: absolute; 
    z-index: 1;  
    left: 20%; 
    display: none; 
}
.form-comment-p1{
    width: 100%; 
    display: flex;
    justify-content: space-between; 
}
.a-comment-content {
    width: 48% !important; 
}
.a-comment-content > select {
    width: 100% !important; 
}
.form-comment-title {
    width: 100%; 
    display: flex; 
    justify-content: space-between;
    align-items: center;
    padding-top: 24px; 
    padding-left: 24px; 
    padding-right: 24px; 
}
.form-comment-content {
    width: 100%; 
    font-size: 1.25rem; 
    padding-left: 24px; 
    padding-right: 24px; 
}
.form-comment-fomr1 {
    width: 48%; 
}
.form-comment-fomr1 > div {
    width: 100%; 
}
#a-ucomment-content {
    width: 100%; 
    border: 1px solid #d0d0d0; 
    border-radius: 4px;
    outline: none; 
    padding-left: 8px; 
    margin-top: 8px; 
    height: 320px;
}
#a-ucomment-content:focus {
    border: 1px solid #50B83C; 
}
#a-ucomment-thread {
    width: 100%; 
    border: 1px solid #d0d0d0; 
    height: 36px;
    border-radius: 4px;
    outline: none; 
    padding-left: 8px; 
    margin-top: 8px;
    margin-bottom: 16px; 
}
#a-ucomment-thread:focus {
    border: 1px solid #50B83C; 
}
.form-comment-user {
    width: 30%;
    margin-top: 16px;  
}
.form-comment-user > img {
    height: 100px;
    width: 100px;
    border-radius: 50%;
    margin-top: 8px; 
    margin-left: 15%;  
    margin-bottom: 8px; 
}
.form-comment-user > div {
    margin-left: 15%; 
}
.form-comment-content-img > img {
    margin-top: 8px; 
    width: 300px;
    height: 200px;
}
</style>

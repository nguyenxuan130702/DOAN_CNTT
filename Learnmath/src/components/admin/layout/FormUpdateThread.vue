<template>
    <div class="form_course" id="form_update_thread">
        <div class="form_course_title">
            <h1>Sửa thông tin bài đăng</h1>
            <div class="icon_close" v-on:click="closeFormUpdateThread()"></div>
        </div>
        <div class="form_course-content-x">
            <div for="" class="a-new-content">
                <b>Tiêu đề bài đăng </b><br>
                <input type="text" placeholder="tên khóa học" id="a-uthread-title" name="a-uthread-title" v-model="thread_title">
            </div>
            <div class="a-new-content">
                <b>Chủ đề bài đăng </b><br>
                <select name="a-uthread-category" id="a-uthread-category" v-model="thread_category">
                    <option :value="item.categoryId" v-for="item in category" :key="item.categoryId">{{ item.categoryName }}</option>
                </select>
            </div>
            <div class="a-new-content">
                <b>Nội dung bài đăng </b><br>
                <textarea name="a-uthread-content" id="a-uthread-content" cols="30" rows="10" placeholder="Mô tả" v-model="thread_content"></textarea>
            </div>
            <div class="a-new-content">
                <b>Ảnh đính kèm (Nếu có) </b><br>
                <input type="file" @change="checkFileUploadNew">
                <img v-if="loadImage" :src="imageUrl" alt="">
                <!-- <p v-else>Chưa có ảnh nào</p> -->
            </div>
        </div>
        <div class="course_btn" v-on:click="validatePutThread()">
            <button>Cập nhật</button>
        </div>
    </div>
</template>
<script>
import BaseRequest from '@/core/BaseRequest';
export default {
    props: ['propGetThread'], 
    data(){
        return {
            thread_title: "", 
            thread_category: "", 
            thread_content: "", 
            imageUrl: "", 
            category: [], 
            file: "", 
            loadImage: false, 
            thread: {}, 
        }
    }, 
    mounted(){
        BaseRequest.get("category")
        .then(response => {
            this.category = response.data; 
        })
        .catch(error => {
            console.log(error.message); 
        })
    }, 
    watch: {
        propGetThread: function(){
            this.thread = this.propGetThread; 
            this.thread_title = this.propGetThread.threadsTitle; 
            this.thread_content = this.propGetThread.threadsContent; 
            this.thread_category = this.propGetThread.categoryId; 
            this.imageUrl = this.propGetThread.threadsImage; 
            if(this.imageUrl === "" || this.imageUrl === null){
                this.loadImage = false; 
            }
            else {
                this.loadImage = true; 
            }
        }
    }, 
    methods: {
        closeFormUpdateThread: function(){
            document.getElementById("form_update_thread").style.display = "none"; 
            document.getElementById("thread_back").style.display = "none"; 
        }, 
        checkFileUploadNew(event){
            this.file = event.target.files[0]; 
            if(this.file){
                //đọc URL của file và gán vào imageUrl để hiển thị 
                const reander = new FileReader(); 
                reander.onload = (e) => {
                    this.imageUrl = e.target.result; 
                    this.loadImage = true; 
                }; 
                reander.readAsDataURL(this.file); 
            }
        },
        validatePutThread: function(){
            const inputTitle = document.getElementById("a-uthread-title"); 
            const inputContent = document.getElementById("a-uthread-category"); 

            const _delete = document.querySelectorAll(".error-thread"); 
            this.isCheck = true;
            for (const item of _delete) {
                item.parentNode.removeChild(item);
            }

            inputTitle.style.borderColor = "#d0d0d0"; 
            inputContent.style.borderColor = "#d0d0d0"; 
            var isCheck = true; 

            if(this.thread_title === "" || this.thread_title === undefined || this.thread_title === null){
                this.thongbao(inputTitle, "Tiêu đề bài đăng không được để trống."); 
                isCheck = false; 
            }

            if(this.thread_content === "" || this.thread_content === undefined || this.thread_content === null){
                this.thongbao(inputContent, "Nội dung bài đăng không được để trống."); 
                isCheck = false; 
            }

            if(isCheck){
                //chỉnh sửa
                if(this.file === "" || this.file === undefined || this.file === null){
                    this.thread.threadsTitle = this.thread_title; 
                    this.thread.threadsContent = this.thread_content; 
                    this.thread.categoryId = this.thread_category; 

                    BaseRequest.put("thread", this.thread)
                    .then(response => {
                        console.log(response.data);
                        //hiển thị thông báo cập nhật thành công. 
                        document.getElementById("dialog_dangky_thread").innerHTML = "Cập nhật thành công"; 
                        document.getElementById("form_update_thread").style.display = "none"; 
                        document.getElementById("thread_back").style.display = "none"; 
                        document.getElementById("dialog_thread").style.opacity = 1; 
                    })
                    .catch(error => {
                        console.log(error.message); 
                    })
                }
                else {
                    //Nếu như có load file lên thì lưu theo cái form imag. 
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
    }
}
</script>
<style scoped>
.a-new-content > img {
    width: 200px; 
    height: 200px;
    margin-top: 8px;  
}
.a-new-content {
    margin-bottom: 16px; 
}
#a-uthread-category {
    height: 36px;
    width: 100%; 
    border: 1px solid #d0d0d0; 
    outline: none; 
    margin-top: 8px; 
}
#a-uthread-category:focus {
    border: 1px solid #50B83C; 
}
</style>

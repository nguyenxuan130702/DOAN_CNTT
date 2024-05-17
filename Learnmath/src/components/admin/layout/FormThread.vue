<template>
    <div class="form_course" id="form_thread">
        <div class="form_course_title">
            <h1>Thêm mới bài đăng</h1>
            <div class="icon_close" v-on:click="closeFormThread()"></div>
        </div>
        <div class="form_course-content-x">
            <div for="" class="a-new-content">
                <b>Tiêu đề bài đăng</b> <br>
                <input type="text" placeholder="Tên khóa học" id="a-thread-title" name="a-thread-title" v-model="thread_title">
            </div>
            <div class="a-new-content">
                <b>Chủ đề bài đăng</b> <br>
                <select name="a-thread-category" id="a-thread-category" v-model="thread_category">
                    <option :value="item.categoryId" v-for="item in category" :key="item.categoryId">{{ item.categoryName }}</option>
                </select>
            </div>
            <div class="a-new-content">
                <b>Nội dung bài đăng</b> <br>
                <textarea name="a-thread-content" id="a-thread-content" cols="30" rows="10" placeholder="Mô tả" v-model="thread_content"></textarea>
            </div>
            <div class="a-new-content">
                Ảnh đính kèm (Nếu có) <br>
                <input type="file" @change="checkFileUploadNew">
                <img :src="imageUrl" alt="">
            </div>
        </div>
        <div class="course_btn" v-on:click="validatePushThread()">
            <button>Thêm mới</button>
        </div>
    </div>

    <!--Thông báo thêm mới thành công-->
    <div class="dialog_dangky" id="dialog_thread" >
        <div class="dialog_dangky_title">
            <div class="icon_thanhcong"></div>
            <p id="dialog_dangky_thread">Thêm mới thành công.</p>
        </div>
        <div class="icon_close" v-on:click="closeDialogThread()"></div>
    </div>
</template>
<script>
import BaseRequest from '@/core/BaseRequest';
export default {
    props: ['propThread'], 
    data(){
        return {
            imageUrl: "", 
            thread_title: "", 
            thread_content: "",
            thread_category: "", 
            index: 0, 
            thread: {}, 
            category: [],
            user: JSON.parse(localStorage.getItem("userLogin")), 
            date: '',  
        }
    }, 
    watch: {
        propThread: function(){
            this.index = this.propThread.length + 1; 
        }
    }, 
    mounted(){
        BaseRequest.get("category")
        .then(response => {
            this.category = response.data; 
            this.thread_category = this.category[0].categoryId; 
        })
        .catch(error => {
            console.log(error.message); 
        })
    }, 
    methods: {
        closeFormThread: function(){
            document.getElementById("form_thread").style.display = "none"; 
            document.getElementById("thread_back").style.display = "none"; 
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
        validatePushThread: function(){
            const inputTitle = document.getElementById("a-thread-title"); 
            const inputContent = document.getElementById("a-thread-content"); 
            
            const _delete = document.querySelectorAll(".error-thread"); 
            this.isCheck = true;
            for (const item of _delete) {
                item.parentNode.removeChild(item);
            }

            inputTitle.style.borderColor = "#d0d0d0"; 
            inputContent.style.borderColor = "#d0d0d0"; 
            var isCheck = true; 

            //Validate rỗng 
            if(this.thread_title === "" || this.thread_title === undefined || this.thread_title === null){
                this.thongbao(inputTitle, "Tiêu đề bài đăng không được bỏ trống."); 
                isCheck = false; 
            }
            
            if(this.thread_content === "" || this.thread_content === undefined || this.thread_content === null){
                this.thongbao(inputContent, "Nội dung bài đăng không được bỏ trống."); 
                isCheck = false; 
            }

            if(isCheck){
                const datenow = new Date(); 
                var d = datenow.getDate();  
                var m = datenow.getMonth() + 1; 
                var y = datenow.getFullYear(); 
                if(d < 10){
                    if(m < 10){
                        this.date = y + "-0" + m + "-0" + d;
                    }
                    else {
                        this.date = y + "-" + m + "-0" + d;
                    }
                }
                else {
                    if(m < 10){
                        this.date = y + "-0" + m + "-" + d;
                    }
                    else {
                        this.date = y + "-" + m + "-" + d;
                    }
                }
                //thực hiện thêm mới vào trong api. 
                if(this.file === "" || this.file === null || this.file === undefined){
                    //dùng api có ảnh là string 
                    this.index = this.propThread.length + 1;
                    var code = "TH" + this.index; 
                    this.thread.threadsCode = code; 
                    this.thread.threadsTitle = this.thread_title; 
                    this.thread.threadsContent = this.thread_content; 
                    this.thread.threadsImage = ""; 
                    this.thread.categoryId = this.thread_category; 
                    this.thread.userId = this.user.userId;
                    this.thread.created_at = this.date; 
                    
                    //Tiền hành thêm mới 
                    BaseRequest.post("thread", this.thread)
                    .then(response => {
                        console.log(response.data); 
                        //hiển thị thông báo thành công.
                        document.getElementById("form_thread").style.display = "none"; 
                        document.getElementById("thread_back").style.display = "none"; 
                        document.getElementById("dialog_thread").style.opacity = 1;
                    })
                    .catch(error => {
                        console.log(error.message); 
                    })
                }
            }
        }, 
        closeDialogThread: function(){
            document.getElementById("dialog_thread").style.display = "none"; 
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
.a-new-content > select {
    width: 100%; 
    border: 1px solid #d0d0d0; 
    height: 36px;
    padding-left: 8px; 
    margin-top: 8px; 
    margin-bottom: 16px; 
}
#a-thread-category {
    outline: none;
} 
#a-thread-category:focus {
    border: 1px solid #50B83C; 
}
</style>

<template>
    <div class="form_course" id="form_news">
        <div class="form_course_title">
            <h1>Thêm mới bản tin</h1>
            <div id="bugg"></div>
            <div class="icon_close" v-on:click="closeFormNew()"></div>
        </div>
        <div class="form_course-content-x">
            <!-- <div class = "bug_course">
               <div>Mã bản tin</div>
                <input type="text" placeholder="mã khóa học" id = "mabt" name="mabt" v-model="mabantin">
            </div> -->
            <div class="a-new-content">
                <div for="" class="title-new">
                    <b>Tiêu đề bản tin</b> <br>
                    <input type="text" placeholder="tên khóa học" id="tieude" name="tieude" v-model="tieude">
                </div>
                <div class="file_new">
                    <b>Ảnh bản tin</b> <br>
                    <img :src="imageUrl" alt="">
                    <input type="file" name="image_face" id="image_face" @change="checkFileUploadNew" accept="image/*">
                </div>
            </div>
            <div class="a-new-content">
                <b>Nội dung</b> <br>
                <textarea name="noidung" id="noidung" cols="30" rows="10" placeholder="Mô tả" v-model="noidung"></textarea>
            </div>
        </div>
        <div class="course_btn" v-on:click="validatePushNew()">
            <button>Thêm mới</button>
        </div>
    </div>

    <!--Thông báo thêm mới thành công-->
    <div class="dialog_dangky" id="dialog_new" >
        <div class="dialog_dangky_title">
            <div class="icon_thanhcong"></div>
            <p id="dialog_dangky_content">Thêm mới thành công.</p>
        </div>
        <div class="icon_close" v-on:click="closeDialogPushNew()"></div>
    </div>
</template>
<script>
import BaseRequest from '@/core/BaseRequest';
//import BaseRequest from '@/core/BaseRequest';
export default{
    props: ['propNews'], 
    data(){
        return{
            imageUrl: 'https://ss-images.saostar.vn/wp700/pc/1613810558698/Facebook-Avatar_3.png', 
            tieude: "", 
            noidung: "", 
            new: {},
            countNew: 0,
            tg: [], 
            currentDate: "",  
            user: {},  
            file: "", 
        }
    }, 
    watch: {
        propNews: function(){
            this.countNew = this.propNews.length + 1; 
        }
    },
    mounted() {
        this.getCurrentDate(); 
        this.user = JSON.parse(localStorage.getItem("userLogin"));  
    }, 
    methods: {
        closeDialogPushNew: function(){
            document.getElementById("dialog_new").style.opacity = "0"; 
        },
        validatePushNew: function(){
            const inputTitle = document.getElementById("tieude"); 
            const inputNoidung = document.getElementById("noidung"); 

            const _delete = document.querySelectorAll(".error-lesson"); 
            this.isCheck = true;
            for (const item of _delete) {
                item.parentNode.removeChild(item);
            }

            inputTitle.style.borderColor = "#d0d0d0"; 
            inputNoidung.style.borderColor = "#d0d0d0";
            
            var isCheck = true; 
            //validate tiêu đề bản tin 
            if(this.tieude === "" || this.tieude === undefined || this.tieude === null ){
                this.thongbao(inputTitle, "Mã tiêu đề không được phép để trống."); 
                isCheck = false; 
            }

            //validate nội dung bản tin 
            if(this.noidung === "" || this.noidung === undefined || this.noidung === null){
                this.thongbao(inputNoidung, "Nội dung bản tin không được phép để trống."); 
                isCheck = false; 
            }

            if(isCheck){ 
                const datenow = new Date(); 
                var d = datenow.getDate();  
                var m = datenow.getMonth() + 1; 
                var y = datenow.getFullYear(); 
                if(d < 10){
                    if(m < 10){
                        this.currentDate = y + "-0" + m + "-0" + d;
                    }
                    else {
                        this.currentDate = y + "-" + m + "-0" + d;
                    }
                }
                else {
                    if(m < 10){
                        this.currentDate = y + "-0" + m + "-" + d;
                    }
                    else {
                        this.currentDate = y + "-" + m + "-" + d;
                    }
                }
                var code = "N" + this.countNew; 
                if(this.file === null || this.file === "" || this.file === undefined){
                    this.new.newCode = code;  
                    this.new.newTitle = this.tieude; 
                    this.new.newContent = this.noidung; 
                    this.new.newDate = this.currentDate; 
                    this.new.newImage = "https://ptdtntbaothang.edu.vn/uploads/news/2020_03/toan-hoc.jpg"; 
                    this.new.createdBy = this.user.userAccout;

                    BaseRequest.post("news", this.new)
                    .then(response => {
                        console.log(response.data);
                        document.getElementById("dialog_dangky_content").innerHTML = "Thêm mới thành công."
                        document.getElementById("news_back").style.display = "none"; 
                        document.getElementById("form_news").style.display = "none";
                        document.getElementById("dialog_new").style.opacity = "1";   
                    })
                    .catch(error => {
                        console.log(error.message);
                    })
                }
                else {
                    const formData = new FormData();

                    formData.append("NewCode", code); 
                    formData.append("NewTitle", this.tieude); 
                    formData.append("NewDate", this.currentDate); 
                    formData.append("NewContent", this.noidung); 
                    formData.append("CreatedBy", this.user.userAccout); 
                    formData.append("Image", this.file);

                    BaseRequest.post("news/PostImage", formData)
                    .then(response => {
                        console.log(response.data); 
                        document.getElementById("dialog_dangky_content").innerHTML = "Thêm mới thành công."
                        document.getElementById("news_back").style.display = "none"; 
                        document.getElementById("form_news").style.display = "none";
                        document.getElementById("dialog_new").style.opacity = "1";
                    })
                    .catch(error => {
                        console.log(error.message); 
                    })
                } 
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
        getCurrentDate() {
            // const today = new Date();
            // const options = { year: 'numeric', month: 'long', day: 'numeric' };
            // this.currentDate = today.toLocaleDateString('vi-VN', options);
            //return moment(this.currentDate).format('YYYY/MM/DD');
        }, 
        closeFormNew: function(){
            document.getElementById("news_back").style.display = "none"; 
            document.getElementById("form_news").style.display = "none";
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
            p.classList.add("error-lesson");
            p.style.color = "red"; 
            p.style.fontStyle = "italic"; 
            p.style.fontSize = "0.9rem"; 
            item.after(p);
            //item.setAttribute("title", mes); 
        },

    }
}
</script>
<style>
#tieude::placeholder{
    font-size: 0.9rem;
    font-style: italic;
}
#noidung::placeholder {
    font-size: 0.9rem; 
    font-style: italic;
}
.a-new-content {
    width: 48% !important; 
}
.date_new > input {
    width: 100%;
    height: 36px;
    margin-top: 16px; 
    border: 1px solid #d0d0d0; 
    padding-left: 8px; 
    outline: none; 
}
.date_new > input:focus {
    border: 1px solid #50B83C; 
}
.file_new {
    margin-top: 24px; 
}
.file_new > img {
    margin-top: 8px; 
    width: 200px; 
    height: 200px;
    border-radius: 50%;
    margin-bottom: 8px; 
}
#noidung {
    width: 100%; 
    height: calc(100% - 56px); 
}
.title-new > input {
    width: 100%; 
    height: 36px; 
    margin-top: 16px; 
    border: 1px solid #d0d0d0;
    padding-left: 8px; 
    outline: none;  
}
.title-new > input:focus {
    border: 1px solid #50B83C;
}
</style>

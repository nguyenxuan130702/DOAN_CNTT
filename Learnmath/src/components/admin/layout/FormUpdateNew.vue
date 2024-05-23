<template>
    <div class="form_course" id="form_news_put">
        <div class="form_course_title">
            <h1>Sửa thông tin bản tin</h1>
            <div id="bugg"></div>
            <div class="icon_close" v-on:click="closeFormPutNew()"></div>
        </div>
        <div class="form_course-content-x">
            <div class="a-new-content">
                <div for="" class="title-new">
                    <b>Tiêu đề bản tin</b> <br>
                    <input type="text" placeholder="tên khóa học" id="new-tieude" name="new-tieude" v-model="tieude">
                </div>
                <div class="file_new">
                    <b>Ảnh bản tin</b> <br>
                    <img :src="imageUrl" alt="">
                    <input type="file" name="new-image_face" id="new-image_face" @change="checkFileUploadNew" accept="image/*">
                </div>
            </div>
            <div class="a-new-content">
                <b>Nội dung</b> <br>
                <textarea name="new-noidung" id="new-noidung" cols="30" rows="10" placeholder="Mô tả" v-model="noidung"></textarea>
            </div>
        </div>
        <div class="course_btn" v-on:click="validatePutNew()">
            <button>Cập nhật</button>
        </div>
    </div>
</template>
<script>
import BaseRequest from '@/core/BaseRequest';
export default{
    props: ['propGetNew'],
    data(){
        return {
            imageUrl: 'https://ss-images.saostar.vn/wp700/pc/1613810558698/Facebook-Avatar_3.png',
            tieude: "",
            noidung: "",
            new: {},  
            file: "", 
        }
    }, 
    watch: {
        propGetNew: function(){
            this.tieude = this.propGetNew.newTitle; 
            this.noidung = this.propGetNew.newContent; 
            this.imageUrl = this.propGetNew.newImage; 
            this.new = this.propGetNew; 
        }
    },
    methods: {
        closeFormPutNew: function(){
            document.getElementById('form_news_put').style.display = "none"; 
            document.getElementById("news_back").style.display = "none"; 
        }, 
        validatePutNew: function(){
            const inputTitle = document.getElementById("new-tieude"); 
            const inputNoidung = document.getElementById("new-noidung"); 

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
                if(this.file === null || this.file === undefined || this.file === ""){
                    this.new.newTitle = this.tieude; 
                    this.new.newContent = this.noidung; 
                    BaseRequest.put("news", this.new)
                    .then(response => {
                        console.log(response.data);
                        document.getElementById("dialog_dangky_content").innerHTML = "Cập nhật  thành công."
                        document.getElementById("news_back").style.display = "none"; 
                        document.getElementById("form_news_put").style.display = "none";
                        document.getElementById("dialog_new").style.opacity = "1"; 
                    })
                    .catch(error => {
                        console.log(error.message); 
                    })
                }
                else {
                    const formData = new FormData(); 
                    formData.append("NewId", this.new.newId); 
                    formData.append("NewCode", this.new.newCode); 
                    formData.append("NewTitle", this.tieude); 
                    formData.append("NewDate", this.new.newDate); 
                    formData.append("NewContent", this.noidung); 
                    formData.append("Image", this.file); 

                    console.log(this.new); 
                    
                    BaseRequest.put("news/UpdateImage", formData)
                    .then(response => {
                        console.log(response.data); 
                        document.getElementById("dialog_dangky_content").innerHTML = "Cập nhật thành công."
                        document.getElementById("news_back").style.display = "none"; 
                        document.getElementById("form_news_put").style.display = "none";
                        document.getElementById("dialog_new").style.opacity = "1"; 
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
            p.classList.add("error-lesson");
            p.style.color = "red"; 
            p.style.fontStyle = "italic"; 
            p.style.fontSize = "0.9rem"; 
            item.after(p);
            //item.setAttribute("title", mes); 
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
    }, 

}
</script>
<style scoped>
.a-new-content {
    width: 48% !important; 
}
</style>

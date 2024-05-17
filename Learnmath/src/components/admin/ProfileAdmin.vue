<template>
<Navbar></Navbar>
<Sidebar></Sidebar>
    <div class="profile_admin">
        <h1>Thông tin tài khoản admin</h1>
        <div class="profile_admin_info">
            <div class="admin_info">
                <h3>Thông tin tài khoản</h3>
                <label for="">
                    Tên tài khoản <br>
                    <input type="text" id="accout-admin" name="accout-admin" v-model="accoutadmin">
                </label>
                <label for="">
                    Họ và tên <br>
                    <input type="text" id="name-admin" name="name-admin" v-model="nameadmin">
                </label>
                <label for="">
                    Email <br>
                    <input type="text" id="email-admin" name="email-admin" v-model="emailadmin">
                </label>
                <label for="" class="btn-info-admin" v-on:click="updateInfor()">
                    <button>Cập nhật thông tin</button>
                </label>
            </div>

            <div class="admin_pass">
                <h3>Đổi mật khẩu</h3>
                <label for="">
                    Mật khẩu mới <br>
                    <input type="password" id="pass-new-admin" name="pass-new-admin" v-model="passnewadmin">
                </label>
                <label for="">
                    Nhập lại mật khẩu mới <br>
                    <input type="password" id="pass-confirm-admin" name="pass-confirm-admin" v-model="passconfirmadmin">
                </label>
                <label for="" style="visibility: hidden;">
                    Mật khẩu cũ <br>
                    <input type="password" id="pass-old-admin" name="pass-old-admin" v-model="passold">
                </label>
                <label for="" class="btn-info-admin" v-on:click="updatePass()">
                    <button>Cập nhật mật khẩu</button>
                </label>
            </div>

            <div class="admin_logo">
                <h3>Ảnh đại diện</h3>
                <img :src="imageUrl" alt="">
                <label for=""> 
                    <input type="file" name="fileanh" id="fileanh" @change="checkFileUploadAdmin" accept="image/*">
                </label>
                <label for="" class="btn-info-admin image-admin" v-on:click="updateImage()">
                    <button>Cập nhật ảnh đại diện</button>
                </label>
            </div>
        </div>
    </div>

    <!--Thông báo thêm mới thành công-->
    <div class="dialog_updateadmin" id="dialog_updateAdmin" >
        <div class="dialog_dangky_title bugg_dialogupdateadmin">
            <div class="icon_thanhcong"></div>
            <p id="dialog_dangky_content">Cập nhật thành công.</p>
        </div>
        <div class="icon_close" v-on:click="closeDialogPutAdmin()"></div>
    </div>
</template>
<script>
import BaseRequest from '@/core/BaseRequest'
import Navbar from './layout/NavbarAdmin.vue'
import Sidebar from './layout/SidebarAdmin.vue'
export default {
    components: {
        Navbar, 
        Sidebar
    }, 
    data(){
        return {
            imageUrl: "https://images.fbox.fpt.vn/wordpress-blog/2023/11/ponyp-va-sosuke.jpg", 
            passold: '', 
            accoutadmin: '', 
            nameadmin: '', 
            emailadmin: '', 
            passnewadmin: '', 
            passconfirmadmin: '',
            admin: JSON.parse(localStorage.getItem("userLogin")), 
            isButtonDisabled1: true, 
            updateAdmin: {}, 
            user: [],
            file: '', 
        }
    }, 
    mounted(){
        this.accoutadmin = this.admin.userAccout; 
        this.nameadmin = this.admin.userName;
        this.emailadmin = this.admin.userEmail; 
        if(this.admin.userImage === null){
            this.imageUrl = "https://images.fbox.fpt.vn/wordpress-blog/2023/11/ponyp-va-sosuke.jpg"; 
        }
        else {
            this.imageUrl = this.admin.userImage;
        } 

        BaseRequest.get("viewaccout/list?userName=" + this.admin.userAccout)
        .then(response => {
            this.user = response.data; 
        })
        .catch(error => {
            console.log(error.message); 
        })
    }, 
    // watch: {
    //     accoutadmin: function(){
    //         this.isButtonDisabled1 = false;
    //     }
    // }, 
    methods: {
        closeDialogPutAdmin: function(){
            document.getElementById("dialog_updateAdmin").style.opacity = 0; 
        },
        updateInfor: function(){
            const _delete = document.querySelectorAll(".error-infor-admin"); 
            this.isCheck = true;
            for (const item of _delete) {
                item.parentNode.removeChild(item);
            }

            const inputUser = document.getElementById("accout-admin"); 
            const inputName = document.getElementById("name-admin"); 
            const inputEmail = document.getElementById("email-admin"); 

            inputUser.style.borderColor = "#d0d0d0"; 
            inputName.style.borderColor = "#d0d0d0"; 
            inputEmail.style.borderColor = "#d0d0d0"; 
            var isCheck = true; 
            //validate rỗng trong sửa thông tin cá nhân 
            if(this.accoutadmin === "" || this.accoutadmim === null || this.accoutadmin === undefined){
                this.thongbao(inputUser, "Tên tài khoản không được để trống."); 
                isCheck = false; 
            }

            //validate rỗng trong họ và tên 
            if(this.nameadmin === "" || this.nameadmin === null || this.nameadmin === undefined){
                this.thongbao(inputName, "Họ và tên không được để trống."); 
                isCheck = false; 
            }

            //validate rỗng trong email 
            if(this.emailadmin === "" || this.emailadmin === null || this.emailadmin === undefined){
                this.thongbao(inputEmail, "Email không được để trống."); 
                isCheck = false; 
            }
            else {
                for (const item of this.user) {
                    if(item.userEmail === this.emailadmin){
                        this.thongbao(inputEmail, "Đã có tài khoản đăng ký bằng email này."); 
                        isCheck = false; 
                    }
                }
            }

            if(isCheck){
                this.admin.userName = this.nameadmin;
                this.admin.userAccout = this.accoutadmin; 
                this.admin.userEmail = this.emailadmin; 
                BaseRequest.put("user", this.admin)
                .then(response => {
                    console.log(response.data); 
                    document.getElementById("dialog_updateAdmin").style.opacity = 1;  
                })
                .catch(error => {
                    console.log(error.message); 
                })
            }


        }, 
        updatePass: function(){
            const _delete = document.querySelectorAll(".error-pass-admin"); 
            this.isCheck = true;
            for (const item of _delete) {
                item.parentNode.removeChild(item);
            }

            const inputPassnew = document.getElementById("pass-new-admin"); 
            const inputCPassnew= document.getElementById("pass-confirm-admin"); 

            inputPassnew.style.borderColor = "#d0d0d0"; 
            inputCPassnew.style.borderColor = "#d0d0d0"; 

            var isCheck = true; 
            //validate rỗng trong sửa thông tin cá nhân 
            if(this.passnewadmin === "" || this.passnewadmin === null || this.passnewadmin === undefined){
                this.thongbao1(inputPassnew, "Mật khẩu mới không được để trống."); 
                isCheck = false; 
            }
            else {
                //validate rỗng trong họ và tên 
                if(this.passconfirmadmin === "" || this.passconfirmadmin === null || this.passconfirmadmin === undefined){
                    this.thongbao1(inputCPassnew, "Xác nhận mật khẩu mới không được để trống."); 
                    isCheck = false; 
                }
                else {
                    if(this.passnewadmin !== this.passconfirmadmin){
                        this.thongbao1(inputCPassnew, "Xác nhận mật khẩu mới không chính xác."); 
                        isCheck = false; 
                    }
                }
            }

            if(isCheck){
                this.admin.userPassword = this.passnewadmin; 
                BaseRequest.put("user", this.admin)
                .then(response => {
                    console.log(response.data); 
                    document.getElementById("dialog_updateAdmin").style.opacity = 1; 
                })
                .catch(error => {
                    console.log(error.message); 
                })
            }
        }, 
        updateImage: function(){
            const formData = new FormData(); 
            formData.append("UserId", this.admin.userId); 
            formData.append("UserCode", this.admin.userCode); 
            formData.append("UserName", this.admin.userName); 
            formData.append("UserAccout", this.admin.userAccout); 
            formData.append("UserEmail", this.admin.userEmail); 
            formData.append("UserPassword", this.admin.userPassword); 
            formData.append("UserType", this.admin.userType); 
            formData.append("Image", this.file);
            
            console.log(this.admin); 
            console.log(this.file); 
            BaseRequest.put("user/UpdateImage", formData, {
                headers: {
                    'Content-Type': 'multipart/form-data'
                }
            })
            .then(response => {
                console.log(response.data); 
                document.getElementById("dialog_updateAdmin").style.opacity = 1;
                const reander = new FileReader(); 
                reander.onload = (e) => {
                    this.imageUrl = e.target.result; 
                };
            })
            .catch(error => {
                console.log(error.message);  
            })
        },
        checkFileUploadAdmin(event){
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
        /**
         * Hàm thêm mới thông báo khi có lỗi vào sau muỗi input lỗi. 
         * created by: NTXuan - 22/01/2024. 
         */
        thongbao: function(item, mes){
            item.style.borderColor = "red"; 
            let p = document.createElement("p"); 
            let node = document.createTextNode(mes); 
            p.appendChild(node); 
            p.classList.add("error-infor-admin");
            p.style.color = "red"; 
            p.style.fontStyle = "italic"; 
            p.style.fontSize = "0.9rem"; 
            item.after(p);
            //item.setAttribute("title", mes); 
        },
        /**
         * Hàm thêm mới thông báo khi có lỗi vào sau muỗi input lỗi. 
         * created by: NTXuan - 22/01/2024. 
         */
        thongbao1: function(item, mes){
            item.style.borderColor = "red"; 
            let p = document.createElement("p"); 
            let node = document.createTextNode(mes); 
            p.appendChild(node); 
            p.classList.add("error-pass-admin");
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
/**style dialog thông báo khi update thành công */
.dialog_updateadmin {
    background-color: #fff; 
    width: 500px; 
    height: 70px;
    border-radius: 5px;
    padding-left: 16px; 
    padding-right: 16px; 
    position: absolute; 
    top: 10px; 
    left: 30%; 
    display: flex; 
    justify-content: space-between;
    align-items: center;
    opacity: 0;
    transition: opacity 2s ease; 
}
.bugg_dialogupdateadmin {
    height: 100%;
    align-items: center;
}
/**----------------------------------------------- */
.profile_admin {
    width: calc(100% - 250px); 
    height: calc(100vh - 56px); 
    background-color: #EEEFF1; 
    float: left;
}
.profile_admin > h1 {
    font-size: 2.5rem; 
    font-weight: 700;
    margin-top: 24px; 
    margin-left: 24px; 
}
.profile_admin_info {
    width: calc(100% - 24px - 24px); 
    margin: auto; 
    background-color: #fff;
    height: calc(100vh - 56px - 100px - 24px);
    display: flex; 
    gap: 24px;
}
.admin_info {
    display: flex; 
    flex-direction: column;
    margin-left: 24px; 
    padding-top: 24px; 
}
.admin_info > label {
    font-size: 1.25rem; 
}
.admin_info > label > input {
    height: 36px;
    width: 350px; 
    margin-top: 8px; 
    margin-bottom: 16px; 
    border: 1px solid #d0d0d0; 
    outline: none;
    padding-left: 8px; 
}
.admin_info > label > input:focus {
    border: 1px solid #50B83C; 
}
.admin_pass {
    display: flex; 
    flex-direction: column;
    margin-left: 24px; 
    padding-top: 24px;
}
.admin_pass > label > input {
    height: 36px;
    width: 350px;
    margin-top: 8px; 
    margin-bottom: 16px; 
    border: 1px solid #d0d0d0; 
    outline: none;
    padding-left: 8px; 
}
.admin_pass > label > input:focus {
    border: 1px solid #50B83C; 
}
.admin_pass > label {
    font-size: 1.25rem; 
}
.admin_logo {
    width: 200px; 
    margin-top: 24px; 
    margin-left: 24px; 
}
.admin_logo > img {
    margin-top: 32px;
    width: 100%; 
    height: 200px;
    border-radius: 50%;
    margin-bottom: 8px; 
}
.btn-info-admin > button{
    width: 350px; 
    height: 46px;
    border: 1px solid #d0d0d0; 
    margin-top: 16px;
    background-color: #50B83C; 
    color: #fff; 
    border-radius: 4px;
}
.image-admin {
    margin-top: 16px;
    font-size: 1.25rem;  
}
.admin_info > h3 {
    margin-bottom: 24px; 
}
.admin_pass > h3 {
    margin-bottom: 24px; 
}
</style>

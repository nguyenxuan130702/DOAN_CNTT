<template>
    <div class="form_course" id="form_update_user">
        <div class="form_course_title">
            <h1 id="title_user_form">Sửa thông tin tài khoản</h1>
            <div class="icon_close" v-on:click="closeFormNew()"></div>
        </div>
        <div class="form_course-content-x">
            <div class="style-image-admin-create">
                <b>Ảnh đại diện</b> <br>
                <img :src="uimageUrl" alt="">
                <input type="file" name="imageu-u_face" id="image-u_face" @change="checkFileUploadAdmin" accept="image/*">
            </div>
            <div class="style-info-admin-create">
                <div for="">
                    <b>Họ và tên</b> <br>
                    <input type="text" placeholder="họ và tên" id="u-hoten" name="u-hoten" v-model="uhoten">
                </div>
                <div>
                    <b>Tên tài khoản</b><br>
                    <input type="text" placeholder="tên tài khoản" id="u-tentk" name="u-tentk" v-model="utentk">
                </div>
                <div>
                    <b>Email</b> <br>
                    <input type="text" placeholder="email" id="u-email" name="u-email" v-model="uemail">
                </div>
                <div>
                    <input type="radio" id="u-nd" value="0" v-model="utype" checked/> 
                    <label for="u-nd">Người dùng </label>
                    <input type="radio" id="u-ad" value="1" v-model="utype"/> 
                    <label for="u-ad">Người quản trị</label>
                </div>
            </div>
            <div class="a-user-pass">
                <div>
                    <b>Mật khẩu </b><br>
                    <input type="password" placeholder="mật khẩu" id="u-passn" name="u-passn" v-model="upassword">
                </div>
                <div>
                    <b>Xác minh lại mật khẩu</b> <br>
                    <input type="password" placeholder="xác minh mật khẩu" id="u-confirmpass" name="u-confirmpass" v-model="uconfirmpass">
                </div>
            </div>
        </div>
        <div class="course_btn" v-on:click="validatePutUser()">
            <button id="btnu-user-form">Cập nhật</button>
        </div>
    </div>

    <!--Thông báo thêm mới thành công-->
    <div class="dialog_updateadmin" id="dialog_suauser" >
        <div class="dialog_dangky_title bugg_dialogupdateadmin">
            <div class="icon_thanhcong"></div>
            <p id="dialog_dangky_content">Cập nhật người dùng thành công.</p>
        </div>
        <div class="icon_close" v-on:click="closeDialogPostUser()"></div>
    </div>
</template>
<script>
import BaseRequest from '@/core/BaseRequest';
export default{
    props: ['propName', 'propUser'], 
    data(){
        return {
            uhoten: "", 
            utentk: "", 
            uemail: "", 
            upassword: "", 
            uconfirmpass: "",
            utype: 0,  
            uimageUrl: "https://images.fbox.fpt.vn/wordpress-blog/2023/11/ponyp-va-sosuke.jpg", 
            file: '', 
            listUser: [], 
            code: 0, 
            updateUser: {}, 
        }
    }, 
    watch: {
        propName: function(){
            this.uhoten = this.propName.userName; 
            this.utentk = this.propName.userAccout; 
            this.uemail = this.propName.userEmail;
            this.upassword = this.propName.userPassword;
            if(this.propName.userImage !== null){ 
                this.uimageUrl = this.propName.userImage;
            } 
            else {
                this.uimageUrl = "https://images.fbox.fpt.vn/wordpress-blog/2023/11/ponyp-va-sosuke.jpg"
            }
            this.utype = this.propName.userType; 
            this.updateUser = this.propName; 
        }, 
        propUser: function(){
            this.listUser = this.propUser;
        }
    },  

    methods: {
        closeDialogPostUser: function(){
            document.getElementById("dialog_suauser").style.opacity = 0; 
        }, 
        closeFormNew: function(){
            document.getElementById("form_update_user").style.display = "none"; 
            document.getElementById("user_back").style.display = "none"; 
        }, 
        validatePutUser: function(){
            const _delete = document.querySelectorAll(".error-user"); 
            this.isCheck = true;
            for (const item of _delete) {
                item.parentNode.removeChild(item);
            }

            //validate rong cac phan tu truoc 
                const inputHoten = document.getElementById("u-hoten"); 
                const inputTentk = document.getElementById("u-tentk");
                const inputEmail = document.getElementById("u-email"); 
                const inputPass = document.getElementById("u-passn");  
                const inputCPassnew = document.getElementById("u-confirmpass"); 

                inputHoten.style.borderColor = "#d0d0d0"; 
                inputTentk.style.borderColor = "#d0d0d0"; 
                inputEmail.style.borderColor = "#d0d0d0"; 
                inputPass.style.borderColor = "#d0d0d0"; 
                inputCPassnew.style.borderColor = "#d0d0d0";

                var isCheck = true; 

            //validate ho ten khong dc de trong
                if(this.uhoten === "" || this.uhoten === null || this.uhoten === undefined){
                    this.thongbao(inputHoten, "Mã người dùng không được để trống."); 
                    isCheck = false; 
                }
            //validate ten tai khoan khong dc de trong 
                if(this.utentk === "" || this.utentk === null || this.utentk === undefined){
                    this.thongbao(inputTentk, "Tên tài khoản không được để trống."); 
                    isCheck = false; 
                }
            //validate email khong duoc de trong.
                if(this.uemail === "" || this.uemail === null || this.uemail === undefined){
                    this.thongbao(inputEmail, "Email không được để trống."); 
                    isCheck = false;
                }
                else {
                    if(!this.checkEmail(this.uemail)){
                        this.thongbao(inputEmail, "Email không hợp lệ."); 
                        isCheck = false; 
                    }
                    else {
                        for (const item of this.listUser) {
                            if(this.uemail === item.userEmail && this.uemail !== this.propName.userEmail){
                                this.thongbao(inputEmail, "Đã có tài khoản sử dụng email này.")
                                isCheck = false;  
                                break; 
                            }
                        }
                    }
                }
            //validate password khong duoc de trong. 
                if(this.upassword === "" || this.upassword === null || this.upassword === undefined){
                    this.thongbao(inputPass, "Password không được để trống."); 
                    isCheck = false; 
                }
                else {
                    //validate confirm password khong duoc de trong
                    if(this.uconfirmpass === "" || this.uconfirmpass === null || this.uconfirmpass === undefined){
                        this.thongbao(inputCPassnew, "Mật khẩu xác thực không được để trống."); 
                        isCheck = false; 
                    }
                    else {
                        //validate confirm pass khong trung voi mat khau. 
                        if(this.uconfirmpass !== this.upassword){
                            this.thongbao(inputCPassnew, "Mật khẩu xác thực không chính xác."); 
                            isCheck = false; 
                        }
                    }
                }
            //thực hiện cập nhật vào trong database. 
                if(isCheck){
                    //Nếu không thay đổi ảnh thì sẽ giữ nguyên ảnh cũ và sử dụng nguồn api update theo chỗi string. 
                    if(this.file === null || this.file === undefined || this.file === ""){
                        this.updateUser.userName = this.uhoten; 
                        this.updateUser.userAccout = this.utentk; 
                        this.updateUser.userEmail = this.uemail; 
                        this.updateUser.userPassword = this.upassword; 
                        this.updateUser.userType = this.utype; 

                        BaseRequest.put("user", this.updateUser)
                        .then(response => {
                            console.log(response.data); 
                            document.getElementById("dialog_suauser").style.opacity = 1;
                            const reander = new FileReader(); 
                            reander.onload = (e) => {
                                this.uimageUrl = e.target.result; 
                            };
                            document.getElementById("form_update_user").style.display = "none"; 
                            document.getElementById("user_back").style.display = "none";  
                        })
                        .catch(error => {
                            console.log(error.message); 
                        })
                    }
                    else {
                        //Nếu có thay đổi ảnh thì sử dụng api update theo ảnh upload. 
                        const formData = new FormData();
                    
                        formData.append("UserId", this.propName.userId); 
                        formData.append("UserCode", this.propName.userCode); 
                        formData.append("UserName", this.uhoten); 
                        formData.append("UserAccout", this.utentk); 
                        formData.append("UserEmail", this.uemail); 
                        formData.append("UserPassword", this.upassword); 
                        formData.append("UserType", this.utype);
                        formData.append("Image", this.file);  

                        BaseRequest.put("user/UpdateImage", formData)
                        .then(response => {
                            console.log(response.data); 
                            document.getElementById("dialog_suauser").style.opacity = 1;
                            const reander = new FileReader(); 
                            reander.onload = (e) => {
                                this.uimageUrl = e.target.result; 
                            };
                            document.getElementById("form_update_user").style.display = "none"; 
                            document.getElementById("user_back").style.display = "none";  
                        })
                        .catch(error => {
                            console.log(error.message); 
                        })
                    }
                }
        }, 
        checkEmail: function(number){
            const regExp = /^(([^<>()\\.,;:\s@"]+(\.[^<>()\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/; 
            if(regExp.test(number)){
                return true; 
            }
            return false; 
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
            p.classList.add("error-user");
            p.style.color = "red"; 
            p.style.fontStyle = "italic"; 
            p.style.fontSize = "0.9rem"; 
            item.after(p);
            //item.setAttribute("title", mes); 
        },
        checkFileUploadAdmin(event){
            this.file = event.target.files[0]; 
            if(this.file){
                //đọc URL của file và gán vào imageUrl để hiển thị 
                const reander = new FileReader(); 
                reander.onload = (e) => {
                    this.uimageUrl = e.target.result; 
                }; 
                reander.readAsDataURL(this.file); 
            }
        },
    }
}
</script>
<style scoped>
/**style dialog thong bao */
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
    z-index: 1;
    opacity: 0;
    transition: opacity 2s ease; 
}
.bugg_dialogupdateadmin {
    height: 100%;
    align-items: center;
}
/**---------------------------------------------------- */
.form_course {
    z-index: 3; 
}
#form_update_user {
    width: 1000px; 
    left: 20%; 
    top: 120px; 
}
.form_course-content-x > div > input[type = "password"]{
    height: 36px;
    width: 100%;
    border: 1px solid #d0d0d0; 
    padding-left: 8px; 
    outline: none; 
    margin-top: 16px; 
}
.style-image-admin-create > img {
    width: 200px; 
    height: 200px;
    border-radius: 50%;
    margin-top: 16px; 
    margin-bottom: 16px; 
    margin-left: 40px; 
}
.style-info-admin-create > div {
    width: 100%; 
}
.style-info-admin-create > div > input[type="text"]{
    height: 36px;
    width: 100%; 
    border: 1px solid #d0d0d0; 
    margin-top: 8px; 
    margin-bottom: 16px; 
    padding-left: 8px; 
    outline: none;
}
.style-info-admin-create > div > input[type="radio"]{
    margin-right: 8px; 
}
.style-info-admin-create > div > label {
    margin-right: 16px; 
}
.style-info-admin-create > div > input[type="text"]:focus {
    border: 1px solid #50B83C;
}
.a-user-pass > div > input{
    width: 100%; 
    height: 36px;
    border: 1px solid #d0d0d0; 
    margin-top: 8px; 
    margin-bottom: 16px; 
    outline: none;
    padding-left: 8px; 
}
.a-user-pass > div > input:focus {
    border: 1px solid #50B83C; 
}
input::placeholder {
    font-style: italic;
    font-size: 0.9rem; 
}
</style>

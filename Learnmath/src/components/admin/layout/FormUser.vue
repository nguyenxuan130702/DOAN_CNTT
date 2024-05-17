<template>
    <div class="form_course" id="form_user">
        <div class="form_course_title">
            <h1 id="title_user_form">Thêm mới người dùng</h1>
            <div class="icon_close" v-on:click="closeFormNew()"></div>
        </div>
        <div class="form_course-content-x">
            <div class="style-image-admin-create">
                <b>Ảnh đại diện</b> <br>
                <img :src="imageUrl" alt="">
                <input type="file" name="image_face" id="image_face" @change="checkFileUploadAdmin" accept="image/*">
            </div>
            <div class="style-info-admin-create">
                <div for="">
                    <b>Họ và tên</b> <br>
                    <input type="text" placeholder="họ và tên" id="hoten" name="hoten" v-model="hoten">
                </div>
                <div>
                    <b>Tên tài khoản</b> <br>
                    <input type="text" placeholder="tên tài khoản" id="tentk" name="tentk" v-model="tentk">
                </div>
                <div>
                    <b>Email</b> <br>
                    <input type="text" placeholder="email" id="email" name="email" v-model="email">
                </div>
                <div>
                    <input type="radio" id="nd" value="0" v-model="type" checked/> 
                    <label for="nd">Người dùng </label>
                    <input type="radio" id="ad" value="1" v-model="type"/> 
                    <label for="ad">Người quản trị</label>
                </div>
            </div>
            <div class="a-user-pass">
                <div>
                    <b>Mật khẩu</b> <br>
                    <input type="password" placeholder="mật khẩu" id="passn" name="passn" v-model="password">
                </div>
                <div>
                    <b>Xác minh lại mật khẩu</b> <br>
                    <input type="password" placeholder="xác minh mật khẩu" id="confirmpass" name="confirmpass" v-model="confirmpass">
                </div>
            </div>
        </div>
        <div class="course_btn" v-on:click="validatePushUser()">
            <button id="btn-user-form">Thêm mới</button>
        </div>
    </div>

</template>
<script>
import BaseRequest from '@/core/BaseRequest';
export default{
    props: ['propUser'], 
    data(){
        return {
            hoten: "", 
            tentk: "", 
            email: "", 
            password: "", 
            confirmpass: "",
            type: 0,  
            imageUrl: "https://images.fbox.fpt.vn/wordpress-blog/2023/11/ponyp-va-sosuke.jpg", 
            file: '', 
            listUser: [], 
            code: 0, 
            userAdd: {}, 
        }
    }, 
    watch: {
        propUser: function(){
            this.listUser = this.propUser;
            this.code = this.listUser.length + 1; 
        }
    },  

    methods: {
        closeDialogPostUser: function(){
            document.getElementById("dialog_themmoiuser").style.opacity = 0; 
        }, 
        closeFormNew: function(){
            document.getElementById("form_user").style.display = "none"; 
            document.getElementById("user_back").style.display = "none"; 
        }, 
        validatePushUser: function(){
            const _delete = document.querySelectorAll(".error-user"); 
            this.isCheck = true;
            for (const item of _delete) {
                item.parentNode.removeChild(item);
            }

            //validate rong cac phan tu truoc 
                const inputHoten = document.getElementById("hoten"); 
                const inputTentk = document.getElementById("tentk");
                const inputEmail = document.getElementById("email"); 
                const inputPass = document.getElementById("passn");  
                const inputCPassnew = document.getElementById("confirmpass"); 

                inputHoten.style.borderColor = "#d0d0d0"; 
                inputTentk.style.borderColor = "#d0d0d0"; 
                inputEmail.style.borderColor = "#d0d0d0"; 
                inputPass.style.borderColor = "#d0d0d0"; 
                inputCPassnew.style.borderColor = "#d0d0d0";

                var isCheck = true; 

            //validate ho ten khong dc de trong
                if(this.hoten === "" || this.hoten === null || this.hoten === undefined){
                    this.thongbao(inputHoten, "Mã người dùng không được để trống."); 
                    isCheck = false; 
                }
            //validate ten tai khoan khong dc de trong 
                if(this.tentk === "" || this.tentk === null || this.tentk === undefined){
                    this.thongbao(inputTentk, "Tên tài khoản không được để trống."); 
                    isCheck = false; 
                }
            //validate email khong duoc de trong.
                if(this.email === "" || this.email === null || this.email === undefined){
                    this.thongbao(inputEmail, "Email không được để trống."); 
                    isCheck = false;
                }
                else {
                    if(!this.checkEmail(this.email)){
                        this.thongbao(inputEmail, "Email không hợp lệ."); 
                        isCheck = false; 
                    }
                    else {
                        for (const item of this.listUser) {
                            if(this.email === item.userEmail){
                                this.thongbao(inputEmail, "Đã có tài khoản sử dụng email này.")
                                isCheck = false;  
                                break; 
                            }
                        }
                    }
                }
            //validate password khong duoc de trong. 
                if(this.password === "" || this.password === null || this.password === undefined){
                    this.thongbao(inputPass, "Password không được để trống."); 
                    isCheck = false; 
                }
                else {
                    //validate confirm password khong duoc de trong
                    if(this.confirmpass === "" || this.confirmpass === null || this.confirmpass === undefined){
                        this.thongbao(inputCPassnew, "Mật khẩu xác thực không được để trống."); 
                        isCheck = false; 
                    }
                    else {
                        //validate confirm pass khong trung voi mat khau. 
                        if(this.confirmpass !== this.password){
                            this.thongbao(inputCPassnew, "Mật khẩu xác thực không chính xác."); 
                            isCheck = false; 
                        }
                    }
                }
            //thêm mới vào API. 
                if(isCheck){
                    var index = this.code; 
                    var userCode = "U" + index;

                    if(this.file === null || this.file === undefined || this.file === ""){
                        this.userAdd.userCode = userCode; 
                        this.userAdd.userName = this.hoten; 
                        this.userAdd.userAccout = this.tentk; 
                        this.userAdd.userEmail = this.email; 
                        this.userAdd.userPassword = this.password; 
                        this.userAdd.userType = this.type; 
                        this.userAdd.userImage = "https://ss-images.saostar.vn/wp700/pc/1613810558698/Facebook-Avatar_3.png"; 

                        BaseRequest.post("user", this.userAdd)
                        .then(response => {
                            console.log(response.data); 
                            document.getElementById("dialog_dangky_content").innerHTML = "Thêm mới người dùng thành công."; 
                            document.getElementById("dialog_suauser").style.opacity = 1;
                            const reander = new FileReader(); 
                            reander.onload = (e) => {
                                this.imageUrl = e.target.result; 
                            };
                            document.getElementById("form_user").style.display = "none"; 
                            document.getElementById("user_back").style.display = "none";
                        })
                        .catch(error => {
                            console.log(error.message); 
                        })
                    }
                    else {
                        const formData = new FormData();
                        formData.append("UserCode", userCode); 
                        formData.append("UserName", this.hoten); 
                        formData.append("UserAccout", this.tentk); 
                        formData.append("UserEmail", this.email); 
                        formData.append("UserPassword", this.password); 
                        formData.append("UserType", this.type);
                        formData.append("Image", this.file);  

                        BaseRequest.post("user/PostImage", formData)
                        .then(response => {
                            console.log(response.data); 
                            document.getElementById("dialog_dangky_content").innerHTML = "Thêm mới người dùng thành công."; 
                            document.getElementById("dialog_suauser").style.opacity = 1;
                            const reander = new FileReader(); 
                            reander.onload = (e) => {
                                this.imageUrl = e.target.result; 
                            };
                            document.getElementById("form_user").style.display = "none"; 
                            document.getElementById("user_back").style.display = "none"; 
                            this.confirmpass = ""; 
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
                    this.imageUrl = e.target.result; 
                }; 
                reander.readAsDataURL(this.file); 
            }
        },
    }
}
</script>
<style scoped>
.form_course {
    z-index: 3; 
}
#form_user {
    width: 1000px; 
    left: 20%; 
    top: 120px; 
}
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
.style-info-admin-create > div > input[type="password"]{
    margin-top: 8px; 
}
.style-info-admin-create > div:last-child {
    display: flex; 
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
    font-style: italic !important;
    font-size: 0.9rem !important; 
}
</style>

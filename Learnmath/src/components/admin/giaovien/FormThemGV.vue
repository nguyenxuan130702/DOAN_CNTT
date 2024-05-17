<template>
    <div class="form_course" id="form_teacher-post">
        <div class="form_course_title">
            <h1 id="title_user_form">Thêm mới giáo viên</h1>
            <div class="icon_close" v-on:click="closeFormNew()"></div>
        </div>
        <div class="form_course-content-x">
            <div class="style-info-admin-create">
                <div for="">
                    <b>Họ và tên</b> <br>
                    <input type="text" placeholder="họ và tên" id="teacher-hoten" name="hoten" v-model="hoten">
                </div>
                <label class="bugg-email-teacher">
                    <b>Email</b> <br>
                    <input type="text" placeholder="email" id="teacher-email" name="email" v-model="email">
                </label>
            </div>
            <div class="a-user-pass">
                <div>
                    <b>Mật khẩu</b> <br>
                    <input type="password" placeholder="mật khẩu" id="teacher-passn" name="passn" v-model="password">
                </div>
                <div>
                    <b>Xác minh lại mật khẩu</b> <br>
                    <input type="password" placeholder="xác minh mật khẩu" id="teacher-confirmpass" name="confirmpass" v-model="confirmpass">
                </div>
            </div>
            <div class="style-image-admin-create">
                <b>Giới thiệu</b> <br>
                <textarea name="" id="teacher-mota" cols="30" rows="10" v-model="mota"></textarea>
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
    props: ['propTeacher'], 
    data(){
        return {
            hoten: "", 
            email: "", 
            password: "", 
            confirmpass: "",
            listUser: [], 
            code: 0, 
            userAdd: {}, 
            mota: '', 
        }
    }, 
    watch: {
        propTeacher: function(){
            this.listUser = this.propTeacher;
            this.code = this.listUser.length + 1; 
        }
    },  

    methods: {
        closeDialogPostUser: function(){
            document.getElementById("dialog_themmoiuser").style.opacity = 0; 
        }, 
        closeFormNew: function(){
            document.getElementById("form_teacher-post").style.display = "none"; 
            document.getElementById("teacher_back").style.display = "none"; 
        }, 
        validatePushUser: function(){
            const _delete = document.querySelectorAll(".error-user"); 
            this.isCheck = true;
            for (const item of _delete) {
                item.parentNode.removeChild(item);
            }

            //validate rong cac phan tu truoc 
                const inputHoten = document.getElementById("teacher-hoten"); 
                const inputEmail = document.getElementById("teacher-email"); 
                const inputPass = document.getElementById("teacher-passn");  
                const inputCPassnew = document.getElementById("teacher-confirmpass"); 


                inputHoten.style.borderColor = "#d0d0d0"; 
                inputEmail.style.borderColor = "#d0d0d0"; 
                inputPass.style.borderColor = "#d0d0d0"; 
                inputCPassnew.style.borderColor = "#d0d0d0";

                var isCheck = true; 

            //validate ho ten khong dc de trong
                if(this.hoten === "" || this.hoten === null || this.hoten === undefined){
                    this.thongbao(inputHoten, "Họ và tên không được để trống."); 
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
                            if(this.email === item.teacherEmail){
                                this.thongbao(inputEmail, "Đã có giáo viên sử dụng email này.")
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

                        this.userAdd.teacherCode = userCode; 
                        this.userAdd.teacherName = this.hoten; 
                        this.userAdd.teacherEmail = this.email; 
                        this.userAdd.teacherPassword = this.password; 
                        this.userAdd.teacherDescription = this.mota; 
                        BaseRequest.post("teacher", this.userAdd)
                        .then(response => {
                            console.log(response.data); 
                            document.getElementById("dialog_dangky_content").innerHTML = "Thêm mới giáo viên thành công."; 
                            document.getElementById("dialog_suauser").style.opacity = 1;
                            document.getElementById("form_teacher-post").style.display = "none"; 
                            document.getElementById("teacher_back").style.display = "none";
                        })
                        .catch(error => {
                            console.log(error.message); 
                        })
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
.bugg-email-teacher > input {
    height: 36px;
    width: 100%; 
    border: 1px solid #d0d0d0; 
    margin-top: 8px; 
    margin-bottom: 16px; 
    padding-left: 8px; 
    outline: none;
}
.bugg-email-teacher > input:focus {
    border: 1px solid #50B83C;
}
</style>

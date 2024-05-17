<template>
    <div class="form_course" id="form_update_teacher">
        <div class="form_course_title">
            <h1 id="title_user_form">Sửa thông tin tài khoản</h1>
            <div class="icon_close" v-on:click="closeFormNew()"></div>
        </div>
        <div class="form_course-content-x">
            <div class="style-info-admin-create">
                <div for="">
                    <b>Họ và tên</b> <br>
                    <input type="text" placeholder="họ và tên" id="u-hoten-teacher" name="u-hoten" v-model="uhoten">
                </div>
                <div>
                    <b>Email</b> <br>
                    <input type="text" placeholder="email" id="u-email-teacher" name="u-email" v-model="uemail">
                </div>
            </div>
            <div class="a-user-pass">
                <div>
                    <b>Mật khẩu </b><br>
                    <input type="password" placeholder="mật khẩu" id="u-passn-teacher" name="u-passn" v-model="upassword">
                </div>
                <div>
                    <b>Xác minh lại mật khẩu</b> <br>
                    <input type="password" placeholder="xác minh mật khẩu" id="u-confirmpass-teacher" name="u-confirmpass" v-model="uconfirmpass">
                </div>
            </div>
            <div class="style-image-admin-create">
                <b>Giới thiệu</b> <br>
                <textarea name="" id="teacher-update-mota" cols="30" rows="10" v-model="mota"></textarea>
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
    props: ['propGetTeacher', 'propTeacher'], 
    data(){
        return {
            uhoten: "", 
            uemail: "", 
            upassword: "", 
            uconfirmpass: "",
            listUser: [], 
            code: 0, 
            updateUser: {}, 
            mota: '', 
        }
    }, 
    watch: {
        propGetTeacher: function(){
            this.uhoten = this.propGetTeacher.teacherName; 
            this.uemail = this.propGetTeacher.teacherEmail;
            this.upassword = this.propGetTeacher.teacherPassword; 
            this.mota = this.propGetTeacher.teacherDescription; 
            this.updateUser = this.propGetTeacher; 
        }, 
        propTeacher: function(){
            this.listUser = this.propTeacher;
        }
    },  

    methods: {
        closeDialogPostUser: function(){
            document.getElementById("dialog_suauser").style.opacity = 0; 
        }, 
        closeFormNew: function(){
            document.getElementById("form_update_teacher").style.display = "none"; 
            document.getElementById("teacher_back").style.display = "none"; 
        }, 
        validatePutUser: function(){
            const _delete = document.querySelectorAll(".error-user"); 
            this.isCheck = true;
            for (const item of _delete) {
                item.parentNode.removeChild(item);
            }

            //validate rong cac phan tu truoc 
                const inputHoten = document.getElementById("u-hoten-teacher"); 
                const inputEmail = document.getElementById("u-email-teacher"); 
                const inputPass = document.getElementById("u-passn-teacher");  
                const inputCPassnew = document.getElementById("u-confirmpass-teacher"); 

                inputHoten.style.borderColor = "#d0d0d0"; 
                inputEmail.style.borderColor = "#d0d0d0"; 
                inputPass.style.borderColor = "#d0d0d0"; 
                inputCPassnew.style.borderColor = "#d0d0d0";

                var isCheck = true; 

            //validate ho ten khong dc de trong
                if(this.uhoten === "" || this.uhoten === null || this.uhoten === undefined){
                    this.thongbao(inputHoten, "Mã người dùng không được để trống."); 
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
                            if(this.uemail !== item.teacherEmail && this.uemail !== this.propTeacher.teacherEmail){
                                this.thongbao(inputEmail, "Đã có giáo viên sử dụng email này.")
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
                    this.updateUser.teacherName = this.uhoten; 
                        this.updateUser.teacherEmail = this.uemail; 
                        this.updateUser.teacherPassword = this.upassword; 
                        this.updateUser.teacherDescription = this.mota; 

                        BaseRequest.put("teacher", this.updateUser)
                        .then(response => {
                            console.log(response.data); 
                            document.getElementById("dialog_suauser").style.opacity = 1;
                            document.getElementById("form_update_teacher").style.display = "none"; 
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

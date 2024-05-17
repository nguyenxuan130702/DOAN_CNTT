<template>
    <NavBar></NavBar>
    <SideBar></SideBar>
    <div class="profile_admin">
        <h1>Thông tin giáo viên</h1>
        <div class="profile_admin_info">
            <div class="teacher_info">
                <h3>Thông tin cá nhân</h3>
                <div class="teacher_infor-form">
                    <div>
                        <label for="">
                            Họ và tên <br>
                            <input type="text" id="name-teacher" name="name-teacher" v-model="nameadmin">
                        </label>
                        <label for="">
                            Email <br>
                            <input type="text" id="email-teacher" name="email-teacher" v-model="emailadmin">
                        </label>
                    </div>
                    <div>
                        <label for="">
                            Mô tả: <br>
                            <textarea name="" id="" cols="30" rows="10" v-model="mota"></textarea>
                        </label>
                    </div>
                </div>
                <label for="" class="btn-info-admin button-teacher" v-on:click="updateInfor()">
                    <button>Cập nhật thông tin</button>
                </label>
            </div>

            <div class="admin_pass">
                <h3>Đổi mật khẩu</h3>
                <label for="">
                    Mật khẩu cũ <br>
                    <input type="password" id="ole-pass-teacher" v-model="oldpassword">
                </label>
                <label for="">
                    Mật khẩu mới <br>
                    <input type="password" id="pass-new-teacher" name="pass-new-admin" v-model="passnewadmin">
                </label>
                <label for="">
                    Nhập lại mật khẩu mới <br>
                    <input type="password" id="pass-confirm-teacher" name="pass-confirm-admin" v-model="passconfirmadmin">
                </label>
                <label for="" class="btn-info-admin" v-on:click="updatePass()">
                    <button>Cập nhật mật khẩu</button>
                </label>
            </div>
        </div>
    </div>
</template>
<script>
import NavBar from '@/components/teacher/layout/NavbarTeacher.vue'
import SideBar from '@/components/teacher/layout/SidebarTeacher.vue'
import BaseRequest from '@/core/BaseRequest'
export default {
    components: {
        NavBar, 
        SideBar
    }, 
    data(){
        return {
            nameadmin: '', 
            emailadmin: '', 
            passnewadmin: '', 
            passconfirmadmin: '', 
            mota: '', 
            teacher: {}, 
            oldpassword: '', 
            user: [], 
        }
    }, 
    mounted(){
        this.loadData(); 
    }, 
    methods: {
        loadData: function(){
            this.teacher = JSON.parse(localStorage.getItem("teacherLogin")); 
            this.nameadmin = this.teacher.teacherName; 
            this.emailadmin = this.teacher.teacherEmail; 
            this.mota = this.teacher.teacherDescription; 

            BaseRequest.get("teacher")
            .then(response => {
                this.user = response.data; 
            })
            .catch(error => {
                console.log(error.message); 
            })
        }, 
        updateInfor: function(){
            const input1 = document.getElementById("name-teacher"); 
            const input2 = document.getElementById("email-teacher"); 

            const _delete = document.querySelectorAll(".error-hoso"); 
            this.isCheck = true;
            for (const item of _delete) {
                item.parentNode.removeChild(item);
            }

            input1.style.borderColor = "#d0d0d0"; 
            input2.style.borderColor = "#d0d0d0";

            var isCheck = true; 
            if(this.nameadmin === "" || this.nameadmin === undefined || this.nameadmin === null){
                this.thongbao(input1, "Họ tên không được phép để trống."); 
                isCheck = false;
            }
            else {
                if(this.nameadmin === this.teacher.teacherName){
                    isCheck = false; 
                }
            }

            //validate rỗng trong email 
            if(this.emailadmin === "" || this.emailadmin === null || this.emailadmin === undefined){
                this.thongbao(input2, "Email không được để trống."); 
                isCheck = false; 
            }
            else {
                if(this.checkEmail(this.emailadmin)){
                    if(this.emailadmin === this.teacher.teacherEmail){ 
                        isCheck = false; 
                    }
                    else{
                        for (const item of this.user) {
                            if(item.teacherEmail === this.emailadmin){
                                this.thongbao(input2, "Đã có giáo viên đăng ký bằng email này."); 
                                isCheck = false; 
                            }
                        }
                    }
                }
                else {
                    this.thongbao(input2, "Email không đúng định dạng."); 
                    isCheck = false; 
                }
            }

            if(this.mota !== "" && this.mota === this.teacher.teacherDescription){
                isCheck = false; 
            }

            if(isCheck){
                this.teacher.teacherName = this.nameadmin; 
                this.teacher.teacherEmail = this.emailadmin; 
                this.teacher.teacherDescription = this.mota; 
                BaseRequest.put("teacher", this.teacher)
                .then(response => {
                    console.log(response.data); 
                    alert("Cập thông tin cá nhân thành công!"); 
                })
                .catch(error => {
                    console.log(error.message); 
                })
            }
        }, 
        updatePass: function(){
            const input = document.getElementById("ole-pass-teacher"); 
            const input1 = document.getElementById("pass-new-teacher"); 
            const input2 = document.getElementById("pass-confirm-teacher"); 

            const _delete = document.querySelectorAll(".error-hoso-pass"); 
            this.isCheck = true;
            for (const item of _delete) {
                item.parentNode.removeChild(item);
            }

            input.style.borderColor = "#d0d0d0"; 
            input2.style.borderColor = "#d0d0d0"; 
            input1.style.borderColor = "#d0d0d0"; 
            var isCheck = true; 

            if(this.oldpassword === "" || this.oldpassword === null || this.oldpassword === undefined){
                this.thongbao1(input, "Mật khẩu cũ không được để trống."); 
                isCheck = false; 
            }
            else {
                if(this.oldpassword !== this.teacher.teacherPassword){
                    this.thongbao1(input, "Mật khẩu cũ không chính xác."); 
                    isCheck = false;
                }
            }

            if(this.passnewadmin === "" || this.passnewadmin === null || this.passnewadmin === undefined){
                this.thongbao1(input1, "Mật khẩu mới không được để trống."); 
                isCheck = false; 
            }
            else {
                if(this.passconfirmadmin === "" || this.passconfirmadmin === null || this.passconfirmadmin === undefined){
                    this.thongbao1(input2, "Mật khẩu xác nhận không được để trống."); 
                    isCheck = false; 
                }
                else {
                    if(this.passconfirmadmin !== this.passnewadmin){
                        this.thongbao1(input2, "Mật khẩu xác nhận không chính xác."); 
                        isCheck = false; 
                    }
                }
            }

            if(isCheck){
                this.teacher.teacherPassword = this.passnewadmin; 
                BaseRequest.put("teacher", this.teacher)
                .then(response => {
                    console.log(response.data); 
                    alert("Cập thông tin cá nhân thành công!"); 
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
        thongbao: function(item, mes){
            item.style.borderColor = "red"; 
            let p = document.createElement("p"); 
            let node = document.createTextNode(mes); 
            p.appendChild(node); 
            p.classList.add("error-hoso");
            p.style.color = "red"; 
            p.style.fontStyle = "italic"; 
            p.style.fontSize = "0.9rem"; 
            item.after(p);
            //item.setAttribute("title", mes); 
        },
        thongbao1: function(item, mes){
            item.style.borderColor = "red"; 
            let p = document.createElement("p"); 
            let node = document.createTextNode(mes); 
            p.appendChild(node); 
            p.classList.add("error-hoso-pass");
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
.admin_logo {
    width: 28%; 
}
.admin_logo > textarea {
    height: 250px; 
    width: 100%; 
    border: 1px solid #d0d0d0; 
    outline: none; 
    padding-left: 8px; 
}
.admin_logo > textarea:focus {
    border: 1px solid #50B83C; 
}
.profile_admin {
    font-size: 1.25rem;
}
.teacher_info > div > div {
    width: 100%; 
}
.teacher_info {
    width: 48%; 
    padding-top: 24px; 
    padding-left: 24px; 
}
.teacher_info > div {
    width: 100%; 
    display: flex; 
}
.teacher_info > h3 {
    font-weight: 500;
}
.teacher_infor-form > div > label > input {
    width: 100%; 
    border: 1px solid #d0d0d0; 
    outline: none; 
    padding-left: 8px; 
    margin-bottom: 16px; 
    margin-top: 8px; 
}
.teacher_infor-form > div > label > input:focus {
    border: 1px solid #50B83C; 
}
.teacher_infor-form > div > label > textarea {
    width: 100%; 
    border: 1px solid #d0d0d0; 
    outline: none; 
    padding-left: 8px; 
    margin-top: 8px; 
}
.teacher_infor-form > div > label > textarea:focus {
    border: 1px solid #50B83C; 
}
.button-teacher {
    margin-left: 20%; 
}
</style>

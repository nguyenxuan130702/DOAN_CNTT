<template>
    <div class="user_profile_setting">
    <div class="user_profile_content_set">
      <h1>Cài đặt tài khoản cho {{ user.userAccout }}</h1>
      <form action="" class="replace_accoutname">
        <label for="" class="label_input" id="label_input">
          Tên tài khoản
          <input type="text" name="accout" id="accoutname" v-model="user_accout" @blur="validateDupplicateName">
        </label>
        <div class="delete" v-show="validateaccoutname">Tên tài khoản đã tồn tại</div>
        <div id="fixbug"></div>
        <div class="caidat-taikhoang-user" v-on:click="checkSaveAccout()"> 
          <div class="button_save_content" lesson>Lưu</div>
        </div>
      </form>

      <form class="replace_face_user">
        <label for="" class="label_input" id="label_input">
          Ảnh đại diện
          <img :src="user_image" alt="">
          <input type="file" @change="checkFileUploadAdmin">
        </label>
        <div class="caidat-taikhoang-user" v-on:click="checkSaveImage()"> 
          <button class="button_save_content" lesson>Lưu</button>
        </div>
      </form>

      <form action="" class="replace_accout_user">
        <h1>Thông tin cá nhân</h1>
        <hr>
        <label for="" >
          Họ và tên 
          <input type="text" name="userName" id="username" v-model="user_name">
        </label>
        <div class="delete" v-show="validatename">Họ và tên không được để trống.</div>
        <label for="">
          Email 
          <input type="text" name="userEmail" id="useremail" v-model="user_email">
        </label>
        <div class="delete" v-show="validateaccoutemail" id="email_error">Email đã có tài khoản đăng ký.</div>
        <div class="caidat-taikhoang-user" v-on:click="checkSaveProfile()"> 
          <div class="button_save_content" lesson>Lưu</div>
        </div>
        <h1 style="margin-top: 40px;">Đổi mật khẩu</h1>
        <hr>
        <label for="">
          Mật Khẩu
          <input type="password" name="userpass" id="userpass" v-model="user_pass">
        </label>
        <div class="delete" v-show="validatepass">Mật khẩu không được để trống.</div>
        <label for="">
          Nhập lại mật khẩu
          <input type="password" name="confirmpass" id="confirmpass" v-model="user_confirmpass">
        </label>
        <div class="delete" v-show="validateconfirm" id="confirm_pass">Mật khẩu khẳng định không đúng.</div>

        <div class="caidat-taikhoang-user" v-on:click="checkSavePass()"> 
          <div class="button_save_content" lesson>Lưu</div>
        </div>
      </form>
    </div>
  </div>

  <div class="dialog_thanhcong" id="dialog_thanhcong">
    <div class="dialog_thanhcong_title">
      <div class="icon_thanhcong"></div>
      <p id="dialog_thanhcong_content">{{ capnhat }}</p>
    </div>
    <div class="icon_close" v-on:click="closeDialog()"></div>
  </div>

  <ChatBot></ChatBot>
</template>
<script>
import BaseRequest from '@/core/BaseRequest';
import ChatBot from './mathjax/ChatBox.vue'
export default{
    components: {ChatBot}, 
    data() {
        return {
        user: {},
        accout: [], 
        user_image: "", 
        user_accout: "", 
        user_name: "", 
        user_email: "", 
        user_pass: "", 
        user_confirmpass: "", 
        validatename: false, 
        validatepass: false, 
        validateaccoutname: false, 
        validateaccoutemail: false, 
        validateconfirm: false, 
        capnhat: "Thành công", 
        file: null, 
        };
    },
    mounted() {
        this.checklogin(); 
        console.log(this.user); 
    },
    methods: {
        checkSaveImage: function(){
          if(this.file === null || this.file === undefined || this.file === ""){
            alert("Bạn phải chọn file ảnh thay đổi."); 
          }
          else {
            console.log(this.user); 
            const formData = new FormData(); 
            formData.append("UserId", this.user.userId); 
            formData.append("UserCode", this.user.userCode); 
            formData.append("UserName", this.user.userName); 
            formData.append("UserAccout", this.user.userAccout); 
            formData.append("UserEmail", this.user.userEmail);
            formData.append("UserPassword", this.user.userPassword); 
            formData.append("UserType", this.user.userType); 
            formData.append("Image", this.file); 

            BaseRequest.put("user/UpdateImage", formData)
            .then(response => {
              console.log(response.data); 
              this.capnhat = "Cập nhật thành công.";
              document.getElementById("dialog_thanhcong").style.opacity = "1"; 
            })
            .catch(error => {
              console.log(error.message); 
            })
          }
        }, 
        checkFileUploadAdmin(event){
          this.file = event.target.files[0]; 
          if(this.file){
              //đọc URL của file và gán vào imageUrl để hiển thị 
              const reander = new FileReader(); 
              reander.onload = (e) => {
                  this.user_image = e.target.result; 
              }; 
              reander.readAsDataURL(this.file); 
          }
        },
        checklogin: function () { 
            this.user = JSON.parse(localStorage.getItem("userLogin")); 
            this.user_accout = this.user.userAccout; 
            this.user_name = this.user.userName; 
            this.user_email = this.user.userEmail; 
            this.user_pass = this.user.userPassword; 
            this.user_image = this.user.userImage; 
            BaseRequest.get("viewaccout/list?userName=" + this.user.userAccout)
            .then((response) => {
                this.accout = response.data; 
                console.log(response.data); 
            })
            .catch((error) =>{
                console.log(error);
            });
        },
        checkSaveAccout: function(){
            for (const item of this.accout) {
                if(this.user_accout === item.userAccout){
                    this.validateaccoutname = !this.validateaccoutname; 
                    break; 
                }
                this.validateaccoutname = false; 
            }
            if(!this.validateaccoutname){
              this.user.userAccout = this.user_accout; 
              BaseRequest.put("user", this.user)
              .then(response => {
                this.user = response.data; 
                this.capnhat = "Cập nhật thành công.";
                document.getElementById("dialog_thanhcong").style.opacity = "1"; 
              })
              .catch(error => {
                console.error("Error updating data: ", error); 
                console.log(error.message); 
              })
            }
        },
        closeDialog: function(){
          document.getElementById("dialog_thanhcong").style.opacity = "0"; 
        }, 
        checkSaveProfile: function(){
          //validate email rỗng và email đã tồn tại.
          if(this.user_email === null || this.user_email === undefined || this.user_email === ""){
            document.getElementById("email_error").innerHTML = "Email không được để trống."; 
            this.validateaccoutemail = true; 
          }
          else {
            for (const item of this.accout) {
              if(this.user_email === item.userEmail){
                  document.getElementById("email_error").innerHTML = "Email đã được đăng ký."; 
                  this.validateaccoutemail = !this.validateaccoutemail; 
                  break; 
              }
              this.validateaccoutemail = false; 
            }
          }

          //validate tên để trống.
          if(this.user_name === null || this.user_name === undefined || this.user_name === ""){
            this.validatename = true;
          }
          else {
            this.validatename = false; 
          }

          if(!this.validatename && !this.validateaccoutemail){
            this.user.userName = this.user_name; 
            this.user.userEmail = this.user_email; 
              BaseRequest.put("user", this.user)
              .then(response => {
                this.user = response.data; 
                this.capnhat = "Cập nhật thành công.";
                document.getElementById("dialog_thanhcong").style.opacity = "1"; 
              })
              .catch(error => {
                console.error("Error updating data: ", error); 
                console.log(error.message); 
              })
          }
        }, 
        checkSavePass: function(){
          //validate nhập lại mật khẩu rỗng 
          if(this.user_pass === null || this.user_pass === undefined || this.user_pass === ""){
            this.validatepass = true;
          }
          else {
            this.validatepass = false; 
          }
          //validate mật khẩu xác nhận không đúng và rỗng. 
          if(this.user_confirmpass === null || this.user_confirmpass === undefined || this.user_confirmpass === ""){
            document.getElementById("confirm_pass").innerHTML = "Mật khẩu xác nhận không được để trống."; 
            this.validateconfirm = true; 
          }
          else {
            if(this.user_confirmpass !== this.user_pass){
              document.getElementById("confirm_pass").innerHTML = "Mật khẩu xác nhận không đúng."; 
              this.validateconfirm = true;
            }
            else {
              this.validateconfirm = false;
            } 
          }

          if(!this.validatepass && !this.validateconfirm){
            this.user.userPassword = this.user_pass; 
              BaseRequest.put("user", this.user)
              .then(response => {
                this.user = response.data;  
                this.capnhat = "Cập nhật thành công.";
                document.getElementById("dialog_thanhcong").style.opacity = "1";
              })
              .catch(error => {
                console.error("Error updating data: ", error); 
                console.log(error.message); 
              })
          }
        }
    }
}
</script>
<style scoped>
.label_input > img {
  width: 200px; 
  height: 200px;
  border-radius: 50%;
  margin: auto; 
}
.user_profile_setting {
  padding-top: 150px; 
  background-color: #1b1b32;
  color: #fff; 
  padding-bottom: 120px; 
  font-family: 'Times New Roman', Times, serif;
}
.user_profile_content_set {
    width: 60%;
    margin: auto; 
}
.user_profile_content_set > h1 {
  text-align: center;
}
.user_profile_content_set > h1 {
  font-size: 2.5rem;
  font-family: 'Times New Roman', Times, serif;
  font-weight: 700;
}
.replace_accoutname {
  width: 80%;
  margin: auto; 
}
.replace_accoutname > label {
  display: flex;
  flex-direction: column;
  gap: 16px; 
  font-size: 1.25rem;
  font-weight: 700;
  font-family: 'Times New Roman', Times, serif;
}
.replace_accoutname > label > input {
  outline: none;
  padding-left: 8px; 
  height: 36px;
  width: 100%; 
  background-color: #0a0a23; 
  border: 1px solid #3b3b4f; 
  color: #fff; 
}
.replace_accoutname > label > input:focus {
  border: 1px solid #99c9f7; 
}
.lesson_title {
    height: 60px;
    position: relative; 
    margin: auto; 
    margin-bottom: 10px; 
    display: flex;
    justify-content: flex-start;
    align-items: center;
    width: 100% !important; 
    font-size: 25px; 
    padding-left: 16px; 
    border: 3px solid #fff; 
    font-size: 1.25rem; 
    color: #fff;
}
.lesson_title > div[lesson] {
    z-index: 1;
}

.back-lesson-setting {
    position: absolute; 
    height: 56px;
    width: 100%; 
    background-color: #858591;
    opacity: 0.3;
    top: 0; 
    left: 0;
    right: 0; 
    bottom: 0; 
}
.lesson_title:hover{
    color: #1b1b32; 
    cursor: pointer;
}
.lesson_title:hover .back-lesson-setting{
    background-color: #f5f6f7;
    opacity: 1;
}
.button_save {
  width: 100%; 
  text-align: center;
  margin-top: 20px; 
}
.button_save_content {
  text-align: center;
  width: 100%; 
  font-size: 1.5rem;
  font-weight: 700;
}
.lesson_title:hover{
    color: #1b1b32; 
    cursor: pointer;
}
.lesson_title:hover .button_save_content {
  color: #0a0a23; 
}
/**--------------------------------------------- */
.replace_accout_user {
  width: 80%; 
  margin: auto; 
  margin-top: 40px; 
}
.replace_accout_user > h1{
    font-size: 2.0rem;
    font-family: 'Times New Roman', Times, serif;
    font-weight: 700;
    text-align: center;
}
.replace_accout_user > label {
  display: flex;
  flex-direction: column;
  gap: 16px; 
  font-size: 1.25rem;
  margin-bottom: 24px; 
  font-weight: 700;
  font-family: 'Times New Roman', Times, serif;
}
.replace_accout_user > label > input {
  outline: none;
  padding-left: 8px; 
  height: 36px;
  width: 100%; 
  background-color: #0a0a23; 
  border: 1px solid #3b3b4f; 
  color: #fff; 
}
.replace_accout_user > label > input:focus {
  border: 1px solid #99c9f7; 
}
.delete {
    width: 100%; 
    height: 70px !important;
    color: #a94442 !important;
    background-color: #f2dede !important;
    padding: 20px !important; 
    font-size: 1.25rem; 
    margin-top: 20px; 
    font-family: 'Times New Roman', Times, serif;
    display: flex; 
    align-items: center;
    margin-bottom: 20px; 
}
.icon_thanhcong {
  background: url('@/assets/Sprites.64af8f61.svg') no-repeat -990px -462px;
	width: 36px;
	height: 36px;
}
.icon_close {
  background: url('@/assets/Sprites.64af8f61.svg') no-repeat -147px -147px;
	width: 18px;
	height: 18px;
}
.dialog_thanhcong {
  position: absolute; 
  width: 500px; 
  padding: 24px; 
  background-color: #fff; 
  color: #0a0a23; 
  display: flex; 
  justify-content: space-between;
  align-items: center;
  padding: 24px; 
  box-shadow: -10px -10px 15px rgba(190, 190, 190, 0.1),
    10px 10px 15px rgba(138, 138, 138, 0.1);
  top: 30px; 
  z-index: 2; 
  border-radius: 6px;
  left: 35%; 
  opacity: 0;
  transition: opacity 2s ease; 
  position: fixed;
}
.dialog_thanhcong_title {
  display: flex; 
  gap: 16px; 
  align-items: center;
  font-size: 1.25rem;
  font-weight: 500;
  font-family: 'Lato', sans-serif; 
}
.replace_face_user {
  width: 80%; 
  margin: auto; 
  margin-top: 40px; 
}
.replace_face_user > h1{
    font-size: 2.0rem;
    font-family: 'Times New Roman', Times, serif;
    font-weight: 700;
    text-align: center;
}
.replace_face_user > label {
  display: flex;
  flex-direction: column;
  gap: 16px; 
  font-size: 1.25rem;
  margin-bottom: 24px; 
  font-weight: 700;
  font-family: 'Times New Roman', Times, serif;
}
.label_input > input[type="file"]{
  margin: auto; 
}
.caidat-taikhoang-user {
  margin-top: 16px; 
}
.button_save_content {
  color: #fff; 
}
.caidat-taikhoang-user:hover .button_save_content {
  background-color: #f5f6f7;
  color: #0a0a23;  
}
</style>
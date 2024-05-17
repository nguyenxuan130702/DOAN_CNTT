<template>
   <div class="containers pt-5 pb-5">
    <div class="login">
      <h1>Đăng Kí</h1>
      <h6 class="text-warning" v-if="message">{{ message }}</h6>
      <form @submit.prevent='register()' class="formregister">
          <div class="form-group">
          <input
            id="reg_username"
            placeholder="Họ và tên"
            v-model='user.userName'
          />
        </div>
        <div class="form-group">
          <input
            id="reg_useremail"
            placeholder="Email"
            v-model='user.userEmail'
          />
        </div>
        <div class="form-group">
          <input
            id="reg_accout"
            placeholder="Tên tài khoản"
            v-model='user.userAccout'
          />
        </div>
        <div class="form-group">
          <input
            id="reg_pass"
            type="password"
            placeholder="Mật khẩu"
            v-model='user.userPassword'
          />
        </div>
        <div class="form-group">
          <input
            id="reg_pass2"
            type="password"
            placeholder="Xác minh mật khẩu"
            v-model='password2'
          />
        </div>
        <input type="submit" class="btn btn-primary btn-block" value="Đăng ký" />
      </form>
      <div class="accout_login">
       Bạn đã có tài khoản ?
        <router-link to="/Login">Login</router-link>
      </div>
    </div>
  </div>

  <div class="dialog_dangky" id="dialog_dangky">
    <div class="dialog_dangky_title">
      <div class="icon_thanhcong"></div>
      <p id="dialog_dangky_content">Đăng ký thành công.</p>
    </div>
    <div class="icon_close" v-on:click="closeDialogDk()"></div>
  </div>

  <ChatBot></ChatBot>
</template>
<script>
import BaseRequest from "../core/BaseRequest"
import ChatBot from './mathjax/ChatBox.vue'
export default {
  components: {
    ChatBot
  }, 
  data() {
    return {
      user: {
        userCode: '', 
        userName: '',
        userAccout: '',
        userEmail: '', 
        userPassword: '',
        userImage: 'https://cdn.sforum.vn/sforum/wp-content/uploads/2023/10/avatar-trang-4.jpg', 
        userType: 0
      },
      password2: '',
      accout: [], 
      message:null,
      isCheck: true, 
      index: 0, 
      login: {}, 
    }
  },
  mounted(){
    this.loadDataAccout(); 
  }, 
  methods: {
      /**
       * Hàm kiểm tra email 
       * created by: NTXuan - 22/01/2024. 
       */
      loadDataAccout(){
        BaseRequest.get("user")
        .then(response => {
            this.accout = response.data;
            this.index = this.accout.length + 1;  
            console.log(response.data); 
        })
        .catch(error =>{
            console.log(error);
        });
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
          p.classList.add("delete");
          item.after(p);
          //item.setAttribute("title", mes); 
      }, 
      register: function () {
        const _delete = document.querySelectorAll(".delete"); 
        this.isCheck = true;
        for (const item of _delete) {
            item.parentNode.removeChild(item);
        }
        const input_userEmail = document.getElementById("reg_useremail"); 
        const input_userName = document.getElementById("reg_username");
        const input_userAccout = document.getElementById("reg_accout"); 
        const input_userPass1 = document.getElementById("reg_pass"); 
        const input_userPass2 = document.getElementById("reg_pass2"); 
        //Validate email
        if(this.user.userEmail === "" || this.user.userEmail === undefined || this.user.userEmail === null){
          this.thongbao(input_userEmail, "Email không được để trống."); 
          this.isCheck = false; 
        }
        else {
          if(!this.checkEmail(this.user.userEmail)){
            this.thongbao(input_userEmail, "Email không hợp lệ."); 
            this.isCheck = false;
          }
          else {
            for (const item of this.accout) {
              if(this.user.userEmail === item.userEmail){
                  this.thongbao(input_userEmail, "Email đã được đăng ký.")
                  this.isCheck = false; 
                  break; 
              }
              this.isCheck = true; 
            }
          }
        }

        //Validate họ tên 
        if(this.user.userName === "" || this.user.userName === undefined || this.user.userName === null){
          this.thongbao(input_userName, "Họ và tên không được để trống."); 
          this.isCheck = false; 
        }

        //Validate tên đăng nhập. 
        if(this.user.userAccout === "" || this.user.userAccout === undefined || this.user.userAccout === null){
          this.thongbao(input_userAccout, "Tên tài khoản không được để trống."); 
          this.isCheck = false; 
        }
        else {
          for (const item of this.accout) {
              if(this.user.userAccout === item.userAccout){
                  this.thongbao(input_userAccout, "Tên tài khoản đã có người sử dụng."); 
                  this.isCheck = false;  
                  break; 
              }
              this.isCheck = true; 
            }
        }

        //validate password 
        if(this.user.userPassword === undefined || this.user.userPassword === null || this.user.userPassword === ""){
          this.thongbao(input_userPass1, "Mật khẩu không được để trống."); 
          this.isCheck = false;  
        }
        //validate mật khẩu xác thực
        if(this.password2 === undefined || this.password2 === null || this.password2 === ""){
          this.thongbao(input_userPass2, "Mật khẩu xác thực không được để trống."); 
          this.isCheck = false; 
        }
        else {
          if(this.password2 !== this.user.userPassword){
            this.thongbao(input_userPass2, "Mật khẩu xác thực không trùng khớp."); 
            this.isCheck = false; 
          }
        }
 
        if(this.isCheck){
          console.log(this.user); 
          BaseRequest.post('user',this.user).then(()=>{
            // document.getElementById("dialog_dangky").style.opacity = "1";

            // localStorage.setItem("userLogin", JSON.stringify(this.user));
            this.login.email = this.user.userEmail; 
            this.login.password = this.user.userPassword;

            BaseRequest.post("login", this.login)
            .then((response) => {
              localStorage.setItem("userLogin", JSON.stringify(response.data));
              this.$router.push({ name: "setting" });
              document.getElementById("login_accout").style.display = "block";
              document.getElementById("singout_accout").style.display = "none";
            })
            .catch((error) => {
              this.message = error.response.data.message;
              if(error.response.data.errors){
                this.error = error.response.data.errors;
              }
            }); 
          }).catch(error=>{
            console.log(error); 
          })
        }
     }
  }
}
</script>
<style>

* {
  padding: 0;
  margin: 0;
}
 body {
  font-family: "Roboto", sans-serif;
  /* background: linear-gradient(45deg, rgb(116, 176, 255), rgb(255, 126, 223)); */
  /* height: 150vh; */
  align-items: center;
  justify-content: center;
  background-size: cover;
  animation: bg 2s linear infinite alternate;
}
@keyframes bg {
  from {
    background-position: left;
  }
  to {
    background-position: right;
  }
}
.containers {
  height: calc(100vh + 50px);
  background-color: #d0d0d5;
  padding-bottom: 40px; 
}
.login {
  width: 450px; 
  border: 5px solid #0a0a23; 
  margin: auto; 
  text-align: center;
  margin-top: 60px; 
  background-color: #fff; 
  padding-top: 20px; 
}
.formregister {
  width: 80%; 
  margin: auto; 
  text-align: center;
}
.form-group > input {
  margin-bottom: 20px; 
  width: 350px !important;
  height: 36px !important;
  border: none !important;
  border-radius: 4px;
  outline: none !important;
  margin-left: 5px !important;
  font-size: 18px !important;
  padding: 0px 10px !important;
  border: 1px solid #EBEBEB !important;
}
.form-group > input:focus {
  border:1px solid #50B83C !important;
}
input[type="submit"] {
  margin-top: 20px !important;
  width: 100% !important;
  height: 50px !important;
  font-size: 20px !important;
  font-weight: 100px !important;
  color: #111 !important;
  background-color: #f1be32 !important;
  box-shadow: -10px -10px 15px rgba(0, 0, 0, 0.1),
    10px 10px 15px rgba(0, 0, 0, 0.1);
    border: none; 
  margin-bottom: 20px; 
}
.accout_login {
  padding-bottom: 20px; 
}
.invalid-feedback {
  text-align: left;
  margin-top: -20px; 
  margin-bottom: 20px; 
  font-style:italic;
  margin-left: 5px; 
}
.form-group > p {
  color: red; 
  text-align: left;
  font-style: italic;
  margin-left: 5px; 
  margin-top: -20px;
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
.dialog_dangky {
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
.dialog_dangky_title {
  display: flex; 
  gap: 16px; 
  align-items: center;
  font-size: 1.25rem;
  font-weight: 500;
  font-family: 'Lato', sans-serif; 
}
</style>
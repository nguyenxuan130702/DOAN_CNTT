<template>
  <div class="containers pt-5 pb-5">
    <div class="login">
      <h1>Đăng Nhập</h1>
       <h6 class="text-warning" v-if="message">{{ message }}</h6>
      <form @submit.prevent="login()">
        <div class="form-group">
          <input
            v-model="user.email"
            class="input_type"
            :class="{ 'is-invalid': error.email }"
            placeholder="Email"
          />
          <div v-if="error.email" class="invalid-feedback center">
            {{ error.email[0]}}
          </div>
        </div>
        <div class="form-group">
          <input
            v-model="user.password"
            type="password"
            class="input_type"
            :class="{ 'is-invalid': error.password }"
            placeholder="Password"
          />
          <div v-if="error.password" class="invalid-feedback center">
            {{ error.password[0] }}
          </div>
        </div>
        <input type="submit" class="btn btn-primary btn-block" value="Đăng nhập" />
      </form>
      <p>khôi phục mật khẩu?</p>
      <div class="register_type">
        Bạn chưa có tài khoản
       <router-link to="/Register">Đăng kí</router-link>
      </div>
    </div>
  </div>
  <ChatBot></ChatBot>
</template>
<script>
import BaseRequest from "../core/BaseRequest"
import ChatBot from './mathjax/ChatBox.vue'
export default {
  components: {
    ChatBot, 
  }, 
  data() {
    return {
      user: {
        email:null,
        password: null,
      },
      error: {},
      message :null,
    };
  },
  methods: {
    login: function () {
       this.error=[];
       this.message =null;
        BaseRequest.post("login", this.user)
        .then((response) => {
          localStorage.setItem("userLogin", JSON.stringify(response.data));
          if(response.data.userType === 0){
            this.$router.push({ name: "start" });
            document.getElementById("login_accout").style.display = "block";
            document.getElementById("singout_accout").style.display = "none"; 
          }
          else {
            this.$router.push({ name: "overview"}); 
            document.getElementById("mainNav").style.display = "none"; 
            document.getElementById("fh5co-footer").style.display = "none"; 
          }
        })
        .catch((error) => {
          this.message = error.response.data.message;
          if(error.response.data.errors){
            this.error = error.response.data.errors;
          }
          
        });
    },
  },
};
</script>
<style scoped>
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
  width: 100%;
  height: 100%;
  background-repeat: no-repeat;
  background-size: cover;
  background-attachment: fixed;
  display: flex;
  justify-content: center;
  align-items: center;
  background-color: #d0d0d5;
}
.login {
    width: 450px; 
    margin: auto; 
    border: 3px solid #0a0a23 !important;
    padding: 5px !important;
    background-size: cover !important;
    background-attachment: fixed !important;
    display: flex !important;
    align-items: center !important;
    flex-direction: column !important;
    background-color: #fff !important;
    margin-top: 120px; 
    margin-bottom: 100px; 
}
.login > h1 {
  padding: 50px 0px 20px 0px;
}
form {
  display: flex !important;
  justify-content: center !important;
  align-items: center !important;
  flex-direction: column !important;
}
.rounded-pill {
  margin-top: 20px;
  width: 300px !important;
  height: 40px !important;
  border: none !important;
  outline: none !important;
  margin-left: 5px !important;
  font-size: 18px !important;
  padding: 0px 10px !important;
  border: 1px solid #111 !important;
}
input[type="submit"] {
  margin-top: 20px !important;
  width: 350px !important;
  height: 50px !important;
  font-size: 20px !important;
  font-weight: 100px !important;
  color: #111 !important;
  background-color: #f1be32 !important;
  box-shadow: -10px -10px 15px rgba(0, 0, 0, 0.1),
    10px 10px 15px rgba(0, 0, 0, 0.1);
    border: none; 
}
p {
  color: #111;
  padding: 15px 0px;
  letter-spacing: 1px;
}
span {
  padding: 18px 0px 0px 0px;
  color: rgb(7, 7, 7);
  font-size: 20px;
}
.input_type {
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
.input_type:focus {
    border:1px solid #50B83C !important; 
}
.register_type {
    padding-bottom: 50px; 
}
</style>

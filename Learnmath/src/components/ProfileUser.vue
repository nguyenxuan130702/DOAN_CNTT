<template>
  <div class="user_login">
    <!-- Khi đăng nhập thành công sẽ vào trang profile đầu tiên. Trong 
    profile có các thông tin gồm: 
    Dòng chào mừng, tên tài khoản,  -->
    <div class="profile_user">
      <h1>Chúc mừng bạn đăng nhập thành công.</h1>
      <div class="profile_user_box">Bạn có thể thay đổi thông tin tên đăng nhập và mật khẩu của mình.</div>
      <div class="profile_user_image">
        <img :src="user.userImage" alt="">
      </div>
      <h1>{{ user.userAccout }}</h1>
    </div>
  </div>
  <ChatBot></ChatBot>
</template>
<script>
//import BaseRequest from "../core/BaseRequest";
import ChatBot from './mathjax/ChatBox.vue'
export default {
  components: {
    ChatBot, 
  }, 
  data() {
    return {
      user: {},
    };
  },
  mounted() {
    this.checklogin();
  },
  methods: {
    checklogin: function () {
      this.user = JSON.parse(localStorage.getItem("userLogin")); 
    }, 
    
  },
};
</script>
<style>
.user_login {
  background-color: #1b1b32; 
  padding-top: 150px; 
  color: #fff; 
  font-family:'Times New Roman', Times, serif; 
}
.profile_user {
  width: 60%; 
  margin: auto; 
  text-align: center;
  height: calc(100vh - 150px);
}
.profile_user > h1 {
  font-size: 2.5rem;
  font-weight: 700;
}
.profile_user_box {
  background-color: #d9edf7;
  border-color: #bce8f1; 
  color: #31708f; 
  padding: 20px; 
  width: 60%; 
  font-size: 1.25rem; 
  margin: auto; 
}
.profile_user_image {
  width: 180px; 
  height: 180px;
  border: 10px solid #d0d0d5; 
    margin: auto; 
  margin-top: 40px; 
  margin-bottom: 40px; 
}
.profile_user_image > img {
  width: 100%; 
  height: 100%;
}

/**--------------------------------------------------- */

</style>
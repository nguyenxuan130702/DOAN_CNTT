<template>
  <div>
  <nav class="navbar navbar-expand-lg navbar-light " id="mainNav">
     <div class="container">
        <router-link class="nav-link js-scroll-trigger logo" to="/">Learnmath</router-link>
        <button
          class="navbar-toggler navbar-toggler-right"
          type="button"
          data-toggle="collapse"
          data-target="#navbarResponsive"
          aria-controls="navbarResponsive"
          aria-expanded="false"
          aria-label="Toggle navigation"
        >
          Menu
          <i class="fas fa-bars ml-1"></i>
        </button>
        <div class="collapse navbar-collapse" id="navbarResponsive">
          <ul class="navbar-nav text-uppercase">
            <li class="nav-item">
              <router-link class="nav-link js-scroll-trigger hover-nav" to="/"
                ><div class="icon_forum"></div> Trang chủ</router-link
              >
            </li>
            <li class="nav-item">
              <router-link class="nav-link js-scroll-trigger hover-nav" to="/Forum"
                ><div class="icon_forum"></div> Diễn đàn</router-link
              >
            </li>
            <li class="nav-item">
              <router-link class="nav-link js-scroll-trigger hover-nav" to="/News"
                ><div class="icon_new"></div> Tin tức</router-link
              >
            </li>
            <li class="nav-item">
              <router-link class="nav-link js-scroll-trigger hover-nav" to="/Radio" target="_blank"
                ><div class="icon_radio"></div> Radio</router-link
              >
            </li>
            <li class="nav-item">
              <router-link class="nav-link js-scroll-trigger hover-nav" to="/Course"
                ><div class="icon_course"></div> Khóa học</router-link>
            </li>
            <li class="nav-item login_accout" id="login_accout">
              <router-link class="nav-link js-scroll-trigger hover-nav" to="/Profile/"
                ><div class="icon_accout"></div>
                Tài khoản
              </router-link>
              <div class="nav-item-menu">
                <router-link to="/Profile/" class="nav-item-menu-nav">Thông tin tài khoản</router-link>
                <router-link to="/SettingProfile" class="nav-item-menu-nav">Cài đặt tài khoản</router-link>
                <router-link to="/" class="nav-item-menu-nav" v-on:click="logout()">Đăng xuất</router-link>
              </div>
            </li>
            <li class="nav-item">
              <router-link class="nav-link js-scroll-trigger hover-nav" id="singout_accout" to="/Login"
                ><i class="fas fa-user mr-2"></i> Đăng nhập</router-link
              >
            </li>
          </ul>
        </div>
      </div>
    </nav>
  </div>
</template>
<script>
export default{
  data(){
    return{
      user: {}, 
      showUser: false, 
      showLogin: true,  
    }
  }, 
  mounted(){
    this.user = JSON.parse(localStorage.getItem("userLogin")); 
    //this.user.userImage = ""; 
    if(this.user !== null){
      this.user = JSON.parse(localStorage.getItem("userLogin"));
      document.getElementById("singout_accout").style.display = "none"; 
      document.getElementById("login_accout").style.display = "block";
    }
    else {
       document.getElementById("singout_accout").style.display = "block"; 
      document.getElementById("login_accout").style.display = "none";
    }
    console.log(this.user); 
  }, 
  watch: {
    
  }, 
  methods: {
    logout: function(){
      localStorage.removeItem("userLogin");  
      document.getElementById("singout_accout").style.display = "block"; 
      document.getElementById("login_accout").style.display = "none"; 

    }, 
    check: function(){
      if(this.user === null || this.user === undefined || this.user === ""){
        this.showUser = false; 
        this.showLogin = true; 
      }
      else {
        this.showUser = true; 
        this.showLogin = false; 
      }
    }, 
    // openForum: function(){
    //   localStorage.removeItem("userLogin"); 
    // }
  }
}
</script>
<style scoped>
.logo{
  font-size: 1.5rem;
}
.js-scroll-trigger > img {
  height: 50px;
  width: 50px; 
  border-radius: 50%;
}
.nav-link {
  display: flex; 
  flex-direction: row;
  align-items: center;
  text-decoration: none;
  color: #fff; 
}
.hover-nav:hover {
  text-decoration: underline;
}
#mainNav {
  background-color: #0a0a23!important;
  position: fixed; 
  top: 0; 
  left: 0; 
  right: 0; 
  z-index: 4; 
}
.navbar-nav{
  /* margin-left: 30% !important; */
  float: right;
  margin-left:auto;
  align-items: center;
}
.nav-link {
  color: #fff !important; 
}
.nav-item-menu {
  width: 220px; 
  background-color: #0a0a23;
  position: absolute; 
  display: none;  
  right: 0px; 
}
.login_accout {
  position: relative;
  
}
.nav-item-menu-nav {
  height: 50px;
  width: 100%;
  display: flex; 
  align-items: center;
  justify-content: center;
  color: #fff; 
  text-decoration: none;
}
.login_accout:hover .nav-item-menu{
  display: block; 
}

.nav-item-menu-nav:hover {
  background-color: #d0d0d5;
  color:#0a0a23; 
} 
</style>

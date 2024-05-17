<template>
    <div class="a-nav">
        <div class="a-nav-logo">
            <div class="icon_menu"></div>
            <div class="a-nav-name">Learnmath</div>
        </div>
        <div class="a-nav-user">
            <img :src="userLogin.userImage" alt="">
            <p>{{ userLogin.userName }}</p>
            <router-link class="a-nav-logout" to="/" v-on:click="logoutAdmin()">
                <div>Đăng xuất</div>
                <div class="icon_logout"></div>
            </router-link>
        </div>
    </div>
</template>
<script>
export default{
    data(){
        return {
            userLogin: {}, 
        }
    }, 
    mounted(){
        this.userLogin = JSON.parse(localStorage.getItem("userLogin")); 
    }, 
    methods: {
        logoutAdmin: function(){
            localStorage.removeItem("userLogin");
            document.getElementById("mainNav").style.display = "block";
            document.getElementById('fh5co-footer').style.display = "block"; 
        }
    }
}
</script>
<style scoped>
.icon_logout {
    background: url('@/assets/admin/logout-svgrepo-com.svg') no-repeat -5px -5px;
	width: 54px;
	height: 54px;
    transform: scale(0.4);
}
.a-nav-logout {
    position: absolute;
    height: 56px;
    width: 100%; 
    top: 56px; 
    bottom: 0; 
    z-index: 2;
    color: #fff; 
    display: flex; 
    justify-content: space-around;
    border-top: 3px solid #fff; 
    align-items: center;
    background-color: #0a0a23;
    transition: background-color 0.5s ease; 
    visibility: hidden;  
}
.a-nav-user:hover .a-nav-logout {
     visibility: visible; 
}
.a-nav-logout:hover {
    background-color: #d0d0d0; 
    color: black; 
    cursor: pointer;
}
.a-nav {
    width: 100%;
    height: 56px;
    background-color: #0a0a23; 
    float: left;
}
.a-nav-logo {
    width: 200px; 
    height: 100%;
    margin-left: 24px; 
}
.icon_menu {
    background: url('@/assets/admin/menu-svgrepo-com.svg') no-repeat 0 0;
	width: 64px;
	height: 64px;
    transform: scale(0.3);
}
.a-nav-logo {
    display: flex;
    align-items: center; 
}
.a-nav-name {
    font-size: 1.75rem;
    color: #fff; 
}
.a-nav-user {
    display: flex; 
    height: 100%;
    align-items: center;
    gap: 16px; 
    color: #fff; 
    font-size: 1.1rem; 
    padding-right: 36px; 
    position: relative; 
}
.a-nav-user > img{
    height: 40px;
    width: 40px;
    border-radius: 50%;
}
.a-nav {
    display: flex; 
    justify-content: space-between;
}

</style>

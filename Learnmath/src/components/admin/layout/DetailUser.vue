<template>
    <div class="detail-user" id="detail-user">
        <div class="detail-user-form">
            <div class="detail-user-title">
                <div></div>
                <h1>Thông tin chi tiết tài khoản</h1>
                <div class="icon_close fix-close" v-on:click="closeDetailUser()"></div>
            </div>
            <div class="detail-user-infor">
                <div class="detail-user-image">
                    <img :src="imageUrl" alt="">
                </div>
                <div class="detail-user-content">
                    <div><b>Mã người dùng: </b> {{ userCode }}</div>
                    <div><b>Họ và tên: </b> {{ userName }}</div>
                    <div><b>Tên người dùng: </b> {{ userAccout }}</div>
                    <div><b>Email: </b> {{ userEmail }}</div>
                    <div><b>Mật khẩu: </b> **********</div>
                    <div><b>Vai trò: </b>{{ renderType(userType)}}</div>
                </div>
            </div>
        </div>
    </div>
</template>
<script>
export default{
    props: ['propName'], 
    data(){
        return {
            userCode: '', 
            userName: '', 
            userAccout: '',
            userEmail: '', 
            userPassword: '', 
            userType: 0, 
            imageUrl: '', 
        }
    }, 
    watch: {
        propName: function(){
            this.userCode = this.propName.userCode; 
            this.userName = this.propName.userName; 
            this.userAccout = this.propName.userAccout; 
            this.userEmail = this.propName.userEmail;
            this.userPassword = this.propName.userPassword;
            if(this.propName.userImage !== null){ 
                this.imageUrl = this.propName.userImage;
            } 
            else {
                this.imageUrl = "https://images.fbox.fpt.vn/wordpress-blog/2023/11/ponyp-va-sosuke.jpg"
            }
            this.userType = this.propName.userType; 
        }
    }, 
    methods: {
        closeDetailUser: function(){
            document.getElementById("detail-user").style.display = "none"; 
            document.getElementById("user_back").style.display = "none";
        }, 
        renderType: function(item){
            if(item === 0){
                return "Người dùng"; 
            }
            else {
                return "Người quản trị";
            }
        }
    }
}
</script>
<style scoped>
.detail-user {
    display: none; 
    width: 700px; 
    height: calc(100vh - 50vh);
    position: absolute; 
    background-color: #fff; 
    left: 25%; 
    top: 150px; 
    box-shadow: rgba(0,0,0,0.078) 0px 3px 6px 0px;
    z-index: 3;
}
.detail-user-title {
    display: flex;
    justify-content: space-around;
    align-items: center; 
    margin-bottom: 24px; 
}
.detail-user-infor {
    width: 80%; 
    display: flex;
    margin: auto;
}
.detail-user-image {
    width: 250px; 
}
.detail-user-image > img {
    width: 200px; 
    height: 200px;
    border-radius: 50%;
    margin-top: 16px; 
}
.detail-user-content > div {
    margin-bottom: 16px; 
}
.detail-user-form {
    width: 100%;  
    margin-top: 24px; 
    padding-right: 24px; 
}
</style>

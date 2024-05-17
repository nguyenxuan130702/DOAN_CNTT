<template>
    <Navbar></Navbar>
    <Sidebar></Sidebar>
    <div class="menu-course">
        <h1>Quản lý câu hỏi</h1>
        <div class="menu-course-nav">Trang quản trị/Quản lý câu hỏi</div>
        <div>
            <div class="menu-course-home">
                <div>Câu hỏi ôn tập trong video</div>
                <div><router-link to="/QuestionVideo">Xem chi tiết</router-link></div>
            </div>
            <div class="menu-course-home">
                <div>Câu hỏi luyện tập</div>
                <div>
                    <router-link to="/QuestionAdmin">Xem chi tiết</router-link>
                </div>
            </div>
        </div>
    </div>
</template>
<script>
import Navbar from './layout/NavbarAdmin.vue'
import Sidebar from './layout/SidebarAdmin.vue'
export default{
    components: {
        Navbar, 
        Sidebar
    }
}
</script>
<style scoped>
.menu-course {
    width: calc(100% - 250px); 
    height: 100vh;
    float: left;
    background-color: #EEEFF1; 
}
.menu-course > h1 {
    padding-top: 24px; 
    padding-left: 24px;
}
.menu-course-nav{
    margin-left: 24px; 
    margin-right: 24px; 
    padding-left: 24px; 
    background-color: #fff;
    border: 3px solid #0a0a23; 
    font-size: 1.25rem; 
}
.menu-course-home {
    width: 30%; 
    border: 3px solid #0a0a23;  
    background-color: #fff;
    font-size: 1.25rem; 
}
.menu-course-home > div:last-child {
    left: 65%; 
    padding-bottom: 24px;
}
.menu-course-home > div:first-child {
    left: 10%;
    padding-top: 24px; 
    padding-bottom: 50px 
}
.menu-course > div:last-child {
    display: flex;
    gap: 24px; 
    margin-left: 24px; 
    margin-top: 24px; 
}
</style>

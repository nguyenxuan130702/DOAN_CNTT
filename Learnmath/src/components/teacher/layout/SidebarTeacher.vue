<template>
    <div class="a-slidebar">
        <div class="a-slidebar-content">
            <router-link class="a-slidebar-content-x" to="/HomeTeacher" :class="{ 'active-link': isActive('/HomeTeacher') }">
                <div class="icon_tongquan"></div>
                <div>Tổng quan </div>
            </router-link>
            <router-link class="a-slidebar-content-x" to="/ProfileTeacher" :class="{ 'active-link': isActive('/ProfileTeacher') }">
                <div class="icon_hoso"></div>
                <div>Hồ sơ</div>
            </router-link>
            <router-link class="a-slidebar-content-x" to="/CourseTeacher" :class="{ 'active-link': isActive('/CourseTeacher') }">
                <div class="icon_qluser"></div>
                <div>Quản lý lớp học.</div>
            </router-link>
            <router-link class="a-slidebar-content-x" to="/DocumentTeacher" :class="{ 'active-link': isActive('/DocumentTeacher') }">
                <div class="icon_qlkhoahoc"></div>
                <div>Quản lý tài liệu</div>
            </router-link>
            <router-link class="a-slidebar-content-x" to="/DanhGia" :class="{ 'active-link': isActive('/DanhGia') }">
                <div class="icon_qlbaihoc"></div>
                <div>Đánh giá và theo dõi tiến độ</div>
            </router-link>
        </div>
    </div>
</template>
<script>
export default{
    methods: {
        isActive(route){
            return this.$route.path === route; 
        }
    }
}
</script>
<style scoped>
.a-slidebar {
    width: 250px; 
    height: calc(100vh - 56px);
    background-color: #0a0a23;
    padding-top: 16px; 
    float: left;
    overflow-y: auto;
}
.a-slidebar-content {
    width: 100%; 
    margin: auto; 
}
.a-slidebar-content-x {
    width: 100%;
    height: 50px;
    background-color:#0a0a23;
    opacity: 0.5;
    border-radius: 6px;
    display: flex; 
    margin-top: 16px; 
    align-items: center;
    transform: scale(0.9);
}
.a-slidebar-content-x:hover {
    background-color:#1b1b32; 
    opacity: 1;
    cursor: pointer;
}
.a-slidebar-content-x:last-child {
    margin-bottom: 40px; 
}
.icon_tongquan {
    background: url('@/assets/admin/home-1-svgrepo-com.svg') no-repeat -3px -6px;
	width: 70px;
	height: 55px;
    transform: scale(0.5);
}
.icon_hoso {
    background: url('@/assets/admin/file-user-svgrepo-com.svg') no-repeat -11px -6px;
	width: 70px;
	height: 55px;
    transform: scale(0.5);
}
.icon_qluser {
    background: url('@/assets/admin/user-svgrepo-com (1).svg') no-repeat -8px -3px;
	width: 70px;
	height: 58px;
    transform: scale(0.5);
}
.icon_qlkhoahoc {
    background: url('@/assets/admin/course-tile-sort-svgrepo-com.svg') no-repeat 0 0;
	width: 70px;
	height: 64px;
    transform: scale(0.4);
}
.icon_qlcauhoi {
    background: url('@/assets/admin/question-circle-svgrepo-com.svg') no-repeat -3px -3px;
	width: 70px;
	height: 58px;
    transform: scale(0.4);
}
.icon_qlbantin {
    background: url('@/assets/admin/news-svgrepo-com (1).svg') no-repeat -6px -6px;
	width: 70px;
	height: 52px;
    transform: scale(0.4);
}
.icon_qlbinhluan {
    background: url('@/assets/admin/comment-3-svgrepo-com.svg') no-repeat 0 0;
	width: 84px;
	height: 64px;
    transform: scale(0.4);
    margin-left: -5px; 
}
.icon_qlchude {
    background: url('@/assets/admin/forum-svgrepo-com.svg') no-repeat -5px -10px;
	width: 75px;
	height: 40px;
    transform: scale(0.5);
    margin-left: -5px; 
}
.icon_qlbaihoc {
    background: url('@/assets/admin/history-alt-svgrepo-com.svg') no-repeat -5px -4px;
	width: 70px;
	height: 56px;
    transform: scale(0.4);
}
.a-slidebar-content-x {
    font-size: 1.1rem; 
    color: #fff; 
    text-decoration: none;
}
.active-link {
    background-color:#1b1b32; 
    opacity: 1;
}
.icon_qlbaidang {
    background: url('@/assets/admin/post-sign-message-svgrepo-com.svg') no-repeat -4px -4px;
	width: 70px;
	height: 56px;
    transform: scale(0.4);
}
.icon_qlvideo {
    background: url('@/assets/admin/video-svgrepo-com.svg') no-repeat -5px -13px;
	width: 74px;
	height: 38px;
    transform: scale(0.5);
    margin-left: -5px; 
}
.icon_qlscore {
    background: url('@/assets/math/score-svgrepo-com.svg') no-repeat -2px -2px;
	width: 74px;
	height: 60px;
    transform: scale(0.5);
    margin-left: -5px; 
}
</style>

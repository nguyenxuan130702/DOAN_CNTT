<template>
    <div class="a-slidebar">
        <div class="a-slidebar-content">
            <router-link class="a-slidebar-content-x" to="/Overview" :class="{ 'active-link': isActive('/Overview') }">
                <div class="icon_tongquan"></div>
                <div>Tổng quan </div>
            </router-link>
            <router-link class="a-slidebar-content-x" to="/ProfileAdmin" :class="{ 'active-link': isActive('/ProfileAdmin') }">
                <div class="icon_hoso"></div>
                <div>Hồ sơ</div>
            </router-link>
            <router-link class="a-slidebar-content-x" to="/Useradmin" :class="{ 'active-link': isActive('/Useradmin') }">
                <div class="icon_qluser"></div>
                <div>Quản lý tài khoản.</div>
            </router-link>
            <router-link class="a-slidebar-content-x" to="/GiaoVien" :class="{ 'active-link': isActive('/GiaoVien') }">
                <div class="icon_qluser"></div>
                <div>Quản lý tài khoản giáo viên</div>
            </router-link>
            <router-link class="a-slidebar-content-x" to="/MenuCourse" :class="{ 'active-link': isActive('/MenuCourse') }">
                <div class="icon_qlkhoahoc"></div>
                <div>Quản lý khóa học</div>
            </router-link>
            <router-link class="a-slidebar-content-x" to="/LessonAdmin" :class="{ 'active-link': isActive('/LessonAdmin') }">
                <div class="icon_qlbaihoc"></div>
                <div>Quản lý bài học</div>
            </router-link>
            <router-link class="a-slidebar-content-x" to="/MenuQuestion" :class="{ 'active-link': isActive('/MenuQuestion') }">
                <div class="icon_qlcauhoi"></div>
                <div>Quản lý câu hỏi</div>
            </router-link>
            <router-link class="a-slidebar-content-x" to="/NewAdmin" :class="{ 'active-link': isActive('/NewAdmin') }">
                <div class="icon_qlbantin"></div>
                <div>Quản lý bản tin</div>
            </router-link>
            <router-link class="a-slidebar-content-x" to="/CategoryAdmin" :class="{ 'active-link': isActive('/CategoryAdmin') }">
                <div class="icon_qlbinhluan"></div>
                <div>Quản lý chủ đề bàn luận</div>
            </router-link>
            <router-link class="a-slidebar-content-x" to="/ThreadAdmin" :class="{ 'active-link': isActive('/ThreadAdmin') }">
                <div class="icon_qlbaidang"></div>
                <div>Quản lý các bài đăng</div>
            </router-link>
            <router-link class="a-slidebar-content-x" to="/CommentAdmin" :class="{ 'active-link': isActive('/CommentAdmin') }">
                <div class="icon_qlchude"></div>
                <div>Quản lý các bình luận</div>
            </router-link>
            <router-link class="a-slidebar-content-x" to="/VideoAdmin" :class="{ 'active-link': isActive('/VideoAdmin') }">
                <div class="icon_qlvideo"></div>
                <div>Quản lý video bài học</div>
            </router-link>
            <router-link class="a-slidebar-content-x" to="/ScoreAdmin" :class="{ 'active-link': isActive('/ScoreAdmin') }">
                <div class="icon_qlscore"></div>
                <div>Quản lý kết quả</div>
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

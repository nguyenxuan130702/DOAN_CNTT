<template>
    <div class="a-home">
        <h1>Chào mừng bạn đã tới với trang dành cho quản trị viên.</h1>
        <div class="a-home-title">
            <p>Bảng tổng quan /</p>
            <p>Quản lý</p>
        </div>
        <div class="a-home-select">
            <div class="a-home-select1 course">
                <div>Khóa học</div>
                <p>
                    <router-link to="/MenuCourse">Xem chi tiết</router-link>
                </p>
                <div class="a-home-select1-back"></div>
            </div>
            <div class="a-home-select1 lesson-home-admin">
                <div>Bài học</div>
                <p>
                    <router-link to="/LessonAdmin">Xem chi tiết</router-link>
                </p>
                <div class="a-home-select1-back"></div>
            </div>
            <div class="a-home-select1 question">
                <div>Câu hỏi</div>
                <p>
                    <router-link to="/MenuQuestion">Xem chi tiết</router-link>
                </p>
                <div class="a-home-select1-back"></div>
            </div>
            <div class="a-home-select1 newsimg">
                <div>Bản tin</div>
                <p>
                    <router-link to="/NewAdmin">Xem chi tiết</router-link>
                </p>
                <div class="a-home-select1-back"></div>
            </div>
            <div class="a-home-select1 comment">
                <div>Chủ đề bình luận</div>
                <p>
                    <router-link to="/CategoryAdmin">Xem chi tiết</router-link>
                </p>
                <div class="a-home-select1-back"></div>
            </div>
            <div class="a-home-select1 forumimg">
                <div>Bài đăng</div>
                <p>
                    <router-link to="/ThreadAdmin">Xem chi tiết</router-link>
                </p>
                <div class="a-home-select1-back"></div>
            </div>
            <div class="a-home-select1 forumimg">
                <div>Bình luận</div>
                <p>
                    <router-link to="/CommentAdmin">Xem chi tiết</router-link>
                </p>
                <div class="a-home-select1-back"></div>
            </div>
            <div class="a-home-select1 forumimg">
                <div>Video khóa học</div>
                <p>
                    <router-link to="/VideoAdmin">Xem chi tiết</router-link>
                </p>
                <div class="a-home-select1-back"></div>
            </div>
            <div class="a-home-select1 forumimg">
                <div>Kết quả</div>
                <p>
                    <router-link to="/VideoAdmin">Xem chi tiết</router-link>
                </p>
                <div class="a-home-select1-back"></div>
            </div>
        </div>
    </div>
</template>
<script>
export default{

}
</script>
<style scoped>
.a-home {
    width: calc(100% - 250px); 
    float: left;
}
.a-home > h1 {
    font-size: 2.5rem;
    font-weight: 700;
    margin-top: 24px; 
    margin-left: 24px; 
}
.a-home-title {
    width: calc(100% - 24px - 24px); 
    background-color:aquamarine; 
    display: flex;
    margin: auto; 
    align-items: center;
    padding-left: 16px; 
    padding-top: 16px; 
}
.a-home-title > p {
    font-size: 1.25rem;
}
.a-home-title > p:first-child{
    color: blue; 
}
.a-home-select1 {
    width: 30%; 
    height: 150px;
    border: 3px solid #0a0a23; 
    position: relative; 
    color: #0a0a23; 
}

.a-home-select {
    width: calc(100% - 24px - 24px); 
    display: flex; 
    flex-wrap: wrap;
    justify-content: space-between;
    gap:24px; 
    margin: auto; 
    margin-top: 16px; 
}
.a-home-select1 > div {
    position: absolute; 
    top: 24px; 
    left: 24px;  
    z-index: 1; 
    font-size: 1.25rem; 
    font-weight: 700;
}
.a-home-select1 > p{
    position: absolute; 
    bottom: 24px; 
    right: 24px;
    z-index: 1; 
}
.a-home-select1-back {
    position: absolute;  
    height: 100%;
    width: 100%;  
    margin-top: -24px; 
    margin-left: -24px; 
    background-color: #0a0a23;
    opacity: 0.3;
}
</style>

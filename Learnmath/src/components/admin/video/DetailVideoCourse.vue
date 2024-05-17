<template>
    <div class="detail-course" id="detail-video-course">
        <div class="detail-course-form">
            <div class="detail-course-form-title">
                <div></div>
                <h1>Thông tin chi tiết khóa học video</h1>
                <div class="icon_close fix-close" v-on:click="closeDetailCourse()"></div>
            </div>
            <div class="detail-course-content">
                <div class="detail-course-content-title">
                    <div>
                        <img :src="img" alt="">
                    </div>
                    <div>
                        <div class="detail-course-title">
                            <div><b>Mã khóa học:</b> {{ makh }}</div>
                            <div><b>Tên khóa học:</b> {{ tenkh }}</div>
                            <div><b>Ngày tạo:</b> {{ renderDate(ngay_tao)}}</div>
                            
                        </div>
                    </div>
                </div>
                <div class="detail-course-content">
                    <b>Nội dung mô tả</b>: {{ content }}
                </div>
            </div>
        </div>
    </div>
</template>
<script>
export default{
    props: ['propGetVideo_Course'], 
    data(){
        return {
            makh: "", 
            tenkh: "", 
            ngay_tao: "", 
            content: "", 
            img: "", 
        }
    }, 
    watch: {
        propGetVideo_Course: function(){
            this.makh = this.propGetVideo_Course.video_CourseCode; 
            this.tenkh = this.propGetVideo_Course.video_CourseTitle; 
            this.ngay_tao = this.propGetVideo_Course.created_at;
            this.content = this.propGetVideo_Course.video_CourseDescription; 
            this.img = this.propGetVideo_Course.video_CourseImage; 
        }
    }, 
    methods: {
        closeDetailCourse: function(){
            document.getElementById("detail-video-course").style.display = "none"; 
            document.getElementById("video-course_back").style.display = "none"; 
        }, 
        renderDate: function(date1){
            const date = new Date(date1);
            const day = date.getDay(); 
            const month = date.getMonth(); 
            const year = date.getFullYear()
            return day + '/' + month + '/' + year; 
        },
    }
}
</script>
<style scoped>
.detail-course {
    width: 60%; 
    position: absolute;
    background-color: #fff;
    padding: 24px; 
    z-index:3;   
    top: 60px; 
    left: 20%; 
    border-radius: 6px;
    box-shadow: 5px 5px 20px 0 rgba(203, 203, 203, 0.5);
    display: none; 
}
.detail-course-form {
    width: 100%; 
}
.detail-course-form > h1 {
    font-size: 2.5rem; 
    font-weight: 700;
    text-align: center;
    margin-bottom: 24px; 
}
.detail-course-title > div{
    margin-bottom: 16px; 
    font-size: 1.25rem; 
}
.detail-course-content > div {
    font-size: 1.25rem;
}
.detail-course-content-title > div {
    width: 45%; 
}
.detail-course-content-title > div > img {
    width: 100%;  
}
.detail-course-form-title {
    display: flex; 
    justify-content: space-between;
    align-items: center;
    margin-bottom: 24px; 
}
.detail-course-form-title > h1 {
    font-weight: 700;
}
.detail-course-content-title {
    display: flex;
    gap: 24px;
    margin-bottom: 24px; 
}
</style>

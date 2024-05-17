<template>
    <div class="detail-course" id="detail-course">
        <div class="detail-course-form">
            <div class="detail-course-form-title">
                <div></div>
                <h1>Thông tin chi tiết khóa học</h1>
                <div class="icon_close fix-close" v-on:click="closeDetailCourse()"></div>
            </div>
            <div class="detail-course-content">
                <div class="detail-course-title">
                    <div><b>Mã khóa học:</b> {{ this.makh }}</div>
                    <div><b>Tên khóa học:</b> {{ this.tenkh }}</div>
                    <div><b>Chủ đề:</b> {{ renderRolse(this.chude) }}</div>
                </div>
                <div class="detail-course-content">
                    <b>Nội dung mô tả</b>: {{ this.content }}
                </div>
            </div>
        </div>
    </div>
</template>
<script>
export default{
    props: ['propGetCourse'], 
    data(){
        return {
            makh: "", 
            tenkh: "", 
            chude: "", 
            content: "", 
        }
    }, 
    watch: {
        propGetCourse: function(){
            this.makh = this.propGetCourse.courseCode; 
            this.tenkh = this.propGetCourse.courseName; 
            this.chude = this.propGetCourse.courseType;
            this.content = this.propGetCourse.courseContent; 
        }
    }, 
    methods: {
        closeDetailCourse: function(){
            document.getElementById("detail-course").style.display = "none"; 
            document.getElementById("course_back").style.display = "none"; 
        }, 
        renderRolse: function(){
            if(this.chude === 0){
                return "Đại số"; 
            }
            else if(this.chude === 1){
                return "Hình học"; 
            }
            else {
                return "Đề thi thử"
            }
        }
    }
}
</script>
<style scoped>
.detail-course {
    width: 600px; 
    position: absolute;
    background-color: #fff;
    padding: 24px; 
    z-index:3;   
    top: 60px; 
    left: 30%; 
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
.detail-course-form-title {
    display: flex; 
    justify-content: space-between;
    align-items: center;
    margin-bottom: 24px; 
}
</style>

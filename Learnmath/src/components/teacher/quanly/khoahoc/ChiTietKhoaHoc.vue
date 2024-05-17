<template>
    <div class="chitiet_course" id="chitiet_course">
        <div>
            <div class="chitiet-course-title">
                <h1>Chi tiết lớp học</h1>
                <div class="icon_close" v-on:click="closeChiTiet()"></div>
            </div>
            <div class="chitiet-course-form">
                <div class="chitiet-course-form1">
                    <div>
                        <b>Mã lớp học: </b> 
                        <div>{{ ma }}</div>
                    </div>
                    <div>
                        <b>Tên lớp học: </b>
                        <div>{{ ten }}</div>
                    </div>
                    <div>
                        <b>Số lượng buổi học: </b>
                        <div>5</div>
                    </div>
                    <div>
                        <b>Mô tả lớp học: </b>
                        <div>{{ mota }}</div>
                    </div>
                </div>
                <div class="chitiet-course-form2">
                    <img src="https://hocmai.vn/course/images/ren-ki-nang-lam-trac-nghiem-qua-5-de-thi-tot-nghiep-thpt-mon-toan-1619430369.jpg" alt="">
                    <div>
                        <b>Thời gian bắt đầu: </b>
                        <div>{{ renderDate(tgbd)}}</div>
                    </div>
                    <div>
                        <b>Thời gian kết thúc: </b>
                        <div>{{ renderDate(tgkt)}}</div>
                    </div>
                    <div>
                        <b>Thời gian mở đăng ký: </b>
                        <div>{{ renderHours(tgdkbd)}} {{ renderDate(tgbd)}}</div>
                    </div>
                    <div>
                        <b>Thời gian đóng đăng ký: </b>
                        <div>{{ renderHours(tgdkkt)}} {{ renderDate(tgdkkt)}}</div>
                    </div>
                </div>
            </div>

        </div>
    </div>
</template>
<script>
export default {
    props: ['propGetCourse'], 
    components: {
        
    }, 
    data(){
        return {
            ma: "", 
            ten: "", 
            soluong: 0, 
            mota: "", 
            img: "", 
            tgbd: "", 
            tgkt: "", 
            tgdkbd: "", 
            tgdkkt: "",
        }
    }, 
    methods: {
        closeChiTiet: function(){
            document.getElementById("chitiet_course").style.display = "none";
            document.getElementById("course_back").style.display = "none"; 
        }, 
        renderDate: function(item){
            let dateObject = new Date(item);

            // Format dateObject to desired date format
            let formattedDate = `${dateObject.getDate()}/${dateObject.getMonth() + 1}/${dateObject.getFullYear()}`;

            // Assign the formatted date to thoiGian
            return formattedDate; 
        }, 
        renderHours: function(item){
            let dateObject = new Date(item); 
            let gio = dateObject.getHours(); 
            let phut = dateObject.getMinutes(); 
            let giay = dateObject.getSeconds(); 
            return gio + " giờ " + phut + " phút " + giay; 
        }, 
    }, 
    watch: {
        propGetCourse: function(){
            this.ma = this.propGetCourse.teacher_CourseCode; 
            this.ten = this.propGetCourse.teacher_CourseName; 
            this.mota = this.propGetCourse.teacher_CourseDescription; 
            this.img = this.propGetCourse.teacher_CourseImage; 
            this.tgbd = this.propGetCourse.startTime; 
            this.tgkt = this.propGetCourse.endTime; 
            this.tgdkbd = this.propGetCourse.registrationStartTime; 
            this.tgdkkt = this.propGetCourse.registrationEndTime; 
        }
    }
    
}
</script>
<style scoped>
.chitiet_course {
    position: absolute; 
    top: 100px; 
    left: 20%; 
    background-color: #fff; 
    z-index: 1;
    width: 60%; 
    padding: 24px;
    display: none;  
}
.chitiet-course-form {
    width: 90%;
    display: flex; 
    gap: 24px; 
    margin: auto; 
}
.chitiet-course-title {
    display: flex; 
    justify-content: space-between;
    align-items: center;
}
.chitiet-course-title > h1 {
    font-weight: 700;
}
.chitiet-course-form1{
    width: 65%; 
}
.chitiet-course-form1 > div{
    margin-bottom: 16px; 
    display: flex; 
    gap: 16px; 
}
.chitiet-course-form2 {
    width: 33%; 
}
.chitiet-course-form2 > img {
    width: 100%; 
    margin-bottom: 16px; 
}
.chitiet-course-form2 > div {
    margin-bottom: 16px; 
    display: flex; 
    justify-content: center;
    gap: 16px;  
}
</style>

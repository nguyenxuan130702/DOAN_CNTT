<template>
    <h2 class="title_course text">Khóa học miễn phí</h2>
    <div class="video">
        <div class="video_title" v-for="(item, index) in video_course" :key="index">
            <img :src="item.video_CourseImage" alt="">
            <h4>{{ item.video_CourseTitle }}</h4>
            <div class="hover_video"></div>
            <div class="hover_title" v-on:click="openVideoCourse(item)">Bắt đầu học</div>
        </div>
    </div>

    <!--Khóa học online-->
    <h2 class="tenphankh">Đăng ký học online với thầy cô</h2>
    <div class="video">
        <div class="video_title" v-for="(item, index) in teacher_course" :key="index">
            <img :src="item.teacher_CourseImage" alt="">
            <h4>{{ item.teacher_CourseName }}</h4>
            <div class="mota-khoahoc">
                <p>Thời gian học: {{ item.thoiGian }} buổi</p>
                <p>Giáo viên hướng dẫn: {{ item.teacherName }}</p>
                <button v-on:click="chiTietKhoaHoc(item)">{{ luachon[index] }}</button>
            </div>
        </div>
    </div>
    <ChatBot></ChatBot>
</template>
<script>
import BaseRequest from '@/core/BaseRequest';
import ChatBot from './mathjax/ChatBox.vue'
export default{
    name: 'CoursePage', 
    components: {
        ChatBot,
    }, 
    data() {
        return {
            video_course: [],
            userLogin: {}, 
            teacher_course: [], 
            check_dangky: {}, 
            luachon: [], 
        };
    },
    mounted() {
        //Khóa học video 
        this.userLogin = JSON.parse(localStorage.getItem("userLogin")); 
        BaseRequest.get("video_course")
        .then(response => (
            this.video_course = response.data
        ))
        .catch((error) =>{
            console.log(error);
        })

        //Khóa học online với thầy cô 
        this.fetchData(); 
    },
    methods: {
        async fetchData() {
            try {
                const response = await BaseRequest.get("view_teacher_course");
                this.teacher_course = response.data; 
                for (let i = 0; i < this.teacher_course.length; i++) {
                    const enrollmentResponse = await BaseRequest.get("enrollment/user_teacher_course?userId=" + this.userLogin.userId + "&teacher_course=" + this.teacher_course[i].teacher_CourseId);
                    const check_dangky = enrollmentResponse.data; 
                    if (check_dangky === null || check_dangky === undefined || check_dangky === "") {
                        this.luachon.push("Chi tiết");
                    } else {
                        // Điều hướng tới trang học tiếp.
                        this.luachon.push("Học tiếp");
                    }
                }
            } catch (error) {
                console.log(error);
            }
        }, 
        openVideoCourse: function(item){
            if(this.userLogin === "" || this.userLogin === undefined || this.userLogin === null){
                this.$router.push("/DetailVideo"); 
                localStorage.setItem("video_course", JSON.stringify(item));
            }
            else {
                this.$router.push("/VideoCourse"); 
                localStorage.setItem("video_course", JSON.stringify(item));
            }
        }, 
        renderDate: function(item){
            let dateObject = new Date(item);

            // Format dateObject to desired date format
            let formattedDate = `${dateObject.getDate()}/${dateObject.getMonth() + 1}/${dateObject.getFullYear()}`;

            // Assign the formatted date to thoiGian
            return formattedDate; 
        }, 
        chiTietKhoaHoc: function(item){
            if(this.userLogin === "" || this.userLogin === undefined || this.userLogin === null){
                localStorage.setItem("teacher_course", JSON.stringify(item)); 
                this.$router.push("/TeacherCourse");
            }
            else {
                BaseRequest.get("enrollment/user_teacher_course?userId=" + this.userLogin.userId + "&teacher_course=" + item.teacher_CourseId)
                .then(response => {
                    this.check_dangky = response.data; 
                    if(this.check_dangky === null || this.check_dangky === undefined || this.check_dangky === ""){
                        //điều hướng tới trang đăng ký 
                        localStorage.setItem("teacher_course", JSON.stringify(item)); 
                        this.$router.push("/TeacherCourse"); 
                    }
                    else {
                        //điều hướng tới trang học tiếp. 

                        localStorage.setItem("teacher_course", JSON.stringify(item)); 
                        this.$router.push("/LearnCourse");

                    }
                })
                .catch(error => {
                    console.log(error.message); 
                })
            }
        }
    }
}
</script>
<style>
.mota-khoahoc {
    text-align: left;
    padding-left: 24px; 
    padding-bottom: 24px; 
}
.mota-khoahoc > button {
    padding: 8px;
    border: none; 
    background-color: #2a70b8; 
    outline: none; 
    color: #fff; 
}
.mota-khoahoc > button:hover {
    background-color: orange; 
}
.tenphankh {
    width: 80%; 
    margin: auto; 
}
h2 {
    font-size: 1.75rem;
    font-weight:bold;
}
h4 {
    font-size: 1.25rem; 
    font-weight: 500;
}
.video {
    width: 80%; 
    display: flex; 
    flex-wrap: wrap;
    gap: 24px; 
    margin: auto; 
    margin-top: 20px; 
    padding-bottom: 40px; 
}
.video_title {
    text-decoration: none;
    color: #0a0a23; 
    width: 32%; 
    position: relative;  
    text-align: center;
    border-radius: 6px;
    box-shadow: 5px 5px 10px 0 rgba(39, 39, 39, 0.5);
    margin-top: 40px; 
}
.hover_video {
    position: absolute; 
    width: 100%; 
    top: 0; 
    background-color:#0a0a23;
    height: 200px; 
    opacity: 0;
    transition: opacity 0.4s;
    border-top-left-radius: 6px;
    border-top-right-radius: 6px;
}
.video_title > img {
    width: 100%; 
    height: 200px;
    border-top-left-radius: 6px;
    border-top-right-radius: 6px;
}
.hover_title {
    color: #0a0a23; 
    width: 150px; 
    padding: 10px; 
    background-color: #fff; 
    font-size: 18px; 
    font-weight: 600;
    position: absolute; 
    top: 35%;
    left: 28%;  
    border-radius: 15px;
    opacity: 0;
    transition: opacity 0.5s;
    border-radius: 6px;
}
.video_title:hover {
    background-color: #0a0a23;
    color: #fff; 
}
.video_title:hover .hover_video {
    opacity: 0.3;
}
.video_title:hover .hover_title {
    opacity: 1;
}
.hover_title:hover {
    background-color: #0a0a23; 
    color: #fff;
}
.title_course {
    width: 80%; 
    margin: auto;  
}
.text {
    margin-top: 120px; 
}
.font {
    margin-top: 40px; 
}
.video_title > h4 {
    padding-top: 20px; 
    padding-bottom: 20px; 
}
.video {
    padding-bottom: 100px; 
}
</style>

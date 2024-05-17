<template>
    <main id="main">
<!-- ======= Phần câu hỏi và kiến thức đại số ========== -->
    <h1 class="startbugg">Chào mừng {{ user.userName }} quay trở lại </h1>
  <section class="faq section-bg">
    <h2>ĐẠI SỐ</h2>
    <div v-for="(item, index) in courseDaiSo" :key="index" class="lesson_title" v-on:click="getLesson(item.courseId, item.courseCode)"> 
        <div :class="icon1[index]" lesson></div>
        <div lesson>{{ item.courseName }}</div>
        <div class="lesson_back"></div>
    </div>
  </section>

  <section class="faq section-bg fixbug">
    <h2>HÌNH HỌC</h2>
    <div v-for="(item, index) in courseHinhHoc" :key="index" class="lesson_title" v-on:click="getLesson(item.courseId, item.courseCode)"> 
        <div :class="icon2[index]" lesson></div>
        <div lesson>{{ item.courseName }}</div>
        <div class="lesson_back"></div>
    </div>
  </section>

  <section class="faq section-bg fixbug">
    <h2>ÔN TẬP VÀ ĐỀ THI THỬ</h2>
    <div v-for="(item, index) in courseOntap" :key="index" class="lesson_title" v-on:click="getLesson(item.courseId, item.courseCode)"> 
        <div :class="icon3[index]" lesson></div>
        <div lesson>{{ item.courseName }}</div>
        <div class="lesson_back"></div>
    </div>
  </section>

  <ChatBot></ChatBot>
  </main><!-- End #main -->
</template>
<script>
import BaseRequest from '@/core/BaseRequest'
import ChatBot from './mathjax/ChatBox.vue'
export default {
    components: {ChatBot}, 
    data(){
        return{
            course: [], 
            courseDaiSo: [], 
            courseHinhHoc: [], 
            courseOntap: [], 
            user: JSON.parse(localStorage.getItem("userLogin")), 
            icon1: ["icon_math1", "icon_math2", "icon_math3", "icon_math4", "icon_math5", "icon_math6"],
            icon2: ["icon_math7", "icon_math8", "icon_math9", "icon_math10", "icon_math11", "icon_math12"],
            icon3: ["icon_math13", "icon_math14", "icon_math15", "icon_math10", "icon_math11", "icon_math12"],
        }
    }, 
    mounted(){
        BaseRequest.get("course")
        .then(response => {
            this.course = response.data;
            for (const item of this.course) {
                if(item.courseType === 0){
                this.courseDaiSo.push(item); 
                }
                else if(item.courseType === 1){
                this.courseHinhHoc.push(item); 
                }
                else {
                this.courseOntap.push(item); 
                }
            }
        })
        .catch(error => {
        console.log(error.message); 
        })
    }, 
    methods: {
        getLesson: function(itemId, itemCode){ 
            BaseRequest.get("course/code?code=" + itemCode)
            .then(response => {
                localStorage.setItem("course", JSON.stringify(response.data)); 
                //console.log(JSON.parse(localStorage.getItem("course"))); 
                this.$router.push("/Lesson/" + itemId);
            })
            .catch(error => {
                console.log("Lỗi" + error.message); 
            })
        }
    }
}
</script>
<style scoped>
h1 {
    color: #0a0a23; 
    padding-top: 120px; 
    text-align: center;
    font-weight: 700;
    font-size: 2.5rem; 
    font-family: 'Lato', sans-serif !important;
    margin-bottom: 24px; 
}
</style>
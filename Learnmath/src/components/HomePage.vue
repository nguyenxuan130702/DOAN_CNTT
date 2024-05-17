<template>
  <div>
    <Baner />
    <main id="main">
<!-- ======= Phần câu hỏi và kiến thức đại số ========== -->
  <section class="faq section-bg" data-aos="fade-down" data-aos-duration="1500">
    <h2>BỘ CÂU HỎI VÀ KIẾN THỨC ĐẠI SỐ ÔN THI THPT QUỐC GIA MÔN TOÁN</h2>
    <div v-for="(item, index) in courseDaiSo" :key="index" class="lesson_title" v-on:click="getLesson(item.courseId, item.courseCode)"> 
        <div :class="icon1[index]" lesson></div>
        <div lesson>{{ item.courseName }}</div>
        <div class="lesson_back"></div>
    </div>
  </section>

  <section class="faq section-bg fixbug" data-aos="fade-up" data-aos-duration="1500">
    <h2>BỘ CÂU HỎI VÀ KIẾN THỨC HÌNH HỌC ÔN THI THPT QUỐC GIA MÔN TOÁN</h2>
    <div v-for="(item, index) in courseHinhHoc" :key="index" class="lesson_title" v-on:click="getLesson(item.courseId, item.courseCode)"> 
        <div :class="icon2[index]" lesson></div>
        <div lesson>{{ item.courseName }}</div>
        <div class="lesson_back"></div>
    </div>
  </section>

  <section class="faq section-bg fixbug" data-aos="fade-down" data-aos-duration="1500">
    <h2>BỘ CÂU HỎI VÀ KIẾN THỨC ÔN TẬP THPT QUỐC GIA MÔN TOÁN</h2>
    <div v-for="(item, index) in courseOntap" :key="index" class="lesson_title" v-on:click="getLesson(item.courseId, item.courseCode)"> 
        <div :class="icon3[index]" lesson></div>
        <div lesson>{{ item.courseName }}</div>
        <div class="lesson_back"></div>
    </div>
  </section>

  <div class="start"  v-on:click="startLesson()">
      <button>Bắt đầu miễn phí</button>
  </div>


  <!--Các câu hỏi thường gặp-->
  <section class="faq section-bg end_home">
    <h2>Các câu hỏi thường gặp: </h2>
    <div data-aos="zoom-in" data-aos-duration="1500">
      <div class="question_alway">
        <h3>Chính xác Learmath là gì? </h3>
        <p>Learmath là một cộng đồng gồm những người từ khắp nơi trên thế giới đang cùng nhau học viết mã. Chúng tôi là tổ chức từ thiện công cộng theo mục 501(c)(3).</p>
      </div>
      <div class="question_alway">
        <h3>Chính xác Learmath là gì? </h3>
        <p>Learmath là một cộng đồng gồm những người từ khắp nơi trên thế giới đang cùng nhau học viết mã. Chúng tôi là tổ chức từ thiện công cộng theo mục 501(c)(3).</p>
      </div>
      <div class="question_alway">
        <h3>Chính xác Learmath là gì? </h3>
        <p>Learmath là một cộng đồng gồm những người từ khắp nơi trên thế giới đang cùng nhau học viết mã. Chúng tôi là tổ chức từ thiện công cộng theo mục 501(c)(3).</p>
      </div>
      <div class="question_alway">
        <h3>Chính xác Learmath là gì? </h3>
        <p>Learmath là một cộng đồng gồm những người từ khắp nơi trên thế giới đang cùng nhau học viết mã. Chúng tôi là tổ chức từ thiện công cộng theo mục 501(c)(3).</p>
      </div>
      <div class="question_alway">
        <h3>Chính xác Learmath là gì? </h3>
        <p>Learmath là một cộng đồng gồm những người từ khắp nơi trên thế giới đang cùng nhau học viết mã. Chúng tôi là tổ chức từ thiện công cộng theo mục 501(c)(3).</p>
      </div>
    </div>
  </section>
  <div class="start end_button" v-on:click="startLesson()">
    <button>Bắt đầu miễn phí</button>
  </div>

  <ChatBox></ChatBox>

  </main><!-- End #main -->

  <!-- <div id="fb-root" >
    <a href="https://m.me/276853985518852">Nhắn tin</a>
  </div>
  
  <div class="fb-messengermessageus" 
       messenger_app_id="YOUR_APP_ID" 
       page_id="YOUR_PAGE_ID" 
       color="blue" 
       size="large">
  </div> -->
  </div>
</template>
<script >
import Baner from "./layout/BanerPage.vue";
import BaseRequest from '@/core/BaseRequest';
import ChatBox from './mathjax/ChatBox.vue'
// import axios from 'axios';

export default {
  components: {
    Baner, 
    ChatBox
  },
  data() {
    return {
      course: [], 
      courseDaiSo: [],
      courseHinhHoc: [], 
      courseOntap: [],
      user: JSON.parse(localStorage.getItem("userLogin")), 
      icon1: ["icon_math1", "icon_math2", "icon_math3", "icon_math4", "icon_math5", "icon_math6"],
      icon2: ["icon_math7", "icon_math8", "icon_math9", "icon_math10", "icon_math11", "icon_math12"],
      icon3: ["icon_math13", "icon_math14", "icon_math15", "icon_math10", "icon_math11", "icon_math12"],
    };
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

    console.log(this.course); 
    console.log(this.courseDaiSo)
  }, 
  methods: {
    startLesson: function(){
      if(this.user === null || this.user === undefined || this.user === ""){
        this.$router.push("/Login"); 
      }
      else{
        this.$router.push("/start"); 
      }
      console.log(this.user); 
    },
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
    }, 
    openPdf: function(){
      window.location.href = "http://res.cloudinary.com/da5nbr4gk/image/upload/v1712733104/Ng%C6%B0%E1%BB%9Di%20Nh%E1%BA%A1y%20C%E1%BA%A3m%20M%C3%B3n%20Qu%C3%A0%20Hay%20L%E1%BB%9Di%20Nguy%E1%BB%81n.pdf"
    }
  }
};
</script>
<style>
/**----------------------------------------icon math---------------- */
.icon_math1 {
  background: url('@/assets/math/math-wiki-svgrepo-com.svg') no-repeat -2px -2px;
	width: 60px;
	height: 60px;
  transform: scale(0.8);
  margin-right: 16px; 
}
.icon_math2 {
  background: url('@/assets/math/math-svgrepo-com.svg') no-repeat -6px -6px;
	width: 60px;
	height: 52px;
  transform: scale(0.8);
  margin-right: 16px; 
}
.icon_math3 {
  background: url('@/assets/math/math-formula-svgrepo-com.svg') no-repeat -8px -5px;
	width: 60px;
	height: 51px;
  transform: scale(0.8);
  margin-right: 16px; 
}
.icon_math4 {
  background: url('@/assets/math/plus-minus-bold-svgrepo-com.svg') no-repeat -4px -4px;
	width: 60px;
	height: 50px;
  transform: scale(0.8);
  margin-right: 16px;
}

.icon_math7 {
  background: url('@/assets/math/geometry-cube-svgrepo-com.svg') no-repeat 0 0;
	width: 64px;
	height: 64px;
  transform: scale(0.8);
  margin-right: 16px;
}
.icon_math8 {
  background: url('@/assets/math/3d-design-3d-icosahedron-3d-shape-geometric-geometry-icosahedron-svgrepo-com.svg') no-repeat -8px -3px;
	width: 64px;
	height: 58px;
  margin-right: 8px; 
  margin-left: 8px; 
}
.icon_math9 {
  background: url('@/assets/math/coordinates-svgrepo-com.svg') no-repeat 0 -4px;
	width: 64px;
	height: 56px;
  transform: scale(0.9);
  margin-right: 16px; 
}
.icon_math13 {
  background: url('@/assets/math/exam-a-plus-svgrepo-com.svg') no-repeat -7px 0;
	width: 64px;
	height: 64px;
  transform: scale(0.8);
  margin-right: 16px; 
}
/**---------------------------------------------------------------- */
.lesson_title {
    height: 70px;
    position: relative; 
    color: #0a0a23; 
    margin: auto; 
    margin-bottom: 10px; 
    display: flex;
    justify-content: flex-start;
    align-items: center;
    width: calc(100% - 16px); 
    font-size: 25px; 
    padding-left: 16px; 
    border: 3px solid #0a0a23; 
    font-size: 1.25rem; 
}
.lesson_title > div[lesson] {
    z-index: 1;
}

.lesson_back {
    position: absolute; 
    height: 66px;
    width: 100%; 
    background-color: #858591;
    opacity: 0.3;
    top: 0; 
    left: 0;
    right: 0; 
    bottom: 0; 
}
.lesson_title:hover{
    color: #fff; 
    cursor: pointer;
}
.lesson_title:hover .lesson_back{
    background-color: #0a0a23;
    opacity: 1;
}
#main {
  background-color: #f5f6f7; 
  text-align: left;
  padding-bottom: 10px; 
}
.faq {
  width: 60%; 
  margin: auto; 
  color: #0a0a23; 
}
.faq:first-child {
  padding-bottom: 40px; 
}
.start {
    width: 400px; 
    margin: auto; 
    margin-top: 40px;
    margin-bottom: 40px; 
    z-index: 0; 
}
.start > button {
    width: 100%; 
    height: 56px;
    border: 3px solid #feac32; 
    background-color: #feac32;
    background-image: linear-gradient(#fecc4c, #ffac33);
    color: #0a0a23; 
    font-size: 1.3rem; 
}
.question_alway > h3 {
  font-size: 1.5rem;
  font-weight: 700;
  color: #0a0a23;  
}
.question_alway > p {
  font-size: 1.25rem; 
  font-weight: 400; 
  line-height: 1.5rem;
  color: #0a0a23; 
}
.faq > h2 {
  font-weight: 700;
  font-size: 2.5rem !important; 
  color: #0a0a23 !important; 
}
.fixbug > h2 {
  color: #0a0a23 !important; 
}
.fixbug {
  padding-bottom: 40px; 
}
.end_home {
  margin-top: 40px; 
  padding-bottom: 20px; 
}
.end_button {
  padding-bottom: 100px; 
}

@-webkit-keyframes pulsate-btn {
  0% {
    transform: scale(0.6, 0.6);
    opacity: 1;
  }
  100% {
    transform: scale(1, 1);
    opacity: 0;
  }
}

@keyframes pulsate-btn {
  0% {
    transform: scale(0.6, 0.6);
    opacity: 1;
  }
  100% {
    transform: scale(1, 1);
    opacity: 0;
  }
}
@media (max-width: 1200px) {
  .faq .faq-list {
    padding: 0;
  }
}
.faq > h2 {
  color: #fff; 
  padding-bottom: 20px; 
  font-family: "Hack-ZeroSlash", monospace;
  font-size: 1.25rem; 
}
.tuvan-page {
  width: 100%; 
  background-color: #3b3b4f; 
  display: flex;
  justify-content: space-around;
  padding-top: 24px; 
  padding-bottom: 24px; 
}
.tuvan-page-title {
  color: #fff; 
  line-height: 3.5rem;
}
.tuvan-page-title > h1 {
  font-weight: 700;
  font-size: 2.5rem;
}
.tuvan-page-infor {
  width: 40%; 
  background-color: #fff; 
  border-radius: 6px;
}
.tuvan-page-input {
  width: 80%; 
  margin: auto; 
}
.tuvan-page-input > input {
  width: 100%; 
  height: 36px;
  padding-left: 8px; 
  border: 1px solid #d0d0d0;
  margin-bottom: 16px;  
  outline: none;
  border-radius: 4px;
}
.tuvan-page-input > input:focus {
  border: 1px solid #3b3b4f; 
}
.tuvan-page-input > select {
  width: 100%; 
  height: 36px;
  border: 1px solid #d0d0d0; 
  margin-bottom: 16px; 
  outline: none; 
}
.tuvan-page-input > select:focus {
  border: 1px solid #3b3b4f;
}
.tuvan-page-button {
  width: 80%; 
  padding-top: 24px; 
  padding-bottom: 24px; 
  margin: auto;  
}
.tuvan-page-button > button {
  width: 100%; 
  height: 46px;
    border: 3px solid #feac32; 
    background-color: #feac32;
    background-image: linear-gradient(#fecc4c, #ffac33);
    color: #0a0a23; 
    font-size: 1.3rem; 
}
.tuvan-page-infor > form > h3 {
  font-weight: 700;
  text-align: center;
  margin-top: 24px; 
  margin-bottom: 24px; 
}
</style>
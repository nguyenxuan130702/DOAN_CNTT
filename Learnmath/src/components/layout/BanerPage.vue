<template>
<!-- <Navbar></Navbar> -->
  <div id="carousel" class="carousel slide" data-ride="carousel">

    <div class="content">
        <div class="banner">
            <img src="@/assets/math/banner.png" alt="">
        </div>
        <div class="evaluate" data-aos="fade-down" data-aos-duration="1000">
            <h1>Học sinh nói gì về Learmath</h1>
            <div class="evaluate_content" data-aos="fade-right" data-aos-duration="1000">
                <div class="evaluate_contentimage">
                    <img src="@/assets/evaluate1.png" alt="">
                </div>
                <div class="evaluate_contentdescrible">
                    <h3>JOSEPH PEACOCK, Trường EMANUEL, NSW</h3>
                    <div>Trường EMANUEL, NSW</div>
                    <div>
                        "Trước khi biết đến Learnmath tôi chưa bao giờ có thể tập trung vào việc ôn thi môn toán. 
                        Các câu hỏi của website đã mang lại nhiều lợi ích cho khả năng nhớ lại kiến thức chính xác và hiểu 
                        biết nhiều hơn nên việc học và ôn thi môn toán trở lên dễ dàng."
                    </div>
                </div>
            </div>
            <div class="evaluate_content" data-aos="fade-right" data-aos-duration="1000">
                <div class="evaluate_contentimage">
                    <img src="@/assets/evaluate2.png" alt="">
                </div>
                <div class="evaluate_contentdescrible">
                    <h3>TOM DING</h3>
                    <div>Học viện ARK, WEMBLEY</div>
                    <div>
                        "Learnmath biến tôi trở thành 1 con nghiện toán học. 
                        Các câu hỏi mang lại lợi ích cho khả năng nhớ lại kiến thức chính xác và hiểu biết sâu sắc về khái niệm, nên 
                        mỗi giờ học cùng learmath giúp tôi giỏi toán hơn và điểm số cũng cao hơn."
                    </div>
                </div>
            </div>
            <div class="evaluate_content" data-aos="fade-right" data-aos-duration="1000">
                <div class="evaluate_contentimage">
                    <img src="@/assets/evaluate3.png" alt="">
                </div>
                <div class="evaluate_contentdescrible">
                    <h3>RENEE HERNANDEZ</h3>
                    <div>Trường THPT GREEN, ALLEN, TEXAS</div>
                    <div>
                        "Tôi luôn gặp khó khăn khi học toán. Tôi đã tham gia nhiều 
                        khóa học nhưng khóa học của learnmath là khóa học tốt nhất. 
                        Nghiên cứu toán học cũng như các dạng bài có trong đề thi THPT trên
                        learnmath đã mang lại cho tôi những kỹ năng và sự tự tin cần thiết
                        để đạt được mục tiêu mơ ước của mình."
                    </div>
                </div>
            </div>
            <div class="start">
                <button>Bắt đầu miễn phí</button>
            </div>
        </div>
    </div>
  </div>
</template>
<script>
//import Navbar from './NavbarPage.vue'
//import Format from './FooterPage.vue'
export default {
    components: {
        //Navbar
    }
}
</script>
<style>
.banner {
    margin-top: 56px; 
    padding-bottom: 60px; 
}
.banner > img {
    width: 100%; 
    height: calc(100vh - 56px);
}
.title_content {
    font-weight: 700;
    font-size: 2.5rem;
}
.content_upto_title {
    font-size: 1.25rem; 
    font-weight: 700;
}
.evaluate_content {
    width: 80%; 
    display: flex; 
    justify-content: space-between;
    margin: auto; 
    margin-bottom: 50px; 
    box-shadow: 8px 8px 20px 0 rgba(153, 152, 152, 0.5);
}
.start {
    width: 400px; 
    margin: auto; 
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
.evaluate_contentimage {
    width: 25%; 
}
.evaluate_contentdescrible {
    width: 75%; 
    font-size: 18px; 
    display: flex;
    flex-direction: column;
    justify-content: center;
    padding: 40px; 
}
.evaluate_contentdescrible > h3 {
    font-size: 1.3rem;
    font-weight: 400;
    margin-bottom: 20px; 
    color: #0a0a23; 
}
.evaluate_contentdescrible > div:last-child {
    margin-top: 20px; 
    font-size: 1.1rem; 
    color: #0a0a23; 
}
.evaluate_contentimage > img {
    width: 100%; 
    height: 100%;
}
.evaluate_content {
    background-color: #fff; 
}
.evaluate > h1 {
    margin-bottom: 40px;
    font-size: 2.5rem; 
    font-weight: 700;
    line-height: 1.1; 
}
.content > h1:first-child {
    padding-top: 120px; 
    margin-bottom: 30px;  
}
.content > img {
    margin-top: 40px; 
    margin-bottom: 40px;
    width: 50%;
    height: 450px; 
}
.content > img {
    margin-top: 20px; 
    margin-bottom: 40px;
    width: 50%;
    height: 450px; 
}
.content {
    background-color: #f5f6f7;
    color: #0a0a23; 
    display: block; 
    text-align: center;
    padding-bottom: 40px;
}
span {
  font-size: 45px;
  color: #fff;  
}
</style>
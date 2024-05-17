<template>
    <div class="detailvideo" id="detailvideo">
        <div class="detailvideo-infor">
            <h1>{{ video_course.video_CourseTitle }}</h1>
            <p>{{ video_course.video_CourseDescription }}</p>
            <div class="detailvideo-title">
                <h1>Chương trình</h1>
                <div class="detailvideo-video" v-for="(item, index) in video" :key="index">
                    <div>{{ item.videoTitle }}</div>
                    <div class="detailvideo-icon">
                        <div title="Phiếu bài tập">
                            <div class="icon_folder"></div>
                        </div>
                        <div title="Bài tập về nhà">
                            <div class="icon_file"></div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="detailvideo-dangky">
            <img :src="video_course.video_CourseImage" alt="">
            <h1>Miễn phí</h1>
            <p></p>
            <button v-on:click="openLoginForm()">Đăng nhập</button>
            <p>Tổng số bài học: {{ video.length }} bài</p>
        </div>
    </div>
    <ChatBot></ChatBot>
</template>
<script>
import BaseRequest from '@/core/BaseRequest';
import ChatBot from './mathjax/ChatBox.vue'
export default {
    components: {
        ChatBot, 
    }, 
    data(){
        return {
            video_course: {}, 
            video: [],
        }
    }, 
    mounted(){
        this.video_course = JSON.parse(localStorage.getItem("video_course"));
        BaseRequest.get("videos/video_Course?video_CourseId=" + this.video_course.video_CourseId)
        .then(response => {
            this.video = response.data; 
        })
        .catch(error => {
            console.log(error.message); 
        })
    }, 
    methods: {
        openLoginForm: function(){
            this.$router.push("/Login"); 
        },
    }
}
</script>
<style>
.icon_folder {
    background: url('@/assets/math/folder-arrow-up-svgrepo-com.svg') no-repeat -5px -10px;
	width: 54px;
	height: 44px;
    transform: scale(0.4);
}
.icon_file{
    background: url('@/assets/math/file-check-svgrepo-com.svg') no-repeat -10px -5px;
	width: 44px;
	height: 54px;
    transform: scale(0.4);
}
.detailvideo-infor {
    width: 65%; 
}
.detailvideo-infor > p {
    font-size: 1.25rem; 
}

.detailvideo-dangky > img {
    width: 100%; 
    border-radius: 6px;
    box-shadow: 5px 5px 10px 0 rgba(173, 173, 173, 0.5);
    margin-bottom: 24px; 
}
.detailvideo {
    width: 70%; 
    margin: auto; 
    display: flex;
    gap: 24px; 
    padding-top: 120px;
    min-height: 100vh;
    padding-bottom: 40px;  
    position: relative; 
}
.detailvideo-title {
    border: 1px solid #d0d0d0; 
    border-top-left-radius: 6px;
    border-top-right-radius: 6px;
}
.detailvideo-title > h1 {
    background-color: #d0d0d0;
    border-top-left-radius: 6px;
    border-top-right-radius: 6px;
    font-size: 1.75rem;
    padding-top: 16px; 
    padding-left: 16px; 
    padding-bottom: 16px;   
    margin-bottom: -2px; 
}
.detailvideo-icon {
    display: flex; 
    align-items: center;
}
.detailvideo-icon > div {
    width: 54px; 
    height: 54px;
    border: 1px solid gray; 
    background-color: #fff; 
    border-radius: 50%;
    display: flex;
    justify-content: center;
    align-items: center;
    transform: scale(0.8);
}
.detailvideo-icon > div:hover {
    background-color: #d0d0d0; 
}
.detailvideo-video {
    display: flex;
    justify-content: space-between;
    align-items: center;
    font-size: 1.25rem; 
    padding-left: 16px; 
    padding-right: 16px; 
    border-top: 1px solid #d0d0d0; 
}
.detailvideo-video:hover {
    background-color: #d0d0d0; 
}
.detailvideo-dangky {
    text-align: center;
}
.detailvideo-dangky > button {
    padding: 8px; 
    border: none; 
    outline: none; 
    border-radius: 20px;
    background-color: #feac32;
    background-image: linear-gradient(#fecc4c, #ffac33);
    margin-bottom: 16px; 
}


.fixed-header {
    width: 25%; 
    position: fixed; 
    top: 120px; 
    right: 10%; 
}
</style>

<template>
    <div class="video">
        <video aria-hidden="true" autoplay="" loop="" playsinline="">
            <source src="@/assets/video1.mp4" type="video/webm">
        </video>
        <div class="video_footer">
            <div class="action_video">
                <div class="icon_menu_audio">
                    <div class="video_menu" v-on:click="openMenuAudio()"></div>
                    <div class="list_audio" v-show="showmenuaudio">
                        <div class="list_audio_lesson" v-for="(item, index) in sound" :key="index">
                            <div class="audio_face">
                                <img src="@/assets/postcard.png" alt="">
                            </div>
                            <div class="audio_content">
                                <h4>{{ item.name }}</h4>
                                <p>{{ item.tg }}</p>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="play_audio">
                    <div class="icon_back_audio">
                        <img src="@/assets/audio/icons8-next-32.png" alt="" v-on:click="back()">
                    </div>
                    <div class="icon_start_audio" id="icon_start_audio">
                        <img src="@/assets/audio/icons8-start-50.png" alt="" v-on:click="play()">
                    </div>
                    <div class="icon_stop_audio" id="icon_stop_audio">
                        <img src="@/assets/audio/icons8-stop-50.png" alt="" v-on:click="stop()">
                    </div>
                    <div class="icon_next_audio">
                        <img src="@/assets/audio/icons8-next-32.png" alt="" v-on:click="next()">
                    </div>
                </div>
            </div>
        </div>
        <p id="fixbug"></p>
    </div>
</template>
<script>
import sound1 from '../assets/audio/better-day.mp3'
import sound2 from '../assets/audio/perfect-beauty.mp3'
import sound3 from '../assets/audio/realize-your-dreams.mp3'
export default{
    
    name: 'RadioPage',
    data(){
        return {
            sound: [
                {source: new Audio(sound1), name: 'Perfect Beauty', tg: 'Daddy_s_Music', check: true}, 
                {source: new Audio(sound2), name: 'Better Day', tg: 'Penguinmusic', check: false}, 
                {source: new Audio(sound3), name: 'Perfect Beauty', tg: 'Daddy_s_Music', check: false}, 
            ],
            showmenuaudio: false, 
        }
    } , 
    methods: {
        play: function(){
            document.getElementById("icon_start_audio").style.display = "none"; 
            document.getElementById("icon_stop_audio").style.display = "block"; 
            for(var i = 0; i<3; i++){
                if(this.sound[i].check){
                    this.sound[i].source.play(); 
                }
            }
        },
        stop: function(){
            document.getElementById("icon_start_audio").style.display = "block"; 
            document.getElementById("icon_stop_audio").style.display = "none"; 
            for(var i = 0; i<3; i++){
                if(this.sound[i].check){
                    this.sound[i].source.pause(); 
                }
            }
        }, 
        back: function(){
            document.getElementById("icon_start_audio").style.display = "none"; 
            document.getElementById("icon_stop_audio").style.display = "block";
            for(var j = 0; j<3; j++){
                this.sound[j].source.pause(); 
            }
            for(var i = 1; i<3; i++){
                if(this.sound[i].check){
                    this.sound[i-1].source.play();
                    this.sound[i-1].check = !this.sound[i-1].check; 
                    this.sound[i].check = !this.sound[i].check; 
                    break; 
                }
            }
        }, 
        next: function(){
            document.getElementById("icon_start_audio").style.display = "none"; 
            document.getElementById("icon_stop_audio").style.display = "block";
            for(var j = 0; j<3; j++){
                this.sound[j].source.pause(); 
            }
            for(var i = 0; i<2; i++){
                if(this.sound[i].check){
                    this.sound[i+1].source.play();
                    this.sound[i+1].check = !this.sound[i+1].check; 
                    this.sound[i].check = !this.sound[i].check; 
                    break; 
                }
            }
        }, 
        openMenuAudio: function(){
            this.showmenuaudio = !this.showmenuaudio; 
        }
    }
}
</script>
<style scoped>
.list_audio {
    width: 100%; 
    height: 300px;
    overflow-y: auto;
    display: flex;
    flex-direction: row;
    flex-wrap: wrap;
    position: absolute; 
    z-index: 1px; 
    bottom: 80px; 
    left: -150px;  
}
.list_audio_lesson {
    width: 100%; 
    display: flex; 
    gap: 10px; 
    align-items: center;
    background-color: #fff; 
    border-bottom: 1px solid black; 
}
.audio_face {
    width: 35%; 
}
.audio_face > img {
    width: 100%; 
}
.icon_back_audio > img {
    transform: rotate(180deg);
}
.play_audio {
    display: flex; 
    align-items: center;
    gap: 20px; 
}
.icon_menu_audio {
    width: 300px; 
    display: flex;
    justify-content: center;
    align-items: center;
    position: relative; 
}
.icon_menu_audio > img {
    width: 20%; 
    margin-left: -180px; 
}
.icon_stop_audio {
    display: none;
}
.icon_back_audio:hover {
    cursor: pointer;
}
.icon_start_audio:hover {
    cursor: pointer;
}
.icon_next_audio:hover {
    cursor: pointer;
}
.playList {
    display: none; 
}
.video {
    margin-top: 56px; 
    width: 100%; 
    height: calc(100vh - 60px); 
    overflow-y: hidden;

}
.video > video {
    width: 100%; 
}
.video_menu {
    background: url('@/assets/radio_menu.svg') no-repeat -2px -2px;
	width: 100px;
	height: 100px;
    transform: scale(0.4);
    margin-left: -320px; 
}
.video_footer {
    border-top: 5px solid #fff; 
    width: 100%; 
    height: 80px;
    background-color: #0a0a23;
    position: fixed;
    bottom: 0; 
    left: 0; 
    right: 0; 
}
.action_video { 
    height: 100%; 
    width: 80%;  
    display: flex; 
    justify-content: space-between;
    align-items: center;
    margin: auto; 
}
.video_play {
    width: 50%; 
    display: flex;
    flex-direction: row;
    justify-content: end;
    align-items: center;
    margin-right: 20px; 
}
.video_start {
    width: 10%; 
}
/* #volume {
    width: 100px; 
    height: 5px;
    background-color: black; 
} */
</style>
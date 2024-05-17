<template>
    <div id="video_detail" v-show="show_video">
        <div class="video_detail" id="video">
            <iframe width="860" height="515" :src="getVideo.videoLink" title="YouTube video player" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share" referrerpolicy="strict-origin-when-cross-origin" allowfullscreen></iframe>
            <div class="icon_close" style="margin-left: 16px" v-on:click="closeVideo()"></div>
        </div>
        <div>
            <h1>{{ getVideo.videoTitle }}</h1>
            <div class="video-detail-link">
                <a :href="getVideo.videoPdfQuestion">Bài tập về nhà</a>
                <a :href="getVideo.videoPdfAnswer">Đáp án</a>
            </div>
        </div>
    </div>
</template>
<script>
export default {
    props: ['propGetVideo'], 
    data(){
        return {
            show_video: false, 
            getVideo: {},  
        }
    }, 
    watch: {
        propGetVideo: function(){
            this.show_video = true;
            this.getVideo = this.propGetVideo;  
        }
    }, 
    methods: {
        closeVideo: function(){
            document.getElementById("video_back").style.display = "none"; 
            this.show_video = false; 
        }
    }
}
</script>
<style scoped>
#video_detail {
    position: absolute; 
    top: 80px; 
    left: 20%; 
    z-index: 1; 
    background-color: #fff; 
    padding: 24px;  
}
.video_detail {
    display: flex; 
    z-index: 2; 
}
.video-detail-link > a {
    margin-right: 16px; 
}
.icon_close {
    z-index: 2;
}
.hidden {
    display: none; 
}
</style>

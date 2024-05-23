<template>
    <div>
        <df-messenger
        intent="WELCOME"
        chat-title="LearnMath"
        agent-id="a5e0c96b-fa28-4a55-b3e8-09d7c9a05d7c"
        language-code="vi"
        ></df-messenger>
    </div>
</template>
<script>
export default {
    data(){
        return {
            open_chat_bot: false, 
        }
    }, 
    methods: {
        openChatBot: function(){
            this.open_chat_bot = true; 
        }, 
        closeChatBot: function(){
            this.open_chat_bot = false;
        }
    }
}
</script>
<style scoped>
.hopchat {
    position: relative; 
    position: fixed; 
    bottom: 10px;
    left: -50px;  
    z-index: 1;
}
.icon_message > img {
    position: absolute; 
    top: 0; 
    left: 0; 
    width: 100%; 
    height: 100%;
    animation: xoay 0.8s ease-out infinite;
}
.icon_message {
    position: absolute;
    width: 80px;
    height: 80px;
    margin-left: 100px; 
    margin-bottom: 50px; 
    z-index: 2;
    bottom: 40px; 
}
.circle-icon_message {
    width: 80px; 
    height: 80px;
    border: 3px solid #faa3a3; 
    border-radius: 50%;
    animation: expand 0.8s ease-out infinite;
}
@keyframes expand {
    0% {
        opacity: .1;
        transform: rotate(0) scale(1.2) skew(1deg);
    }
    30% {
        opacity: .5;
        transform: rotate(0) scale(1.4) skew(1deg);
    }
    100% {
        opacity: .1;
        transform: rotate(0) scale(1) skew(1deg);
    }
}

@keyframes xoay {
  0% {
    transform: rotate(0) scale(1) skew(1deg);
    }
    10% {
        transform: rotate(-25deg) scale(1) skew(1deg);
    }
    20% {
        transform: rotate(25deg) scale(1) skew(1deg);
    }
    30% {
        transform: rotate(-25deg) scale(1) skew(1deg);
    }
    40% {
        transform: rotate(25deg) scale(1) skew(1deg);
    }
    50% {
        transform: rotate(0) scale(1) skew(1deg);
    }
    100% {
        transform: rotate(0) scale(1) skew(1deg);
    }
}

.chat-box {
    position: absolute; 
    background-color: #fff; 
    border-radius: 6px;
    width: 300px; 
    padding-top: 24px;
    padding-left: 24px; 
    padding-right: 24px; 
    bottom: 180px;
    left: 150px;  
    box-shadow: 5px 5px 10px 0 rgba(133, 133, 133, 0.5);
}
.icon_bacham {
    background: url('@/assets/math/dots-3-horizontal-svgrepo-com.svg') no-repeat -8px -26px;
	width: 50px;
	height: 12px;
    transform: scale(0.3);
}
.icon_thunho {
    background: url('@/assets/math/dash-svgrepo-com.svg') no-repeat -5px -29px;
	width: 50px;
	height: 6px;
    transform: scale(0.3);
    margin-left: -25px; 
}
.chat-box-icon-title {
    display: flex; 
    align-items: center;
}
.chat-box-title {
    display: flex; 
    justify-content: space-between;
}
.chat-box-content > h1 {
    font-size: 1.75rem; 
    font-weight: 600;
    margin-top: 24px; 
    margin-bottom: 100px; 
}
.chat-box-chat > button {
    width: 100%; 
    padding-top: 10px; 
    padding-bottom: 10px;
    border: none; 
    outline: none;
    border-radius: 15px;
    background-color: rgb(255, 158, 174);  
}
.icon_mess {
    background: url('@/assets/math/messager-177-svgrepo-com.svg') no-repeat 0 0;
	width: 64px;
	height: 64px;
    transform: scale(0.3);
}
.chat-box-chat > div {
    width: 100%; 
    display: flex; 
    align-items: center;
    margin: auto; 
}
.chat-box-chat > button > a{
    text-decoration: none;
    color: black;
}
</style>

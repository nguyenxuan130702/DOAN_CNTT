<template>
    <div class="detailquestionform" id="detail_question">
        <div class="detailquestiontitle">
            <div></div>
            <h1>Thông tin chi tiết câu hỏi</h1>
            <div class="icon_close" v-on:click="closeDetailQuestion()"></div>
        </div>
        <div class="detail-question">
            <div class="detail-question-content">
                <div><b>Đề bài: </b>{{ debai }}</div>
                <div class="detail-question-img" id="detail-question-img">
                    <img :src="urlDebai" alt="">
                </div>
                <div>
                    <div><b>Đáp án A: </b>{{ dapana }}</div>
                    <div><b>Đáp án B: </b>{{ dapanb }}</div>
                    <div><b>Đáp án C: </b>{{ dapanc }}</div>
                    <div><b>Đáp án D: </b>{{ dapand }}</div>
                </div>
            </div>
            <div class="detail-question-suggest">
                <div>
                    <b>Gợi ý trả lời</b> <br>
                    <img :src="urlGoiy" alt="">
                </div>
            </div>
        </div>
    </div>
</template>
<script>
export default{
    props: ["propGetQuestion"],
    watch: {
        propGetQuestion: function(){
            this.debai = this.propGetQuestion.questionContent;
             
            if(this.propGetQuestion.questionImage === null){
                this.urlDebai = " ";
                document.getElementById("detail-question-img").style.display = "none"; 
            }
            else {
                document.getElementById("detail-question-img").style.display = "block"; 
                this.urlDebai = this.propGetQuestion.questionImage;
            }
            this.dapana =  this.propGetQuestion.answerA; 
            this.dapanb = this.propGetQuestion.answerB; 
            this.dapanc = this.propGetQuestion.answerC; 
            this.dapand = this.propGetQuestion.answerD;
            this.urlGoiy = this.propGetQuestion.questionSuggest;  
        }
    }, 
    data(){
        return {
            debai: '', 
            urlDebai: '', 
            dapana: '', 
            dapanb: '', 
            dapanc: '', 
            dapand: '', 
            urlGoiy: '', 
        }
    }, 
    methods: {
        closeDetailQuestion: function(){
            document.getElementById("detail_question").style.display = "none"; 
            document.getElementById("question_back").style.display = "none";
        }
    }
}
</script>
<style scoped>
.detailquestionform {
    position: absolute;
    z-index: 1; 
    width: 90%; 
    background-color: #fff; 
    top: 5px; 
    left: 100px; 
    padding: 24px; 
    display: none; 
}
.detailquestiontitle {
    width: 100%; 
    display: flex; 
    justify-content: space-between;
}
.detailquestiontitle > h1 {
    margin-bottom: 24px; 
    text-align: center;
    font-size: 2.5rem;
    font-weight: 700;
}
.detail-question {
    width: 100%; 
    display: flex; 
    font-size: 1.25rem; 
    justify-content: space-between;
}
.detail-question-content {
    width: 48%; 
}
.detail-question-content > div {
    margin-bottom: 16px; 
}
.detail-question-suggest {
    width: 48%; 
}
.detail-question-suggest > div > img {
    width: 100%; 
    max-height: calc(100% - 56px);
}
.detail-question-img > img{
    width: 400px; 
    height: 250px;
    margin-bottom: 24px; 
}
</style>

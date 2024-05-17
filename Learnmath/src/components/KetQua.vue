<template>
     <div class="exam-page">
        <div class="exam-page-title">
            <h1>Thi Thử Đề Thi THPT Quốc Gia</h1>
            <div>{{ lesson.lessonName }}</div>
            <div>Người làm bài: Nguyễn Hương Lan</div>
            <button v-on:click="openDetailExam()">Làm lại</button>
        </div>
        <div class="exam-page-content">
            <div class="exam-page-content-title">
                <div class="exam-question-time">
                    <div>{{ question.length }} câu hỏi</div>
                    <div>90 phút</div>
                </div>
                <div>
                    <button>90:00</button>
                </div>
            </div>
            <div class="exam-page-content-infor">
                <div>Đề thi thử THPT Quốc Gia môn Toán</div>
                <div>
                    <table>
                        <thead>
                            <tr>
                                <td>Lần thi</td>
                                <td>Tên Đề Thi</td>
                                <td>Thời gian</td>
                                <td>Điểm</td>
                            </tr>
                            <tr v-for="(item, index) in score" :key="index">
                                <td>{{ score.length - index}}</td>
                                <td>{{ lesson.lessonName }}</td>
                                <td>{{ item.time }}</td>
                                <td>{{ item.result }}</td>
                            </tr>
                        </thead>
                    </table>
                </div>
            </div>
        </div>
    </div>
</template>
<script>
export default {
    data(){
        return {
            lesson: {}, 
            score: [], 
            question: [], 
        }
    }, 
    mounted(){
        this.score = JSON.parse(localStorage.getItem("ketqua")); 
        this.lesson = JSON.parse(localStorage.getItem("getLesson"));
        this.question = JSON.parse(localStorage.getItem("listquestion")); 
    }, 
    methods: {
        openDetailExam: function(){
            this.$router.push("/DetailExam");
        }
    }
}
</script>
<style scoped>

</style>

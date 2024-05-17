<template>
    <div class="exam-page">
        <div class="exam-page-title">
            <h1>Thi Thử Đề Thi THPT Quốc Gia</h1>
            <div>{{ lesson.lessonName }}</div>
            <div>Người làm bài: Nguyễn Hương Lan</div>
            <button v-on:click="openDetailExam()">Bắt đầu làm bài</button>
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
                                <td>Tên Đề Thi</td>
                                <td>Số câu hỏi</td>
                                <td>Thời gian</td>
                            </tr>
                            <tr>
                                <td>{{ lesson.lessonName }}</td>
                                <td>{{ question.length }}</td>
                                <td>90 phút</td>
                            </tr>
                        </thead>
                    </table>
                </div>
            </div>
        </div>
    </div>
</template>
<script>
export default{
    data(){
        return {
            lesson: {}, 
            question: [], 
        }
    }, 
    mounted(){
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
<style>
.exam-page {
    padding-top: 120px; 
    width: 100%; 
    background-color: #d0d0d0; 
    min-height: 100vh;
    font-size: 1.25rem; 
    padding-bottom: 40px; 
}
.exam-page-title {
    width: 60%; 
    margin: auto; 
    text-align: center;
}
.exam-page-title > button {
    height: 36px;
    width: 200px;
    border-radius: 4px;
    margin-top: 24px;  
    background-color: #f1be32 !important;
    box-shadow: -10px -10px 15px rgba(0, 0, 0, 0.1),
    10px 10px 15px rgba(0, 0, 0, 0.1);
    border: none;
}
.exam-page-content{
    width: 60%; 
    margin: auto; 
}
.exam-page-content-title {
    display: flex; 
    justify-content: space-between;
    padding-bottom: 24px; 
    margin-top: 24px; 
}
.exam-page-content-title > div:last-child > button{
    height: 36px;
    background-color: gray; 
    color: #fff; 
    outline: none; 
    border: none; 
    padding-left: 8px; 
    padding-right: 8px; 
    border-radius: 4px; 
}
.exam-question-time {
    display: flex;
    gap: 24px; 
}
.exam-page-content-infor {
    width: 100%; 
    background-color: #fff; 
    border-top: 5px solid #0a0a23; 
}
.exam-page-content-infor > div:first-child {
    padding-top: 24px; 
    padding-bottom: 24px; 
    font-size: 1.5rem;
    padding-left: 24px;  
}
.exam-page-content-infor > div:last-child {
    width: 100%; 
}
.exam-page-content-infor > div > table {
    width: 100%;
}
.exam-page-content-infor > div > table, td {
    border: none; 
    padding-left: 16px; 
}
</style>

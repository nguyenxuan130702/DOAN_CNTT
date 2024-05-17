<template>
    <div class="video_course">
        <div class="video_course-video" v-show="open_video_course">
            <!-- <iframe width="100%" height="550px" :src="video_link" title="YouTube video player" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share" referrerpolicy="strict-origin-when-cross-origin" allowfullscreen></iframe> -->
            <iframe width="100%" height="550px" :src="video.videoLink" title="YouTube video player" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share" referrerpolicy="strict-origin-when-cross-origin" allowfullscreen></iframe>
            <h1>{{ video.videoTitle }}</h1>
        </div>

        <div id="home-work" v-show="open_home_work">
            <h1 style="margin-top: 24px">{{ video.videoTitle }}</h1>
            <div class="home-work-back">
                <img src="@/assets/video_course.png" alt="">
                <div class="home-work-button">
                    <button><a :href="video.videoPdfQuestion" target="_blank">Bài tập</a></button>
                    <button v-on:click="openBaiLam()">Làm bài</button>
                    <button><a :href="video.videoPdfAnswer" target="_blank">Lời giải chi tiết</a></button>
                </div>
            </div>
        </div>

        <div id="nop-bai" v-show="open_bai_thi">
            <h1>{{ video.videoTitle }}</h1>
            <div class="nop-bai-lam">
                <div class="bo_kiemtra">
                    <div class="bo_kiemtra-infor">
                        <h3>Câu hỏi</h3>
                        <p>Số câu đã làm: {{dem}}/{{question.length}}</p>
                        <div class="bo_kiemtra-question">
                            <div class="check_question" v-bind:class="{'select-question': select_question[index], 'done-answer': done_answer[index], 
                            'fail-answer': fail_answer[index]}" v-for="(item, index) in question" :key="index"><a :href="'#' + item.video_QuestionCode">{{ index + 1 }}</a></div>
                        </div>
                        <button v-on:click="nopBai()">Nộp bài</button>
                    </div>
                    <div class="bo_kiemtra-infor" v-show="ketqua">
                        <table>
                            <thead>
                                <tr>
                                    <th>Lần</th>
                                    <th>Số câu trả lời đúng</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr v-for="(item, index) in learning" :key="index">
                                    <td>{{ index + 1 }}</td>
                                    <td>{{ item.learningAnswer }} / {{ question.length }}</td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <div v-show="dapan" class="bo_kiemtra-infor" v-bind:class="{'qua_man': hoanthanh, 'ketqua-video': !hoanthanh}">
                        <div style="font-size: 1.25rem; padding-top: 24px; color: #fff">
                            Số câu trả lời đùng: {{ correct }}/{{ question.length }} <br>
                            <span style="font-size: 1.25rem;" v-show="hoanthanh">Hoàn thành</span>
                            <span style="font-size: 1.25rem;" v-show="!hoanthanh">Chưa hoàn thành</span>
                        </div>
                    </div>
                    <div v-show="dapan">
                        <div class="start"  v-on:click="reset_ketqua()" v-show="!hoanthanh">
                            <button>Làm lại</button>
                        </div>
                    </div>
                </div>
                <div class="baithi">
                    <div class="phan_lambai" :id="item.video_QuestionCode" v-for="(item, index) in question" :key="index" v-bind:class="{'check_answer-fail': !check_answer[index]}">
                        <div>Câu {{ index + 1 }}</div>
                        <MathJax :formula="item.video_QuestionContent" class="v-question"></MathJax>
                        <img :src="item.video_QuestionImage" alt="">
                        <div class="nop-bai-dapan" v-on:click="selectAnswer(index)">
                            <div>
                                <div class="custom-raido">
                                    <input type="radio" :name="item.video_QuestionCode" v-model="get_answer[index]" value="A">
                                    <label for="a" id="v-answerA"><MathJax :formula="item.video_QuestionAnswerA"></MathJax></label>
                                </div>
                                <br>
                                <div class="custom-raido">
                                    <input type="radio" :name="item.video_QuestionCode" v-model="get_answer[index]" value="B">
                                    <label for="b" id="v-answerB"><MathJax :formula="item.video_QuestionAnswerB"></MathJax></label>
                                </div>
                            </div>
                            <div>
                                <div class="custom-raido">
                                    <input type="radio" :name="item.video_QuestionCode" v-model="get_answer[index]" value="C">
                                    <label for="c" id="v-answerC"><MathJax :formula="item.video_QuestionAnswerC"></MathJax></label>
                                </div>
                                <br>
                                <div class="custom-raido">
                                    <input type="radio" :name="item.video_QuestionCode" v-model="get_answer[index]" value="D">
                                    <label for="d" id="v-answerD"><MathJax :formula="item.video_QuestionAnswerD"></MathJax></label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <br>
                </div>
            </div>
        </div>
        <div class="video_course-infor">
            <div>Nội dung khóa học</div>
            <div v-for="(item, index) in videos" :key="index">
                <div class="lesson-video vohieuhoa" v-bind:class="{'cohieuluc': check[index], 'ketqua-video': r[index]}" v-on:click="openVideoCourse(item,index)">
                    <div>{{ item.videoTitle }}</div>
                    <div class="icon_lockup"></div>
                </div>
                <div class="lesson-video vohieuhoa" v-bind:class="{'cohieuluc': check[index], 'ketqua-video': h[index]}" v-on:click="openHomework(item, index)">
                    <div class="btvn-video">Câu hỏi ôn tập</div>
                    <div class="icon_douling"></div>
                </div>
            </div>
        </div>
    </div>

    <!--Cảnh báo chưa hoàn thành câu hỏi mà vẫn nộp bài-->
    <div class="ad-dialog-delete" id="ad-dialog-nopbai">
        <div class="ad-dialog">
            <div class="ad-dialog-main">
                <div class="ad-dialog-title">
                    <div id="dialog-title">Thông báo</div>
                    <div class="icon_close" v-on:click="closeDialogDelete()"></div>
                </div>
                <div class="ad-dialog-content">
                    <div class="icon_cauhoi"></div>
                    <div id="thognbao">Bạn vẫn còn câu hỏi chưa trả lời. Bạn có chắc chắn muốn nộp bài?</div>
                </div>
            </div>
            <div class="ad-dialog-btn" id="ad-dialog-btn" v-on:click="guiKetQua()">
                <button>Đồng ý</button>
            </div>
        </div>

        <div class="ad-dialog-back" id="ad-dialog-back"></div>
    </div>

</template>
<script>
import BaseRequest from '@/core/BaseRequest'
import MathJax from './mathjax/MathJax.vue'

export default {
    components: {
        MathJax,
    }, 
    data(){
        return {
            videos: [],
            v: {},  
            title: "",
            titlefix: "",
            check: [],  
            getVideoStart: {}, 
            chi_so: 0, 
            j: 0, 
            video_course: {}, 
            video_link: "", 
            tg_link: "", 
            open_home_work: false, 
            open_video_course: true, 
            open_bai_thi: false,
            //Luu dap an ma nguoi hoc tra loi 
            get_answer: [], 
            //Dung de thay doi mau sac cua cac cau hoi da tra loi.
            select_question: [],

            video: {},  
            //mang luu cau hoi cua video
            question: [], 
            tg_question: "", 
            //Cai bien nay dung de dem so luong cau tra loi da lam
            dem: 0, 
            //Mang dung de kiem tra cau tra loi dung hay sai. 
            check_answer: [],
            //Bien nay dung de luu cau tra loi dung 
            correct: 0, 
            dapan: false, 
            hoanthanh: false, 
            userLogin: {}, 

            //Biến dùng để tìm kiếm xem người dùng này đã học video này chưa. Nếu học rồi thì mở mã hóa. Còn nếu chưa học thì để nó mở bài đầu tiên. 
            learning: [], //lấy theo user. sau đó tiến hành tìm kiếm mã videoId trong danh sách. Nếu có thì mở mã hóa nó. 
            //Biến này dùng để hiển thị video đang hoạt động cua video
            r: [], 
            //Biến này dùng để hiển thị bài làm đang hoạt động của phần câu hỏi ôn tập. 
            h: [], 
            //Biến này dùng để hiển thị kết quả làm bài lần trước. 
            ketqua: false, 
            //Biến để lưu kết quả vào learning 
            learning_today: {},
            index: 0, // biến này để sinh mã code tự nhiên.  
            currentDate: "",
            //Biến dùng để check kêt quả 
            done_answer: [], 
            fail_answer: [], 
        }
    }, 
    mounted(){
        this.video_course = JSON.parse(localStorage.getItem("video_course")); 
        this.userLogin = JSON.parse(localStorage.getItem("userLogin")); 

        //Lấy ra danh sách video đã học. 
        BaseRequest.get("learning/user?userId=" + this.userLogin.userId)
        .then(response => {
            this.learning = response.data; 
            console.log(this.learning); 
        })
        .catch(error => {
            console.log(error.message); 
        })

        //Lấy ra danh sách video thuộc vào khóa học đang xét. 
        BaseRequest.get("videos/video_Course?video_CourseId=" + this.video_course.video_CourseId)
        .then(response => {
            this.videos = response.data; 
            console.log(this.videos)
            //Tiến hành kiểm tra xem video đã được học chưa, nếu rồi thì mở vô hiệu quả. 
            for(var k = 0; k<this.videos.length; k++){
                for (const item of this.learning) {
                    if(this.videos[k].videoId === item.videoId ){
                        this.check[k] = true;
                        this.check[k+1] = true;
                        this.r[k+1] = true;  
                        
                        break;  
                    }
                }
            }
            console.log(this.check); 
            for (var m = this.videos.length; m >= 0; m--) {
                if(this.check[m]){
                    this.getVideoStart = this.videos[m];
                    this.video = this.getVideoStart; 
                    break; 
                }
                else {
                    this.getVideoStart = this.videos[m]; 
                    this.video = this.getVideoStart; 
                    this.check[0] = true; 
                }
            }

        })
        .catch(error => {
            console.log(error.message); 
        }); 

        //Lấy số lượng thông tin bài học 
        BaseRequest.get("learning")
        .then(response => {
            console.log(response.data); 
            this.index = response.data.length + 1; 
        })
        .catch(error => {
            console.log(error.message); 
        })


    }, 
    methods: {
        reset_ketqua: function(){
            this.dem = 0; 
            this.select_question = []; 
            this.dapan = false; 
            this.get_answer = []; 
            this.fail_answer = []; 
            this.done_answer = []; 
            for (var i = 0; i<this.question.length; i++) {
                document.getElementById(`${this.question[i].video_QuestionCode}`).style.borderColor = "#0a0a23"; 
            }
        }, 
        closeDialogDelete: function(){
            document.getElementById("ad-dialog-nopbai").style.display = "none";
        }, 
        selectAnswer: function(index){
            if(this.select_question[index] === undefined || this.select_question[index] === null || this.select_question[index] === "" || this.select_question[index] === false){
                this.dem++; 
            }
            this.select_question[index] = true; 
        }, 
        /**
         * Hàm để mở video khóa học khi click vào video. 
         */
        openVideoCourse: function(item, index){
            this.open_video_course = true; 
            this.open_home_work = false; 
            this.open_bai_thi = false;
            for(var i = 0; i<this.videos.length; i++){
                this.r[i] = false; 
                this.h[i] = false; 
            }

            this.r[index] = true; 

            this.v = item; 
            this.title = item.videoTitle; 
            this.getVideoStart = item; 
            this.chi_so = index; 
            this.tg_link = item.videoLink; 
        }, 
        /**
         * Hàm để next video tiếp theo. 
         */
        nextVideo: function(){
            this.check[this.j + 1] = true;
        }, 
        /**
         * Hàm để làm bài về nhà. 
         */
        openHomework: function(item, index){
            this.title = item.videoTitle; 
            this.getVideoStart = item; 
            this.open_home_work = true; 
            this.open_video_course = false; 
            this.open_bai_thi = false;
            for(var i = 0; i<this.videos.length; i++){
                this.h[i] = false; 
                this.r[i] = false; 
            }

            this.h[index] = true; 

            /**
             * Lấy ra câu hỏi thuộc vào video bài học đang xét.
             */
            BaseRequest.get("video_question/video?videoId=" + item.videoId)
            .then(response => {
                this.question = response.data;
                for(var i = 0; i<this.question.length; i++){
                    this.check_answer[i] = true; 
                }
            })
            .catch(error => {
                console.log(error.message); 
            })
        }, 
        /**
         * Mở bài làm. 
         */
        openBaiLam: function(){
            this.open_bai_thi = true; 
            this.open_home_work = false; 
            this.open_video_course = false; 

            BaseRequest.get("video_question/video?videoId=" + this.video.videoId)
            .then(response => {
                this.question = response.data;
                for(var i = 0; i<this.question.length; i++){
                    this.check_answer[i] = true; 
                }
            })
            .catch(error => {
                console.log(error.message); 
            })
        }, 
        /**
         * Click vào nút nộp bài và tiến hành validate rồi gửi kết quả. 
         */
        nopBai: function(){
            if(this.dem !== this.question.length){
                document.getElementById("ad-dialog-nopbai").style.display = "block"; 
            } 
            else {
                this.dapan = true; 
                this.ketqua = false; 
                this.guiKetQua(); 
            }
        }, 
        /**
         * Hàm xử lý sâu ở bên trong. 
         */
        guiKetQua: function(){
            var diem = 0; 
            console.log(this.get_answer); 
            //Kiểm tra xem mảng câu hỏi người dùng làm có câu nào sai và câu nào đúng. 
            for (var i = 0; i<this.question.length; i++) {
                if(this.get_answer[i] === this.question[i].video_QuestionResult){
                    this.check_answer[i] = true; 
                    this.done_answer[i] = true; 
                    document.getElementById(`${this.question[i].video_QuestionCode}`).style.borderColor = "green"; 
                    diem++; 
                }
                else {
                    this.fail_answer[i] = true; 
                    this.check_answer[i] = false; 
                }
            }
            //Lấy điểm để hiển thị. 
            this.correct = diem; 
            this.dapan = true; 
            if(this.correct >= this.question.length/2){
                this.hoanthanh = true; 
                //mo ma hoa bai hoc tiep theo.
                this.check[this.chi_so + 1] = true; 
                //Khi câu trả lời vượt qua thì sẽ hiển thị màu xanh. Nếu chưa vượt qua thì hiển thị màu hồng và không mở mã hóa bài tiếp theo.  

                //Tiến hành luu kêt quả vào trong bảng learning. 
                const datenow = new Date(); 
                var d = datenow.getDate();  
                var m = datenow.getMonth() + 1; 
                var y = datenow.getFullYear(); 
                if(d < 10){
                    if(m < 10){
                        this.currentDate = y + "-0" + m + "-0" + d;
                    }
                    else {
                        this.currentDate = y + "-" + m + "-0" + d;
                    }
                }
                else {
                    if(m < 10){
                        this.currentDate = y + "-0" + m + "-" + d;
                    }
                    else {
                        this.currentDate = y + "-" + m + "-" + d;
                    }
                }

                var code = "LV" + this.index
                this.learning_today.learningCode = code; 
                this.learning_today.userId = this.userLogin.userId; 
                this.learningAnswer = this.correct; 
                this.videoId = this.video.videoId; 
                this.created_at = this.currentDate; 

                BaseRequest.post("learning", this.learning_today)
                .then(response => {
                    console.log(response.data); 
                })
                .catch(error => {
                    console.log(error.message); 
                })
            }
            else {
                this.hoanthanh = false; 
            }
        }
    }, 
    watch: {
        title: function(){
            this.titlefix = this.title; 
            this.j = this.chi_so; 
            this.video_link = this.tg_link; 
            this.video = this.getVideoStart; 
        }, 
    }
}
</script>
<style>
.start{
    width: 200px;
    margin-top: 16px; 
    margin-bottom: 16px; 
    z-index: 3; 
}
.start:hover {
    cursor: pointer;
}
#dialog_hoanthanh {
    opacity: 1;
    position: absolute; 
    top: 56px; 
    left: 30%; 
}
#ad-dialog-nopbai {
    display: none; 
    position: absolute; 
    top: 0;  
}
.ad-dialog {
    left: 30%; 
}
.custom-raido > input {
    width: 20px; 
    height: 20px;
}
.custom-raido {
    display: flex; 
    align-items: center;
    gap: 8px; 
}
.nop-bai-dapan {
    display: flex;
    gap: 50px; 
}
#nop-bai {
    width: 75%; 
    height: calc(100vh - 56px);
    padding-left: 24px; 
    padding-right: 24px; 
    background-color: #d0d0d0; 
}
#nop-bai > h1 {
    text-align: center;
    margin-bottom: 24px; 
    margin-top: 24px; 
}
.nop-bai-lam {
    display: flex;
    gap: 24px; 
}
.bo_kiemtra {
    width: 25%;  
}
.bo_kiemtra-infor {
    border-radius: 10px;
    background-color: #fff;
    box-shadow: 5px 5px 10px 0 rgba(203, 203, 203, 0.5);
    text-align: center;
    padding-bottom: 24px; 
    margin-bottom: 24px; 
}
.bo_kiemtra-infor > h3 {
    border-top-left-radius: 10px;
    border-top-right-radius: 10px;
    background-color: #3b3b4f;  
    padding-top: 16px; 
    padding-bottom: 16px;
    text-align: center;
    color: #fff;  
}
.bo_kiemtra-infor > table {
    width: 100%; 
}
.bo_kiemtra-infor > table > thead > tr{
    text-align: center;
}
.phan_lambai {
    width: 100%; 
    font-size: 1.25rem; 
    background-color: #fff; 
    border-top: 5px solid #0a0a23; 
    padding-left: 16px; 
    padding-top: 16px; 
    margin-bottom: 16px; 
    padding-bottom: 16px;
}
.phan_lambai > div:first-child {
    font-weight: 700;
}
.baithi {
    width: 70%; 
    height: calc(100vh - 56px - 56px - 56px - 20px);
    overflow-y: auto;
}
.check_question {
    width: 35px; 
    height: 35px;
    border-radius: 50%;
    border: 1px solid #0a0a23; 
    display: flex;
    justify-content: center;
    align-items: center;
}
.check_question > a{
    text-decoration: none;
    color: black; 
}
.bo_kiemtra-question {
    width: 85%; 
    margin: auto; 
    display: flex; 
    gap: 10px; 
    flex-wrap: wrap;
    margin-top: 24px; 
    margin-bottom: 24px; 
}
.bo_kiemtra-infor > button {
    padding: 8px;
    outline: none;
    border-radius: 4px;
    color: #fff; 
    border: none; 
    background-color: rgb(91, 91, 250);
    background-image: linear-gradient(rgb(91, 91, 250), rgb(143, 143, 245));
    box-shadow: 5px 5px 10px 0 rgb(143, 143, 245);
    transition: all 0.3s;
}
.bo_kiemtra-infor > button:hover {
    transform: scale(1.2); 
}
* {
    position: relative; 
}
.icon_lockup {
    background: url('@/assets/math/copybook-lesson-task-homework-svgrepo-com.svg') no-repeat -5px -5px;
	width: 51px;
	height: 54px;
    transform: scale(0.3);
}
.icon_douling {
    background: url('@/assets/math/duolingo-svgrepo-com.svg') no-repeat -2px -9px;
	width: 60px;
	height: 46px;
    transform: scale(0.3);
}
.video_course {
    padding-top: 56px; 
    height: 100vh;
    display: flex; 
    background-color: #3b3b4f;
    padding-bottom: 56px;   
}
.video_course-video {
    width: 75%; 
    height: calc(100vh - 56px);
    overflow-y: auto;
    color: #fff; 
}
.video_course-infor {
    width: 25%; 
    height: 100vh;
    background-color: #fff;
}
.video_course-infor > div:first-child {
    border-bottom: 1px solid #d0d0d0; 
    padding: 24px; 
    font-size: 1.5rem; 
    font-weight: 700;
    color: #0a0a23; 
}
.lesson-video {
    width: 100%; 
    display: flex; 
    justify-content: space-between;
    align-items: center;
    border-bottom: 1px solid #d0d0d0; 
    padding-left: 16px; 
}
.lesson-video:hover {
    background-color: #d0d0d0; 
}
.video_course-select {
    width: 100%; 
    height: 56px;
    position: fixed; 
    bottom: 0; 
    left: 0; 
    right: 0; 
    z-index: 1;
    background-color: #fff;
    display: flex;
    justify-content: center;
    align-items: center;
    gap: 24px; 
    border-top: 1px solid #0a0a23; 
}
.video_course-select > button {
    height: 36px;
    outline: none; 
    border: 1px solid orangered; 
    padding-left: 16px; 
    padding-right: 16px; 
    color: orangered; 
}
.video_course-select > button:first-child {
    color: #0a0a23; 
    border: 1px solid #0a0a23; 
}
.video_course-select > button:first-child:hover {
    color: gray; 
    border: 1px solid gray; 
}
.video_course-video > h1 {
    margin-top: 24px;
    padding-left: 24px; 
    font-size: 1.5rem; 
}
.click_video {
    background-color: #fff; 
}
.click_video:hover {
    background-color: #d0d0d0; 
}
.btvn-video {
    padding-top: 16px; 
    padding-bottom: 16px; 
}
.lesson-video:hover {
    cursor: pointer;
}
#home-work {
    width: 75%; 
    text-align: center;
    background-color: #3b3b4f; 
    color: #fff; 
    display: flex; 
    flex-direction: column;
    justify-content: center;
    align-items: center;
}
.home-work-back {
    width: 80%; 
    padding: 24px; 
    text-align: center;
    background-color: #fff; 
    border: 10px solid #59596e; 
    border-radius: 6px;
    margin: auto; 
    margin-top: 24px; 
}
.home-work-back > img {
    text-align: center;
    margin-bottom: 24px; 
    border-radius: 10px;
}
.home-work-button > button > a {
    text-decoration: none;
    color: #0a0a23; 
}
.home-work-button {
    width: 100%; 
    display: flex;
    justify-content: space-between;
}
.home-work-button > button {
    padding: 8px; 
    border: none; 
    border-radius: 10px;
    background-color: red; 
    color: #fff; 
    font-size: 1.25rem; 
    background-image: linear-gradient(red, rgb(253, 63, 63));
    box-shadow: 5px 5px 10px 0 rgb(253, 63, 63);
    transition: all 0.3s;
}
.home-work-button > button:first-child {
    color: #0a0a23; 
    background-color: yellow;
    background-image: linear-gradient(yellow, rgb(253, 251, 93));
    box-shadow: 5px 5px 10px 0 rgb(253, 251, 93);
}
.home-work-button > button:last-child {
    color: #fff; 
    background-color: green;
    background-image: linear-gradient(green, rgb(78, 227, 78));
    box-shadow: 5px 5px 10px 0 rgb(78, 227, 78);
}
.home-work-button > button:hover {
    transform: scale(1.2);
}
.select-question {
    background-color: orange; 
}
.v-question {
    display: flex; 
    flex-wrap: wrap;
    align-items: center;
    gap: 8px; 
}
.check_answer-correct {
    border-color: green; 
}
.check_answer-fail {
    border-color: red; 
}
.qua_man{
    background-color: rgb(126, 233, 126); 
}
.vohieuhoa {
    pointer-events: none;
    cursor: not-allowed; 
    background-color: #d0d0d0; 
}
.cohieuluc {
    pointer-events: auto;
    background-color: #fff; 
}
.ketqua-video {
    background-color: rgb(201, 99, 99); 
    pointer-events: none;
}
.done-answer {
    background-color: green; 
}
.fail-answer {
    background-color: red; 
}
</style>
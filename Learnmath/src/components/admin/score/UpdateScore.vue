<template>
    <div class="form_course" id="form-put-score">
        <div class="form_course_title">
            <h1>Sửa điểm thi thử</h1>
            <div class="icon_close" v-on:click="closeFormPutScore()"></div>
        </div>
        <div class="form_course-content-x">
            <div class="form-course-title a-course-title" >
                <div for="">
                    <b>Người làm bài:</b> {{ getScore.userName }}
                </div>
                <div for="" class="score-img">
                    <img :src="getScore.userImage" alt="">
                </div>
                <div>
                    <b>Ngày làm bài:</b> {{ renderDate(getScore.time) }}
                </div>
            </div>
            <div for="" class="a-course-title">
                <div>
                    <b>Điểm thi:</b> <br>
                    <input type="text" v-model="score" id="score-title">
                </div>
            </div>
        </div>
        <div style="margin-top: 16px; margin-left: 24px;">
            <b>Tên đề thi:</b> {{ getScore.lessonName }}
        </div>
        <div class="course_btn" v-on:click="validatePutScore()">
            <button>Cập nhật</button>
        </div>
    </div>

    <div class="dialog_dangky" id="dialog_score" >
        <div class="dialog_dangky_title">
            <div class="icon_thanhcong"></div>
            <p id="dialog_score_content">Thêm mới thành công.</p>
        </div>
        <div class="icon_close" v-on:click="closeDialogPushVideo()"></div>
    </div>
</template>
<script>
import BaseRequest from '@/core/BaseRequest';
export default {
    props: ["propGetScore"], 
    data(){
        return {
            getScore: {}, 
            score: 0, 
        }
    }, 
    watch: {
        propGetScore: function(){
            this.getScore = this.propGetScore; 
            this.score = this.propGetScore.result; 
        }
    }, 
    methods: {
        closeFormPutScore: function(){
            document.getElementById('form-put-score').style.display = "none"; 
            document.getElementById("score_back").style.display = "none"; 
        }, 
        renderDate: function(item){
            var date = new Date(item); 
            var d = date.getDay(); 
            var m = date.getMonth() + 1; 
            var y = date.getFullYear(); 
            return d + "/" + m + "/" + y; 
        },
        validatePutScore: function(){
            const inputScore = document.getElementById("score-title");
            var isCheck = true;  
            if(this.score === "" || this.score === undefined || this.score === ""){
                this.thongbao(inputScore, "Tên khóa học không được phép để trống."); 
                isCheck = false; 
            }

            if(isCheck){
                //Gọi api update 
                this.getScore.result = this.score; 
                BaseRequest.put("score", this.getScore)
                .then(response => {
                    console.log(response.data); 
                    document.getElementById("form-put-score").style.display = "none"; 
                    document.getElementById("score_back").style.display = "none"; 
                    document.getElementById("dialog_score_content").innerHTML = "Cập nhật kết quả thành công!"; 
                    document.getElementById("dialog_score").style.opacity = "1";
                })
                .catch(error => {
                    console.log(error.message); 
                })
            }
        }, 
        /**
         * Hàm thêm mới thông báo khi có lỗi vào sau muỗi input lỗi. 
         * created by: NTXuan - 22/01/2024. 
         */
        thongbao: function(item, mes){
            item.style.borderColor = "red"; 
            let p = document.createElement("p"); 
            let node = document.createTextNode(mes); 
            p.appendChild(node); 
            p.classList.add("error-course");
            p.style.color = "red"; 
            p.style.fontStyle = "italic"; 
            p.style.fontSize = "0.9rem"; 
            item.after(p);
            //item.setAttribute("title", mes); 
        },
    }
}
</script>
<style scoped>
#form-put-score {
    display: none; 
    top: 200px;
}
.score-img > img {
    height: 80px;
    width: 80px;
}
</style>

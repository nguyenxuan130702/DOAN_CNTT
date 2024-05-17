<template>
    <div class="import-excel" id="import-excel-video">
        <div v-show="buoc1" class="buoc1-import">
            <p>Chọn dữ liệu đã chuẩn bị để nhập khẩu vào phần mềm.</p>
            <input type="file" placeholder="Chọn tệp" @change="checkFileUploadExcel">
        </div>

        <div v-show="buoc2" class="a-course_table-content buggg_in">
            <div class="import-excel-buoc2">
                <div>{{ this.done }} / {{ this.question.length }} dòng hợp lệ</div>
                <div>{{ this.fail }} / {{ this.question.length }} dòng không hợp lệ</div>
            </div>
            <table>
                <thead>
                    <tr>
                        <td style="width: 80px;">Mã câu hỏi</td>
                        <td style="width: 200px">Đề bài</td>
                        <td style="width: 80px">Đáp án đúng</td>
                        <td style="width: 150px;">Đáp án A</td>
                        <td style="width: 150px;">Đáp án B</td>
                        <td style="width: 150px;">Đáp án C</td>
                        <td style="width: 150px;">Đáp án D</td>
                        <td style="width: 200px;">Mã bài học</td>
                        <td>Tình trạng</td>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="(item, index) in question" :key="index">
                        <td style="width: 80px;">{{ item.video_QuestionCode }}</td>
                        <td style="width: 200px">{{ item.video_QuestionContent }}</td>
                        <td style="width: 80px">{{ item.video_QuestionResult }}</td>
                        <td style="width: 150px;">{{ item.video_QuestionAnswerA }}</td>
                        <td style="width: 150px;">{{ item.video_QuestionAnswerB }}</td>
                        <td style="width: 150px;">{{ item.video_QuestionAnswerC }}</td>
                        <td style="width: 150px;">{{ item.video_QuestionAnswerD }}</td>
                        <td style="width: 200px;">{{ item.videoId }}</td>
                        <td v-bind:class="{'tinhtrang-fail': !item.isImported, 'tinhtrang-done': item.isImported}">{{ item.importValidateError }}</td>
                    </tr>
                </tbody>
            </table>
        </div>

        <div v-show="buoc3" class="buoc3-import">
            <h3>Kết quả nhập khẩu</h3>
            <ul>
                <li>Số dòng nhập khẩu thành công: {{ this.done }}</li>
                <li>Số dòng nhập khẩu không thành công: {{ this.fail }}</li>
            </ul>
        </div>

        <div class="import-button">
            <button v-on:click="backBuoc()">Quay lại</button>
            <button v-on:click="nextBuoc2()">Tiếp theo</button>
            <button v-on:click="removeImport()">Đóng</button>
        </div>
    </div>
</template>
<script>
import BaseRequest from '@/core/BaseRequest';
export default {
    data(){
        return{
            buoc1: true, 
            buoc2: false,
            buoc3: false, 
            file: null,
            previousFile: null,  
            question: [],
            done: 0, 
            fail: 0, 
        }
    }, 
    methods: {
        nextBuoc2: function(){
            if(this.buoc1){ 
                //Gọi api import file excel 
                if(this.file === undefined || this.file === null){
                    alert("Bạn chưa chọn file nhập khẩu."); 
                }
                else {
                    this.buoc1 = false; 
                    this.buoc2 = true; 
                    this.buoc3 = false;
                    if(this.previousFile !== this.file){
                        const fromFile = new FormData(); 
                        fromFile.append("excelFile", this.file); 

                        BaseRequest.post("video_question/import", fromFile)
                        .then(response => {
                            this.question = response.data; 
                            console.log(this.file); 
                            console.log(this.question); 
                            var x = 0; 
                            var y = 0; 
                            for (const item of this.question) {
                                if(item.isImported){
                                    x++; 
                                }
                                else {
                                    y++; 
                                }
                            }
                            this.done = x; 
                            this.fail = y; 

                        })
                        .catch(error => {
                            console.log(error.message); 
                        })
                    }
                    this.previousFile = this.file; 
                }
            } 
            else if(this.buoc2){
                this.buoc1 = false; 
                this.buoc2 = false; 
                this.buoc3 = true; 
            }
            else {
                this.buoc1 = false; 
                this.buoc2 = false; 
                this.buoc3 = true; 
            }
        }, 
        backBuoc: function(){
            if(this.buoc1){
                this.buoc1 = true; 
                this.buoc2 = false; 
                this.buoc3 = false; 
            } 
            else if(this.buoc2){
                this.buoc1 = true; 
                this.buoc2 = false; 
                this.buoc3 = false; 
            }
            else {
                this.buoc1 = false; 
                this.buoc2 = true; 
                this.buoc3 = false; 
            }
        }, 
        removeImport: function(){
            document.getElementById("import-excel-video").style.display = "none"; 
            document.getElementById("video-question_back").style.display = "none"; 
        }, 
        checkFileUploadExcel(event){
            const fileUpload = event.target.files[0]; 
            this.file = fileUpload; 
            //đọc URL của file và gán vào imageUrl để hiển thị 
            const reander = new FileReader(); 
            reander.readAsDataURL(this.file);  
        }, 

    }
}
</script>
<style scoped>
.import-excel {
    position: absolute; 
    z-index: 1;
    background-color: #fff; 
    width: 90%; 
    height: calc(100vh - 56px);
    top: 23px; 
    left: 5%; 
    border-radius: 6px;
    display: none; 
}
.buoc1-import {
    padding-top: 24px; 
    padding-left: 24px; 
}
.import-button {
    position: absolute; 
    bottom: 0; 
    padding-bottom: 16px; 
    padding-top: 16px; 
    padding-right: 16px; 
    background-color: #d0d0d0; 
    width: 100%; 
    display: flex; 
    justify-content: flex-end;
    gap: 16px; 
}
.import-button > button {
    background-color: #50B83C; 
    padding: 8px; 
    outline: none;
    border: 1px solid #d0d0d0; 
    border-radius: 6px;
}
.import-button > button:last-child {
    background-color: #fff;
}
.import-button > button:first-child {
    background-color: #fff; 
}
.import-button > button:hover {
    background-color: #64bb53; 
}
.import-button > button:first-child:hover {
    background-color: yellowgreen; 
}
.import-button > button:last-child:hover {
    background-color: rgb(248, 132, 132); 
}
.buggg_in {
    padding-left: 16px; 
    padding-right: 16px; 
}
.buggg_in > table > tbody{
    height: 400px;
}
.import-excel-buoc2 {
    display: flex; 
    gap: 56px; 
    margin-bottom: 24px; 
}
.buoc3-import {
    padding-top: 24px; 
    padding-left: 24px; 
}
.delete-import {
    background-color: #fff; 
}
.tinhtrang-done {
    color:#50B83C; 
}
.tinhtrang-fail {
    color: red; 
}
.delete-import {
    background-color: rgb(243, 78, 78) !important; 
}
</style>

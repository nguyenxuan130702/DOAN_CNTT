<template>
    <div class="form_course form_lesson" id="form_update_lesson">
        <div class="form_course_title">
            <h1>Sửa thông tin bài học</h1>
            <p id="ubugg"></p>
            <div class="icon_close" v-on:click="closeFormUpdateLesson()"></div>
        </div>
        <div class="form_course-content-x">
            <div class="a-lesson-content">
                <div for="">
                    <b>Tên bài học </b> <br>
                    <input type="text" placeholder="tên khóa học" id="leson-update-tenbh" name="leson-update-tenbh" v-model="tenbaihoc">
                </div>
                <div for="" class="course_radio">
                    <b>Khóa học</b> <br>
                    <select name="" id="lesson-option_course" v-model="coursename">
                        <!-- <option disabled value="" class="option_first">Please select one</option> -->
                        <option v-for="x in course" :key="x.courseId" :value="x.courseId">{{ x.courseName }}</option>
                    </select>
                </div>
                <div style="margin-top: 16px;" class="course_radio">
                    <b>Tài liệu khóa học</b> <br>
                    <div style="display: none;">{{ pdfUrl }}</div>
                    <a :href="pdfUrl" target="_blank">Xem chi tiết tài liệu</a>
                    <div>Bạn muốn thay đổi file tài liệu?</div>
                    <input type="file" @change="checkFileUploadNew">
                </div>
            </div>
            <div class="a-lesson-content">
                <b>Mô tả</b><br>
                <textarea name="lesson-update-motabh" id="lesson-update-motabh" cols="30" rows="10" placeholder="Mô tả" v-model="mota"></textarea>
            </div>
        </div>
        <div class="course_btn" v-on:click="validatePutLesson()">
            <button>Cập nhật</button>
        </div>
    </div>
</template>
<script>
import BaseRequest from '@/core/BaseRequest'

export default {
    props: ['propLesson', 'propGetLesson'], 
    data(){
        return {
            indexCode: 0,
            getItem: {}, 
            tenbaihoc: '', 
            course: [], 
            coursename: '', 
            mota: '', 
            pdfUrl: '', 
            file: '', 
        }
    }, 
    created(){
        BaseRequest.get("course")
        .then(response => {
            this.course = response.data; 
        })
        .catch(error => {
            console.log(error.message); 
        })
    }, 
    watch: {
        propLesson: function(){
            this.indexCode = this.propLesson.length + 1; 
        }, 
        propGetLesson: function(){
            this.getItem = this.propGetLesson; 
            this.tenbaihoc = this.propGetLesson.lessonName; 
            this.mota = this.propGetLesson.lessonContent; 
            this.coursename = this.propGetLesson.courseId;
            this.pdfUrl = this.propGetLesson.lessonPdf;   
        }
    }, 
    methods: {
        checkFileUploadNew(event){
            this.file = event.target.files[0]; 
            if(this.file){
                //đọc URL của file và gán vào imageUrl để hiển thị 
                const reander = new FileReader(); 
                
                reander.readAsDataURL(this.file); 
            }
        }, 
        closeFormUpdateLesson: function(){
            document.getElementById("form_update_lesson").style.display = "none"; 
            document.getElementById("lesson_back").style.display = "none";
        }, 
        validatePutLesson: function(){
            const _delete = document.querySelectorAll(".error-lesson"); 
            this.isCheck = true;
            for (const item of _delete) {
                item.parentNode.removeChild(item);
            }

            const inputTen = document.getElementById("tenbh"); 
            const inputMota = document.getElementById("motabh");
            const selectcourse = document.getElementById("option_course"); 

            inputTen.style.borderColor = "#d0d0d0"; 
            inputMota.style.borderColor = "#d0d0d0";
            selectcourse.style.borderColor = "#d0d0d0";  

            var isCheck = true; 

            //validate rỗng tên bài học 
            if(this.tenbaihoc === null || this.tenbaihoc === undefined || this.tenbaihoc === ""){
                this.thongbao(inputTen, "Tên bài học không được phép để trống."); 
                isCheck = false;
            }

            //validate rỗng mô tả 
            if(this.mota === null || this.mota === undefined || this.mota === ""){
                this.thongbao(inputMota, "Nội dung mô tả bài học không được phép để trống."); 
                isCheck = false; 
            }
            
            //validate rỗng khóa học 
            if(this.coursename === "" || this.coursename === undefined || this.coursename === null){
                this.thongbao(selectcourse, "Khóa học không được để trống."); 
                isCheck = false; 
            }

            if(isCheck){
                this.getItem.lessonName = this.tenbaihoc; 
                this.getItem.lessonContent = this.mota; 
                this.getItem.courseId = this.coursename; 

                if(this.file === "" || this.file === undefined || this.file === null){
                    BaseRequest.put("lesson", this.getItem)
                    .then(response => {
                        console.log(response.data); 
                        document.getElementById("dialog_dangky_content").innerHTML = "Cập nhật bài học thành công."
                        document.getElementById('dialog_lesson').style.opacity = "1"; 
                        document.getElementById("form_update_lesson").style.display = "none"; 
                        document.getElementById("lesson_back").style.display = "none"; 
                    })
                    .catch(error => {
                        console.log(error.message); 
                    })
                }
                else {
                    const formData = new FormData();

                    formData.append("LessonId", this.getItem.lessonId); 
                    formData.append("LessonCode", this.getItem.lessonCode); 
                    formData.append("LessonName", this.tenbaihoc); 
                    formData.append("LessonContent", this.mota); 
                    formData.append("CourseId", this.coursename); 
                    formData.append("File", this.file);

                    BaseRequest.put("lesson/FilePdf", formData)
                    .then(response => {
                        console.log(response.data); 
                        document.getElementById("dialog_dangky_content").innerHTML = "Cập nhật bài học thành công."
                        document.getElementById('dialog_lesson').style.opacity = "1"; 
                        document.getElementById("form_update_lesson").style.display = "none"; 
                        document.getElementById("lesson_back").style.display = "none";
                    })
                    .catch(error => {
                        console.log(error.message); 
                    })
                }
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
            p.classList.add("error-lesson");
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
.course_radio > input[type="file"]{
    border: none; 
}
#lesson-option_course {
    width: 100%;
    height: 36px; 
    margin-top: 8px; 
    outline: none; 
    border: 1px solid #d0d0d0; 
}
#lesson-option_course:focus {
    border: 1px solid #50B83C
}
.a-lesson-content {
    width: 48% !important; 
}
.a-lesson-content > div > input {
    width: 100%; 
    height: 36px;
    border: 1px solid #d0d0d0; 
    outline: none; 
    padding-left: 8px;
    margin-top: 8px;
    margin-bottom: 16px; 
}
.a-lesson-content > div > input:focus {
    border: 1px solid #50B83C; 
}
</style>

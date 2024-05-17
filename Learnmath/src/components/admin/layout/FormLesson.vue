<template>
    <div class="form_course form_lesson" id="form_lesson" data-aos="fade-up" data-aos-duration="1500">
        <div class="form_course_title">
            <h1>Thêm mới bài học</h1>
            <div class="icon_close" v-on:click="closeFormLesson()"></div>
        </div>
        <div class="form_course-content-x">
            <div class="a-lesson-content">
                <div for="">
                    <b>Tên bài học</b> <br>
                    <input type="text" placeholder="tên khóa học" id="tenbh" name="tenbh" v-model="tenbaihoc">
                </div>
                <div for="" class="course_radio">
                    <b>Khóa học</b> <br>
                    <select name="" id="option_course" v-model="coursename">
                        <option v-for="x in course" :key="x.courseId" :value="x.courseId">{{ x.courseName }}</option>
                    </select>
                </div>
                <div class="course_radio" style="margin-top: 16px;">
                    <b>Tài liệu khóa học</b>
                    <input type="file" name="pdf-lesson" id="pdf-lesson" @change="checkFileUploadNew">
                </div>
            </div>
            <div class="a-lesson-content">
                <b>Mô tả </b><br>
                <textarea name="motabh" id="motabh" cols="30" rows="10" placeholder="Mô tả" v-model="mota"></textarea>
            </div>
        </div>
        <div class="course_btn" v-on:click="validatePushLesson()">
            <button>Thêm mới</button>
        </div>
    </div>

    <!--Thông báo thêm mới thành công-->
    <div class="dialog_dangky" id="dialog_lesson" >
        <div class="dialog_dangky_title">
            <div class="icon_thanhcong"></div>
            <p id="dialog_dangky_content">Thêm mới thành công.</p>
        </div>
        <div class="icon_close" v-on:click="closeDialogLesson()"></div>
    </div>
</template>
<script>
import BaseRequest from '@/core/BaseRequest';
export default{
    props: ['propLesson'], 
    data(){
        return {
            tenbaihoc:'', 
            mota: '', 
            course: [], 
            coursename: '', 
            lesson_post: {},
            indexCode: 0,  
            file: '', 
        }
    }, 
    created(){
        BaseRequest.get("course")
        .then(response => {
            this.course = response.data; 
            this.coursename = this.course[0].courseId; 
        })
        .catch(error => {
            console.log(error.message); 
        })
    }, 
    watch: {
        propLesson: function(){
            this.indexCode = this.propLesson.length + 1; 
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
        closeFormLesson: function(){
            document.getElementById("lesson_back").style.display = "none"; 
            document.getElementById("form_lesson").style.display = "none"; 
        }, 
        validatePushLesson: function(){
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
                var code = "L" + this.indexCode; 
                this.lesson_post.lessonCode = code; 
                this.lesson_post.lessonName = this.tenbaihoc; 
                this.lesson_post.lessonContent = this.mota; 
                this.lesson_post.courseId = this.coursename; 
                if(this.file === "" || this.file === undefined || this.file === null){
                    BaseRequest.post("lesson", this.lesson_post)
                    .then(response => {
                        console.log(response.data); 
                        document.getElementById("dialog_dangky_content").innerHTML = "Thêm mới thành công."
                        document.getElementById('dialog_lesson').style.opacity = "1"; 
                        document.getElementById("form_lesson").style.display = "none"; 
                        document.getElementById("lesson_back").style.display = "none"; 
                    })
                    .catch(error => {
                        console.log(error.message); 
                    })
                }
                else {
                    const formData = new FormData();

                    formData.append("LessonCode", code); 
                    formData.append("LessonName", this.tenbaihoc); 
                    formData.append("LessonContent", this.mota); 
                    formData.append("CourseId", this.coursename); 
                    formData.append("File", this.file);

                    BaseRequest.post("lesson/FilePdf", formData)
                    .then(response => {
                        console.log(response.data); 
                        document.getElementById("dialog_dangky_content").innerHTML = "Thêm mới thành công."
                        document.getElementById('dialog_lesson').style.opacity = "1"; 
                        document.getElementById("form_lesson").style.display = "none"; 
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
        closeDialogLesson: function(){
            document.getElementById('dialog_lesson').style.opacity = "0";
        }
    }
}
</script>
<style scoped>
.form_lesson {
    width: 900px;
}
#form_lesson {
    display: none; 
}
#option_course {
    width: 100%;
    height: 36px; 
    margin-top: 8px; 
    outline: none; 
    border: 1px solid #d0d0d0; 
}
#option_course:focus {
    border: 1px solid #50B83C
}
.option_first {
    color: #d0d0d0; 
    font-style: italic;
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
input::placeholder {
    font-size: 0.9rem; 
    font-style: italic;
}
.course_radio > input[type="file"]{
    border: none; 
    margin-top: 8px; 
}
</style>

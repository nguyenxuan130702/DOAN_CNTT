<template>
    <div class="form_course" id="form-put-course">
        <div class="form_course_title">
            <h1>Sửa thông tin khóa học</h1>
            <div id="buggg"></div>
            <div class="icon_close" v-on:click="closeFormPutCourse()"></div>
        </div>
        <div class="form_course-content-x">
            <div class="form-course-title a-course-title" >
                <div for="">
                    Tên khóa học <br>
                    <input type="text" placeholder="tên khóa học" id="u-tenkh" name="u-tenkh" v-model="u_tenkhoahoc">
                </div>
                <div for="" class="course_radio">
                    Loại khóa học <br>
                    <input type="radio" name="coursetype" value="0" v-model="u_loaiKh" checked/>
                    <label for="1">Đại số</label> <br>
                    <input type="radio" name="coursetype" value="1" v-model="u_loaiKh"/>
                    <label for="2">Hình học.</label> <br>
                    <input type="radio" name="coursetype" value="2" v-model="u_loaiKh"/>
                    <label for="3">Đề thi.</label> <br>
                </div>
            </div>
            <div for="" class="a-course-title">
                Mô tả <br>
                <textarea name="u-motakh" id="u-motakh" cols="30" rows="10" placeholder="Mô tả" v-model="u_mota"></textarea>
            </div>
        </div>
        <div class="course_btn" v-on:click="validatePutCourse()">
            <button>Cập nhật</button>
        </div>
    </div>
</template>
<script>
import BaseRequest from '@/core/BaseRequest';
export default {
    props: ['propCourse', 'propGetCourse'], 
    data(){
        return {
            u_tenkhoahoc: "", 
            u_loaiKh: 0, 
            u_mota: "",
            listCourse: [],
            coursePut: {},   
        }
    }, 
    watch: {
        propGetCourse: function(){
            this.u_tenkhoahoc = this.propGetCourse.courseName; 
            this.u_loaiKh = this.propGetCourse.courseType; 
            this.u_mota = this.propGetCourse.courseContent; 
            this.coursePut = this.propGetCourse; 
        }, 
        propCourse: function(){
            this.listCourse = this.propCourse; 
        }
    },
    methods: {
        closeFormPutCourse: function(){
            document.getElementById("course_back").style.display = "none";
            document.getElementById("form-put-course").style.display = "none"; 
        }, 
        validatePutCourse: function(){
            const _delete = document.querySelectorAll(".error-course"); 
            for (const item of _delete) {
                item.parentNode.removeChild(item);
            }

            const inputTen = document.getElementById("u-tenkh"); 
            const inputMota = document.getElementById("u-motakh"); 

            inputTen.style.borderColor = "#d0d0d0"; 
            inputMota.style.borderColor = "#d0d0d0"; 

            var isCheck = true; 

            if(this.u_tenkhoahoc === null || this.u_tenkhoahoc === undefined || this.u_tenkhoahoc === ""){
                this.thongbao(inputTen, "Tên khóa học không được phép để trống."); 
                isCheck = false; 
            }
            else {
                for (const item of this.listCourse) {
                    if(item.courseName === this.u_tenkhoahoc && this.u_tenkhoahoc !== this.propGetCourse.courseName){
                        this.thongbao(inputTen, "Khóa học đã tồn tại."); 
                        isCheck = false; 
                        break; 
                    }
                }
            }

            if(this.u_mota === null || this.u_mota === undefined || this.u_mota === ""){
                this.thongbao(inputMota, "Nội dung mô tả không được để trống."); 
                isCheck = false; 
            }

            if(isCheck){
                this.coursePut.courseName = this.u_tenkhoahoc; 
                this.coursePut.courseContent = this.u_mota;
                this.coursePut.courseType = this.u_loaiKh;  

                console.log(this.coursePut); 
                
                BaseRequest.put("course", this.coursePut)
                .then(response => {
                    console.log(response.data);
                    document.getElementById("dialog_dangky_content").innerHTML = "Cập nhật khóa học thành công."; 
                    document.getElementById("form-put-course").style.display = "none"; 
                    document.getElementById("course_back").style.display = "none"; 
                    document.getElementById("dialog_themmoicourse").style.opacity = "1"; 
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
#form-put-course {
    display: none; 
}
.form_course {
    z-index: 3;
}
</style>

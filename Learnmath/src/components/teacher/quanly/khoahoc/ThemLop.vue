<template>
    <div class="update_class" id="post_class">
        <div class="update_class-title">
            <h1>Thêm lịch học</h1>
            <div class="icon_close" v-on:click="closeThemClass()"></div>
        </div>
        <div class="update_class-form">
            <div class="update_class-form1">
                <label for="">
                    <b>Nội dung mô tả: </b> <br>
                    <input type="text" id="mota-class" v-model="ten">
                </label> <br>
                <label for="" class="update_class-time">
                    <b>Thời gian học: </b> <br>
                    <div>
                        <input type="time" id="time-class" v-model="gio"> <br>
                        <input type="date" id="date-class" v-model="ngay">
                    </div>
                </label>
            </div>
            <div class="update_class-form2">
                <label for="">
                    <b>Link lớp học</b> <br>
                    <input type="text" id="lop-class" v-model="link_lop">
                </label> <br>
                <label for="">
                    <b>Tài liệu tham khảo</b> <br>
                    <input type="file" id="tailieu-class" @change="checkFileUploadAdmin">
                </label>
            </div>
        </div>
        <div class="update_class-button" v-on:click="themNhatLop()">
            <button>Cập nhật</button>
        </div>
    </div>
</template>
<script>
//import BaseRequest from '@/core/BaseRequest';
import moment from 'moment';
import BaseRequest from '@/core/BaseRequest';
export default {
    props: ['propCourse', 'propListClass'], 
    data(){
        return {
            ten: "", 
            gio: "", 
            ngay: "", 
            link_lop: "", 
            file: "", 
            course: {}, 
            listClass: [],
            timeDifference: {
                hours: 0,
                minutes: 0
            }
        }
    }, 
    mounted(){
        // BaseRequest.get("view_class_assignment/teacher_course?teacher_CourseId=" + this.course.teacher_CourseId)
        //     .then(response => {
        //         this.listClass = response.data; 
        //     })
        //     .catch(error => {
        //         console.log(error.message); 
        //     })
    }, 
    watch: {
        propCourse: function(){
            this.course = this.propCourse; 
            // BaseRequest.get("view_class_assignment/teacher_course?teacher_CourseId=" + this.course.teacher_CourseId)
            // .then(response => {
            //     this.listClass = response.data; 
            // })
            // .catch(error => {
            //     console.log(error.message); 
            // })
            this.ten = ""; 
            this.gio = ""; 
            this.ngay = ""; 
            this.link_lop = ""; 
            this.file = ""; 
        }, 
        propListClass: function(){
            this.listClass = this.propListClass; 
        }
    }, 
    methods: {
        closeThemClass: function(){
            document.getElementById("post_class").style.display = "none"; 
            document.getElementById("course_back").style.display = "none";
        }, 
        themNhatLop: function(){
            console.log(this.listClass); 
            const input1 = document.getElementById("mota-class"); 
            const input2 = document.getElementById("time-class"); 
            const input3 = document.getElementById("date-class"); 
            const input4 = document.getElementById('lop-class'); 
            const input5 = document.getElementById("tailieu-class"); 

            const _delete = document.querySelectorAll(".error-class"); 
            this.isCheck = true;
            for (const item of _delete) {
                item.parentNode.removeChild(item);
            }

            input1.style.borderColor = "#d0d0d0"; 
            input2.style.borderColor = "#d0d0d0"; 
            input3.style.borderColor = "#d0d0d0"; 
            input4.style.borderColor = "#d0d0d0"; 
            input5.style.borderColor = "#d0d0d0";

            var isCheck = true; 
            if(this.ten === "" || this.ten === undefined || this.ten === null){
                this.thongbao(input1, "Nội dung mô tả không được để trống."); 
                isCheck = false; 
            }
            
            if(this.gio === "" || this.gio === undefined || this.gio === null){
                this.thongbao(input2, "Giờ học không được để trống."); 
                isCheck = false; 
            }
            else {
                if(this.ngay === "" || this.ngay === undefined || this.ngay === null){
                    this.thongbao(input3, "Ngày học không được để trống."); 
                    isCheck = false; 
                }
                else {
                    this.gio = this.gio + ":00"
                    const datenew = new Date(); 
                    console.log(this.renderDate(datenew)); 
                    
                    for (const item of this.listClass) {
                        this.sosanhGio(item.time, this.gio); 
                        if(this.timeDifference.hours < 1 && this.ngay === this.renderDate(item.date) && this.ngay > this.renderDate(datenew)){
                            this.thongbao(input2, "Trùng lịch với ngày " + this.ngay); 
                            isCheck = false;
                            break;  
                        }
                        if(this.gio === item.time && this.ngay === this.renderDate(item.date) && this.ngay > this.renderDate(datenew)){
                            this.thongbao(input2, "Trùng lịch với ngày " + this.ngay); 
                            isCheck = false;
                            break;  
                        }
                    }
                }
            }

            if(this.ngay === "" || this.ngay === undefined || this.ngay === null){
                this.thongbao(input3, "Ngày học không được để trống."); 
                isCheck = false; 
            }


            if(this.link_lop === "" || this.link_lop === undefined || this.link_lop === null){
                this.thongbao(input4, "Link lớp học không được để trống."); 
                isCheck = false; 
            }

            if(this.file === "" || this.file === undefined || this.file === null){
                this.thongbao(input5, "File tài liệu không được để trống."); 
                isCheck = false; 
            }

            if(isCheck){
                //Tiến hành thêm mới vào database. 
                //document.getElementById("dmdm").innerHTML = this.gio; 
                
                const formData = new FormData(); 
                formData.append("ClassCode", "string"); 
                formData.append("Teacher_CourseId", this.propCourse.teacher_CourseId); 
                formData.append("Date", this.ngay); 
                formData.append("Time", this.gio); 
                formData.append("ClassLink", this.link_lop); 
                formData.append("File", this.file); 
                formData.append("ClassName", this.ten); 


                console.log(this.course.teacher_CourseId + "-" + this.ngay + "-" + this.gio + "-" + this.link_lop + this.ten); 

                BaseRequest.post("class/FilePdf", formData)
                .then(response => {
                    alert("Thêm mới buổi học thành công!"); 
                    document.getElementById("post_class").style.display = "none"; 
                    document.getElementById("course_back").style.display = "none"; 
                    console.log(response.data); 
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
            p.classList.add("error-class");
            p.style.color = "red"; 
            p.style.fontStyle = "italic"; 
            p.style.fontSize = "0.9rem"; 
            item.after(p);
            //item.setAttribute("title", mes); 
        },
        checkFileUploadAdmin(event){
            this.file = event.target.files[0]; 
            if(this.file){
                //đọc URL của file và gán vào imageUrl để hiển thị 
                const reander = new FileReader(); 
                reander.readAsDataURL(this.file); 
            }
        },
        renderDate: function(item){
            let dateObject = new Date(item);
            let y = dateObject.getFullYear(); 
            let m = dateObject.getMonth() + 1; 
            if(m < 10){
                m = '0' + m; 
            }

            let d = dateObject.getDate(); 
            if(d < 10){
                d = '0' + d; 
            }

            // Format dateObject to desired date format
            let formattedDate = y + "-" + m + "-" + d; 

            // Assign the formatted date to thoiGian
            return formattedDate;  
        },
        sosanhGio: function(item1, item2){
            const start = moment(item1, 'HH:mm');
            const end = moment(item2, 'HH:mm');
            const duration = moment.duration(end.diff(start));
            this.timeDifference.hours = duration.hours();
            this.timeDifference.minutes = duration.minutes();


            // const start = moment(item1); 
            // const end = moment(item2); 
            // const duration = moment.duration(end.diff(start)); 
            // return duration.days; 
        }
    }
}
</script>
<style scoped>
.update_class {
    position: absolute; 
    top: 100px; 
    left: 20%; 
    background-color: #fff; 
    width: 60%;
    z-index: 1;
    display: none; 
}
.update_class-title {
    display: flex; 
    justify-content: space-between;
    align-items: center;
    padding-top: 24px; 
    padding-left: 24px;
    padding-right: 24px; 
}
.update_class-form {
    display: flex; 
    justify-content: space-between;
    padding: 24px; 
}
.update_class-time {
    display: flex; 
    gap: 16px; 
    margin-top: 16px; 
}
.update_class-time > div {
    width: 80%; 
}
.update_class-time > div > input {
    width: 100%; 
    margin-bottom: 16px; 
}
.update_class-time > div > input {
    width: 100%; 
    margin-top: 8px; 
    height: 36px;
    border: 1px solid #d0d0d0; 
    border-radius: 6px;
    outline: none;
    padding-left: 8px;
}
.update_class-time > div > input:focus {
    border: 1px solid #50B83C; 
}
.update_class-form1 {
    width: 45%;
}
.update_class-form2 {
    width: 45%; 
}
.update_class-form1 > label {
    width: 100%; 
}
.update_class-form2 > label{
    width: 100%; 
    margin-bottom: 16px; 
}
.update_class-form1 > label > input{
    width: 100%; 
    margin-top: 8px; 
    height: 36px;
    border: 1px solid #d0d0d0; 
    border-radius: 6px;
    outline: none;
    padding-left: 8px;
}
.update_class-form2 > label > input {
    width: 100%; 
    margin-top: 8px; 
    height: 36px;
    border: 1px solid #d0d0d0; 
    border-radius: 6px;
    outline: none;
    padding-left: 8px; 
}
.update_class-form2 > label:last-child > input {
    border: none; 
}
.update_class-form1 > label > input:focus {
    border: 1px solid #50B83C; 
}
.update_class-form2 > label > input:focus {
    border: 1px solid #50B83C; 
}
.update_class-form2 > label > input {
    width: 100%; 
    margin-top: 8px; 
}
.update_class-button {
    width: 100%; 
    padding: 24px; 
    background-color: #d0d0d0; 
    display: flex; 
    justify-content: flex-end;
}
.update_class-button > button {
    padding: 8px; 
    background-color: #50B83C; 
    border: none; 
    color: #fff; 
}
</style>

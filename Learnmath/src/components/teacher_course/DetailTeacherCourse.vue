<template>
    <div class="detailvideo" id="detailvideo">
        <div class="detailvideo-infor">
            <h1>{{ teacher_course.teacher_CourseName }}</h1>
            <p>{{ teacher_course.teacher_CourseDescription }}</p>
            <div class="detailvideo-title">
                <h1>Chương trình</h1>
                <div class="detailvideo-video" v-for="(item, index) in classList" :key="index">
                    <div>{{ item.className}}</div>
                    <div class="detailvideo-icon">
                        <div title="Phiếu bài tập">
                            <div class="icon_folder"></div>
                        </div>
                        <div title="Bài tập về nhà">
                            <div class="icon_file"></div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="detailvideo-dangky">
            <img :src="teacher_course.teacher_CourseImage" alt="">
            <h1>Miễn phí</h1>
            <p>Thời gian đăng ký còn lại.</p>
            <p>{{ thoiGianConLai }}</p>
            <button v-on:click="dangKy()">Đăng ký</button>
            <p>Tổng số buổi học: {{ teacher_course.thoiGian }} buổi</p>
        </div>
    </div>
</template>
<script>
import BaseRequest from '@/core/BaseRequest';
export default {
    data(){
        return {
            //Biến lưu thông tin khóa học
            teacher_course: {}, 
            //Biến lưu lộ trình khóa học
            classList: [], 
            thoiGianConLai: "", 
            dangky: {}, 
            userLogin: {}, 
        }
    }, 
    mounted(){
        this.userLogin = JSON.parse(localStorage.getItem("userLogin")); 
        this.teacher_course = JSON.parse(localStorage.getItem("teacher_course")); 
        BaseRequest.get("view_class_assignment/teacher_course?teacher_CourseId=" + this.teacher_course.teacher_CourseId)
        .then(response => {
            this.classList = response.data; 
        })
        .catch(error => {
            console.log(error.message); 
        })

        this.updateRemainingTime();

        setInterval(this.updateRemainingTime, 1000);
    }, 
    methods: {
        updateRemainingTime() {
            // Assume you have fetched thoiBatDau and thoiKetThuc from the API
            const thoiKetThuc = new Date(this.teacher_course.registrationEndTime); // Example end time from API

            // Calculate remaining registration time
            const hienTai = new Date();
            const thoiGianConLai = thoiKetThuc - hienTai;

            // Convert remaining time to days, hours, minutes, seconds
            const ngay = Math.floor(thoiGianConLai / (1000 * 60 * 60 * 24));
            const gio = Math.floor((thoiGianConLai % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
            const phut = Math.floor((thoiGianConLai % (1000 * 60 * 60)) / (1000 * 60));
            const giay = Math.floor((thoiGianConLai % (1000 * 60)) / 1000);

            if(ngay < 0 || gio < 0 || phut < 0 || giay < 0){
                this.thoiGianConLai = "Đã hết thời gian đăng ký."; 
            }
            else {
                this.thoiGianConLai = `${ngay} ngày ${gio} giờ ${phut} phút ${giay} giây`;
            }

            // Format remaining time as a string
        }, 
        dangKy: function(){
            if(this.userLogin === null || this.userLogin === "" || this.userLogin === undefined){
                this.$router.push("/Login"); 
            }
            else {
                const dateNow = new Date(); 
                const d = dateNow.getDate(); 
                const m = dateNow.getMonth() + 1; 
                const y = dateNow.getFullYear();

                const h = dateNow.getHours(); 
                const p = dateNow.getMinutes(); 
                const s = dateNow.getSeconds(); 

                var x = ''; 
                var k = ''; 
                var z = ''; 
                var day = ''; 
                var month = ''; 
                if(h < 10){
                    x = '0' + h; 
                }
                else {
                    x = h; 
                }
                if(p < 10){
                    k = '0' + p; 
                }
                else {
                    k = p; 
                }
                if(s < 10){
                    z = '0' + s; 
                }
                else {
                    z = s; 
                }
                if(d < 10){
                    day = '0' + d; 
                }
                else {
                    day = d; 
                }
                if(m < 10){
                    month = '0' + m; 
                }
                else {
                    month = m; 
                }
                const date_new = y + '/' + month + '/' + day + ' ' + x + ':' + k + ':' + z; 


                this.dangky.enrollmentCode = "string"; 
                this.dangky.teacher_CourseId = this.teacher_course.teacher_CourseId;
                this.dangky.userId = this.userLogin.userId; 
                this.dangky.date = date_new; 

                BaseRequest.post("enrollmeent", this.dangky)
                .then(response => {
                    console.log(response.data); 
                    alert("Đăng ký khóa học thành công!"); 
                    this.$router.push("/LearnCourse"); 
                })
                .catch(error => {
                    console.log(error.message); 
                })
            }
        }
    }
}
</script>
<style scoped>

</style>

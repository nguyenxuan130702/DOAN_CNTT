<template>
    <Navbar></Navbar>
    <Sidebar></Sidebar>
    <div class="home-teacher" id="course-danhgia-teacher">
        <h1>Danh sách lớp học</h1>
        <div class="a-course_table">
            <div class="a-course_table-content">
                <table>
                    <thead>
                        <tr>
                            <td style="width: 200px;">Mã lớp học</td>
                            <td style="width: 500px">Tên lớp học</td>
                            <td >Thời gian bắt đầu</td>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-for="(item, index) in teacher_course" :key="index">
                            <td style="width: 200px;">{{ item.teacher_CourseCode }}</td>
                            <td style="width: 500px">{{ item.teacher_CourseName }}</td>
                            <td >{{ renderDate(item.startTime)}}</td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <div class="a-course_table-page">
                <div style="margin-left: 24px;">
                    Tổng: 10
                </div>
                <div style="margin-right: 24px" class="a-course_table-page1">
                    <div>Số bản ghi/trang: 
                        <select name="" id="">
                            <option value="5">5</option>
                            <option value="10">10</option>
                            <option value="15">15</option>
                        </select>
                    </div>
                    <div class="icon_back" v-on:click="backPageCourse()"></div>
                    <div class="icon_next" v-on:click="nextPageCourse()"></div>
                </div>
            </div>
        </div>
    </div>
</template>
<script>
import BaseRequest from '@/core/BaseRequest'
import Navbar from './layout/NavbarTeacher.vue'
import Sidebar from './layout/SidebarTeacher.vue'
export default {
    components: {
        Navbar, 
        Sidebar
    }, 
    data(){
        return {
            teacher_course: [], 
        }
    }, 
    mounted(){
        BaseRequest.get("teacher_course")
        .then(response => {
            this.teacher_course = response.data; 
        })
        .catch(error => {
            console.log(error.message);
        })
    }, 
    methods: {
        renderDate: function(item){
            let dateObject = new Date(item);

            let formattedDate = `${dateObject.getDate()}/${dateObject.getMonth() + 1}/${dateObject.getFullYear()}`;

            return formattedDate; 
        },
    }
}
</script>
<style scoped>
.home-teacher {
    width: calc(100% - 250px); 
    float: left;
    background-color: #f5f6f7; 
    height: calc(100vh - 56px); 
}
.home-teacher > h1 {
    text-align: center;
    font-weight: 600;
    margin-top: 24px; 
}
.a-course_table-content > table > tbody {
    min-height: 400px;
}
</style>
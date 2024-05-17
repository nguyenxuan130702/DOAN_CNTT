<template>
<Navbar></Navbar>
<Sidebar></Sidebar>
<FormUser :propTeacher="user"></FormUser>
<FormUpdateUser :propGetTeacher="getUser" :propTeacher="user"></FormUpdateUser>
<DetailUser :propName="getUser"></DetailUser>
    <div>
        <div class="a-course">
        <h1>Quản lý tài khoản giáo viên</h1>
        <div class="a-course_table">
            <div class="a-course_table-title">
                <div class="a-course_table-title-search">
                    <input type="text" placeholder="Tìm kiếm theo mã hoặc vai trò" id="search-teacher">
                    <div class="search-course">
                        <div class="icon_search" v-on:click="searchUser()"></div>
                    </div>
                </div>
                <div class="a-course_table-title-btn">
                    <button v-on:click="openFormUser()">Thêm mới</button>
                    <div class="icon_loaddata_sty">
                        <div class="icon_loaddata" v-on:click="loadDataAfterReplace()"></div>
                    </div>
                </div>
            </div>
            <div class="a-course_table-content user-admin">
                <table>
                    <thead>
                        <tr>
                            <td style="width: 100px;">Mã tài khoản</td>
                            <td style="width: 300px">Họ và tên</td>
                            <td style="width: 300px">Email</td>
                            <td >Mô tả</td>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-for="item in user" :key="item.teacherId">
                            <td style="width: 100px; text-align: center;">{{ item.teacherCode }}</td>
                            <td style="width: 300px">{{ item.teacherName }}</td>
                            <td style="width: 300px">{{ item.teacherEmail }}</td>
                            <td class="chucnang_course"> {{ item.teacherDescription }}
                                <div class="chucnang_course-style hover_user">
                                    <div class="updatehover" title="Sửa thông tin" v-on:click="updateItem(item)">
                                        <div class="icon_update"></div>
                                    </div>
                                    <div class="deletehover" title="Xóa người dùng" v-on:click="deleteItem(item)">
                                        <div class="icon_delete"></div>
                                    </div>
                                </div>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <div class="a-course_table-page">
                <div style="margin-left: 24px;">
                    Tổng: {{ this.user.length }}
                </div>
                <div style="margin-right: 24px" class="a-course_table-page1">
                    <div>Số bản ghi/trang: 
                        <select name="" id="" v-model="pageSize">
                            <option value="10">10</option>
                            <option value="20">20</option>
                            <option value="30">30</option>
                        </select>
                    </div>
                    <div class="icon_back" v-on:click="backPageUser()"></div>
                    <div class="icon_next" v-on:click="nextPageUser()"></div>
                </div>
            </div>
        </div>
    </div>
    </div>
    <div class="course_back" id="teacher_back"></div>


    <CanhBao></CanhBao>

</template>
<script>
import Navbar from '@/components/admin/layout/NavbarAdmin.vue'
import FormUser from '@/components/admin/giaovien/FormThemGV.vue'
import Sidebar from '@/components/admin/layout/SidebarAdmin.vue'
import BaseRequest from '@/core/BaseRequest'
import FormUpdateUser from '@/components/admin/giaovien/UpdateGV.vue'
import DetailUser from '@/components/admin/layout/DetailUser.vue'
import CanhBao from '@/components/admin/layout/DialogCanhbao.vue'
export default{
    data(){
        return {
            user: [], 
            getUser: {}, 
            thongbao: "Bạn có chắc chắc muôn xóa", 
            showDelete: true, 
            tg: [], 
            pageSize: 10, 
            page: 1, 

            //Biến dùng để luu các kết quả thi thử của người dùng. 
            teacher_course: [], 
            teacher_course_code: [],  

            //Biến dùng để lưu các kết quả làm câu hỏi của người dùng 
            enrollment: [], 
            enrollment_code: [],  

            //Biến dùng để lưu các kết quả học video. 
            class: [],
            class_code: [], 

            //Biến dùng để lưu comment 
            assign: [], 
            assign_code: [], 
            //Biến dùng để lưu thread
            submit: [], 
            submit_code: [], 
            result: [], 
            result_code: [], 
        }
    }, 
    components: {
        FormUser, 
        Sidebar, 
        Navbar, 
        FormUpdateUser, 
        DetailUser, 
        CanhBao, 
    }, 
    created(){
        this.loadData(); 
    }, 
    watch: {
        pageSize: function(){
            BaseRequest.get("teacher")
            .then(response => {
                this.user = response.data; 
                console.log(this.user); 
            })
            .catch(error => {
                console.log(error.message); 
            })
        }
    },
    methods: {
        nextPageUser: function(){
            this.page++; 
            BaseRequest.get("teacher")
            .then(response => {
                this.user = response.data; 
                console.log(this.user); 
            })
            .catch(error => {
                console.log(error.message); 
            })
        }, 
        backPageUser: function(){
            this.page--; 
            BaseRequest.get("teacher")
            .then(response => {
                this.user = response.data; 
                console.log(this.user); 
            })
            .catch(error => {
                console.log(error.message); 
            })
        }, 
        loadData(){
            BaseRequest.get("teacher")
            .then(response => {
                this.user = response.data; 
                console.log(this.user); 
            })
            .catch(error => {
                console.log(error.message); 
            })
        },
        loadDataAfterReplace: function(){
            BaseRequest.get("teacher")
            .then(response => {
                this.user = response.data; 
                console.log(this.user); 
            })
            .catch(error => {
                console.log(error.message); 
            })
        }, 
        openFormUser: function(){
            document.getElementById("teacher_back").style.display = "block"; 
            document.getElementById("form_teacher-post").style.display = "block"; 
        }, 
        renderRole: function(item){
            if(item === 0){
                return "Người dùng"; 
            }
            else {
                return "Người quản trị"; 
            }
        },
        updateItem: function(item){
            this.getUser = item; 
            document.getElementById("form_update_teacher").style.display = "block"; 
            document.getElementById("teacher_back").style.display = "block"; 
        }, 
        seeDetailItem: function(item){
            this.getUser = item; 
            document.getElementById("detail-user").style.display = "block"; 
            document.getElementById("user_back").style.display = "block";
        }, 
        deleteItem: function(item){
            
            document.getElementById("thognbao").innerHTML = "Bạn có chắc chắn muốn xóa giáo viên có mã " +  item.teacherCode; 

            document.getElementById("ad-dialog-delete").style.display = "block"; 

            document.getElementById("ad-dialog-btn").addEventListener("click", ()=>{
                //Thực hiện xóa các dữ liệu ở các bảng liên quan. 
                //bảng teacher_course 
                BaseRequest.get("teacher_course/teacher?teacherId=" + item.teacherId)
                .then(response => {
                    this.teacher_course = response.data; 
                    if(this.teacher_course.length > 0){
                        for (const item of this.teacher_course) {
                            this.teacher_course_code.push(item.teacher_CourseCode); 
                            BaseRequest.get("enrollment/user_enrollment_teacher_course?teacher_CourseId=" + item.teacher_CourseId)
                            .then(response => {
                                this.enrollment = response.data; 
                                if(this.enrollment.length > 0){
                                    for (const item1 of this.enrollment) {
                                        this.enrollment_code.push(item1.enrollmentCode); 
                                    }
                                    //Xóa danh sách enroll 
                                }
                            })

                            //Bảng class 
                            BaseRequest.get("class/teacher_course?teacher_CourseId=" + item.teacher_CourseId)
                            .then(response => {
                                this.class = response.data; 
                                if(this.class.length > 0){
                                    for (const item2 of this.class) {
                                        this.class_code.push(item2.classCode); 
                                        //Xóa bảng bài tập 
                                        BaseRequest.get("assignment/class?classId=" + item2.classId)
                                        .then(response => {
                                            this.assign = response.data;
                                            if(this.assign.length > 0){
                                                for (const item3 of this.assign) {
                                                    this.assign_code.push(item3.assignmentCode); 
                                                    //Bảng nộp bài
                                                    BaseRequest.get("submit/submit_assignment?assignmentId=" + item3.assignmentId)
                                                    .then(response => {
                                                        this.submit = response.data; 
                                                        if(this.submit.length > 0){
                                                            for (const item of this.submit) {
                                                                this.submit_code.push(item.submitCode); 
                                                            }
                                                            //xóa bảng submit 
                                                            BaseRequest.delete("submit/any", this.submit_code)
                                                            .then(response => {
                                                                console.log(response.data); 
                                                            })
                                                        }
                                                    })

                                                    //Bảng điểm 
                                                    BaseRequest.get("result/assignment?assignmentId=" + item3.assignmentId)
                                                    .then(response => {
                                                        this.result = response.data; 
                                                        if(this.result.length > 0){
                                                            for (const item of this.result) {
                                                                this.result_code.push(item.resultCode);
                                                            }
                                                            //xóa bảng submit 
                                                            BaseRequest.delete("result/any", this.result_code)
                                                            .then(response => {
                                                                console.log(response.data); 
                                                            })
                                                        }
                                                    })
                                                }
                                            }
                                            
                                            //Xóa bảng bài tập
                                            BaseRequest.delete("assignment/any", this.assign_code)
                                            .then(response => {
                                                console.log(response.data); 
                                            })
                                        })
                                    }
                                    //Xóa bảng class 
                                    BaseRequest.delete("class/any", this.class_code)
                                    .then(response => {
                                        console.log(response.data);
                                    })
                                }
                            
                            })
                            //Bảng đăng ký 
                            BaseRequest.delete("enrollment/any", this.enrollment_code)
                            .then(response => {
                                console.log(response.data); 
                            })
                        }
                        //Bảng khóa học 
                        BaseRequest.delete("teacher_course/any", this.teacher_course_code)
                        .then(response => {
                            console.log(response.data); 
                        })

                        //Bảng teacher 
                        BaseRequest.delete("teacher?code=" + item.teacherCode)
                        .then(response => {
                            console.log(response.data); 
                            document.getElementById("dialog_dangky_content").innerHTML = "Xóa giáo viên thành công."
                            document.getElementById("ad-dialog-delete").style.display = "none"; 
                            document.getElementById("dialog_suauser").style.opacity = 1; 
                        })
                    }
                    else {
                        //Bảng teacher 
                        BaseRequest.delete("teacher?code=" + item.teacherCode)
                        .then(response => {
                            console.log(response.data); 
                            document.getElementById("dialog_dangky_content").innerHTML = "Xóa giáo viên thành công."
                            document.getElementById("ad-dialog-delete").style.display = "none"; 
                            document.getElementById("dialog_suauser").style.opacity = 1; 
                        })
                    }
                })
            })
        }, 
        searchUser: function(){
            var x = document.getElementById("search-teacher").value; 
            for (const item of this.user) {
                if(item.teacherCode === x || item.teacherName === x){
                    this.tg.push(item); 
                }
            }
            this.user = this.tg; 
        }, 
    }
}
</script>
<style>
/**Style thong bao xoa */
.success_deluser {
    width: 500px; 
    height: 56px;
    display: flex; 
    justify-content: space-between;
    align-items: center;
    position: absolute; 
    top: 20px; 
    left: 30%; 
    background-color: #fff;
    border-radius: 4px;
    font-size: 1.25rem;
    padding-left: 24px; 
    padding-right: 24px; 
    box-shadow: rgba(0,0,0,0.078) 0px 3px 6px 0px;
}
.success_deluser_title {
    height: 100%;
    display: flex; 
    z-index: 1; 
    width: 100%; 
    align-items: center; 
    gap: 16px; 
}
.hover_user {
    bottom: 5px; 
}
.user-admin > table > tbody > tr {
    height: 66px;
}
#user_back {
    display: none; 
}
.dialog_delete {
    width: 500px; 
    /* height: 100px; */
    position: absolute; 
    top: 200px; 
    left: 35%; 
    background-color: #fff;  
    padding: 24px; 
    border-radius: 4px;
    box-shadow: rgba(0,0,0,0.078) 0px 3px 6px 0px; 
    opacity: 0;
    transition: ease-out 0.3s;
    z-index: 2;
}
.icon_cauhoi {
    background: url('@/assets/Sprites.64af8f61.svg') no-repeat -752px -462px;
	width: 36px;
	height: 36px;
}
/* .dialog-delete-title {
    display: flex; 
    height: 100%;
    align-items: center;
    gap: 16px; 
    font-size: 1.25rem; 
} */
.dialog-delete-title-close {
    width: calc(100% - 24px -24px); 
    display: flex; 
    justify-content: space-between;
    margin: auto; 
}
.dialog-delete-title-close > div:first-child {
    font-size: 1.5rem;
    font-weight: 700; 
}
.dialog_delete-title {
    display: flex; 
    gap: 16px; 
    font-size: 1.25rem; 
    margin-top: 24px; 
}
.dialog-delete-btn {
    display: flex; 
    justify-content: flex-end;
    margin-top: 24px; 
    border-top: 1px solid #d0d0d0;
}
.dialog-delete-btn > button {
    height: 36px;
    border: none; 
    background-color: #50B83C; 
    padding-left: 16px; 
    padding-right: 16px; 
    color: #fff; 
    border-radius: 4px; 
    margin-top: 24px; 
}
#dialog_xoauser {
    display: block; 
}
</style>

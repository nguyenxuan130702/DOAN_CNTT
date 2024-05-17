<template>
    <NavBar></NavBar>
    <Sidebar></Sidebar>
    <div class="course-teacher" id="course-file-teacher">
        <h1>Quản lý tài liệu lớp học</h1>
        <div class="a-course_table">
            <div class="a-course_table-title">
                <div class="a-course_table-title-search">
                    <input type="text" placeholder="Tìm kiếm theo mã, chủ đề khóa học" id="search-course">
                    <div class="search-course" v-on:click="searchCourse()">
                        <div class="icon_search"></div>
                    </div>
                </div>
                <div class="a-course_table-title-btn">
                    <div class="icon_loaddata_sty">
                        <div class="icon_loaddata" v-on:click="loadDataCourse()"></div>
                    </div>
                </div>
            </div>
            <div class="a-course_table-content">
                <table>
                    <thead>
                        <tr>
                            <td style="width: 100px;">Mã lớp học</td>
                            <td style="width: 500px">Tên lớp học</td>
                            <td style="width: 200px">Thời gian bắt đầu</td>
                            <td >Lịch học</td>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-for="(item, index) in course" :key="index">
                            <td style="width: 100px;">{{ item.teacher_CourseCode }}</td>
                            <td style="width: 500px">{{ item.teacher_CourseName }}</td>
                            <td style="width: 200px">{{ renderDate(item.startTime)}}</td>
                            <td style="text-align: center" class="xemchitiet" v-on:click="openClassFile(item)">
                                <button>Xem</button>
                            </td>
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

    <div class="course-teacher" id="class-file-teacher">
        <div class="hocsinh-teacher-title">
            <h1>Danh sách buổi học</h1>
            <div class="lichhoa_back" v-on:click="backFile1()">
                <div class="icon_backup"></div>
                <div>Quay lại</div>
            </div>
        </div>
        <div class="a-course_table">
            <div class="a-course_table-title">
                <div class="a-course_table-title-search">
                    <input type="text" placeholder="Tìm kiếm theo mã, chủ đề khóa học" id="search-course">
                    <div class="search-course" v-on:click="searchCourse()">
                        <div class="icon_search"></div>
                    </div>
                </div>
                <div class="a-course_table-title-btn">
                    <div class="icon_loaddata_sty">
                        <div class="icon_loaddata" v-on:click="loadDataCourse()"></div>
                    </div>
                </div>
            </div>
            <div class="a-course_table-content">
                <table>
                    <thead>
                        <tr>
                            <td style="width: 100px;">Mã buổi học</td>
                            <td style="width: 500px">Nội dung mô tả</td>
                            <td style="width: 200px">Thời gian học</td>
                            <td >Tài liệu</td>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-for="(item, index) in classList" :key="index">
                            <td style="width: 100px;">{{ item.classCode }}</td>
                            <td style="width: 500px">{{ item.className }}</td>
                            <td style="width: 200px; text-align: center" class="hocsinh">
                                {{ item.time }} {{ renderDate(item.date)}}
                            </td>
                            <td style="text-align: center" class="xemchitiet" v-on:click="openFileBTVN(item)">
                                <button>Xem</button>
                            </td>
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


    <div class="course-teacher" id="tailieu-file-teacher">
        <div class="hocsinh-teacher-title">
            <h1>Danh sách File BTVN buổi 1</h1>
            <div class="lichhoa_back" v-on:click="backFile2()">
                <div class="icon_backup"></div>
                <div>Quay lại</div>
            </div>
        </div>
        <div class="a-course_table">
            <div class="a-course_table-title">
                <div class="a-course_table-title-search">
                    <input type="text" placeholder="Tìm kiếm theo mã, chủ đề khóa học" id="search-course">
                    <div class="search-course" v-on:click="searchCourse()">
                        <div class="icon_search"></div>
                    </div>
                </div>
                <div class="a-course_table-title-btn">
                    <button v-on:click="openFormThemMoi()">Thêm mới</button>
                    <div class="icon_loaddata_sty">
                        <div class="icon_loaddata" v-on:click="loadDataCourse()"></div>
                    </div>
                </div>
            </div>
            <div class="a-course_table-content">
                <table>
                    <thead>
                        <tr>
                            <td style="width: 300px;">Tên file</td>
                            <td style="width: 300px">Hạn nộp</td>
                            <td style="width: 200px">Chỉnh sửa</td>
                            <td >Xóa</td>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-for="(item, index) in fileList" :key="index">
                            <td style="width: 300px; text-align: center;">
                                <a :href="item.assignmentLink">{{ item.assignmentName }}</a>
                            </td>
                            <td style="width: 300px">{{ renderHours(item.deadline)}} ngày {{ renderDate(item.deadline)}}</td>
                            <td style="width: 200px; text-align: center" class="hocsinh" v-on:click="suaFileTheoMa(item)">
                                <button>Sửa</button>
                            </td>
                            <td style="text-align: center" class="xemchitiet" v-on:click="xoaFileTheoMa(item)">
                                <button>Xóa</button>
                            </td>
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




    <!--Form thêm mới-->
    <div class="them-file" id="them-file">
        <div class="them-file-title">
            <h1>Thêm mới tài liệu</h1>
            <p id="mmm"></p>
            <div class="icon_close" v-on:click="closeSuaFile()"></div>
        </div>
        <div class="them-file-input">
            <div>
                <label for="">
                    <b>Tên tài liệu: </b> <br>
                    <input type="text" v-model="ten" id="file-name">
                </label> <br>
                <label for="">
                    <b>Tải file lên: </b> <br>
                    <input type="file" style="border: none;" @change="checkFileUploadAdmin" id="file-upload">
                </label> <br>
                <label for="">
                    <b>Hạn nộp</b> <br>
                    <input type="datetime-local" v-model="deadline" id="time-file">
                </label>
            </div>
            <div>
                <label for="">
                    <b>Mô tả: </b> <br>
                    <textarea name="" id="mota-file" cols="30" rows="10" v-model="mota"></textarea>
                </label>
            </div>
        </div>
        <div class="them-file-buttom">
            <button v-on:click="themMoiFile()">Thêm mới</button>
        </div>
    </div>


    <!--Form sửa ---> 
    <div class="them-file" id="sua-file">
        <div class="them-file-title">
            <h1>Sửa tài liệu</h1>
            <p id="mmm"></p>
            <div class="icon_close" v-on:click="closeSuaFile()"></div>
        </div>
        <div class="them-file-input">
            <div>
                <label for="">
                    <b>Tên tài liệu: </b> <br>
                    <input type="text" v-model="uten" id="ufile-name">
                </label> <br>
                <label for="">
                    <a :href="link_ufile" target="_blank">File tài liệu cũ</a> <br>
                    <b>Tải file lên: </b> <br>
                    <input type="file" style="border: none;" @change="checkFileUploadUFile" id="ufile-upload">
                </label> <br>
                <label for="">
                    <b>Hạn nộp</b> <br>
                    <input type="datetime-local" v-model="udeadline" id="utime-file">
                </label>
            </div>
            <div>
                <label for="">
                    <b>Mô tả: </b> <br>
                    <textarea name="" id="umota-file" cols="30" rows="10" v-model="umota"></textarea>
                </label>
            </div>
        </div>
        <div class="them-file-buttom">
            <button v-on:click="suaFile()">Cập nhật</button>
        </div>
    </div>

    <CanhBao></CanhBao>

    <div class="course_back" id="file_back"></div>
</template>
<script>
import NavBar from '@/components/teacher/layout/NavbarTeacher.vue'
import Sidebar from '@/components/teacher/layout/SidebarTeacher.vue'
import BaseRequest from '@/core/BaseRequest'
import CanhBao from '@/components/admin/layout/DialogCanhbao.vue'
export default {
    components: {
        NavBar, 
        Sidebar,
        CanhBao
    }, 
    data(){
        return {
            course: [], 
            classList: [], 
            teacher: {}, 
            fileList: [], 
            x: {}, 
            getClass: {}, 
            getFile: {}, 


            /**Biến cho hàm thêm */
            ten: '', 
            mota: '', 
            deadline: '', 
            file: '', 
            isPdf: null, 

            /**Biến cho form sửa */
            uten: '', 
            umota: '', 
            udeadline: '', 
            ufile: '', 
            uisPdf: null, 
            link_ufile: "", 
            updateFile: {}, 
            deleteFile: {}, 
        }
    }, 
    mounted(){
        this.teacher = JSON.parse(localStorage.getItem("teacherLogin"))
        BaseRequest.get("teacher_course/teacher?teacherId=" + this.teacher.teacherId)
        .then(response => {
            this.course = response.data; 
            console.log(response.data); 
        })
        .catch(error => {
            console.log(error.message); 
        })
    }, 
    methods: {
        /**
         * Hàm mở form thêm mới
         */
        openFormThemMoi: function(){
            document.getElementById("them-file").style.display = "block"; 
            document.getElementById("file_back").style.display = "block"; 
        }, 
        /**
         * Hàm quay lại màn hình danh sách buổi học
         */
        backFile1: function(){
            document.getElementById("course-file-teacher").style.display = "block";
            document.getElementById("class-file-teacher").style.display = "none";
            document.getElementById('tailieu-file-teacher').style.display = "none"; 
        }, 
        /**
         * Hàm quay lại màn hình danh sách lớp học
         */
        backFile2: function(){
            document.getElementById("course-file-teacher").style.display = "none";
            document.getElementById("class-file-teacher").style.display = "block";
            document.getElementById('tailieu-file-teacher').style.display = "none"; 
        }, 
        /**
         * Hàm mở danh sách lớp học 
         */
        openClassFile: function(item){
            document.getElementById("course-file-teacher").style.display = "none";
            document.getElementById("class-file-teacher").style.display = "block"; 
            document.getElementById('tailieu-file-teacher').style.display = "none"; 
            BaseRequest.get("class/teacher_course?teacher_CourseId=" + item.teacher_CourseId)
            .then(response => {
                this.classList = response.data; 
            })
            .catch(error => {
                console.log(error.message); 
            })
        }, 
        /**
         * Hàm mở danh sách file 
         */
        openFileBTVN: function(item){
            this.getClass = item; 
            document.getElementById("course-file-teacher").style.display = "none";
            document.getElementById("class-file-teacher").style.display = "none";
            document.getElementById("tailieu-file-teacher").style.display = "block"; 
            BaseRequest.get("assignment/class?classId=" + item.classId)
            .then(response => {
                this.fileList = response.data; 
            })
            .catch(error => {
                console.log(error.message); 
            })
        }, 
        renderDate: function(item){
            let dateObject = new Date(item);

            let formattedDate = `${dateObject.getDate()}/${dateObject.getMonth() + 1}/${dateObject.getFullYear()}`;

            return formattedDate; 
        }, 
        renderHours: function(item){
            let dateObject = new Date(item); 
            let gio = dateObject.getHours(); 
            let phut = dateObject.getMinutes(); 
            let giay = dateObject.getSeconds(); 
            return gio + " giờ " + phut + " phút " + giay; 
        }, 


        //Dành cho form thêm mới 
        closeThemMoi: function(){
            document.getElementById("them-file").style.display = "none"; 
            document.getElementById("file_back").style.display = "none";
        }, 
        themMoiFile: function(){
            const input1 = document.getElementById("file-name"); 
            const input2 = document.getElementById("time-file"); 
            const input3 = document.getElementById("mota-file"); 
            const input4 = document.getElementById("file-upload"); 

            const _delete = document.querySelectorAll(".error-file"); 
            this.isCheck = true;
            for (const item of _delete) {
                item.parentNode.removeChild(item);
            }

            input1.style.borderColor = "#d0d0d0"; 
            input2.style.borderColor = "#d0d0d0"; 
            input3.style.borderColor = "#d0d0d0";
            input4.style.borderColor = "#d0d0d0"; 

            var isCheck = true; 
            
            if(this.ten === "" || this.ten === undefined || this.ten === null){
                this.thongbao(input1, "Tên file không được để trống."); 
                isCheck = false; 
            }

            if(this.file === "" || this.file === undefined || this.file === null){
                this.thongbao(input4, "File upload không được để trống."); 
                isCheck = false; 
            }
            const date = new Date(); 

            if(this.renderDate(this.deadline) < this.renderDate(date)){
                this.thongbao(input2, "Hạn nộp phải sau ngày hôm nay."); 
                isCheck = false; 
            }


            if (this.file) {
                const fileType = this.file.type;
                this.isPdf = fileType === 'application/pdf';
            } else {
                this.isPdf = false;
            }

            if(this.isPdf === null){
                this.thongbao(input4, "File upload không đúng định dạng Pdf."); 
                isCheck = false;
            }

            if(isCheck){
                //Tiến hành thêm vào api 
                const formData = new FormData(); 

                formData.append("AssignmentCode", "string");
                formData.append("AssignmentName", this.ten); 
                formData.append("Deadline", this.deadline); 
                formData.append("Description", this.mota); 
                formData.append("ClassId", this.getClass.classId); 
                formData.append("File", this.file);  

                BaseRequest.post("assignment/FilePdf", formData)
                .then(response => {
                    console.log(response.data); 
                    alert("Thêm mới tài liệu cho buổi học " + this.getClass.classCode + " thành công!"); 
                    document.getElementById("them-file").style.display = "none"; 
                    document.getElementById("file_back").style.display = "none"; 
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
            p.classList.add("error-file");
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
            this.isPdf = null; 
        },
        checkIfPdf() {
            if (this.file) {
                const fileType = this.file.type;
                this.isPdf = fileType === 'application/pdf';
            } else {
                this.isPdf = false;
            }
        },


        //Dành cho form sửa 
        suaFileTheoMa: function(item){
            this.getFile = item; 
            document.getElementById("sua-file").style.display = "block"; 
            document.getElementById("file_back").style.display = "block";
            this.uten = this.getFile.assignmentName; 
            this.umota = this.getFile.description; 
            this.udeadline = this.getFile.deadline; 
            this.link_ufile = this.getFile.assignmentLink; 
        }, 
        closeSuaFile: function(){
            document.getElementById("sua-file").style.display = "none"; 
            document.getElementById("file_back").style.display = "none";
        }, 
        thongbao1: function(item, mes){
            item.style.borderColor = "red"; 
            let p = document.createElement("p"); 
            let node = document.createTextNode(mes); 
            p.appendChild(node); 
            p.classList.add("error-update-file");
            p.style.color = "red"; 
            p.style.fontStyle = "italic"; 
            p.style.fontSize = "0.9rem"; 
            item.after(p);
            //item.setAttribute("title", mes); 
        },
        checkFileUploadUFile(event){
            this.ufile = event.target.files[0]; 
            if(this.ufile){
                //đọc URL của file và gán vào imageUrl để hiển thị 
                const reander = new FileReader();  
                reander.readAsDataURL(this.file); 
            }
            this.uisPdf = null; 
        },
        suaFile: function(){
            const input1 = document.getElementById("ufile-name"); 
            const input2 = document.getElementById("utime-file"); 
            const input3 = document.getElementById("umota-file"); 
            const input4 = document.getElementById("ufile-upload"); 


            const _delete = document.querySelectorAll(".error-update-file"); 
            this.isCheck = true;
            for (const item of _delete) {
                item.parentNode.removeChild(item);
            }

            input1.style.borderColor = "#d0d0d0"; 
            input2.style.borderColor = "#d0d0d0"; 
            input3.style.borderColor = "#d0d0d0";
            input4.style.borderColor = "#d0d0d0"; 

            var isCheck = true; 
            if(this.uten === "" || this.uten === undefined || this.uten === null){
                this.thongbao1(input1, "Tên file không được để trống."); 
                isCheck = false; 
            }

            const date = new Date(); 

            if(this.renderDate(this.deadline) < this.renderDate(date)){
                this.thongbao(input2, "Hạn nộp phải sau ngày hôm nay."); 
                isCheck = false; 
            }


            if (this.ufile) {
                const fileType = this.file.type;
                this.uisPdf = fileType === 'application/pdf';
            } else {
                this.uisPdf = false;
            }

            if(this.uisPdf === null){
                this.thongbao(input4, "File upload không đúng định dạng Pdf."); 
                isCheck = false;
            }

            if(isCheck){
                this.updateFile = this.getFile; 
                //update file 
                if(this.ufile === null || this.ufile === undefined || this.ufile === ""){
                    //goi api binh thuong 
                    this.updateFile.assignmentName = this.uten; 
                    this.updateFile.deadline = this.udeadline; 
                    this.updateFile.description = this.umota; 

                    BaseRequest.put("assignment", this.updateFile)
                    .then(response => {
                        console.log(response.data); 
                        alert("Đã cập nhật thành công tài liệu có mã " + this.updateFile.assignmentCode + "!"); 
                        document.getElementById("sua-file").style.display = "none"; 
                        document.getElementById("file_back").style.display = "none";
                    })
                    .catch(error => {
                        console.log(error.message); 
                    })
                }
                else {
                    //goi api upload anh 
                }
            }
        }, 


        /**Xóa thông tin */
        xoaFileTheoMa: function(item){
            this.deleteFile = item; 
            document.getElementById("ad-dialog-delete").style.display = "block"; 
            document.getElementById('thognbao').innerHTML = "Bạn có chắc chắn muốn xóa tài liệu có mã " + item.assignmentCode; 

            document.getElementById("ad-dialog-btn").addEventListener("click", function(){
                //Tiến hành xóa document. 
            })
        }

    }
}
</script>
<style scoped>
#sua-file {
    display: none; 
}
#tailieu-file-teacher {
    display: none; 
}
#class-file-teacher {
    display: none; 
}
.hocsinh-teacher-title {
    display: flex; 
    width: 100%; 
    justify-content: space-between;
    padding-top: 24px; 
    padding-left: 24px; 
    padding-right: 24px; 
}
.lichhoa_back {
    display: flex; 
    align-items: center;
    margin-left: 16px; 
    margin-bottom: -16px; 
}
.lichhoa_back:hover {
    text-decoration: underline;
    cursor: pointer;
}
.icon_backup {
    background: url('@/assets/math/back-square-svgrepo-com.svg') no-repeat -5px -5px;
	width: 54px;
	height: 54px;
    transform: scale(0.6);
}
.course-teacher {
    width: calc(100% - 250px); 
    float: left;
    background-color: #f5f6f7; 
    height: calc(100vh - 56px);
    display: block; 
}
.course-teacher > h1 {
    padding-top: 24px; 
    padding-left: 24px; 
    font-weight: 700;
}
.xemchitiet > button {
    background-color: orange; 
    padding: 8px; 
    border: none; 
    color: #fff; 
    border-radius: 6px;
}
.hocsinh > button {
    padding: 8px; 
    background-color: blue; 
    border: none; 
    color: #fff; 
    border-radius: 6px;
}
.xoalophoc > button {
    padding: 8px; 
    background-color: red; 
    border: none; 
    color: #fff; 
    border-radius: 6px;
}
#hocsinh-teacher {
    width: calc(100% - 250px); 
    float: left;
    background-color: #f5f6f7; 
    height: calc(100vh - 56px);
    display: none; 
}
.lichhoc > button {
    background-color: blue; 
    padding: 8px; 
    border: none; 
    color: #fff; 
    border-radius: 6px;
} 


/**----------------------------------------Form thêm mới */
.them-file {
    position: absolute;
    top: 100px; 
    left: 30%; 
    background-color: #fff; 
    z-index: 1;
    width: 50%; 
    display: none;
}
.them-file-title {
    display: flex; 
    justify-content: space-between;
    align-items: center;
    padding: 24px; 
}
.them-file-title > h1 {
    font-weight: 700;
}
.them-file-input {
    display: flex; 
    justify-content: space-between;
    gap: 16px; 
    padding-left: 24px; 
    padding-right: 24px; 
}
.them-file-input > div {
    width: 48%; 
}
.them-file-input > div > label {
    width: 100%; 
}
.them-file-input > div > label > input {
    width: 100%; 
    height: 36px;
    border: 1px solid #d0d0d0; 
    outline: none; 
    padding-left: 8px; 
    margin-top: 8px; 
    margin-bottom: 16px;
}
.them-file-input > div > label > input:focus {
    border: 1px solid #50B83C;  
}
.them-file-input > div > label > textarea {
    border: 1px solid #d0d0d0; 
    padding-left: 8px;
    margin-top: 8px;  
    width: 100%; 
    outline: none;
}
.them-file-input > div > label > textarea:focus {
    border: 1px solid #50B83C; 
}
.them-file-buttom {
    background-color: #d0d0d0; 
    padding-top: 16px; 
    padding-bottom: 16px; 
    padding-right: 24px; 
    display: flex;
    justify-content: flex-end;
    margin-top: 16px; 
}
.them-file-buttom > button {
    padding: 8px; 
    border: none; 
    background-color: #50B83C; 
    color: #fff; 
    border-radius: 6px;
}
</style>

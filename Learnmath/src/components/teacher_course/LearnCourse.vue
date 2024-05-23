<template>
    <div class="learn_course">
        <h1>{{teacher_course.teacher_CourseName}}</h1>
        <p>{{ teacher_course.teacher_CourseDescription }}</p>
        <h1>Bài học</h1>
        <div class="baihoc">
            <div v-for="(item, index) in classList" :key="index" class="tieude-lophoc">
                <div class="o-lesson-course" v-on:click="openChiTiet(index, item)">
                    <div class="o-lesson-title">
                        <div class="icon_tick" v-if="check_tick[index]"></div>
                        <div class="icon_untick" v-else></div>
                        <h2>{{ item.className }}</h2>
                    </div>
                    <div class="o-lesson-menu">
                        <div class="icon_down"></div>
                        <div class="icon_up"></div>
                    </div>
                    <div class="o-leson-back"></div>
                </div>
                <div class="learn-course-detail hidden" :id="item.classId">
                    <div class="o-less_question-sty">
                        <div class="task-course">
                            <div>
                                <div class="icon_link"></div>
                                <div>
                                    <a :href="item.classLink" target="_blank">Lớp học trực tuyến</a>
                                </div>
                            </div>
                            <div>Thời gian học: {{ item.time }} {{ renderDate(item.date)}}</div>
                            <div>
                                <p v-if="sosanh_date[index]" style="color: green;">Đã học</p>
                                <p v-else style="color: red;">Chưa học</p>
                            </div>
                        </div>
                        <hr>
                        <div class="task-course">
                            <div>
                                <div class="icon_document"></div>
                                <div>
                                    <a :href="item.documentLink" target="_blank">File tài liệu học tập</a>
                                </div>
                            </div>
                        </div>
                        <hr>
                        <div class="task-course">
                            <div>
                                <div class="icon_homework"></div>
                                <div>Phiếu giao bài tập</div>
                            </div>
                            <div v-for="(item1, index1) in assignment[index]" :key="index1" style="margin-bottom: 24px;">
                                <div class="task-course-homework">
                                    <p>
                                        <a :href="item1.assignmentLink" target="_blank">{{ item1.assignmentName }}</a>
                                    </p>
                                    <p>Hạn nộp: {{renderHours(item1.deadline)}} {{ renderDate(item1.deadline)}}</p>
                                    <p v-if="!nopbai[item1.assignmentId] && renderDate(item1.deadline) < renderDate(tg)" style="color: red;">Đã quá hạn</p>
                                    <p v-else-if="item1.deadline < tg_nop_bai[item1.assignmentId]" style="color: yellow;">Đã nộp muộn</p>
                                    <p v-else-if="renderDate(item1.deadline) > renderDate(tg) && !nopbai[item1.assignmentId]">Chưa nộp</p>
                                    <p v-else style="color: green;">Đã nộp sớm</p>
                                </div>
                                <div class="task-course-table">
                                    <h3>Trạng thái nộp bài của phiếu {{ item1.assignmentName }}</h3>
                                    <table>
                                        <tbody>
                                            <tr>
                                                <td>Trạng thái nộp bài</td>
                                                <td>
                                                    <p v-if="nopbai[item1.assignmentId]">Đã nộp mới nhất vào lúc {{ renderHours(tg_nop_bai[item1.assignmentId])}} ngày {{ renderDate(tg_nop_bai[item1.assignmentId])}}</p>
                                                    <p v-else>Chưa nộp bài</p>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Trạng thái chấm điểm</td>
                                                <td>
                                                    <p v-if="result[item1.assignmentId]">
                                                        {{ result[item1.assignmentId].score }} điểm
                                                    </p>
                                                    <p v-else>Chưa chấm</p>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Nhận xét của giáo viên</td>
                                                <td>
                                                    <p v-if="result[item1.assignmentId]">
                                                        {{ result[item1.assignmentId].feedback }}
                                                    </p>
                                                    
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>File bài làm</td>
                                                <td>
                                                    <p v-if="nopbai[item1.assignmentId]">
                                                        <a :href="link_bai_lam[item1.assignmentId]" target="_blank">Link file bài làm</a>
                                                    </p>
                                                    <p v-else>Chưa có</p>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                    <button v-on:click="nopBai(item1, index, index1)">Nộp bài</button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>



    <div class="nopbai" id="nopbai">
        <div class="nopbai-content">
            <div class="nopbai-title">
                <h2>Nộp bài tập về nhà</h2>
                <div class="icon_close" v-on:click="closeNopBai()"></div>
            </div>
            <div class="nopbai-file">
                <b>Đính kèm: </b> <br>
                <input type="file" id="filesubmit" name="filesubmit" @change="checkFileUploadNew">
            </div>
            <div class="nopbai-tacgia">
                <b>Tác giả</b> <br>
                <input type="text" v-model="nguoinop">
            </div>
            <div>
                <button v-on:click="dangTai()">Đăng tệp này</button>
            </div>
        </div>
        <div class="back-nopbai"></div>
    </div>
</template>
<script>
import BaseRequest from '@/core/BaseRequest';
export default{
    data(){
        return {
            userLogin: {}, 
            teacher_course: {},
            classList: [],  
            assignment:  Array(100).fill(Array(100)), 
            chitiet: [], 
            check_nop_bai: [], 
            nopbai:  [], 
            sosanh_date: [], 
            nguoinop: "", 
            pushSubmit: {}, 
            submit_assignment: {}, 
            index1: 0, 
            index2: 0, 
            file: '', 
            link_bai_lam: [], 
            tg_nop_bai: [], 
            check_tick: [], 
            tg: new Date(), 

            //result 
            result: [], 
        }
    }, 
    mounted(){
        this.userLogin = JSON.parse(localStorage.getItem("userLogin")); 
        this.teacher_course = JSON.parse(localStorage.getItem("teacher_course")); 
        console.log("teacer-course"); 
        console.log(this.teacher_course); 

        const now = new Date(); 
        let hours = now.getHours();
        let minutes = now.getMinutes();
        let seconds = now.getSeconds();

        if(hours < 10){
            hours = '0' + hours; 
        }
        if(minutes < 10){
            minutes = '0' + minutes; 
        }
        if(seconds < 10){
            seconds = '0' + seconds; 
        }
        let time_now = `${hours}:${minutes}:${seconds}`; 

        BaseRequest.get("class/teacher_course?teacher_CourseId=" + this.teacher_course.teacher_CourseId)
        .then(response => {
            this.classList = response.data;
            this.assignment = Array(this.classList.length).fill([]);
            if (this.classList.length > 0) {
                for (var j = 0; j < this.classList.length; j++) {
                    if(this.renderDate(now) > this.renderDate(this.classList[j].date)){
                        this.sosanh_date[j] = true; 
                        this.check_tick[j] = true; 
                    }
                    else {
                        if(this.renderDate(now) === this.renderDate(this.classList[j].date)){
                            if(time_now > this.classList[j].time){
                                this.sosanh_date[j] = true; 
                                this.check_tick[j] = true; 
                            }
                        }
                        else {
                            this.check_tick[j] = false; 
                        }
                    }
                    //let dem = 0;
                    BaseRequest.get("assignment/class?classId=" + this.classList[j].classId)
                        .then(response => {
                            this.assignment[j] = response.data; 
                            let assignments = response.data;

                            if (assignments.length > 0) {
                                for (var i = 0; i < assignments.length; i++) {
                                    let assignmentId = assignments[i].assignmentId;
                                    BaseRequest.get("submit/submit_user_assignment?userId=" + this.userLogin.userId + "&assignmentId=" + assignmentId)
                                    .then(response => {
                                        let check_nop_bai = response.data;
                                        if (check_nop_bai.length > 0) {
                                            this.nopbai[assignmentId] = true;
                                            this.link_bai_lam[assignmentId] = check_nop_bai[0].submitLink;
                                            this.tg_nop_bai[assignmentId] = check_nop_bai[0].date;
                                        } else {
                                            this.nopbai[assignmentId] = false;
                                        }
                                    }); 

                                    BaseRequest.get("result/user_assignment?userId=" + this.userLogin.userId + "&assignmentId=" + assignmentId)
                                    .then(response => {
                                        this.result[assignmentId] = response.data; 
                                    })
                                    .catch(error => {
                                        console.log(error.message); 
                                    })
                                }  
                            }
                        });
                }
            }
        })
        .catch(error => {
            console.log(error.message);
        });
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
        nopBai: function(item, index1, index2){
            document.getElementById("nopbai").style.display = "block"; 
            //Goi api de nop bai 
            this.nguoinop = this.userLogin.userName; 
            this.submit_assignment = item; 
            this.index1 = index1; 
            this.index2 = index2; 
        }, 
        closeNopBai: function(){
            document.getElementById("nopbai").style.display = "none"; 
            localStorage.removeItem("submit"); 
            localStorage.removeItem("index_submit"); 
        }, 
        dangTai: function(){
            this.pushSubmit = this.submit_assignment; 
            console.log(this.pushSubmit); 
            const inputSubmit = document.getElementById("filesubmit"); 
            //const index = JSON.parse(localStorage.getItem("index_submit")); 

            const _delete = document.querySelectorAll(".error-submit"); 
            this.isCheck = true;
            for (const item of _delete) {
                item.parentNode.removeChild(item);
            }

            var isCheck = true; 
            //thêm vào database 
            if(this.file === null || this.file === undefined || this.file === ""){
                this.thongbao(inputSubmit, "File upload không được để trống."); 
                isCheck = false; 
            }

            if(isCheck){
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

                console.log(date_new); 
                //Thêm vào database 
                const formData = new FormData(); 
                formData.append("SubmitCode", "string"); 
                formData.append("AssignmentId", this.pushSubmit.assignmentId); 
                formData.append("UserId", this.userLogin.userId); 
                formData.append("Date", date_new); 
                formData.append("File", this.file); 

                
                console.log("AssignmentId" + this.pushSubmit.assignmentId); 
                console.log("UserId" + this.userLogin.userId); 
                console.log("Date" + date_new); 
                console.log("File" + this.file); 

                BaseRequest.post("submit/FilePdf", formData)
                .then(response => {
                    console.log(response.data); 
                    //thông báo thành công
                    alert("Nộp bài thành công cho bài " + this.pushSubmit.className); 
                    document.getElementById("nopbai").style.display = "none"; 
                    //this.nopbai[index] = true; 
                    //this.nopbai[this.index1][this.index2] = true; 

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
            p.classList.add("error-submit");
            p.style.color = "red"; 
            p.style.fontStyle = "italic"; 
            p.style.fontSize = "0.9rem"; 
            item.after(p);
            //item.setAttribute("title", mes); 
        },
        openChiTiet: function(index, item){
            this.chitiet[index] = !this.chitiet[index];
            // //Lấy danh sách các BTVN đã 

            // Gửi yêu cầu AJAX để lấy danh sách các tài liệu cho buổi học item
            BaseRequest.get("assignment/class?classId=" + item.classId)
            .then(response => {
                this.assignment[index] = response.data; 
                let assignment = response.data; 
                if (assignment.length > 0) {
                    for (var i = 0; i < assignment.length; i++) {
                        let assignmentId = assignment[i].assignmentId;
                        BaseRequest.get("submit/submit_user_assignment?userId=" + this.userLogin.userId + "&assignmentId=" + assignment[i].assignmentId)
                        .then(response => {
                            this.check_nop_bai = response.data; 
                            console.log(this.check_nop_bai);  
                            if (this.check_nop_bai.length > 0) {
                                this.nopbai[assignmentId] = true; 
                                this.link_bai_lam[assignmentId] = this.check_nop_bai[0].submitLink;
                                this.tg_nop_bai[assignmentId] = this.check_nop_bai[0].date; 

                            }
                            else {
                                this.nopbai[assignmentId] = false; 
                            }
                        }) 
                    }
                } 
            })

            var hiddenContent = document.getElementById(`${item.classId}`);
            if (hiddenContent.classList.contains('hidden')) {
                hiddenContent.style.display = 'block';
                setTimeout(function() {
                    hiddenContent.style.height = hiddenContent.scrollHeight + 'px';
                }, 100); // Delay to allow the display property to take effect before changing height
                hiddenContent.classList.remove('hidden');
            } else {
                hiddenContent.style.height = '0';
                setTimeout(function() {
                    hiddenContent.style.display = 'none';
                }, 500); // Match the duration of the transition
                hiddenContent.classList.add('hidden');
            }

            //Lấy file bài  làm mới nhất. 


        }, 
        renderDate: function(item){
            let dateObject = new Date(item);

            // Format dateObject to desired date format
            let formattedDate = `${dateObject.getDate()}/${dateObject.getMonth() + 1}/${dateObject.getFullYear()}`;

            // Assign the formatted date to thoiGian
            return formattedDate; 
        }, 
        renderHours: function(item){
            let dateObject = new Date(item); 
            let gio = dateObject.getHours(); 
            let phut = dateObject.getMinutes(); 
            let giay = dateObject.getSeconds(); 
            return gio + " giờ " + phut + " phút " + giay; 
        }, 
        checkNopBai: function(item){
            BaseRequest.get("submit/submit_user_assignment?userId=" + this.userLogin.userId + "&assignmentId=" + item.assignmentId)
            .then(response => {
                this.check_nop_bai = response.data; 
                // if(this.check_nop_bai === 0){
                //     this.nopbai[] = true; 
                // }
                // else {
                //     this.nopbai[index] = false; 
                // }
            })
        }
    }
}
</script>
<style scoped>
.learn_course {
    font-size: 1.25rem; 
}
.nopbai {
    position: absolute; 
    top: 0; 
    left: 0; 
    z-index: 1; 
    width: 100%; 
    height: 100vh;
    display: none;
    position: fixed; 
}
.back-nopbai {
    position: absolute; 
    top: 0px; 
    left: 0px; 
    width: 100%; 
    background-color: #0a0a23; 
    opacity: 0.3;
    height: 100vh;
}
.nopbai-title {
    display: flex; 
    align-items: center;
    justify-content: space-between;
    background-color: #0a0a23; 
    padding-top: 16px; 
    padding-bottom: 16px; 
    padding-left: 24px;
    padding-right: 24px;  
    color: #fff; 
}
.nopbai-content {
    position: absolute;
    width: 500px; 
    z-index: 1; 
    background-color: #fff; 
    margin-top: 100px; 
    top: 20%; 
    left: 35%; 
}
.nopbai-content > div:last-child {
    margin-top: 24px;
    text-align: center;
}
.nopbai-content > div:last-child > button{
    padding: 8px; 
    border: none; 
    background-color:#50B83C; 
    color: #fff; 
    border-radius: 4px; 
    margin-bottom: 24px;  
}
.nopbai-tacgia {
    padding-left: 24px; 
    padding-right: 24px; 
}
.nopbai-file {
    padding-left: 24px; 
    padding-right: 24px; 
    margin-top: 24px; 
    margin-bottom: 24px; 
}
.nopbai-tacgia > input {
    width: 100%; 
    margin-top: 8px; 
    height: 36px;
    border: 1px solid #d0d0d0; 
    outline: none; 
    border-radius: 4px;
    padding-left: 8px; 
}
.nopbai-tacgia > input:focus {
    border: 1px solid #50B83C;
}
.hidden {
    display: none;
    height: 0;
    overflow: hidden;
    transition: height 0.5s ease-in-out;
}
.learn_course {
    padding-top: 100px; 
    width: 80%; 
    margin: auto; 
    text-align: center;
}
.icon_link {
    background: url('@/assets/math/link-alt-1-svgrepo-com.svg') no-repeat -4px -11px;
	width: 64px;
	height: 42px;
    transform: scale(0.4);
}
.icon_document {
    background: url('@/assets/math/document-medicine-svgrepo-com.svg') no-repeat -6px -3px;
	width: 64px;
	height: 58px;
    transform: scale(0.4);
}
.icon_homework {
    background: url('@/assets/math/task-assignment-task-homework-assignment-svgrepo-com.svg') no-repeat 0 0;
	width: 64px;
	height: 64px;
    transform: scale(0.4);
}
.task-course > div:first-child {
    display: flex; 
    align-items: center;
    gap: 24px; 
}
.task-course{
    display: flex; 
    align-items: center;
    justify-content: space-between;
}
.learn-course-detail {
    border: 5px solid #0a0a23; 
}
.task-course:last-child {
    display: flex; 
    flex-direction: column;
    align-items: flex-start;
}
.task-course:last-child > div:last-child {
    padding-left: 24px; 
}
.task-course-homework {
    display: flex; 
    gap: 100px; 
}
.task-course-table > button {
    margin-top: 24px; 
    padding: 8px; 
    background-color: blue; 
    border: none; 
    outline: none; 
    color: #fff; 
}
.tieude-lophoc {
    margin-top: 24px; 
}
.baihoc {
    padding-bottom: 100px;
}
.icon_tick {
    margin-left: 20px; 
}
.learn-course-detail {
    padding-bottom: 100px; 
}
</style>

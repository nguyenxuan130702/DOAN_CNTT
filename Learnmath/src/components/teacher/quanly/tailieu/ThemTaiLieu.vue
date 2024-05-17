<template>
    <div class="them-file" id="them-file">
        <div class="them-file-title">
            <h1>Thêm mới tài liệu cho buổi học</h1>
            <p id="mmm"></p>
            <div class="icon_close" v-on:click="closeThemMoi()"></div>
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
</template>
<script>
export default {
    props: ['propFileClass'], 
    data(){
        return {
            ten: '', 
            mota: '', 
            deadline: '', 
            file: '', 
            isPdf: null, 
            getClass: {}, 
            thaydoi: "", 
        }
    }, 
    mounted(){
        this.loadData();  
    }, 
    watch: {
        propFileClass: function(){
            this.ten= ''; 
            this.mota= ''; 
            this.deadline= '';  
            this.file= ''; 
            this.isPdf= null;
        }, 
        thaydoi: function(){
            this.getClass = JSON.parse(localStorage.getItem("classId")); 
        }
    }, 
    methods: {
        loadData: function(){
            this.getClass = JSON.parse(localStorage.getItem("classId")); 
            this.thaydoi = this.getClass.classId; 
            console.log("bugg2"); 
            console.log(this.getClass);
        }, 
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
                //formData.append("ClassId", this.getClass.classId); 
                formData.append("File", this.file);  
            }
            document.getElementById("mmm").innerHTML = this.getClass.classId; 

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
    }
}
</script>
<style scoped>
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
}
.them-file-input > div > label > textarea:focus {
    border: 1px solid #d0d0d0; 
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

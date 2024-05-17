<template>
    <div class="update_class" id="detail_class">
        <div class="update_class-title">
            <h1>Thông tin buổi học</h1>
            <div class="icon_close" v-on:click="closeDetailClass()"></div>
        </div>
        <div class="update_class-form">
            <div class="update_class-form1">
                <label for="">
                    <b>Nội dung mô tả: </b> <br>
                    <p>{{ ten }}</p>
                </label> <br>
                <label for="" class="update_class-time">
                    <b>Thời gian học: </b> <br>
                    <div>
                        {{ gio}} ngày {{ renderDate(ngay)}} <br>
                    </div>
                </label>
            </div>
            <div class="update_class-form2">
                <label for="">
                    <b>Link lớp học</b> <br>
                    <a :href="link_lop" target="_blank">Link lớp học</a>
                </label> <br>
                <label for="">
                    <b>Tài liệu tham khảo</b> <br>
                    <a :href="link_tailieu" target="_blank">Tài liệu tham khảo</a>
                </label>
            </div>
        </div>
    </div>
</template>
<script>
export default {
    props: ['propGetClass'], 
    data(){
        return {
            getClass: {}, 
            ten: "", 
            gio: "", 
            ngay: "", 
            link_lop: "",
            link_tailieu: "", 
        }
    }, 
    watch: {
        propGetClass: function(){
            this.getClass = this.propGetClass; 
            this.ten = this.propGetClass.className;
            this.gio = this.propGetClass.time; 
            this.ngay = this.propGetClass.date; 
            this.link_lop = this.propGetClass.classLink; 
            this.link_tailieu = this.propGetClass.documentLink; 
        }
    }, 
    mounted(){
        console.log("Thông tin lóp học");
        console.log(this.getClass);  
    }, 
    methods: {
        closeDetailClass: function(){
            document.getElementById("detail_class").style.display = "none"; 
            document.getElementById("course_back").style.display = "none"; 
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

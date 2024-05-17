<template>
    <div class="form_course" id="form_category">
        <div class="form_course_title">
            <h1>Thêm mới chủ đề</h1>
            <div class="icon_close" v-on:click="closeFormNew()"></div>
        </div>
        <div class="form_course-content-x">
            <div for="" class="a-new-content">
                <b>Tên chủ đề </b><br>
                <input type="text" placeholder="Tên chủ đề" id="a-category-chude" name="a-category-chude" v-model="tenchude">
            </div>
            <div class="a-new-content">
                <b>Mô tả </b><br>
                <textarea name="a-category-mota" id="a-category-mota" cols="30" rows="10" placeholder="Mô tả" v-model="motachude"></textarea>
            </div>
        </div>
        <div class="course_btn" v-on:click="validatePushCategory()">
            <button>Thêm mới</button>
        </div>
    </div>

    <!--Thông báo thêm mới thành công-->
    <div class="dialog_dangky" id="dialog_category" >
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
    props: ['propCategory'], 
    data(){
        return {
            tenchude: "",
            motachude: "",
            thread: {},
            index: 0, 
        }
    }, 
    watch: {
        propCategory: function(){
            this.index = this.propCategory.length + 1;  
        }
    }, 
    methods: {
        closeDialogLesson: function(){
            document.getElementById("dialog_category").style.opacity = 0; 
        }, 
        closeFormNew: function(){
            document.getElementById("form_category").style.display = "none"; 
            document.getElementById("category_back").style.display = "none"; 
        }, 
        validatePushCategory: function(){
            const inputTitle = document.getElementById("a-category-chude"); 
            const inputMota = document.getElementById("a-category-mota"); 

            const _delete = document.querySelectorAll(".error-category"); 
            this.isCheck = true;
            for (const item of _delete) {
                item.parentNode.removeChild(item);
            }

            inputTitle.style.borderColor = "#d0d0d0"; 
            inputMota.style.borderColor = "#d0d0d0"; 
            var isCheck = true; 

            if(this.tenchude === "" || this.tenchude === undefined || this.tenchude === null){
                this.thongbao(inputTitle, "Tên chủ đề không được để trống."); 
                isCheck = false;
            }

            if(this.motachude === "" || this.motachude === undefined || this.motachude === null){
                this.thongbao(inputMota, "Mô tả chủ đề không được để trống."); 
                isCheck = false; 
            }

            if(isCheck){
                this.index = this.propCategory.length + 1;  
                var code = "CD" + this.index; 
                this.thread.categoryCode = code; 
                this.thread.categoryName = this.tenchude; 
                this.thread.categoryDescription = this.motachude; 

                
                BaseRequest.post("category", this.thread)
                .then(response => {
                    console.log(response.data); 
                    //hiển thị thông báo thành công.
                    document.getElementById("form_category").style.display = "none"; 
                    document.getElementById("category_back").style.display = "none"; 
                    document.getElementById("dialog_category").style.opacity = 1;
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
            p.classList.add("error-category");
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
</style>

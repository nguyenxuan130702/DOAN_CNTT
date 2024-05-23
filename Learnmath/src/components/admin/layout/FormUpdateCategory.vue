<template>
    <div class="form_course" id="form_update_category">
        <div class="form_course_title">
            <h1>Sửa chủ đề</h1>
            <div class="icon_close" v-on:click="closeForm()"></div>
        </div>
        <div class="form_course-content-x">
            <div for="" class="a-new-content">
                <b>Tên chủ đề </b><br>
                <input type="text" placeholder="tên chủ đề" id="a-ucategory-chude" name="a-ucategory-chude" v-model="tenchude">
            </div>
            <div class="a-new-content">
                <b>Mô tả </b><br>
                <textarea name="a-ucategory-mota" id="a-ucategory-mota" cols="30" rows="10" placeholder="Mô tả" v-model="motachude"></textarea>
            </div>
        </div>
        <div class="course_btn" v-on:click="validatePutCategory()">
            <button>Cập nhật</button>
        </div>
    </div>
    
</template>
<script>
import BaseRequest from '@/core/BaseRequest';
export default {
    props: ['propGetCategory'], 
    data(){
        return {
            item: {}, 
            tenchude: "", 
            motachude: "", 
        }
    }, 
    methods: {
        /**
         * Hàm đóng form update form. 
         */
        closeForm: function(){
            document.getElementById("form_update_category").style.display = "none"; 
            document.getElementById("category_back").style.display = "none"; 
        }, 
        /**
         * Hàm chỉnh sửa vào api. 
         */
        validatePutCategory: function(){
            const inputTitle = document.getElementById("a-ucategory-chude"); 
            const inputMota = document.getElementById("a-ucategory-mota"); 

            const _delete = document.querySelectorAll(".error-ucategory"); 
            this.isCheck = true;
            for (const item of _delete) {
                item.parentNode.removeChild(item);
            }
            var isCheck = true; 

            inputTitle.style.borderColor = "#d0d0d0"; 
            inputMota.style.borderColor = "#d0d0d0";

            //validate rong 
            if(this.tenchude === "" || this.tenchude === undefined || this.tenchude === null){
                this.thongbao(inputTitle, "Tên chủ đề không được phép để trống."); 
                isCheck = false; 
            }

            if(this.motachude === "" || this.motachude === undefined || this.tenchude === null){
                this.thongbao(inputMota, "Mô tả chủ đề không được phép để trống."); 
                isCheck = false; 
            }

            if(isCheck){
                //chỉnh sửa vào database. 
                this.item.categoryName = this.tenchude; 
                this.item.categoryDescription = this.motachude;
                BaseRequest.put("category", this.item)
                .then(response => {
                    console.log(response.data); 
                    //thông báo chỉnh sửa thành công. 
                    document.getElementById("form_update_category").style.display = "none"; 
                    document.getElementById("category_back").style.display = "none"; 
                    document.getElementById("dialog_dangky_content").innerHTML = "Cập nhật thành công!"; 
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
            p.classList.add("error-ucategory");
            p.style.color = "red"; 
            p.style.fontStyle = "italic"; 
            p.style.fontSize = "0.9rem"; 
            item.after(p);
            //item.setAttribute("title", mes); 
        },
    }, 
    watch: {
        propGetCategory: function(){
            this.item = this.propGetCategory; 
            this.tenchude = this.propGetCategory.categoryName; 
            this.motachude = this.propGetCategory.categoryDescription; 
        }
    }
}
</script>
<style scoped>

</style>
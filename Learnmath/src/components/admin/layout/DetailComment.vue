<template>
    <div class="form-comment" id="form_detail_thread">
        <div class="form-comment-title">
            <h1>Thông tin chi tiết của bình luận</h1>
            <div class="icon_close" v-on:click="closeFormDetailComment()"></div>
        </div>
        <div class="form-comment-detail">
            <div>
                <div class="form-comment-detail-user"><b>Tài khoản: </b>
                    <img :src="imgUser" alt="">
                    <div>{{ userAccout }}</div>
                </div>
                <div><b>Bài đăng: </b> {{ threadsTitle }}</div>
                <div><b>Nội dung: </b> {{ commentContent }}</div>
                <div class="form-comment-detail-img"><b>Ảnh đính kèm:</b> <br>
                    <img v-if="loadImg" :src="imgComment" alt="">
                    <div v-else>Không có.</div>
                </div>
                <div><b>Ngày bình luận: </b>
                    {{ date }}
                </div>
            </div>
        </div>
    </div>
</template>
<script>
export default{
    props: ['propGetComment'],
    data(){
        return {
            userAccout: "", 
            threadsTitle: "", 
            commentContent: "", 
            date: "", 
            imgUser: "", 
            imgComment: "", 
            loadImg: false,
        }
    }, 
    methods: {
        closeFormDetailComment: function(){
            document.getElementById("form_detail_thread").style.display = "none"; 
            document.getElementById("comment_back").style.display = "none";
        }
    }, 
    watch: {
        propGetComment: function(){
            this.userAccout = this.propGetComment.userAccout; 
            this.threadsTitle = this.propGetComment.threadsTitle; 
            this.commentContent = this.propGetComment.commentContent; 
            this.date = this.propGetComment.created_at; 
            this.imgUser = this.propGetComment.userImage; 
            this.imgComment = this.propGetComment.commentImage; 
            if(this.imgComment === null || this.imgComment === undefined || this.imgComment === ""){
                this.loadImg = false; 
            }
            else {
                this.loadImg = true; 
            }
        }
    }
}
</script>
<style scoped>
#form_detail_thread {
    width: 50%; 
    top: 100px; 
    left: 25%; 
}
.form-comment-detail {
    padding-left: 15%; 
    padding-right: 15%; 
    font-size: 1.25rem; 
    margin-top: 24px; 
    padding-bottom: 24px; 
}
.form-comment-detail-user {
    display: flex; 
    align-items: center;
    gap: 16px; 
}
.form-comment-detail-user > img {
    width: 80px; 
    height: 80px;
}
.form-comment-detail-img > img{
    width: 200px;
    height: 200px;
    margin-left: 16px; 
}
</style>

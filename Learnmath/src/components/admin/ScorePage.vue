<template>
    <NavBar></NavBar>
    <SideBar></SideBar>
    <UpdateScore :propGetScore="getScore"></UpdateScore>
    <div>
        <div class="a-course">
        <h1>Quản lý điểm thi thử</h1>
        <div class="a-course_table">
            <div class="a-course_table-title">
                <div class="a-course_table-title-search">
                    <input type="text" placeholder="Tìm kiếm theo mã, chủ đề khóa học" id="search-score">
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
                            <td style="width: 120px;">Mã điểm thi</td>
                            <td style="width: 100px">Người làm bài</td>
                            <td style="width: 400px">Tên đề thi</td>
                            <td style="width: 250px">Ngày thi thử</td>
                            <td>Kết quả</td>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-for="(item, index) in score" :key="index">
                            <td style="width: 120px; text-align: center;">{{ item.scoreCode }}</td>
                            <td style="width: 100px;" class="score-image">
                                <img :src="item.userImage" alt="">
                            </td>
                            <td style="width: 400px; padding-right: 8px;">{{ item.lessonName }}</td>
                            <td style="width: 250px; text-align: center;">
                                {{ renderDate(item.time) }}
                            </td>
                            <td style="text-align: center;">{{ item.result }}
                                <div class="chucnang_course-style">
                                    <div class="updatehover" title="Sửa khóa học" v-on:click="openFormPutScore(item)">
                                        <div class="icon_update"></div>
                                    </div>
                                    <div class="deletehover" title="Xóa khóa học" v-on:click="deleteScore(item)">
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
                    Tổng: {{ score.length }}
                </div>
                <div style="margin-right: 24px" class="a-course_table-page1">
                    <div>Số bản ghi/trang: 
                        <select name="" id="" v-model="pageSize">
                            <option value="5">5</option>
                            <option value="10">10</option>
                            <option value="15">15</option>
                        </select>
                    </div>
                    <div class="icon_back"></div>
                    <div class="icon_next"></div>
                </div>
            </div>
        </div>
    </div>
    </div>
    <div class="course_back" id="score_back"></div>
    <CanhBao></CanhBao>
</template>
<script>
import BaseRequest from '@/core/BaseRequest'
import NavBar from './layout/NavbarAdmin.vue'
import SideBar from './layout/SidebarAdmin.vue'
import UpdateScore from './score/UpdateScore.vue'
import CanhBao from './layout/DialogCanhbao.vue'

export default {
    components: {
        NavBar, 
        SideBar, 
        UpdateScore,
        CanhBao
    }, 
    data(){
        return {
            score: [], 
            getScore: {}, 
            tg: [], 
        }
    }, 
    mounted(){
        BaseRequest.get("score_view")
        .then(response => {
            this.score = response.data; 
        })
        .catch(error => {
            console.log(error.message); 
        })
    }, 
    methods: {
        loadDataCourse: function(){
            BaseRequest.get("score_view")
            .then(response => {
                this.score = response.data; 
            })
            .catch(error => {
                console.log(error.message); 
            })
        }, 
        searchCourse: function(){
            var x = document.getElementById("search-score").value; 
            for (const item of this.score) {
                if(item.scoreCode === x){
                    this.tg.push(item); 
                }
            }
            this.score = this.tg; 
        }, 
        renderDate: function(date1){
            let dateObject = new Date(date1);

            let formattedDate = `${dateObject.getDate()}/${dateObject.getMonth() + 1}/${dateObject.getFullYear()}`;

            return formattedDate; 
        },
        openFormPutScore: function(item){
            this.getScore = item; 
            document.getElementById('form-put-score').style.display = "block"; 
            document.getElementById("score_back").style.display = "block";
        }, 
        deleteScore: function(item){
            document.getElementById("thognbao").innerHTML = "Bạn có chắc chắn muốn xóa kết quả có mã " +  item.scoreCode; 

            document.getElementById("ad-dialog-delete").style.display = "block";
            document.getElementById("ad-dialog-btn").addEventListener("click", ()=>{
                BaseRequest.delete("score?code=" + item.scoreCode)
                .then(response => {
                    console.log(response.data); 
                    document.getElementById("ad-dialog-delete").style.display = "none"; 
                    document.getElementById("dialog_score_content").innerHTML = "Xóa kết quả thành công!"; 
                    document.getElementById("dialog_score").style.opacity = "1";
                })
                .catch(error => {
                    console.log(error.message); 
                })
            })
        }
    }
}
</script>
<style scoped>
.score-image > img {
    height: 80px;
    width: 80px;
    padding-right: 8px; 
}

</style>

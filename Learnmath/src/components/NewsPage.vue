<template>
  <div class="new_form">
    <div  v-for="(item, index) in news" :key="index" class="new_style" data-aos="fade-down" data-aos-duration="1500">
      <div class="style_image_new">
        <img
            class="image_new"
            :src="item.newImage"
            alt="..."
        />
      </div>
      <div class="card-body">
        <!-- <h2 class="card-body-title">{{item.newTitle}}</h2> -->
        <router-link :to="/news/ +item.newCode" class="card-body-title">{{item.newTitle}}</router-link>
        <div class="card-body-create">
          <div>{{ item.createdBy }}</div>
          <div class="card-body-date">{{ new Date(item.newDate).toLocaleString() }}</div>
        </div>
      </div>
    </div>
    <div class="new-page">
      <div>Tổng: 10</div>
      <div class="a-course_table-page1">
          <div>Số bản ghi/trang: 
              <select name="" id="" v-model="page">
                  <option value="5">5</option>
                  <option value="10">10</option>
                  <option value="15">15</option>
              </select>
          </div>
          <div class="icon_back" v-on:click="backPageNew(pagenow)"></div>
          <div class="icon_next" v-on:click="nextPageNew(pagenow)"></div>
      </div>
    </div>
  </div>
  <ChatBot></ChatBot>
</template>
<script>
//import Pagination from "laravel-vue-pagination";
import BaseRequest from "@/core/BaseRequest";
import ChatBot from './mathjax/ChatBox.vue'
export default {
  components: {
    ChatBot, 
  }, 
  data() {
    return {
      newss:null,
      news: [],
      page: 5,  
      pagenow: 1,
    };
  },
  mounted() {
    BaseRequest.get("news/page?page=" + this.pagenow + "&pageSize=" + this.page)
      .then(response => (
        this.news = response.data 
      ))
      .catch((error) =>{
          console.log(error);
      })
  },
  watch: {
    page: function(){
      BaseRequest.get("news/page?page=" + this.pagenow + "&pageSize=" + this.page)
      .then(response => (
        this.news = response.data 
      ))
      .catch((error) =>{
          console.log(error);
      })
    }
  }, 
  methods: {
    backPageNew: function(item){
      item--; 
      BaseRequest.get("news/page?page=" + item + "&pageSize=" + this.page)
      .then(response => (
        this.news = response.data 
      ))
      .catch((error) =>{
          console.log(error);
      })
    },
    nextPageNew: function(item){
      item++; 
      BaseRequest.get("news/page?page=" + item + "&pageSize=" + this.page)
      .then(response => (
        this.news = response.data 
      ))
      .catch((error) =>{
          console.log(error);
      })
    }, 
    loaddata : function() {
      BaseRequest.get("news")
      .then(response => (
        this.news = response.data.new 
      ))
      .catch((error) =>{
          console.log(error);
      })
    },
  },
}

</script>
<style scoped>
.new-page {
  width: 60%; 
  display: flex; 
  justify-content: space-between;
  margin: auto; 
  border-top: 1px solid #d0d0d0; 
  padding-top: 16px; 
}
.new_form {
  padding-bottom: 80px;
  padding-top: 100px;  
}
.page-link {
  height :40px;
  width :40px;
}
.new-form {
  margin-bottom: 100px; 
}
.new_style {
  width: 60%; 
  display: flex !important;; 
  justify-content: space-between;
  align-items: center;
  gap: 30px; 
  margin: auto; 
  margin-bottom: 40px;  
}
.style_image_new {
  width: 30%; 
}
.image_new {
  width: 100%; 
  height: 150px;
}
.card-body {
  width: 65%;
}
.card-body-title {
  color: #0a0a23; 
  font-size: 1.5rem; 
  font-weight: 700;
  text-decoration: none;
}
.card-body-title:hover {
  text-decoration: underline;
}
.card-body-create {
  width: 100%; 
  display: flex; 
  justify-content: space-between;
  align-items: center;
}
</style>

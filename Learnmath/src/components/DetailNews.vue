<template>
  <div class="style_new_page">
    <div class="row">
      <div class="">
        <!-- Post content-->
        <article>
          <!-- Post header-->
          <header class="mb-4">
            <div class="text-muted mb-2">
             {{new Date(news.newDate).toLocaleString()}}
            </div>
            <!-- Post title-->
            <h1 class="fw-bolder mb-1">{{news.newTitle}}</h1>
            <!-- Post meta content-->
            <!-- Post categories-->
            
          </header>
          <!-- Preview image figure-->
          <figure class="mb-4">
            <img
              class="style_imgnew_page"
              :src="news.newImage"
              alt="..."
            />
          </figure>
          <!-- Post content-->
          <section class="mb-5" v-html="news.newContent">
            
          </section>
        </article>
        <!-- Comments section-->
        
      </div>
      <!-- Side widgets-->
      <div class="">
        
      </div>
    </div>
  </div>
  <ChatBot></ChatBot>
</template>
<script>
import BaseRequest from "../core/BaseRequest"
import ChatBot from "./mathjax/ChatBox.vue"
export default {
     data(){
        return{
            news: {},
        }
    },
    components: {ChatBot}, 
    mounted() {
        this.details();
    },
    methods:{
       details:function(){
           BaseRequest.get("news/code?code="+this.$route.params.id)
           .then((response)=>{
               this.news = response.data
                if(this.news.newCode==null){
                   this.$router.push({ name: "404" });
               }
           })
           .catch((error) => {
              if(error.response.status === 442){
                this.$router.push({ name: "404" });
              }
           })
       },
    }
}
</script>
<style>
.style_new_page {
  width: 100% !important;
}
.row {
  margin: auto; 
  margin-top: 150px; 
  width: 60% !important; 
}
.text-muted {
  color: #3b3b4f; 
  font-size: 1.4rem;
  font-weight: 600;
}
.mb-1 {
  color: #0a0a23; 
  font-size: 3rem;
  font-weight: 700;
}
.mb-5 {
  font-size: 1.5rem;
  line-height: 1.6;
}
.style_imgnew_page {
  width: 100%; 
  height: 450px;
  margin-bottom: 40px; 
}
</style>
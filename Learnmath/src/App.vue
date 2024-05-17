<template>
<link href='https://fonts.googleapis.com/css?family=Lato:400,700' rel='stylesheet' type='text/css'>
   <div id="app">
    <Navbar></Navbar>
    <router-view></router-view>
    <Footer></Footer>
    
   </div>
</template>

<script>
import Navbar from './components/layout/NavbarPage.vue'
import Footer from './components/layout/FooterPage.vue'
export default {
  name: "App",
  components: {
    Navbar, 
    Footer, 
  },
};
import 'jquery/src/jquery.js';
</script>

<style>

#app {
  font-family: 'Lato', sans-serif;
  font-weight: 400; 
  scroll-behavior: smooth;
}

@import "~bootstrap/dist/css/bootstrap.css";
@import "~sweetalert2/dist/sweetalert2.min.css";
</style>



// import Login from './components/LoginPage.vue'
// import Register from './components/RegisterUser.vue'
// import Home from './components/HomePage.vue'
// import Product from './components/ProductStore.vue'
// import DetailProduct from './components/DetailProduct.vue'
// import Tuvan from './components/TuvanPage.vue'
// import News from './components/NewsPage.vue'
// import DetailNews from './components/DetailNews.vue'
// import Profile from './components/ProfileUser.vue'
// import Bank from './components/BankPage.vue'
// import Checkout from './components/CheckoutPage.vue'
// import Feedback from './components/FeedbackPage.vue'
// import notfound from './components/layout/V404Page.vue'

import 'bootstrap'
// import AOS from 'aos'
import 'aos/dist/aos.css'
import "sweetalert2/dist/sweetalert2.min.css"

import { createApp } from 'vue'
import App from './App.vue'; 


import router from './router.js'; 


const app = createApp(App)
app.use(router)
app.mount('#app')

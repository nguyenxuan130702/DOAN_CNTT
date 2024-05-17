<template>
<!--Màn hình tổng quan trang quản trị-->
    <div>
        <NavbarAdmin></NavbarAdmin>
        <Sidebar></Sidebar>
        <HomeAdmin></HomeAdmin>
    </div>
</template>
<script>
import NavbarAdmin from './layout/NavbarAdmin.vue'
import Sidebar from './layout/SidebarAdmin.vue'
import HomeAdmin from './layout/HomeAdmin.vue'
export default{
    components: {
        NavbarAdmin, 
        Sidebar, 
        HomeAdmin, 
    }
}
</script>
<style scoped>

</style>
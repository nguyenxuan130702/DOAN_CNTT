<template>
    <div class="ad-dialog-delete" id="ad-dialog-delete">
        <div class="ad-dialog">
            <div class="ad-dialog-main">
                <div class="ad-dialog-title">
                    <div id="dialog-title">Cảnh báo</div>
                    <div class="icon_close" v-on:click="closeDialogDelete()"></div>
                </div>
                <div class="ad-dialog-content">
                    <div class="icon_cauhoi"></div>
                    <div id="thognbao">Bạn có chắc chắn muốn xóa đối tượng</div>
                </div>
            </div>
            <div class="ad-dialog-btn" id="ad-dialog-btn">
                <button>Đồng ý</button>
            </div>
        </div>

        <div class="ad-dialog-back" id="ad-dialog-back"></div>
    </div>
</template>
<script>
export default {
    methods: {
        closeDialogDelete: function(){
            document.getElementById("ad-dialog-delete").style.display= "none"; 
        },
    }
}
</script>
<style>
.ad-dialog {
    position: absolute; 
    background-color: #fff; 
    padding: 24px;
    top: 200px; 
    left: 35%; 
    border-radius: 4px;
    box-shadow: rgba(0,0,0,0.078) 0px 3px 6px 0px; 
    z-index: 1;
}
.ad-dialog-delete {
    display: block; 
    position: relative;
    width: 100%; 
    height: 100vh; 
    display: none; 
}
.ad-dialog-main {
    width: 100%; 
}
.ad-dialog-title {
    display: flex; 
    justify-content: space-between;
    align-items: center;
}
.ad-dialog-title > div:first-child {
    font-size: 2.5rem; 
    font-weight: 700;
}
.icon_cauhoi {
    background: url('@/assets/Sprites.64af8f61.svg') no-repeat -752px -462px;
	width: 36px;
	height: 36px;
}
.dialog_delete-title {
    display: flex; 
    gap: 16px; 
    font-size: 1.25rem; 
    margin-top: 24px; 
}
.ad-dialog-content {
    display: flex;
    gap: 16px; 
    align-items: center;
    font-size: 1.25rem; 
    margin-top: 24px; 
    margin-bottom: 24px; 
}
.ad-dialog-btn {
    display: flex; 
    justify-content: flex-end;
    border-top: 1px solid #d0d0d0; 
}
.ad-dialog-btn > button {
    height: 36px;
    padding-left: 16px; 
    padding-right: 16px; 
    border: 1px solid #d0d0d0; 
    background-color: #50B83C; 
    color: #fff; 
    border-radius: 5px;
    margin-top: 24px; 
}
.ad-dialog-back {
    position: absolute; 
    top: 0; 
    right: 0; 
    left: 0; 
    bottom: 0; 
    background-color: #0a0a23;
    opacity: 0.4;
} 
</style>

<template>
  <div ref="mathjax"></div>
</template>

<script>
export default {
  props: ['formula'],
  mounted() {
    this.$nextTick(() => {
      this.renderMath();
      if (window.MathJax) {
        window.MathJax.typeset(); // Kích hoạt MathJax để xử lý lại công thức toán học
      }
    });
  },
  methods: {
    renderMath() {
      this.$refs.mathjax.innerHTML = this.formula;
      if (this.$MathJax) {
        this.$MathJax.typesetPromise([this.$refs.mathjax])
          .catch((err) => console.log('Error rendering MathJax:', err));
      }
    }
  }
}
</script>
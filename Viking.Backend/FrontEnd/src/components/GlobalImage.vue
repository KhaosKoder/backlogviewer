<template>
  <img src="@/assets/Viking.png" alt="Viking Image" class="global-image" />
</template>

<script>
export default {
  name: 'GlobalImage'
}
</script>

<style scoped>
.global-image {
  position: fixed;
  top: 0;
  right: 0;
  opacity: 0.5;
  z-index: 3000;  /* Lower z-index allows other content to overlap with the Viking image */
  width: 200px;  /* Set width to 200px */
  height: 200px;  /* Set height to 200px */
}
</style>

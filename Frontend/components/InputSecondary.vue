<script setup>
const model = defineModel();
</script>
<template>
  <input class="border rounded-full focus:outline-none focus:border-orange-600 px-2"
         v-model="model">
</template>
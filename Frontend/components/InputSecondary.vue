<script setup>
const modelValue = defineModel();
</script>
<template>
  <input class="border rounded-full focus:outline-none focus:border-orange-600 px-2"
         v-model="modelValue">
</template>